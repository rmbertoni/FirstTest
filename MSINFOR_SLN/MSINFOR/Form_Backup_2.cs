using System;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace MSINFOR
{
  partial class Form_Backup
    {
        private Backup BackupSQLServer;

        private string Instance;
        private string Catalog;
        private string User;
        private string Pwd;
        private string FileName;

      

        internal void Execute()
        {
            if (Program.Parametros.Count == 6)
            {
                Instance = @Program.Parametros[1];
                Catalog = @Program.Parametros[2];
                User = @Program.Parametros[3];
                Pwd = @Program.Parametros[4];
                FileName = @Program.Parametros[5];
            }

            /*
            if (Params.Count == 5)
            {
                SQLServer DB = new SQLServer();

                DB.Instance = @Params[0];
                DB.Catalog = @Params[1];
                DB.User = @Params[2];
                DB.Pwd = @Params[3];
                DB.FileName = @Params[4];

                Console.Clear();
                Console.WriteLine("Backup do Banco de Dados - " + DB.Catalog);
                Console.WriteLine("");

                DB.Execute();

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Fim do Backup !");

                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                Console.WriteLine("Parâmetros inválidos !");
            }
            */











            ServerConnection SC = new ServerConnection(@Instance, User, Pwd);

            Server SRV = new Server(SC);

            BackupSQLServer = new Backup();

            BackupSQLServer.Action = BackupActionType.Database;
            BackupSQLServer.Database = Catalog;
            BackupSQLServer.Incremental = false;

            BackupSQLServer.Devices.AddDevice(@FileName, DeviceType.File);

            BackupSQLServer.PercentCompleteNotification = 1;
            BackupSQLServer.PercentComplete += new PercentCompleteEventHandler(Processing);

            BackupSQLServer.SqlBackup(SRV);
        }

        private void Processing(object sender, PercentCompleteEventArgs e)
        {
            progressBar1.Value = e.Percent;
            //Console.CursorLeft = 0;
            //Console.Write("Processando " + e.Percent.ToString() + " %");
        }
    }
}