using DoubleCheck.Classes;
using System;
using System.Collections.Generic;
using System.IO;
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
            string[] efeitos = new string[999];
            string[] linhas = new string[9999];
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

            //TODO: Comparar listaLinhasArq1 e listaLinhasArq2
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
                    string[] configTexto = texto.Split(';');
                    linhaAtual.texto.Add(configTexto);

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
