using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.Entities
{
    public class DataCollaudo
    {
        public DateTime Data;
        public string Brand;

        public DataCollaudo(string Brand, DateTime Data)
        {
            this.Brand = Brand;
            this.Data = Data;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Data.ToShortDateString(), Brand);
        }
    }
}
