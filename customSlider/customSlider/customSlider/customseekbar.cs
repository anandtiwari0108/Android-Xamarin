using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace customSlider
{
    class CustomSeekBar:ImageView
    {
        public static readonly Color DefaultActiveColor = Color.Argb(0xFF, 0x33, 0xB5, 0xE5);

       
        //    An invalid pointer id.
     
        public const int InvalidPointerId = 255;

        
        public const int ActionPointerIndexMask = 0x0000ff00, ActionPointerIndexShift = 8;
        //variable to set minimum slider
        public const int DefaultMinimum = 0;
        //variable to set maximum slider
        public const int DefaultMaximum = 100;

        public const int HeightInDp = 16;
        public const int TextLateralPaddingInDp = 5;

        private const int InitialPaddingInDp = 8;
        //private const int DefaultTextSizeInSp = 15;
        //private const int DefaultTextDistanceToButtonInDp = 8;
        private const int DefaultTextDistanceToTopInDp = 8;

        private const int DefaultStepValue = 0;

        private const int LineHeightInDp = 40;
        private readonly Paint _paint = new Paint(PaintFlags.AntiAlias);
        private readonly Paint _shadowPaint = new Paint();
        private readonly Matrix _thumbShadowMatrix = new Matrix();
        private readonly Path _translatedThumbShadowPath = new Path();

        private int _activePointerId = InvalidPointerId;
        private int _distanceToTop;

        private float _downMotionX;
        private float _internalPad;

        private bool _isDragging;

        private float _padding;
        private Thumb? _pressedThumb;
        private RectF _rect;

        private int _scaledTouchSlop;

      
        private float _thumbHalfHeight;

        private float _thumbHalfWidth;
        private int _thumbShadowBlur;
        private Path _thumbShadowPath;
        protected float AbsoluteMinValue, AbsoluteMaxValue;
        protected float MinDeltaForDefault = 0;
        protected float NormalizedMaxValue = 1f;
        protected float NormalizedMinValue;
        private Color _activeColor;
        private bool _minThumbHidden;
        private bool _maxThumbHidden;
        
        private float _barHeight;
        private string _textFormat = "F0";

        private float MinToMaxRange
        {
            get
            {
                return AbsoluteMaxValue - AbsoluteMinValue;
            }
        }

      

        public CustomSeekBar(Context context) : base(context)
        {
            Init(context,null);
        }

        public CustomSeekBar(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public CustomSeekBar(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        public CustomSeekBar(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context, attrs);
        }

        public bool ActivateOnDefaultValues { get; set; }

        public Color ActiveColor
        {
            get { return _activeColor; }
            set
            {
                _activeColor = value;
                Invalidate();
            }
        }

        public Func<Thumb, float, string> FormatLabel { get; set; }

        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
                Invalidate();
            }
        }

        public bool AlwaysActive { get; set; }
        public Color DefaultColor { get; set; }
        

        
        
        public string TextFormat
        {
            get { return _textFormat; }
            set
            {
                _textFormat = string.IsNullOrWhiteSpace(value) ? "F0" : value;
                Invalidate();
            }
        }

        public bool MinThumbHidden
        {
            get { return _minThumbHidden; }
            set
            {
                _minThumbHidden = value;
                Invalidate();
            }
        }

        public bool MaxThumbHidden
        {
            get { return _maxThumbHidden; }
            set
            {
                _maxThumbHidden = value;
                Invalidate();
            }
        }

      
        public Bitmap ThumbDisabledImage { get; set; }

        public Bitmap ThumbImage { get; set; }
        public Bitmap ThumbPressedImage { get; set; }

        public bool ThumbShadow { get; set; }
        public int ThumbShadowXOffset { get; set; }
        public int ThumbShadowYOffset { get; set; }

        
        //     Should the widget notify the listener callback while the user is still dragging a thumb? Default is false.
        
        public bool NotifyWhileDragging { get; set; }

       
        //     default 0.0 =disabled
       
        public float StepValue { get; set; }

        
        //If false the slider will move freely with the tounch. When the touch ends, the value will snap to the nearest step value
        //If true the slider will stay in its current position until it reaches a new step value.
        // default false
        
        public bool StepValueContinuously { get; set; }

        private float ExtractNumericValueFromAttributes(TypedArray a, int attribute, int defaultValue)
        {
            var tv = a.PeekValue(attribute);
            return tv == null ? defaultValue : a.GetFloat(attribute, defaultValue);
        }


        //method to set the value when the constructor is called
        private void Init(Context context, IAttributeSet attrs)
        {
            var thumbNormal = Resource.Drawable.star1;
            var thumbPressed = Resource.Drawable.star2;
            var thumbDisabled = Resource.Drawable.star3;
            Color thumbShadowColor;
            var defaultShadowColor = Color.Argb(75, 0, 0, 0);
            var defaultShadowYOffset = dimensionHelper.DpToPx(context, 2);
            var defaultShadowXOffset = dimensionHelper.DpToPx(context, 0);
            var defaultShadowBlur = dimensionHelper.DpToPx(context, 2);

            _distanceToTop = dimensionHelper.DpToPx(context, DefaultTextDistanceToTopInDp);

            if (attrs == null)
            {
                SetRangeToDefaultValues();
                _internalPad = dimensionHelper.DpToPx(context, InitialPaddingInDp);
                _barHeight = dimensionHelper.DpToPx(context, LineHeightInDp);
                ActiveColor = DefaultActiveColor;
                DefaultColor = Color.Gray;
                AlwaysActive = false;
               
                
                thumbShadowColor = defaultShadowColor;
                ThumbShadowXOffset = defaultShadowXOffset;
                ThumbShadowYOffset = defaultShadowYOffset;
                _thumbShadowBlur = defaultShadowBlur;
                ActivateOnDefaultValues = false;
                
            }
            else
            {
                var a = Context.ObtainStyledAttributes(attrs, Resource.Styleable.CustomSeekbar, 0, 0);
                try
                {
                    SetRangeValues(ExtractNumericValueFromAttributes(a, Resource.Styleable.CustomSeekbar_absoluteMinValue, DefaultMinimum),
                        ExtractNumericValueFromAttributes(a, Resource.Styleable.CustomSeekbar_absoluteMaxValue, DefaultMaximum));
                    
                    
                    MinThumbHidden = a.GetBoolean(Resource.Styleable.CustomSeekbar_minThumbHidden, false);
                    MaxThumbHidden = a.GetBoolean(Resource.Styleable.CustomSeekbar_maxThumbHidden, false);
                   
                    _internalPad = a.GetDimensionPixelSize(Resource.Styleable.CustomSeekbar_internalPadding, InitialPaddingInDp);
                    _barHeight = a.GetDimensionPixelSize(Resource.Styleable.CustomSeekbar_barHeight, LineHeightInDp);
                    ActiveColor = a.GetColor(Resource.Styleable.CustomSeekbar_activeColor, DefaultActiveColor);
                    DefaultColor = a.GetColor(Resource.Styleable.CustomSeekbar_defaultColor, Color.Gray);
                    AlwaysActive = a.GetBoolean(Resource.Styleable.CustomSeekbar_alwaysActive, false);
                    StepValue = ExtractNumericValueFromAttributes(a,
                        Resource.Styleable.CustomSeekbar_stepValue, DefaultStepValue);
                    StepValueContinuously = a.GetBoolean(Resource.Styleable.CustomSeekbar_stepValueContinuously,
                        false);
                    //set the image of the thumb
                    var normalDrawable = a.GetDrawable(Resource.Styleable.CustomSeekbar_thumbNormal);
                    if (normalDrawable != null)
                    {
                        ThumbImage = ImageHelper.DrawableToBitmap(normalDrawable);
                    }
                    var disabledDrawable = a.GetDrawable(Resource.Styleable.CustomSeekbar_thumbDisabled);
                    if (disabledDrawable != null)
                    {
                        ThumbDisabledImage = ImageHelper.DrawableToBitmap(disabledDrawable);
                    }
                    var pressedDrawable = a.GetDrawable(Resource.Styleable.CustomSeekbar_thumbPressed);
                    if (pressedDrawable != null)
                    {
                        ThumbPressedImage = ImageHelper.DrawableToBitmap(pressedDrawable);
                    }
                    ThumbShadow = a.GetBoolean(Resource.Styleable.CustomSeekbar_thumbShadow, false);
                    thumbShadowColor = a.GetColor(Resource.Styleable.CustomSeekbar_thumbShadowColor, defaultShadowColor);
                    ThumbShadowXOffset = a.GetDimensionPixelSize(Resource.Styleable.CustomSeekbar_thumbShadowXOffset, defaultShadowXOffset);
                    ThumbShadowYOffset = a.GetDimensionPixelSize(Resource.Styleable.CustomSeekbar_thumbShadowYOffset, defaultShadowYOffset);
                    _thumbShadowBlur = a.GetDimensionPixelSize(Resource.Styleable.CustomSeekbar_thumbShadowBlur, defaultShadowBlur);

                    ActivateOnDefaultValues = a.GetBoolean(Resource.Styleable.CustomSeekbar_activateOnDefaultValues, false);
                    
                }
                finally
                {
                    a.Recycle();
                }
            }

            if (ThumbImage == null)
            {
                ThumbImage = BitmapFactory.DecodeResource(Resources, thumbNormal);
            }
            if (ThumbPressedImage == null)
            {
                ThumbPressedImage = BitmapFactory.DecodeResource(Resources, thumbPressed);
            }
            if (ThumbDisabledImage == null)
            {
                ThumbDisabledImage = BitmapFactory.DecodeResource(Resources, thumbDisabled);
            }

            _thumbHalfWidth = 0.5f * ThumbImage.Width;
            _thumbHalfHeight = 0.5f * ThumbImage.Height;

            SetBarHeight(_barHeight);

            // make RangeSliderControl focusable. This solves focus handling issues in case EditText widgets are being used along with the RangeSliderControl within ScrollViews.
            Focusable = true;
            FocusableInTouchMode = true;
            _scaledTouchSlop = ViewConfiguration.Get(Context).ScaledTouchSlop;

            
        }

        public void SetBarHeight(float barHeight)
        {
            _barHeight = barHeight;
            if (_rect == null)
                _rect = new RectF(_padding,
                      _thumbHalfHeight - barHeight / 2,
                    Width - _padding,
                     _thumbHalfHeight + barHeight / 2);
            else
                _rect = new RectF(_rect.Left,
                    _thumbHalfHeight - barHeight / 2,
                    _rect.Right,
                    _thumbHalfHeight + barHeight / 2);
            Invalidate();
        }

        public void SetRangeValues(float minValue, float maxValue)
        {
            var oldMinValue = NormalizedToValue(NormalizedMinValue);
            var oldMaxValue = NormalizedToValue(NormalizedMaxValue);
            AbsoluteMinValue = minValue;
            AbsoluteMaxValue = maxValue;
            if (Math.Abs(MinToMaxRange) < float.Epsilon)
            {
                SetNormalizedMinValue(0f, true, true);
                SetNormalizedMaxValue(0f, true, true);
            }
            else
            {
                SetNormalizedMinValue(ValueToNormalized(oldMinValue), true, true);
                SetNormalizedMaxValue(ValueToNormalized(oldMaxValue), true, true);
            }
            Invalidate();
        }

      

       

       
        // only used to set default values when initialised from XML without any values specified
       
        private void SetRangeToDefaultValues()
        {
            AbsoluteMinValue = DefaultMinimum;
            AbsoluteMaxValue = DefaultMaximum;
        }

        public void ResetSelectedValues()
        {
            SetSelectedMinValue(AbsoluteMinValue);
            SetSelectedMaxValue(AbsoluteMaxValue);
        }

        
        // Returns the absolute minimum value of the range that has been set at construction time.
        
        //The absolute minimum value of the range.
        public float GetAbsoluteMinValue()
        {
            return AbsoluteMinValue;
        }

        //
        // Returns the absolute maximum value of the range that has been set at construction time.
       
        //The absolute maximum value of the range.
        public float GetAbsoluteMaxValue()
        {
            return AbsoluteMaxValue;
        }

        /// <summary>
        /// Returns the currently selected min value.
        /// </summary>
        /// <returns>The currently selected min value.</returns>
        public float GetSelectedMinValue()
        {
            return NormalizedToValue(NormalizedMinValue);
        }

        /// <summary>
        /// Sets the currently selected minimum value. The widget will be Invalidated and redrawn.
        /// </summary>
        /// <param name="value">The Number value to set the minimum value to. Will be clamped to given absolute minimum/maximum range.</param>
        public void SetSelectedMinValue(float value, bool force = false)
        {
            if (_pressedThumb == Thumb.Lower && !force)
                return;
            // in case absoluteMinValue == absoluteMaxValue, avoid division by zero when normalizing.
            SetNormalizedMinValue(Math.Abs(MinToMaxRange) < float.Epsilon
                ? 0f
                : ValueToNormalized(value), true, false);
        }

      
        // Returns the currently selected max value.
        
        // The currently selected max value.
        public float GetSelectedMaxValue()
        {
            return NormalizedToValue(NormalizedMaxValue);
        }

       
        /// Sets the currently selected maximum value.
       
        //The Number value to set the maximum value to. Will be clamped to given absolute minimum/maximum range
        public void SetSelectedMaxValue(float value, bool force = false)
        {
            if (_pressedThumb == Thumb.Upper && !force)
                return;
            // in case absoluteMinValue == absoluteMaxValue, avoid division by zero when normalizing.
            SetNormalizedMaxValue(Math.Abs(MinToMaxRange) < float.Epsilon
                ? 1f
                : ValueToNormalized(value), true, false);
        }

        
        // Set the path that defines the shadow of the thumb. This path should be defined assuming
        // that the center of the shadow is at the top left corner(0,0) of the canvas.The
        // <see cref="DrawThumbShadow"/> method will place the shadow appropriately.
     
        //thumbShadowPath=The path defining the thumb shadow
        public void SetThumbShadowPath(Path thumbShadowPath)
        {
            _thumbShadowPath = thumbShadowPath;
        }

       //     Handles thumb selection and movement. Notifies listener callback on certain evs.
       
        public override bool OnTouchEvent(MotionEvent ev)
        {
            if (!Enabled)
            {
                return false;
            }

            int pointerIndex;

            var action = ev.Action;
            switch (action & MotionEventActions.Mask)
            {
                case MotionEventActions.Down:
                    // Remember where the motion ev started
                    _activePointerId = ev.GetPointerId(ev.PointerCount - 1);
                    pointerIndex = ev.FindPointerIndex(_activePointerId);
                    _downMotionX = ev.GetX(pointerIndex);

                    _pressedThumb = EvalPressedThumb(_downMotionX);

                    // Only handle thumb presses.
                    if (_pressedThumb == null)
                    {
                        return base.OnTouchEvent(ev);
                    }

                    Pressed = true;
                    Invalidate();
                    OnStartTrackingTouch();
                    TrackTouchEvent(ev, StepValueContinuously);
                    AttemptClaimDrag();

                    break;
                case MotionEventActions.Move:
                    if (_pressedThumb != null)
                    {
                        if (_isDragging)
                        {
                            TrackTouchEvent(ev, StepValueContinuously);
                        }
                        else
                        {
                            // Scroll to follow the motion ev
                            pointerIndex = ev.FindPointerIndex(_activePointerId);
                            var x = ev.GetX(pointerIndex);

                            if (Math.Abs(x - _downMotionX) > _scaledTouchSlop)
                            {
                                Pressed = true;
                                Invalidate();
                                OnStartTrackingTouch();
                                TrackTouchEvent(ev, StepValueContinuously);
                                AttemptClaimDrag();
                            }
                        }

                        if (NotifyWhileDragging)
                        {
                            if (_pressedThumb == Thumb.Lower)
                                OnLowerValueChanged();
                            if (_pressedThumb == Thumb.Upper)
                                OnUpperValueChanged();
                        }
                    }
                    break;
                case MotionEventActions.Up:
                    if (_isDragging)
                    {
                        TrackTouchEvent(ev, true);
                        OnStopTrackingTouch();
                        Pressed = false;
                    }
                    else
                    {
                        // Touch up when we never crossed the touch slop threshold
                        // should be interpreted as a tap-seek to that location.
                        OnStartTrackingTouch();
                        TrackTouchEvent(ev, true);
                        OnStopTrackingTouch();
                    }
                    if (_pressedThumb == Thumb.Lower)
                        OnLowerValueChanged();
                    if (_pressedThumb == Thumb.Upper)
                        OnUpperValueChanged();
                    _pressedThumb = null;
                    Invalidate();
                    break;
                case MotionEventActions.PointerDown:
                    var index = ev.PointerCount - 1;
                    // readonly int index = ev.getActionIndex();
                    _downMotionX = ev.GetX(index);
                    _activePointerId = ev.GetPointerId(index);
                    Invalidate();
                    break;
                case MotionEventActions.PointerUp:
                    OnSecondaryPointerUp(ev);
                    Invalidate();
                    break;
                case MotionEventActions.Cancel:
                    if (_isDragging)
                    {
                        OnStopTrackingTouch();
                        Pressed = false;
                    }
                    Invalidate(); // see above explanation
                    break;
            }
            return true;
        }

        private void OnSecondaryPointerUp(MotionEvent ev)
        {
            var pointerIndex = (int)(ev.Action & MotionEventActions.PointerIndexMask) >>
                               (int)MotionEventActions.PointerIndexShift;

            var pointerId = ev.GetPointerId(pointerIndex);
            if (pointerId == _activePointerId)
            {
                // This was our active pointer going up. Choose
                // a new active pointer and adjust accordingly.
                // TODO: Make this decision more intelligent.
                var newPointerIndex = pointerIndex == 0 ? 1 : 0;
                _downMotionX = ev.GetX(newPointerIndex);
                _activePointerId = ev.GetPointerId(newPointerIndex);
            }
        }

        private void TrackTouchEvent(MotionEvent ev, bool step)
        {
            var pointerIndex = ev.FindPointerIndex(_activePointerId);
            var x = ev.GetX(pointerIndex);

            if (Thumb.Lower.Equals(_pressedThumb) && !MinThumbHidden)
            {
                SetNormalizedMinValue(ScreenToNormalized(x), step, true);
            }
            else if (Thumb.Upper.Equals(_pressedThumb) && !MaxThumbHidden)
            {
                SetNormalizedMaxValue(ScreenToNormalized(x), step, true);
            }
        }

        
      
       
        private void AttemptClaimDrag()
        {
            Parent?.RequestDisallowInterceptTouchEvent(true);
        }

        
        //     on touch event.
       
        private void OnStartTrackingTouch()
        {
            _isDragging = true;
            DragStarted?.Invoke(this, EventArgs.Empty);
        }

       
        //     This is called when we  touch the thumb or the touch is canceled.
        
        private void OnStopTrackingTouch()
        {
            _isDragging = false;
            DragCompleted?.Invoke(this, EventArgs.Empty);
        }

       
        //    Ensures correct size of the widget.
        
        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            var width = 200;
            if (MeasureSpecMode.Unspecified != MeasureSpec.GetMode(widthMeasureSpec))
            {
                width = MeasureSpec.GetSize(widthMeasureSpec);
            }

            var height = ThumbImage.Height;
                        
            if (MeasureSpecMode.Unspecified != MeasureSpec.GetMode(heightMeasureSpec))
            {
                height = Math.Min(height, MeasureSpec.GetSize(heightMeasureSpec));
            }
            SetMeasuredDimension(width, height);
        }

       
        // Draws the seekbar on the given canvas.
       
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

         
            _paint.SetStyle(Paint.Style.Fill);
            _paint.Color = DefaultColor;
            _paint.AntiAlias = true;
            

           
            _padding = _internalPad  + _thumbHalfWidth;

            // draw seek bar background line
            _rect.Left = _padding;
            _rect.Right = Width - _padding;
            canvas.DrawRect(_rect, _paint);

            var selectedValuesAreDefault = NormalizedMinValue <= MinDeltaForDefault &&
                                           NormalizedMaxValue >= 1 - MinDeltaForDefault;

            var colorToUseForButtonsAndHighlightedLine =
                !Enabled || (!AlwaysActive && !ActivateOnDefaultValues && selectedValuesAreDefault)
                    ? DefaultColor // default values
                    : ActiveColor; // non default, filter is active

            // draw seek bar active range line
            _rect.Left = NormalizedToScreen(NormalizedMinValue);
            _rect.Right = NormalizedToScreen(NormalizedMaxValue);

            _paint.Color = colorToUseForButtonsAndHighlightedLine;
            canvas.DrawRect(_rect, _paint);

            // draw minimum thumb (& shadow if requested) if not a single thumb control
            if (!MinThumbHidden)
            {
                if (ThumbShadow)
                {
                    DrawThumbShadow(NormalizedToScreen(NormalizedMinValue), canvas);
                }
                DrawThumb(NormalizedToScreen(NormalizedMinValue), Thumb.Lower.Equals(_pressedThumb), canvas,
                   selectedValuesAreDefault);
            }

            // draw maximum thumb & shadow (if necessary)
            if (!MaxThumbHidden)
            {
                if (ThumbShadow)
                {
                    DrawThumbShadow(NormalizedToScreen(NormalizedMaxValue), canvas);
                }
                DrawThumb(NormalizedToScreen(NormalizedMaxValue), Thumb.Upper.Equals(_pressedThumb), canvas,
                   selectedValuesAreDefault);
            }
            
        }

        protected string ValueToString(float value, Thumb thumb)
        {
            var func = FormatLabel;
            return func == null
                ? value.ToString(_textFormat, CultureInfo.InvariantCulture)
                : func(thumb, value);
        }

       
        
        //   Draws the "normal" resp. "pressed" thumb image on specified x-coordinate.
       
        //screenCoord=The x-coordinate in screen space where to draw the image.
        //pressed=Is the thumb currently in "pressed" state?
        //canvas=The canvas to draw upon.
        //areSelectedValuesDefault.
        private void DrawThumb(float screenCoord, bool pressed, Canvas canvas, bool areSelectedValuesDefault)
        {
            Bitmap buttonToDraw;
            if (!Enabled || (!ActivateOnDefaultValues && areSelectedValuesDefault))
                buttonToDraw = ThumbDisabledImage;
            else
                buttonToDraw = pressed ? ThumbPressedImage : ThumbImage;

            canvas.DrawBitmap(buttonToDraw, screenCoord - _thumbHalfWidth, -4, _paint);
        }

        
        //     Draws a drop shadow beneath the slider thumb.
      
        // screenCoord=the x-coordinate of the slider thumb
        // canvas=the canvas on which to draw the shadow
        private void DrawThumbShadow(float screenCoord, Canvas canvas)
        {
            _thumbShadowMatrix.SetTranslate(screenCoord + ThumbShadowXOffset,
                _thumbHalfHeight + ThumbShadowYOffset);
            _translatedThumbShadowPath.Set(_thumbShadowPath);
            _translatedThumbShadowPath.Transform(_thumbShadowMatrix);
            canvas.DrawPath(_translatedThumbShadowPath, _shadowPaint);
        }

       
        //Decides which (if any) thumb is touched by the given x-coordinate.
       
        //touchX=The x-coordinate of a touch ev in screen space.
        // The pressed thumb or null if none has been touched.
        private Thumb? EvalPressedThumb(float touchX)
        {
            Thumb? result = null;
            var minThumbPressed = IsInThumbRange(touchX, NormalizedMinValue);
            var maxThumbPressed = IsInThumbRange(touchX, NormalizedMaxValue);
            if (minThumbPressed && maxThumbPressed)
                // if both thumbs are pressed (they lie on top of each other), choose the one with more room to drag. this avoids "stalling" the thumbs in a corner, not being able to drag them apart anymore.
                result = touchX / Width > 0.5f ? Thumb.Lower : Thumb.Upper;
            else if (minThumbPressed)
                result = Thumb.Lower;
            else if (maxThumbPressed)
                result = Thumb.Upper;
            return result;
        }

       
        //    Decides if given x-coordinate in screen space needs to be interpreted as "within" the normalized thumb
        //    x-coordinate.
       
        //touchX=The x-coordinate in screen space to check.
        //normalizedThumbValue=The normalized x-coordinate of the thumb to check.
        //true if x-coordinate is in thumb range, false otherwise.
        private bool IsInThumbRange(float touchX, float normalizedThumbValue)
        {
            return Math.Abs(touchX - NormalizedToScreen(normalizedThumbValue)) <= _thumbHalfWidth;
        }

       
        // Sets normalized min value to value so that 0 <= value <= normalized max value <= 1. The View will get Invalidated when calling this method.
       
        //value=The new normalized min value to set.
        //step=If true then value is rounded StepValue
        //checkValue If true check if value falls inside min/max
        private void SetNormalizedMinValue(float value, bool step, bool checkValue)
        {
            NormalizedMinValue = checkValue
                                    ? Math.Max(0f, Math.Min(1f, Math.Min(value, NormalizedMaxValue)))
                                    : value;
            if (step)
                NormalizedMinValue = ValueToNormalized(NormalizedToValue(NormalizedMinValue));
            Invalidate();
        }

        // Sets normalized max value to value so that 0 <= normalized min value <= value <= 1. The View will get Invalidated when calling this method.
        
        //value=The new normalized max value to set.
        //step=If true then value is rounded to 
        ///checkValue=If true check if value falls inside min/max
        private void SetNormalizedMaxValue(float value, bool step, bool checkValue)
        {
            NormalizedMaxValue = checkValue
                                    ? Math.Max(0f, Math.Min(1f, Math.Max(value, NormalizedMinValue)))
                                    : value;
            if (step)
                NormalizedMaxValue = ValueToNormalized(NormalizedToValue(NormalizedMaxValue));
            Invalidate();
        }

      //Converts a normalized value to a Number object in the value space between absolute minimum and maximum.
        
        protected float NormalizedToValue(float normalized)
        {
            var v = normalized * MinToMaxRange;
            // TODO parameterize this rounding to allow variable decimal points
            if (Math.Abs(StepValue) < float.Epsilon)
                return AbsoluteMinValue + (float)Math.Round(v * 100) / 100f;
            var normalizedToValue = AbsoluteMinValue + (float)Math.Round(v / StepValue) * StepValue;
            return Math.Max(AbsoluteMinValue, Math.Min(AbsoluteMaxValue, normalizedToValue));
        }

       
        // Converts the given Number value to a normalized float.
        
        //value=The Number value to normalize.
        //The normalized float
        protected float ValueToNormalized(float value)
        {
            if (Math.Abs(MinToMaxRange) < float.Epsilon)
            {
                // prev division by zero, simply return 0.
                return 0f;
            }
            return (value - AbsoluteMinValue) / MinToMaxRange;
        }

       

        
        //Converts a normalized value into screen space.
        
        //normalizedCoord=The normalized value to convert.
        //return The converted value in screen space.
        private float NormalizedToScreen(float normalizedCoord)
        {
            return _padding + normalizedCoord * (Width - 2 * _padding);
        }

        
        /// Converts screen space x-coordinates into normalized values.
       
        //screenCoord=The x-coordinate in screen space to convert.
        //return The normalized value
       private float ScreenToNormalized(float screenCoord)
        {
            var width = Width;
            if (width <= 2 * _padding)
            {
                // prev division by zero, simply return 0.
                return 0f;
            }
            var result = (screenCoord - _padding) / (width - 2 * _padding);
            return Math.Min(1f, Math.Max(0f, result));
        }

        public event EventHandler LowerValueChanged;
        public event EventHandler UpperValueChanged;
        public event EventHandler DragStarted;
        public event EventHandler DragCompleted;

        protected virtual void OnLowerValueChanged()
        {
            LowerValueChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUpperValueChanged()
        {
            UpperValueChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}