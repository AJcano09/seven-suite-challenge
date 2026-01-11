<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="SevenSuite.Web.Clientes" %>

<%@ Import Namespace="System.Web.Optimization" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Clientes</title>
    <%: Styles.Render("~/content/css") %>
    <%: Scripts.Render("~/bundles/jquery") %>
    <%: Scripts.Render("~/bundles/jqueryui") %>
    <%: Scripts.Render("~/bundles/app-core") %>
    <%: Scripts.Render("~/bundles/clientes") %>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">

            <!-- HEADER -->
            <div class="page-header">
                <div>
                    <h1>Clientes</h1>
                    <p class="subtitle">Mantenimiento y gestión de clientes</p>
                </div>

                <div class="header-actions">
                    <button type="button" id="btnReporte" class="btn-secondary" title="Ver reporte (ReportViewer) con filtros aplicados">
                        Ver Reporte
                    </button>
                    <button type="button" id="btnLogout" class="btn-secondary" title="Cerrar sesión">
                        Logout
                    </button>
                </div>
            </div>
            <div class="grid-layout">

                <!-- FORMULARIO -->
                <!-- FORMULARIO -->
                <div class="card">
                    <div class="card-title">
                        <h3>Datos del Cliente</h3>
                        <span id="lblMode" class="pill">Nuevo</span>

                        <div class="session-timer">
                            <span id="lblSessionTime"></span>
                            <span id="lblSessionCountdown"></span>
                        </div>
                    </div>


                    <input type="hidden" id="Id" />

                    <!-- ROW 1 -->
                    <div class="form-row">
                        <div class="form-group">
                            <label>Cédula</label>
                            <input
                                type="text"
                                id="Cedula"
                                title="Número de identificación del cliente" />
                            <span class="field-error" data-for="Cedula">Cédula requerida
                            </span>
                        </div>

                        <div class="form-group">
                            <label>Nombre</label>
                            <input
                                type="text"
                                id="Nombre"
                                title="Nombre completo del cliente" />
                            <span class="field-error" data-for="Nombre">Nombre requerido
                            </span>
                        </div>
                    </div>

                    <!-- ROW 2 -->
                    <div class="form-row">
                        <div class="form-group">
                            <label>Género</label>
                            <select id="Genero" title="Seleccione el género del cliente">
                                <option value="">-- Seleccione --</option>
                                <option value="M">Masculino</option>
                                <option value="F">Femenino</option>
                            </select>
                            <span class="field-error" data-for="Genero">Seleccione su genero.
                            </span>
                        </div>

                        <div class="form-group">
                            <label>Fecha Nacimiento</label>
                            <input
                                type="text"
                                id="FechaNac"
                                title="Fecha de nacimiento del cliente" />
                            <span class="field-error" data-for="FechaNac">Fecha requerida (YYYY-MM-DD)
                            </span>
                        </div>

                        <div class="form-group">
                            <label>Estado Civil</label>
                            <select
                                id="EstadoCivilId"
                                title="Estado civil del cliente">
                            </select>
                            <span class="field-error" data-for="EstadoCivilId">Seleccione un estado civil
                            </span>
                        </div>
                    </div>

                    <!-- ACTIONS -->
                    <div class="form-actions">
                        <button type="button" id="btnGuardar" class="btn-primary">
                            Guardar
                        </button>
                        <button type="button" id="btnLimpiar" class="btn-secondary">
                            Limpiar
                        </button>
                    </div>

                    <div id="msg" class="msg" style="display: none;"></div>
                </div>


                <!-- BUSQUEDA -->
                <div class="card">
                    <div class="card-title">
                        <h3>Búsqueda</h3>
                        <span class="muted">Filtra por cédula o nombre</span>
                    </div>

                    <div class="form-row">
                        <div class="form-group">
                            <label>Cédula</label>
                            <input type="text" id="fCedula" />
                        </div>

                        <div class="form-group">
                            <label>Nombre</label>
                            <input type="text" id="fNombre" />
                        </div>

                        <div class="form-group actions">
                            <button type="button" id="btnBuscar" class="btn-primary">Buscar</button>
                            <button type="button" id="btnLimpiarFiltros" class="btn-secondary">Limpiar</button>
                        </div>
                    </div>
                </div>

                <!-- TABLA -->
                <div class="card full-width">
                    <div class="card-title">
                        <h3>Listado</h3>
                        <span id="lblTotal" class="muted">0 registros</span>
                    </div>

                    <div class="table-wrap">
                        <table class="grid">
                            <thead>
                                <tr>
                                    <th>Cédula</th>
                                    <th>Nombre</th>
                                    <th>Género</th>
                                    <th>Fecha Nac</th>
                                    <th>Estado Civil</th>
                                    <th style="width: 160px;">Acciones</th>
                                </tr>
                            </thead>
                            <tbody id="tblClientes"></tbody>
                        </table>
                    </div>
                </div>

            </div>

        </div>
    </form>

    <!-- DIALOG DELETE -->
    <div id="dlgDelete" title="Eliminar cliente" style="display: none;">
        <p>¿Seguro que deseas eliminar este registro?</p>
    </div>

</body>
</html>
