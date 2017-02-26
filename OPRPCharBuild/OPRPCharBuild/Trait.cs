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
        // -------------------------------------------------------------------
        // MEMBER VARIABLES
        // -------------------------------------------------------------------

        private string name;
        private int genNum;
        private int profNum;
        private string desc;

        // Constructor
        public Trait(string name_, int gen_, int prof_, string desc_) {
            name = name_;
            genNum = gen_;
            profNum = prof_;
            desc = desc_;
        }

        // -------------------------------------------------------------------
        // Constructor & Functions
        // -------------------------------------------------------------------

        public int getGenNum() {
            return genNum;
        }

        public int getProfNum() {
            return profNum;
        }

        public string getTraitDesc() {
            return desc;
        }
    }
}
