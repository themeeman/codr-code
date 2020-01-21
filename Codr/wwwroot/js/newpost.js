const working_post = [];

function escapeHtml(unsafe) {
    for (const key of Object.keys(unsafe)) {
        if (typeof unsafe[key] === "string")
            unsafe[key] = unsafe[key]
                .replace(/&/g, "&amp;")
                .replace(/</g, "&lt;")
                .replace(/>/g, "&gt;")
                .replace(/"/g, "&quot;")
                .replace(/'/g, "&#039;");
    }
}

window.onload = () => {
    const els = document.getElementsByClassName("post-component-form");
    console.log(els);
    Array.from(els).forEach(
        element => element.addEventListener('submit', e => {
            e.preventDefault();
            var o = {Type: e.target.name};
            new FormData(e.target).forEach((value, name) => o[name] = value);
            //escapeHtml(o);
            console.log(o);
            working_post.push(o);
            fetch(`/Post/Render?components=${JSON.stringify(working_post)}`)
                .then(response => response.text())
                .then(post => {
                    var container = document.getElementById("profile-posts");
                    while (container.firstChild)
                        container.firstChild.remove();
                    container.innerHTML = post;
                });
        })
    );
};

function submitPost() {
    if (!working_post.length) {
        alert("Post cannot be empty");
        return;
    }
    document.getElementById("submit-post-button").disabled = true;
    var data = new FormData();
    data.append("components", encodeURIComponent(JSON.stringify(working_post)));
    console.log(data.get("components"));
    fetch(`/App/Profile/NewPost`, {
        method: "POST",
        body: data
    }).then(() => window.location.href = "/App/Profile");
}