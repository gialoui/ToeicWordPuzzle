using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOEICWordPuzzle
{
    public interface IMeaningDAO
    {
        ObservableCollection<Meaning> getAll();
        Meaning findById(int id);
        StringBuilder getMeaningByID(int id);
        Word findIdByWord(string word);
    }
}
