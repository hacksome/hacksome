$(document).ready(function(){
    var mapOptions = {
        zoom: 4
    };
    $.getJSON('test/testdata/dummytwitter.txt').done(function (data) {

        initialize('map-canvas', data, mapOptions);
    });


});