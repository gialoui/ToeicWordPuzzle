using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace TOEICWordPuzzle
{
    static class Program
    {
        public static SQLiteConnection conn = new SQLiteConnection("Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\twp.db");
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainScreen());
        }

        static Random _random = new Random();
        public static char getRandLetter()
        {
            // This method returns a random uppercase letter.
            // ... Between 'A' and 'Z' inclusize.
            int num = _random.Next(0, 26); // Zero to 25
            char let = (char)('A' + num);
            return let;
        }
    }
}
