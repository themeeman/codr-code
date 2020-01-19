const working_post = [];
window.onload = () => {
    const els = document.getElementsByClassName("post-component-form");
    console.log(els);
    Array.from(els).forEach(
        element => element.addEventListener('submit', e => {
            e.preventDefault();
            var o = {Type: e.target.name};
            new FormData(e.target).forEach((value, name) => o[name] = value);
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
    var data = new FormData();
    data.append("components", JSON.stringify(working_post));
    fetch(`/App/Profile/NewPost`, {
        method: "POST",
        body: data
    }).then(() => window.location.href = "/App/Profile");
}