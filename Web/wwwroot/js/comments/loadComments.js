function loadComments(postId, userId, skipActive) {
    const commentsList = document.getElementById(`comments-list-${postId}`);
    if (!skipActive) {
        if (!commentsList.classList.contains("active")) {
            commentsList.classList.add("active");
        }
        else {
            commentsList.classList.remove("active");
            return;
        }
    }

    commentsList.innerHTML = `
    <div class="text-center py-3">
        <div class="spinner-border text-primary" role="status">
            <span class="visually-hidden">Загрузка...</span>
        </div>
    </div>
`;

    $.ajax({
        url: `/comment/getpostcomments?postId=${postId}`,
        method: 'GET',
        success: function (response) {
            if (response.$values.length > 0) {
                commentsList.innerHTML = '';
                response.$values.forEach(comment => {
                    const commentHtml = `
                        <div class="comment mb-3" id="comment-${comment.commentId}">
                            <div class="d-flex align-items-start">
                                <img src="/${comment.userImagePath}" 
                                     class="rounded-circle me-2" 
                                     width="40" 
                                     height="40" 
                                     alt="User">
                                <div class="ms-2">
                                    <div class="d-flex align-items-center">
                                        <strong class="me-2">${comment.userName}</strong>
                                        <small title="${comment.commentDate}" class="text-muted">${comment.commentDateHumanized} назад</small>
                                        ${comment.userId === userId || comment.userPostedId === userId ? '<button class="btn btn-danger btn-sm ms-2" onclick="deleteComment(' + comment.commentId + ', \'' + userId + '\',' + comment.postId + ')">✖</button>' : ''}
                                    </div>
                                    <p class="mb-0">${comment.text}</p>
                                </div>
                            </div>
                        </div>
                    `;

                    commentsList.insertAdjacentHTML('beforeend', commentHtml);
                });
            } else {
                commentsList.innerHTML = `
                <div class="text-center py-3 text-muted">
                    Пока нет комментариев. Будьте первым!
                </div>
            `;
            }
        },
        error: function () {
            commentsList.innerHTML = `
            <div class="text-center py-3 text-danger">
                Ошибка при загрузке комментариев. Попробуйте позже.
            </div>
        `;
        }
    });
}