using DoubleCheck.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DoubleCheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string[] efeitos = new string[99999];
            string[] linhas = new string[99999];
            char delimiterChar = '\n';
            List<UR> listaUnidadesRecebiveisArq1 = new List<UR>();
            List<UR> listaUnidadesRecebiveisArq2 = new List<UR>();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                string dados = File.ReadAllText(fileName);
                configuraLinhaEfeitos(ref efeitos, delimiterChar, listaUnidadesRecebiveisArq1, dados);
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                string dados = File.ReadAllText(fileName);
                configuraLinhaEfeitos(ref efeitos, delimiterChar, listaUnidadesRecebiveisArq2, dados);
            }

            if (listaUnidadesRecebiveisArq1.Count == listaUnidadesRecebiveisArq2.Count && listaUnidadesRecebiveisArq2.Count > 0)
            {
                try
                {
                    List<UR> listaOrdenada1 = new List<UR>();
                    List<UR> listaOrdenada2 = new List<UR>();

                    listaOrdenada1 = listaUnidadesRecebiveisArq1.OrderBy(x => x.DataLiquidacao)
                        .ThenBy(x => x.UsuarioFinalRecebedor)
                        .ThenBy(x => x.CredenciadoraOuSubCred)
                        .ThenBy(x => x.ArranjoDePagamento).ToList();

                    listaOrdenada2 = listaUnidadesRecebiveisArq2.OrderBy(x => x.DataLiquidacao)
                        .ThenBy(x => x.UsuarioFinalRecebedor)
                        .ThenBy(x => x.CredenciadoraOuSubCred)
                        .ThenBy(x => x.ArranjoDePagamento).ToList();

                    ComparaListasAP008(listaOrdenada1, listaOrdenada2);

                    MessageBox.Show("Informações conferem.");
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else if (listaUnidadesRecebiveisArq1.Count == 0 || listaUnidadesRecebiveisArq2.Count == 0)
            {
                MessageBox.Show("É necessário selecionar um arquivo com informações.");
            }
            else
            {
                MessageBox.Show("Arquivos não possuem o mesmo tamanho de linhas.");
            }
        }

        private static void ComparaListasAP008(List<UR> listaOrdenada1, List<UR> listaOrdenada2)
        {
            for (int i = 0; i < listaOrdenada1.Count; i++)
            {
                if (!listaOrdenada1[i].UsuarioFinalRecebedor.Equals(listaOrdenada2[i].UsuarioFinalRecebedor))
                {
                    throw new Exception("UsuarioFinalRecebedor não corresponde.");
                }
                else if (!listaOrdenada1[i].CredenciadoraOuSubCred.Equals(listaOrdenada2[i].CredenciadoraOuSubCred))
                {
                    throw new Exception("CredenciadoraOuSubCred não corresponde");
                }
                else if (!listaOrdenada1[i].ArranjoDePagamento.Equals(listaOrdenada2[i].ArranjoDePagamento))
                {
                    throw new Exception("ArranjoDePagamento não corresponde");
                }
                else if (!listaOrdenada1[i].DataLiquidacao.Equals(listaOrdenada2[i].DataLiquidacao))
                {
                    throw new Exception("DataLiquidacao não corresponde");
                }
                ComparaEfeitosUR(listaOrdenada1, listaOrdenada2, i);
            }
        }

        private static void ComparaEfeitosUR(List<UR> listaOrdenada1, List<UR> listaOrdenada2, int i)
        {
            for (int j = 0; j < listaOrdenada1[i].listaEfeitos.Count; j++)
            {
                try
                {
                    if (!listaOrdenada1[i].listaEfeitos[j].Protocolo.Equals(listaOrdenada2[i].listaEfeitos[j].Protocolo))
                    {
                        throw new Exception("Protocolo do efeito não corresponde. Protocolo do Efeito: " + listaOrdenada1[i].listaEfeitos[j].Protocolo);
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].IndicadorEfeitosContrato.Equals(listaOrdenada2[i].listaEfeitos[j].IndicadorEfeitosContrato))
                    {
                        throw new Exception("IndicadorEfeitosContrato do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].EntidadeRegistradora.Equals(listaOrdenada2[i].listaEfeitos[j].EntidadeRegistradora))
                    {
                        throw new Exception("EntidadeRegistradora do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].TipoEfeito.Equals(listaOrdenada2[i].listaEfeitos[j].TipoEfeito))
                    {
                        throw new Exception("TipoEfeito do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].BeneficiarioTitular.Equals(listaOrdenada2[i].listaEfeitos[j].BeneficiarioTitular))
                    {
                        throw new Exception("BeneficiarioTitular do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].RegrasDivisao.Equals(listaOrdenada2[i].listaEfeitos[j].RegrasDivisao))
                    {
                        throw new Exception("RegrasDivisao do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].ValorComprometido.Equals(listaOrdenada2[i].listaEfeitos[j].ValorComprometido))
                    {
                        string valorStr1 = listaOrdenada1[i].listaEfeitos[j].ValorComprometido.Replace('.', ',');
                        string valorStr2 = listaOrdenada2[i].listaEfeitos[j].ValorComprometido.Replace('.', ',');
                        double valorArq1 = double.Parse(valorStr1);
                        double valorArq2 = double.Parse(valorStr2);
                        if (!valorArq1.Equals(valorArq2))
                        {
                            throw new Exception("Valor do efeito não corresponde");
                        }
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].DocumentoTitularDomicilio.Equals(listaOrdenada2[i].listaEfeitos[j].DocumentoTitularDomicilio))
                    {
                        throw new Exception("DocumentoTitularDomicilio do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].TipoConta.Equals(listaOrdenada2[i].listaEfeitos[j].TipoConta))
                    {
                        throw new Exception("TipoConta do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].COMPE.Equals(listaOrdenada2[i].listaEfeitos[j].COMPE))
                    {
                        throw new Exception("COMPE do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].ISPB.Equals(listaOrdenada2[i].listaEfeitos[j].ISPB))
                    {
                        throw new Exception("ISPB do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].Agencia.Equals(listaOrdenada2[i].listaEfeitos[j].Agencia))
                    {
                        throw new Exception("Agencia do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].NumeroContaPagamento.Equals(listaOrdenada2[i].listaEfeitos[j].NumeroContaPagamento))
                    {
                        throw new Exception("NumeroContaPagamento do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].NomeTitularDomicilio.Equals(listaOrdenada2[i].listaEfeitos[j].NomeTitularDomicilio))
                    {
                        throw new Exception("NomeTitularDomicilio do efeito não corresponde");
                    }
                    if (!listaOrdenada1[i].listaEfeitos[j].IDContrato.Equals(listaOrdenada2[i].listaEfeitos[j].IDContrato))
                    {
                        throw new Exception("IDContrato do efeito não corresponde");
                    }
                }
                catch (Exception error)
                {
                    throw new Exception(error.Message);
                }
            }
        }


        private static void configuraLinhaEfeitos(ref string[] efeitos, char delimiterChar, List<UR> listaUnidadeRecebiveis, string dados)
        {
            string[] linhas = dados.Split(delimiterChar);
            string infoLinha = string.Empty;
            foreach (string linha in linhas)
            {
                if (!string.IsNullOrEmpty(linha))
                {
                    string texto = PegarConteudoForaAspas(linha, 0);
                    var urInfo = texto.Split(';');
                    UR unidadeRecebivel = new UR(urInfo[0], urInfo[2], urInfo[3], DateTime.Parse(urInfo[4]));
                    string efeitoStr = PegarConteudoEntreAspas(linha, 0);
                    if (!string.IsNullOrEmpty(efeitoStr))
                    {
                        efeitos = efeitoStr.Split('|');
                        foreach (string efeito in efeitos)
                        {
                            Efeito efeitoLinha = new Efeito();
                            string[] efeitoConfigurado = efeito.Split(';');
                            PopulaCamposEfeito(efeitoLinha, efeitoConfigurado);
                            unidadeRecebivel.listaEfeitos.Add(efeitoLinha);
                        }
                    }
                    listaUnidadeRecebiveis.Add(unidadeRecebivel);
                }
            }
        }

        private static void PopulaCamposEfeito(Efeito efeitoLinha, string[] efeitoConfigurado)
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
    }
}
