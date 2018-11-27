var placeSearch, autocomplete, map, marker;
var componentForm = {
    street_number: 'short_name',
    route: 'long_name',
    locality: 'long_name',
    administrative_area_level_1: 'short_name',
    country: 'long_name',
    postal_code: 'short_name'
};
function initialize() {
    initMap();
    initAutocomplete();
}
function initMap() {
    // The location of Uluru
    var aalborg = { lat: 57.0488, lng: 9.9217 };
    // The map, centered at Uluru
    map = new google.maps.Map(
        document.getElementById('map'), { zoom: 15, center: aalborg });
    marker = new google.maps.Marker({ position: aalborg, map: map });
    // The marker, positioned at Uluru
}

function initAutocomplete() {
    // Create the autocomplete object, restricting the search to geographical
    // location types.
    autocomplete = new google.maps.places.Autocomplete(
            /** @type {!HTMLInputElement} */(document.getElementById('autocomplete')),
        { types: ['geocode'] });

    // When the user selects an address from the dropdown, populate the address
    // fields in the form.
    autocomplete.addListener('place_changed', fillInAddress);
}

function fillInAddress() {
    // Get the place details from the autocomplete object.
    var place = autocomplete.getPlace();
    console.log(place);
    for (var component in componentForm) {
        document.getElementById(component).value = '';
        document.getElementById(component).disabled = false;
    }
    document.getElementById("autocomplete").value = "";
    document.getElementById("autocomplete").placeholder = "";
    // Get each component of the address from the place details
    // and fill the corresponding field on the form.
    for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];
        if (componentForm[addressType]) {
            var val = place.address_components[i][componentForm[addressType]];
            document.getElementById(addressType).value = val;
        }
    }
    document.getElementById("locality").disabled = "true";
    document.getElementById("route").disabled = "true";
    document.getElementById("postal_code").disabled = "true";
    document.getElementById("street_number").disabled = "true";
    var currentCenter;
    //var jsonURL = "https://maps.googleapis.com/maps/api/geocode/json?address=" + document.getElementById("route").value + "%20" + document.getElementById("street_number").value + "%20" + document.getElementById("locality").value + "&key=AIzaSyD-i9eQttqqwEOLH8tqPgYocn3Cx-aAa7I";
    //console.log(jsonURL);
    //var obj = $.getJSON(jsonURL).done(function (data) {
    //    $.each(data.items, function (i, item) {
    //    });
    //});
    var geocoder = new google.maps.Geocoder();
    var address = document.getElementById("route").value + " " + document.getElementById("street_number").value + " " + document.getElementById("locality").value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === 'OK') {
            map.setCenter(results[0].geometry.location);
            marker.setMap(null);
            marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
    //var geo = obj.responseJSON.results[0].geometry.bounds.northeast;
    //console.log(geo);
    //var currentCenter = { lat: geo.lat, lng: geo.lng };
    //map.setCenter(currentCenter);
    //var marker = new google.maps.Marker({ position: currentCenter, map: map });
}

// Bias the autocomplete object to the user's geographical location,
// as supplied by the browser's 'navigator.geolocation' object.
function geolocate() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var geolocation = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            var circle = new google.maps.Circle({
                center: geolocation,
                radius: position.coords.accuracy
            });
            autocomplete.setBounds(circle.getBounds());
        });
    }
}