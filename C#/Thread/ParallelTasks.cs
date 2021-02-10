using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest {
    class ParallelTasks {
        public static void Test() {
            DirectoryBytes(AppDomain.CurrentDomain.BaseDirectory, "*", SearchOption.TopDirectoryOnly);
        }

        static Int64 DirectoryBytes(String path, String searchPattern, SearchOption searchOption) {
            Int64 totalSize = 0;
            var files = Directory.EnumerateFiles(path, searchPattern, searchOption);
            Parallel.ForEach<String, Int64>(
                files,
                () => { // localInit: 执行前初始化
                    return 0; /// localTotalSize = 0
                },

                (file, pls, id, localTotalSize) => { // body: 主体
                    Int64 fileLength = 0;
                    FileStream fs = null;
                    try {
                        fs = File.OpenRead(file);
                        fileLength = fs.Length;
                    }
                    catch (IOException) { }
                    finally {
                        if (fs != null) {
                            fs.Dispose();
                        }
                    }
                    return localTotalSize + fileLength;
                },

                localTotalSize => { // localFinally: 结束
                    Interlocked.Add(ref totalSize, localTotalSize); /// 同步累加
                });

            return totalSize;
        }
    }
}
