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


            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                string dados = File.ReadAllText(fileName);
                configuraLinhaEfeitos(ref efeitos, delimiterChar, listaLinhasArq1, dados);
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName;
                fileName = dlg.FileName;
                string dados = File.ReadAllText(fileName);
                configuraLinhaEfeitos(ref efeitos, delimiterChar, listaLinhasArq2, dados);
            }

            if (listaLinhasArq1.Count == listaLinhasArq2.Count && listaLinhasArq1.Count > 0)
            {
                try
                {
                    FormataData(listaLinhasArq1);
                    FormataData(listaLinhasArq2);

                    for (int i = 0; i < listaLinhasArq1.Count; i++)
                    {
                        var linhaEncontrada = listaLinhasArq1.Find(a => listaLinhasArq2.Any(x => x.texto == a.texto));
                        if (linhaEncontrada != null)
                        {
                            var linhaArq1 = listaLinhasArq1.Find(a => a.texto.Equals(linhaEncontrada.texto));
                            var linhaArq2 = listaLinhasArq2.Find(a => a.texto.Equals(linhaEncontrada.texto));

                            ComparaEfeitos(linhaArq1, linhaArq1, i);
                        }
                        else
                        {
                            throw new Exception("Linha não encontrada no outro arquivo. Linha: " + i+1);
                        }
                    }
                    MessageBox.Show("Informações conferem.");

                    
                    
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
            else
            {
                MessageBox.Show("Arquivos não possuem o mesmo tamanho de linhas.");
            }
        }

        private static void FormataData(List<Linha> listaLinhasArq)
        {
            foreach (Linha linha in listaLinhasArq)
            {
                linha.texto = linha.texto.Remove(113);
            }
        }

        private static void ComparaEfeitos(Linha LinhasArq1, Linha LinhasArq2, int i)
        {
            if (LinhasArq1.ListaEfeitos[0].efeitos.Count == LinhasArq2.ListaEfeitos[0].efeitos.Count)
            {
                for (int percorreEfeito = 0; percorreEfeito < LinhasArq1.ListaEfeitos[0].efeitos.Count; percorreEfeito++)
                {
                    for (int j = 0; j < LinhasArq1.ListaEfeitos[0].efeitos[0].Length; j++)
                    {
                        if (LinhasArq1.ListaEfeitos[0].efeitos[0][j] != LinhasArq2.ListaEfeitos[0].efeitos[0][j])
                        {
                            throw new Exception("Informações de efeitos não conferem. Linha: " + i+1);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Quantidade de efeitos não confere. Linha: " + i);
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
                    linhaAtual.texto = texto;

                    Efeitos efeitoLinha = new Efeitos();
                    string efeitoStr = PegarConteudoEntreAspas(linha, 0);
                    efeitos = efeitoStr.Split('|');
                    foreach (string efeito in efeitos)
                    {
                        string[] efeitoConfigurado = efeito.Split(';');
                        efeitoLinha.efeitos.Add(efeitoConfigurado);
                    }
                    linhaAtual.ListaEfeitos.Add(efeitoLinha);
                    listaLinhasArq1.Add(linhaAtual);
                }
            }
        }

        static string PegarConteudoEntreAspas(string texto, int linha)
        {
            int i, j;
            string strLinha;
            EncontraIndice(texto, linha, out i, out j, out strLinha);

            return strLinha.Substring(i, j - i);
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
            int i, j;
            string strLinha;
            EncontraIndice(texto, linha, out i, out j, out strLinha);

            return strLinha.Remove(i, j - i);
        }
    }
}
