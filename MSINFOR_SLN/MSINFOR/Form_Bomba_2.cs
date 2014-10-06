using System;
using System.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace MSINFOR
{
    partial class Form_Bomba
    {
        private bool OK;
        private readonly string Path = @"\\servidor\c\msinfor\";
        //private readonly string Path = @"C:\MSINFOR\"; --- PARA TESTES !!!      

        private void AbrePorta()
        {
            int NumeroDaPorta = 7;

            try
            {
                bool Retorno = Metodos.InicializaSerial(System.Convert.ToByte(NumeroDaPorta));

                if (Retorno)
                {
                    OK = true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Não conseguiu abrir a porta COM" + NumeroDaPorta.ToString().Trim() + " !");
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

        private void FechaPorta()
        {
            try
            {
                int Ret = Metodos.FechaSerial();

                if (Ret == 0)
                {
                    progressBar1.Value = 90;
                }
                else
                {
                    OK = false;
                }
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);

                OK = false;
            }
            finally
            {

            }
        }

        private void Status()
        {
            progressBar1.Value = 10;

            AbrePorta();

            if (OK)
            {
                try
                {
                    Metodos.LimpaSerial();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("LimpaSerial => " + ex.Message);

                    OK = false;
                }
                finally
                {
                }

                progressBar1.Value = 20;
            }

            if (OK)
            {
                OleDbConnection DBF_Connection = new OleDbConnection();
                OleDbCommand DBF_Command = new OleDbCommand();

                DBF_Connection.ConnectionString = @"Provider=vfpoledb;Data Source=" + Path.Trim() + "POBOM.dbf;Collating Sequence=machine";

                DBF_Connection.Open();

                DBF_Command.Connection = DBF_Connection;

                DBF_Command.Parameters.Clear();
                DBF_Command.Parameters.Add("@POEmpCod", OleDbType.SmallInt).Value = 1;

                DBF_Command.CommandText = @"SELECT * FROM " + Path.Trim() + "POBOM.DBF WHERE POEmpCod = ?";

                DataTable dt = new DataTable();

                dt.Load(DBF_Command.ExecuteReader());

                progressBar1.Value = 30;

                if (dt.Rows.Count > 0)
                {
                    for (int idx = 0; idx < dt.Rows.Count; idx++)
                    {
                        if (progressBar1.Value <= 90)
                        {
                            progressBar1.Value += 10;
                        }

                        int POBomCod = System.Convert.ToInt32(dt.Rows[idx]["POBomCod"]);
                        string POBomCanal = System.Convert.ToString(dt.Rows[idx]["POBomCanal"]);

                        if (POBomCanal.Trim().Length > 0)
                        {
                            try
                            {
                                int Volume = Metodos.C_ReadTotalsVolume(POBomCanal);

                                if (Volume > 0)
                                {
                                    //System.Windows.Forms.MessageBox.Show("Comunicação normal com bico/canal => " + POBomCanal);
                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show("Problema com a porta serial. Não está comunicando com o concentrador !");

                                    break;
                                }
                            }
                            catch (System.Exception ex)
                            {
                                System.Windows.Forms.MessageBox.Show("C_ReadTotalsVolume => " + ex.Message);

                                OK = false;
                            }
                            finally
                            {
                            }
                        }
                    }
                }

                DBF_Connection.Close();

                progressBar1.Value = 90;

                if (OK)
                {
                    FechaPorta();
                }

                progressBar1.Value = 100;
            }
        }





        private void Zerar()
        {
            progressBar1.Value = 10;

            AbrePorta();

            progressBar1.Value = 20;

            if (OK)
            {
                //string Comando = "(?F00L0000000000000000000000010000000032)";
                //int Ret = Metodos.CS_SendReceiveText(ref Comando);
                //System.Windows.Forms.MessageBox.Show("RET => " + Ret.ToString());
                //System.Windows.Forms.MessageBox.Show("Comando => " + Comando);

                string Comando = "(&S)";
                int Ret = Metodos.CS_SendReceiveText(ref Comando);

                System.Windows.Forms.MessageBox.Show("RET => " + Ret.ToString());
                System.Windows.Forms.MessageBox.Show("Comando => " + Comando);


                /*
                Comando = "(?F)";
                Ret = Metodos.CS_SendReceiveText(ref Comando);

                System.Windows.Forms.MessageBox.Show("RET => " + Ret.ToString());
                System.Windows.Forms.MessageBox.Show("Comando => " + Comando);

                Comando = "(?F00L0000000000000000000026400000000000)";
                Ret = Metodos.CS_SendReceiveText(ref Comando);

                System.Windows.Forms.MessageBox.Show("RET => " + Ret.ToString());
                System.Windows.Forms.MessageBox.Show("Comando => " + Comando);
                */
            }

            FechaPorta();
        }

        private void Encerrantes()
        {
            progressBar1.Value = 10;

            AbrePorta();

            if (OK)
            {
                try
                {
                    Metodos.LimpaSerial();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("LimpaSerial => " + ex.Message);

                    OK = false;
                }
                finally
                {
                }

                progressBar1.Value = 20;
            }

            if (OK)
            {
                OleDbConnection DBF_Connection = new OleDbConnection();
                OleDbCommand DBF_Command = new OleDbCommand();

                DBF_Connection.ConnectionString = @"Provider=vfpoledb;Data Source=" + Path.Trim() + "POBOM.dbf;Collating Sequence=machine";

                DBF_Connection.Open();

                DBF_Command.Connection = DBF_Connection;

                DBF_Command.Parameters.Clear();
                DBF_Command.Parameters.Add("@POEmpCod", OleDbType.SmallInt).Value = 1;

                DBF_Command.CommandText = @"SELECT * FROM " + Path.Trim() + "POBOM.DBF WHERE POEmpCod = ?";

                DataTable dt = new DataTable();

                dt.Load(DBF_Command.ExecuteReader());

                progressBar1.Value = 30;

                if (dt.Rows.Count > 0)
                {
                    for (int idx = 0; idx < dt.Rows.Count; idx++)
                    {
                        if (progressBar1.Value <= 90)
                        {
                            progressBar1.Value += 10;
                        }

                        int POBomCod = System.Convert.ToInt32(dt.Rows[idx]["POBomCod"]);
                        string POBomCanal = System.Convert.ToString(dt.Rows[idx]["POBomCanal"]);
                        decimal POBomEncer = 0;

                        if (POBomCanal.Trim().Length > 0)
                        {
                            try
                            {
                                int Volume = Metodos.C_ReadTotalsVolume(POBomCanal);

                                if (Volume > 0)
                                {
                                    POBomEncer = System.Convert.ToDecimal(Volume) / 100;
                                }
                                else
                                {
                                    POBomEncer = 0;
                                }
                            }
                            catch (System.Exception ex)
                            {
                                System.Windows.Forms.MessageBox.Show("C_ReadTotalsVolume2 => " + ex.Message);

                                OK = false;
                            }
                            finally
                            {
                            }
                        }

                        if (OK)
                        {
                            System.Threading.Thread.Sleep(1000);

                            if (POBomEncer > 0)
                            {
                                try
                                {
                                    DBF_Command.Parameters.Clear();

                                    DBF_Command.Parameters.Add("@POBomEncer", OleDbType.Decimal).Value = POBomEncer;
                                    DBF_Command.Parameters.Add("@POBomAtuEn", OleDbType.Char).Value = "S";
                                    DBF_Command.Parameters.Add("@POEmpCod", OleDbType.SmallInt).Value = 1;
                                    DBF_Command.Parameters.Add("@POBomCod", OleDbType.SmallInt).Value = POBomCod;

                                    DBF_Command.CommandText = @"UPDATE " + Path.Trim() + "POBOM SET POBomEncer = ?, POBomAtuEn = ? WHERE POEmpCod = ? AND POBomCod = ?";

                                    int RowsUpdated = DBF_Command.ExecuteNonQuery();

                                    if (RowsUpdated != 1)
                                    {
                                        System.Windows.Forms.MessageBox.Show("Erro: atualizou " + RowsUpdated.ToString().Trim() + " registros !");
                                    }
                                }
                                catch (System.Exception ex)
                                {
                                    System.Windows.Forms.MessageBox.Show("Erro Gravando Encerrante => " + ex.Message);

                                    OK = false;
                                }
                                finally
                                {

                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                DBF_Connection.Close();

                progressBar1.Value = 90;

                if (OK)
                {
                    FechaPorta();
                }

                progressBar1.Value = 100;
            }
        }

        private void Abastecimentos()
        {
            progressBar1.Value = 10;

            OK = false;

            AbrePorta();

            if (OK)
            {
                progressBar1.Value = 20;

                try
                {
                    Metodos.LimpaSerial();
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("LimpaSerial => " + ex.Message);

                    OK = false;
                }
                finally
                {
                }
            }

            if (OK)
            {
                progressBar1.Value = 30;

                bool Continuar = true;

                while (Continuar)
                {
                    try
                    {
                        var Abast2 = default(abast2);
                        var Pointer = Marshal.AllocHGlobal(Marshal.SizeOf(Abast2));

                        Marshal.StructureToPtr(Abast2, Pointer, false);
                        Metodos.LeStructStFix(Pointer);

                        Abast2 = (abast2)Marshal.PtrToStructure(Pointer, typeof(abast2));

                        Marshal.FreeHGlobal(Pointer);

                        System.Threading.Thread.Sleep(1000);

                        if (Abast2.data.Trim() == "00/00/0000")
                        {
                            Continuar = false;
                        }
                        else
                        {
                            if (Grava(Abast2))
                            {
                                try
                                {
                                    Metodos.Incrementa();
                                }
                                catch (System.Exception ex)
                                {
                                    System.Windows.Forms.MessageBox.Show("Incrementa => " + ex.Message);

                                    OK = false;
                                }
                                finally
                                {

                                }
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Não conseguiu gravar o registro de abastecimento !");
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("LeStructStFix => " + ex.Message);

                        OK = false;
                    }
                    finally
                    {

                    }

                    if (OK)
                    {
                        if (progressBar1.Value < 70)
                        {
                            progressBar1.Value += 2;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                progressBar1.Value = 70;
            }

            if (OK)
            {
                progressBar1.Value = 80;

                FechaPorta();
            }

            progressBar1.Value = 100;
        }

        private bool Grava(abast2 ab)
        {
            DateTime PK_DataHora = DateTime.Now;
            decimal ab_Valor = 0;
            decimal ab_Litros = 0;
            string ab_Canal = string.Empty;
            DateTime ab_Data = DateTime.Now;
            string ab_Hora = string.Empty;
            decimal ab_Registro = 0;
            decimal ab_Encerrante = 0;
            decimal ab_PU = 0;
            DateTime DataNula = new DateTime(1899, 12, 30);

            bool OK = true;

            try
            {
                ab_Valor = System.Convert.ToDecimal(ab.total_dinheiro.Trim()) / 100;
                ab_Litros = System.Convert.ToDecimal(ab.total_litros.Trim()) / 100;

                ab_Canal = ab.canal.Trim();
                ab_Data = System.Convert.ToDateTime(ab.data.Trim());
                ab_Hora = ab.hora.Trim();
                ab_Registro = System.Convert.ToDecimal(ab.registro.Trim());
                ab_Encerrante = System.Convert.ToDecimal(ab.encerrante.Trim()) / 100;
                ab_PU = System.Convert.ToDecimal(ab.PU.Trim()) / 1000;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);

                System.Windows.Forms.MessageBox.Show("Data => " + ab.data);

                OK = false;
            }
            finally
            {

            }

            if (OK)
            {
                OleDbConnection DBF_Connection = new OleDbConnection();
                OleDbCommand DBF_Command = new OleDbCommand();

                DBF_Connection.ConnectionString = @"Provider=vfpoledb;Data Source=" + Path.Trim() + "POCF1.dbf;Collating Sequence=machine";

                DBF_Connection.Open();

                DBF_Command.Connection = DBF_Connection;

                if (OK)
                {
                    bool Continuar = true;
                    int Tentativas = 0;

                    while (Continuar)
                    {
                        Tentativas += 1;

                        if (Tentativas > 10)
                        {
                            System.Windows.Forms.MessageBox.Show("REGISTRO JÁ EXISTE - 10 tentativas!!!");

                            Continuar = false;
                            OK = false;

                            break;
                        }
                        else
                        {
                            PK_DataHora = DateTime.Now;

                            DBF_Command.Parameters.Clear();

                            DBF_Command.Parameters.Clear();
                            DBF_Command.Parameters.Add("@POEmpCod", OleDbType.SmallInt).Value = 1;
                            DBF_Command.Parameters.Add("@POCF1Tst", OleDbType.DBTimeStamp).Value = PK_DataHora;

                            DBF_Command.CommandText = @"SELECT * FROM " + Path.Trim() + "pocf1.DBF WHERE POEmpCod = ? and POCF1Tst = ?";

                            DataTable dt = new DataTable();

                            dt.Load(DBF_Command.ExecuteReader());

                            if (dt.Rows.Count > 0)
                            {
                                System.Threading.Thread.Sleep(2000);
                            }
                            else
                            {
                                Continuar = false;
                            }
                        }
                    }
                }

                if (OK)
                {
                    try
                    {
                        DBF_Command.Parameters.Clear();

                        DBF_Command.Parameters.Add("@POEmpCod", OleDbType.SmallInt).Value = 1;
                        DBF_Command.Parameters.Add("@POCF1Tst", OleDbType.DBTimeStamp).Value = PK_DataHora;
                        DBF_Command.Parameters.Add("@POCF1VlrCx", OleDbType.Decimal).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1Usu", OleDbType.Char).Value = string.Empty;
                        DBF_Command.Parameters.Add("@POCF1VlrMo", OleDbType.Decimal).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1VlrDs", OleDbType.Decimal).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1VlrAc", OleDbType.Decimal).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1VlrSa", OleDbType.Decimal).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1TipMo", OleDbType.Char).Value = "B";
                        DBF_Command.Parameters.Add("@POCF1Sts", OleDbType.Char).Value = "A";
                        DBF_Command.Parameters.Add("@POCF1Obs", OleDbType.Char).Value = string.Empty;
                        DBF_Command.Parameters.Add("@POCF1DtaMo", OleDbType.DBDate).Value = DataNula;
                        DBF_Command.Parameters.Add("@POCF1SeqMo", OleDbType.Integer).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1Prg", OleDbType.Char).Value = string.Empty;
                        DBF_Command.Parameters.Add("@POCF1Bic", OleDbType.SmallInt).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1BomCo", OleDbType.SmallInt).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1ProCo", OleDbType.Decimal).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1Qtd", OleDbType.Decimal).Value = ab_Litros;
                        DBF_Command.Parameters.Add("@POCF1VelIn", OleDbType.Decimal).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1VelFi", OleDbType.Decimal).Value = ab_Encerrante;
                        DBF_Command.Parameters.Add("@POCF1VlrUn", OleDbType.Decimal).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1TstFe", OleDbType.DBTimeStamp).Value = DataNula;
                        DBF_Command.Parameters.Add("@POCF1FilCo", OleDbType.Decimal).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1VlrBo", OleDbType.Decimal).Value = ab_Valor;
                        DBF_Command.Parameters.Add("@POCF1Idx", OleDbType.LongVarChar).Value = string.Empty;
                        DBF_Command.Parameters.Add("@POCF1TstCo", OleDbType.DBTimeStamp).Value = DataNula;
                        DBF_Command.Parameters.Add("@POCF1Can", OleDbType.Char).Value = string.Empty;
                        DBF_Command.Parameters.Add("@POCF1TstCa", OleDbType.DBTimeStamp).Value = DataNula;
                        DBF_Command.Parameters.Add("@POCF1UsuCa", OleDbType.Char).Value = string.Empty;
                        DBF_Command.Parameters.Add("@POCF1Canal", OleDbType.Char).Value = ab_Canal;
                        DBF_Command.Parameters.Add("@POCF1Regis", OleDbType.Decimal).Value = ab_Registro;
                        DBF_Command.Parameters.Add("@POCF1DataA", OleDbType.DBDate).Value = ab_Data;
                        DBF_Command.Parameters.Add("@POCF1HoraA", OleDbType.Char).Value = ab_Hora;
                        DBF_Command.Parameters.Add("@POCF1PUBom", OleDbType.Decimal).Value = ab_PU;
                        DBF_Command.Parameters.Add("@POCF1TipOr", OleDbType.Char).Value = string.Empty;
                        DBF_Command.Parameters.Add("@POCF1ProDe", OleDbType.Char).Value = string.Empty;
                        DBF_Command.Parameters.Add("@POCF1CusUn", OleDbType.Decimal).Value = 0;
                        DBF_Command.Parameters.Add("@POCF1DCx", OleDbType.DBTimeStamp).Value = DataNula;

                        DBF_Command.CommandText = "INSERT INTO POCF1 (";

                        for (int idx = 0; idx < DBF_Command.Parameters.Count; idx++)
                        {
                            if (idx > 0)
                            {
                                DBF_Command.CommandText += ", ";
                            }

                            DBF_Command.CommandText += DBF_Command.Parameters[idx].ParameterName.Trim().Substring(1, DBF_Command.Parameters[idx].ParameterName.Length - 1);
                        }


                        DBF_Command.CommandText += ") VALUES (";

                        for (int idx = 0; idx < DBF_Command.Parameters.Count; idx++)
                        {
                            if (idx > 0)
                            {
                                DBF_Command.CommandText += ", ";
                            }

                            DBF_Command.CommandText += "?";
                        }

                        DBF_Command.CommandText += ")";

                        int RowsInserted = DBF_Command.ExecuteNonQuery();

                        if (RowsInserted != 1)
                        {
                            System.Windows.Forms.MessageBox.Show("Erro: inseriu " + RowsInserted.ToString().Trim() + " registros !");
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

                DBF_Connection.Close();
            }

            return OK;
        }

        private void Altera_Preco_Bomba()
        {
            progressBar1.Value = 10;

            AbrePorta();

            progressBar1.Value = 20;

            if (OK)
            {
                string POBomCanal = string.Empty;
                string Preco = string.Empty;

                if (Program.Parametros.Count == 4)
                {
                    POBomCanal = Program.Parametros[2].Trim();
                    Preco = Program.Parametros[3].Trim();
                }

                if (POBomCanal != string.Empty)
                {
                    try
                    {
                        progressBar1.Value = 50;

                        string Command = "&U" + POBomCanal.Trim() + "00" + Preco;
                        byte[] CommandBytes = System.Text.Encoding.ASCII.GetBytes(Command);

                        int SumBytes = 0;

                        for (int i = 0; i <= 9; i++)
                        {
                            SumBytes += CommandBytes[i];
                        }

                        string SumHexa = SumBytes.ToString("X");
                        string CheckSum = string.Empty;

                        if (SumHexa.Length == 1) { CheckSum = SumHexa.Substring(0, 1); }
                        if (SumHexa.Length == 2) { CheckSum = SumHexa.Substring(0, 2); }
                        if (SumHexa.Length == 3) { CheckSum = SumHexa.Substring(1, 2); }

                        string Command2 = "(" + Command.Trim() + CheckSum.Trim() + ")";

                        int ret2 = Metodos.CS_SendReceiveText(ref Command2);

                        progressBar1.Value = 90;
                    }
                    catch (System.Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("CS_SendReceiveText => " + ex.Message);

                        OK = false;
                    }
                    finally
                    {
                    }
                }

                FechaPorta();

                progressBar1.Value = 100;
            }
        }
    }
}