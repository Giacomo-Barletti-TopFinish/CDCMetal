using CDCMetal.Data;
using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.BLL
{
    public class CDCBLL
    {

        public List<DateTime> LeggiDateCollaudo()
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                return bCDCMetal.LeggiDateRiferimento();
            }
        }

        public void LeggiDateCollaudo(CDCDS ds, DateTime dataSelezionata)
        {
            using (CDCMetalBusiness bCDCMetal = new CDCMetalBusiness())
            {
                bCDCMetal.FillCDC(ds, dataSelezionata);
            }
        }
    }
}
