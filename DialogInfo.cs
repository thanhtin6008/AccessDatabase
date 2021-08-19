using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class DialogInfo
    {
        public string FileName { get; set; }
        public string PathFile { get; set; }
        public string FullPath { get; set; }
        public string Pathtemp { get; set; }

        public DialogInfo()
        {
            FileName = "";
            PathFile = "";
            Pathtemp = "";
            FullPath = "";
        }
    }
}
