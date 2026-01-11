var ClienteService = (function () {

    var BASE = "/Services/ClienteService.asmx/";

    return {
        getEstadosCiviles: function (cb) {
            ApiClient.post(BASE + "GetEstadosCiviles", null, cb);
        },

        search: function (cedula, nombre, cb) {
            ApiClient.post(BASE + "Search", {
                cedula: cedula || null,
                nombre: nombre || null
            }, cb);
        },

        save: function (cliente, cb) {
            ApiClient.post(BASE + "Save", { cliente: cliente }, cb);
        },

        remove: function (id, cb) {
            ApiClient.post(BASE + "Delete", { id: id }, cb);
        }
    };

})();
