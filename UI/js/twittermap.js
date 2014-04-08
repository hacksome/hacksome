$(document).ready(function(){
    var mapOptions = {
        zoom: 4
    };

    var mapData;
    $.ajax({
        url: 'twittertweets.ashx?map=1',
        type: 'GET',
        dataType: 'json',
        success: function(data) {
            try {
                mapData = data.length == 0 ? dummyMapData : data;
            } catch (e) {
                mapData = dummyMapData;
            }
            initialize('map-canvas', mapData, mapOptions);
        },
        error: function () {
            initialize('map-canvas', dummyMapData, mapOptions);
        }
    });

});