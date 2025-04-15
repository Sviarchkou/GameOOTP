using System.IO;
using System.Runtime.Serialization;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GameOOTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameClass game;
        public MainWindow()
        {
            InitializeComponent();            
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            game = GameClass.Instanse(GameCanvas);
            game.isPaused = false;
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            game?.Window_KeyDown(sender, e);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GameCanvas.Height = e.NewSize.Height - 50;
            GameCanvas.Width = e.NewSize.Width - 50;
        }

        public void ChangeWindowState()
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }
    }
    
}