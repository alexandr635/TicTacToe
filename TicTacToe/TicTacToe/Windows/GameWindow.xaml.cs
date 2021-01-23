using System.Windows;

namespace TicTacToe.Windows
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public DataBase.User currentUser { set; get; }
        public GameWindow(DataBase.User user)
        {
            InitializeComponent();
            currentUser = user;
            Logic.FrameManager.mainFrame = gameFrm;
            Logic.FrameManager.mainFrame.Navigate(new Pages.ChoiceOfDimensionPage(this));
        }
    }
}
