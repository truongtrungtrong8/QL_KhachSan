$(document).ready(loadPro());

function loadPro() {
    $.getJSON('/Owners/Hotels/GetDataPro', function (response) {
        if (response != null) {
            var menu = $("#ProvinceIdd");
            $("#DistrictIdd").append("<option>" + "Tất cả" + "</option>");
            $("#WardIdd").append("<option>" + "Tất cả" + "</option>");
            menu.append("<option>Tất cả</option>");
            $.each(response, function (index, value) {
                menu.append("<option>" +  value.provinceName + "</option>");
            });
        };
    }
    );

}

$(document).on('change', '#ProvinceIdd', function () {
    var menu = $("#DistrictIdd");
    $("#WardIdd").empty();
    $("#WardIdd").append("<option>" + "Tất cả" + "</option>");
    menu.empty();
    
    
    $.getJSON('/Owners/Hotels/GetDataDis', function (response) {
        if (response != null) {
            menu.append("<option>Tất cả</option>");
            $.each(response, function (index, value) {
                if (value.provinceId == $("#ProvinceId").val()) {
                    menu.append("<option>" + value.districtName + "</option>");
                }
            });
        };
    }
    );
    $.getJSON('/Owners/Hotels/GetIdPro?name=' + $("#ProvinceIdd").val(), function (response) {
        $("#ProvinceId").val(response.provinceId);
    }
    );
});

$(document).on('change', '#DistrictIdd', function () {
    var menu = $("#WardIdd");
    menu.empty();
    $.getJSON('/Owners/Hotels/GetDataWar', function (response) {
        if (response != null) {
            menu.append("<option>Tất cả</option>");
            $.each(response, function (index, value) {
                if (value.districtId == $("#DistrictId").val()) {
                    menu.append("<option>" + value.wardName + "</option>");
                }
            });
        };
    }
    );
    $.getJSON('/Owners/Hotels/GetIdDis?name=' + $("#DistrictIdd").val(), function (response) {
        $("#DistrictId").val(response.districtId);
    }
    );
});

$(document).on('change', '#WardIdd', function () {
    $.getJSON('/Owners/Hotels/GetIdWar?name=' + $("#WardIdd").val(), function (response) {
        $("#WardId").val(response.wardId);
    }
    );
});

function openPopup() {
    
    //Khai báo đối tượng chính chứa bản đồ gắn vào thẻ div tên "map"
    var mapObject = L.map("map", { center: [10.481006, 104.900434], zoom: 17 });
    //hoặc: var mapObject = L.map('map').setView([10.030249, 105.772097], 17);

    //Bản đồ nền dạng Raster
    L.tileLayer(
        "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
        {
            attribution: '&copy; <a href="http://' +
                'www.openstreetmap.org/copyright">OpenStreetMap</a>'
        }
    ).addTo(mapObject);

    //	var popup = L.popup();
    //	mapObject.on('click', function(e) {
    //    alert("Lat, Lon : " + e.latlng.lat + ", " + e.latlng.lng)
    //});






    var pxthem = "", qhthem = "", dc = "";

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

    //Tạo nút lệnh Save
    var control = L.control({ position: "topright" });
    control.onAdd = function (map) {
        var div = L.DomUtil.create("div", "divsave");
        div.innerHTML = '<input type="button" id="save" class="btn btn-success btn-lg" value="Save">';
        return div;
    };
    control.addTo(mapObject);
    var qhthem = "", pxthem = "";
    //Khi vẽ thì thêm vào lớp drawnItems
    mapObject.on("draw:created", function (e) {
        layer = e.layer;
        layer.addTo(drawnItems);
        var popupContent =
            '<form>' +
            '<input type="button" value="Submit" id="submit"><br><br>' +
            '</form>';
        layer.bindPopup(popupContent).openPopup();
    });



    drawnItems.on('popupopen', function (e) {
        layer = e.layer;
    });


    // lưu feature vào CSDL
    $("#save").on("click", function () {
        drawnItems.eachLayer(function (layer) {
            var geo = layer.toGeoJSON();
            var x = layer.toGeoJSON().geometry.coordinates[0];
            var y = layer.toGeoJSON().geometry.coordinates[1];


            location.href = "/Owners/Hotels/AddPoint?x=" + x + "&y=" + y;
            window.close();
        });

    });

}
