using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using OpenCvSharp;

namespace Web
{
    class Program
    {
        public static HttpListener listener;
        public static string url = "http://localhost:2020/";
        public static string pageData = 
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>HttpListener Example</title>" +
            "  </head>" +
            "  <body>" +
            "<img id=\"image\"/>"+
            " <script type=\"text/javascript\">var image = document.getElementById('image');function refresh() {image.src = \"/image?\" + new Date().getTime();image.onload= function(){setTimeout(refresh, 30);}}refresh();</script>   "+
            "  </body>" +
            "</html>";

        public static VideoCapture capture = new VideoCapture(0);

        // https://gist.github.com/define-private-public/d05bc52dd0bed1c4699d49e2737e80e7

        public static async Task HandleIncomingConnections()
        {
            while (true)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                // Print out some info about the request
                // Console.WriteLine(req.Url.ToString());
                // Console.WriteLine(req.HttpMethod);
                // Console.WriteLine(req.UserHostName);
                // Console.WriteLine(req.UserAgent);
                // Console.WriteLine();

                if ((req.HttpMethod == "GET") && (req.Url.AbsolutePath.StartsWith("/image"))) {

                    resp.ContentType = "image/jpeg";
                    using (Mat image = new Mat()) 
                    {
                        capture.Read(image); 
                        Cv2.ImEncode(".jpg", image, out var imageData);
                        await resp.OutputStream.WriteAsync(imageData, 0, imageData.Length);
                        resp.Close();
                    }
                }
                else {

                    // Write the response info
                    byte[] data = Encoding.UTF8.GetBytes(pageData);
                    resp.ContentType = "text/html";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;

                    // Write out to the response stream (asynchronously), then close it
                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
                    resp.Close();
                }
            }
        }

        static void Main(string[] args)
        {
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }
    }
}
