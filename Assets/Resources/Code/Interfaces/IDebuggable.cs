using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.Code.Interfaces
{
    public interface IDebuggable
    {
        bool InDebugMode { get; set; }

        DebugLogger Logger { get; }
    }
}
