using Autodesk.Navisworks.Api.Resolver;
using System;
using System.Windows.Forms;


namespace CadListExtractor
{

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Ensure the Navisworks runtime is available
            String runtimeName = Resolver.TryBindToRuntime(RuntimeNames.Any);
            if (String.IsNullOrEmpty(runtimeName))
            {
                throw new Exception("Failed to bind to Navisworks runtime");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

        }
    }
}
