window.popInterop = {
    registerGlobalOutsideClick: function (dotNetHelper) {
        function clickHandler(event) {
            console.log("Clicekd")
            // Check if click happened inside any popup
            if (!event.target.closest(".popup")) {
                dotNetHelper.invokeMethodAsync("ClosePopup");
                console.log("In")
            }
        }

        document.addEventListener("click", clickHandler);

        return {
            dispose: () => document.removeEventListener("click", clickHandler)
        };
    }
}