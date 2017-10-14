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
        public string tier;
        public string description;

        // Default Constructor
        public DevilFruit(string name_, string type_, string tier_, string desc_) {
            name = name_;
            type = type_;
            tier = tier_;
            description = desc_;
        }

    }
}
