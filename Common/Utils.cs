﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Common
{
    public class Utils
    {
        public String createUUID()
        {
            var uuid = Guid.NewGuid().ToString();
                return uuid; ;
        }
    }
}
