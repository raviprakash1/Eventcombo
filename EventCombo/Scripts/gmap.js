

var geocoder;
var map;
var marker;

// initialise the google maps objects, and add listeners
function gmaps_init_MultiVenue() {

    // center of the universe
    var latlng = new google.maps.LatLng(51.751724, -1.255284);

    var options = {
        zoom: 2,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    // create our map object

    if (!map) {
        // create our map object
        map = new google.maps.Map(document.getElementById("map_canvas"), options);
    }
    else {
        map.setOptions(options);
    }
    // the geocoder object allows us to do latlng lookup based on address
    geocoder = new google.maps.Geocoder();

    // the marker shows us the position of the latest address
    marker = new google.maps.Marker({
        map: map,
        draggable: true
    });

    // event triggered when marker is dragged and dropped
    google.maps.event.addListener(marker, 'dragend', function () {
        geocode_lookup('latLng', marker.getPosition());
    });

    // event triggered when map is clicked
    google.maps.event.addListener(map, 'click', function (event) {
        marker.setPosition(event.latLng)
        geocode_lookup('latLng', event.latLng);
    });
    google.maps.event.trigger(map, "resize");
};
function gmaps_init_Venue() {
    //map1 = new google.maps.Map(document.getElementById("map_canvas"), options);
    // center of the universe
    var latlng = new google.maps.LatLng(51.751724, -1.255284);

    var options = {
        zoom: 2,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    if (!map) {
        // create our map object
        map = new google.maps.Map(document.getElementById("map_canvas"), options);
    }
    else {
        map.setOptions(options);
    }

   

    // the geocoder object allows us to do latlng lookup based on address
    geocoder = new google.maps.Geocoder();

    // the marker shows us the position of the latest address
    marker = new google.maps.Marker({
        map: map1,
        draggable: true
    });

    // event triggered when marker is dragged and dropped
    google.maps.event.addListener(marker, 'dragend', function () {
        geocode_lookup('latLng', marker.getPosition());
    });

    // event triggered when map is clicked
    google.maps.event.addListener(map1, 'click', function (event) {
        marker.setPosition(event.latLng)
        geocode_lookup('latLng', event.latLng);
    });

   // $('#gmaps-error').hide();
};

// move the marker to a new position, and center the map on it
function update_map(geometry) {
  gmaps_init_MultiVenue();
  map.fitBounds( geometry.viewport )
  marker.setPosition(geometry.location)
}

// fill in the UI elements with new position data
function update_ui( address, latLng ) {
    $("#txtEventLocation").autocomplete("close");
    $("#txtEventLocation").val(address);
  //$('#gmaps-output-latitude').html(latLng.lat());
  //$('#gmaps-output-longitude').html(latLng.lng());
}

function geocode_lookup( type, value, update ) {
  // default value: update = false
  update = typeof update !== 'undefined' ? update : false;

  request = {};
  request[type] = value;

  geocoder.geocode(request, function(results, status) {
    $('#gmaps-error').html('');
    $('#gmaps-error').hide();
    if (status == google.maps.GeocoderStatus.OK) {
      // Google geocoding has succeeded!
      if (results[0]) {
        // Always update the UI elements with new location data
        update_ui( results[0].formatted_address,
                   results[0].geometry.location )

        // Only update the map (position marker and center map) if requested
        if( update ) { update_map( results[0].geometry ) }
      } else {
        // Geocoder status ok but no results!?
        $('#gmaps-error').html("Sorry, something went wrong. Try again!");
        $('#gmaps-error').show();
      }
    } else {
      // Google Geocoding has failed. Two common reasons:
      //   * Address not recognised (e.g. search for 'zxxzcxczxcx')
      //   * Location doesn't map to address (e.g. click in middle of Atlantic)

      if( type == 'address' ) {
        // User has typed in an address which we can't geocode to a location
       // $('#gmaps-error').html("Sorry! We couldn't find " + value + ". Try a different search term, or click the map." );
    //$('#gmaps-error').show();
      } else {
        // User has clicked or dragged marker to somewhere that Google can't do a reverse lookup for
        // In this case we display a warning, clear the address box, but fill in LatLng
      //  $('#gmaps-error').html("Woah... that's pretty remote! You're going to have to manually enter a place name." );
       // $('#gmaps-error').show();
        update_ui('', value)
      }
    };
  });
};

// initialise the jqueryUI autocomplete element
function autocomplete_init_MultiVenue() {
  $("#VenueNamesss").autocomplete({

    // source is the list of input options shown in the autocomplete dropdown.
    // see documentation: http://jqueryui.com/demos/autocomplete/
    source: function(request,response) {

      // the geocode method takes an address or LatLng to search for
      // and a callback function which should process the results into
      // a format accepted by jqueryUI autocomplete
      geocoder.geocode( {'address': request.term }, function(results, status) {
        response($.map(results.slice(0,6), function(item) {
          return {
            label: item.formatted_address, // appears in dropdown box
            value: item.formatted_address, // inserted into input element when selected
            geocode: item                  // all geocode data: used in select callback event
          }
        }));
      })
    },

    // event triggered when drop-down option selected
    select: function (event, ui) {
        update_ui_MultiVenue(ui.item.value, ui.item.geocode.geometry.location)
      update_map(ui.item.geocode.geometry)
      
    }
  });
  

  // triggered when user presses a key in the address box
  $("#VenueName").bind('keydown', function(event) {
    if(event.keyCode == 13) {
      geocode_lookup( 'address', $("#VenueName").val(), true );

      // ensures dropdown disappears when enter is pressed
      $("#VenueName").autocomplete("disable")
    } else {
      // re-enable if previously disabled above
        $("#VenueName").autocomplete("enable")
        
    }
  });
}; // autocomplete_init
function autocomplete_init_Venue() {
    $("#txtEventLocation").autocomplete({
   
        // source is the list of input options shown in the autocomplete dropdown.
        // see documentation: http://jqueryui.com/demos/autocomplete/
        source: function (request, response) {

            // the geocode method takes an address or LatLng to search for
            // and a callback function which should process the results into
            // a format accepted by jqueryUI autocomplete
            geocoder.geocode({ 'address': request.term }, function (results, status) {
                response($.map(results.slice(0,6), function (item) {
                    return {
                        label: item.formatted_address, // appears in dropdown box
                        value: results[0].geometry.location, // inserted into input element when selected
                        geocode: item                  // all geocode data: used in select callback event
                    }
                }));

                $(".ui-autocomplete").addClass("myClass");
                $(".ui-autocomplete").append("<div style='padding:3px 0;'> <a data-target='#myModal' onclick='CannotFindLocation();' style='cursor: pointer; cursor: hand; margin-left:7px; color:#3cf;' data-toggle='modal' href='#' draggable='false'>Can't find location ?</a></div>");
                                        
            })
        },

        // event triggered when drop-down option selected
        select: function (event, ui) {
            update_ui_Venue(ui.item.value, ui.item.geocode.geometry.location)
           //update_map(ui.item.geocode.geometry)

        }
    });


    // triggered when user presses a key in the address box
    $("#txtEventLocation").bind('keydown', function (event) {
        if (event.keyCode == 13) {
            //geocode_lookup('address', $('#txtEventLocation').val(), true);

            // ensures dropdown disappears when enter is pressed
            $('#txtEventLocation').autocomplete("disable")
        } else {
            // re-enable if previously disabled above
            $('#txtEventLocation').autocomplete("enable")

        }
    });
};


$(document).ready(function () {
    if ($('#map_canvas').length) {
    gmaps_init_MultiVenue();
    autocomplete_init_MultiVenue();
   // gmaps_init_Venue();
    autocomplete_init_Venue();
    };
   
});

function a()
{
    alert("Mission Successful !!!!");
}



// fill in the UI elements with new position data
function update_ui_Venue(address, latLng) {
    $('#txtEventLocation').autocomplete("close");
    $('#txtEventLocation').val(address);
    //$('#gmaps-output-latitude').html(latLng.lat());
    //$('#gmaps-output-longitude').html(latLng.lng());
}

// fill in the UI elements with new position data
function update_ui_MultiVenue(address, latLng) {
    $('#VenueName').autocomplete("close");
    $('#VenueName').val(address);
    //$('#gmaps-output-latitude').html(latLng.lat());
    //$('#gmaps-output-longitude').html(latLng.lng());
}