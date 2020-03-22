/* React */
import React, { useEffect, useState } from 'react';
import PropTypes from 'prop-types';

/* Leaflet */
import L from 'leaflet';
import 'leaflet/dist/leaflet.css';
import icon from 'leaflet/dist/images/marker-icon.png';
import iconShadow from 'leaflet/dist/images/marker-shadow.png';

// Set default icon for marker since we have url error from leaflet
L.Marker.prototype.options.icon = L.icon({
    iconUrl: icon,
    shadowUrl: iconShadow
});

// Token for map box tile.
// See Also: https://account.mapbox.com/access-tokens/
const ACCESS_TOKEN = 'pk.eyJ1IjoiYXZpdGFuaWRhbiIsImEiOiJjazgwbzE3cGkwMzhkM2tsbW14NWtlMnl0In0.fduwu0tyyu3HF07rGIR_cQ';

// Map warpper style
const DEAFULT_STYLE = { height: '350px' };

const Map = (props) => {
    const { 
        initialView, 
        markers = [], 
        style = DEAFULT_STYLE 
    } = props;

    const [map, setMap] = useState(null);

    const loadMap = () => {
        const myMap = L.map('mapid', {
            center: initialView,
            zoom: 13
        });//.setView(HAIFA_LOCATION, 13)
    
        L.tileLayer(`https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=${ACCESS_TOKEN}`, {
            maxZoom: 18,
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' +
                '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
                'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            id: 'mapbox/streets-v11',
            tileSize: 512,
            zoomOffset: -1
        }).addTo(myMap);

        setMap(myMap);
    };

    useEffect(() => {
        loadMap();
    }, []);

    useEffect(() => {
        // Return when map not initialized
        if(!map) {
            return;
        }
        
        // Add markers to map
        if(markers.length) {
            markers.forEach(marker => marker.addTo(map));

            // Fit bounds according to markers
            map.fitBounds(markers.map(marker => [marker._latlng.lat, marker._latlng.lng]));
        }
    }, [map, markers]);

    return (
        <div id="mapid" style={style}></div>
    );
};

Map.propTypes = {
    initialView: PropTypes.arrayOf(PropTypes.number),
    markers: PropTypes.arrayOf(PropTypes.object),
    style: PropTypes.object
}

export default Map;