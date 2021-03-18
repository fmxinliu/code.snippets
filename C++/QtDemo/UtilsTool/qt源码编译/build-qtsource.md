#### Qt源码编译

##### 1.源码下载
```
git clone https://code.qt.io/qt/qt5.git
cd qt5
git checkout 版本
git submodule update --init --recursive
```
##### 2.编译工具

`VS`、`ActivePerl`、`Python 2.x`

##### 3.配置

(1) 运行`VS`命令提示行，进入`Qt`源码目录。
(2) 执行命令：
```
configure
-opensource 编译开源版

-platform win32-msvc 目标库运行平台
-prefix E:\\QtSource\build 编译到指定目录
-debug-and-release  编译debug+release
-force-debug-info 强制生成调试信息（release版本也生成.pdb文件）
-mp 启动多核编译

-c++std c++11 支持C++11

/*不编译模块*/
-nomake examples
-nomake tests

//-opengl dynamic 指定opengl库
-no-opengl
-no-angle

/*跳过模块*/
-skip qtquick1
-skip qtquickcontrols
-skip qtsensors
-skip qtwebkit

-skip qtmultimedia
-skip qtsensors
-skip qtwebengine ////有需要依赖的库
-skip qtgamepad
-skip qtlocation
//-skip qtserialbus 串行总线协议
-confirm-license
```
##### 4.编译安装

`make && make install`

等待`configure`执行完成，输入`nmake`开始编译，如果没有错误，输入`nmake install`等待结束即可。

