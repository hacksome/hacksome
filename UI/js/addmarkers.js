var timeline = [];

function getTweets(pos) {
    $.getJSON('twittertweets.ashx?map=true').done(function (data) {
        timeline.push(data.statuses);
        setMarker(data);
    });
}

// used when clicking on marker
function bindInfoW(marker, contentString, infowindow) {
    google.maps.event.addListener(marker, 'click', function () {
        var timeline = [];
        infowindow.setContent(contentString);
        infowindow.open(map, marker);
    });
}

function setMarker(data) {
    for (var i = 0; i < data.length; i++) {
        if (data[i].Long != 0.0 ||data[i].Lat != 0.0) {
            var dat = data[i];
            var sn = dat.User.ScreenName;
            var msg = dat.Msg;
            var avi = dat.User.ProfileImgUrl;
            var handle = "http://www.twitter.com/" + sn;
            var dt = "<br><br><right><small>" + dat.Date + "</small></right>";

            var aviLink = "<a href='" + handle + "' target='_blank'><img class='avi' src=" + avi + "></img></a>";
            var wLogo = aviLink + "<a href='" + handle + "' target='_blank'>@" + sn + "</a>: " + msg + dt;
            var tweet = "<a href='" + handle + "' target='_blank'>@" + sn + "</a>:" + msg;
            var bq = "<blockquote class=\"twitter-tweet\"><a href=\"https://twitter.com/" + sn + "/status/" + dat.MsgId + "\"></a></blockquote>";

            var pinIcon = new google.maps.MarkerImage(
			    avi,
			    null, /* size is determined at runtime */
			    null, /* origin is 0,0 */
			    null, /* anchor is bottom center of the scaled image */
			    new google.maps.Size(52, 52)
			);

            var marker = new google.maps.Marker({
                map: map,
                icon: pinIcon,
                title: msg,
                position: new google.maps.LatLng(data[i].Lat, data[i].Long)
            });

            marker.setAnimation(google.maps.Animation.DROP);
            var infowindow = new google.maps.InfoWindow;
            bindInfoW(marker, wLogo, infowindow);

            //var template = "<tr class='tweet' data-id='" + data.statuses[i].id + "' ><td>" + aviLink + "</td><td>" + tweet + "</td></tr>";

         //   $("tbody#jstweets").prepend(template);
        }
    }
}