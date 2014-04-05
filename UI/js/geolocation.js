var map;
var mapOptions = {
    zoom: 4
    //,
    //mapTypeId: google.maps.MapTypeId.HYBRID
};
var useragent = navigator.userAgent;
var mapdiv = document.getElementById("map-canvas");
var markers = [];

function detectBrowser() {
  //if (useragent.indexOf('iPhone') != -1 || useragent.indexOf('Android') != -1 ) {
  //  mapdiv.style.width = '100%';
  //  mapdiv.style.height = '100%';
  //} else {
  //  mapdiv.style.width = '600px';
  //  mapdiv.style.height = '800px';
  //}
}

function initialize() {
    mapdiv.style.width = '600px';
    mapdiv.style.height = '600px';

    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

    
    // Try HTML5 geolocation
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position) {
            var pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            //map.setCenter(pos);

            //var marker = new google.maps.Marker({
              //  map: map,
               // title: "You are here",
               // position: pos
        //    });
          // marker.setMap(map);
            var allowedBounds = new google.maps.LatLngBounds(
                new google.maps.LatLng(85, -180),           // top left corner of map
                new google.maps.LatLng(-85, 180)            // bottom right corner
            );


            //var k = 5.0;
            //var n = allowedBounds.getNorthEast().lat() - k;
            //var e = allowedBounds.getNorthEast().lng() - k;
            //var s = allowedBounds.getSouthWest().lat() + k;
            //var w = allowedBounds.getSouthWest().lng() + k;
            //var neNew = new google.maps.LatLng(n, e);
            //var swNew = new google.maps.LatLng(s, w);
            //boundsNew = new google.maps.LatLngBounds(swNew, neNew);
            map.fitBounds(allowedBounds);
            getTweets(pos);
        }, function() {
            handleNoGeolocation(true);
        });
    } else {
        // Browser doesn't support Geolocation
        handleNoGeolocation(false);
    }
}
google.maps.event.addDomListener(window, 'load', initialize);