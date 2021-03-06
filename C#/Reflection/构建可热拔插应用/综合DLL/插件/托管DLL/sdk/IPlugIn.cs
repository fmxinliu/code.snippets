using System;

namespace SDK {
    public interface IPlugIn {
        Int32 AddInt32(Int32 a, Int32 b);
        String GetVersion();
        void RunLib();
    }
}
