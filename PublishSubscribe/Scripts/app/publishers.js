//TODO:all this should be replaced to be angular component
$().ready(function () {

    //TODO: your are hard coding the URLs you need to handle this by angular service
    $.getJSON("http://localhost:34637/api/PubSub/GetPublishers", function (data) {
        
        var select = $("#publishers");
        select.empty();

        $.each(data, function (key, val) {
            //TODO: the next snipped should be replaced with angular template
            select.append("<option value='" + val.Key + "'>" + val.Value + "</option>");
        });

    });

    $("#distribute").click(function () {

        var publisherId = $("#publishers option:selected").attr("value");
        var title = $("#title").val();
        var message = $("#message").val();
        var tags = $("#tags").val();

        $.post("http://localhost:34637/api/PubSub/DistributeMessage?publisherId=" + publisherId + "&title=" + title + "&message=" + message + "&tags=" + tags, function () { });
    });
});