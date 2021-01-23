using System;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Windows;

namespace TicTacToe.Pages
{
    /// <summary>
    /// Interaction logic for ChoiceOfDimensionPage.xaml
    /// </summary>
    public partial class ChoiceOfDimensionPage : Page
    {
        private GameWindow gameWindow {set; get;}
        public ChoiceOfDimensionPage(GameWindow window)
        {
            InitializeComponent();
            gameWindow = window;
            choiceOfDimensionCmbBx.ItemsSource = Logic.DBQuery.GetListOfDimensions();
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (choiceOfDimensionCmbBx.SelectedIndex > -1)
            {
                int size = Convert.ToInt32(choiceOfDimensionCmbBx.Text);
                Logic.FrameManager.mainFrame.Navigate(new GamePage(size, gameWindow));
            }
            else
                MessageBox.Show("Выберите размерность поля!", "Ошибка заполнения");
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            gameWindow.Close();
            mainWindow.Show();
        }
    }
}
