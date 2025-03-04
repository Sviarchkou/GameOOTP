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

        public void MoveUniformly();
        public void MoveUniformly(int velocity);
        public void MoveAccelerated();
        public void MoveAccelerated(int acelerator);
        public void Bounce(Canvas canvas);
        public void Update(Canvas canvas);
    }
}
