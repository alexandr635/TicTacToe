using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TicTacToe.Windows;

namespace TicTacToe.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private int counter = 0;
        private GameWindow gameWindow { set; get; }
        private int size { set; get; }
        private Button[,] arrayOfButton { set; get; }

        public GamePage(int size, GameWindow window)
        {
            InitializeComponent();
            gameWindow = window;
            this.size = size;
            arrayOfButton = new Button[size, size];
            CreateArea();
            AddControls();
        }

        private void AddControls()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button button = new Button()
                    {
                        Height = 130,
                        Margin = new Thickness(0),
                        Foreground = new SolidColorBrush(Colors.White),
                        FontSize = 27
                    };

                    arrayOfButton[i, j] = button;

                    Border border = new Border()
                    {
                        BorderThickness = new Thickness(2),
                        BorderBrush = Brushes.Black,
                    };
                    gameAreaGrd.Children.Add(button);
                    Grid.SetColumn(button, j);
                    Grid.SetRow(button, i);

                    gameAreaGrd.Children.Add(border);
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    button.Click += Button_Click;
                }
            } 
        }

        private void CreateArea()
        {
            for (int i = 0; i < size; i++)
            {
                gameAreaGrd.ColumnDefinitions.Add(new ColumnDefinition());
                gameAreaGrd.RowDefinitions.Add(new RowDefinition());
            }
        }

        private bool? CheckWin()
        {
            int win;
            int lose;
            //проверка на горизонталь
            for (int i = 0; i < size; i++)
            {
                win = lose = 0;
                for (int j = 0; j < size; j++)
                {
                    if ((string)arrayOfButton[i, j].Content == "x")
                    {
                        win++;
                        lose = 0;
                    }
                    else if ((string)arrayOfButton[i, j].Content == "o")
                    {
                        lose++;
                        win = 0;
                    }
                    else
                        break;

                    if (win == size)
                        return true;
                    if (lose == size)
                        return false;
                }
            }

            //проверка на вертикаль
            for (int i = 0; i < size; i++)
            {
                win = lose = 0;
                for (int j = 0; j < size; j++)
                {
                    if ((string)arrayOfButton[j, i].Content == "x")
                    {
                        win++;
                        lose = 0;
                    }
                    else if ((string)arrayOfButton[j, i].Content == "o")
                    {
                        lose++;
                        win = 0;
                    }
                    else
                        break;

                    if (win == size)
                        return true;
                    if (lose == size)
                        return false;
                }
            }

            win = lose = 0;
            //проверка на диагональ сверху вниз
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        if ((string)arrayOfButton[j, i].Content == "x")
                        {
                            win++;
                        }
                        else if ((string)arrayOfButton[j, i].Content == "o")
                        {
                            lose++;
                        }
                        else
                            break;

                        if (win == size)
                            return true;
                        if (lose == size)
                            return false;
                    }
                }
            }
            win = lose = 0;

            //проверка на диагональ снизу вверх
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i + j == size - 1)
                    {
                        if ((string)arrayOfButton[j, i].Content == "x")
                        {
                            win++;
                        }
                        else if ((string)arrayOfButton[j, i].Content == "o")
                        {
                            lose++;
                        }
                        else
                            break;

                        if (win == size)
                            return true;
                        if (lose == size)
                            return false;
                    }
                }
            }

            return null;
        }

        private void ClearContent()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    arrayOfButton[i, j].Content = null;
                }
            }
        }

        private bool Message()
        {
            if (CheckWin() == true)
            {
                MessageBox.Show("Вы выйграли");
                ClearContent();
                counter = 0;
                if (gameWindow.currentUser != null)
                {
                    var exception = Logic.DBQuery.IncrementCountOfWin(gameWindow.currentUser);
                    if (exception != null)
                        MessageBox.Show("К сожалению мы не смогли добавить победу...", "Ошибка сервера");
                }
                return true;
            }
            else if (CheckWin() == false)
            {
                MessageBox.Show("Вы проиграли");
                ClearContent();
                counter = 0;
                return true;
            }

            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content == null)
            {
                (sender as Button).Content = "x";
                if (Message() == false)
                {
                    counter++;
                    if (counter != arrayOfButton.Length)
                    {
                        arrayOfButton = Logic.Bot.BotAct(arrayOfButton);
                        counter++;
                        Message();
                    }
                    else
                    {
                        MessageBox.Show("Ничья");
                        ClearContent();
                        counter = 0;
                    }
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Logic.FrameManager.mainFrame.Navigate(new ChoiceOfDimensionPage(gameWindow));
        }
    }
}
