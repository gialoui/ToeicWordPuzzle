using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace TOEICWordPuzzle
{
    class MeaningDAO : IMeaningDAO
    {

        public ObservableCollection<Meaning> getAll()
        {
            IWordDAO wordDao = new WordDAO();
            ObservableCollection<Meaning> meanings = new ObservableCollection<Meaning>();
            try
            {
                Program.conn.Open();    // Open new connection
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM meaning", Program.conn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    meanings.Add(new Meaning(reader.GetInt32(0), reader.GetString(2), wordDao.findById(reader.GetInt32(1))));
                }

                return meanings;

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

        public Meaning findById(int id)
        {
            try
            {
                Program.conn.Open();    // Open new connection
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM meaning WHERE id = " + id, Program.conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                Meaning meaning = new Meaning();
                IWordDAO wordDao = new WordDAO();

                while (reader.Read())
                {

                    meaning = new Meaning(reader.GetInt32(0), reader.GetString(2), wordDao.findById(reader.GetInt32(1)));
                }

                return meaning;
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

        public StringBuilder getMeaningByID(int id)
        {
            IWordDAO wordDao = new WordDAO();
            StringBuilder strb = new StringBuilder();
            try
            {
                Program.conn.Open();    // Open new connection
                SQLiteCommand cmd = new SQLiteCommand("select * from meaning where word = " + id, Program.conn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    strb.Append("- ").Append(reader.GetString(2) + "\r\n");
                }

                return strb;

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

        public Word findIdByWord(string word)
        {
            Word quiz = new Word();
            IThemeDAO themeDao = new ThemeDAO();
            try
            {

                Program.conn.Open();//get connect;
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM word WHERE name LIKE '%" + word + "%'", Program.conn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string form = reader.GetString(2);
                    int themeid = reader.GetInt32(3);

                    quiz = new Word(id, name, form, themeDao.findById(themeid));
                }
                return quiz;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                return quiz;
            }
            finally
            {
                Program.conn.Close();   // Close connection
            }
        }
    }
}
