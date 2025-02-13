// Метод для удаления друга из списка друзей по ссылке /Profile
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

function removeFriend(friendTag, userTag) {
    $.ajax({
        url: '/friend/removefriend',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ userTag: friendTag }),
        success: function (data) {
            if (data === 0) {
                const friendCard = $(`#friend-card-${friendTag}`);
                friendCard.find('.btn-danger').removeClass('btn-danger').addClass('btn-success').text('Добавить').attr('onclick', `addFriend('${friendTag}', '${userTag}')`);
                friendCard.find('.btn-warning').removeClass('btn-warning').addClass('btn-success').text('Добавить').attr('onclick', `addFriend('${friendTag}', '${userTag}')`);
                friendCard.find('small.text-muted').text('Бывший друг');
            }
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}

