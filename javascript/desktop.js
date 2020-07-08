const cv = require('opencv4nodejs');

const vCap = new cv.VideoCapture(0);
 
const delay = 10;
while (true) {
  let frame = vCap.read();
  // loop back to start on end of stream reached
  if (frame.empty) {
    vCap.reset();
    frame = vCap.read();
  }
 
  cv.imshow('OpenCV Node.js', frame);
  const key = cv.waitKey(delay); // Press ESC to quit
  if (key == 27) {break;}
}