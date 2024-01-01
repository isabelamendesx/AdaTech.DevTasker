﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.CodeManager.Model
{
    public enum Status
    {
        BackLog = 0,
        ToDo = 1,
        Doing = 2,
        Review = 3,
        Testing = 4,
        Done = 5,
        Cancelled = 6
    }
}
