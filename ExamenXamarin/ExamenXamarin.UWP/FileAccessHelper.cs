using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenXamarin.UWP
{
    /// <summary>
    /// Clase FileAccessHelper
    /// </summary>
    class FileAccessHelper
    {
        /// <summary>
        /// El método GetLocalFilePath obtendrá la ruta de la BBDD
        /// </summary>
        /// <param name="filename">Nombre del fichero</param>
        /// <returns></returns>
        public static string GetLocalFilePath(string filename)
        {
            string path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            return System.IO.Path.Combine(path, filename);
        }
    }
}
