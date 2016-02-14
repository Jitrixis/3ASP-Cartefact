$("input[name='geolocate']").click(function () { geolocate(); });

function geolocate() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(putlocation);
    } else {
        $("#geolocate-error").text("Geolocation is not supported by this browser.");
    }
}

function putlocation(position) {
    var lat = position.coords.latitude;
    var lon = position.coords.longitude;
    $("input[name='Location.Latitude'").val(lat.toString().replace(/\./g, ','));
    $("input[name='Location.Longitude'").val(lon.toString().replace(/\./g, ','));
}