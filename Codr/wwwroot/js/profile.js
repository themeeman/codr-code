'use strict';

const names_cache = {};
const responding_to = {};
let _is_friend = undefined;

function escapeHtml(unsafe) {
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}

function isFriend(lhs, rhs) {
    if (_is_friend !== undefined) {
        return new Promise(resolve => resolve(_is_friend));
    } else {
        return fetch(`/Api/IsFriend?lhs=${lhs}&rhs=${rhs}`)
            .then(r => r.json())
            .then(i => {
                _is_friend = i;
                return i;
            });
    }
}

async function updateFriendButton(loggedIn, profile) {
    const container = document.getElementById("profile-sidebar");
    if (document.getElementById("friend-button")) {
        document.getElementById("friend-button").remove();
    }
    if (loggedIn === profile) {
        return;
    }
    const button = document.createElement("button");
    button.id = "friend-button";
    const f = await isFriend(loggedIn, profile);
    if (f) {
        button.innerText = "Remove Friend";
        button.onclick = () => removeFriend(loggedIn, profile);
    } else {
        button.innerText = "Add Friend";
        button.onclick = () => addFriend(loggedIn, profile);
    }
    container.appendChild(button);
}

async function addFriend(loggedIn, profile) {
    _is_friend = true;
    const data = new FormData();
    data.append("lhs", loggedIn);
    data.append("rhs", profile);
    fetch("/Api/AddFriend", {
        method: "POST",
        body: data
    }).then(() => updateFriendButton(loggedIn, profile));
}

async function removeFriend(loggedIn, profile) {
    _is_friend = false;
    const data = new FormData();
    data.append("lhs", loggedIn);
    data.append("rhs", profile);
    fetch("/Api/RemoveFriend", {
        method: "POST",
        body: data
    }).then(() => updateFriendButton(loggedIn, profile));
}

async function postComment(postId) {
    const content = document.getElementById(`comment-box-${postId}`).value;
    if (!content) {
        alert("Comment must not be empty");
        return;
    }
    document.getElementById(`comment-box-${postId}`).value = "";
    let formData = new FormData();
    formData.append("postId", responding_to[postId] ? responding_to[postId] : postId);
    responding_to[postId] = undefined;
    formData.append("content", content);
    fetch("/App/Profile/AddComment", {
        method: "POST",
        body: formData
    }).then(() => displayComments(postId));
}


async function getName(userId) {
    if (names_cache[userId])
        return new Promise(resolve => resolve(names_cache[userId]));
    else
        return fetch(`/Api/GetName?id=${userId}`)
            .then(j => j.json())
            .then(name => {
                names_cache[userId] = name;
                return name;
            })
            .catch(err => console.log(err));
}

async function loadComments(postId, depth = 0) {
    const result = [];
    const add_depth = comment => { return { depth: depth, ...comment }; };
    return fetch(`/Api/GetReplies?id=${postId}`)
        .then(r => r.json())
        .then(async (replies) => {
            for (const comment of replies) {
                comment.Content = escapeHtml(comment.Content);
                result.push(add_depth(comment));
                result.push(
                    ...await loadComments(comment.Id, depth + 1)
                );
            }
            return result;
        })
        .catch(err => console.log(err));
}

async function displayComments(postId) {
    const comments = await loadComments(postId);
    const container = document.getElementById(`comment-container-${postId}`);
    while (container.firstChild)
        container.firstChild.remove();

    for (const value of comments) {
        const node = document.createElement("div");
        node.classList.add("comment");

        const name = await getName(value.Author);
        const left = value.depth * 50;
        const content = value.Content;
        node.innerHTML = `<p>${content}</p><p>Posted by <a href="/App/Profile?id=${value.Author}">${name}</a> at ${value.Created}</p>`;

        const button = document.createElement("button");
        button.onclick = () => responding_to[postId] = value.Id;
        button.innerText = "Reply";
        node.appendChild(button);

        node.style.left = `${left}px`;
        node.style.width = `calc(100% - ${left}px)`;
        container.appendChild(node);
    }
}