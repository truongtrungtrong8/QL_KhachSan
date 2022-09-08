function dangKy(id) {
    if (confirm('Bạn có muốn đăng ký đăng tin Khách sạn không?')) {
        alert("Đăng ký thành công!");
        location.href = "/Customers/Customers/Businessregistration/" + id;
    } else {
        alert("Đăng ký không thành công!");
    }
    
}