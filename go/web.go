package main

import (
	"fmt"
	"log"
	"net/http"
	"strings"

	"gocv.io/x/gocv"
)

var webcam *gocv.VideoCapture
var img gocv.Mat

func handler(w http.ResponseWriter, r *http.Request) {
	if r.URL.Path == "/" {
		pageData := "<!DOCTYPE>" +
			"<html>" +
			"  <head>" +
			"    <title>HttpListener Example</title>" +
			"  </head>" +
			"  <body>" +
			"<img id=\"image\"/>" +
			" <script type=\"text/javascript\">var image = document.getElementById('image');function refresh() {image.src = \"/image?\" + new Date().getTime();image.onload= function(){setTimeout(refresh, 30);}}refresh();</script>   " +
			"  </body>" +
			"</html>"

		w.Header().Set("Content-Type", "text/html")
		w.Write([]byte(pageData))
	} else if strings.HasPrefix(r.URL.Path, "/image") {
		webcam.Read(&img)
		jpg, _ := gocv.IMEncode(".jpg", img)
		w.Write(jpg)
	} else {
		fmt.Fprintf(w, "Page Not Found")
	}

}

func main() {
	fmt.Println("Running at port 2020...")
	webcam, _ = gocv.OpenVideoCapture(0)
	img = gocv.NewMat()
	http.HandleFunc("/", handler)
	log.Fatal(http.ListenAndServe(":2020", nil))
	webcam.Close()
}
