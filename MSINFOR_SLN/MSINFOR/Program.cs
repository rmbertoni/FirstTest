using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MSINFOR
{
    static class Program
    {
        public static List<string> Parametros;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Parametros = new List<string>();

            foreach (string Param in args)
            {
                Parametros.Add(Param);
            }

            if (Program.Parametros.Count > 0)
            {
                bool ParamOK = false;

                if (Program.Parametros[0] == "BACKUP")
                {
                    Application.Run(new Form_Backup());
                    ParamOK = true;
                }

                if (Program.Parametros[0] == "BOMBA")
                {
                    Application.Run(new Form_Bomba());
                    ParamOK = true;
                }

                if (Program.Parametros[0] == "BALANCA")
                {
                    Balanca balanca = new Balanca();
                    ParamOK = true;
                }

                if (Program.Parametros[0] == "ZEBRA")
                {
                    Application.Run(new Form_Printers());
                    ParamOK = true;
                }

                if (!ParamOK)
                {
                    System.Windows.Forms.MessageBox.Show("Parâmetros para a chamada são inválidos !");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Parâmetros para a chamada não foram informados !");
            }
        }
    }
}