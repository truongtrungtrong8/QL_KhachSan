var mapObject = L.map("map", { center: [10.029768199902994, 105.76862483005652], zoom: 15 });

L.tileLayer(
    "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
    {
        attribution: '&copy; <a href="http://' +
            'www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }
).addTo(mapObject);

var layerObject = L.layerGroup().addTo(mapObject);

let windowObjectReference;
let windowFeatures = "left=500,top=100,width=500,height=500";

function openPopup(id) {
    windowObjectReference = window.open("/HotelDetail/Index/" + id, "mozillaWindow", windowFeatures);

}

$(document).ready(loadAllMap());

// load feature từ CSDL
function loadAllMap() {
    $.getJSON('/Home/GetPointMap', function (response) {
        if (response != null) {
            $.each(response, function (index, value) {// dữ liệu nhận đc từ controller
                var id = value.hotelid;
                var price = value.price;
                var pointx = value.pointx;
                var pointy = value.pointy;
                var name = value.name;
                var tempPoint = "point(" + pointx + " " + pointy + ")" 
                var wkt = new Wkt.Wkt();
                wkt.read(tempPoint);
                var feature = { "type": "Feature", 'properties': { 'Name': price }, "geometry": wkt.toJson() };
                L.geoJSON(feature).addTo(layerObject);
                L.geoJSON(feature).addTo(layerObject).bindPopup('Tên khách sạn: ' + name + '<br/>' +
                    '<a href="#" onclick="openPopup(' + id + ')">' + price + '</a>' + '<br/>' +
                    '<a href="/Customers/Customers/Routing?X=' + pointx + '&Y=' + pointy + '">Chỉ đường</a>', { closeOnClick: false, autoClose: false }).openPopup();
            });
        };
    }
    );
    getLocation();
}



var pointStyle1 = L.icon({								//Cho điểm khi click
    iconUrl: "/leaflet/css/images/blueicon.png",
    shadowUrl: "/leaflet/css/images/marker-shadow.png",
    iconAnchor: [13, 41]  //Giữa đáy ảnh 25, 41 (RClick trên ảnh / Properties)
});
var pointStyle2 = L.icon({								//cho điểm khi tìm thỏa khoảng cách
    iconUrl: "/leaflet/css/images/redicon.png",
    shadowUrl: "/leaflet/css/images/marker-shadow.png",
    iconAnchor: [13, 41]
});
var lineStyle1 = { color: "blue", weight: 2 };				//cho đường tìm thỏa khoảng cách
var lineStyle2 = { color: "red", weight: 1 };				//Cho đường nối


var layerGroup = L.layerGroup().addTo(mapObject);
var myLocation = L.layerGroup().addTo(mapObject);		//chứa điểm khi click
var findLocations = L.layerGroup().addTo(mapObject);	//chứa các đối tượng tìm khi thỏa khoảng cách

//Thêm điều khiển mới là Textbox rỗng lên bản đồ
var control2 = L.control({ position: "topleft" });
control2.onAdd = function (map) {
    var div = L.DomUtil.create("div", "div1");
    div.innerHTML = '<input id="txtkc" type="number" style="width:200px;height:25px" placeholder="Nhập khoảng cách cần tìm (km)"/>';
    return div;
};
control2.addTo(mapObject);

// lưu vị trí của người dùng
let x, y;

// lấy vị trí
function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(setPosition);
    }
    else {
        alert("Geolocation is not supported by this browser.");
    }
}

// thêm routing bắt đầu từ vị trí của người dùng
function setPosition(position) {
    x = position.coords.latitude;
    y = position.coords.longitude;

    L.marker([x, y], { icon: pointStyle2 }).addTo(myLocation);
}

$(document).on('change', '#txtkc', function () {
    if ($("#txtkc").val() == "") {
        myLocation.clearLayers();
        L.marker([x,y], { icon: pointStyle2 }).addTo(myLocation);
        findLocations.clearLayers();
        layerGroup.clearLayers();
        layerObject.clearLayers();
        loadAllMap();
    }
    else {
        myLocation.clearLayers();
        L.marker([x,y], { icon: pointStyle2 }).addTo(myLocation);
        var clickCoords = L.latLng(x, y);
        findLocations.clearLayers();
        layerGroup.clearLayers();
        layerObject.clearLayers();

        $.getJSON('/Home/GetPointMap', function (response) {
            if (response != null) {
                $.each(response, function (index, value) {// dữ liệu nhận đc từ controller
                    var id = value.id;
                    var pointx = value.pointx;
                    var price = value.price;
                    var pointy = value.pointy;
                    var name = value.name;
                    var tempPoint = "point(" + pointx + " " + pointy + ")"
                    var wkt = new Wkt.Wkt();
                    wkt.read(tempPoint)
                    var feature = { "type": "Feature", 'properties': { 'Name': name }, "geometry": wkt.toJson() };
                    // nếu là điểm thì lấy vị trí lon, lat
                    if (wkt.toJson().type == "Point") {
                        var x = feature.geometry.coordinates[0];
                        var y = feature.geometry.coordinates[1];

                        // lấy trị tuyệt đối của điểm chọn tới các điểm còn lại
                        let x1 = Math.abs(clickCoords.lng - x);
                        let y1 = Math.abs(clickCoords.lat - y);
                        // tính khoảng cách giữa 2 điểm
                        let dis = parseFloat(Math.sqrt(x1 * x1 + y1 * y1) * 100);
                        let kc = parseFloat($("#txtkc").val());
                        // nếu khoảng cách giữa 2 điểm < khoảng cách ở textbox thì hiện lên bản đồ
                        if (kc >= dis) {
                            L.geoJSON(feature).addTo(layerGroup);
                            L.geoJSON(feature).addTo(layerObject).bindPopup('Tên khách sạn: ' + name + '<br/>' +
                                "Cách vị trí của bạn: " + dis.toFixed(3) + " km" + '<br/>' +
                                '<a href="#" onclick="openPopup(' + id + ')">' + price + '</a>' + '<br/>' +
                                '<a href="/Customers/Customers/Routing?X=' + pointx + '&Y=' + pointy + '">Chỉ đường</a>', { closeOnClick: false, autoClose: false }).openPopup();
                                
                        }
                    }
                }
                );
            };
        }
        );


    }
});


