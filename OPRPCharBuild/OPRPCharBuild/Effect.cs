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

        // Constructor for just the name
        public Effect(string name_) {
            name = name_;
            general = true;
            cost = 0;
            minRank = 0;
            desc = "";
        }

        // Overrides Removal
        public override bool Equals(object obj) {
            if (obj == null) return false;
            Effect objAsEffect = obj as Effect;
            if (objAsEffect == null) return false;
            else return Equals(objAsEffect);
        }

        public override int GetHashCode() {
            return name.GetHashCode();
        }

        public bool Equals(Effect other) {
            if (other == null) return false;
            return (name.Equals(other.name));
        }
    }
}
