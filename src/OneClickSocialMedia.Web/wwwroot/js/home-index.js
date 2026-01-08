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