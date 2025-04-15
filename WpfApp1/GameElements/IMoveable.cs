using GameOOTP.GameElements.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameOOTP.GameElements
{
    interface IMoveable
    {
        public double Velocity { get; set; }
        public double Accelerator { get; set; }
        public double Angle { get; set; }

        public void Bounce(Canvas canvas, List<Figure> figures);
        public void Update(Canvas canvas, List<Figure> figures);
    }
}
