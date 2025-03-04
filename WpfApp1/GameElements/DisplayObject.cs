using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameOOTP.GameElements
{
    public abstract class DisplayObject
    {
        public virtual int Height { get; set; }
        public virtual int Width { get; set; }

        public virtual double X { get; set; }
        public virtual double Y { get; set; }

        public DisplayObject(double x, double y, int height, int width)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }

        public abstract DisplayObject Display(Canvas canvas);
    }
}
