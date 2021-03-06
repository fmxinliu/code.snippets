using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Host.DllLoader {
    sealed class RemoteLoader : MarshalByRefObject {
        private Object obj;
        private Type type;

        public RemoteLoader(String assemblyFile, String typeName) {
            var assembly = Assembly.LoadFrom(assemblyFile);
            type = assembly.GetType(typeName);
            obj = Activator.CreateInstance(type);
        }

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
    }
}
