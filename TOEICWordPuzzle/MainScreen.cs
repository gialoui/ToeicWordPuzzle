using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOEICWordPuzzle
{
    public partial class MainScreen : MetroForm
    {
        private IThemeDAO themeDAO = new ThemeDAO();
        public MainScreen()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ObservableCollection<Theme> themes = themeDAO.getAll();

            if (themes != null)
            {
                foreach (Theme theme in themes)
                {
                    metroComboBox1.Items.Add(theme);
                }
            }

            metroComboBox1.ValueMember = "id";
            metroComboBox1.DisplayMember = "name";
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedItem != null)
            {
                // this.Close();
                MetroForm playingScreen = new PlayingScreen(metroComboBox1.SelectedItem as Theme);
                playingScreen.Show();
            }
        }

        private void btnPlayAll_Click(object sender, EventArgs e)
        {
            MetroForm playingScreen = new PlayingScreen(null);
            playingScreen.Show();
        }
    }
}
