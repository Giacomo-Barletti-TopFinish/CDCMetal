﻿using CDCMetal.Entities;
using CDCMetal.Security;
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
//        public CDCDS DS;
   //     public CDCDS.USR_USERRow Utente;
        public Utente Utente;
        public string PathCollaudo;
        public string PathSchedeTecniche;
        public string PathAnalisiPiombo;
        public string PathRefertiLaboratorio { private get; set; }
        public string StrumentoColore;
        public string StrumentoSpessore;
        public string GetPathRefertiLaboratorio(string Brand)
        {
            return string.Format(PathRefertiLaboratorio, Brand);
        }
    }
}
