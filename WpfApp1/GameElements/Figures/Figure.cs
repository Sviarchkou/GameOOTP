using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace GameOOTP.GameElements.Figures
{
    public abstract class Figure : DisplayObject, IMoveable
    {
        protected double velocity;
        protected double accelerator;
        protected double angle;

        Type type;        

        private bool isAccelerated = false;
        private bool isOutOfXRange = false;
        private bool isOutOfYRange = false;

        protected Shape Shape;

        public Figure(double x, double y, int height, int width) : base(x, y, height, width) { 
            
            
        }

        public Shape getShape() => Shape;

        public double Velocity { get => velocity; set => velocity = value; }
        public double Accelerator { get => accelerator; set => accelerator = value; }
        public double Angle { get => angle; set => angle = value; }

        private void Move()
        {
            X += Math.Cos(Angle) * velocity;
            Y += Math.Sin(Angle) * velocity;
        }

        public void MoveAccelerated()
        {
            isAccelerated = true;
            velocity += accelerator;
            Move();
        }

        public void MoveAccelerated(int acelerator)
        {
            Accelerator = accelerator;
            MoveAccelerated();
        }

        public void MoveUniformly()
        {
            isAccelerated = false;
            Move();
        }

        public void MoveUniformly(int velocity)
        {
            isAccelerated = false;
            Velocity = velocity;
            Move();
        }

        public void Bounce(Canvas canvas)
        {
            if (X <= 0 || X >= canvas.Width - Width)
            {
                if (X > canvas.Width - Width)
                    X = canvas.Width - Width;

                if (!isOutOfXRange)
                {
                    angle = Math.PI - angle;
                    isOutOfXRange = true;
                }
            }
            else
                isOutOfXRange = false;


            if (Y <= 0 || Y >= canvas.Height - Height)
            {
                if (Y > canvas.Height - Height)
                    Y = canvas.Height - Height;
                if (!isOutOfYRange)
                {
                    angle = -angle;
                    isOutOfYRange = true;
                }

            }
            else
                isOutOfYRange = false;
            /*
            if (X <= 0 || X >= canvas.Width - Width)
            {
                if (!isOutOfXRange)
                {
                    angle = Math.PI - angle;
                    isOutOfXRange = true;
                }
            }
            else
                isOutOfXRange = false;

            if (Y <= 0 || Y >= canvas.Height - Height)
            {
                if (!isOutOfYRange)
                {
                    angle = -angle;
                    isOutOfYRange = true;
                }

            }
            else
                isOutOfYRange = false;*/
        }

        public void Update(Canvas canvas)
        {
            if (isAccelerated)
                MoveAccelerated();
            else
                MoveUniformly();
            Bounce(canvas);
            Canvas.SetTop(Shape, Y);
            Canvas.SetLeft(Shape, X);
        }

        public override DisplayObject Display(Canvas canvas)
        {
            Canvas.SetTop(Shape, Y);
            Canvas.SetLeft(Shape, X);
            canvas.Children.Add(Shape);
            return this;
        }

    }
}
