using System;
using System.Net;
using System.Windows.Forms;
using CheckersClient.Forms;
using Domain.Models;

namespace CheckersClient
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new IndexForm());
        }
    }
}