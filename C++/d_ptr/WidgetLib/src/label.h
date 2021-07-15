#ifndef LABEL_H
#define LABEL_H

#include "libversion.h"

#if V1_0 || V1_1
  #include "label_v1x.h"
#elif V2_0 || V2_1
  #include "label_v2x.h"
#endif


#endif // LABEL_H
