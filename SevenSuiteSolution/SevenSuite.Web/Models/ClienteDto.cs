using System;

namespace SevenSuite.Web.Models
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string FechaNac { get; set; }
        public int EstadoCivilId { get; set; }
    }
}