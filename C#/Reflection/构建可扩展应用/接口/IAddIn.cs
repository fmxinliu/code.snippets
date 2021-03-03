using System;

/// <summary>
/// host 和 plugin 公共类型
/// </summary>
namespace SDK {
    public interface IAddIn {
        String DoSomething(Int32 x);
    }
}
