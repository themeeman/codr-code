'use strict';
function postComment(postId) {
    const content = document.getElementById(`comment-box-${postId}`).value;
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
        let formData = new FormData();
        formData.append("id", postId);
        console.log(postId);
        fetch(`/App/GetComments?id=${postId}`)
            .then(response => response.json())
            .then(comments => {
                comments.forEach(value => {
                    let node = document.createElement("div");
                    const setHtml = (content, name) =>
                        node.innerHTML = `<p>${content}</p><p>Posted by <a href="/App/Profile?id=${value.author}">${name}</a></p>`;

                    if (names_cache[value.author])
                        setHtml(value.content, names_cache[value.author]);
                    else
                        fetch(`/App/GetName?id=${value.author}`)
                            .then(j => j.json())
                            .then(name => {
                                names_cache[value.author] = name;
                                setHtml(value.content, name);
                            });
                    
                    document.getElementById(`comment-container-${postId}`).appendChild(node);
                });
            })
            .catch(err => console.log(err));
    }
}