using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild 
{
    public class DevilFruit 
    {
        // -------------------------------------------------------------------
        // MEMBER VARIABLES
        // -------------------------------------------------------------------
        public string name;
        public string type;
        public int tier;
        public string description;
        public string freeEffect;

        // Default Constructor
        public DevilFruit(string name_, string type_, int tier_, string desc_, 
            string free_) {
            name = name_;
            type = type_;
            tier = tier_;
            description = desc_;
            freeEffect = free_;
        }

    }
}
