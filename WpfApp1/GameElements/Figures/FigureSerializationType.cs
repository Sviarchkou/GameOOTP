using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GameOOTP.GameElements.Figures
{
    public class FigureSerializationType
    {
        public string Type { get; set; }
        public double Velocity { get ; set; }
        public double Accelerator { get; set; }
        public double Angle { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        
    }
}
