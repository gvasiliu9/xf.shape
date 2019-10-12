using System;
using SkiaSharp;

namespace Utmdev.Xf.Components.Models
{
    public class CanvasInfo
    {
        public SKImageInfo ImageInfo { get; set; }

        public SKSurface Surface { get; set; }

        public SKCanvas Canvas { get; set; }
    }
}
