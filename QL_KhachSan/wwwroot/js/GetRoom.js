$(document).ready(loadPro());

function loadPro() {
    $.getJSON('/Owners/Roomimages/GetAllHotel', function (response) {
        if (response != null) {
            var menu = $("#ListHotel");
            $("#RoomId").append("<option>" + "Tất cả" + "</option>");
            menu.append("<option>Tất cả</option>");
            $.each(response, function (index, value) {
                menu.append("<option>" + value.hotelName + "</option>");
            });
        };
    }
    );

}

$(document).on('change', '#ListHotel', function () {

    $.getJSON('/Owners/Roomimages/GetIdHotel?name=' + $("#ListHotel").val(), function (response) {
        if (response != null) {
            document.getElementById("HotelId").value = response.hotelId;

        };
    }
    );

    var menu = $("#ListRoom");
    menu.empty();
    document.getElementById("RoomId").value = "";

    $.getJSON('/Owners/Roomimages/GetAllRoom', function (response) {
        if (response != null) {
            menu.append("<option>Tất cả</option>");
            $.each(response, function (index, value) {
                if (value.hotelId == document.getElementById("HotelId").value) {
                    menu.append("<option>" + value.roomName + "</option>");
                }
            });
        };
    }
    );

    
});

$(document).on('change', '#ListRoom', function () {

    $.getJSON('/Owners/Roomimages/GetIdRoom', function (response) {
        if (response != null) {
            $.each(response, function (index, value) {
                if (value.hotelId == document.getElementById("HotelId").value && value.roomName == $("#ListRoom").val())
                document.getElementById("RoomId").value = value.roomId;
            });

        };
    }
    );
});