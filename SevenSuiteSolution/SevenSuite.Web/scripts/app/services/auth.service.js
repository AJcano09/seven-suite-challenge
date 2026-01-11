var AuthService = (function () {

    var BASE_URL = "/Services/AuthService.asmx/";

    function ajax(method, payload, cb) {
        $.ajax({
            url: BASE_URL + method,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: payload ? JSON.stringify(payload) : "{}",

            success: function (res) {
                if (typeof cb === "function") {
                    cb(res.d);
                }
            },

            error: function (xhr) {
                console.error("Auth error:", xhr.responseText);

                if (typeof cb === "function") {
                    cb({ success: false });
                }
            }
        });
    }

    return {
        login: function (username, password, cb) {
            ajax("Login", { username:username, password:password }, cb);
        },

        logout: function (cb) {
            ajax("Logout", null, cb);
        },

        hasSession: function (cb) {
            ajax("HasSession", null, cb);
        },

        getSessionRemainingMinutes: function (cb) {
            ajax("GetSessionRemainingMinutes", null, cb);
        }
    };

})();
