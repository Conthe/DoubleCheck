using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleCheck.Classes
{
    public class UR
    {
        public string CredenciadoraOuSubCred { get; set; }
        public string UsuarioFinalRecebedor { get; set; }
        public string ArranjoDePagamento { get; set; }
        public DateTime DataLiquidacao { get; set; }

        public List<Efeito> listaEfeitos { get; set; } = new List<Efeito>();

        public UR(string credenciadoraOuSubCred, string usuarioFinalRecebedor, string arranjoDePagamento, DateTime dataLiquidacao)
        {
            CredenciadoraOuSubCred = credenciadoraOuSubCred;
            UsuarioFinalRecebedor = usuarioFinalRecebedor;
            ArranjoDePagamento = arranjoDePagamento;
            DataLiquidacao = dataLiquidacao;
        }
    }
}
