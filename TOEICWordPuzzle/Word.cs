using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOEICWordPuzzle
{
    public class Word
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(14), Unique]
        public String name { get; set; }

        [MaxLength(5)]
        public String form { get; set; }

        public Theme theme { get; set; }

        public Word()
        {

        }

        public Word(int id, String name, String form, Theme theme)
        {
            this.id = id;
            this.name = name;
            this.form = form;
            this.theme = theme;
        }
    }
}
