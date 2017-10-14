using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOEICWordPuzzle
{
    class ThemeDAO : IThemeDAO
    {
        public ObservableCollection<Theme> getAll()
        {
            ObservableCollection<Theme> themes = new ObservableCollection<Theme>();
            try
            {
                Program.conn.Open();    // Open new connection
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM theme", Program.conn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    themes.Add(new Theme(reader.GetInt32(0), reader.GetString(1)));
                }

                return themes;

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                return null;
            }
            finally
            {
                Program.conn.Close();   // Close connection
            }
        }

        public Theme findById(int id)
        {
            try
            {
                if (Program.conn.State == System.Data.ConnectionState.Closed)
                {
                    Program.conn.Open();    // Open new connection
                }
                
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM theme WHERE id = " + id, Program.conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                Theme theme = new Theme();

                while (reader.Read())
                {
                    theme = new Theme(reader.GetInt32(0), reader.GetString(1));
                }

                return theme;

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                return null;
            }
        }
    }
}
