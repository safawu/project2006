using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace project2006.Graphics
{
    [Flags]
    internal enum GraphicsFrom
    {
        Own=1,
        File=2,
        Other=16,
        All = Own|File
    }

    internal enum CacheKeep
    {
        Intern ,
        Extern ,
        Temp
    }

    internal enum TransitionType
    {
        Move,
        Rotate,
        Scale,
        Alpha,
        Color
    }

}
