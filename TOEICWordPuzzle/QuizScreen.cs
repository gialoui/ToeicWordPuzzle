using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace TOEICWordPuzzle
{
    public partial class QuizScreen : MetroForm
    {
        private int Value = 0;
        IThemeDAO themeDao = new ThemeDAO();
        IMeaningDAO meaningDao = new MeaningDAO();
        IWordDAO wordDao = new WordDAO();
        
        public QuizScreen(Word word)
        {
            // Word wordquiz;
            ObservableCollection<Word> words = new ObservableCollection<Word>();

            // IWordDAO wordDao = new WordDAO();  
            InitializeComponent();
            
            metroLabel1.Font = new Font("Comic Sans MS", 16);
            metroLabel1.Text = "What does " + word.name + " (" + word.form + ") means?";

            if (Value == 0)
            {
                metroButton1.Enabled = false;
            }

            Random rnd = new Random();
            Word randomWord1 = new Word();
            Word randomWord2 = new Word();
            Value = rnd.Next(1, 4);

            do
            {
                randomWord1 = wordDao.getRandomWord();
            } while (randomWord1.id == word.id || randomWord1 == null);

            do
            {
                randomWord2 = wordDao.getRandomWord();
            } while (randomWord2.id == word.id || randomWord2.id == randomWord1.id || randomWord2 == null);

            StringBuilder answer = meaningDao.getMeaningByID(word.id);
            StringBuilder randomAnswer1 = meaningDao.getMeaningByID(randomWord1.id);
            StringBuilder randomAnswer2 = meaningDao.getMeaningByID(randomWord2.id);

            if (Value == 1)
            {
                metroTextBox1.Text = answer.ToString();
                metroTextBox2.Text = randomAnswer1.ToString();
                metroTextBox3.Text = randomAnswer2.ToString();
            }
            else if (Value == 2)
            {
                metroTextBox1.Text = randomAnswer1.ToString();
                metroTextBox2.Text = answer.ToString();
                metroTextBox3.Text = randomAnswer2.ToString();
            }
            else if (Value == 3)
            {
                metroTextBox1.Text = randomAnswer1.ToString();
                metroTextBox2.Text = randomAnswer2.ToString();
                metroTextBox3.Text = answer.ToString();
            }
        }

        private void metroTextButton1_Click(object sender, EventArgs e)
        {
            chooseAnswer(groupBox1, 1);
        }

        private void metroTextButton2_Click(object sender, EventArgs e)
        {
            chooseAnswer(groupBox2, 2);
        }

        private void metroTextButton3_Click(object sender, EventArgs e)
        {
            chooseAnswer(groupBox3, 3);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chooseAnswer(GroupBox groupBox, int buttonIndex)
        {
            if (buttonIndex == Value)
            {
                MessageBox.Show("Congratulation!", "You're right");
                groupBox.BackColor = Color.LightCyan;
                metroButton1.Enabled = true;

                metroTextButton1.Enabled = false;
                metroTextButton2.Enabled = false;
                metroTextButton3.Enabled = false;
            }
            else
            {
                MessageBox.Show("Choose other answer!!", "FALSE!");
            }
        }

    }
}
