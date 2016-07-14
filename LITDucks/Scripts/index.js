$(function() {
    var realDuckAudio = new Audio("/Content/Sounds/duck_quack.mp3");
    var rubberDuckAudio = new Audio("/Content/Sounds/rubber_duckie.mp3");

    var quackHub = $.connection.quackHub;
    $.connection.hub.start();

    realDuckAudio.onended = function() {
        $("#real-duck").css('cursor', 'pointer');
    }

    rubberDuckAudio.onended = function () {
        $("#rubber-duck").css('cursor', 'pointer');
    }

    $("#real-duck").on('click', function () {
        if ($(this).css('cursor') !== 'pointer') {
            return;
        }
        $(this).css('cursor', 'not-allowed');
        realDuckAudio.play();
        $.post("/home/AddRealDuckQuack");
    });

    $("#rubber-duck").on('click', function () {
        if ($(this).css('cursor') !== 'pointer') {
            return;
        }
        $(this).css('cursor', 'not-allowed');
        rubberDuckAudio.play();
        $.post("/home/AddRubberDuckQuack");
    });

    quackHub.client.quackCountsReceived = function (result) {
        $("#real-duck-count").text(result.RealDuckCount);
        $("#rubber-duck-count").text(result.RubberDuckCount);
    }
    var isRumbling = false;
    $('body').jrumble();
    $("#donate").on('click', function() {
        
        if (isRumbling) {
            $('body').trigger('stopRumble');
            isRumbling = false;
        } else {
            isRumbling = true;
            $('body').trigger('startRumble');
        }
        

    });
});