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
        public override void setDefaultColor()
        {
            Shape.Fill = Brushes.Green;
        }
    }
}
