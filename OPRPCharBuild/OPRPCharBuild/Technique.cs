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
        public string branchTech;
        public int branchRank;
        public string type;
        public string range;
        public Stats stats;
        public bool autoCalc;
        public bool NApower;
        public List<Effect> effects;
        public string power;
        public List<bool> checkBoxDF;
        public List<bool> cyborgBoosts;
        public string note;
        public string customNote;
        public string description;

        // Constructor
        public Technique(Rokushiki roku_, int rank_, int reg_, int sp_,
            string rankTr_, string spTr_, string brTech_, int brRank_,
            string type_, string range_, Stats stats_, bool auto_,
            bool NApow_, List<Effect> eff_, string pow_, List<bool> DFs_,
            List<bool> cyborg_, string note_, string custom_, string desc_) {
            roku = roku_;
            rank = rank_;
            regTP = reg_;
            spTP = sp_;
            rankTrait = rankTr_;
            specialTrait = spTr_;
            branchTech = brTech_;
            branchRank = brRank_;
            type = type_;
            range = range_;
            stats = stats_;
            autoCalc = auto_;
            NApower = NApow_;
            effects = eff_;
            power = pow_;
            checkBoxDF = DFs_;
            cyborgBoosts = cyborg_;
            note = note_;
            customNote = custom_;
            description = desc_;
        }
    }
}
