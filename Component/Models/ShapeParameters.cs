using System;
namespace Utmdev.Xf.Shape.Models
{
    public class ShapeParameters
    {
        public ShapeParameters(float topLeft, float topRight, float bottomRight, float bottomLeft)
        {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
        }

        public float TopLeft { get; set; }

        public float TopRight { get; set; }

        public float BottomRight { get; set; }

        public float BottomLeft { get; set; }
    }
}
