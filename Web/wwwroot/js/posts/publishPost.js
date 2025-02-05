function publishPost(userId, userImage, userName) {
    var text = $('#feedTextArea').val();

    if (!text) {
        alert("Пожалуйста, введите текст поста.");
        return;
    }

    $.ajax({
        url: '/posts/publishpost',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ userId: userId, text: text }),
        success: function() {
            $('#newsFeed').prepend(`
                <div class="card post-card mb-3">
                    <div class="card-body">
                        <div class="d-flex align-items-center mb-3">
                            <img src="${userImage}" style="width: 60px; height: 60px;" class="rounded-circle me-3 img-thumbnail" alt="Profile">
                            <div class="d-flex justify-content-between align-items-center w-100">
                                <div>
                                    <h6 class="mb-0">${userName}</h6>
                                    <small class="text-muted">Только что</small>
                                </div>
                                <button class="btn btn-outline-danger btn-sm ms-2" onclick="deletePost(${postId})">
                                    <i class="fas fa-times"></i>
                                </button>
                            </div>
                        </div>
                        <p>${text}</p>
                        <div class="d-flex justify-content-between">
                            <button class="btn btn-outline-danger btn-sm">
                                <i class="far fa-thumbs-up"></i> Нравится (0)
                            </button>
                            <button class="btn btn-outline-primary btn-sm">
                                <i class="far fa-comment"></i> Комментировать (0)
                            </button>
                            <button class="btn btn-outline-secondary btn-sm">
                                <i class="fas fa-share"></i> Поделиться
                            </button>
                        </div>
                    </div>
                </div>

            `);

            var postCount = $('#newsFeed > div').length;
            if (postCount > 5) {
                $('#newsFeed > div:last-child').remove(); // Удаляем последний элемент
            }

            $('#feedTextArea').val('');
        },
        error: function(xhr, status, error) {
            console.error('Ошибка при публикации поста:', error);
        }
    });
}
