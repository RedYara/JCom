let isLoadingUserFriends = false;
let isLoadingUserSubscriptions = false;

function getUserFriendList(currentUserTag, userTag) {
    if (isLoadingUserFriends) return;
    isLoadingUserFriends = true;
    $.ajax({
        url: `/Friend/GetFriendList?userTag=${userTag}`,
        type: 'GET',
        success: function (data) {
            if (data && data.friends.$values.length > 0) {
                $('.friends-list').empty(); // Очистить список друзей перед добавлением новых
                data.friends.$values.forEach(friend => {
                    $('.friends-list').append(`
                        <div class="friend-card card mb-3" id="friend-card-${friend.userTag}">
                            <div class="card-body d-flex align-items-center">
                                <a class="text-decoration-none text-dark" href="/Profile?id=${friend.userTag}">
                                    <img src="${friend.userImagePath}" class="rounded-circle me-3" alt="${friend.userName}'s avatar" width="50" height="50" />
                                </a>
                                <div class="flex-grow-1">
                                    <a class="text-decoration-none text-dark" href="/Profile?id=${friend.userTag}">
                                        <h5 class="mb-0">${friend.userName}</h5>
                                    </a>
                                    <small class="text-muted">Друг</small>
                                </div>
                                ${currentUserTag === userTag ? `
                                        <button class="btn btn-danger btn-sm" onclick="removeFriend('${friend.userTag}', '${userTag}')">Удалить</button>`
                            : ``
                        }
                            </div >
                        </div >
                    `);
                });
                $('#friends-count').text(data.friends.$values.length); // Обновить количество друзей
            } else {
                $('.friends-list').append('<p class="text-center">Нет друзей.</p>');
                $('#friends-count').text(0); // Обновить количество друзей
            }
        },
        error: function (xhr, status, error) {
            console.error('Error loading friends:', error);
        },
        complete: function () {
            isLoadingUserFriends = false;
        }
    });
}

function getUserSubscriptions(userTag) {
    if (isLoadingUserSubscriptions) return;
    isLoadingUserSubscriptions = true;
    $.ajax({
        url: `/Friend/GetSubscriptions?userTag=${userTag}`,
        type: 'GET',
        success: function (data) {
            if (data && data.subscriptions.$values.length > 0) {
                $('.subscriptions-list').empty(); // Очистить список подписок перед добавлением новых
                data.subscriptions.$values.forEach(subscription => {
                    $('.subscriptions-list').append(`
                        <div class="subscription-card card mb-3" id="subscription-card-${subscription.userTag}">
                            <div class="card-body d-flex align-items-center">
                                <a class="text-decoration-none text-dark" href="/Profile?id=${subscription.userTag}">
                                    <img src="${subscription.userImagePath}" class="rounded-circle me-3" alt="${subscription.userName}'s avatar" width="50" height="50" />
                                </a>
                                <div class="flex-grow-1">
                                    <a class="text-decoration-none text-dark" href="/Profile?id=${subscription.userTag}">
                                        <h5 class="mb-0">${subscription.userName}</h5>
                                    </a>
                                    <small class="text-muted">Подписка</small>
                                </div>
                            </div>
                        </div>
                    `);
                });
                $('#subscriptions-count').text(data.subscriptions.$values.length); // Обновить количество подписок
            } else {
                $('.subscriptions-list').append('<p class="text-center">Нет подписок.</p>');
                $('#subscriptions-count').text(0); // Обновить количество подписок
            }
        },
        error: function (xhr, status, error) {
            console.error('Error loading subscriptions:', error);
        },
        complete: function () {
            isLoadingUserSubscriptions = false;
        }
    });
}
