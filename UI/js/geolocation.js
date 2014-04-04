var map;
var mapOptions = {
    zoom: 14,
    mapTypeId: google.maps.MapTypeId.ROADMAP,
    minZoom: 10
};
var useragent = navigator.userAgent;
var mapdiv = document.getElementById("map-canvas");
var markers = [];

function detectBrowser() {
  if (useragent.indexOf('iPhone') != -1 || useragent.indexOf('Android') != -1 ) {
    mapdiv.style.width = '100%';
    mapdiv.style.height = '100%';
  } else {
    mapdiv.style.width = '600px';
    mapdiv.style.height = '800px';
  }
}

function initialize() {
  
    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

    
    // Try HTML5 geolocation
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position) {
            var pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
            map.setCenter(pos);

            var marker = new google.maps.Marker({
                map: map,
                title: "You are here",
                position: pos
            });
            marker.setMap(map);
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