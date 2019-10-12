using SkiaSharp;
using System.Timers;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using Utmdev.Xf.Components.Models;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Component.Helpers;

namespace Utmdev.Xf.Components
{
    public partial class Switcher : ContentView
    {
        #region Fields

        // Paints
        private SKPaint _circlePaint;

        private SKPaint _backgroundPaint;

        private SKPaint _backgroundCirclePaint;

        // Info
        private bool _isEnabled = true;

        private SKRect _backgroundRoundedRect;

        private float _circleRadius;

        private float _circleOffset;

        private float _circleX;

        private float _percentage = 100;

        private CanvasInfo _canvasInfo = new CanvasInfo();

        // Animation
        private Timer _animationTimer;

        private Interval _circleInterval;

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty CircleColorProperty = BindableProperty
            .Create(nameof(CircleColor), typeof(Color), typeof(Switcher), default(Color));

        public Color CircleColor
        {
            get => (Color)GetValue(CircleColorProperty);
            set => SetValue(CircleColorProperty, value);
        }

        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty
            .Create(nameof(BackgroundColor), typeof(Color), typeof(Switcher), Color.FromHsla(0.36, 0.5, 0.5));

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public static readonly BindableProperty IsOnProperty = BindableProperty
            .Create(nameof(IsOn), typeof(bool?), typeof(Switcher));

        public bool IsOn
        {
            get => (bool)GetValue(IsOnProperty);
            set => SetValue(IsOnProperty, value);
        }

        #endregion

        #region Commands

        public static readonly BindableProperty TapCommandProperty = BindableProperty
            .Create(nameof(TapCommand), typeof(ICommand), typeof(Switcher));

        public ICommand TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }

        #endregion

        public Switcher()
        {
            InitializeComponent();

            InitializeGestures();

            InitializePaints();

            InitializeAnimation();
        }

        #region Events

        private void Canvas_PaintSurface
            (object sender, SKPaintSurfaceEventArgs e)
        {
            // Get canvas info
            _canvasInfo.Canvas = e.Surface.Canvas;
            _canvasInfo.ImageInfo = e.Info;
            _canvasInfo.Surface = e.Surface;

            Calculate();

            e.Surface.Canvas.Clear(SKColors.Transparent);

            // Background
            _backgroundPaint.Color = SKColor.Parse("#DDDDDD");

            e.Surface.Canvas.DrawRoundRect
                (_backgroundRoundedRect, _circleRadius, _circleRadius, _backgroundPaint);

            _backgroundPaint.Color = SKColor.FromHsl
            ((float)(BackgroundColor.Hue * 360),
                (float)(BackgroundColor.Saturation * 100),
                (float)(BackgroundColor.Luminosity * 100),
                (byte)(_percentage / 100 * 255));

            e.Surface.Canvas.DrawRoundRect
                (_backgroundRoundedRect, _circleRadius, _circleRadius, _backgroundPaint);

            // Circle
            _canvasInfo.Canvas.DrawCircle
            (_circleX, e.Info.Rect.MidY
                , _circleRadius - _circleOffset
                , _circlePaint);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            // Circle color
            if (propertyName == CircleColorProperty.PropertyName)
            {
                _circlePaint.Color = SKColor.Parse(CircleColor.GetHexString());

                canvas.InvalidateSurface();
            }

            // Background color
            if (propertyName == BackgroundColorProperty.PropertyName)
            {
                _backgroundPaint.Color = SKColor.Parse(BackgroundColor.GetHexString());

                canvas.InvalidateSurface();
            }

            // Is enabled
            if (propertyName == IsOnProperty.PropertyName)
            {
                if (IsOn)
                    _percentage = 0;
                else
                    _percentage = 100;

                _animationTimer.Start();
            }
        }

        private void AnimationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            switch (IsOn)
            {
                case true:
                    _percentage += 10;
                    break;
                case false:
                    _percentage -= 10;
                    break;
            }

            Device.BeginInvokeOnMainThread(() => { canvas.InvalidateSurface(); });

            // Check
            if (_percentage <= 0 || _percentage >= 100)
                _animationTimer.Stop();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize gestures
        /// </summary>
        private void InitializeGestures()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += (s, e) =>
            {
                IsOn = !IsOn;

                XamarinHelper.ExecuteCommand(TapCommand, IsOn);
            };

            GestureRecognizers.Add(tapGestureRecognizer);
        }

        /// <summary>
        /// Initialize animation timer
        /// </summary>
        private void InitializeAnimation()
        {
            // Animation
            _animationTimer = new Timer
            {
                AutoReset = true,
                Interval = 33
            };

            _animationTimer.Elapsed += AnimationTimer_Elapsed;
        }

        /// <summary>
        /// Initialize paints
        /// </summary>
        private void InitializePaints()
        {
            // Circle
            _circlePaint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.White,
            };

            // Background
            _backgroundPaint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColor.Parse("#47D86C"),
            };

            _backgroundCirclePaint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColor.Parse("#47D86C"),
            };

            canvas.PaintSurface += Canvas_PaintSurface;
        }

        /// <summary>
        /// Get circle position from percentage
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        private float GetCirclePosition(float percentage)
        {
            // Check percentage
            if (percentage < 0 || percentage > 100)
                return 0;

            // Calculate circle position based on specified percentage
            var maxMinDifference = _circleInterval.Min - _circleInterval.Max;

            var y = (maxMinDifference * percentage +
                (maxMinDifference * -100 - -100 * -_circleInterval.Max)) / -100;

            return y;
        }

        /// <summary>
        /// Calculate elements properties
        /// </summary>
        private void Calculate()
        {
            // Calculate circle radius
            _circleRadius = _canvasInfo.ImageInfo.Height / 2;

            // Background
            _backgroundRoundedRect = new SKRect
            {
                Top = 0,
                Left = _circleRadius,
                Right = _canvasInfo.ImageInfo.Width,
                Bottom = _canvasInfo.ImageInfo.Height
            };

            // Circle
            _circleOffset = 7;
            _circleInterval.Max = _backgroundRoundedRect.Right - _circleRadius;
            _circleInterval.Min = _backgroundRoundedRect.Left + _circleRadius;
            _circleX = GetCirclePosition(_percentage);

            // Check animation timer
            if (_circleX <= _circleInterval.Min
                || _circleX >= _circleInterval.Max)
                _animationTimer.Stop();
        }

        #endregion

        #region Types

        struct Interval
        {
            public float Max;
            public float Min;
        }

        #endregion
    }
}
