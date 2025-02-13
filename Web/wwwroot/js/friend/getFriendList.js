let isLoadingFriends = false;

function getFriendList(userId) {
    if (isLoadingFriends) return;
    isLoadingFriends = true;
    $.ajax({
        url: `/Friend/GetFriendList?userTag=${userId}`,
        type: 'GET',
        success: function (data) {
            if (data && data.friends.$values.length > 0) {
                data.friends.$values.forEach(friend => {
                    $('.friends-list').append(`
                        <div class="friend-card card mb-3" id="friend-card-${friend.userTag}">
                            <div class="card-body d-flex align-items-center">
                                <a class="text-decoration-none text-dark" href="/Profile?userTag=${friend.userTag}">
                                    <img src="${friend.userImagePath}" class="rounded-circle me-3" alt="${friend.userName}'s avatar" width="50" height="50" />
                                </a>
                                <div class="flex-grow-1">
                                    <a class="text-decoration-none text-dark" href="/Profile?userTag=${friend.userTag}">
                                        <h5 class="mb-0">${friend.userName}</h5>
                                    </a>
                                    <small class="text-muted">friend</small>
                                </div>
                                <button class="btn btn-outline-danger btn-sm" onclick="removeFriend('${friend.userTag}')">
                                    <i class="fas fa-user-minus"></i> Удалить из друзей
                                </button>
                            </div>
                        </div>
                    `);
                });
            } else {
                $('.friends-list').append('<p class="text-center">Пора искать новых друзей!</p>');
            }
        },
        error: function (xhr, status, error) {
            console.error('Error loading friends:', error);
        },
        complete: function () {
            isLoadingFriends = false;
        }
    });
}
