let page = 0;
let isLoading = false;

function getUserPosts(userTag) {
    if (isLoading) return;
    isLoading = true;
    $.ajax({
        url: `/profile/posts/loadposts?userTag=${userTag}&page=${page}`,
        type: 'GET',
        success: function (data) {
            if (data && data.$values.length > 0) {
                data.$values.forEach(post => {
                    $('#newsFeed').append(`
                        <div class="card post-card mb-3">
                            <div class="card-body">
                                <div class="d-flex align-items-center mb-3">
                                    <a href="/Profile?id=${post.userPostedTag}">
                                        <img loading="lazy" src="/${post.userImage}" style="width: 60px; height: 60px;" class="rounded-circle me-3 img-thumbnail" alt="Profile">
                                    </a>
                                    <div class="d-flex justify-content-between align-items-center w-100">
                                        <div>
                                            <a href="/Profile?id=${post.userPostedTag}" style="text-decoration: none; color: inherit;">
                                                <h6 class="mb-0">${post.userName}</h6>
                                            </a>
                                            <small title="${new Date(post.postDate).toLocaleString()}" class="text-muted">${post.postDateHumanized} назад</small>
                                        </div>
                                        ${post.userId === userTag ? `
                                        <button class="btn btn-outline-danger btn-sm ms-2" onclick="deletePost(${post.id})">
                                            <i class="fas fa-times"></i>
                                        </button>
                                        ` : ''}
                                    </div>
                                </div>

                                <p>${post.text}</p>
                                <div class="d-flex justify-content-between">
                                    <button id="likes-${post.postId}" class="btn ${post.isLiked ? "btn-danger" : "btn-outline-danger"} btn-sm" onclick="likePost('${post.postId}','${userTag}')">
                                        <i class="far fa-thumbs-up"></i> Нравится (${post.likesCount})
                                    </button>
                                    <button class="btn btn-outline-primary btn-sm" data-bs-toggle="collapse"
                                    data-bs-target="#comments-${post.postId}" onclick="loadComments('${post.postId}', '${userTag}')">
                                        <i class="far fa-comment"></i> Комментировать (${post.commentsCount ?? 0})
                                    </button>
                                    <button class="btn btn-outline-secondary btn-sm">
                                        <i class="fas fa-share"></i> Поделиться
                                    </button>
                                </div>

                                    
                                <div class="collapse" id="comments-${post.postId}">
                                <div class="mt-4">
                                    <!-- Список комментариев -->
                                    <div class="comments-list mb-3" id="comments-list-${post.postId}">
                                        <!-- Комментарии будут загружены сюда -->
                                        <div class="text-center py-3">
                                            <div class="spinner-border text-primary" role="status">
                                                <span class="visually-hidden">Загрузка...</span>
                                            </div>
                                        </div>
                                    </div>
                                        
                                    <!-- Форма нового комментария -->
                                    <div class="comment-form">
                                        <div class="input-group">
                                            <textarea class="form-control" placeholder="Напишите комментарий..."
                                                rows="2"></textarea>
                                            <button class="btn btn-primary" type="button"
                                                onclick="addComment(${post.postId}, '${userTag}')">
                                                <i class="fas fa-paper-plane"></i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            </div>
                        </div>


                    `);
                });
                page++;
                $('#loadMorePosts').show();
            } else {
                $('#loadMorePosts').hide();
                $('#noMorePostsMessage').show();
            }
        },
        error: function (xhr, status, error) {
            console.error('Ошибка при загрузке постов:', error);
        },
        complete: function () {
            isLoading = false;
        }
    });
}
