jQuery(function ($) {

    function getMostRecentTweets() {
        $.ajax({
            dataType: "json",
            type: 'GET',
            url: 'twittertweets.ashx?limit=true',
            success: function (data) {
                try {
                    setData(data);
                } catch (e)  {
                    setData(dummyTwitterData);
                }
            },
            error: function() {
                setData(dummyTwitterData);
            }
        });
    }

    function setData(data) {
        for (var i = 0; i < data.length; i++) {
            var tweet = data[i];

            var template = '<div class="itemdiv dialogdiv">' +
                '<div class="user">' +
                '<img src="' +tweet.User.ProfileImgUrl+ '" />' +
                '</div>' +
                '<div class="body">' +
                '<div class="time">' +
                '<i class="icon-time"></i>' +
                '<span class="green">' +tweet.Date+'</span>' +
                '</div>' +
                '<div class="name">' +
                '<a href="http://www.twitter.com/' + tweet.User.ScreenName + '">' +  tweet.User.ScreenName + '</a>' +
                '</div>' +
                '<div class="text">' +tweet.Msg + '</div>' +
                '<div class="tools">' +
                '<a href="#" class="btn btn-minier btn-info">' +
                '<i class="icon-only icon-share-alt"></i>' +
                '</a>' +
                '</div>' +
                '</div>' +
                '</div>' +
                '</div>';
            //"<tr class='tweet' data-id='" + data.statuses[i].id + "' ><td>" + aviLink + "</td><td>" + tweet + "</td></tr>";

               $("#tweets").prepend(template);

        }
    }

    getMostRecentTweets();

});