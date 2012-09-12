using System;
using System.Windows.Forms;
using TfsCommitMonitor.Injection;
using Microsoft.Practices.Unity;

namespace TfsCommitMonitor
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var program = IOC.Container.Resolve<MainForm>();
            Application.Run(program);
        }
    }
}
