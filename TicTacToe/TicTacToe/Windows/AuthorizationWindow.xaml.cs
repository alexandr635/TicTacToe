using System.Windows;

namespace TicTacToe.Windows
{
    /// <summary>
    /// Interaction logic for AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(loginTxtBx.Text) && !string.IsNullOrWhiteSpace(passwordTxtBx.Text))
            {
                DataBase.User currentUser = new DataBase.User();
                currentUser.Login = loginTxtBx.Text;
                currentUser.Password = passwordTxtBx.Text;
                DataBase.User user = Logic.DBQuery.Authorization(currentUser);
                if (user != null)
                {
                    if (user.Login != null)
                    {
                        GameWindow gameWindow = new GameWindow(user);
                        Close();
                        gameWindow.Show();
                    }
                    else
                        MessageBox.Show("Данной записи не существует. Пожалуйста, проверьте правильность ввода данных", "Отсутствие данных");
                }
                else
                    MessageBox.Show("Что-то пошло не так. Пожалуйста зайдите позже...", "Ошибка подключения");
            }
            else
                MessageBox.Show("Сначала заполните все поля!", "Ошибка заполнения");
                    
        }

        private void AuthorizationWindow_Load(object sender, RoutedEventArgs e)
        {
            loginTxtBx.Focus();
        }
    }
}
