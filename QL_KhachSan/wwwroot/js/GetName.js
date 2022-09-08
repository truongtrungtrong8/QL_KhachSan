function getNamePro(id, index) {
    $.get('/Home/GetNamePro?id=' + String(id), function (response) {
        var temp = response;
        document.getElementById("pro_" + index).innerHTML = temp;
    });
}

function getNameDis(id, id2, index) {
    $.get('/Home/GetNameDis?proid=' + String(id) + '&disid=' + String(id2), function (response) {
        document.getElementById("dis_" + index).innerHTML = response;
    });
}
function getAddressHotel(id, id2, id3, name) {
    $.get('/Home/GetAddressHotel?proid=' + String(id) + '&disid=' + String(id2) + '&warid=' + String(id3), function (response) {
        document.getElementById(name).innerHTML = response;
    });
}

function getMinPrice(id, index, name) {
    $.get('/Home/GetMinPriceHotel?id=' + String(id), function (response) {
        if (index != null && name == null) {
            document.getElementById("price_" + index).innerHTML = response.toLocaleString('it-IT') + "<small> VNĐ/đêm</small>";
            document.getElementById("priceold_" + index).innerHTML = (response + response / 100 * 30).toLocaleString('it-IT', { style: 'currency', currency: 'VND' });
        }
        else if(index == null && name != null){
            document.getElementById(name).innerHTML = response.toLocaleString('it-IT');
        }
    });
}
function getImagesHotel(id) {
    $.get('/Home/GetImagesHotel?id=' + String(id), function (response) {
        if (response != null) {
            //start
            var div = document.createElement("div");
            div.setAttribute("id", "slide-room-lg");
            div.setAttribute("class", "owl-carousel owl-theme");
            div.setAttribute("style", "display: block; opacity: 1");
            document.getElementById("slide-lg").appendChild(div);

            var div = document.createElement("div");
            div.setAttribute("class", "owl-wrapper-outer");
            div.setAttribute("id", "owl-wrapper-outer-id");
            document.getElementById("slide-room-lg").appendChild(div);

            var div = document.createElement("div");
            div.setAttribute("class", "owl-wrapper");
            div.setAttribute("id", "owl-wrapper-id");
            div.setAttribute("style", "width: 328px");
            document.getElementById("owl-wrapper-outer-id").appendChild(div);
            //end

            var div = document.createElement("div");
            div.setAttribute("class", "row");
            div.setAttribute("id", "row-sm");
            document.getElementById("slide-sm").appendChild(div);
            var div = document.createElement("div");
            div.setAttribute("class", "col-md-8 col-md-offset-2");
            div.setAttribute("id", "col-md-8 col-md-offset-2 id");
            document.getElementById("row-sm").appendChild(div);
            var div = document.createElement("div");
            div.setAttribute("id", "slide-room-sm");
            document.getElementById("col-md-8 col-md-offset-2 id").appendChild(div);

            $.each(response, function (index, value) {
                console.log(value);

                var div = document.createElement("div");
                div.setAttribute("class", "owl-item");
                div.setAttribute("id", "owl-item-id");
                document.getElementById("owl-wrapper-id").appendChild(div);

                var elem = document.createElement("img");
                elem.setAttribute("src", "/images/" + value.roomimageAvt);
                document.getElementsByClassName("owl-item-id")[index].appendChild(elem);

                var elem = document.createElement("img");
                elem.setAttribute("src", "/images/" + value.roomimageAvt);
                document.getElementById("slide-room-sm").appendChild(elem);
            });
            
        }
    });
}