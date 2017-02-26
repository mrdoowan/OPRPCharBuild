using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild
{
    public class Technique
    {
        public Rokushiki roku;
        public int rank;
        public int regTP;
        public int spTP;
        public string rankTrait;
        public string specialTrait;
        public string tech_Branch;
        public int rankBranch;
        public string type;
        public string range;
        public TechStats stats;
        public bool NA_power;
        public List<Effect> effects;
        public string power;
        public List<bool> DF_checkBox;
        public List<bool> Cyborg_Boosts;
        public string note;
        public string desc;

        // Constructor
        public Technique() {

        }
    }
}
