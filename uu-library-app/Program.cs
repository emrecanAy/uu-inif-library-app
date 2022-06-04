using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using uu_library_app.FormUI;
using uu_library_app.FormUI.Other_Operations;
using uu_library_app.FormUI.Register_Login;
using uu_library_app.FormUI.Deposit;
using uu_library_app.FormUI.Book;

namespace uu_library_app
{
   public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NewLogin());
        }
    }
}
