using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOEICWordPuzzle
{
    class MyButton
    {
        private char text;

        public char Text
        {
            get { return text; }
            set { text = value; }
        }
        private int row;

        public int Row
        {
            get { return row; }
            set { row = value; }
        }
        private int column;

        public int Column
        {
            get { return column; }
            set { column = value; }
        }

        public MyButton()
        {

        }

        public MyButton(char text, int row, int column)
        {
            this.text = text;
            this.row = row;
            this.column = column;
        }
    }
}
