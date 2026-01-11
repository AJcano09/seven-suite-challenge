<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SevenSuite.Web.Login" %>
<%@ Import Namespace="System.Web.Optimization" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inicio de Sesion</title>
<%: Styles.Render("~/content/css") %>
<%: Scripts.Render("~/bundles/jquery") %>
<%: Scripts.Render("~/bundles/jqueryui") %>
<%: Scripts.Render("~/bundles/app-core") %>
<%: Scripts.Render("~/bundles/login") %>
</head>
<body>
    <form id="form1" runat="server">

  <div class="login-page">

    <div class="login-card-2col">

      <!-- LEFT PANEL -->
      <div class="login-left-panel">
        <div class="brand">SevenSuite</div>

        <div class="quote">
          “Gestión simple y clara para tus clientes.”
        </div>

        <div class="author">
          Equipo SevenSuite
        </div>
      </div>

      <!-- RIGHT PANEL -->
      <div class="login-right-panel">

        <h2>¡Bienvenido!</h2>

        <p class="subtitle">
          Inicia sesión para continuar
        </p>

        <div class="input-group">
          <label>Usuario</label>
          <input id="txtUser" type="text" placeholder="usuario@email.com"
                 title="Ingrese su usuario" />
        </div>

        <div class="input-group">
          <label>Contraseña</label>
          <input id="txtPass" type="password" placeholder="••••••••"
                 title="Ingrese su contraseña" />
        </div>

        <button type="button" id="btnLogin" class="btn-primary">
          Iniciar Session
        </button>

      </div>

    </div>

  </div>

</form>

</body>
</html>
