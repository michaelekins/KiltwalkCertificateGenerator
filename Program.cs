using System;
using System.Windows.Forms;

namespace KiltwalkCertificateGenerator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }

            else
            {
                var certificateGenerator = new CertificateGenerator();

                certificateGenerator.Execute(args[0], args[1]);
            }
        }
    }
}
