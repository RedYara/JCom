
function addComment(postId, userId) {
    const commentText = document.querySelector(`#comments-${postId} textarea`).value;
    if (!commentText.trim()) return;

    $.ajax({
        url: '/comment/AddComment',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            postId: postId,
            text: commentText,
            userId: userId
        }),
        success: function (response) {
            loadComments(postId, userId, true);

            document.querySelector(`#comments-${postId} textarea`).value = '';

            const commentsCount = document.querySelector(`[data-bs-target="#comments-${postId}"]`);
            const currentCount = parseInt(commentsCount.textContent.match(/\d+/)[0]) || 0;
            commentsCount.innerHTML = `<i class="far fa-comment"></i> Комментировать (${currentCount + 1})`;
        },
        error: function (a, b, c) {
            alert('Ошибка при добавлении комментария');
        }
    });
}