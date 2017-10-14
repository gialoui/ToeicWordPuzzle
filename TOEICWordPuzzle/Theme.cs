using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TOEICWordPuzzle
{
    public class Theme
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(45), Unique]
        public String name { get; set; }

        public Theme(int id, String name)
        {
            this.id = id;
            this.name = name;
        }

        public Theme()
        {

        }
    }
}
