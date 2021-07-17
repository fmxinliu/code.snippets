## C# 构建可热拔插DLL应用 -- [基于AppDomain的"插件式"开发](https://blog.csdn.net/minsenwu/article/details/38391621)

1. 必须在单独的AppDomain中操作 DLL 。
2. 如果操作的是非托管 DLL：
   - 必须使用 __stdcall 规约生成；
   - 若 DLL 有依赖，
     - 调用LoadLibrary加载前，必须设置环境变量；
     - 调用LoadLibraryEx加载时，必须传入LOAD_WITH_ALTERED_SEARCH_PATH = 8。
   - 卸载AppDomain前，必须调用FreeLibrary，否则 DLL 无法同AppDomain一起卸载。
