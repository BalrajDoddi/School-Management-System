using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace School_Management_System
{
    static class Program
    {
        public static Login lgForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          //  Application.Run(new Master());
            
          lgForm = new Login();
          Application.Run(lgForm);
        }
    }
}
