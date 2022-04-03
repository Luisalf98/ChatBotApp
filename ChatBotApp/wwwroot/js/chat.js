"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/MessagingHub").build();
var username = $('#username').val();

$('.btn.sender').attr('disabled', true);

connection.start().then(function () {
  initializeChatRooms();
}).catch(function (err) {
  return console.error(err.toString());
});

function initializeChatRooms() {
  $(document).ready(function () {
    $('.chat').each(function (el) {
      let chatRoomId = $(this).data('chatroomid');
      connection.invoke("SubscribeToChatRoom", chatRoomId).catch(function (err) {
        return console.error(err.toString());
      });
    });
  });
}

connection.on("SubscribedSuccessfully", function (chatRoomId) {
  $(`.btn.sender[data-buttonchatroomid="${chatRoomId}"]`).attr('disabled', false);
});

connection.on("ReceiveMessage", function (chatRoomId, sender, message, createdAt) {
  let alignment = 'justify-content-start';
  let bgColor = 'bg-info';
  let senderHtml = `<b>${sender}: </b>`;
  if (sender === username) {
    alignment = 'justify-content-end';
    senderHtml = '';
    bgColor = 'bg-success';
  } 
  $(`.chat[data-chatroomid="${chatRoomId}"]`).append(
    `
      <div class="d-flex ${alignment}" >
        <p class="p-2 rounded ${bgColor} text-white text-wrap text-break" style="width: fit-content; max-width: 80%;">
          <b style="font-size: 11px;">[${createdAt}]</b> ${senderHtml}${message}
        </p>
      </div>
    `
  );
});

connection.on("HandleError", function (message) {
  try {
    if ($('#handle-error-toast').length === 0) {
      let toastHtml = `
        <div id="handle-error-toast" class="toast bg-danger text-white" style="position: absolute; top: 10px; right: 10px;">
          <div class="toast-body">
            ${message}
          </div>
        </div>
      `;
      $('body').append(toastHtml);
      $('#handle-error-toast').toast({ delay: 2000 });
    } else {
      $('#handle-error-toast toast-body').html(message);
    }
    $('#handle-error-toast').toast('show');
  } catch (ex) {
    console.error(ex);
    alert(message);
  }
});

$('.btn.sender').click(function (event) {
  let chatRoomId = $(this).data('buttonchatroomid');
  let messageInput = $(`textarea[data-messagechatroomid="${chatRoomId}"]`);
  let message = messageInput.val();
  if (!message || message.trim().length === 0) return;
  
  messageInput.val('');
  connection.invoke("SendMessage", chatRoomId, message).catch(function (err) {
    return console.error(err.toString());
  });
});