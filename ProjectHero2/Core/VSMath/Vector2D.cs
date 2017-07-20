using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.VSMath
{
    public class Vector2D
    {
        #region Properties

        public double X { get; set; }
        public double Y { get; set; }

        /// <summary>
        /// Get whether or not this is a normalized Unit Vector.
        /// </summary>
        public bool IsNormalized { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initialize a new instance of the Vector2D Class.
        /// </summary>
        public Vector2D()
        {
            this.X = 0;
            this.Y = 0;
        }

        /// <summary>
        /// Initialize a new instance of the Vector2D Class.
        /// </summary>
        /// <param name="x">Set the X coordinate.</param>
        /// <param name="y">Set the Y coordinate.</param>
        public Vector2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Returns the current theta (facing) of this vector in Radians.
        /// </summary>
        /// <returns></returns>
        public double GetThetaAsRadian()
        {
            return Math.Atan(this.Y / this.X);
        }

        /// <summary>
        /// Returns the current theta (facing) of this vector in Degrees.
        /// </summary>
        /// <returns></returns>
        public double GetThetaAsDegree()
        {
            double angle = GetThetaAsRadian();
            return Mathlib.ToDegrees(angle);
        }

        /// <summary>
        /// Returns the total length of this Vector.
        /// </summary>
        /// <returns></returns>
        public double GetLength()
        {
            return Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2));
        }

        /// <summary>
        /// Gets the angle this vector is at in relationship to another Vector.
        /// Take note that 0 degrees increases clockwise.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public double GetAngleFromClockwise(Vector2D src)
        {
            double angle = 0.0d;

            // We are within 0° - 180°.
            if (this.Y > src.Y)
            {
                // We are in Quadrant I (0° - 90°).
                if (this.X > src.X)
                {
                    angle = (double)(this.X - src.X) / (double)(this.Y - src.Y);
                    angle = Math.Atan(angle);
                    angle = 90 - Mathlib.ToDegrees(angle);
                }
                else if (this.X < src.X)
                {
                    // We are in Quadrant II (91° - 180°).
                    angle = (double)(src.X - this.X) / (double)(this.Y - src.Y);
                    angle = Math.Atan(-angle);
                    angle = 90 - Mathlib.ToDegrees(angle);
                }
                else if (this.X == src.X)
                {
                    // We are between Quadrant I and II (90°).
                    angle = 90;
                }
            }
            else if (this.Y < src.Y)
            {
                // We are in Quadrant III (180° - 270°).
                if (this.X < src.X)
                {
                    angle = (double)(src.X - this.X) / (double)(src.Y - this.Y);
                    angle = Math.Atan(angle);
                    angle = 270 - Mathlib.ToDegrees(angle);
                }
                // We are in Quadrant IV (271° - 360°).
                else if (this.X > src.X)
                {
                    angle = (double)(this.X - src.X) / (double)(src.Y - this.Y);
                    angle = Math.Atan(-angle);
                    angle = 270 - Mathlib.ToDegrees(angle);
                }
                // We are between Quadrant III and IV (270°).
                else if (this.X == src.X)
                {
                    angle = 270;
                }
            }
            else if (this.Y == src.Y)
            {
                // We are at 180°.
                if (this.X < src.X)
                {
                    angle = 180;
                }
                // We are at 0° or 360°.
                else if (this.X > src.X)
                {
                    angle = 0;
                }
            }

            return angle;
        }

        /// <summary>
        /// Gets the angle this vector is at in relationship to another Vector.
        /// Take note that 0 degrees increases counter-clockwise.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public double GetAngleFromCounterClockwise(Vector2D src)
        {
            double angle = 0.0d;

            // We are within 0° - 180°.
            if (this.Y < src.Y)
            {
                // We are in Quadrant I (0° - 90°).
                if (this.X > src.X)
                {
                    angle = (double)(this.X - src.X) / (double)(src.Y - this.Y);
                    angle = Math.Atan(angle);
                    angle = 90 - Mathlib.ToDegrees(angle);
                }
                // We are in Quadrant II (91° - 180°).
                else if (this.X < src.X)
                {
                    angle = (double)(src.X - this.X) / (double)(src.Y - this.Y);
                    angle = Math.Atan(-angle);
                    angle = 90 - Mathlib.ToDegrees(angle);
                }
                // We are between Quadrant I and II (90°).
                else if (this.X == src.X)
                {
                    angle = 90;
                }
            }
            // We are within 180° - 360°.
            else if (this.Y > src.Y)
            {
                // We are in Quadrant III (180° - 270°).
                if (this.X > src.X)
                {
                    angle = (double)(this.X - src.X) / (double)(this.Y - src.Y);
                    angle = Math.Atan(-angle);
                    angle = 270.0 - Mathlib.ToDegrees(angle);
                }
                // We are in Quadrant IV (271° - 360°).
                else if (this.X < src.X)
                {
                    angle = (double)(src.X - this.X) / (double)(this.Y - src.Y);
                    angle = Math.Atan(angle);
                    angle = 270.0 - Mathlib.ToDegrees(angle);
                }
                // We are between Quadrant III and IV (270°).
                else if (this.X == src.X)
                {
                    angle = 270.0;
                }
            }
            // We are at either halfway points of a circle.
            else if (this.Y == src.Y)
            {
                // We are at 0° or 360°.
                if (this.X > src.X)
                {
                    angle = 0;
                }
                // We are at 180°.
                else if (this.X < src.X)
                {
                    angle = 180;
                }
            }
            return angle;
        }

        /// <summary>
        /// Rotates this vector around another vector.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="angleToRotateTo"></param>
        /// <param name="distance"></param>
        public void RotateAround(Vector2D src, double angleToRotateTo, double distance)
        {
            /*
             * Not needed so function definition removed.
             * 
             * double newX = distance * Math.Cos(angleToRotateTo * Math.PI / 180);
             * double newY = distance * Math.Sin(angleToRotateTo * Math.PI / 180);
             */
        }

        /// <summary>
        /// Scales this vector by the specified scale factor on both axis.
        /// </summary>
        /// <param name="scaleFactor">The scale factor to increase this vector by.</param>
        public void Scale(double scaleFactor)
        {
            this.X = scaleFactor * this.X;
            this.Y = scaleFactor * this.Y;
        }

        /// <summary>
        /// Scales this vector along the X axis by the specified scale factor.
        /// </summary>
        /// <param name="scaleFactor">The scale factor to increase this vector by.</param>
        public void ScaleX(double scaleFactor)
        {
            this.X = scaleFactor * this.X;
        }

        /// <summary>
        /// Scales this vector along the Y axis by the specified scale factor.
        /// </summary>
        /// <param name="scaleFactor">The scale factor to increase this vector by.</param>
        public void ScaleY(double scaleFactor)
        {
            this.Y = scaleFactor * this.Y;
        }

        /// <summary>
        /// Normalizes the vector converting it into a Unit Vector with a length of 1.
        /// </summary>
        public void NormalizeVector()
        {
            if (X == 0 && Y == 0)
                return;

            double length = GetLength();
            double x = this.X / length;
            double y = this.Y / length;
            double factor = Math.Round(Math.Sqrt((x * x) + (y * y)));

            IsNormalized = ((int)factor) == 1;
            if (IsNormalized)
            {
                this.X = x;
                this.Y = y;
            }
        }

        #endregion Methods

        #region Static Methods

        /// <summary>
        /// Scales a vector by the specified scale factor on both axis.
        /// </summary>
        /// <param name="scaleFactor">The scale factor to increase the vector by.</param>
        /// <returns></returns>
        public static Vector2D Scale(Vector2D src, double scaleFactor)
        {
            return new Vector2D(scaleFactor * src.X, scaleFactor * src.Y);
        }

        /// <summary>
        /// Returns the (X,Y) coordinates for a vector based on the length
        /// and the angle that the length is point towards.
        /// </summary>
        /// <param name="centerX">The center point of the X coordinate in the circle.</param>
        /// <param name="centerY">The center point of the Y coordinate in the circle.</param>
        /// <param name="length">The radius of the circle in question.</param>
        /// <param name="angle">The angle that the vector is pointing towards.</param>
        /// <returns></returns>
        public static Vector2D GetXYFromAngle(double centerX, double centerY, double length, double angle)
        {
            double r = angle * (Math.PI / 180);
            double x = ((length * Math.Cos(r)) + length);
            double y = ((length * Math.Sin(r)) + length);
            return new Vector2D(x, y);
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D(b.X - a.X, b.Y - a.Y);
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return new Vector2D(b.X + a.X, b.Y + a.Y);
        }

        public static Vector2D operator *(Vector2D a, Vector2D b)
        {
            return new Vector2D(b.X * a.X, b.Y * a.Y);
        }

        public static Vector2D MoveToVector(Vector2D src, Vector2D dst)
        {
            return new Vector2D(dst.X - src.X, dst.Y - src.Y);
        }

        #endregion Static Methods
    }
}
