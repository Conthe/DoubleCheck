using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleCheck.Classes
{
    public class UR008
    {
        public string CredenciadoraOuSubCred { get; set; }
        public string ConstituicaoUR { get; set; }
        public string UsuarioFinalRecebedor { get; set; }
        public string ArranjoDePagamento { get; set; }
        public string DataLiquidacao { get; set; }


        public List<Efeito008> listaEfeitos { get; set; } = new List<Efeito008>();

        public UR008(string credenciadoraOuSubCred, string constituicaoUR, string usuarioFinalRecebedor, string arranjoDePagamento, string dataLiquidacao)
        {
            CredenciadoraOuSubCred = credenciadoraOuSubCred;
            UsuarioFinalRecebedor = usuarioFinalRecebedor;
            ArranjoDePagamento = arranjoDePagamento;
            DataLiquidacao = dataLiquidacao;
            ConstituicaoUR = constituicaoUR;
        }
    }
}
