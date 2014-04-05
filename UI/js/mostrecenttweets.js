jQuery(function ($) {

    function getMostRecentTweets() {
        $.getJSON('twittertweets.ashx?limit=true').done(function (data) {
            setData(data);
        });
    }

    function setData(data) {
        for (var i = 0; i < data.data.length; i++) {
           // var tweets = data.[i];

        }
    }

    getMostRecentTweets();

});