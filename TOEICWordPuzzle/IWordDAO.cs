using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOEICWordPuzzle
{
    interface IWordDAO
    {
        ObservableCollection<Word> getAll(Theme theme);

        ObservableCollection<Word> getAll();

        Word findById(int id);

        Boolean isIdExist(int id);

        Word getRandomWord();
    }
}
