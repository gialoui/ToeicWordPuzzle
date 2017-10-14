using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOEICWordPuzzle
{
    public partial class PlayingScreen : MetroForm
    {
        IWordDAO wordDao = new WordDAO();
        private int buttonSize = 29;
        private Button[,] _buttons = new Button[15, 15];
        private static ObservableCollection<Word> allWords = new ObservableCollection<Word>();
        private readonly List<Word> _words = new List<Word>();
        private List<Word> _usedWords = new List<Word>();

        private List<Word> _foundWords = new List<Word>();
        private List<Word> _leftWords = new List<Word>();

        private MyButton _firstClick = null;
        private MyButton _secondClick = null;

        int xPos = 0;
        int yPos = 0;
        Random rand = new Random();

        Crossword _board = new Crossword(15, 15);

        public PlayingScreen(Theme theme)
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
            if (theme != null)
            {
                allWords = wordDao.getAll(theme);
                label1.Text = theme.name;
            }
            else if (theme == null)
            {
                allWords = wordDao.getAll();
                metroLabel1.Text = "";
                label1.Text = "";
            }

            if (allWords != null)
            {
                foreach (Word data in allWords)
                {
                    _words.Add(data);
                }

                clearRound();
            }
        }

        private void initWords()
        {
            _board.Reset();
            _buttons = new Button[15, 15];
            panel1.Controls.Clear();

            // Add words to board
            foreach (Word data in _words.Take(5))
            {
                _board.AddWord(data.name);
            }

            // Remove 5 items from temp list and add to used-words List
            _usedWords.AddRange(_words.Take(5));
            _leftWords.AddRange(_words.Take(5));
            _words.RemoveRange(0, 5);

            var board = _board.GetBoard;

            for (var i = 0; i < _board.N; i++)
            {
                for (var j = 0; j < _board.M; j++)
                {
                    var letter = board[i, j] == '*' ? ' ' : board[i, j];
                    Button button;

                    if (letter != ' ')
                    {
                        button = new Button()
                        {
                            Text = letter.ToString(),
                            Tag = i + ";" + j,  // Store array index in Tag
                            FlatStyle = FlatStyle.Flat,
                            Width = buttonSize,
                            Height = buttonSize,
                            Left = xPos,
                            Top = yPos,
                            Font = new Font("Comic Sans MS", 12)
                        };
                    }
                    else
                    {
                        button = new Button()
                        {
                            Text = Program.getRandLetter() + "",    // Get random char
                            Tag = i + ";" + j,  // Store array index in Tag
                            FlatStyle = FlatStyle.Flat,
                            Width = buttonSize,
                            Height = buttonSize,
                            Left = xPos,
                            Top = yPos,
                            Font = new Font("Comic Sans MS", 12)
                        };
                    }


                    xPos = xPos + buttonSize + 1;
                    // Add button to Array
                    _buttons[i, j] = button;
                    // Set event handler for every button
                    _buttons[i, j].Click += (sender, e) => buttonClickHandler(sender, e);
                    panel1.Controls.Add(_buttons[i, j]);
                }

                xPos = 0;
                yPos = yPos + buttonSize + 1;
            }

            xPos = 0;
            yPos = 0;
        }

        private void buttonClickHandler(object sender, EventArgs e)
        {
            // Get the button control we need to respond too.
            Button button = sender as Button;
            int i = 0;
            int j = 0;

            if (button.Tag.ToString().IndexOf(';') == 2)
            {
                i = Int32.Parse(button.Tag.ToString().Substring(0, 2));
                j = Int32.Parse(button.Tag.ToString().Substring(3));
            }
            else if (button.Tag.ToString().IndexOf(';') == 1)
            {
                i = Int32.Parse(button.Tag.ToString().Substring(0, 1));
                j = Int32.Parse(button.Tag.ToString().Substring(2));
            }

            for (var row = 0; row < _board.N; row++)
            {
                for (var col = 0; col < _board.M; col++)
                {
                    if (_buttons[row, col].ForeColor == Color.DarkRed || _buttons[row, col].BackColor == Color.LightBlue)
                    {
                        _buttons[row, col].ForeColor = Color.Black;
                        _buttons[row, col].BackColor = Color.White;
                        _buttons[row, col].Font = new Font(_buttons[row, col].Font, FontStyle.Regular);
                        _buttons[row, col].FlatStyle = FlatStyle.Flat;
                    }
                }
            }

            if (_firstClick == null)
            {
                _firstClick = new MyButton(button.Text.First(), i, j);
                button.BackColor = Color.LightBlue;
                button.Font = new Font(button.Font, FontStyle.Bold);
            }
            else if (_secondClick == null)
            {
                _secondClick = new MyButton(button.Text.First(), i, j);
                if (_firstClick.Row == _secondClick.Row && _firstClick.Column == _secondClick.Column)
                {
                    _firstClick = new MyButton(button.Text.First(), i, j);
                    button.BackColor = Color.LightBlue;
                    button.Font = new Font(button.Font, FontStyle.Bold);
                    _secondClick = null;
                }
                else if (_firstClick.Row == _secondClick.Row || _firstClick.Column == _secondClick.Column)
                {
                    string temp = "";
                    if (_firstClick.Row == _secondClick.Row)
                    {
                        if (_firstClick.Column > _secondClick.Column)
                        {
                            for (int index = _secondClick.Column; index <= _firstClick.Column; index++)
                            {
                                temp += _buttons[_firstClick.Row, index].Text;
                            }

                            if (findWordInList(temp))
                            {
                                for (int index = _secondClick.Column; index <= _firstClick.Column; index++)
                                {
                                    _buttons[_firstClick.Row, index].BackColor = System.Drawing.Color.LightGreen;
                                    _buttons[_firstClick.Row, index].Font = new Font(_buttons[_firstClick.Row, index].Font, FontStyle.Bold);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong darling!");
                            }
                        }
                        else
                        {
                            for (int index = _firstClick.Column; index <= _secondClick.Column; index++)
                            {
                                temp += _buttons[_secondClick.Row, index].Text;
                            }

                            if (findWordInList(temp))
                            {
                                for (int index = _firstClick.Column; index <= _secondClick.Column; index++)
                                {
                                    _buttons[_secondClick.Row, index].BackColor = System.Drawing.Color.LightGreen;
                                    _buttons[_secondClick.Row, index].Font = new Font(_buttons[_secondClick.Row, index].Font, FontStyle.Bold);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong darling!");
                            }
                        }
                    }
                    else if (_firstClick.Column == _secondClick.Column)
                    {
                        if (_firstClick.Row > _secondClick.Row)
                        {
                            for (int index = _secondClick.Row; index <= _firstClick.Row; index++)
                            {
                                temp += _buttons[index, _firstClick.Column].Text;
                            }

                            if (findWordInList(temp))
                            {
                                for (int index = _secondClick.Row; index <= _firstClick.Row; index++)
                                {
                                    _buttons[index, _firstClick.Column].BackColor = System.Drawing.Color.LightGreen;
                                    _buttons[index, _firstClick.Column].Font = new Font(_buttons[index, _firstClick.Column].Font, FontStyle.Bold);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong darling!");
                            }
                        }
                        else
                        {
                            for (int index = _firstClick.Row; index <= _secondClick.Row; index++)
                            {
                                temp += _buttons[index, _firstClick.Column].Text;
                            }

                            if (findWordInList(temp))
                            {
                                for (int index = _firstClick.Row; index <= _secondClick.Row; index++)
                                {
                                    _buttons[index, _firstClick.Column].BackColor = System.Drawing.Color.LightGreen;
                                    _buttons[index, _firstClick.Column].Font = new Font(_buttons[index, _firstClick.Column].Font, FontStyle.Bold);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong darling!");
                            }
                        }
                    }

                    _firstClick = null;
                    _secondClick = null;

                    if (_leftWords.Count == 0)
                    {
                        btnNext.Visible = true;
                        btnHelp.Visible = false;
                        txtHint.Text = "";
                    }
                }
                else
                {
                    _firstClick = new MyButton(button.Text.First(), i, j);
                    button.BackColor = Color.LightBlue;
                    button.Font = new Font(button.Font, FontStyle.Bold);
                    _secondClick = null;
                }
            }
        }

        private Boolean findWordInList(string word)
        {
            bool flag = false;
            if (word != "" && _leftWords.Count > 0)
            {
                foreach (var data in _leftWords)
                {
                    if (word == data.name)
                    {
                        flag = true;
                        MetroForm quizForm = new QuizScreen(data);
                        quizForm.Show();
                        setFoundWord(word);
                        _leftWords.Remove(data);
                        _foundWords.Add(data);
                        return flag;
                    }
                }
            }

            return flag;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (_leftWords.Count > 0)
            {
                int i = rand.Next(0, _leftWords.Count);
                char firstLetter = _leftWords.ElementAt(i).name.First();
                for (var row = 0; row < _board.N; row++)
                {
                    txtHint.Text = firstLetter + "";
                    for (var col = 0; col < _board.M; col++)
                    {
                        if (_buttons[row, col].Text == firstLetter + "" && _buttons[row, col].BackColor != Color.LightGreen)
                        {
                            _buttons[row, col].ForeColor = System.Drawing.Color.DarkRed;
                            _buttons[row, col].Font = new Font(_buttons[row, col].Font, FontStyle.Bold);
                        }
                        else if (_buttons[row, col].ForeColor == System.Drawing.Color.DarkRed)
                        {
                            _buttons[row, col].ForeColor = System.Drawing.Color.Black;
                            _buttons[row, col].Font = new Font(_buttons[row, col].Font, FontStyle.Regular);
                        }
                    }
                }
            }
        }

        private void setFoundWord(string word)
        {
            Font labelFont = new Font("Microsoft Sans Serif", 14, FontStyle.Strikeout);
            Color fontColor = System.Drawing.Color.OrangeRed;

            if (label2.Text == "")
            {
                label2.Text = word;
                label2.Font = labelFont;
                label2.ForeColor = fontColor;
            }
            else if (label3.Text == "")
            {
                label3.Text = word;
                label3.Font = labelFont;
                label3.ForeColor = fontColor;
            }
            else if (label4.Text == "")
            {
                label4.Text = word;
                label4.Font = labelFont;
                label4.ForeColor = fontColor;
            }
            else if (label5.Text == "")
            {
                label5.Text = word;
                label5.Font = labelFont;
                label5.ForeColor = fontColor;
            }
            else if (label6.Text == "")
            {
                label6.Text = word;
                label6.Font = labelFont;
                label6.ForeColor = fontColor;
            }
        }

        private void clearRound()
        {
            btnNext.Visible = false;
            btnHelp.Visible = true;
            initWords();
            txtHint.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_words.Count != 0)
            {
                clearRound();
            }
            else
            {
                // If player find all words in this theme
                this.Close();
                MessageBox.Show("You've just finished the game");
            }
        }
    }
}
