function initMap() {
    var latlng = new google.maps.LatLng(57.10711800000001, 12.252090700000053);
    var options = {
        zoom: 14, center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    $(document).ready(function () {
        var map = new google.maps.Map(document.getElementById("map"), options);  

        var marker = new google.maps.Marker({
            position: latlng,
            map: map,
            title: "Location"
        });

        marker.setMap(map);
    });
}

