import http.server
import socketserver
from time import sleep
import cv2
import threading 

PORT = 2020

pageData = "<!DOCTYPE>" + \
            "<html>" + \
            "  <head>" + \
            "    <title>HttpListener Example</title>" + \
            "  </head>" + \
            "  <body>" + \
            "<img id=\"image\"/>"+ \
            " <script type=\"text/javascript\">var image = document.getElementById('image');function refresh() {image.src = \"/image?\" + new Date().getTime();image.onload= function(){setTimeout(refresh, 30);}}refresh();</script>   "+ \
            "  </body>" + \
            "</html>"

class MyHandler(http.server.BaseHTTPRequestHandler):
    def do_GET(self):
        
        if self.path == '/':
            self.send_response(200)
            self.send_header("Content-type", "text/html")
            self.end_headers()
            self.wfile.write(bytes(pageData, "utf8"))
        elif self.path.startswith('/image'):
            self.send_response(200)
            self.send_header("Content-type", "image/jpeg")
            self.end_headers()

            # ret, frame = cap.read()
            # _, jpg = cv2.imencode(".jpg", frame)

            self.wfile.write(jpg)
        else:
            self.send_response(404)
        
# continuously get frames from webcam
cap = cv2.VideoCapture(0)  
ret, frame = cap.read()
_, jpg = cv2.imencode(".jpg", frame)
  
class FrameThread (threading.Thread):
    def __init__(self):
        threading.Thread.__init__(self)
        self.isRunning = True

    def run(self):
        global jpg, cap

        while self.isRunning:
            ret, frame = cap.read()
            _, jpg = cv2.imencode(".jpg", frame)
            sleep(0.03)

        print("Quit thread")

frame_thread = FrameThread()
frame_thread.start()

# run the server
with socketserver.TCPServer(("", PORT), MyHandler) as httpd:
    print("Serving at port ", PORT)
    try:
        httpd.serve_forever()
    except:
        pass

# quit the app
print('Server is stopped')
frame_thread.isRunning = False
frame_thread.join()
cap.release()

