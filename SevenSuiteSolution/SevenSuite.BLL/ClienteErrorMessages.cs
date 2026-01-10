using SevenSuite.BLL.Enums;

namespace SevenSuite.BLL
{
    public static class ClienteErrorMessages
    {
        public static string GetMessage(ClienteError error)
        {
            switch (error)
            {
                case ClienteError.CedulaRequerida:
                    return "La cédula es obligatoria.";

                case ClienteError.NombreRequerido:
                    return "El nombre es obligatorio.";

                case ClienteError.FechaNacimientoRequerida:
                    return "La fecha de nacimiento es obligatoria.";

                case ClienteError.EstadoCivilRequerido:
                    return "Debe seleccionar un estado civil.";

                case ClienteError.IdInvalido:
                    return "Id inválido.";

                default:
                    return "Error de validación.";
            }
        }
    }
}
