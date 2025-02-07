function openBankListModal() {
    let bankList = document.getElementById("bankList");
    bankList.innerHTML = ""; 

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
