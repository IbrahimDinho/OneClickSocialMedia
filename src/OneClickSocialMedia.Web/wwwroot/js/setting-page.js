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

document.addEventListener("DOMContentLoaded", function () {

    const inputAPI = document.getElementById("TwitterApiSecret");
    const toggleAPI = document.getElementById("TwitterAPISecretToggle");

    const inputToken = document.getElementById("TwitterAccessTokenSecret");
    const toggleToken = document.getElementById("TwitterAccessTokenSecretToggle");

    const inputInstaAPI = document.getElementById("InstagramAccessToken");
    const toggleInstaAPI = document.getElementById("InstagramSecretToggle");

    // Hide toggle initially
    toggleToken.style.display = "none";
    toggleAPI.style.display = "none";

    inputToken.addEventListener("input", function () {
        if (inputToken.value.trim().length > 0) {
            toggleToken.style.display = "flex";   // show button
        } else {
            toggleToken.style.display = "none";   // hide button
        }
    });

    inputAPI.addEventListener("input", function () {
        if (inputAPI.value.trim().length > 0) {
            toggleAPI.style.display = "flex";   // show button
        } else {
            toggleAPI.style.display = "none";   // hide button
        }
    });
});

const originalValues = new Map();

document.querySelectorAll(".credential-toggle").forEach(checkbox => {
    const targetClass = checkbox.dataset.target;
    const inputs = document.querySelectorAll(`.${targetClass}`);

    inputs.forEach(input => {
        originalValues.set(input, input.value);
    });

    checkbox.addEventListener("change", function () {
        inputs.forEach(input => {
            if (checkbox.checked) {
                input.disabled = false;
                input.value = "";
                input.placeholder = "Enter new value";
            } else {
                input.disabled = true;
                input.value = originalValues.get(input);
                input.placeholder = "";
            }
        });
    });
});
