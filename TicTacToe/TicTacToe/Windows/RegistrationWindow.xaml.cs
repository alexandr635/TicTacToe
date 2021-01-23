using System.Windows;

namespace TicTacToe.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private DataBase.User newUser = new DataBase.User();
        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = newUser;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }

        private void RegistrationWindow_Load(object sender, RoutedEventArgs e)
        {
            loginTxtBx.Focus();
        }

        private bool CheckFillingOfFields()
        {
            if (string.IsNullOrWhiteSpace(loginTxtBx.Text))
                return false;
            if (string.IsNullOrWhiteSpace(passwordTxtBx.Text))
                return false;
            if (string.IsNullOrWhiteSpace(repeatPasswordTxtBx.Text))
                return false;
            if (string.IsNullOrWhiteSpace(mailTxtBx.Text))
                return false;

            return true;
        }

        private void ClearFields()
        {
            loginTxtBx.Text = null;
            passwordTxtBx.Text = null;
            repeatPasswordTxtBx.Text = null;
            mailTxtBx.Text = null;
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFillingOfFields())
            {
                if (passwordTxtBx.Text == repeatPasswordTxtBx.Text)
                {
                    var exception = Logic.DBQuery.AddUser(newUser);
                    if (exception == null)
                    {
                        MessageBox.Show("Пользователь добавлен!", "Удача");
                        ClearFields();
                    }
                    else if (exception.Source == null)
                        MessageBox.Show("Пользователь с данным логином уже существует. Пожалуйста, введите другой логин", "Неверное имя");
                    else
                        MessageBox.Show("Что-то пошло не так. Попробуйте позже...", "Ошибка сервера");
                }
                else
                    MessageBox.Show("Поля 'Пароль' и 'Повторите пароль' не совпадают. Измените данные!'", "Несоответствие данных");
            }
            else
                MessageBox.Show("Сначала заполните все поля!", "Ошибка заполнения");
        }
    }
}
