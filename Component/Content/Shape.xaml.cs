using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SkiaSharp;
using Utmdev.Xf.Components.Models;
using Utmdev.Xf.Shape.Models;
using Xamarin.Forms;
using static Utmdev.Xf.Shape.Models.Enums;

namespace Utmdev.Xf.Shape.Content
{
    public partial class Shape : ContentView
    {
        #region Fields

        private CanvasInfo _canvasInfo;

        // Paints
        private SKPaint _shapePaint;

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty ParametersProperty = BindableProperty
            .Create(nameof(Parameters),
            typeof(ShapeParameters),
            typeof(Shape),
            new ShapeParameters(5, 5, 5, 5));

        public ShapeParameters Parameters
        {
            get
            {
                return (ShapeParameters)GetValue(ParametersProperty);
            }
            set
            {
                SetValue(ParametersProperty, value);
            }
        }

        public static readonly BindableProperty TypeProperty = BindableProperty
            .Create(nameof(Type),
            typeof(ShapeType),
            typeof(Shape),
            default(ShapeType));

        public ShapeType Type
        {
            get
            {
                return (ShapeType)GetValue(TypeProperty);
            }
            set
            {
                SetValue(TypeProperty, value);
            }
        }

        public static readonly BindableProperty SectionProperty = BindableProperty
            .Create(nameof(Section),
            typeof(View),
            typeof(Shape),
            default(View));

        public View Section
        {
            get
            {
                return (View)GetValue(SectionProperty);
            }
            set
            {
                SetValue(SectionProperty, value);
            }
        }

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty
            .Create(nameof(BackgroundColor),
            typeof(Color),
            typeof(Shape),
            default(Color));

        public new Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(BackgroundColorProperty);
            }
            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }

        public static new readonly BindableProperty PaddingProperty = BindableProperty
            .Create(nameof(Padding),
            typeof(Thickness),
            typeof(Shape),
            default(Thickness));

        public new Thickness Padding
        {
            get
            {
                return (Thickness)GetValue(PaddingProperty);
            }
            set
            {
                SetValue(PaddingProperty, value);
            }
        }

        public static readonly BindableProperty GradientProperty = BindableProperty
            .Create(nameof(Gradient),
            typeof(ShapeGradient),
            typeof(Shape),
            default(ShapeGradient));

        public ShapeGradient Gradient
        {
            get
            {
                return (ShapeGradient)GetValue(GradientProperty);
            }
            set
            {
                SetValue(GradientProperty, value);
            }
        }

        #endregion

        public Shape()
        {
            InitializeComponent();

            InitializePaints();

            ApplyDefaultStyles();
        }

        #region Methods

        private void InitializePaints()
        {
            _canvasInfo = new CanvasInfo();

            // Paint surface
            canvas.PaintSurface += Canvas_PaintSurface;

            _shapePaint = new SKPaint
            {
                IsAntialias = true,
            };
        }

        private void ApplyDefaultStyles()
        {
            // Layout options
            VerticalOptions = LayoutOptions.Start;
            HorizontalOptions = LayoutOptions.Fill;
        }

        private void DrawShape()
        {
            // Check shape type
            switch (Type)
            {
                case ShapeType.Rounded: DrawRoundedShape(); break;
                case ShapeType.Rectangle: DrawRectangeleShape(); break;
            }
        }

        private void DrawRoundedShape()
        {
            using (var path = new SKPath())
            {
                // Top left
                path.MoveTo(new SKPoint(0, Parameters.TopLeft));
                path.QuadTo(new SKPoint(0, 0), new SKPoint(Parameters.TopLeft, 0));

                // Top right
                path.LineTo(_canvasInfo.ImageInfo.Width - Parameters.TopRight, 0);
                path.QuadTo(new SKPoint(_canvasInfo.ImageInfo.Width, 0),
                    new SKPoint(_canvasInfo.ImageInfo.Width, Parameters.TopRight));

                // Bottom right
                path.LineTo(_canvasInfo.ImageInfo.Width, _canvasInfo.ImageInfo.Height - Parameters.BottomRight);
                path.QuadTo(new SKPoint(_canvasInfo.ImageInfo.Width, _canvasInfo.ImageInfo.Height),
                    new SKPoint(_canvasInfo.ImageInfo.Width - Parameters.BottomRight, _canvasInfo.ImageInfo.Height));

                // Bottom left
                path.LineTo(Parameters.BottomLeft, _canvasInfo.ImageInfo.Height);
                path.QuadTo(new SKPoint(0, _canvasInfo.ImageInfo.Height),
                    new SKPoint(0, _canvasInfo.ImageInfo.Height - Parameters.BottomLeft));

                // Gradient & Background color
                if (Gradient != null)
                    AddGradinet();
                else
                    _shapePaint.Color = SKColor.Parse(BackgroundColor.ToHex());

                // Draw
                _canvasInfo.Canvas.DrawPath(path, _shapePaint);
            }
        }

        private void DrawRectangeleShape()
        {
            // Not implemented ;)
        }

        private void AddContent()
        {
            // Check child
            if (Section == null)
                return;

            // Add child
            section.Children.Clear();
            section.Children.Add(Section);
        }

        private void AddGradinet()
        {
            _shapePaint.Shader = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0),
                    new SKPoint(_canvasInfo.ImageInfo.Width, _canvasInfo.ImageInfo.Height),
                    new SKColor[] { SKColor.Parse(Gradient.Start.ToHex()), SKColor.Parse(Gradient.End.ToHex()) },
                    new float[] { Gradient.StartPosition, Gradient.EndPosition },
                    SKShaderTileMode.Clamp,
                    SKMatrix.MakeRotation(Gradient.Degrees,
                        _canvasInfo.ImageInfo.Rect.MidX,
                        _canvasInfo.ImageInfo.Rect.MidY));
        }

        #endregion

        #region Events

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Parameters
            if (propertyName == ParametersProperty.PropertyName)
            {
                canvas.InvalidateSurface();
            }

            // Type
            if (propertyName == TypeProperty.PropertyName)
            {
                canvas.InvalidateSurface();
            }

            // Section
            if (propertyName == SectionProperty.PropertyName)
            {
                canvas.InvalidateSurface();
            }

            // Background color
            if (propertyName == BackgroundColorProperty.PropertyName)
            {
                canvas.InvalidateSurface();
            }

            // Padding
            if (propertyName == PaddingProperty.PropertyName)
            {
                section.Padding = Padding;
            }

            // Gradient
            if (propertyName == GradientProperty.PropertyName)
            {
                canvas.InvalidateSurface();
            }
        }

        private void Section_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //container.HeightRequest = section.Height;
        }

        private void Canvas_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            // Get info
            _canvasInfo.Canvas = e.Surface.Canvas;
            _canvasInfo.Surface = e.Surface;
            _canvasInfo.ImageInfo = e.Info;

            // Clear
            e.Surface.Canvas.Clear(SKColors.Transparent);

            // Draw
            DrawShape();
            AddContent();
        }

        #endregion
    }
}
