using System;
using OpenCvSharp;

namespace Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoCapture capture = new VideoCapture(0);
            using (Window window = new Window("Webcam"))
            {
                using (Mat image = new Mat())
                {
                    while (true)
                    {
                        capture.Read(image);
                        if (image.Empty()) break;
                        window.ShowImage(image);
                        int key = Cv2.WaitKey(30);
                        if (key == 27) break;
                    }
                }
            }
        }
    }
}
