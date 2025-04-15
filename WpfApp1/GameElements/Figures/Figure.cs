using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOOTP.GameElements.Figures
{
    public abstract class Figure : DisplayObject, IMoveable
    {
        protected double velocity;
        protected double accelerator;
        protected double angle;       

        public bool isAccelerated = false;

        protected Shape Shape;

        public Figure(double x, double y, int height, int width) : base(x, y, height, width) { 
            
            
        }

        public Shape getShape() => Shape;

        public double Velocity { get => velocity; set => velocity = value; }
        public double Accelerator { get => accelerator; set => accelerator = value; }
        public double Angle { get => angle; set => angle = value; }
        public override int Height
        {
            get => base.Height;
            set
            {
                base.Height = value;
                if (Shape != null)
                    Shape.Height = value;
            }
        }

        public override int Width 
        {
            get => base.Width;
            set
            {
                base.Width = value;
                if (Shape != null)
                    Shape.Width = value;
            }
        }

        private void Move()
        {
            if (isAccelerated)
                velocity += accelerator;
            X += Math.Cos(Angle) * velocity;
            Y += Math.Sin(Angle) * velocity;
        }

        public void Bounce(Canvas canvas, List<Figure> figures)
        {
            if (X <= 0 || X >= canvas.Width - Width)
            {
                if (X > canvas.Width - Width)
                    X = canvas.Width - Width;
                if (X < 0)
                    X = 0;
                angle = Math.PI - angle;
            }


            if (Y <= 0 || Y >= canvas.Height - Height)
            {
                if (Y > canvas.Height - Height)
                    Y = canvas.Height - Height;
                if (Y < 0)
                    Y = 0;
                angle = -angle;
            }
        }

        public static bool checkCollision(Figure shape1, Figure shape2)
        {
            if (shape1.Equals(shape2))
                return false;

            Point center1= new Point(shape1.X + shape1.Width / 2, shape1.Y + shape1.Height / 2);
            Point center2 = new Point(shape2.X + shape2.Width / 2, shape2.Y + shape2.Height / 2);
            double dx = center2.X - center1.X;
            double dy = center2.Y - center1.Y;
            double dist = Math.Sqrt(dx * dx + dy * dy);

            if (dist <= shape1.Width/2 + shape2.Width / 2)
                return true;
            return false;
        } 

        public static void HandleCollision(Figure shape1, Figure shape2)
        {
            Point center1 = new Point(shape1.X + shape1.Width / 2, shape1.Y + shape1.Height / 2);
            Point center2 = new Point(shape2.X + shape2.Width / 2, shape2.Y + shape2.Height / 2);

            double dx = center2.X - center1.X;
            double dy = center2.Y - center1.Y;
            double d = (double)Math.Sqrt(dx * dx + dy * dy);
            double nx = dx / d;
            double ny = dy / d;

            double xVel1 = shape1.Velocity * Math.Cos(shape1.Angle);
            double yVel1 = shape1.Velocity * Math.Sin(shape1.Angle);
            double xVel2 = shape2.Velocity * Math.Cos(shape2.Angle);
            double yVel2 = shape2.Velocity * Math.Sin(shape2.Angle);

            double a1 = xVel1 * nx + yVel1 * ny;
            double a2 = xVel2 * nx + yVel2 * ny;
            double mass1 = shape1.Height * shape1.Height;
            double mass2 = shape2.Height * shape2.Height;

            double p = 2 * (a1 - a2) / (mass1 + mass2);
            xVel1 = xVel1 - p * nx * mass2;
            yVel1 = yVel1 - p * ny * mass2;
            xVel2 = xVel2 + p * nx * mass1;
            yVel2 = yVel2 + p * ny * mass1;

            shape1.angle = Math.Atan2(yVel1, xVel1);
            shape2.angle = Math.Atan2(yVel2, xVel2);

            shape1.velocity = Math.Sqrt(xVel1 * xVel1 + yVel1 * yVel1);
            shape2.velocity = Math.Sqrt(xVel2 * xVel2 + yVel2 * yVel2);


            double overlap = (shape1.Width / 2 + shape2.Width / 2) - d;


            // Раздвигаем объекты по направлению столкновения
            shape1.X -= nx * overlap / 2;
            shape1.Y -= ny * overlap / 2;

            shape2.X += nx * overlap / 2;
            shape2.Y += ny * overlap / 2;

        }

        
        public void Update(Canvas canvas, List<Figure> figures)
        {
            Move();
            Bounce(canvas, figures);
            Canvas.SetTop(Shape, Y);
            Canvas.SetLeft(Shape, X);
        }

        public void AfterCollisionDisplay(Canvas canvas)
        {
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

        public void setCollisionColor(SolidColorBrush color)
        {
            Shape.Fill = color;
        }

        public abstract void setDefaultColor();

    }
}



/*

 
 */









/*
 
 // bounce for all
if (((X > shape2.X && X < shape2.X + shape2.Width) ||
                    (X + Width > shape2.X && X + Width < shape2.X + shape2.Width)) &&
                    ((Y > shape2.Y && Y < shape2.Y + shape2.Height) ||
                    (Y > shape2.Y && Y < shape2.Y + shape2.Height)))
                {
                    if (center1.X - ((shape2.X + shape2.Width) / 2) >= center1.Y - ((shape2.Y + shape2.Height) / 2))
                    {
                        if ((Math.Cos(angle) <= 0 && Math.Cos(shape2.angle) <= 0))
                        {
                            if (X < shape2.X)
                                shape2.angle = Math.PI - shape2.angle;
                            else
                                angle = Math.PI - angle;
                        }
                        else if (Math.Cos(angle) >= 0 && Math.Cos(shape2.angle) >= 0)
                        {
                            if (X > shape2.X)
                                shape2.angle = Math.PI - shape2.angle;
                            else
                                angle = Math.PI - angle;
                        }
                        else
                        {
                            angle = Math.PI - angle;
                            shape2.angle = Math.PI - shape2.angle;
                        }

                        if (X < shape2.X)
                        {
                            double dx = X + Width - shape2.X;
                            X -= dx / 2;
                            shape2.X += dx / 2;
                        }
                        else
                        {
                            double dx = shape2.X + shape2.Width - X;
                            shape2.X -= dx / 2;
                            X += dx / 2;
                        }
                    }
                    else
                    {
                        if ((Math.Sin(angle) <= 0 && Math.Sin(shape2.angle) <= 0))
                        {
                            if (Y < shape2.Y)
                                shape2.angle = -shape2.angle;
                            else
                                angle = -angle;
                        }
                        else if (Math.Sin(angle) >= 0 && Math.Sin(shape2.angle) >= 0)
                        {
                            if (Y > shape2.Y)
                                shape2.angle = -shape2.angle;
                            else
                                angle = -angle;
                        }
                        else
                        {
                            angle = -angle;
                            shape2.angle = -shape2.angle;
                        }

                        if (Y < shape2.Y)
                        {
                            double dy = Y + Height - shape2.Y;
                            Y -= dy / 2;
                            shape2.Y += dy / 2;
                        }
                        else
                        {
                            double dy = shape2.Y + shape2.Height - Y;
                            Y += dy / 2;
                            shape2.Y -= dy / 2;
                        }
                    }
                }
  
 */


/*
 
                    if (collisionAngle < Math.PI / 2)
                    {
                       
                        Point collisionPoint = new Point(center1.X + Height * Math.Sin(collisionAngle), center1.Y + Height * Math.Cos(collisionAngle));
                        X = collisionPoint.X - Height / 2 - Height * Math.Cos(collisionAngle);
                        Y = collisionPoint.Y - Height / 2 - Height * Math.Sin(collisionAngle);

                        shape2.X = collisionPoint.X + Height / 2 - Height * Math.Cos(collisionAngle);
                        shape2.Y = collisionPoint.Y + Height / 2 - Height * Math.Sin(collisionAngle);
                    }
                    else if (collisionAngle < Math.PI)
                    {
                        Point collisionPoint = new Point(center1.X + Height * Math.Sin(collisionAngle), center1.Y + Height * Math.Cos(collisionAngle));
                        X = collisionPoint.X + Height / 2 - Height * Math.Cos(collisionAngle);
                        Y = collisionPoint.Y - Height / 2 - Height * Math.Sin(collisionAngle);

                        shape2.X = collisionPoint.X - Height / 2 - Height * Math.Cos(collisionAngle);
                        shape2.Y = collisionPoint.Y + Height / 2 - Height * Math.Sin(collisionAngle);
                    }
                    if (collisionAngle < 3 * Math.PI / 2)
                    {
                        Point collisionPoint = new Point(center1.X + Height * Math.Sin(collisionAngle), center1.Y + Height * Math.Cos(collisionAngle));
                        X = collisionPoint.X + Height / 2 - Height * Math.Cos(collisionAngle);
                        Y = collisionPoint.Y + Height / 2 - Height * Math.Sin(collisionAngle);

                        shape2.X = collisionPoint.X - Height / 2 - Height * Math.Cos(collisionAngle);
                        shape2.Y = collisionPoint.Y - Height / 2 - Height * Math.Sin(collisionAngle);
                    }
                    else
                    {
                        Point collisionPoint = new Point(center1.X + Height * Math.Sin(collisionAngle), center1.Y + Height * Math.Cos(collisionAngle));
                        X = collisionPoint.X - Height / 2 - Height * Math.Cos(collisionAngle);
                        Y = collisionPoint.Y + Height / 2 - Height * Math.Sin(collisionAngle);

                        shape2.X = collisionPoint.X + Height / 2 - Height * Math.Cos(collisionAngle);
                        shape2.Y = collisionPoint.Y - Height / 2 - Height * Math.Sin(collisionAngle);
                    }
 
 
 
 */


/*
 
 angle = -angle;
                        shape2.angle = -shape2.angle;
                        if (center1.X < center2.X)
                        {
                            Point p1 = new Point(center1.X + r1 * Math.Cos(collisionAngle), center1.Y + r1 * Math.Sin(collisionAngle));
                            Point p2 = new Point(center2.X - r1 * Math.Cos(collisionAngle), center2.Y - r1 * Math.Sin(collisionAngle));
                            Point collisionPoint = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                            X = collisionPoint.X - r1 - r1 * Math.Cos(collisionAngle);
                            Y = collisionPoint.Y - r1 - r1 * Math.Sin(collisionAngle);

                            shape2.X = collisionPoint.X - r1 + r1 * Math.Cos(collisionAngle);
                            shape2.Y = collisionPoint.Y - r1 + r1 * Math.Sin(collisionAngle);
                        }
                        else
                        {
                            Point p1 = new Point(center1.X - r1 * Math.Cos(collisionAngle), center1.Y - r1 * Math.Sin(collisionAngle));
                            Point p2 = new Point(center2.X + r1 * Math.Cos(collisionAngle), center2.Y + r1 * Math.Sin(collisionAngle));
                            Point collisionPoint = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);                            
                            X = collisionPoint.X + r1 - r1 * Math.Cos(collisionAngle);
                            Y = collisionPoint.Y + r1 - r1 * Math.Sin(collisionAngle);

                            shape2.X = collisionPoint.X - r1 - r1 * Math.Cos(collisionAngle);
                            shape2.Y = collisionPoint.Y - r1 - r1 * Math.Sin(collisionAngle);
                        }                            
                    }
                    else 
                    {
                        angle = Math.PI - angle;
                        shape2.angle = Math.PI - shape2.angle;
                        if (center1.X > center2.X)
                        {
                            Point p1 = new Point(center1.X - r1 * Math.Cos(collisionAngle), center1.Y - r1 * Math.Sin(collisionAngle));
                            Point p2 = new Point(center2.X + r1 * Math.Cos(collisionAngle), center2.Y + r1 * Math.Sin(collisionAngle));
                            Point collisionPoint = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);                            
                            X = collisionPoint.X - r1 + r1 * Math.Cos(collisionAngle);
                            Y = collisionPoint.Y - r1 + r1 * Math.Sin(collisionAngle);

                            shape2.X = collisionPoint.X - r1 - r1 * Math.Cos(collisionAngle);
                            shape2.Y = collisionPoint.Y - r1 + r1 * Math.Sin(collisionAngle);
                        }
                        else
                        {
                            Point p1 = new Point(center1.X + r1 * Math.Cos(collisionAngle), center1.Y + r1 * Math.Sin(collisionAngle));
                            Point p2 = new Point(center2.X - r1 * Math.Cos(collisionAngle), center2.Y - r1 * Math.Sin(collisionAngle));
                            Point collisionPoint = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
                            X = collisionPoint.X - r1 - r1 * Math.Cos(collisionAngle);
                            Y = collisionPoint.Y - r1 + r1 * Math.Sin(collisionAngle);

                            shape2.X = collisionPoint.X - r1 + r1 * Math.Cos(collisionAngle);
                            shape2.Y = collisionPoint.Y - r1 + r1 * Math.Sin(collisionAngle);
                        }
 
 
 */