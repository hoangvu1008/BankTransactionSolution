function BankAccountModal() {
    var self = this;
    var page_size = 10;
    self.currentPage = ko.observable(1);
    self.bankAccounts = ko.observableArray([]);
    self.bankAccountsForUser = ko.observableArray([]);
    self.totalPages = ko.observable(1);
    self.currentServiceId = ko.observable();

    self.loadBankAccount = function () {
        $.ajax({
            url: '/BankAccount/GetBankAccount',
            type: 'GET',
            success: function (data) {
                console.log(data);
                var mappedServices = data.map(function (item) {
                    return new BankAccountView(item.id,
                        item.user_id,
                        item.user_full_name,
                        item.bank_account, item.bank_code,
                        item.bank_name);
                });
                self.bankAccounts(mappedServices);

                setTimeout(function () {
                    $('.table tbody tr').off('click');
                }, 100);
            },
            error: function () {
                alert("Không thể tải danh sách dịch vụ.");
            }
        });
    };

    self.loadBankAccountForUser = function () {
        $.ajax({
            url: '/BankAccount/GetListBankForUser',
            type: 'GET',
            success: function (data) {
                console.log(data);
                var mappedServices = data.map(function (item) {
                    return new BankAccountView(item.id,
                        item.user_id,
                        item.user_full_name,
                        item.bank_account, item.bank_code,
                        item.bank_name);
                });
                self.bankAccountsForUser(mappedServices);
            },
            error: function () {
                alert("Không thể tải danh sách dịch vụ.");
            }
        });
    };

    self.viewDetail = function (id) {
        self.currentServiceId(id);
        OpenDetailBankAccount(id);
        openBankListModal();
    };

    self.createTransaction = function () {
        var amount = parseFloat($('#transaction_amount').val()) || 0;
        var balance = parseFloat($('#bank_balance_display').text().replace(/,/g, '')) || 0; // Lấy số dư và loại bỏ dấu phẩy
        var toAccountId = parseInt($('#detail_id_bank_account').val(), 10) || 0;

        console.log(`Số dư hiện tại: ${balance}, Số tiền giao dịch: ${amount}`);

        if (amount > balance) {
            Swal.fire({
                toast: true,
                position: 'top-end',
                icon: 'error',
                title: 'Tài khoản của bạn không đủ số dư, vui lòng chọn tài khoản ngân hàng khác.',
                showConfirmButton: false,
                timer: 3000
            });
            return;
        }
        console.log("from_account_id", $('#id_bank_account_user').val())
        var request = {
            from_account_id: $('#id_bank_account_user').val(),
            to_account_id: toAccountId,
            amount: amount,
            currency: "VND",
            description: $('#transaction_description').val()
        };

        console.log("Yêu cầu giao dịch:", request);

        $.ajax({
            url: '/Transaction/CreateTransaction',
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
                closeDetailModal();
                $('#transaction_description').val("");
                $('#transaction_amount').val(0);
            },
            error: function () {
                console.error("AJAX Error - Không thể tạo giao dịch");
                Swal.fire({
                    toast: true,
                    position: 'top-end',
                    icon: 'error',
                    title: 'Không thể tạo giao dịch.',
                    showConfirmButton: false,
                    timer: 3000
                });
            }
        });
    };


    self.loadBankAccount();
}
