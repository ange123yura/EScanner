using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Database;
using System.Threading.Tasks;

namespace EScanner.Includes
{
    internal class GlobalVariables
    {
        public static FirebaseClient client = new("https://e-scan-e33b0-default-rtdb.asia-southeast1.firebasedatabase.app/");

        public static string _id,_fn,userkey, _ec,_en,_es,_ee,_ed, id,barcodeid ,purpose, fullname, visitorkey, date, timein, timeout, eventkey,studkey;
    }
}
