using System;
using System.Data;
using Microsoft.Reporting.WebForms;
using SevenSuite.BLL;


namespace SevenSuite.Web
{
    public partial class ReporteClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            string cedula = string.IsNullOrWhiteSpace(Request.QueryString["cedula"])
    ? null
    : Request.QueryString["cedula"].Trim();

            string nombre = string.IsNullOrWhiteSpace(Request.QueryString["nombre"])
                ? null
                : Request.QueryString["nombre"].Trim();

            rvClientes.Reset();
            rvClientes.ProcessingMode = ProcessingMode.Local;

       
            rvClientes.LocalReport.ReportPath =
                Server.MapPath("~/Reports/ClientesReporte.rdlc");
            var service = new ClienteService();
            DataTable dt = service.GetReporte(
                string.IsNullOrWhiteSpace(cedula) ? null : cedula,
                string.IsNullOrWhiteSpace(nombre) ? null : nombre
            );

            rvClientes.LocalReport.DataSources.Clear();
            rvClientes.LocalReport.DataSources.Add(
                new ReportDataSource("ClientesDS", dt)
            );

           
            rvClientes.LocalReport.Refresh();
        }

    }
}