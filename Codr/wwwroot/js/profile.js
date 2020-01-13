'use strict';
function escapeHtml(unsafe) {
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}

function postComment(postId) {
    const content = document.getElementById(`comment-box-${postId}`).value;
    if (!content) {
        alert("Comment must not be empty");
        return;
    }
    document.getElementById(`comment-box-${postId}`).value = "";
    let formData = new FormData();
    formData.append("postId", postId);
    formData.append("content", content);
    fetch("/App/Profile/AddComment", {
        method: "POST",
        body: formData
    }).then(() => loadComments(postId));
}

const names_cache = {};

async function loadComments(postId) {
    var post = document.getElementById(`post-${postId}`);

    if (post) {
        const formData = new FormData();
        formData.append("id", postId);
        console.log(postId);
        fetch(`/Api/GetReplies?id=${postId}`)
            .then(response => response.json())
            .then(comments => {
                comments.forEach(c => c.Content = escapeHtml(c.Content)); return comments;
            })
            .then(comments => {
                const container = document.getElementById(`comment-container-${postId}`);
                while (container.firstChild) {
                    container.removeChild(container.firstChild);
                }
                comments.forEach(value => {
                    console.log(value);
                    const node = document.createElement("div");
                    node.classList.add("comment");
                    console.log(value.Created);
                    const setHtml = (content, name) =>
                        node.innerHTML = `<p>${content}</p><p>Posted by <a href="/App/Profile?id=${value.Author}">${name}</a> at ${value.Created}</p>`;

                    if (names_cache[value.Author])
                        setHtml(value.Content, names_cache[value.Author]);
                    else
                        fetch(`/Api/GetName?id=${value.Author}`)
                            .then(j => j.json())
                            .then(name => {
                                names_cache[value.Author] = name;
                                setHtml(value.Content, name);
                            });
                    
                    container.appendChild(node);
                });
            })
            .catch(err => console.log(err));
    }
}