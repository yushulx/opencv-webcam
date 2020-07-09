# Webcam Video Display for Desktop and Web

The repository contains some simple webcam display programs implemented by using different programming languages and OpenCV. The web camera apps are available for both local and remote access via any web browser. 

## Programming Language List
- [JavaScript](#JavaScript)
- [C#](#CSharp)
- [Python](#Python)
- [Golang](#Golang)


## JavaScript


Install [opencv4nodejs](https://www.npmjs.com/package/opencv4nodejs):

    ```
    npm i opencv4nodejs
    ```

#### Desktop

Run:

```
node desktop.js
```

#### Web

1. Run:

    ```
    node web.js
    ```
2. Open `double-click.htm` directly or visit `locahost:2020` in any web browser.

## CSharp

Install [OpenCvSharp](https://github.com/shimat/opencvsharp):

**For Windows**

```
dotnet add package OpenCvSharp4
dotnet add package OpenCvSharp4.runtime.win
```

### Desktop
Run:

```
dotnet restore
dotnet run
```

### Web

1. Run:

    ```
    dotnet restore
    dotnet run
    ```

2. Visit `locahost:2020` in any web browser.

## Python

Install [OpenCV Python](https://pypi.org/project/opencv-python/):

```
pip install opencv-python
```

### Desktop
Run:

```
python desktop.py
```

### Web
1. Run:

    ```
    python web.py
    ```

2. Visit `locahost:2020` in any web browser.

## Golang
Install [gocv](https://gocv.io/getting-started):

```
go get -u -d gocv.io/x/gocv
```

**For Windows**

1. Install MinGW-W64 [x86_64-7.3.0-posix-seh-rt_v5-rev2](https://sourceforge.net/projects/mingw-w64/files/Toolchains%20targetting%20Win32/Personal%20Builds/mingw-builds/7.3.0/)
2. Install [CMake](https://cmake.org/download/)
3. Build the OpenCV module:

    ```
    chdir %GOPATH%\src\gocv.io\x\gocv
    win_build_opencv.cmd
    ```
4. Add `C:\opencv\build\install\x64\mingw\bin` to your system path.



### Desktop
Run:

```
go run desktop.go
```

### Web
1. Run:

    ```
    go run web.go
    ```

2. Visit `locahost:2020` in any web browser.
