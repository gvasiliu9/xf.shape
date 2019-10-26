using System;
using Xamarin.Forms;

namespace Utmdev.Xf.Shape.Models
{
    public class ShapeGradient
    {
        public Color Start { get; set; }

        public Color End { get; set; }

        public float Degrees { get; set; }

        public float StartPosition { get; set; }

        public float EndPosition { get; set; } = 1;
    }
}
