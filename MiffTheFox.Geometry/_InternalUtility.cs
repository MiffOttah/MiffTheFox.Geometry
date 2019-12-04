﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MiffTheFox.Geometry
{
    internal static class _InternalUtility
    {
        internal static void ValidateDouble(double value, string paramName)
        {
            if (double.IsNaN(value) || double.IsInfinity(value)) throw new ArgumentException("Value cannot be NaN or Infinity.", paramName);
        }
    }
}
