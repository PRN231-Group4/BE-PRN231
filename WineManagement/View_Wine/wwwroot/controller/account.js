$(document).ready(function () {
    var token = localStorage.getItem('token');


    if (token) {
        $('#account-section').hide();
        $('#account-register').hide(); 
        $('#logout-button').show();
    } else {
        $('#account-section').show();
        $('#account-register').show();
        $('#logout-button').hide();
    }

    // Đăng xuất
    $('#logout-button').on('click', function () {
        localStorage.removeItem('token');
        window.location.href = '/login';
    });



    $('#registerForm').on('submit', function (event) {
        event.preventDefault();

        var registerData = {
            username: $('#username').val(),
            password: $('#password').val(),
            roleId: $('#roleId').val(),
            status: $('#status').val() === 'true'
        };

        $.ajax({
            url: '/Register/Register',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(registerData),
            success: function (response, textStatus, xhr) {
                if (xhr.status === 200 || xhr.status === 201) {
                    alert('Đăng ký thành công.');
                    window.location.href = '/login';
                } else {
                    alert('Cập nhật tài khoản không trả về đúng mã trạng thái.');
                }
            },
            error: function (xhr) {
                console.error(xhr); // Log chi tiết lỗi
                var errorMessage = xhr.responseJSON?.message || 'Đăng ký thành công.';
                alert(errorMessage);
                window.location.href = '/login';
            }
        });
    });


    $('#loginForm').on('submit', function (event) {
        event.preventDefault();

        var loginData = {
            username: $('#username').val(),
            password: $('#password').val()
        };

        $.ajax({
            url: '/Account/Login',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(loginData),
            success: function (response) {
                if (response.code === 200 && response.data && response.data.token) {
                    localStorage.setItem('token', response.data.token);
                    alert('Đăng nhập thành công.');
                    $('#account-section').hide(); // Ẩn phần "Tài khoản"
                    $('#logout-button').show(); // Hiển thị nút "Đăng Xuất"
                    window.location.href = '/';
                } else {
                    alert('Đăng nhập thất bại: ' + (response.message || 'Không tìm thấy token.'));
                }
            },
            error: function (xhr) {
                var errorMessage = xhr.responseJSON?.message || 'Đăng nhập thất bại.';
                alert(errorMessage);
            }
        });
    });

   
});
