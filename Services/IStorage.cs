﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework11._6.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
