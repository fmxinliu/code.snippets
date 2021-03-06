using System;
using System.Linq.Expressions;
using System.Reflection;
using Host.DllLoader;

namespace Host.DllLoader_2 {
    /// <summary>
    /// 不依赖于接口，加载并导出托管 DLL 中的函数
    /// </summary>
    sealed class RemoteLoader : MarshalByRefObject, IDllLoader {
        private Object obj;
        private Type type;

        public RemoteLoader(String assemblyFile, String typeName) {
            this.Load(assemblyFile, typeName);
        }

        #region DLL初始化
        public Object Load(String assemblyFile, String typeName) {
            var assembly = Assembly.LoadFrom(assemblyFile);
            type = assembly.GetType(typeName);
            obj = Activator.CreateInstance(type);
            return obj;
        }

        public void Unload() { }
        #endregion

        #region 函数导出
        public void RunLib(String methodName = "RunLib") {
            var lambda = Expression.Lambda<Action>(
                    Expression.Call(Expression.Constant(obj), type.GetMethod(methodName)), null);
            lambda.Compile()();
        }

        public String GetVersion(String methodName = "GetVersion") {
            var lambda = Expression.Lambda<Func<String>>(
                    Expression.Call(Expression.Constant(obj), type.GetMethod(methodName)), null);
            return lambda.Compile()();
        }

        public Int32 AddInt32(Int32 a, Int32 b, String methodName = "AddInt32") {
            var lambda = Expression.Lambda<Func<Int32>>( // 表达式方式调用，可不依赖于接口
                    Expression.Call(
                        Expression.Constant(obj),
                        type.GetMethod(methodName),
                        Expression.Constant(a),
                        Expression.Constant(b)
                    )
                );
            return lambda.Compile()();
        }
        #endregion
    }
}
