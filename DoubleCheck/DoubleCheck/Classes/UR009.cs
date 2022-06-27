using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleCheck.Classes
{
    public class UR009
    {
        public string RefExterna { get; set; }
        public string DataReferencia { get; set; }
        public string CredenciadoraOuSub { get; set; }
        public string UsuarioFinalRecebedor { get; set; }
        public string ArranjoDePagamento { get; set; }
        public string DataLiquidacao { get; set; }
        public string TitularUnidadeRecebivel { get; set; }
        public string ValorBrutoTotal { get; set; }
        public string ValorConstituidoTotal { get; set; }
        public string ValorConstituidoPreContratado { get; set; }
        public string ValorLiquidadoPosContratadas { get; set; }

        public UR009(string refExterna, string dataReferencia, string credenciadoraOuSub, string usuarioFinalRecebedor, string arranjoDePagamento, string dataLiquidacao, string titularUnidadeRecebivel, string valorBrutoTotal, string valorConstituidoTotal, string valorConstituidoPreContratado, string valorLiquidadoPosContratadas)
        {
            RefExterna = refExterna;
            DataReferencia = dataReferencia;
            CredenciadoraOuSub = credenciadoraOuSub;
            UsuarioFinalRecebedor = usuarioFinalRecebedor;
            ArranjoDePagamento = arranjoDePagamento;
            DataLiquidacao = dataLiquidacao;
            TitularUnidadeRecebivel = titularUnidadeRecebivel;
            ValorBrutoTotal = valorBrutoTotal;
            ValorConstituidoTotal = valorConstituidoTotal;
            ValorConstituidoPreContratado = valorConstituidoPreContratado;
            ValorLiquidadoPosContratadas = valorLiquidadoPosContratadas;
        }
    }
}
