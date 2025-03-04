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
/*
            switch (e.Key)
            {
                case Key.A:
                case Key.S:
                case Key.R:
                case Key.P:
                    if (game != null)
                        game.Window_KeyDown(sender, e);
                    break;
                case Key.F:
                    ChangeWindowState();
                    break;
            }*/
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GameCanvas.Height = e.NewSize.Height - 50;
            GameCanvas.Width = e.NewSize.Width - 50;
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            if (game != null)
                game.Finish();
            App.Current.Shutdown();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Collapsed;
            game = GameClass.Instanse(GameCanvas);
            game.isPaused = false;
        }

        public void ChangeWindowState()
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = Visibility.Collapsed;
            SettingsPanel.Visibility = Visibility.Visible;
            if (game != null)
                game.Settings();

        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = Visibility.Visible;
            SettingsPanel.Visibility = Visibility.Collapsed;
        }

        private void ButtonMouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = Brushes.Black;//new SolidColorBrush(Color.FromRgb(61, 82, 61));
        }
        private void ButtonMouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Background = Brushes.Transparent;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

    }
    
}