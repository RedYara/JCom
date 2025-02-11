function addToFriendList(userTag) {
    $.ajax({
        url: '/friend/addtofriends',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ userTag: userTag }),
        success: function (data) {
            console.log(data);
            if (data === 1) {
                const friendsButton = document.getElementById('friends-button');
                friendsButton.innerHTML = '<button class="btn btn-warning" onclick="removeFromFriendList(\'' + userTag + '\')">Отписаться</button>';
            }
            if (data === 2) {
                const friendsButton = document.getElementById('friends-button');
                friendsButton.innerHTML = '<button class="btn btn-danger" onclick="removeFromFriendList(\'' + userTag + '\')">Удалить из друзей</button>';
            }
        },
        error: function (xhr, status, error) {
            console.error('Ошибка:', error);
        }
    });
}
