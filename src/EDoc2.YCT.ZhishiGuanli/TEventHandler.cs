using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.ZhishiGuanli
{
    public delegate void TEventHandler<Sender, Args>(Sender sender, Args args);

    public delegate void TEventHandler<Args>(Args args);
}
