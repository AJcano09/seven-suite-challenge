using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenSuite.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNac { get; set; }
        public int EstadoCivilId { get; set; }
        public string EstadoCivil { get; set; }
    }
}
