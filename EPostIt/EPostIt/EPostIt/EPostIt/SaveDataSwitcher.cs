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
