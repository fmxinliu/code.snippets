using System;

namespace Host.DllLoader {
    public interface IDllLoader {
        Object Load(String assemblyFile, String typeName);
        void Unload();
    }
}
