﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TrackerLibrary.GlobalConfig.InitializeConnection(TrackerLibrary.DataAccess.DatabaseType.txt);

            Application.Run(new TournamentDashboardForm());
        }
    }
}
