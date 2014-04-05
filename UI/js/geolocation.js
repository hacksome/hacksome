var useragent = navigator.userAgent;
var markers = [];

function detectBrowser() {
  }

function initialize(div, data, mapOptions) {
    var mapdiv = document.getElementById(div);
   
    var map = new google.maps.Map(mapdiv, mapOptions);

    // Try HTML5 geolocation
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position) {
            var pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            var allowedBounds = new google.maps.LatLngBounds(
                new google.maps.LatLng(85, -180),           // top left corner of map
                new google.maps.LatLng(-85, 180)            // bottom right corner
            );

            map.fitBounds(allowedBounds);
            setMarker(data, map);
        }, function() {
            handleNoGeolocation(true);
        });
    } else {
        // Browser doesn't support Geolocation
        handleNoGeolocation(false);
    }
}
