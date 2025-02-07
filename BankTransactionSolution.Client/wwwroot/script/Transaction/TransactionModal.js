function BankAccountView(id, user_id, user_full_name, bank_account, bank_code, bank_name, balance) {
    this.id = id;
    this.user_id = user_id;
    this.user_full_name = user_full_name;
    this.bank_account = bank_account;
    this.bank_code = bank_code;
    this.bank_name = bank_name;
    this.balance = balance;
}

function TransactionModal() {
    var self = this;
    self.bankAccountsForUser = ko.observableArray([]);
    self.transactionHistories = ko.observableArray([]);
    self.selectedBankAccount = ko.observable();
    self.createTransaction = function () {
        var request = {
            from_account_id: $('#id_bank_account_user').val(),
            to_account_id: $('#detail_id_bank_account').val(),
            amount: $('#transaction_amount').val(),
            currency: "VND",
            description: $('#transaction_description').val(),
        };

        $.ajax({
            url: '/Transation/CreateTransaction',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(request),
            success: function (response) {
                Swal.fire({
                    toast: true,
                    position: 'top-end',
                    icon: 'success',
                    title: 'Tạo giao dịch thành công',
                    showConfirmButton: false,
                    timer: 3000
                });
                self.loadServices();
                //self.currentServiceId(response.success);
                $('#booking_process_content').val('');
                closeDetailModal();
                const imageContainer = document.getElementById("imageCreateContainer");
                imageContainer.innerHTML = "";
            },
            error: function () {
                console.error("AJAX Error - cannot create service");
                Swal.fire({
                    toast: true,
                    position: 'top-end',
                    icon: 'error',
                    title: 'Không thể tạo .',
                    showConfirmButton: false,
                    timer: 3000
                });
            }
        });
    };

    self.loadBankAccountForUser = function () {
        $.ajax({
            url: '/BankAccount/GetListBankForUser/',
            type: 'GET',
            success: function (data) {
                console.log(data);
                var mappedServices = data.map(function (item) {
                    return new BankAccountView(item.id,
                        item.user_id,
                        item.user_full_name,
                        item.bank_account, item.bank_code,
                        item.bank_name,
                        item.balance);
                });
                self.bankAccountsForUser(mappedServices);
            },
            error: function () {
                alert("Không thể tải danh sách dịch vụ.");
            }
        });
    };

    self.loadTransactionHistory = function (bank) {
        self.selectedBankAccount(bank);
        console.log("Ngân hàng được chọn:", bank);

        $.ajax({
            url: '/Transaction/GetTransactionHistories',
            type: 'GET',
            data: { bank_account_id: bank.id },
            success: function (data) {
                console.log("Lịch sử giao dịch:", data);

                var html = `
            <div class="modal-content" id="historyModal" >
                <span class="close" id="closeHistoryBtn">&times;</span>
                <h2> Lịch sử giao dịch </h2>
                <p><strong>Ngân hàng:</strong> ${bank.bank_name}</p>
                <p><strong>Số tài khoản:</strong> ${bank.bank_account}</p>
                <table border="1">
                    <thead>
                        <tr>
                            <th>STT</th>
                            <th>Ngày</th>
                            <th>Từ</th>
                            <th>Đến</th>
                            <th>Số tiền</th>
                            <th>Mô tả</th>
                        </tr>
                    </thead>
                    <tbody>
            `;

                data.forEach(function (item, index) {
                    html += `
                <tr>
                    <td>${index + 1}</td> 
                    <td>${item.date_created}</td>
                    <td>${item.from_account} (${item.from_account_bank})</td>
                    <td>${item.to_account} (${item.to_account_bank})</td>
                    <td>${item.amount} ${item.currency}</td>
                    <td>${item.description}</td>
                </tr>
            `;
                });

                html += `
                    </tbody>
                </table>
            </div>
        `;

                $("#historyModal").html(html).show();

                $("#closeHistoryBtn").on("click", function () {
                    $("#historyModal").hide();
                });
            },
            error: function () {
                alert("Không thể tải lịch sử giao dịch.");
            }
        });
    };

    self.closeHistoryModal = function () {
        console.log("aaa");
        $("#historyModal").hide();
    };

    self.loadBankAccountForUser();
}