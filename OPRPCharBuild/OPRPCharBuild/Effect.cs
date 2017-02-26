using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPRPCharBuild
{
	public class Effect
	{
        // -----------------------------------------------------------------
        // Main Member variables and struct
        // -----------------------------------------------------------------

        public string name;
        public bool general;
        public int cost;
        public int minRank;
        public string desc;

        // Default Constructor
        public Effect(string name_, bool gen_, int cost_, int minRank_, string desc_) {
            name = name_;
            general = gen_;
            cost = cost_;
            minRank = minRank_;
            desc = desc_;
        }
	}
}
