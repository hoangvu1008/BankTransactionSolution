function TransactionView(amount, currency, date_created, description, from_account, from_account_bank, from_account_id, id, status, status_text, to_account, to_account_bank, to_account_id) {
    this.amount = amount;
    this.currency = currency;
    this.date_created = date_created;
    this.description = description;
    this.from_account = from_account;
    this.from_account_bank = from_account_bank;
    this.from_account_id = from_account_id;
    this.id = id;
    this.status = status;
    this.status_text = status_text;
    this.to_account = to_account;
    this.to_account_bank = to_account_bank;
    this.to_account_id = to_account_id;
}