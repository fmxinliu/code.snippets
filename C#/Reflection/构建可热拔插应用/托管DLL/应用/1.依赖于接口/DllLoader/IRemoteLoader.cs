using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host.DllLoader {
    interface IRemoteLoader {
        Object Load();
        void Unload();
    }
}
