using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOEICWordPuzzle
{
    class WordDAO : IWordDAO
    {
        private IThemeDAO themeDao = new ThemeDAO();

        public System.Collections.ObjectModel.ObservableCollection<Word> getAll(Theme theme)
        {
            ObservableCollection<Word> words = new ObservableCollection<Word>();
            try
            {
                Program.conn.Open();    // Open new connection
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM word WHERE theme = " + theme.id + " ORDER BY RANDOM()", Program.conn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    words.Add(new Word(reader.GetInt32(0), reader.GetString(1).ToUpper().Trim(), reader.GetString(2), theme));
                }

                return words;
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

        public Word findById(int id)
        {
            try
            {
                Program.conn.Open();    // Open new connection
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM word WHERE id = " + id, Program.conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                Word word = new Word();

                while (reader.Read())
                {
                    Theme theme = themeDao.findById(reader.GetInt32(3));
                    word = new Word(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), theme);
                }

                return word;
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

        public bool isIdExist(int id)
        {
            try
            {
                Program.conn.Open();    // Open new connection
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM word WHERE id = " + id, Program.conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                Word word = new Word();

                if (!reader.HasRows)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                return false;
            }
            finally
            {
                Program.conn.Close();   // Close connection
            }
        }


        public Word getRandomWord()
        {
            try
            {
                Program.conn.Open();    // Open new connection
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM word ORDER BY RANDOM() LIMIT 1", Program.conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                Word word = new Word();

                while (reader.Read())
                {
                    Theme theme = themeDao.findById(reader.GetInt32(3));
                    word = new Word(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), theme);
                }

                return word;
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


        public ObservableCollection<Word> getAll()
        {
            ObservableCollection<Word> words = new ObservableCollection<Word>();
            try
            {
                Program.conn.Open();    // Open new connection
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM word ORDER BY RANDOM()", Program.conn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Theme theme = themeDao.findById(reader.GetInt32(3));
                    words.Add(new Word(reader.GetInt32(0), reader.GetString(1).ToUpper().Trim(), reader.GetString(2), theme));
                }

                return words;
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
    }
}
