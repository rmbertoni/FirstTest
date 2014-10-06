using System.IO;
using System.IO.Ports;
using System.Text;

namespace MSINFOR
{
    public class Balanca
    {
        public Balanca()
        {
            if (Program.Parametros.Count == 5)
            {
                if (Program.Parametros[1] == "MAGNA")
                {
                    Magna();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Modelo de balança inválido !");
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Parâmetros inválidos para balança !");
            }
        }

        private void Magna()
        {
            string NomeArquivoTXT = @Program.Parametros[2].Trim();    //  @"C:\Sistemas\SIL300090\DATA004\PESO.TXT"   VIRÁ DO PARÂMETRO  !!!!

            SerialPort Porta = new SerialPort();

            try
            {
                Porta.PortName = Program.Parametros[3].Trim(); // COM1
                Porta.BaudRate = System.Convert.ToInt32(Program.Parametros[4].Trim());  // 9600
                Porta.DataBits = 8;
                Porta.Parity = Parity.None;
                Porta.StopBits = StopBits.One;

                Porta.Open();

                if (Porta.IsOpen)
                {
                    byte[] ENQ = new byte[1];

                    ENQ[0] = 0x05;

                    Porta.Write(ENQ, 0, 1);

                    System.Threading.Thread.Sleep(500);

                    if (Porta.BytesToRead > 0)
                    {
                        byte[] Buffer = new byte[Porta.BytesToRead];

                        int BufferSize = Porta.Read(Buffer, 0, Porta.BytesToRead);

                        if (BufferSize > 0)
                        {
                            string Retorno = Encoding.UTF8.GetString(Buffer, 0, Buffer.Length);

                            StreamWriter ArquivoTXT = new StreamWriter(NomeArquivoTXT, false);

                            ArquivoTXT.WriteLine(Retorno);

                            ArquivoTXT.Close();
                        }
                    }

                    Porta.Close();
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
    }
}