using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using GameOOTP.GameElements;
using GameOOTP.GameElements.Figures;
using Newtonsoft.Json;
using System.IO;

namespace GameOOTP
{
    class GameClass
    {
        private const int NUMBER_OF_FIGURES = 40;

        Canvas GameCanvas;

        List<Figure> FigureList = new List<Figure>();
        MenuClass MenuClass;
        Random random = new Random();

        private DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(20) };
        //Timer timer = new Timer();

        private static volatile GameClass instance = null;
        private static readonly object syncRoot = new object();

        public bool isPaused = false;

        private GameClass(Canvas canvas)
        {
            GameCanvas = canvas;
            GenerateFigures();
            timer.Tick += UpdateGame;

        }

        private GameClass(Canvas canvas, List<Figure> figures)
        {
            GameCanvas = canvas;
            FigureList = figures;
            timer.Tick += UpdateGame;

        }

        public static GameClass Instanse(Canvas canvas)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new GameClass(canvas);
                }
            }
            return instance;
        }

        public void GenerateFigures()
        {

            for (int i = 0; i < NUMBER_OF_FIGURES; i++)
            {
                int height = random.Next((int)GameCanvas.Height / 40, (int)GameCanvas.Height / 8);
                int width = height;
                int x = random.Next(0, (int)GameCanvas.Width - width);
                int y = random.Next(0, (int)GameCanvas.Height - height);

                Figure figure;

                if (i < 10)
                    figure = new Circle(x, y, width, height);
                else if (i < 20)
                    figure = new Square(x, y, width, height);
                else if (i < 30)
                    figure = new Triangle(x, y, width, height);
                else
                    figure = new Hexagon(x, y, width, height);

                double angle = random.NextDouble() * 2 * Math.PI;
                double velocity = random.Next(-5, 5); ;
                double accelerator = random.NextDouble() / 10;

                figure.Accelerator = accelerator;
                figure.Velocity = velocity;
                figure.Angle = angle;

                figure.Display(GameCanvas);
                FigureList.Add(figure);
            }

            MenuClass = new MenuClass(GameCanvas.Width / 2, GameCanvas.Height / 2, 300, 250);
            MenuClass.Display(GameCanvas);
            MenuClass.StartButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                isPaused = false;
                timer.Start();
            };
            MenuClass.LoadButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                ////// LOAD //////
            };
            MenuClass.SaveButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                saveGame();
                ////// SAVE //////
            };
            MenuClass.FinishButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                timer.Stop();
                Application.Current.Shutdown();
            };

        }

        private void saveGame()
        {
            string json = JsonConvert.SerializeObject(FigureList, Formatting.Indented);

            using(StreamWriter writer = File.CreateText("data.game"))
            {
                writer.Write(json);
            }
        }

        private void UpdateGame(object? sender, EventArgs e)
        {
            foreach (var figure in FigureList)
            {
                figure.Update(GameCanvas);
            }
        }

        private void StartUniformMotion()
        {
            foreach (var figure in FigureList)
            {
                figure.MoveUniformly();
            }
            timer.Start();
        }

        private void StartAcceleratedMotion()
        {
            foreach (var figure in FigureList)
            {
                figure.MoveAccelerated();
            }
            timer.Start();
        }

        public void Start()
        {
            timer.Start();
        }

      
        public void Finish()
        {
            timer.Stop();
        }

        public void Settings()
        {

        }

        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    StartAcceleratedMotion();
                    break;
                case Key.S:
                    StartUniformMotion();
                    break;
                case Key.R:
                    GameCanvas.Children.Clear();
                    GenerateFigures();
                    break;
                case Key.P:
                    if (isPaused)
                    {
                        MenuClass.MenuVisibility = Visibility.Collapsed;
                        timer.Start();
                        isPaused = false;
                        return;
                    }
                    timer.Stop();
                    isPaused = true;
                    MenuClass.MenuVisibility = Visibility.Visible;
                    break;
                case Key.F:
                    ((MainWindow)sender).ChangeWindowState();
                    break;
            }
        }

    }
}
