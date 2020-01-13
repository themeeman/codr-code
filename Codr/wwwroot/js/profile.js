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
                    node.innerText = value.content;
                    document.getElementById(`post-${postId}`).appendChild(node);
                });
            })
            .catch(err => console.log(err));
    }
}