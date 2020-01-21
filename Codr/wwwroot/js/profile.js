'use strict';

const names_cache = {};
const responding_to = {};

function escapeHtml(unsafe) {
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
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
    if (document.getElementById(`post-${postId}`)) {
        const formData = new FormData();
        formData.append("id", postId);
        console.log(postId);
        const comments = await loadComments(postId);
        console.log(comments);
        const container = document.getElementById(`comment-container-${postId}`);
        while (container.firstChild)
            container.firstChild.remove();

        for (const value of comments) {
            console.log(value.depth);
            const node = document.createElement("div");
            node.classList.add("comment");
            const setHtml = (content, name) =>
                node.innerHTML = `<p>${content}</p><p>Posted by <a href="/App/Profile?id=${value.Author}">${name}</a>at ${value.Created}</p>
<button onclick='responding_to["${postId}"] = "${value.Id}"'>Reply</button>`;

            const name = await getName(value.Author);
            const left = value.depth * 50;
            node.style.left = `${left}px`;
            node.style.width = `calc(100% - ${left}px)`;
            setHtml(value.Content, name);
            container.appendChild(node);
        }
    }
}