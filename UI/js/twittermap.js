$(document).ready(function(){
    var mapOptions = {
        zoom: 4
    };

    var mapData;
    $.ajax({
        url: 'twittertweets.ashx?map=1',
        success: function (data) {
            try {
                var parseData = JSON.parse(data);
                mapData = parseData.data.length == 0 ? dummyTwitterData : parseData;
            } catch (e) {
                mapData = dummyTwitterData;
            }

            initialize('map-canvas', mapData, mapOptions);
        },
        error: function () {
            initialize('map-canvas', dummyTwitterData, mapOptions);
        }
    });


});