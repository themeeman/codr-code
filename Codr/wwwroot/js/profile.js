function postComment(postId) {
    const content = document.getElementById(`comment-box-${postId}`).value;
    let formData = new FormData();
    formData.append("postId", postId);
    formData.append("content", content);
    fetch("/App/Profile/AddComment", {
        method: "POST",
        body: formData
    });
}

async function loadComments(postId) {
}