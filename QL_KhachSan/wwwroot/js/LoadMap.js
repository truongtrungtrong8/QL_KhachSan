
var mapObject = L.map("map", { center: [10.038035, 105.777698], zoom: 9 });

L.tileLayer(
    "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
    {
        attribution: '&copy; <a href="http://' +
            'www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }
).addTo(mapObject);



//Thêm điều khiển vẽ; Icon mặc nhiên trong thư mục css/images
//https://cdnjs.com/libraries/leaflet.draw
var drawnItems = L.featureGroup().addTo(mapObject);
new L.Control.Draw({
    edit: {
        featureGroup: drawnItems,
    },
    draw: {
        circle: false,
        circlemarker: false, 		// Turns off this drawing tool
    },
}).addTo(mapObject);

//layer để giữ đối tượng đang vẽ hoặc đang được chọn
var layer = new L.Layer();

//Khi vẽ thì thêm vào lớp drawnItems
mapObject.on("draw:created", function (e) {
    layer = e.layer;
    layer.addTo(drawnItems);
    var popupContent =
        'Bạn đã chọn thêm khách sạn tại vị trí này';
    layer.bindPopup(popupContent).openPopup();
});


var control = L.control({ position: "topright" });
control.onAdd = function (map) {
    var div = L.DomUtil.create("div", "divsave");
    div.innerHTML = '<input type="button" id="save" class="btn btn-success btn-lg" value="Save">';
    return div;
};
control.addTo(mapObject);
drawnItems.on('popupopen', function (e) {
    layer = e.layer;
});


$("#save").on("click", function () {
    drawnItems.eachLayer(function (layer) {
        var geo = layer.toGeoJSON();
        var x = layer.toGeoJSON().geometry.coordinates[0];
        var y = layer.toGeoJSON().geometry.coordinates[1];

        $.post("/Owners/Hotels/AddPoint?x=" + x + "&y=" + y);


    });

    window.close();
    alert("Thêm thành công!");
});
