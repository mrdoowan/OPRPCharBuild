using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This will only be used to load Traits for the menu. 
// TraitName will be mainly used for outside functions

namespace OPRPCharBuild
{
    public class Trait
    {
        public string name;
        public string custom;
        public int genNum;
        public int profNum;
        public string desc;

        // Constructor for database
        public Trait(string name_, int gen_, int prof_, string desc_) {
            name = name_;
            custom = "";
            genNum = gen_;
            profNum = prof_;
            desc = desc_;
        }

        // Full initialized Constructor
        public Trait(string name_, string custom_, int gen_, int prof_, string desc_) {
            name = name_;
            custom = custom_;
            genNum = gen_;
            profNum = prof_;
            desc = desc_;
        }

        public int getTotal() {
            return genNum + profNum;
        }

        // Return name if custom is blank
        // Return custom otherwise
        public string getName() {
            return (string.IsNullOrWhiteSpace(custom)) ? name : custom;
        }
    }

    // To keep track of use of Special Traits
    public class SpTrait
    {
        public string name;
        public string custom; // Will either be same name or custom
        public int usedTP;
        public int totalTP;

        public SpTrait(string name_, string custom_, int used_, int tot_) {
            name = name_;
            custom = custom_;
            usedTP = used_;
            totalTP = tot_;
        }

        // For the Character.cs
        public SpTrait(string name_, string custom_, int tot_) {
            name = name_;
            custom = custom_;
            usedTP = 0;
            totalTP = tot_;
        }

        public string getName() {
            return (string.IsNullOrWhiteSpace(custom)) ? name : custom;
        }
    };
}
