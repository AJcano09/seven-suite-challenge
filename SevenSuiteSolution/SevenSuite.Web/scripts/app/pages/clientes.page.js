// ============================================================================
// ESTADO GLOBAL
// ============================================================================
var _clientesCache = [];
var __sessionSeconds = 0;
var __sessionTotalSeconds = 0;
var __sessionTimer = null;



// ============================================================================
// INIT
// ============================================================================
$(function () {
    initClientesPage();
});

function initClientesPage() {
    setupUI();
    bindEvents();
    cargarEstadosCiviles();
    buscar();
    showSessionTime();

    setInterval(showSessionTime, 30000);
}

// ============================================================================
// UI SETUP
// ============================================================================
function setupUI() {

    $(document).tooltip({
        position: {
            items: "[title]",
            show: false,
            hide: false,
            my: "left+15 center",
            at: "right center",
            collision: "flipfit"
        }
    });


    $("#FechaNac").datepicker({
        dateFormat: "yy-mm-dd",
        changeMonth: true,
        changeYear: true,
        yearRange: "1900:2025"
    });

    $("#dlgDelete").dialog({
        autoOpen: false,
        modal: true,
        buttons: {
            "Eliminar": function () {
                var id = $(this).data("id");
                doDelete(id);
                $(this).dialog("close");
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });
}

// ============================================================================
// EVENTOS
// ============================================================================
function bindEvents() {

    $("#btnBuscar").click(buscar);
    $("#btnGuardar").click(guardar);
    $("#btnLimpiar").click(limpiarForm);


    $("#Cedula, #Nombre, #FechaNac").on("input", function () {
        clearFieldError(this.id);
    });

    $("#Genero, #EstadoCivilId").on("change", function () {
        clearFieldError(this.id);
    });

    $("#btnLimpiarFiltros").click(function () {
        $("#fCedula").val("");
        $("#fNombre").val("");
        buscar();
    });

    $("#btnReporte").click(function () {
        var ced = encodeURIComponent($("#fCedula").val() || "");
        var nom = encodeURIComponent($("#fNombre").val() || "");
        window.location.href =
            "ReporteClientes.aspx?cedula=" + ced + "&nombre=" + nom;
    });

    $("#btnLogout").click(function () {
        AuthService.logout(function () {
            window.location.href = "Login.aspx";
        });
    });

    $("#FechaNac").datepicker({
        dateFormat: "yy-mm-dd",
        changeMonth: true,
        changeYear: true,
        yearRange: "1900:2025",

        onSelect: function () {
            clearFieldError("FechaNac");
        },

        onClose: function () {
            $(document).tooltip("enable");
            clearFieldError("FechaNac");
        },

        beforeShow: function () {
            $(document).tooltip("disable");
        }
    });

    $("#FechaNac").on("change", function () {
        clearFieldError("FechaNac");
    });

}

// ============================================================================
// CRUD
// ============================================================================
function cargarEstadosCiviles() {
    ClienteService.getEstadosCiviles(function (r) {
        if (!r || !r.Success) {
            alert("Error cargando estados civiles");
            return;
        }

        var ddl = $("#EstadoCivilId").empty();
        ddl.append("<option value=''>-- Seleccione --</option>");

        $.each(r.Data || [], function (_, x) {
            ddl.append(
                "<option value='" + x.Id + "'>" + x.Nombre + "</option>"
            );
        });
    });
}

function buscar() {
    ClienteService.search(
        $("#fCedula").val(),
        $("#fNombre").val(),
        function (r) {
            if (!r || !r.Success) {
                alert("Error buscando clientes");
                return;
            }

            _clientesCache = r.Data || [];
            $("#lblTotal").text(_clientesCache.length + " registros");

            renderTable(_clientesCache);
        }
    );
}

function guardar() {

    if (!validateForm()) return;

    var cliente = {
        Id: $("#Id").val() ? parseInt($("#Id").val(), 10) : 0,
        Cedula: $("#Cedula").val(),
        Nombre: $("#Nombre").val(),
        Genero: $("#Genero").val() || null,
        FechaNac: $("#FechaNac").val(),
        EstadoCivilId: $("#EstadoCivilId").val()
    };

    ClienteService.save(cliente, function (r) {
        if (!r || !r.Success) {
            alert(r.Error && r.Error.Message
                ? r.Error.Message
                : "Error guardando");
            return;
        }

        limpiarForm();
        buscar();
    });
}

function doDelete(id) {
    ClienteService.remove(id, function (r) {
        if (!r || !r.Success) {
            alert("Error eliminando");
            return;
        }
        buscar();
        limpiarForm();
    });
}

// ============================================================================
// RENDER / UI
// ============================================================================
function renderTable(list) {
    var tbody = $("#tblClientes").empty();
    $.each(list, function (_, c) {
        tbody.append(renderRow(c));
    });
}

function renderRow(c) {
    var fecha = toISODateString(c.FechaNac);

    return ""
        + "<tr>"
        + "<td>" + (c.Cedula || "") + "</td>"
        + "<td>" + (c.Nombre || "") + "</td>"
        + "<td>" + (c.Genero || "") + "</td>"
        + "<td>" + fecha + "</td>"
        + "<td>" + (c.EstadoCivil || "") + "</td>"
        + "<td class='actions'>"
        + "<button type='button' class='btn-mini' onclick='editar(" + c.Id + ")'>Editar</button> "
        + "<button type='button' class='btn-danger' onclick='confirmEliminar(" + c.Id + ")'>Eliminar</button>"
        + "</td>"
        + "</tr>";
}

window.editar = function (id) {
    var c = findById(id);
    if (!c) return;

    $("#lblMode").text("Editando");
    $("#Id").val(c.Id);
    $("#Cedula").val(c.Cedula || "");
    $("#Nombre").val(c.Nombre || "");
    $("#Genero").val(c.Genero || "");
    $("#FechaNac").val(toISODateString(c.FechaNac));
    $("#EstadoCivilId").val(c.EstadoCivilId || "");
};

window.confirmEliminar = function (id) {
    $("#dlgDelete").data("id", id).dialog("open");
};

function limpiarForm() {
    clearValidation();
    $("#lblMode").text("Nuevo");
    $("#Id").val("");
    $("#Cedula").val("").focus();
    $("#Nombre").val("");
    $("#Genero").val("");
    $("#FechaNac").val("");
    $("#EstadoCivilId").val("");
}

function updateSessionUI() {

    var min = Math.floor(__sessionSeconds / 60);
    var sec = __sessionSeconds % 60;
    var secStr = sec < 10 ? "0" + sec : sec;

    $("#lblSessionTime").text("Sesión");
    $("#lblSessionCountdown").text(min + ":" + secStr);

    var ratio = __sessionSeconds / __sessionTotalSeconds;

    var $timer = $("#lblSessionTime, #lblSessionCountdown");

    $timer.removeClass("session-green session-yellow session-red");

    if (ratio <= 0.2) {
        $timer.addClass("session-red");       //  < 20%
    } else if (ratio <= 0.5) {
        $timer.addClass("session-yellow");    //  < 50%
    } else {
        $timer.addClass("session-green");     //  > 50%
    }
}



// ============================================================================
// VALIDACIÓN
// ============================================================================
function validateForm() {
    clearValidation();
    var valid = true;

    if (!$("#Cedula").val()) {
        setError("Cedula", "Cédula requerida");
        valid = false;
    }

    if (!$("#Nombre").val()) {
        setError("Nombre", "Nombre requerido");
        valid = false;
    }

    if (!isValidISODate($("#FechaNac").val())) {
        setError("FechaNac", "Fecha requerida (YYYY-MM-DD)");
        valid = false;
    }

    if (!$("#EstadoCivilId").val()) {
        setError("EstadoCivilId", "Seleccione estado civil");
        valid = false;
    }

    if (!$("#Genero").val()) {
        setError("Genero", "Seleccione su genero.");
        valid = false;
    }

    return valid;
}

function clearValidation() {
    $(".input-error").removeClass("input-error");
    $(".field-error").hide();
}

function setError(fieldId, message) {
    $("#" + fieldId).addClass("input-error");
    $(".field-error[data-for='" + fieldId + "']")
        .text(message)
        .show();
}

function clearFieldError(fieldId) {
    $("#" + fieldId).removeClass("input-error");
    $(".field-error[data-for='" + fieldId + "']").hide();
}

// ============================================================================
// UTILIDADES
// ============================================================================
function findById(id) {
    for (var i = 0; i < _clientesCache.length; i++) {
        if (_clientesCache[i].Id == id) {
            return _clientesCache[i];
        }
    }
    return null;
}

function isValidISODate(value) {
    if (!value) return false;
    if (!/^\d{4}-\d{2}-\d{2}$/.test(value)) return false;

    var d = new Date(value);
    return !isNaN(d.getTime());
}

function toISODateString(value) {
    if (!value) return "";

    if (typeof value === "string" &&
        /^\d{4}-\d{2}-\d{2}$/.test(value)) {
        return value;
    }

    if (typeof value === "string") {
        var match = /\/Date\((\d+)\)\//.exec(value);
        if (match && match[1]) {
            var d = new Date(parseInt(match[1], 10));
            return d.getFullYear()
                + "-" + ("0" + (d.getMonth() + 1)).slice(-2)
                + "-" + ("0" + d.getDate()).slice(-2);
        }
    }

    return "";
}

function showSessionTime() {
    AuthService.getSessionRemainingMinutes(function (r) {

        if (!r || !r.Success) {
            forceLogout();
            return;
        }

        __sessionSeconds = r.Data * 60;

        // solo setear el total la primera vez
        if (__sessionTotalSeconds === 0) {
            __sessionTotalSeconds = __sessionSeconds;
        }

        updateSessionUI();

        if (__sessionTimer) {
            clearInterval(__sessionTimer);
        }

        __sessionTimer = setInterval(tickSessionCountdown, 1000);
    });
}



function tickSessionCountdown() {
    __sessionSeconds--;

    if (__sessionSeconds <= 0) {
        clearInterval(__sessionTimer);
        forceLogout();
        return;
    }

    updateSessionUI();
}

function forceLogout() {
    // Evitar múltiples llamadas
    if (window.__loggingOut) return;
    window.__loggingOut = true;

    AuthService.logout(function () {
        alert("Tu sesión ha expirado. Debes iniciar sesión nuevamente.");
        window.location.href = "Login.aspx";
    });
}



