function OpenDetailBankAccount(id) {
    $.ajax({
        url: '/BankAccount/GetBankAccountDetail/' + id,
        type: 'GET',
        success: function (data) {
            if (data) {
                console.log(data);
                $('#detail_full_name').val(data.user_full_name);
                $('#detail_bank_account').val(data.bank_account);
                $('#detail_bank_code').val(data.bank_code);
                $('#detail_bank_name').val(data.bank_name);
                $('#detail_id_bank_account').val(data.id);

                $('#detailModal').show();

                $(document).on('keydown', function (event) {
                    if (event.key === "Escape") {
                        closeDetailModal();
                    }
                });

                $(document).on('click', function (event) {
                    if (!$(event.target).closest('#detailModal .modal-content').length) {
                        closeDetailModal();
                    }
                });
            } else {
                alert("Không tìm thấy dịch vụ.");
            }
        },
        error: function () {
            alert("Không thể tải danh sách dịch vụ.");
        }
    });
}


function openBankListModal() {
    let bankList = document.getElementById("bankList");
    bankList.innerHTML = ""; // Xóa danh sách cũ trước khi cập nhật

    $.ajax({
        url: '/BankAccount/GetListBankForUser/',
        type: 'GET',
        success: function (data) {
            if (data) {
                console.log(data);

                let firstBank = data[0];
                updateBankCard(firstBank);

                data.forEach((bank, index) => {
                    let li = document.createElement("li");
                    li.textContent = `${bank.bank_name} (${bank.bank_code}) - ${bank.bank_account} | Số dư: ${bank.balance} VND`;

                    li.onclick = function () {
                        updateBankCard(bank);
                        document.getElementById("bankListModal").style.display = "none";
                    };

                    bankList.appendChild(li);
                });

                // Hiển thị modal danh sách ngân hàng
                document.getElementById("bankListModal").style.display = "block";

            } else {
                alert("Không tìm thấy danh sách ngân hàng.");
            }
        },
        error: function () {
            alert("Không thể tải danh sách ngân hàng.");
        }
    });
}


// Đóng modal
function closeBankListModal() {
    document.getElementById("bankListModal").style.display = "none";
}

function updateBankCard(bank) {
    //document.getElementById("id_bank_account_user").textContent = bank.id;
    $('#id_bank_account_user').val(bank.id);
    document.getElementById("bank_name_display").textContent = bank.bank_name;
    document.getElementById("bank_code_display").textContent = bank.bank_code;
    document.getElementById("bank_account_display").textContent = bank.bank_account;
    document.getElementById("bank_balance_display").textContent = `${bank.balance} VND`;
}


