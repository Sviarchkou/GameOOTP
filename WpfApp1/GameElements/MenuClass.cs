using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOOTP.GameElements
{
    public class MenuClass : DisplayObject
    {

        public RoutedEventHandler StartButtonClickEvent { set => StartButton.Click += value; }
        public RoutedEventHandler LoadButtonClickEvent { set => LoadButton.Click += value; }
        public RoutedEventHandler SaveButtonClickEvent { set => SaveButton.Click += value; }
        public RoutedEventHandler FinishButtonClickEvent { set => FinishButton.Click += value; }


        Grid grid = new Grid()
        {
            Background = Brushes.SandyBrown,
        };

        StackPanel menuStackPanel = new StackPanel()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };

        StackPanel settingsStackPanel = new StackPanel()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Visibility = Visibility.Collapsed,
        };


        /// <summary>
        /// Buttons
        /// </summary>

        Button StartButton = new Button()
        {
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Content = "Start",
            Foreground = Brushes.Black,
            BorderThickness = new Thickness(0)
        };
        Button LoadButton = new Button()
        {
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Content = "Load",
            Foreground = Brushes.Black,
            BorderThickness = new Thickness(0)
        };
        Button SaveButton = new Button()
        {
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Content = "Save",
            Foreground = Brushes.Black,
            BorderThickness = new Thickness(0)
        };
        Button SettingsButton = new Button()
        {
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Content = "Settings",
            Foreground = Brushes.Black,
            BorderThickness = new Thickness(0)
        };
        Button FinishButton = new Button()
        {
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Content = "Finish",
            Foreground = Brushes.Black,
            BorderThickness = new Thickness(0)
        };

        Button BackToMenuButton = new Button()
        {
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Content = "Bask to menu",
            Foreground = Brushes.Black,
            BorderThickness = new Thickness(0)
        };

        /// <summary>
        /// TextBlocks
        /// </summary>

        private TextBlock MenuText = new TextBlock()
        {
            Text = "Menu",
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            Foreground = Brushes.Black
        };

        private TextBlock SettingsText = new TextBlock()
        {
            Text = "Settings",
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            Foreground = Brushes.Black
        };



        public Visibility MenuVisibility
        {
            get => grid.Visibility;
            set => grid.Visibility = value;
        }

        public override int Height
        {
            get => base.Height;
            set
            {
                if (value < 30)
                    throw new ArgumentException();

                base.Height = value;
                grid.Height = value;
                menuStackPanel.Height = value;
                settingsStackPanel.Height = value;

                MenuText.Height = base.Height / 6;
                MenuText.FontSize = MenuText.Height * 3 / 5;
                //MenuText.Margin = new Thickness(0, 0, 0, Height * 5 / 6);

                StartButton.Height = base.Height / 6;
                StartButton.FontSize = StartButton.Height * 3 / 5;
                //StartButton.Margin = new Thickness(0, Height * 1 / 6, 0, Height * 4 / 6);

                LoadButton.Height = base.Height / 6;
                LoadButton.FontSize = LoadButton.Height * 3 / 5;
                //LoadButton.Margin = new Thickness(0, Height * 2 / 6, 0, Height * 3 / 6);

                SaveButton.Height = base.Height / 6;
                SaveButton.FontSize = SaveButton.Height * 3 / 5;
                //SaveButton.Margin = new Thickness(0, Height * 3 / 6, 0, Height * 2 / 6);

                FinishButton.Height = base.Height / 6;
                FinishButton.FontSize = FinishButton.Height * 3 / 5;
                //FinishButton.Margin = new Thickness(0, Height * 4 / 6, 0, Height * 1 / 6);

                SettingsButton.Height = base.Height / 6;
                SettingsButton.FontSize = SettingsButton.Height * 3 / 5;
                //SettingsButton.Margin = new Thickness(0, Height * 5 / 6, 0, 0);

                SettingsText.Height = base.Height / 6;
                SettingsText.FontSize = SettingsText.Height * 3 / 5;
                //SettingsText.Margin = new Thickness(0, 0, 0, Height * 5 / 6);

                BackToMenuButton.Height = base.Height / 6;
                BackToMenuButton.FontSize = BackToMenuButton.Height * 3 / 5;
                //BackToMenuButton.Margin = new Thickness(0, Height * 5 / 6, 0, 0);

            }
        }

        public override int Width
        {
            get => base.Width;
            set
            {
                if (value < 50)
                    throw new ArgumentException();

                base.Width = value;
                grid.Width = value;
                menuStackPanel.Width = value;
                settingsStackPanel.Width = value;


                MenuText.Width = value;
                StartButton.Width = value;
                LoadButton.Width = value;
                SaveButton.Width = value;
                FinishButton.Width = value;
                SettingsButton.Width = value;
                SettingsText.Width = value;
            }

        }

        public MenuClass(double x, double y, int height, int width) : base(x, y, height, width)
        {
            if (y - height / 2 < 0 || x - width / 2 < 0)
                throw new ArgumentException("In MenuClass X and Y is center of the object");

            Height = height;

            grid.Height = height;
            grid.Width = width;

            MenuVisibility = Visibility.Collapsed;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            SettingsButton.Click += SettingsButtonClickEvent;
            BackToMenuButton.Click += BackToMenuButton_Click;

            menuStackPanel.Children.Add(MenuText);
            menuStackPanel.Children.Add(StartButton);
            menuStackPanel.Children.Add(LoadButton);
            menuStackPanel.Children.Add(SaveButton);
            menuStackPanel.Children.Add(SettingsButton);
            menuStackPanel.Children.Add(FinishButton);

            settingsStackPanel.Children.Add(SettingsText);
            settingsStackPanel.Children.Add(BackToMenuButton);

            grid.Children.Add(menuStackPanel);
            grid.Children.Add(settingsStackPanel);
        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            menuStackPanel.Visibility = Visibility.Visible;
            settingsStackPanel.Visibility = Visibility.Collapsed;
        }

        public override DisplayObject Display(Canvas canvas)
        {
            Canvas.SetTop(grid, Y - Height / 2);
            Canvas.SetLeft(grid, X - base.Width / 2);
            canvas.Children.Add(grid);
            return this;
        }

        private void SettingsButtonClickEvent(object sender, RoutedEventArgs e)
        {
            menuStackPanel.Visibility = Visibility.Collapsed;
            settingsStackPanel.Visibility = Visibility.Visible;
        }
    }
}
