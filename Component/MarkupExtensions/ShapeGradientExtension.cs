using System;
using Utmdev.Xf.Shape.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Utmdev.Xf.Shape.MarkupExtensions
{
    public class ShapeGradientExtension : IMarkupExtension<ShapeGradient>
    {
        public Color Start { get; set; }

        public Color End { get; set; }

        public float Degrees { get; set; }

        public float StartPosition { get; set; }

        public float EndPosition { get; set; }

        public ShapeGradient ProvideValue(IServiceProvider serviceProvider)
        {
            return new ShapeGradient
            {
                Start = Start,
                End = End,
                Degrees = Degrees,
                StartPosition = StartPosition,
                EndPosition = EndPosition,
            };
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<ShapeParameters>).ProvideValue(serviceProvider);
        }
    }
}
