function removeFromFriendList(userTag) {
    $.ajax({
        url: '/friend/removefriend',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ userTag: userTag }),
        success: function (data) {
            console.log(data);
            if (data === 0) {
                const friendsButton = document.getElementById('friends-button');
                friendsButton.innerHTML = '<button class="btn btn-success" onclick="addToFriendList(\'' + userTag + '\')">Добавить в друзья</button>';
            }
        },
        error: function (xhr, status, error) {
            console.error('Ошибка:', error);
        }
    });
}
