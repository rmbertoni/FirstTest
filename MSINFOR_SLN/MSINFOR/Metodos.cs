using System;
using System.Runtime.InteropServices;

namespace MSINFOR
{
    internal class Metodos
    {
        [DllImport("c:\\MSINFOR\\companytec.dll")]
        public static extern bool InicializaSerial(byte Porta);

        [DllImport("c:\\MSINFOR\\companytec.dll")]
        public static extern int FechaSerial();

        [DllImport("c:\\MSINFOR\\companytec.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void LimpaSerial();

        [DllImport("c:\\MSINFOR\\companytec.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void LeStructStFix(IntPtr ab);

        [DllImport("c:\\MSINFOR\\companytec.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void Incrementa();

        [DllImport("c:\\MSINFOR\\companytec.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int C_ReadTotalsVolume(string bico);

        [DllImport("c:\\MSINFOR\\companytec.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int CS_SendReceiveText(ref string Comando);

        [DllImport("c:\\MSINFOR\\companytec.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int C_ReadTotalsVolume2(string bico);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct abast2
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string value;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string total_dinheiro;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        public string total_litros;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string PU;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string tempo;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string canal;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string data;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
        public string hora;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 56)]
        public string st_full;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
        public string registro;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string encerrante;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string integridade;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string checksum;
    }
}