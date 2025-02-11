function deleteComment(commentId, userId, postId) {
    Swal.fire({
        title: 'Вы уверены?',
        text: 'Этот комментарий будет удален!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Да, удалить!',
        cancelButtonText: 'Отмена'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/comment/removecomment',
                method: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({
                    commentId: commentId,
                    userId: userId,
                    postId: postId
                }),
                success: function (response) {
                    const commentElement = document.querySelector(`#comment-${commentId}`);
                    if (commentElement) {
                        commentElement.remove();
                        Swal.fire('Удалено!', 'Комментарий успешно удален.', 'success');

                        const commentsCount = document.querySelector(`[data-bs-target="#comments-${postId}"]`);
                        const currentCount = parseInt(commentsCount.textContent.match(/\d+/)[0]) || 0;
                        commentsCount.innerHTML = `<i class="far fa-comment"></i> Комментировать (${currentCount - 1})`;

                        if (currentCount - 1 <= 0) {
                            const commentsList = document.getElementById(`comments-list-${postId}`);
                            commentsList.innerHTML = `
                <div class="text-center py-3 text-muted">
                    Пока нет комментариев. Будьте первым!
                </div>
            `;
                        }
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire('Ошибка сети!', 'Попробуйте позже.', 'error');
                }
            });
        }
    });
}