using System;
using System.Text;
using System.Windows.Forms;

namespace MSINFOR
{
    public partial class Form_Printers : Form
    {
        public Form_Printers()
        {
            InitializeComponent();
        }

        private void Form_Printers_Shown(object sender, EventArgs e)
        {
            if (Program.Parametros.Count == 2)
            {
                comboBox1.Items.Clear();

                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    comboBox1.Items.Add(printer);
                }

                if (comboBox1.Items.Count > 0)
                {
                    foreach (string OnePrinter in comboBox1.Items)
                    {
                        if (OnePrinter.Length > 9)
                        {
                            if (OnePrinter.Substring(0, 9).ToUpper() == "ZDESIGNER")
                            {
                                comboBox1.SelectedItem = OnePrinter;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Nenhuma impressora foi detectada !");
                }
            }
            else
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ArquivoTXT = @Program.Parametros[1];

            try
            {

                string printerName = comboBox1.Items[comboBox1.SelectedIndex].ToString();


                if (printerName == null)
                {
                    throw new ArgumentNullException("printerName");
                }

                StringBuilder sb   =  new StringBuilder();

                System.IO.StreamReader file = new System.IO.StreamReader(ArquivoTXT);
                int counter = 0;
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    //System.Windows.Forms.MessageBox.Show(line);
                    sb.AppendLine(line);
                    counter++;
                }


                RawPrinterHelper.SendStringToPrinter(printerName, sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {

            }



            this.Close();
        }


    }
}