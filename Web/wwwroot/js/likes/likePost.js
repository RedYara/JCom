function likePost(postId) {
    $.ajax({
        url: `/like/likepost`,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ postId: postId }),
        success: function (data) {
            const likesCount = document.querySelector(`#likes-${postId}`);
            const currentCount = parseInt(likesCount.textContent.match(/\d+/)[0]) || 0;

            if (likesCount.classList.contains("btn-outline-danger")) {
                likesCount.innerHTML = `<i class="far fa-thumbs-up"></i> Нравится (${currentCount + 1})`;
                likesCount.classList.remove("btn-outline-danger");
                likesCount.classList.add("btn-danger");
            }
            else {
                likesCount.innerHTML = `<i class="far fa-thumbs-up"></i> Нравится (${currentCount - 1})`;
                likesCount.classList.remove("btn-danger");
                likesCount.classList.add("btn-outline-danger");

            }


        },
        error: function (xhr, status, error) {

        },
        complete: function () {
        }
    });
}
