using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using CheckersClient.GameGraphics;
using Domain.Models;

namespace CheckersClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            Console.WriteLine(hostName);
            // Get the IP
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            Console.WriteLine("My IP Address is :"+myIP);
            Console.ReadKey();

            
            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new GameForm());
        }
    }
}