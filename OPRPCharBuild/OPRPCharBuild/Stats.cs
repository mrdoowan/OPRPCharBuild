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

        public string statsName;    // Specifically for comboBox_StatOpt in Techniques
        public int strength;
        public int speed;
        public int stamina;
        public int accuracy;

        // Default Constructor
        public Stats() {
            statsName = "";
            strength = 0;
            speed = 0;
            stamina = 0;
            accuracy = 0;
        }

        // Initialize constructor
        public Stats(string name_, int str_, int spe_, int sta_, int acc_) {
            statsName = name_;
            strength = str_;
            speed = spe_;
            stamina = sta_;
            accuracy = acc_;
        }
        
        public bool hasStats() {
            return (strength == 0 && speed == 0 && 
                stamina == 0 && accuracy == 0);
        }
    }
}
