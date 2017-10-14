using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOEICWordPuzzle
{
    public class Meaning
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(255)]
        public String meaning { get; set; }

        public Word word { get; set; }

        public Meaning()
        {

        }

        public Meaning(int id, String meaning, Word word)
        {
            this.id = id;
            this.meaning = meaning;
            this.word = word;
        }
    }
}
