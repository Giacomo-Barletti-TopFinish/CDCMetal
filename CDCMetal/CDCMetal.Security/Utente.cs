using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.Security
{
    public class Utente
    {
        private UserPrincipal userPrincipal;
        public bool AbilitaCartelle { get; private set; }
        public bool AbilitaConformita { get; private set; }
        public bool AbilitaDimensioni { get; private set; }
        public bool AbilitaEtichette { get; private set; }
        public bool AbilitaExcel { get; private set; }
        public bool AbilitaLaboratorio { get; private set; }
        public bool AbilitaPDF { get; private set; }

        public string DisplayName { get; private set; }
        public string FULLNAMEUSER { get; private set; }

        public string IDUSER { get; private set; }

        public Utente()
        {
            string username = Environment.UserName;

            AbilitaCartelle = false;
            AbilitaConformita = false;
            AbilitaDimensioni = false;
            AbilitaEtichette = false;
            AbilitaExcel = false;
            AbilitaLaboratorio = false;
            AbilitaPDF = false;
            FULLNAMEUSER = "Sconosciuto";
            DisplayName = "Sconosciuto";
            IDUSER = string.Empty;

            //  PrincipalContext domainctx = new PrincipalContext(ContextType.Domain,"example","DC=example,DC=com");
            PrincipalContext domainctx = new PrincipalContext(ContextType.Domain);

            userPrincipal = UserPrincipal.FindByIdentity(domainctx, IdentityType.SamAccountName, username);

            if (userPrincipal != null)
            {
                AbilitaCartelle = userPrincipal.IsMemberOf(domainctx, IdentityType.Name, GruppiDominio.Cartelle);
                AbilitaConformita = userPrincipal.IsMemberOf(domainctx, IdentityType.Name, GruppiDominio.Conformita);
                AbilitaDimensioni = userPrincipal.IsMemberOf(domainctx, IdentityType.Name, GruppiDominio.Dimensioni);
                AbilitaEtichette = userPrincipal.IsMemberOf(domainctx, IdentityType.Name, GruppiDominio.Etichette);
                AbilitaExcel = userPrincipal.IsMemberOf(domainctx, IdentityType.Name, GruppiDominio.Excel);
                AbilitaLaboratorio = userPrincipal.IsMemberOf(domainctx, IdentityType.Name, GruppiDominio.Laboratorio);
                AbilitaPDF = userPrincipal.IsMemberOf(domainctx, IdentityType.Name, GruppiDominio.PDF);
                FULLNAMEUSER = userPrincipal.DisplayName.Length > 50 ? userPrincipal.DisplayName.Substring(0, 50) : userPrincipal.DisplayName;
                IDUSER = userPrincipal.UserPrincipalName;
                DisplayName = userPrincipal.DisplayName;
            }
        }
    }
}
