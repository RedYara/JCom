function addToFriendList(friendUserTag, userTag) {
    if (friendUserTag === userTag) return;
    $.ajax({
        url: '/friend/addtofriends',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ userTag: friendUserTag }),
        success: function (data) {
            console.log(data);
            if (data === 1) {
                const friendsButton = document.getElementById('friends-button');
                friendsButton.innerHTML = '<button class="btn btn-warning" onclick="removeFromFriendList(\'' + friendUserTag + '\')">Отписаться</button>';
            }
            if (data === 2) {
                const friendsButton = document.getElementById('friends-button');
                friendsButton.innerHTML = '<button class="btn btn-danger" onclick="removeFromFriendList(\'' + friendUserTag + '\')">Удалить из друзей</button>';
            }
        },
        error: function (xhr, status, error) {
            console.error('Ошибка:', error);
        }
    });
}

function addFriend(friendTag, userTag) {
    $.ajax({
        url: '/friend/addtofriends',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ userTag: friendTag }),
        success: function (data) {
            console.log(data);
            if (data === 1) {
                const friendCard = $(`#friend-card-${friendTag}`);
                friendCard.find('.btn-success').removeClass('btn-success').addClass('btn-warning').text('Отписаться').attr('onclick', `removeFriend('${friendTag}', '${userTag}')`);
                friendCard.find('small.text-muted').text('Вы подписаны');
            }
            if (data === 2) {
                const friendCard = $(`#friend-card-${friendTag}`);
                friendCard.find('.btn-success').removeClass('btn-success').addClass('btn-danger').text('Удалить').attr('onclick', `removeFriend('${friendTag}', '${userTag}')`);
                friendCard.find('small.text-muted').text('Бывший друг');
            }
        },
        error: function (xhr, status, error) {
            console.error('Ошибка:', error);
        }
    });

}
