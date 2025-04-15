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
    public class Hexagon : Figure
    {
        public Hexagon(double x, double y, int height, int width) : base(x, y, height, width)
        {
            Shape = new Polygon()
            {
                Points = {
                    new Point(0, height / 4),
                    new Point(0, height * 3 / 4),
                    new Point(width / 2, height),
                    new Point(width, height * 3 / 4),
                    new Point(width, height / 4),
                    new Point(width / 2, 0)
                },
                Height = height,
                Width = width,
                Fill = Brushes.Purple,
            };
        }

        public override void setDefaultColor()
        {
            Shape.Fill = Brushes.Purple;
        }
    }
}
