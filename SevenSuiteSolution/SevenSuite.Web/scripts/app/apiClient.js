var ApiClient = (function () {

    function post(url, payload, cb) {
        $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: payload ? JSON.stringify(payload) : "{}",

            success: function (res) {
                if (typeof cb !== "function") return;

                if (!res || !res.d) {
                    cb({
                        Success: false,
                        Error: { Message: "Respuesta inválida del servidor" }
                    });
                    return;
                }

                cb(res.d);
            },

            error: function (xhr) {
                console.error("Error técnico:", xhr.responseText);

                if (typeof cb === "function") {
                    cb({
                        Success: false,
                        Error: { Message: "Error técnico del servidor" }
                    });
                }
            }
        });
    }

    return {
        post: post
    };

})();
