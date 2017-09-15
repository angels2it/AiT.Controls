using Android.Content;
using Android.Graphics;
using Android.Widget;

namespace AiT.Controls.CameraView.Droid
{
    public class PaintCodeButton : Button
    {
        public PaintCodeButton(Context context) : base(context)
        {
            Background.Alpha = 0;
        }


        protected override void OnDraw(Canvas canvas)
        {
            var frame = new Rect(Left, Top, Right, Bottom);

            Paint paint;
            // Local Colors
            var color = Color.White;

            RectF bezierRect = new RectF(
                frame.Left + (float)Java.Lang.Math.Floor((frame.Width() - 120f) * 0.5f + 0.5f),
                frame.Top + (float)Java.Lang.Math.Floor((frame.Height() - 120f) * 0.5f + 0.5f),
                frame.Left + (float)Java.Lang.Math.Floor((frame.Width() - 120f) * 0.5f + 0.5f) + 120f,
                frame.Top + (float)Java.Lang.Math.Floor((frame.Height() - 120f) * 0.5f + 0.5f) + 120f);
            Path bezierPath = new Path();
            bezierPath.MoveTo(frame.Left + frame.Width() * 0.5f, frame.Top + frame.Height() * 0.08333f);
            bezierPath.CubicTo(frame.Left + frame.Width() * 0.41628f, frame.Top + frame.Height() * 0.08333f, frame.Left + frame.Width() * 0.33832f, frame.Top + frame.Height() * 0.10803f, frame.Left + frame.Width() * 0.27302f, frame.Top + frame.Height() * 0.15053f);
            bezierPath.CubicTo(frame.Left + frame.Width() * 0.15883f, frame.Top + frame.Height() * 0.22484f, frame.Left + frame.Width() * 0.08333f, frame.Top + frame.Height() * 0.3536f, frame.Left + frame.Width() * 0.08333f, frame.Top + frame.Height() * 0.5f);
            bezierPath.CubicTo(frame.Left + frame.Width() * 0.08333f, frame.Top + frame.Height() * 0.73012f, frame.Left + frame.Width() * 0.26988f, frame.Top + frame.Height() * 0.91667f, frame.Left + frame.Width() * 0.5f, frame.Top + frame.Height() * 0.91667f);
            bezierPath.CubicTo(frame.Left + frame.Width() * 0.73012f, frame.Top + frame.Height() * 0.91667f, frame.Left + frame.Width() * 0.91667f, frame.Top + frame.Height() * 0.73012f, frame.Left + frame.Width() * 0.91667f, frame.Top + frame.Height() * 0.5f);
            bezierPath.CubicTo(frame.Left + frame.Width() * 0.91667f, frame.Top + frame.Height() * 0.26988f, frame.Left + frame.Width() * 0.73012f, frame.Top + frame.Height() * 0.08333f, frame.Left + frame.Width() * 0.5f, frame.Top + frame.Height() * 0.08333f);
            bezierPath.Close();
            bezierPath.MoveTo(frame.Left + frame.Width(), frame.Top + frame.Height() * 0.5f);
            bezierPath.CubicTo(frame.Left + frame.Width(), frame.Top + frame.Height() * 0.77614f, frame.Left + frame.Width() * 0.77614f, frame.Top + frame.Height(), frame.Left + frame.Width() * 0.5f, frame.Top + frame.Height());
            bezierPath.CubicTo(frame.Left + frame.Width() * 0.22386f, frame.Top + frame.Height(), frame.Left, frame.Top + frame.Height() * 0.77614f, frame.Left, frame.Top + frame.Height() * 0.5f);
            bezierPath.CubicTo(frame.Left, frame.Top + frame.Height() * 0.33689f, frame.Left + frame.Width() * 0.0781f, frame.Top + frame.Height() * 0.19203f, frame.Left + frame.Width() * 0.19894f, frame.Top + frame.Height() * 0.10076f);
            bezierPath.CubicTo(frame.Left + frame.Width() * 0.28269f, frame.Top + frame.Height() * 0.03751f, frame.Left + frame.Width() * 0.38696f, frame.Top, frame.Left + frame.Width() * 0.5f, frame.Top);
            bezierPath.CubicTo(frame.Left + frame.Width() * 0.77614f, frame.Top, frame.Left + frame.Width(), frame.Top + frame.Height() * 0.22386f, frame.Left + frame.Width(), frame.Top + frame.Height() * 0.5f);
            bezierPath.Close();

            paint = new Paint();
            paint.SetStyle(Android.Graphics.Paint.Style.Fill);
            paint.Color = (color);
            canvas.DrawPath(bezierPath, paint);

            paint = new Paint();
            paint.StrokeWidth = (1f);
            paint.StrokeMiter = (10f);
            canvas.Save();
            paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
            paint.Color = (Color.Black);
            canvas.DrawPath(bezierPath, paint);
            canvas.Restore();

            RectF ovalRect = new RectF(
                frame.Left + (float)Java.Lang.Math.Floor(frame.Width() * 0.12917f) + 0.5f,
                frame.Top + (float)Java.Lang.Math.Floor(frame.Height() * 0.12083f) + 0.5f,
                frame.Left + (float)Java.Lang.Math.Floor(frame.Width() * 0.87917f) + 0.5f,
                frame.Top + (float)Java.Lang.Math.Floor(frame.Height() * 0.87083f) + 0.5f);
            Path ovalPath = new Path();
            ovalPath.AddOval(ovalRect, Path.Direction.Cw);

            paint = new Paint();
            paint.SetStyle(Android.Graphics.Paint.Style.Fill);
            paint.Color = (color);
            canvas.DrawPath(ovalPath, paint);

            paint = new Paint();
            paint.StrokeWidth = (1f);
            paint.StrokeMiter = (10f);
            canvas.Save();
            paint.SetStyle(Android.Graphics.Paint.Style.Stroke);
            paint.Color = (Color.Black);
            canvas.DrawPath(ovalPath, paint);
            canvas.Restore();
        }
    }
}