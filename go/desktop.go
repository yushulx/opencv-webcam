package main

import (
	"fmt"

	"gocv.io/x/gocv"
)

func main() {
	webcam, _ := gocv.OpenVideoCapture(0)
	window := gocv.NewWindow("Webcam")
	img := gocv.NewMat()

	for {
		webcam.Read(&img)
		window.IMShow(img)
		key := window.WaitKey(10)
		if key == 27 { // ESC
			break
		}
	}

	fmt.Println("Exit")

	// must call close() to terminate the program
	webcam.Close()
}
