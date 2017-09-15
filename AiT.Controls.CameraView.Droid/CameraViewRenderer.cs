using System;
using System.Linq;
using System.Threading.Tasks;
using AiT.Controls.CameraView;
using AiT.Controls.CameraView.Droid;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CameraView), typeof(CameraViewRenderer))]
namespace AiT.Controls.CameraView.Droid
{
    public class CameraViewRenderer : ViewRenderer, TextureView.ISurfaceTextureListener
    {
        RelativeLayout _mainLayout;
        TextureView _liveView;
        PaintCodeButton _capturePhotoButton;

#pragma warning disable 618
        Android.Hardware.Camera _camera;
#pragma warning restore 618

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);
            SetupUserInterface();
            SetupEventHandlers();
        }

        void SetupUserInterface()
        {
            _mainLayout = new RelativeLayout(Context);
            _liveView = new TextureView(Context);
            RelativeLayout.LayoutParams liveViewParams = new RelativeLayout.LayoutParams(
                LayoutParams.MatchParent,
                LayoutParams.MatchParent);
            _liveView.LayoutParameters = liveViewParams;
            _mainLayout.AddView(_liveView);

            _capturePhotoButton = new PaintCodeButton(Context);
            RelativeLayout.LayoutParams captureButtonParams = new RelativeLayout.LayoutParams(
                LayoutParams.WrapContent,
                LayoutParams.WrapContent);
            captureButtonParams.Height = 120;
            captureButtonParams.Width = 120;
            _capturePhotoButton.LayoutParameters = captureButtonParams;
            _mainLayout.AddView(_capturePhotoButton);



            AddView(_mainLayout);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            if (!changed)
                return;
            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);
            _mainLayout.Measure(msw, msh);
            _mainLayout.Layout(0, 0, r - l, b - t);

            _capturePhotoButton.SetX(_mainLayout.Width / 2 - 60);
            _capturePhotoButton.SetY(_mainLayout.Height - 200);
        }

        public void SetupEventHandlers()
        {
            _capturePhotoButton.Click += async (sender, e) =>
            {
                var bytes = await TakePhoto();
                (Element as CameraView)?.SetPhotoResult(bytes, _liveView.Bitmap.Width, _liveView.Bitmap.Height);
            };
            _liveView.SurfaceTextureListener = this;
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                (Element as CameraView)?.Cancel();
                return false;
            }
            return base.OnKeyDown(keyCode, e);
        }

        public async Task<byte[]> TakePhoto()
        {
            (Element as CameraView)?.BeginTakePhoto();
            _camera.StopPreview();
            var ratio = ((decimal)Height) / Width;
            var image = Bitmap.CreateBitmap(_liveView.Bitmap, 0, 0, _liveView.Bitmap.Width, (int)(_liveView.Bitmap.Width * ratio));
            byte[] imageBytes;
            using (var imageStream = new System.IO.MemoryStream())
            {
                await image.CompressAsync(Bitmap.CompressFormat.Jpeg, 50, imageStream);
                image.Recycle();
                imageBytes = imageStream.ToArray();
            }
            _camera.StartPreview();
            return imageBytes;
        }

        private void StopCamera()
        {
            _camera.StopPreview();
            _camera.Release();
        }

        private void StartCamera()
        {
            _camera.SetDisplayOrientation(90);
            _camera.StartPreview();
        }


        #region TextureView.ISurfaceTextureListener implementations

        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
#pragma warning disable 618
            _camera = Android.Hardware.Camera.Open();
#pragma warning restore 618
            var parameters = _camera.GetParameters();
            var aspect = height / ((decimal)width);

            // Find the preview aspect ratio that is closest to the surface aspect
            var previewSize = parameters.SupportedPreviewSizes
                                        .OrderBy(s => Math.Abs(s.Width / (decimal)s.Height - aspect))
                                        .First();

            System.Diagnostics.Debug.WriteLine($"Preview sizes: {parameters.SupportedPreviewSizes.Count}");

            parameters.SetPreviewSize(previewSize.Width, previewSize.Height);
            _camera.SetParameters(parameters);

            _camera.SetPreviewTexture(surface);
            StartCamera();
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            StopCamera();
            return true;
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
        }
        #endregion
    }
}
