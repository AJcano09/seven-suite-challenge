using SevenSuite.Entities;
using SevenSuite.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Services;

namespace SevenSuite.Web.Services
{
    /// <summary>
    /// Summary description for ClienteService
    /// </summary>
    [WebService(Namespace = "http://seventsuite.local/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ClienteService : System.Web.Services.WebService
    {

        private readonly SevenSuite.BLL.ClienteService _service =
             new SevenSuite.BLL.ClienteService();

        [WebMethod]
        public ApiResponse<List<EstadoCivil>> GetEstadosCiviles()
        {
            try
            {
                return ApiResponse<List<EstadoCivil>>
                    .Ok(_service.GetEstadosCiviles());
            }
            catch (Exception ex)
            {
                return ApiResponse<List<EstadoCivil>>
                    .Fail("InternalError", ex.Message);
            }
        }

        [WebMethod]
        public ApiResponse<List<Cliente>> Search(string cedula, string nombre)
        {
            try
            {
                return ApiResponse<List<Cliente>>
                    .Ok(_service.Search(cedula, nombre));
            }
            catch (Exception ex)
            {
                return ApiResponse<List<Cliente>>
                    .Fail("InternalError", ex.Message);
            }
        }

        [WebMethod]
        public ApiResponse<int> Save(ClienteDto cliente)
        {
            try
            {
                if (cliente == null)
                    return ApiResponse<int>.Fail("ValidationError", "Cliente requerido");

                DateTime fechaNac;

                if (!DateTime.TryParseExact(
                    cliente.FechaNac,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out fechaNac))
                {
                    return ApiResponse<int>.Fail(
                        "ValidationError",
                        "Formato de fecha inválido. Use yyyy-MM-dd"
                    );
                }

                var entity = new Cliente
                {
                    Id = cliente.Id,
                    Cedula = cliente.Cedula,
                    Nombre = cliente.Nombre,
                    Genero = cliente.Genero,
                    FechaNac = fechaNac,
                    EstadoCivilId = cliente.EstadoCivilId
                };

                var id = _service.Save(entity);
                return ApiResponse<int>.Ok(id);
            }
            catch (Exception ex)
            {
                return ApiResponse<int>.Fail(
                    "BusinessError",
                    ex.Message
                );
            }
        }

        [WebMethod]
        public ApiResponse<bool> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return ApiResponse<bool>.Ok(true);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>
                    .Fail("BusinessError", ex.Message);
            }
        }
    }
}
