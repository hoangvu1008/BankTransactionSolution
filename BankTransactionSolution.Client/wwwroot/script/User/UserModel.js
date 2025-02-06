function UserModel() {
    var self = this;
    self.getDetail = function () {
        $.ajax({
            url: '/User/GetUser',
            type: 'GET',
            contentType: 'application/json',
            success: function (response) {
                console.log(response);
                $('#fullName').text(response.full_name);

                if (document.getElementById('profile_user_full_name')) {
                    document.getElementById('profile_user_full_name').value = response.full_name;
                }

                if (document.getElementById('profile_user_user_name')) {
                    document.getElementById('profile_user_user_name').value = response.user_name;
                }

                if (document.getElementById('profile_user_phone')) {
                    document.getElementById('profile_user_phone').value = response.phone;
                }

                if (document.getElementById('profile_user_email')) {
                    document.getElementById('profile_user_email').value = response.email;
                }
            },
            error: function () {
                console.error("Error while retrieving user details.");
            }
        });
    };

    //self.updateUser = function () {
    //    var newUser = {
    //        full_name: $('#profile_user_full_name').val(),
    //        telegram_username: $('#profile_user_telegram_username').val(),
    //        address: $('#profile_user_address').val(),
    //        email: $('#profile_user_email').val(),
    //        phone: $('#profile_user_phone').val(),
    //        is_active: true,
    //    };

    //    $.ajax({
    //        url: '/User/Update/',
    //        type: 'POST',
    //        contentType: 'application/json',
    //        data: JSON.stringify(newUser),
    //        success: function (response) {
    //            console.log("Success:", response);
    //            //self.loadServices();
    //            //closeDetailModal();
    //            location.reload();
    //            Swal.fire({
    //                toast: true,
    //                position: 'top-end',
    //                icon: 'success',
    //                title: 'Cập nhật thành công',
    //                showConfirmButton: false,
    //                timer: 3000
    //            });
    //            //self.isSubmitted(false);
    //        },
    //        error: function () {
    //            console.error("AJAX Error - cannot create service");
    //            alert('Không thể tạo dịch vụ');
    //        }
    //    });
    //};

    self.getDetail();
}

