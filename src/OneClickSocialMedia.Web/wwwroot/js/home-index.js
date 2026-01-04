// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// keep this global (nav, shared behavior) as its for the whole site.

    const fileInput = document.getElementById("inputGroupFile02");
    const preview = document.getElementById("imagePreview");
    const placeholder = document.getElementById("imagePlaceholder");


    fileInput.addEventListener("change", function () {
        const file = this.files[0];
        if (!file) return;

        const reader = new FileReader();
        reader.onload = function (e) {
            preview.src = e.target.result;
            preview.classList.remove("d-none");
            placeholder.classList.add("d-none");
        };

        reader.readAsDataURL(file);
    });