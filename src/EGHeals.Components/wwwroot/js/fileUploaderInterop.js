window.fileUploader = {
    readFileAsBase64: function (inputId) {
        return new Promise((resolve, reject) => {
            const input = document.getElementById(inputId);
            if (!input || !input.files || input.files.length === 0) {
                resolve(null);
                return;
            }

            const file = input.files[0];
            const reader = new FileReader();

            reader.onload = function (e) {
                resolve(e.target.result); // data:xxx;base64,...
            };

            reader.onerror = function () {
                reject("File read error");
            };

            reader.readAsDataURL(file);
        });
    },

    getFileSize: function (inputId) {
        const input = document.getElementById(inputId);
        if (!input || !input.files || input.files.length === 0) {
            return 0;
        }
        return input.files[0].size;
    },

    getFileName: function (inputId) {
        const input = document.getElementById(inputId);
        if (!input || !input.files || input.files.length === 0) {
            return "";
        }
        return input.files[0].name;
    }
};
