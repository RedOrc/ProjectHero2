using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.VSMath
{
    public static class Mathlib
    {
        public static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public static double ToDegrees(double radians)
        {
            return radians * (180 / Math.PI);
        }

        public static bool IsRECTOutOfBounds(Rectangle srcRect, Rectangle destRect)
        {
            return
                (srcRect.X < destRect.X) ||
                (srcRect.Y < destRect.Y) ||
                ((srcRect.X + srcRect.Width) > (destRect.X + destRect.Width)) ||
                ((srcRect.Y + srcRect.Height) > (destRect.Y + destRect.Height));
        }

        /// <summary>
        /// Returns true if the source (src) rectangle is within the boundaries
        /// of a circle and at any angle of the circle.
        /// </summary>
        /// <param name="srcRect"></param>
        /// <param name="arcCenterX"></param>
        /// <param name="arcCenterY"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static bool IsRECTOutOfBounds(Rectangle srcRect, double arcCenterX, double arcCenterY, double radius)
        {
            bool bIsOutOfBounds = false;

            // Define all 4 side points of the rectangle.
            Vector2D vSrc1 = new Vector2D(srcRect.X, srcRect.Y);
            Vector2D vSrc2 = new Vector2D(srcRect.X + srcRect.Width, srcRect.Y);
            Vector2D vSrc3 = new Vector2D(srcRect.X, srcRect.Y + srcRect.Height);
            Vector2D vSrc4 = new Vector2D(srcRect.X + srcRect.Width, srcRect.Y + srcRect.Height);

            // By creating a vector on the center point of the rectangle
            // we can greatly reduce our calculations.
            Vector2D vSrcCenter = new Vector2D(srcRect.X + (srcRect.Width / 2), srcRect.Y + (srcRect.Height / 2));

            // Define the center point of the arc.
            Vector2D vArc = new Vector2D(arcCenterX, arcCenterY);

            // Get angles that each point in the rectangle is at relationship
            // to the center point of the arc so we can determine if the point is
            // within the boundaries or not.
            // ONLY DEFINED FOR DEBUGGING PURPOSES.
            double angSrc1 = vSrc1.GetAngleFromClockwise(vArc);
            double angSrc2 = vSrc2.GetAngleFromClockwise(vArc);
            double angSrc3 = vSrc3.GetAngleFromClockwise(vArc);
            double angSrc4 = vSrc4.GetAngleFromClockwise(vArc);
            double angSrcCenter = vSrcCenter.GetAngleFromClockwise(vArc);

            // Calculate which points are at the corresponding angles so we know whether or not
            // we would be out of bounds with the source rectangle.
            Vector2D vDest1 = Vector2D.GetXYFromAngle(arcCenterX, arcCenterY, radius, angSrc1);
            Vector2D vDest2 = Vector2D.GetXYFromAngle(arcCenterX, arcCenterY, radius, angSrc2);
            Vector2D vDest3 = Vector2D.GetXYFromAngle(arcCenterX, arcCenterY, radius, angSrc3);
            Vector2D vDest4 = Vector2D.GetXYFromAngle(arcCenterX, arcCenterY, radius, angSrc4);

#if NDEBUG
            Debug.Print(string.Format("Source = 1:{0}, 2:{1}, 3:{2}, 4:{3}", angSrc1, angSrc2, angSrc3, angSrc4));
#endif
            /*
             * Based on where the point is lets determine if any of its sides are out of bounds.
             * 
             * (vSrc1) <-------------------> (vSrc2)
             *         |                   |
             *         |                   |
             *         |                   |
             *         |                   |
             * (vSrc3) <-------------------> (vSrc4)
             * 
             * The small diagram above is a representation of a rectangle
             * along with a label for each corner so you can understand what
             * I am referring to in my calculations.
             */
            if (angSrcCenter <= 90)
            {
                bIsOutOfBounds = (vSrc2.X > vDest2.X) || (vSrc4.X > vDest4.X) || (vSrc3.Y > vDest3.Y);
            }
            else if (angSrcCenter > 90 && angSrcCenter <= 180)
            {
                bIsOutOfBounds = (vSrc1.X < vDest1.X) || (vSrc3.X < vDest3.X) || (vSrc3.Y > vDest3.Y) || (vSrc4.Y > vDest4.Y);
            }
            else if (angSrcCenter > 180 && angSrcCenter <= 270)
            {
                bIsOutOfBounds = (vSrc1.X < vDest1.X) || (vSrc1.Y < vDest1.Y) || (vSrc3.X < vDest3.X) || (vSrc2.Y < vDest2.Y);
            }
            else if (angSrcCenter > 270 && angSrcCenter <= 360)
            {
                bIsOutOfBounds = (vSrc1.Y < vDest1.Y) || (vSrc2.X > vDest2.X) || (vSrc2.Y < vDest2.Y) || (vSrc4.X > vDest4.X);
            }
            else if (angSrcCenter == 0)
            {
                bIsOutOfBounds = (vSrc2.X > vDest2.X) || (vSrc4.X > vDest4.X);
            }

            return bIsOutOfBounds;
        }

        public static int GetAngleFromXY(int x, int y, int centerX, int centerY)
        {
            double angle = Math.Atan((x - centerX) / (centerY - y)) * (180 / Math.PI);
            if (x > centerX && y > centerY)
                angle = (90 + angle) + 90;
            else if (x < centerX && y > centerY)
                angle = angle + 180;
            else if (x < centerX && y < centerY)
                angle = (90 + angle) + 270;

            return (int)angle;
        }
    }
}
