using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOOTP.GameElements.Figures
{
    public class Triangle : Figure
    {
        public Triangle(double x, double y, int height, int width) : base(x, y, height, width)
        {
            Shape = new Polygon()
            {
                Points = { new Point(0, height), new Point(width / 2, 0), new Point(width, height) },
                Height = height,
                Width = width,
                Fill = Brushes.Green,
            };
        }
        public override string ToString()
        {
            return $"Triangle;{X};{Y};{Height};{Width};{Velocity};{Accelerator};{Angle}";
        }

        public Triangle getInstanceFromFileString(string str)
        {
            string[] strs = str.Split(";");
            X = double.Parse(strs[1]);
            Y = double.Parse(strs[2]);
            Height = int.Parse(strs[3]);
            Width = int.Parse(strs[4]);
            Velocity = double.Parse(strs[5]);
            Accelerator = double.Parse(strs[6]);
            Angle = double.Parse(strs[7]);
            return this;
        }
    }
}
