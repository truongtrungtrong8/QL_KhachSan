var mapObject = L.map("map", { center: [10.0316021442065, 105.75310272495963], zoom: 10 });
//hoặc: var mapObject = L.map('map').setView([10.030249, 105.772097], 17);

//Bản đồ nền dạng Raster
L.tileLayer(
    "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
    {
        attribution: '&copy; <a href="http://' +
            'www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }
).addTo(mapObject);

$(document).ready(getLocation());

var layerObject = L.layerGroup().addTo(mapObject);


// lưu vị trí của người dùng
let x, y;
var x2 = document.getElementById("ycoor").value;
var y2 = document.getElementById("xcoor").value;
// lấy vị trí
function getLocation(x, y) {
    x2 = x;
    y2 = y;
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(setPosition);
    }
    else {
        alert("Geolocation is not supported by this browser.");
    }
}

var control;

// thêm routing bắt đầu từ vị trí của người dùng
function setPosition(position) {
    x = position.coords.latitude;
    y = position.coords.longitude;
    control = L.Routing.control(L.extend(window.lrmConfig, {
        waypoints: [
            L.latLng(x, y),
            L.latLng(x2, y2)
        ],
        language: 'vi',
        formatter: new L.Routing.Formatter({
            language: 'vi'
        }),
        geocoder: L.Control.Geocoder.nominatim(),
        routeWhileDragging: true,
        reverseWaypoints: true,
        showAlternatives: true,
        //router: L.Routing.graphHopper('29fd90fc-1dec-410e-ae81-80db57c19ad7'),
        altLineOptions: {
            styles: [
                { color: 'black', opacity: 0.15, weight: 9 },
                { color: 'white', opacity: 0.8, weight: 6 },
                { color: 'blue', opacity: 0.5, weight: 2 }
            ]
        },
        createMarker: function (i, waypoint, n) {
            const marker = L.marker(waypoint.latLng, {
                draggable: true,
                bounceOnAdd: false,
                bounceOnAddOptions: {
                    duration: 1000,
                    height: 800,
                    function() {
                        (bindPopup(myPopup).openOn(map))
                    }
                },
                icon: L.icon({
                    iconUrl: '/leaflet/css/images/redicon.png',
                    iconSize: [30, 40],
                    iconAnchor: [30, 35],
                    popupAnchor: [-3, -76],
                    shadowUrl: '/leaflet/css/images/marker-shadow.png',
                    shadowSize: [30, 40],
                    shadowAnchor: [30, 35]
                })
            });
            return marker;
        }
    })).addTo(mapObject);
    L.Routing.errorControl(control).addTo(mapObject);
}




