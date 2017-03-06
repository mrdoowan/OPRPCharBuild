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
        public string duration;     // Specifically for Professional Buff/Debuff
        public int strength;
        public int speed;
        public int stamina;
        public int accuracy;

        // Default Constructor
        public Stats() {
            statsName = "";
            duration = "";
            strength = 0;
            speed = 0;
            stamina = 0;
            accuracy = 0;
        }

        // Initialize constructor
        public Stats(string name_, string dur_, 
            int str_, int spe_, int sta_, int acc_) {
            statsName = name_;
            duration = dur_;
            strength = str_;
            speed = spe_;
            stamina = sta_;
            accuracy = acc_;
        }
        
        public bool hasStats() {
            return (strength == 0 && speed == 0 && 
                stamina == 0 && accuracy == 0);
        }

        // Generate Tech Stats string
        public string getTechString() {
            string finalStr = "";
            if (strength != 0) {
                if (strength > 0) { finalStr += "+"; }
                finalStr += strength + " Strength, ";
            }
            if (speed != 0) {
                if (speed > 0) { finalStr += "+"; }
                finalStr += speed + " Speed, ";
            }
            if (stamina != 0) {
                if (stamina > 0) { finalStr += "+"; }
                finalStr += stamina + " Stamina, ";
            }
            if (accuracy != 0) {
                if (accuracy > 0) { finalStr += "+"; }
                finalStr += accuracy + " Accuracy";
            }
            if (string.IsNullOrWhiteSpace(finalStr)) {
                finalStr = "N/A";
            }
            return finalStr.TrimEnd(',', ' ');
        }
    }
}
