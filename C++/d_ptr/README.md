## d指针
### 应用
```
CuteApp.exe 链接到 WidgetLib.dll，WidgetLib.dll 从 v1.0 升级到 v1.1。
1. 二进制兼容： CuteApp.exe 可直接运行在v1.1版本。
2. 源代码兼容： CuteApp.exe 源程序不需要额外修改，重新编译能运行在v1.1版本。
```
### 案例
```
类库升级
===
v1.0 -> v1.1
v2.0 -> v2.1

相同点：新增一个数据字段
不同点：v1.x 数据定义在.h文件
       v2.x 数据封装到.cpp文件
```
```
构建
===
1.编译Lib: v1.0、v1.1、v2.0、v2.1
2.编译App: link v1.0 生成 A 版本
           link v2.0 生成 B 版本
           link v2.1 生成 C 版本
```
```
验证
===
1. A + v1.0    -
2. A + v1.1 ->   Release崩溃，不兼容
3. B + v2.0    -
4. B + v2.1 ->   可直接运行，二进制兼容
5. C + v2.1 ->   可直接运行，源代码兼容
```
### 多继承结构d指针优化前后对比
![多继承结构d指针优化前后对比](多继承结构d指针优化前后对比.png)