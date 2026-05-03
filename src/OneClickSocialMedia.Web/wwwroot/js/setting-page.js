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

    const inputfbAPI = document.getElementById("FacebookAccessToken");
    const togglefbAPI = document.getElementById("FacebookSecretToggle");

    // Helper to setup toggle behaviour
    function setupToggle(input, toggle) {
        if (!input || !toggle) return;

        // Initial state
        toggle.style.display = input.value.trim().length > 0 ? "flex" : "none";

        input.addEventListener("input", function () {
            toggle.style.display = input.value.trim().length > 0 ? "flex" : "none";
        });
    }

    // Apply to all
    setupToggle(inputToken, toggleToken);
    setupToggle(inputAPI, toggleAPI);
    setupToggle(inputInstaAPI, toggleInstaAPI);
    setupToggle(inputfbAPI, togglefbAPI);
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
