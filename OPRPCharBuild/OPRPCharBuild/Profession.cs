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
        public string custom;
        public bool primary;
        public string desc;
        public string bonus;

        // Constructor for database
        public Profession(string name_, string desc_, string bonus_) {
            name = name_;
            primary = false;
            custom = "";
            desc = desc_;
            bonus = bonus_;
        }

        // Constructor when making a new Profession
        public Profession(string name_, bool pri_, string custom_, 
            string desc_, string bonus_) {
            name = name_;
            primary = pri_;
            custom = custom_;
            desc = desc_;
            bonus = bonus_;
        }
    }
}
