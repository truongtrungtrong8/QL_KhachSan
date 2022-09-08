function CheckAcc() {
    var vnf_regex = /((09|03|07|08|05)+([0-9]{8})\b)/g;
    if ($("#Username").val() == "") {
        alert("Số điện thoại không được trống");
        $('#Username').focus();
    }
    else if (vnf_regex.test($("#Username").val()) == false) {
        alert("Số điện thoại không hợp lệ");
        $('#Username').focus();
    }
    else if ($("#Password").val() == "") {
        alert("Mật khẩu không được trống");
        $('#Password').focus();
    }
    else if ($("#Password").val() != $("#Password2").val()) {
         alert("Mật khẩu không khớp");
        $('#Password2').focus();
    }
    else {
        $.get('/Home/CheckUserName?username=' + $("#Username").val(), function (response) {
            if (response == 1) {
                alert("Số điện thoại đã được đăng ký. Vui lòng chọn số điện thoại khác.");
                $('#Username').focus();
            }
            else {
                alert("Chúc mừng bạn đã đăng ký thành công.")
                $("#btnSignup").click();
            }
        });
    }
}
