

using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignExchange.Interfaces
{
    public interface IConfig
    {
       // string GetLocalFilePath(string filename);

        //ISQLitePlatform Platform { get; }

        string GetLocalFilePath(string filename);
    }

}
