//TODO:all this should be replaced to be angular component
$().ready(function () {

    //TODO: your are hard coding the URLs you need to handle this by angular service
    $.getJSON("http://localhost:34637/api/PubSub/GetSubscribers", function (data) {

        var list = $("#subscribers");
        list.empty();

        $.each(data, function (key, val) {
            //TODO: this should be replaced with angular template
            //select.append("<option value='" + val.Key + "'>" + val.Value + "</option>");
            list.append("<a href='#' class='list-group-item list-group-item-action subscriber' Value='" +
                val.Key
                +"'>"+ val.Value +"</a>");
        });

    });


    $("body").on("click", "a.subscriber", function () {
       
        //remove active from the others and activate current 
        $("#subscribers a.subscriber").each(function(index, element) {
            $( element ).removeClass("active");
        });
        $(this).addClass("active");

        //Get Messages and pull criteria for the selected Subscriber
        getMessagesFor($(this).attr("Value"));
        getPullCriteriaFor($(this).attr("Value"));
    });

    function getMessagesFor(subscriber) {
        $.getJSON("http://localhost:34637/api/PubSub/GetAllMessages?subscriberId=" + subscriber, function (data) {
            var list = $("#subscribersNewMessages");
            list.empty();

            $.each(data, function (key, val) {
                //TODO: this should be replaced with angular template
                list.append("<a href='#' class='list-group-item list-group-item-action active'>" +
                    "<h5 class='list-group-item-heading'>" + val.Title + "</h5>" +
                    "<p class='list-group-item-text'>" + val.Content + "</p>" +
                    "<span>" + val.Tags + "</span></a>");
            });
        });
    }

    function getPullCriteriaFor(subscriberId) {

        $("#tags").val("");
        $("#titleContains").val("");
        $("#contentContains").val("");

        $.getJSON("http://localhost:34637/api/PubSub/GetSubscriberCriteria?subscriberId=" + subscriberId, function (data) {

            //TODO: this should be replaced by normal angular binding
            $("#tags").val(data.Tags);
            $("#titleContains").val(data.TitleContains);
            $("#contentContains").val(data.ContentContains);
        });
    }

    $("#saveCriteria").click(function () {

        var subscriberId = $("#subscribers a.active").attr("value");
        var titleContains = $("#titleContains").val();
        var contentContains = $("#contentContains").val();
        var tags = $("#tags").val();

        $.post("http://localhost:34637/api/PubSub/UpdateSubscriberCriteria?subscriberId=" + subscriberId + "&titleContains=" + titleContains + "&contentContains=" + contentContains + "&tags=" + tags, function () { });
    });
});