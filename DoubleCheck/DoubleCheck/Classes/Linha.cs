using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleCheck.Classes
{
    public class Linha
    {
        public List<string[]> texto { get; set; } = new List<string[]>();
        public List<Efeitos> ListaEfeitos = new List<Efeitos>();
    }
}
