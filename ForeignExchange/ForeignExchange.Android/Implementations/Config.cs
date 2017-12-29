
using System.IO;

using System.Runtime.CompilerServices;
using Xamarin.Forms;
using ForeignExchange.Interfaces;




[assembly: Xamarin.Forms.Dependency(typeof(ForeignExchange.Droid.Implementations.Config))]

namespace ForeignExchange.Droid.Implementations
{
    public class Config :IConfig
    {

        public string GetLocalFilePath(string filename)
        {
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}