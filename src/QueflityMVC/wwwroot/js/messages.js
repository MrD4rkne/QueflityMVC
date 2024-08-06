function createMessageDiv(message, userId) {
    
    const messageContainer = $('<div class="message"></div>');
    const messageDiv = $('<div class="message-content"></div>').text(message.content);
    const dateDiv = $('<div class="date"></div>').text(formatDateTime(message.sentAt));

    if (message.userId === userId) {
        messageDiv.addClass('message-right');
        dateDiv.addClass('date-right');
    } else {
        messageDiv.addClass('message-left');
        dateDiv.addClass('date-left');
    }

    messageContainer.append(messageDiv).append(dateDiv);
    return messageContainer;
}

// Function to format date and time
function formatDateTime(dateTime) {
    const now = new Date();
    const date = new Date(dateTime);

    const time = date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

    if (date.toDateString() === now.toDateString()) {
        return time;
    }

    const oneDay = 24 * 60 * 60 * 1000;
    const diffDays = Math.round((now - date) / oneDay);

    let day;
    if (diffDays === 1) {
        day = "Yesterday";
    } else if (diffDays < 7) {
        day = date.toLocaleDateString([], { weekday: 'long' });
    } else {
        day = date.getFullYear() === now.getFullYear()
            ? date.toLocaleDateString([], { day: '2-digit', month: '2-digit' })
            : date.toLocaleDateString([], { day: '2-digit', month: '2-digit', year: 'numeric' });
    }

    return `${time} | ${day}`;
}