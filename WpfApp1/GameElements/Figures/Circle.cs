using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOOTP.GameElements.Figures
{
    public class Circle : Figure
    {
        public Circle(double x, double y, int height, int width) : base(x, y, height, width)
        {
            Shape = new Ellipse()
            {
                Height = height,
                Width = width,
                Fill = Brushes.Navy
            };
        }

        public override void setDefaultColor()
        {
            Shape.Fill = Brushes.Navy;
        }

    }
}
