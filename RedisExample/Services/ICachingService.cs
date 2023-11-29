﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisExample.Services
{
    public interface ICachingService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key,T value,DateTimeOffset expirationTime);
        object RemoveData(string key);
    }
}
