var placeSearch, autocomplete, autocomplete2, map, directionsService, directionsDisplay;
var markers = [];
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
    initAutocomplete2();
}
function initMap() {
    // The location of Uluru
    directionsService = new google.maps.DirectionsService;
    directionsDisplay = new google.maps.DirectionsRenderer;
    var aalborg = { lat: 57.0488, lng: 9.9217 };
    // The map, centered at Uluru
    map = new google.maps.Map(
        document.getElementById('map'), { zoom: 15, center: aalborg });
    directionsDisplay.setMap(map);
    // The marker, positioned at Uluru
}

function initAutocomplete() {
    // Create the autocomplete object, restricting the search to geographical
    // location types.
    var auto = 'autocomplete';
    autocomplete = new google.maps.places.Autocomplete(
            /** @type {!HTMLInputElement} */(document.getElementById('autocomplete')),
        { types: ['geocode'] });

    // When the user selects an address from the dropdown, populate the address
    // fields in the form.
    autocomplete.addListener('place_changed', function () { fillInAddress(auto) });
}
function initAutocomplete2() {
    // Create the autocomplete object, restricting the search to geographical
    // location types.
    var auto = 'autocomplete2';
    autocomplete2 = new google.maps.places.Autocomplete(
            /** @type {!HTMLInputElement} */(document.getElementById('autocomplete2')),
        { types: ['geocode'] });

    // When the user selects an address from the dropdown, populate the address
    // fields in the form.
    autocomplete2.addListener('place_changed', function () { fillInAddress(auto) });
}
function fillInAddress(auto) {
    // Get the place details from the autocomplete object.
    var mapLabel;
    var autoToUse;
    if (auto === 'autocomplete') {
        autoToUse = autocomplete;
        mapLabel = 'A';
    }
    else {
        autoToUse = autocomplete2;
        mapLabel = 'B';
    } 
    var place = autoToUse.getPlace();
    for (var component in componentForm) {
        document.getElementById(component).value = '';
        document.getElementById(component).disabled = false;
    }
    // Get each component of the address from the place details
    // and fill the corresponding field on the form.
    for (var i = 0; i < place.address_components.length; i++) {
        var addressType = place.address_components[i].types[0];
        if (componentForm[addressType]) {
            var val = place.address_components[i][componentForm[addressType]];
            document.getElementById(addressType).value = val;
        }
    }
    console.log(auto);
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
    for (var i = 0; i < markers.length; i++)
    {
        if (markers[i].label === mapLabel)
        {
            markers[i].setMap(null);
            markers[i].setLabel(null);
        }
    }
    var geocoder = new google.maps.Geocoder();
    var address = document.getElementById("route").value + " " + document.getElementById("street_number").value + " " + document.getElementById("locality").value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === 'OK') {
            map.setCenter(results[0].geometry.location);
            markers.push(new google.maps.Marker({
                map: map,
                position: results[0].geometry.location,
                label:mapLabel
            }));
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
    checkMarkers();
    //var geo = obj.responseJSON.results[0].geometry.bounds.northeast;
    //console.log(geo);
    //var currentCenter = { lat: geo.lat, lng: geo.lng };
    //map.setCenter(currentCenter);
    //var marker = new google.maps.Marker({ position: currentCenter, map: map });
}
function checkMarkers(){
    setTimeout(function () {
        var DestIsThere;
        var OriginIsThere;
        for (var i = 0; i < markers.length; i++) {
            if (markers[i].label === 'A') OriginIsThere = true;
            else if (markers[i].label === 'B') DestIsThere = true;
        }
        if (DestIsThere && OriginIsThere) {
            calculateAndDisplayRoute(directionsService, directionsDisplay);
        }
    }, 1000);  
}
function calculateAndDisplayRoute(directionsService, directionsDisplay) {
    var origin;
    var destination;
    for (var i = 0; i < markers.length; i++) {
        if (markers[i].label === 'A') {
            origin = "" + markers[i].position.lat() + "," + markers[i].position.lng();
        }
        else if (markers[i].label === 'B') {
            destination = "" + markers[i].position.lat() + "," + markers[i].position.lng();
        }
    }
    console.log(origin);
    console.log(destination);
    directionsService.route({
        origin: origin,
        destination: destination,
        travelMode: 'BICYCLING'
    }, function (response, status) {
        if (status === 'OK') {
            directionsDisplay.setDirections(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });
    var service = new google.maps.DistanceMatrixService;
    service.getDistanceMatrix({
        origins: [origin],
        destinations: [destination],
        travelMode: 'BICYCLING',
        unitSystem: google.maps.UnitSystem.METRIC
    }, function (response, status) {
        if (status === 'OK') {
            console.log(response);
            var results = response.rows[0].elements;
            document.getElementById("distance").value = results[0].distance.text;
            var a = results[0].distance.value;
            var b = a/1000;
            var c = Math.ceil(b / 1) * 1; 
            document.getElementById("price").value = c*10+10;
        }
        })
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