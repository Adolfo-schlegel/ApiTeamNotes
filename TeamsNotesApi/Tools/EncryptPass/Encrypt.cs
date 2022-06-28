using Microsoft.VisualBasic;
using TeamsNotesApi.Tools.Interface;

namespace TeamsNotesApi.Tools.EncryptPass
{
    public class Encrypt : IEncrypt
    {
        public string encrypted(string pstr_ds_password)
        {
            int lint_pos;
            long llng_suma;
            string lstr_res = "";

            if (pstr_ds_password != "")
            {
                llng_suma = 0L;

                if (true)                
                    pstr_ds_password = pstr_ds_password.ToUpper();
                
                var loopTo = pstr_ds_password.Length;
                for (lint_pos = 1; lint_pos <= loopTo; lint_pos++)
                    llng_suma = (long)(llng_suma + Strings.Asc(Strings.Mid(pstr_ds_password, lint_pos, 1)));

                llng_suma = llng_suma * 121269L;
                lstr_res = llng_suma.ToString();

                return lstr_res;
            }

            return null;
        }
    }
}
