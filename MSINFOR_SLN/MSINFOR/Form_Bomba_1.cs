using System;
using System.Windows.Forms;

namespace MSINFOR
{
    public partial class Form_Bomba : Form
    {
        public Form_Bomba()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            progressBar1.Value = 0;
            label1.Refresh();
            progressBar1.Refresh();
            System.Threading.Thread.Sleep(2000);

            if (Program.Parametros.Count > 0)
            {
                if (Program.Parametros[0] == "BOMBA")
                {
                    if (Program.Parametros.Count >= 2)
                    {
                        if (Program.Parametros[1] == "ABASTECIMENTOS")
                        {
                            Abastecimentos();
                        }
                        else
                        {
                            if (Program.Parametros[1] == "ENCERRANTES")
                            {
                                Encerrantes();
                            }
                            else
                            {
                                if (Program.Parametros[1] == "PRECO")
                                {
                                    Altera_Preco_Bomba();
                                }
                                else
                                {
                                    if (Program.Parametros[1] == "STATUS")
                                    {
                                        Status();
                                    }
                                    else
                                    {
                                        if (Program.Parametros[1] == "ZERAR")
                                        {
                                            Zerar();
                                        }
                                        else
                                        {
                                            System.Windows.Forms.MessageBox.Show("Parâmetro para operação com BOMBAS inválido => " + Program.Parametros[1]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Parâmetros inválidos !");
                    }
                }
            }

            this.Close();
        }
    }
}