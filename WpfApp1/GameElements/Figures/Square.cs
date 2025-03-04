using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOOTP.GameElements.Figures
{
    public class Square : Figure
    {
        public Square(double x, double y, int height, int width) : base(x, y, height, width)
        {
            Shape = new Rectangle()
            {
                Height = height,
                Width = width,
                Fill = Brushes.Red
            };
        }
        public override string ToString()
        {
            return $"Square;{X};{Y};{Height};{Width};{Velocity};{Accelerator};{Angle}";
        }
    }
}
