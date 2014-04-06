jQuery(function ($) {
    var search1 = $('#search-input1');
    var search2 = $('#search-input2');
    var search1Data;
    var search2Data;
    
    var mapOptions = {
        zoom: 2
    };
    search1.keypress(function (e) {
        if (search1.val().length>0 &&  e.which == 13) {
            getSearchResult(search1.val(),  'searcha');
            e.preventDefault();
            return false;
        }
    });

    search2.keypress(function (e) {
        if (search2.val().length > 0 && e.which == 13) {

            getSearchResult(search2.val(), 'searchb');

            e.preventDefault();
            return false;
        }
    });
    function getSearchResult(q, id) {

        $.ajax({
            url: 'search.ashx?q=' + q,
            success: function (data) {
                try {
                    var parseData = JSON.parse(data);
                    if (id == "searcha") {
                        search1Data = parseData.data.length == 0 ? dummySearchAData : parseData;
                    } else {
                        search2Data = parseData.data.length == 0 ? dummySearchBData : parseData;
                    }
                } catch (e) {
                    if (id == "searcha") {
                        search1Data = dummySearchAData;
                    } else {
                        search2Data = dummySearchBData;
                    }
                }

                setData(id=="searcha"?search1Data:search2Data, id);
            },
            error: function () {
                setData(id == "searcha" ? dummySearchAData : dummySearchBData, id);
            }
        });
    }
    
    function setData(data,id) {
        var tweets = data.TweetInfos;
        var ranking = data.Ranking;
        $('#' + id + ' .title').html(ranking.Name);

        var template =  '<li>' +
                           '<i class="icon-caret-right blue"></i>' +
                            'Total Tweets: ' + ranking.Total + 
                        '</li>' +
                        '<li>' +
                           '<i class="icon-caret-right blue"></i>' +
                            'Average Sentimal Score: ' + ranking.Score + 
                        '</li>' +
                        '<li>' +
                           '<i class="icon-caret-right blue"></i>' +
                            'Index Value: ' + ranking.Index +
                        '</li>';

        $('#' + id + '_rankinginfo').html(template);
        setTweetData(tweets, id + '_tweets');

        initialize(id + '_map', tweets, mapOptions);

    }

    function setTweetData(data, id) {
        $("#" + id).html();
        for (var i = 0; i < data.length; i++) {
            var tweet = data[i];

            var template = '<div class="itemdiv dialogdiv">' +
                '<div class="user">' +
                '<img src="' + tweet.User.ProfileImgUrl + '" />' +
                '</div>' +
                '<div class="body">' +
                '<div class="time">' +
                '<i class="icon-time"></i>' +
                '<span class="green">' + tweet.Date + '</span>' +
                '</div>' +
                '<div class="name">' +
                '<a href="http://www.twitter.com/' + tweet.User.ScreenName + '">' + tweet.User.ScreenName + '</a>' +
                '</div>' +
                '<div class="text">' + tweet.Msg + '</div>' +
                '<div class="tools">' +
                '<a href="#" class="btn btn-minier btn-info">' +
                '<i class="icon-only icon-share-alt"></i>' +
                '</a>' +
                '</div>' +
                '</div>' +
                '</div>' +
                '</div>';
            //"<tr class='tweet' data-id='" + data.statuses[i].id + "' ><td>" + aviLink + "</td><td>" + tweet + "</td></tr>";

            $("#" + id).prepend(template);
        }
    }

});