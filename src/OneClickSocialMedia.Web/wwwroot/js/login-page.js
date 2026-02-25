function togglePassword(inputId, btn) {
    const input = document.getElementById(inputId);
    const icon = btn.querySelector("i");
    const text = btn.querySelector(".toggle-text");

    if (!input.value || input.value.trim() === "") {
        return;
    }

    if (input.type === "password") {
        input.type = "text";
        icon.classList.remove("fa-eye");
        icon.classList.add("fa-eye-slash");
        text.innerText = "Hide";
    } else {
        input.type = "password";
        icon.classList.remove("fa-eye-slash");
        icon.classList.add("fa-eye");
        text.innerText = "Show";
    }
}
