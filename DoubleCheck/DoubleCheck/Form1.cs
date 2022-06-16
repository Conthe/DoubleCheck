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
            List<Linha> listaLinhasArq1 = new List<Linha>();
            List<Linha> listaLinhasArq2 = new List<Linha>();

            var dado1 = string.Empty;
            var dado2 = string.Empty;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                string dados = File.ReadAllText(fileName);
                dado1 = dados;
                configuraLinhaEfeitos(ref efeitos, delimiterChar, listaLinhasArq1, dados);
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                string dados = File.ReadAllText(fileName);
                dado2 = dados;
                configuraLinhaEfeitos(ref efeitos, delimiterChar, listaLinhasArq2, dados);
            }

            if (listaLinhasArq1.Count == listaLinhasArq2.Count && listaLinhasArq1.Count > 0)
            {
                try
                {
                    foreach(Linha linhaArq1 in listaLinhasArq1)
                    {
                        foreach(Efeito dadosEfeito in linhaArq1.ListaEfeitos)
                        {
                            string idEfeito = dadosEfeito.efeito[0];
                            bool efeitoEncontradoNoArq2 = false;
                            foreach (Linha linhaArq2 in listaLinhasArq2)
                            {
                                foreach(Efeito dadosEfeitoArq2 in linhaArq2.ListaEfeitos)
                                {
                                    if (!efeitoEncontradoNoArq2)
                                    {
                                        if(dadosEfeitoArq2.efeito[0] == idEfeito)
                                        {
                                            efeitoEncontradoNoArq2 = true;

                                            ComparaCamposEfeito(dadosEfeito, dadosEfeitoArq2);
                                        }
                                    }
                                }
                            }
                            if (!efeitoEncontradoNoArq2)
                            {
                                throw new Exception("Efeito não encontrado no outro arquivo.");
                            }
                        }
                        if(linhaArq1.ListaEfeitos.Count == 0)
                        {
                            throw new Exception("Existem linhas sem efeitos cadastrados.");
                        }
                    }
                    MessageBox.Show("Informações conferem.");



                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else if (listaLinhasArq1.Count == 0 || listaLinhasArq2.Count == 0)
            {
                MessageBox.Show("É necessário selecionar um arquivo com informações.");
            }
            else
            {
                MessageBox.Show("Arquivos não possuem o mesmo tamanho de linhas.");
            }
        }

        private static void ComparaCamposEfeito(Efeito dadosEfeito, Efeito dadosEfeitoArq2)
        {
            for (int percorreCamposEfeito = 0; percorreCamposEfeito < dadosEfeito.efeito.Length; percorreCamposEfeito++)
            {
                if (percorreCamposEfeito == 6)
                {//Verificação de valores
                    dadosEfeito.efeito[percorreCamposEfeito] = dadosEfeito.efeito[percorreCamposEfeito].Replace('.', ',');
                    dadosEfeitoArq2.efeito[percorreCamposEfeito] = dadosEfeitoArq2.efeito[percorreCamposEfeito].Replace('.', ',');

                    double valorConvertidoArq1 = double.Parse(dadosEfeito.efeito[percorreCamposEfeito]);
                    double valorConvertidoArq2 = double.Parse(dadosEfeitoArq2.efeito[percorreCamposEfeito]);

                    if (valorConvertidoArq1 != valorConvertidoArq2)
                    {
                        throw new Exception("Valores dentro do efeito não conferem.");
                    }
                }
                else if (!dadosEfeito.efeito[percorreCamposEfeito].Equals(dadosEfeitoArq2.efeito[percorreCamposEfeito]))
                {
                    throw new Exception("Diferença encontrada na comparação dos campos do efeito.");
                }
            }
        }

        private static void configuraLinhaEfeitos(ref string[] efeitos, char delimiterChar, List<Linha> listaLinhasArq1, string dados)
        {
            string[] linhas = dados.Split(delimiterChar);
            string infoLinha = string.Empty;
            foreach (string linha in linhas)
            {
                if (!string.IsNullOrEmpty(linha))
                {
                    Linha linhaAtual = new Linha();
                    string texto = PegarConteudoForaAspas(linha, 0);
                    linhaAtual.texto = texto.Split(';');
                    string efeitoStr = PegarConteudoEntreAspas(linha, 0);
                    if (!string.IsNullOrEmpty(efeitoStr))
                    {
                        efeitos = efeitoStr.Split('|');
                        foreach (string efeito in efeitos)
                        {
                            Efeito efeitoLinha = new Efeito();
                            string[] efeitoConfigurado = efeito.Split(';');
                            efeitoLinha.efeito = efeitoConfigurado;
                            linhaAtual.ListaEfeitos.Add(efeitoLinha);
                        }
                    }
                    listaLinhasArq1.Add(linhaAtual);
                }
            }
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
