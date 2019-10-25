using System;
using Utmdev.Xf.Shape.Models;
using Xamarin.Forms.Xaml;

namespace Utmdev.Xf.Shape.MarkupExtensions
{
    public class ShapeParametersExtension : IMarkupExtension<ShapeParameters>
    {
        public float TopLeft { get; set; }

        public float TopRight { get; set; }

        public float BottomRight { get; set; }

        public float BottomLeft { get; set; }

        public ShapeParameters ProvideValue(IServiceProvider serviceProvider)
        {
            return new ShapeParameters(TopLeft, TopRight, BottomRight, BottomLeft);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<ShapeParameters>).ProvideValue(serviceProvider);
        }
    }
}
