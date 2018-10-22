using CDCMetal.Data;
using CDCMetal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCMetal.BLL
{
    public class AccountBLL
    {
        public bool VerificaPassword(string user, string password, out CDCDS.USR_USERRow userRow)
        {
            userRow = null;
            user = user.ToUpper();
            password = password.ToUpper();

            using (CDCMetalBusiness bCDC = new CDCMetalBusiness())
            {
                CDCDS ds = new CDCDS();
                bCDC.LeggiUtente(ds, user);

                userRow = ds.USR_USER.Where(x => x.UIDUSER.Trim() == user).FirstOrDefault();
                if (userRow == null)
                    return false;

                if (userRow.PWDUSER.ToUpper() == password)
                    return true;
                return false;
            }
        }
    }
}
