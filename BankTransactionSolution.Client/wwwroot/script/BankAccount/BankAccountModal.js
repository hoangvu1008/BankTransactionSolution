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
    };

    self.loadBankAccount();
}
