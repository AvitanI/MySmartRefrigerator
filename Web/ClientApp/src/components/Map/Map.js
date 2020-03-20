import React, { useEffect } from 'react';
import { map, tileLayer } from 'leaflet'
import 'leaflet/dist/leaflet.css';

const ACCESS_TOKEN = 'pk.eyJ1IjoiYXZpdGFuaWRhbiIsImEiOiJjazgwbzE3cGkwMzhkM2tsbW14NWtlMnl0In0.fduwu0tyyu3HF07rGIR_cQ';

const Map = () => {
    
    const loadMap = () => {
        const myMap = map('mapid', {
            center: [32.7996897, 35.0517954],
            zoom: 13
        }).setView([32.7996897, 35.0517954], 13)
    
        tileLayer(`https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=${ACCESS_TOKEN}`, {
            maxZoom: 18,
            attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' +
                '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
                'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
            id: 'mapbox/streets-v11',
            tileSize: 512,
            zoomOffset: -1
        }).addTo(myMap);
    };

    useEffect(() => {
        loadMap();
    }, []);

    return (
        <div id="mapid" style={{ height: '350px' }}></div>
    );
};

export default Map;