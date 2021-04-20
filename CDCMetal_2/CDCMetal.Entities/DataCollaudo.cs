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
    public class DataCollaudoSTR
    {
        public string Data;
        public string Brand;

        public DataCollaudoSTR(string Brand, string Data)
        {
            this.Brand = Brand;
            this.Data = Data;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Data, Brand);
        }
    }
    public class DataCollaudoSingle
    {
        public DateTime Data;        

        public DataCollaudoSingle(DateTime Data)
        {           
            this.Data = Data;
        }

        public override string ToString()
        {
            return string.Format("{0}", Data.ToShortDateString());
        }
    }
    public class BrandCollaudoSingle
    {
        public string Brand;

        public BrandCollaudoSingle(string Brand)
        {
            this.Brand = Brand;
        }

        public override string ToString()
        {
            return string.Format("{0}",  Brand);
        }
    }
    public class ColoreSingle
    {
        public string Colore;

        public ColoreSingle(string Colore)
        {
            this.Colore = Colore;
        }

        public override string ToString()
        {
            return string.Format("{0}", Colore);
        }
    }
}
