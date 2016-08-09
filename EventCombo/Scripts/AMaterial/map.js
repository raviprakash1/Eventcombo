var props = [
	{
	  dealid: 1,
	  dealtitle: 'An authentic Italian meal for two',
	  dealimg: 'images/deal1.jpg',
	  dealoff: '51% OFF',
	  dealdetails: '$15 for $30 Worth of Southern Itialian Dinner for Two or More at Cucina Calandra',
	  dealdesbold: 'Cucina Calandra',
	  dealdessmall: 'Fairfield (16.9 miles)',
	  dealthumbsup: '94% of 273 customers recommend0',
	  position: {
	    lat: 40.696047,
	    lng: -73.997159
	  },
	  markerIcon: "marker-green.png",
	  markerWidth: 23,
	  markerHeight: 27
	},
	{
	  dealid: 2,
	  dealtitle: 'An authentic Italian meal for two',
	  dealimg: 'images/deal2.jpg',
	  dealoff: '52% OFF',
	  dealdetails: '$15 for $30 Worth of Southern Itialian Dinner for Two or More at Cucina Calandra',
	  dealdesbold: 'Cucina Calandra',
	  dealdessmall: 'Fairfield (16.9 miles)',
	  dealthumbsup: '94% of 273 customers recommend0',
	  position: {
	    lat: 40.688042,
	    lng: -73.996472
	  },
	  markerIcon: "marker-pink.png",
	  markerWidth: 28,
	  markerHeight: 40
	},
	{
	  dealid: 3,
	  dealtitle: 'An authentic Italian meal for two',
	  dealimg: 'images/deal1.jpg',
	  dealoff: '53% OFF',
	  dealdetails: '$15 for $30 Worth of Southern Itialian Dinner for Two or More at Cucina Calandra',
	  dealdesbold: 'Cucina Calandra',
	  dealdessmall: 'Fairfield (16.9 miles)',
	  dealthumbsup: '94% of 273 customers recommend0',
	  position: {
	    lat: 40.702620,
	    lng: -73.989682
	  },
	  markerIcon: "marker-green.png",
	  markerWidth: 23,
	  markerHeight: 27
	},
	{
	  dealid: 4,
	  dealtitle: 'An authentic Italian meal for two',
	  dealimg: 'images/deal2.jpg',
	  dealoff: '54% OFF',
	  dealdetails: '$15 for $30 Worth of Southern Itialian Dinner for Two or More at Cucina Calandra',
	  dealdesbold: 'Cucina Calandra',
	  dealdessmall: 'Fairfield (16.9 miles)',
	  dealthumbsup: '94% of 273 customers recommend0',
	  position: {
	    lat: 40.694355,
	    lng: -73.985229
	  },
	  markerIcon: "marker-pink.png",
	  markerWidth: 28,
	  markerHeight: 40
	},
	{
	  dealid: 5,
	  dealtitle: 'An authentic Italian meal for two',
	  dealimg: 'images/deal1.jpg',
	  dealoff: '55% OFF',
	  dealdetails: '$15 for $30 Worth of Southern Itialian Dinner for Two or More at Cucina Calandra',
	  dealdesbold: 'Cucina Calandra',
	  dealdessmall: 'Fairfield (16.9 miles)',
	  dealthumbsup: '94% of 273 customers recommend0',
	  position: {
	    lat: 40.686838,
	    lng: -73.990078
	  },
	  markerIcon: "marker-green.png",
	  markerWidth: 23,
	  markerHeight: 27
	},
	{
	  dealid: 6,
	  dealtitle: 'An authentic Italian meal for two',
	  dealimg: 'images/deal2.jpg',
	  dealoff: '56% OFF',
	  dealdetails: '$15 for $30 Worth of Southern Itialian Dinner for Two or More at Cucina Calandra',
	  dealdesbold: 'Cucina Calandra',
	  dealdessmall: 'Fairfield (16.9 miles)',
	  dealthumbsup: '94% of 273 customers recommend0',
	  position: {
	    lat: 40.703686,
	    lng: -73.982910
	  },
	  markerIcon: "marker-pink.png",
	  markerWidth: 28,
	  markerHeight: 40
	},
	{
	  dealid: 7,
	  dealtitle: 'An authentic Italian meal for two',
	  dealimg: 'images/deal1.jpg',
	  dealoff: '57% OFF',
	  dealdetails: '$15 for $30 Worth of Southern Itialian Dinner for Two or More at Cucina Calandra',
	  dealdesbold: 'Cucina Calandra',
	  dealdessmall: 'Fairfield (16.9 miles)',
	  dealthumbsup: '94% of 273 customers recommend0',
	  position: {
	    lat: 40.702189,
	    lng: -73.995098
	  },
	  markerIcon: "marker-green.png",
	  markerWidth: 23,
	  markerHeight: 27
	},
	{
	  dealid: 8,
	  dealtitle: 'An authentic Italian meal for two',
	  dealimg: 'images/deal2.jpg',
	  dealoff: '58% OFF',
	  dealdetails: '$15 for $30 Worth of Southern Itialian Dinner for Two or More at Cucina Calandra',
	  dealdesbold: 'Cucina Calandra',
	  dealdessmall: 'Fairfield (16.9 miles)',
	  dealthumbsup: '94% of 273 customers recommend0',
	  position: {
	    lat: 40.694120,
	    lng: -73.974413
	  },
	  markerIcon: "marker-green.png",
	  markerWidth: 23,
	  markerHeight: 27
	},
	{
	  dealid: 9,
	  dealtitle: 'An authentic Italian meal for two',
	  dealimg: 'images/deal1.jpg',
	  dealoff: '59% OFF',
	  dealdetails: '$15 for $30 Worth of Southern Itialian Dinner for Two or More at Cucina Calandra',
	  dealdesbold: 'Cucina Calandra',
	  dealdessmall: 'Fairfield (16.9 miles)',
	  dealthumbsup: '94% of 273 customers recommend0',
	  position: {
	    lat: 40.682665,
	    lng: -73.000934
	  },
	  markerIcon: "marker-pink.png",
	  markerWidth: 28,
	  markerHeight: 40
	}];

var markers = [];

// custom infowindow object
var infobox = new InfoBox({
  disableAutoPan: false,
  maxWidth: 600,
  pixelOffset: new google.maps.Size(-101, -285),
  zIndex: null,
  boxStyle: {
    background: "url('/Images/AMaterial/infobox-bg.png') no-repeat",
    opacity: 1,
    width: "280px",
    height: "220px"
  },
  closeBoxMargin: "28px 26px 0px 0px",
  closeBoxURL: "",
  infoBoxClearance: new google.maps.Size(1, 1),
  pane: "floatPane",
  enableEventPropagation: false
});

function createMap(lat, lng) {
  "use strict";

  // Custom options for map
  var options = {
    zoom: 14,
    scrollwheel: false,
    mapTypeId: 'Styled',
    disableDefaultUI: false,
    mapTypeControlOptions: {
      mapTypeIds: ['Styled']
    }
  };

  var styles = [{ "featureType": "administrative", "elementType": "labels.text.fill", "stylers": [{ "color": "#444444" }] }, { "featureType": "landscape", "elementType": "all", "stylers": [{ "color": "#f2f2f2" }] }, { "featureType": "poi", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "all", "stylers": [{ "saturation": -100 }, { "lightness": 45 }] }, { "featureType": "road.highway", "elementType": "all", "stylers": [{ "visibility": "simplified" }] }, { "featureType": "road.arterial", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "transit", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "water", "elementType": "all", "stylers": [{ "color": "#46bcec" }, { "visibility": "on" }] }];

  var newMarker = null;

  // json for properties markers on map

  var map = new google.maps.Map(document.getElementById('mapView'), options);
  var styledMapType = new google.maps.StyledMapType(styles, {
    name: 'Styled'
  });

  map.mapTypes.set('Styled', styledMapType);
  map.setCenter(new google.maps.LatLng(lat, lng));
  map.setZoom(14);
  return map;
}

// function that adds the markers on map
function addDealMarkers(props, map) {
  "use strict";
  $.each(props, function (i, prop) {
    var latlng = new google.maps.LatLng(prop.position.lat, prop.position.lng);
    var marker = new google.maps.Marker({
      position: latlng,
      map: map,
      icon: new google.maps.MarkerImage(
          '/Images/AMaterial/' + prop.markerIcon,
          null,
          null,
          null,
          new google.maps.Size(prop.markerWidth, prop.markerHeight)
      ),
      draggable: false,
      animation: google.maps.Animation.DROP,
    });
    var infoboxContent = '<div class="infoW">' +
          '<h3 class="infoHead">' + prop.dealtitle + '</h3>' +
          '<div class="infoContent">' +
                                '<img class="propImg" src="' + prop.dealimg + '">' +
                                '<div class="propTitle">' + prop.dealoff + '</div>' +
                                '<div class="propAddress">' + prop.dealdetails + '</div>' +
                            '</div>' +
                            '<div class="clearfix"></div>' +
                            '<div class="infoButtons">' +
                                '<a onclick="addToCartMap(this, ' + prop.dealid + ')" class="addInfo">Add to Cart</a>' +
                                '<a href="" class="viewInfo">Details</a>' +
                            '</div>' +
                         '</div>';
    google.maps.event.addListener(marker, 'click', (function (marker, i) {
      return function () {
        infobox.setContent(infoboxContent);
        infobox.open(map, marker);
      }
    })(marker, i));

    $(document).on('click', '.addInfo', function () {
      infobox.open(null, null);
    });

    markers.push(marker);
  });
}

// Sets the map on all markers in the array.
function setMapOnAll(map) {
  for (var i = 0; i < markers.length; i++) {
    markers[i].setMap(map);
  }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
  setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers(map) {
  setMapOnAll(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
  clearMarkers();
  markers = [];
}

// function that adds the markers on map
function addEventMarker(eventInfo, map) {
  "use strict";
  var latlng = new google.maps.LatLng(eventInfo.Latitude, eventInfo.Longitude);
  var marker = new google.maps.Marker({
    position: latlng,
    map: map,
    icon: new google.maps.MarkerImage(
        '/Images/AMaterial/marker-pink.png',
        null,
        null,
        null,
        new google.maps.Size(28, 40)
    ),
    draggable: false,
    animation: google.maps.Animation.DROP,
  });
  var infoboxContent = '<div class="infoW">' +
        '<h3 class="infoHead">' + eventInfo.EventTitle + '</h3>' +
        '<div class="infoContent">' +
                              '<img class="propImg" src="' + eventInfo.ImageUrl + '">' +
                              '<div class="propTitle">' + eventInfo.Address + '</div>' +
                              '<div class="propAddress">' + eventInfo.EventDescription + '</div>' +
                          '</div>' +
                          '<div class="clearfix"></div>' +
                          '<div class="infoButtons">' +
                              '<a href="/EventManagement/ViewEvent?EventId=' + eventInfo.EventId + '" class="viewInfo">Details</a>' +
                          '</div>' +
                       '</div>';
  google.maps.event.addListener(marker, 'click', (function (marker) {
    return function () {
      infobox.setContent(infoboxContent);
      infobox.open(map, marker);
    }
  })(marker));

  markers.push(marker);
}
