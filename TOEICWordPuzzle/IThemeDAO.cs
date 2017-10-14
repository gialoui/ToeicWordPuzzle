using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOEICWordPuzzle
{
    public interface IThemeDAO
    {
        ObservableCollection<Theme> getAll();
        Theme findById(int id);
    }
}
