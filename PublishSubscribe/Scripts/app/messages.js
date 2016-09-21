//TODO:all this should be replaced to be angular component
$().ready(function () {

    //when loaded get the messages 
    getMessages();

    function getMessages() {
        $.getJSON("http://localhost:34637/api/PubSub/GetAllMessages", function (data) {
            var table = $("#table tbody");
            $("#table tbody > tr").remove();

            $.each(data, function (key, val) {
                //TODO: this should be replaced with angular template
                table.append("<tr><th>" + val.StringId + "</th><td>" + val.Title
                    + "</td><td>" + val.Content + "</td><td>" + val.Tags + "</td></tr>");
            });
        });
    }

    //when refresh pressed get the messages
    $("#refresh").click(function() {
        getMessages();
    });
});