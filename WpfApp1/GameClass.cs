using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using GameOOTP.GameElements;
using GameOOTP.GameElements.Figures;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Automation;
using System;
using Microsoft.Win32;
using System.Windows.Media;

namespace GameOOTP
{
    class GameClass
    {

        private int NUMBER_OF_FIGURES = 20;

        Canvas GameCanvas;

        List<Figure> FigureList = new List<Figure>();
        MenuClass MenuClass;
        Random random = new Random();

        private DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(20) };
        //Timer timer = new Timer();

        private static volatile GameClass? instance = null;
        private static readonly object syncRoot = new object();

        public bool isPaused = false;
        public bool isDebagging = false;

        private int FIGURE_SIZE = 80;

        private GameClass(Canvas canvas)
        {
            GameCanvas = canvas;
            GenerateFigures();
            timer.Tick += UpdateGame;
            isPaused = true;
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
                Figure figure = GenerateRandomFigure();
                figure.Display(GameCanvas);
                FigureList.Add(figure);
            }

            MenuClass = new MenuClass(GameCanvas.Width / 2, GameCanvas.Height / 2, 300, 250);
            MenuClass.Display(GameCanvas);
            MenuClass.StartButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                Start();
            };
            MenuClass.LoadButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                loadGame();
            };
            MenuClass.SaveButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                saveGame();
            };
            MenuClass.FinishButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                Finish();
            };
            MenuClass.FigureAddButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                AddRandomFigure();
                MenuClass.setFigureCount(FigureList.Count);
            };
            MenuClass.FigureRemoveButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                RemoveRandomFigure();
                MenuClass.setFigureCount(FigureList.Count);
            };
            MenuClass.FigureEnlargeButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                ChangeSizeOfFigures(5);
                MenuClass.setFigureSize(FIGURE_SIZE);
            };
            MenuClass.FigureReduceButtonClickEvent = delegate (object sender, RoutedEventArgs e)
            {
                ChangeSizeOfFigures(-5);
                MenuClass.setFigureSize(FIGURE_SIZE);
            };
        }

        private Figure GenerateRandomFigure()
        {
            int height = FIGURE_SIZE;//random.Next((int)GameCanvas.Height / 12, (int)GameCanvas.Height / 8);
            int width = height;
            int x = random.Next(0, (int)GameCanvas.Width - width);
            int y = random.Next(0, (int)GameCanvas.Height - height);

            Figure figure;
            int t = 1; //random.Next(1, 4);
            switch (t)
            {
                case 2:
                    figure = new Square(x, y, width, height);
                    break;
                case 3:
                    figure = new Triangle(x, y, width, height);
                    break;
                case 4:
                    figure = new Hexagon(x, y, width, height);
                    break;
                case 1:
                default:
                    figure = new Circle(x, y, width, height);
                    break;
            }


            double angle = random.NextDouble() * 2 * Math.PI;
            double velocity = 0;
            while (velocity == 0 || Math.Abs(velocity) < 0.05)
                velocity = random.Next(-5, 5);

            double accelerator = random.NextDouble() / 10;
            if (accelerator < 0.000005)
                accelerator *= 100;

            figure.Accelerator = accelerator;
            figure.Velocity = velocity;
            figure.Angle = angle;
            return figure;
        }

        public void Start()
        {
            isPaused = false;
            MenuClass.MenuVisibility = Visibility.Collapsed;
            timer.Start();
        }

        public void Finish()
        {
            timer.Stop();
            App.Current.Shutdown();
        }

        private void saveGame()
        {
            List<FigureSerializationType> figureSerializationTypes = GetFigureSerializationTypes();
            string json = JsonConvert.SerializeObject(figureSerializationTypes, Formatting.Indented);

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "data"; 
            dlg.DefaultExt = ".game";
            dlg.Filter = "Game documents (.game)|*.game"; 

            Nullable<bool> result = dlg.ShowDialog();
            
            string? filename = null;
            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                MessageBox.Show("Ошибка выбора файла");
                return;
            }
                
            using (StreamWriter writer = File.CreateText(filename))
            {
                writer.Write(json);
            }
            MessageBox.Show("Игра сохранена");
        }

        private void loadGame()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "data";
            dlg.DefaultExt = ".game";
            dlg.Filter = "Game documents (.game)|*.game";

            Nullable<bool> result = dlg.ShowDialog();

            string? filename = null;
            if (result == true)
            {
                filename = dlg.FileName;
            }
            else
            {
                MessageBox.Show("Ошибка выбора файла");
                return;
            }

            List<Figure> newFigureList = new List<Figure>();            

            string json = File.ReadAllText(filename);
            try {
                List<FigureSerializationType>? figureSerializationTypes = JsonConvert.DeserializeObject<List<FigureSerializationType>>(json);
                if (figureSerializationTypes == null)
                {
                    MessageBox.Show("В файле нет валидных данных");
                    return;
                }
                foreach (FigureSerializationType fst in figureSerializationTypes)
                {
                    Figure figure = null;
                    switch(fst.Type)
                    {
                        case "Circle":
                            figure = new Circle(fst.X, fst.Y, fst.Height, fst.Width);
                            break;
                        case "Triangle":
                            figure = new Triangle(fst.X, fst.Y, fst.Height, fst.Width);
                            break;
                        case "Square":
                            figure = new Square(fst.X, fst.Y, fst.Height, fst.Width);
                            break;
                        case "Hexagon":
                            figure = new Hexagon(fst.X, fst.Y, fst.Height, fst.Width);
                            break;
                    }

                    if (figure == null)
                        return;
                    figure.Accelerator = fst.Accelerator;
                    figure.Velocity = fst.Velocity;
                    figure.Angle = fst.Angle;
                    newFigureList.Add(figure);
                }
            }
            catch (Exception)
            {

            }

            FigureList = newFigureList;            
            GameCanvas.Children.Clear();
            
            foreach (Figure f in FigureList)
                f.Display(GameCanvas);

            MenuClass.Display(GameCanvas);
        }

        private List<FigureSerializationType> GetFigureSerializationTypes()
        {
            List<FigureSerializationType> list = new List<FigureSerializationType>();
            foreach(var figure in FigureList)
            {
                FigureSerializationType f = new FigureSerializationType();
                f.X = figure.X;
                f.Y = figure.Y;
                f.Height = figure.Height;
                f.Width = figure.Width;
                f.Velocity = figure.Velocity;
                f.Accelerator = figure.Accelerator;
                f.Angle = figure.Angle;
                f.Type = figure.GetType().Name;
                list.Add(f);
            }
            return list;
        }

        private void UpdateGame(object? sender, EventArgs e)
        {
            if (isPaused)
                return;
            foreach (var figure in FigureList)
            {
                figure.Update(GameCanvas, FigureList);
            }
            for(int i = 0; i < FigureList.Count; i++)
            {
                for(int j = i+1; j < FigureList.Count; j++)
                {
                    if (Figure.checkCollision(FigureList[i], FigureList[j]))
                    {
                        Figure.HandleCollision(FigureList[i], FigureList[j]);
                        
                        FigureList[i].AfterCollisionDisplay(GameCanvas);
                        FigureList[j].AfterCollisionDisplay(GameCanvas);
                        //FigureList[i].getShape().
                        if (isDebagging)
                        {
                            FigureList[i].setCollisionColor(Brushes.Tomato);
                            FigureList[j].setCollisionColor(Brushes.Black);
                            isPaused = true;
                        }
                    }
                }
            }
        }

        private void StartUniformMotion()
        {
            foreach (var figure in FigureList)
            {
                figure.isAccelerated = false;
            }
            timer.Start();
        }

        private void StartAcceleratedMotion()
        {
            foreach (var figure in FigureList)
            {
                figure.isAccelerated = true;
            }
            timer.Start();
        }

        private void AddRandomFigure()
        {
            Figure figure = GenerateRandomFigure();
            figure.Display(GameCanvas);
            FigureList.Add(figure);
        }
        private void RemoveRandomFigure()
        {
            if (FigureList.Count <= 0)
                return;
            int i = random.Next(0, FigureList.Count - 1);
            GameCanvas.Children.Remove(FigureList[i].getShape());
            FigureList.Remove(FigureList[i]);
        }
        private void ChangeSizeOfFigures(int dn)
        {
            if (FIGURE_SIZE + dn < 20 || FIGURE_SIZE + dn > 300)
                return;
            FIGURE_SIZE += dn;
            foreach (var figure in FigureList)
            {                
                figure.Height += dn;
                figure.Width += dn;
                //figure.AfterCollisionDisplay(GameCanvas);
            }
        }


        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (isPaused)
                if (e.Key != Key.P && e.Key != Key.F && e.Key != Key.M && e.Key != Key.D)
                    return;

            switch (e.Key)
            {
                case Key.A:
                    StartAcceleratedMotion();
                    break;
                case Key.S:
                    StartUniformMotion();
                    break;
                case Key.L:
                    ChangeSizeOfFigures(5);
                    break;
                case Key.K:
                    ChangeSizeOfFigures(-5);
                    break;
                case Key.D:
                    if (isDebagging)
                    {
                        foreach (var f in FigureList)
                            f.setDefaultColor();
                        isDebagging = false;
                    }
                    else
                        isDebagging = true;                    
                    break;
                case Key.P:
                    if (isPaused)
                    {
                        if (isDebagging)
                        {
                            foreach (var f in FigureList)
                                f.setDefaultColor();
                        }
                        timer.Start();
                        isPaused = false;
                    }
                    else
                    {
                        timer.Stop();
                        isPaused = true;
                    }
                    break;
                case Key.R:
                    GameCanvas.Children.Clear();
                    GenerateFigures();
                    break;
                case Key.M:
                    if (isPaused)
                    {
                        MenuClass.MenuVisibility = Visibility.Collapsed;
                        timer.Start();
                        isPaused = false;
                        return;
                    }
                    timer.Stop();
                    isPaused = true;                    
                    Canvas.SetZIndex(MenuClass.getGrid, 1);
                    MenuClass.setFigureCount(FigureList.Count);
                    MenuClass.setFigureSize(FIGURE_SIZE);
                    MenuClass.MenuVisibility = Visibility.Visible;                    
                    break;
                case Key.F:
                    ((MainWindow)sender).ChangeWindowState();
                    break;
                case Key.X:
                    AddRandomFigure();
                    break;
                case Key.Z:
                    RemoveRandomFigure();
                    break;
            }
        }

    }
}
