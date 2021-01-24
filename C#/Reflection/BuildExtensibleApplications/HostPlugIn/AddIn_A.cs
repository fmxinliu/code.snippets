using System;
using HostSDK;

/// <summary>
/// 依托公共类型，插件自定义类型
/// </summary>
namespace HostPlugIn {
    public class AddIn_A : IAddIn {
        public String DoSomething(Int32 x) {
            return "AddIn_A: " + x.ToString();
        }
    }
}
