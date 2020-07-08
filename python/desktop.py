import cv2

cap = cv2.VideoCapture(0)

while True:
    ret, frame = cap.read()
    cv2.imshow("Webcam", frame)
    if cv2.waitKey(1) & 0xFF == 27: # use ESC to quit
        break

cap.release()
cv2.destroyAllWindows()


