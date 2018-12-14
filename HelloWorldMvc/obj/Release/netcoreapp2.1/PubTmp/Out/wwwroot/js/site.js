var stat = $("#status");
var statText = $("#status span");
var messageId = $("#msgId");
var messageCount = $(".message-count span");
$(document).ready(function () {
    //on change listener for message id
    $("#msgId").change(function () {
        getMessage();
    });

    //submit button
    $("#submit").on("click", function () {
        addMessage();
    });

    //purge button
    $("#purge").on("click", function () {
        purgeMessages();
    });
});

function purgeMessages() {
    var purgeMessage = {
        GreetingMessageId: "-1",
        GreetingMessage: "PURGE"
    };
    $.ajax({
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        type: "POST",
        url: "/Home/ModifyMessages",
        data: JSON.stringify(purgeMessage),
        success: function (response) {
            //var obj = JSON.parse(response);
            console.log("Purge Complete");
            statText.html(response.status);
            statuschange();
            //display the remaining message
            messageId.val(1);
            getMessage();
        },
        error: function (xhr, error) {
            console.log("Something went wrong! " + xhr + ", " + error);
        },
        complete: function (xhr, status) {
        }

    });
}

function getMessage() {
    var displayedMessage = $("#cm input");
    var messageCount = $(".message-count span");
    var messageId = $("#msgId").val();
    $.ajax({
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        type: "POST",
        url: "/Home/GetMessage",
        data: JSON.stringify(messageId),
        success: function (response) {
            console.log("Message retrieved");
            
            //var obj = JSON.parse(response);
            //console.log("GreetingMessageId: " + obj.GreetingMessageId + ", " + "GreetingMessage: " + obj.GreetingMessage);

            var mid = response.message.greetingMessageId;
            var mtext = response.message.greetingMessage;
            var mcount = response.info.count;
            var mstatus = response.info.status;
            console.log("Message Info: count=" + mcount + ", status=" + mstatus + ". Message: GreetingMessageId=" + mid + "GreetingMessage=" + mtext);
            //var mid = response.greetingMessageId;
            //var mtext = response.greetingMessage;

            //messageCount.html("You are viewing message " + " of " + response.count);

            messageCount.html(mstatus);

            if (mtext !== null) {
                $("#msgId").val(mid);
                displayedMessage.val(mtext);
                //console.log("GreetingMessageId: " + response.greetingMessageId + ", GreetingMessage: " + response.greetingMessage);
            }
            else {
                $("#msgId").val(1);
                $("#cm span").html("Hello World!");
                //console.log("Id is out of range");
            }
        },
        complete: function () {
            console.log("getMessages ended");
        }
    });



}

function addMessage() {
    var addedMessage = $("#addedMessage").val();
    var themessage = {
        GreetingMessageId: "0",
        GreetingMessage: addedMessage
    };

    $.ajax({
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        type: "POST",
        url: "/Home/ModifyMessages",
        data: JSON.stringify(themessage),
        success: function (response) {
            console.log("Message successfully recorded");
            console.log(response);
            $("#addedMessage").val("");
            statText.html(response.status);
            statuschange();
        },
        error: function (xhr, error) {
            console.log("Something went wrong! " + xhr + ", " + error);
        },
        complete: function (xhr, status) {
            console.log("Stringified data: " + JSON.stringify(themessage));
            console.log("Regular data: " + themessage);
        }
    });

}

function statuschange() {
    //stat.addClass("visible");
    stat.css("opacity", "1");
    setTimeout(function () {
        stat.animate({
            opacity: 0
        }, 3000, function () {
            //stat.removeClass("visible");
            stat.removeAttr("style");
        });
    },2000);
}