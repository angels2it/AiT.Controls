using System;
using Xamarin.Forms;

namespace AiT.Controls.CameraView
{
    public class CameraView : View
    {
        public CameraView()
        {
        }
        public delegate void PhotoResultEventHandler(PhotoResultEventArgs result);

        public event PhotoResultEventHandler OnPhotoResult;
        public void SetPhotoResult(byte[] image, int width = -1, int height = -1)
        {
            OnPhotoResult?.Invoke(new PhotoResultEventArgs(image, width, height));
        }
        public void Cancel()
        {
            OnPhotoResult?.Invoke(new PhotoResultEventArgs());
        }
        public event EventHandler OnBeginTakePhoto;

        public void BeginTakePhoto()
        {
            OnBeginTakePhoto?.Invoke(this, EventArgs.Empty);
        }
    }
}
