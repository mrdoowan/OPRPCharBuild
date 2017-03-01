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
        public int genNum;
        public int profNum;
        public string desc;

        // Full initialized Constructor
        public Trait(string name_, int gen_, int prof_, string desc_) {
            name = name_;
            genNum = gen_;
            profNum = prof_;
            desc = desc_;
        }

        public int getTotalTraits() {
            return genNum + profNum;
        }
    }

    // To keep track of use of Special Traits
    public class SpTrait
    {
        public string name;
        public int usedTP;
        public int totalTP;

        public SpTrait(string name_, int used_, int tot_) {
            name = name_;
            usedTP = used_;
            totalTP = tot_;
        }
    };
}
