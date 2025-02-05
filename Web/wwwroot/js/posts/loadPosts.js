let page = 0;
let isLoading = false;

function loadPosts(userId) {
    if (isLoading) return; 
    isLoading = true;
    $.ajax({
        url: `/posts/loadposts?userId=${userId}&page=${page}`,
        type: 'GET',
        success: function(data) {
            if (data && data.$values.length > 0) {
                data.$values.forEach(post => {
                    $('#newsFeed').append(`
                        <div class="card post-card mb-3">
                            <div class="card-body">
                                <div class="d-flex align-items-center mb-3">
                                    <img src="${post.userImage}" style="width: 60px; height: 60px;" class="rounded-circle me-3 img-thumbnail" alt="Profile">
                                    <div class="d-flex justify-content-between align-items-center w-100">
                                        <div>
                                            <h6 class="mb-0">${post.userName}</h6>
                                            <small class="text-muted">${new Date(post.postDate).toLocaleString()}</small>
                                        </div>
                                        ${post.userId === userId ? `
                                        <button class="btn btn-outline-danger btn-sm ms-2" onclick="deletePost(${post.id})">
                                            <i class="fas fa-times"></i>
                                        </button>
                                        ` : ''}
                                    </div>
                                </div>
                                <p>${post.text}</p>
                                <div class="d-flex justify-content-between">
                                    <button class="btn btn-outline-danger btn-sm">
                                        <i class="far fa-thumbs-up"></i> Нравится (${post.likesCount})
                                    </button>
                                    <button class="btn btn-outline-primary btn-sm">
                                        <i class="far fa-comment"></i> Комментировать (${post.commentsCount ?? 0})
                                    </button>
                                    <button class="btn btn-outline-secondary btn-sm">
                                        <i class="fas fa-share"></i> Поделиться
                                    </button>
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
        error: function(xhr, status, error) {
            console.error('Ошибка при загрузке постов:', error);
        },
        complete: function(){
            isLoading = false;
        }
    });
}
