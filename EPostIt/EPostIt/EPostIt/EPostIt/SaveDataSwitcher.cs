using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPostIt
{
    class SaveDataSwitcher
    {
        public static string savedText="";
        public static bool isSwitchType=false;
        public static void TransferData(string s)
        {
            savedText = s;
            isSwitchType = true;
        }
        
    }
}
