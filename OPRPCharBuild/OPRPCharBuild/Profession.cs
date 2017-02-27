using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild 
{
    public class Profession 
    {
        // -------------------------------------------------------------------
        // MEMBER VARIABLES
        // -------------------------------------------------------------------
        public string name;
        public string desc;
        public string bonus;

        // Default Constructor
        public Profession(string name_, string desc_, string bonus_) {
            name = name_;
            desc = desc_;
            bonus = bonus_;
        }
    }
}
