$(function () {
    $("#btnLogin").click(function () {
        var user = $("#txtUser").val();
        var pass = $("#txtPass").val();

        if (!user || !pass) {
            alert("Debe ingresar usuario y contraseña");
            return;
        }

        AuthService.login(user, pass, function (res) {
            if (!res.Success) {
                alert(res.Error.Message);
                return;
            }

            window.location.href = "Clientes.aspx";
        });
    });
});
