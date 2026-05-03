using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    // Program.cs — Punto de entrada
    
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
