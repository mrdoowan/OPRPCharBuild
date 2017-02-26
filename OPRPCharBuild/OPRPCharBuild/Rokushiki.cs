using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild
{
	public class Rokushiki
	{
        // ----------------------------------------------------------------------------------------
        // MEMBER VARIABLES
        // ----------------------------------------------------------------------------------------

        public string name;
        public int baseRank;
        public string type;
        public string range;
        public int basePower;
        public int str;
        public int spe;
        public int sta;
        public int acc;
        public string desc;

        // Default Constructor
        public Rokushiki(string name_, int rank_, string type_, string range_, int power_,
                int str_, int spe_, int sta_, int acc_, string desc_) {
            name = name_;
            baseRank = rank_;
            type = type_;
            range = range_;
            basePower = power_;
            str = str_;
            spe = spe_;
            sta = sta_;
            acc = acc_;
            desc = desc_;
        }
		
	}
}