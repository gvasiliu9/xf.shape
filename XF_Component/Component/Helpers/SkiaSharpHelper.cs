using System;
using System.IO;
using System.Reflection;
using Component.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Component.Helpers
{
    public static class SkiaSharpHelper
    {
        /// <summary>
        /// Load embeded font
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        public static SKTypeface LoadTtfFont(string fontName)
        {
            var assembly = typeof(SkiaSharpHelper).GetTypeInfo().Assembly;
            var file = $"Component.Resources.Fonts.{fontName}.ttf";

            using (var resource = assembly.GetManifestResourceStream(file))
            {
                using (var memory = new MemoryStream())
                {
                    resource?.CopyTo(memory);

                    var bytes = memory?.ToArray();

                    using (var stream = new SKMemoryStream(bytes))
                    {
                        return SKTypeface.FromStream(stream);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static float GetPercentage(float input, Range range)
        {
            float x = range.To - range.From;

            float result = (100 * (input - range.From)) / x;

            return result / 100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="canvasView"></param>
        /// <returns></returns>
        public static SKPoint ToPixel(float x, float y, ref SKCanvasView canvasView)
        {
            return new SKPoint((float)Math.Round((canvasView.CanvasSize.Width * x / canvasView.Width))
                , (float)Math.Round((canvasView.CanvasSize.Height * y / canvasView.Height)));
        }
    }
}
