﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEA.Core.ProblemModels;

namespace MOEA.Core.ComponentModels
{
    public interface ISolution
    {
        ISolution Clone();
    }
}
