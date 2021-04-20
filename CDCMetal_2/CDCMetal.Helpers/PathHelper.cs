using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.Helpers
{
    public class PathHelper
    {
        public static string VerificaPathPerScritturaOracle(string path)
        {
            return path.Replace("'", string.Empty);
        }
    }
}
