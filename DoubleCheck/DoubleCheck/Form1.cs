using DoubleCheck.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace DoubleCheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int sucesso = 0;
        static int falha = 0;
        private async void btn_ComparaArqvs_Click(object sender, EventArgs e)
        {

            switch (cmb_EscolheAP.SelectedItem)
            {
                case ("AP008"):
                    string[] efeitos = new string[99999];
                    char delimiterChar = '\n';
                    StreamReader sr1;
                    StreamReader sr2;
                    string fileName1 = string.Empty;
                    string fileName2 = string.Empty;
                    long cont1 = 0;
                    long cont2 = 0;

                    sucesso = 0;
                    falha = 0;

                    SelecionarArquivos(ref fileName1, ref fileName2, ref cont1, ref cont2);

                    double contadorPorcentagem = 0;
                    if (cont1 > cont2)
                    {
                        sr1 = new StreamReader(fileName2);
                        sr2 = new StreamReader(fileName1);
                        contadorPorcentagem = cont2 * 0.30;
                    }
                    else
                    {
                        sr1 = new StreamReader(fileName1);
                        sr2 = new StreamReader(fileName2);
                        contadorPorcentagem = cont1 * 0.30;
                    }

                    int contadorAmostragem = 0;

                    while (!sr1.EndOfStream)
                    {
                        if (contadorAmostragem < (int)contadorPorcentagem)
                        {
                            string linha = sr1.ReadLine();
                            var urArq1 = configuraLinhaEfeitos(ref efeitos, delimiterChar, linha);
                            var csv = urArq1.CredenciadoraOuSubCred + ";" + urArq1.ConstituicaoUR + ";" + urArq1.UsuarioFinalRecebedor + ";" + urArq1.ArranjoDePagamento + ";" + urArq1.DataLiquidacao + ";";
                            string linha2 = string.Empty;
                            while (!sr2.EndOfStream)
                            {
                                string linha3 = sr2.ReadLine();
                                if (linha3.StartsWith(csv))
                                {
                                    linha2 = linha3;
                                    break;
                                }
                            }
                            sr2 = new StreamReader(fileName2);
                            if (linha2 == string.Empty)
                            {
                                LogError008(urArq1);
                            }
                            else
                            {
                                var urArq2 = configuraLinhaEfeitos(ref efeitos, delimiterChar, linha2);
                                ComparaAP008(urArq1, urArq2);
                                sucesso++;
                            }
                            contadorAmostragem++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    MessageBox.Show("A comparação foi finalizada. Sucesso: " + sucesso + "." + " Falhas: " + falha + "." + " Taxa de Amostragem: " + (int)contadorPorcentagem + ".");
                    break;
                case ("AP009"):
                    fileName1 = string.Empty;
                    fileName2 = string.Empty;
                    cont1 = 0;
                    cont2 = 0;
                    sucesso = 0;
                    falha = 0;

                    SelecionarArquivos(ref fileName1, ref fileName2, ref cont1, ref cont2);

                    contadorPorcentagem = 0;
                    if (cont1 > cont2)
                    {
                        sr1 = new StreamReader(fileName2);
                        sr2 = new StreamReader(fileName1);
                        contadorPorcentagem = cont2 * 0.30;
                    }
                    else
                    {
                        sr1 = new StreamReader(fileName1);
                        sr2 = new StreamReader(fileName2);
                        contadorPorcentagem = cont1 * 0.30;
                    }

                    contadorAmostragem = 0;
                    while (!sr1.EndOfStream)
                    {
                        if (contadorAmostragem < (int)contadorPorcentagem)
                        {
                            string linha = sr1.ReadLine();
                            var urArq1 = ConfiguraArq009(linha);
                            string linha2 = string.Empty;

                            while (!sr2.EndOfStream)
                            {
                                string linha3 = sr2.ReadLine();
                                if (linha3.StartsWith(linha))
                                {
                                    linha2 = linha3;
                                    break;
                                }
                            }
                            sr2 = new StreamReader(fileName2);
                            if (linha2 == string.Empty)
                            {
                                LogError009(urArq1);
                            }
                            else
                            {
                                var urArq2 = ConfiguraArq009(linha2);
                                ComparaAP009(urArq1, urArq2);
                                sucesso++;
                            }
                            contadorAmostragem++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    MessageBox.Show("A comparação foi finalizada. Sucesso: " + sucesso + "." + " Falhas: " + falha + "." + " Taxa de Amostragem: " + (int)contadorPorcentagem + ".");
                    break;
                case ("AP010"):
                    break;
                case ("AP011"):
                    break;
                case ("AP012"):
                    break;

            }

        }

        private void ComparaAP009(UR009 urArq1, UR009 urArq2)
        {
            bool erroEncontrado = false;

            if(!urArq1.RefExterna.Equals(urArq2.RefExterna))
            {
                LogError009(urArq1);
                erroEncontrado = true;
            }
            if (!erroEncontrado)
            {
                if (!urArq1.DataReferencia.Equals(urArq2.DataReferencia))
                {
                    LogError009(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                if (!urArq1.CredenciadoraOuSub.Equals(urArq2.CredenciadoraOuSub))
                {
                    LogError009(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                if (!urArq1.UsuarioFinalRecebedor.Equals(urArq2.UsuarioFinalRecebedor))
                {
                    LogError009(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                if (!urArq1.ArranjoDePagamento.Equals(urArq2.ArranjoDePagamento))
                {
                    LogError009(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                if (!urArq1.DataLiquidacao.Equals(urArq2.DataLiquidacao))
                {
                    LogError009(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                if (!urArq1.TitularUnidadeRecebivel.Equals(urArq2.TitularUnidadeRecebivel))
                {
                    LogError009(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                if (!urArq1.ValorBrutoTotal.Equals(urArq2.ValorBrutoTotal))
                {
                    LogError009(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                if (!urArq1.ValorConstituidoTotal.Equals(urArq2.ValorConstituidoTotal))
                {
                    LogError009(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                if (!urArq1.ValorConstituidoPreContratado.Equals(urArq2.ValorConstituidoPreContratado))
                {
                    LogError009(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                if (!urArq1.ValorLiquidadoPosContratadas.Equals(urArq2.ValorLiquidadoPosContratadas))
                {
                    LogError009(urArq1);
                    erroEncontrado = true;
                }
            }
        }

        private void LogError009(UR009 arqErro)
        {

            string dir = @"C:\LogErrorAPs";
            // If directory does not exist, create it
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            StreamWriter sw = new StreamWriter(@"C:\LogErrorAPs\log009.txt", true);
            sw.WriteLine(arqErro.RefExterna + ";" 
                + arqErro.DataReferencia + ";" 
                + arqErro.CredenciadoraOuSub + ";" 
                + arqErro.UsuarioFinalRecebedor + ";"
                + arqErro.ArranjoDePagamento + ";"
                + arqErro.DataLiquidacao + ";"
                + arqErro.TitularUnidadeRecebivel + ";"
                + arqErro.ValorBrutoTotal + ";"
                + arqErro.ValorConstituidoTotal + ";"
                + arqErro.ValorConstituidoPreContratado + ";"
                + arqErro.ValorLiquidadoPosContratadas);
            sw.Close();
            falha++;
        }

        private UR009 ConfiguraArq009(string linha)
        {
            var infoUr = linha.Split(';');
            UR009 ur = new UR009(infoUr[0], infoUr[1], infoUr[2], infoUr[3], infoUr[4], infoUr[5], infoUr[6], infoUr[7], infoUr[8], infoUr[9], infoUr[10]);
            return ur;
        }

        private void SelecionarArquivos(ref string fileName1, ref string fileName2, ref long cont1, ref long cont2)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileName1 = dlg.FileName;
                FileInfo fi = new FileInfo(fileName1);
                cont1 = CountLinesLINQ(fi);
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileName2 = dlg.FileName;
                FileInfo fi = new FileInfo(fileName2);
                cont2 = CountLinesLINQ(fi);
            }
        }

        public long CountLinesLINQ(FileInfo file)
            => File.ReadLines(file.FullName).Count();

        private static void ComparaAP008(UR008 urArq1, UR008 urArq2)
        {
            bool erroEncontrado = false;
            if (!urArq1.UsuarioFinalRecebedor.Equals(urArq2.UsuarioFinalRecebedor))
            {
                LogError008(urArq1);
                erroEncontrado = true;
            }
            if (!erroEncontrado)
            {
                if (!urArq1.CredenciadoraOuSubCred.Equals(urArq2.CredenciadoraOuSubCred))
                {
                    LogError008(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                if (!urArq1.ArranjoDePagamento.Equals(urArq2.ArranjoDePagamento))
                {
                    LogError008(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                if (!urArq1.DataLiquidacao.Equals(urArq2.DataLiquidacao))
                {
                    LogError008(urArq1);
                    erroEncontrado = true;
                }
            }
            if (!erroEncontrado)
            {
                ComparaEfeitosUR(urArq1, urArq2);
            }


        }

        private static void ComparaEfeitosUR(UR008 urArq1, UR008 urArq2)
        {
            if (urArq1.listaEfeitos.Count == urArq2.listaEfeitos.Count)
            {
                for (int j = 0; j < urArq1.listaEfeitos.Count; j++)
                {
                    try
                    {
                        bool erroEncontrado = false;
                        if (!urArq1.listaEfeitos[j].Protocolo.Equals(urArq2.listaEfeitos[j].Protocolo))
                        {
                            LogError008(urArq1);
                            erroEncontrado = true;
                            break;
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].IndicadorEfeitosContrato.Equals(urArq2.listaEfeitos[j].IndicadorEfeitosContrato))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].EntidadeRegistradora.Equals(urArq2.listaEfeitos[j].EntidadeRegistradora))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].TipoEfeito.Equals(urArq2.listaEfeitos[j].TipoEfeito))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].BeneficiarioTitular.Equals(urArq2.listaEfeitos[j].BeneficiarioTitular))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].RegrasDivisao.Equals(urArq2.listaEfeitos[j].RegrasDivisao))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].ValorComprometido.Equals(urArq2.listaEfeitos[j].ValorComprometido))
                            {
                                string valorStr1 = urArq1.listaEfeitos[j].ValorComprometido.Replace('.', ',');
                                string valorStr2 = urArq2.listaEfeitos[j].ValorComprometido.Replace('.', ',');
                                double valorArq1 = double.Parse(valorStr1);
                                double valorArq2 = double.Parse(valorStr2);
                                if (!valorArq1.Equals(valorArq2))
                                {
                                    LogError008(urArq1);
                                    erroEncontrado = true;
                                    break;
                                }
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].DocumentoTitularDomicilio.Equals(urArq2.listaEfeitos[j].DocumentoTitularDomicilio))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].TipoConta.Equals(urArq2.listaEfeitos[j].TipoConta))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].COMPE.Equals(urArq2.listaEfeitos[j].COMPE))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].ISPB.Equals(urArq2.listaEfeitos[j].ISPB))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].Agencia.Equals(urArq2.listaEfeitos[j].Agencia))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].NumeroContaPagamento.Equals(urArq2.listaEfeitos[j].NumeroContaPagamento))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].NomeTitularDomicilio.Equals(urArq2.listaEfeitos[j].NomeTitularDomicilio))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                        if (!erroEncontrado)
                        {
                            if (!urArq1.listaEfeitos[j].IDContrato.Equals(urArq2.listaEfeitos[j].IDContrato))
                            {
                                LogError008(urArq1);
                                erroEncontrado = true;
                                break;
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        throw new Exception(error.Message);
                    }
                }
            }
            else
            {
                LogError008(urArq1);
            }
        }

        private static void LogError008(UR008 arqErro)
        {
            string dir = @"C:\LogErrorAPs";
            // If directory does not exist, create it
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            StreamWriter sw = new StreamWriter(@"C:\LogErrorAPs\log008.txt", true);
            sw.WriteLine(arqErro.CredenciadoraOuSubCred + ";" + arqErro.ConstituicaoUR + ";" + arqErro.UsuarioFinalRecebedor + ";" + arqErro.ArranjoDePagamento + ";" + arqErro.DataLiquidacao + ";");
            sw.Close();
            falha++;
        }
        private static UR008 configuraLinhaEfeitos(ref string[] efeitos, char delimiterChar, string dados)
        {
            string[] linhas = dados.Split(delimiterChar);
            string infoLinha = string.Empty;
            foreach (string linha in linhas)
            {
                if (!string.IsNullOrEmpty(linha))
                {
                    string texto = PegarConteudoForaAspas(linha, 0);
                    var urInfo = texto.Split(';');
                    UR008 unidadeRecebivel = new UR008(urInfo[0], urInfo[1], urInfo[2], urInfo[3], urInfo[4]);
                    string efeitoStr = PegarConteudoEntreAspas(linha, 0);
                    if (!string.IsNullOrEmpty(efeitoStr))
                    {
                        efeitos = efeitoStr.Split('|');
                        foreach (string efeito in efeitos)
                        {
                            Efeito008 efeitoLinha = new Efeito008();
                            string[] efeitoConfigurado = efeito.Split(';');
                            PopulaCamposEfeito(efeitoLinha, efeitoConfigurado);
                            unidadeRecebivel.listaEfeitos.Add(efeitoLinha);
                        }
                    }
                    return unidadeRecebivel;
                }
            }
            return null;
        }

        private static void PopulaCamposEfeito(Efeito008 efeitoLinha, string[] efeitoConfigurado)
        {
            efeitoLinha.Protocolo = efeitoConfigurado[0];
            efeitoLinha.IndicadorEfeitosContrato = efeitoConfigurado[1];
            efeitoLinha.EntidadeRegistradora = efeitoConfigurado[2];
            efeitoLinha.TipoEfeito = efeitoConfigurado[3];
            efeitoLinha.BeneficiarioTitular = efeitoConfigurado[4];
            efeitoLinha.RegrasDivisao = efeitoConfigurado[5];
            efeitoLinha.ValorComprometido = efeitoConfigurado[6];
            efeitoLinha.DocumentoTitularDomicilio = efeitoConfigurado[7];
            efeitoLinha.TipoConta = efeitoConfigurado[8];
            efeitoLinha.COMPE = efeitoConfigurado[9];
            efeitoLinha.ISPB = efeitoConfigurado[10];
            efeitoLinha.Agencia = efeitoConfigurado[11];
            efeitoLinha.NumeroContaPagamento = efeitoConfigurado[12];
            efeitoLinha.NomeTitularDomicilio = efeitoConfigurado[13];
            efeitoLinha.IDContrato = efeitoConfigurado[14];
        }

        static string PegarConteudoEntreAspas(string texto, int linha)
        {
            var existeAspas = texto.Any(x => x == '"');
            if (existeAspas)
            {
                int i, j;
                string strLinha;
                EncontraIndice(texto, linha, out i, out j, out strLinha);

                return strLinha.Substring(i, j - i);
            }
            return string.Empty;
        }

        private static void EncontraIndice(string texto, int linha, out int i, out int j, out string strLinha)
        {
            i = 0;
            for (int x = 0; x < linha; x++) i = texto.IndexOf("\n", i) + 1; // Encontra a linha...
            j = texto.IndexOf("\n", i);
            if (j < 0) j = texto.Length - 1;

            strLinha = texto.Substring(i, j - i);
            i = strLinha.IndexOf("\"") + 1;
            j = strLinha.IndexOf("\"", i);
        }

        static string PegarConteudoForaAspas(string texto, int linha)
        {
            var existeAspas = texto.Any(x => x == '"');
            if (existeAspas)
            {
                int i, j;
                string strLinha;
                EncontraIndice(texto, linha, out i, out j, out strLinha);

                return strLinha.Remove(i, j - i);
            }
            return texto;
        }

        private void cmb_EscolheAP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
