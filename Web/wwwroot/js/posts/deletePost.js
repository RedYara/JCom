function deletePost(userId, postId) {
    Swal.fire({
        title: 'Вы уверены?',
        text: 'Этот пост будет удален!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Да, удалить!',
        cancelButtonText: 'Отмена'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/posts/deletepost',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ userId: userId, postId: postId }),
                success: function (data) {
                    const postCardElement = document.querySelector(`#post-card-${postId}`);
                    if (postCardElement) {
                        postCardElement.remove();
                        Swal.fire('Удалено!', 'Пост успешно удален.', 'success');
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire('Ошибка сети!', 'Попробуйте позже.', 'error');
                }
            });
        }
    });
}
