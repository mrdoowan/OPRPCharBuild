using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild 
{
    public class Equipment 
    {
        // -------------------------------------------------------------------
        // MEMBER VARIABLES
        // -------------------------------------------------------------------
        public string name;
        public string desc;

        // Default Constructor
        public Equipment(string name_, string desc_) {
            name = name_;
            desc = desc_;
        }
    }
}
