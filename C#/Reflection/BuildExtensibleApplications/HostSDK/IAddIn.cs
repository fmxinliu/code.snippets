using System;

/// <summary>
/// host 和 plugin 公共类型
/// </summary>
namespace HostSDK {
    public interface IAddIn {
        String DoSomething(Int32 x);
    }
}
