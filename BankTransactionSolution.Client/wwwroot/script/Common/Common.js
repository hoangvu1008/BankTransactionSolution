function closeDetailModal() {
    const modal = document.getElementById("detailModal");
    if (modal) {
        modal.style.display = "none";
    }

    // Kiểm tra sự tồn tại của "page1" và "page2" trước khi cập nhật style
    const page1 = document.getElementById("page1");
    const page2 = document.getElementById("page2");

    if (page1) {
        page1.style.display = "block";
    }

    if (page2) {
        page2.style.display = "none";
    }

    $(modal).off('keydown');
    $(modal).off('click');

}

