const http = require('http'); 
const cv = require('opencv4nodejs')
const wCap = new cv.VideoCapture(0);
wCap.set(cv.CAP_PROP_FRAME_WIDTH, 640);
wCap.set(cv.CAP_PROP_FRAME_HEIGHT, 480);

var img = null;
function capture() {
    var frame = wCap.read()
    if (frame.empty) {
        wCap.reset();
        frame = wCap.read();
    }

    img = cv.imencode('.jpg', frame);
    setTimeout(capture, 30);
}

capture();

var fs=require("fs");
var html = fs.readFileSync("index.htm", "utf8");

var server = http.createServer(function (req, res) {   
    if (req.url === '/' || req.url === '/index.htm') { 
        
        res.writeHead(200, { 'Content-Type': 'text/html' });   
        res.write(html);
        res.end();
    
    }
    else if (req.url.startsWith("/image")) {
        
        res.writeHead(200, { 'Content-Type': 'image/jpeg' });
        res.write(img);
        res.end();
    
    }
    else
        res.end('Invalid Request!');

});

server.listen(2020);

console.log('Node.js web server is running on port 2020...')