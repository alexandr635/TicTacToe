using System.Windows;
using TicTacToe.Windows;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tableOfResultsDtGrd.ItemsSource = Logic.DBQuery.GetListOfUsers();
            if (tableOfResultsDtGrd.ItemsSource == null)
            {
                MessageBox.Show("Приложение не может связаться с сервером. Пожалуйста, зайдите позже или воспользуйтесь офлайн-версией приложения.", "Что-то пошло не так...");
                authorizationBtn.IsEnabled = false;
                registrationBtn.IsEnabled = false;
            }
        }

        private void AsQuestBtn_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(null);
            Close();
            gameWindow.Show();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            Close();
            registrationWindow.Show();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            Close();
            authorizationWindow.Show();
        }

        private void GameWindow_Load(object sender, RoutedEventArgs e)
        {
            authorizationBtn.Focus();
        }
    }
}
