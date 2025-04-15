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
        public RoutedEventHandler FigureAddButtonClickEvent { set => plusCountButton.Click += value; }
        public RoutedEventHandler FigureRemoveButtonClickEvent { set => minusCountButton.Click += value; }
        public RoutedEventHandler FigureEnlargeButtonClickEvent { set => plusSizeButton.Click += value; }
        public RoutedEventHandler FigureReduceButtonClickEvent { set => minusSizeButton.Click += value; }


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
        StackPanel figureCountPanel = new StackPanel()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Orientation = Orientation.Horizontal,
            Width = Double.NaN           
        };
        StackPanel figureSizePanel = new StackPanel()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Orientation = Orientation.Horizontal,
            Width = Double.NaN
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
            Content = "Back to menu",
            Foreground = Brushes.Black,
            BorderThickness = new Thickness(0)
        };
        
        Button plusCountButton = new Button()
        {
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Content = "+",
            FontWeight = FontWeights.Bold,
            Foreground = Brushes.Black,
            BorderThickness = new Thickness(0),
            HorizontalAlignment = HorizontalAlignment.Center
        };
        Button minusCountButton = new Button()
        {
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Content = "-",
            FontWeight = FontWeights.Bold,
            Foreground = Brushes.Black,
            BorderThickness = new Thickness(0),
            HorizontalAlignment = HorizontalAlignment.Center
        };
        Button plusSizeButton = new Button()
        {
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Content = "+",
            FontWeight = FontWeights.Bold,
            Foreground = Brushes.Black,
            BorderThickness = new Thickness(0),
            HorizontalAlignment = HorizontalAlignment.Center
        };
        Button minusSizeButton = new Button()
        {
            Background = Brushes.Transparent,
            BorderBrush = Brushes.Transparent,
            Content = "-",
            FontWeight = FontWeights.Bold,
            Foreground = Brushes.Black,
            BorderThickness = new Thickness(0),
            HorizontalAlignment = HorizontalAlignment.Center
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

        private TextBlock figureCountLabelText = new TextBlock()
        {
            Text = "Count of figures",
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            Foreground = Brushes.Black,
            Margin = new Thickness(5)
        };
        private TextBlock figureCountText = new TextBlock()
        {
            Background = Brushes.White,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            Foreground = Brushes.Black,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(5)

        };
        private TextBlock figureSizeLabelText = new TextBlock()
        {
            Text = "Size of figures",
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            Foreground = Brushes.Black,
            Margin = new Thickness(5)
        };
        private TextBlock figureSizeText = new TextBlock()
        {
            Background = Brushes.White,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            Foreground = Brushes.Black,
            Margin = new Thickness(5),
            HorizontalAlignment = HorizontalAlignment.Center

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
                
                StartButton.Height = base.Height / 6;
                StartButton.FontSize = StartButton.Height * 3 / 5;
                
                LoadButton.Height = base.Height / 6;
                LoadButton.FontSize = LoadButton.Height * 3 / 5;
                
                SaveButton.Height = base.Height / 6;
                SaveButton.FontSize = SaveButton.Height * 3 / 5;
                
                FinishButton.Height = base.Height / 6;
                FinishButton.FontSize = FinishButton.Height * 3 / 5;
                
                SettingsButton.Height = base.Height / 6;
                SettingsButton.FontSize = SettingsButton.Height * 3 / 5;                

                SettingsText.Height = base.Height / 6;
                SettingsText.FontSize = SettingsText.Height * 3 / 5;

                BackToMenuButton.Height = base.Height / 6;
                BackToMenuButton.FontSize = BackToMenuButton.Height * 3 / 5;

               
                figureCountLabelText.Height = base.Height / 10;
                figureCountLabelText.FontSize = figureCountLabelText.Height * 4 / 5;


                figureCountPanel.Height = base.Height / 6;

                plusCountButton.Height = base.Height / 6;
                minusCountButton.Height = base.Height / 6;
                figureCountText.Height = base.Height / 6;

                plusCountButton.Width = plusCountButton.Height;
                minusCountButton.Width = minusCountButton.Height;
                figureCountText.Width = 2 * figureCountText.Height;
                
                plusCountButton.FontSize = plusCountButton.Height * 3 / 5;
                minusCountButton.FontSize = minusCountButton.Height * 3 / 5;                
                figureCountText.FontSize = figureCountText.Height * 3 / 5;

                
                figureSizeLabelText.Height = base.Height / 10;
                figureSizeLabelText.FontSize = figureSizeLabelText.Height * 4 / 5;


                figureSizePanel.Height = base.Height / 6;

                plusSizeButton.Height = base.Height / 6;
                minusSizeButton.Height = base.Height / 6;
                figureSizeText.Height = base.Height / 6;

                plusSizeButton.Width = plusSizeButton.Height;
                minusSizeButton.Width = minusSizeButton.Height;
                figureSizeText.Width = 2 * figureSizeText.Height;

                plusSizeButton.FontSize = plusSizeButton.Height * 3 / 5;
                minusSizeButton.FontSize = minusSizeButton.Height * 3 / 5;
                figureSizeText.FontSize = figureSizeText.Height * 3 / 5;

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
                figureCountLabelText.Width = value;
                figureSizeLabelText.Width = value;

                // figureCountPanel.Width = value;
               //  double marginValue = (value - 4 * figureCountPanel.Height) / 2;
                // figureCountPanel.pa = new Thickness(marginValue, 0, marginValue, 0);
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

            
            figureCountPanel.Children.Add(minusCountButton);
            figureCountPanel.Children.Add(figureCountText); 
            figureCountPanel.Children.Add(plusCountButton);

            
            figureSizePanel.Children.Add(minusSizeButton);
            figureSizePanel.Children.Add(figureSizeText);
            figureSizePanel.Children.Add(plusSizeButton);


            settingsStackPanel.Children.Add(SettingsText);
            settingsStackPanel.Children.Add(figureCountLabelText);
            settingsStackPanel.Children.Add(figureCountPanel);
            settingsStackPanel.Children.Add(figureSizeLabelText);
            settingsStackPanel.Children.Add(figureSizePanel);
            settingsStackPanel.Children.Add(BackToMenuButton);

            grid.Children.Add(menuStackPanel);
            grid.Children.Add(settingsStackPanel);
        }

        public void setFigureCount(int value) => figureCountText.Text = value.ToString();
        public void setFigureSize(int value) => figureSizeText.Text = value.ToString();


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

        public Grid getGrid => grid;
    }
}
