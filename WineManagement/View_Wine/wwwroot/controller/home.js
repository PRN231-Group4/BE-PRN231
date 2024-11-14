$(document).ready(function () {
    document.querySelectorAll('[id^="iconToggle-"]').forEach(function (icon) {
        icon.addEventListener("click", function () {
            const accountId = this.id.split('-')[1]; // Lấy AccountId từ id của icon
            const actionIcons = document.getElementById(`actionIcons-${accountId}`);

            // Chuyển đổi giữa hiển thị và ẩn
            actionIcons.style.display = actionIcons.style.display === "none" || actionIcons.style.display === "" ? "block" : "none";
        });
    });
    
    $('.view-account').on('click', function () {
        var accountId = $(this).data('id');

        // Make an Ajax request to get account details
        $.ajax({
            url: '/Home/GetAccountDetails',
            type: 'GET',
            data: { accountId: accountId },
            success: function (data) {
                // Populate modal with data
                $('#modalAccountId').text(data.accountId);
                $('#modalUsername').text(data.username);
                var statusText = data.status === 'active' ? 'Mở' : 'Khóa';
                $('#modalStatus').text(statusText);

                var role;
                switch (data.roleId) {
                    case 1:
                        role = 'Quản trị viên';
                        break;
                    case 2:
                        role = 'Quản lý';
                        break;
                    case 3:
                        role = 'Người dùng';
                        break;
                    default:
                        role = 'Không xác định';
                }
                $('#modalRoleId').text(role);

                $('#accountDetailsModal').modal('show');
            },
            error: function () {
                alert('Không thể lấy thông tin tài khoản.');
            }
        });
    });


    //View
    $('.edit-account').on('click', function () {
        var accountId = $(this).data('id');
        
        $.ajax({
            url: '/Home/GetAccountDetails',
            type: 'GET',
            data: { accountId: accountId },
            success: function (data) {
                $('#editAccountId').val(data.accountId);
                $('#editUsername').val(data.username);
                $('#editStatus').val(data.status);
                $('#editRoleId').val(data.roleId);

                $('#editAccountModal').modal('show');
            },
            error: function () {
                alert('Không thể lấy thông tin tài khoản');
            }
        });
    });


    //Update
    $('#saveChanges').on('click', function () {
        var accountId = $('#editAccountId').val();
        var updateData = {
            username: $('#editUsername').val(),
            role: $('#role').val(), 
            status: $('#editStatus').val(),
            roleId: $('#editRoleId').val()
        };

        $.ajax({
            url: '/Home/UpdateAccount?accountId=' + accountId,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(updateData),
            success: function (response, textStatus, xhr) {
                if (xhr.status === 204) { 
                    alert('Cập nhật tài khoản thành công.');
                    $('#editAccountModal').modal('hide');
                    location.reload(); 
                } else {
                    alert('Cập nhật tài khoản không trả về đúng mã trạng thái.');
                }
            },
            error: function (xhr) {
                alert('Cập nhật tài khoản thất bại.');
            }
        });
    });

   
  
});

//Xoa1
function deleteAccount(accountId) {
    if (!accountId || accountId <= 0) {
        alert('ID tài khoản không hợp lệ.');
        return;
    }
    $.ajax({
        url: '/Home/DeleteUser',
        type: 'DELETE',
        data: { accountId: accountId },
        success: function () {
            alert('Xóa tài khoản thành công.');
            location.reload(); 
        },
        error: function () {
            alert('Xóa tài khoản thất bại.');
        }
    });

}
function getAuthHeaders() {
    const token = localStorage.getItem('token');
    if (token) {
        return {
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json' 
        };
    }
    return {};
}

function fetchProtectedData() {
    $.ajax({
        url: '/Home/GetAccountDetails',
        type: 'GET',
        headers: getAuthHeaders(),
        success: function (response) {
            console.log('Dữ liệu:', response);
        },
        error: function (xhr) {
            console.error('Lỗi:', xhr);
            if (xhr.status === 401) {
                alert('Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.');
                localStorage.removeItem('token'); 
                window.location.href = '/tai-khoan'; 
            }
        }
    });
}
