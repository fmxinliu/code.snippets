#ifndef WIDGET_H
#define WIDGET_H

#include "libversion.h"

#if V1_0
  #include "widget_v10.h"
#elif V1_1
  #include "widget_v11.h"
#elif V2_0
  #include "widget_v20.h"
#elif V2_1
  #include "widget_v21.h"
#elif V3_X
  #include "widget_v3x.h"
#endif

#endif // WIDGET_H
