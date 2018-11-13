using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal
{
    public class CDCContext
    {
        public bool UtenteConnesso;
        public CDCDS DS;
        public CDCDS.USR_USERRow Utente;
        public string PathCollaudo;
        public string PathSchedeTecniche;
        public string PathRefertiLaboratorio;
        public string StrumentoColore;
        public string StrumentoSpessore;
    }
}
