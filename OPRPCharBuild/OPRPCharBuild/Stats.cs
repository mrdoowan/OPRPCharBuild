using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild 
{
    public class Stats
    {
        // --------------------------------------------------------------------
        // MEMBER VARIABLES
        // --------------------------------------------------------------------

        public int strength;
        public int speed;
        public int stamina;
        public int accuracy;

        // Default Constructor
        public TechStats() {
            strength = 0;
            speed = 0;
            stamina = 0;
            accuracy = 0;
        }

        // Initialize constructor
        public TechStats(int str_, int spe_, int sta_, int acc_) {
            strength = str_;
            speed = spe_;
            stamina = sta_;
            accuracy = acc_;
        }
        
    }
}
