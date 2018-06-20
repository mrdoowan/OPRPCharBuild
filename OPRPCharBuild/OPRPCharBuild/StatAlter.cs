using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild 
{
    public class StatAlter 
    {
        public const int BUFF = 0;
        public const int DEBUFF = 1;
        public const int STANCE = 2;

        public int type;            // Buff or Debuff?
        public bool majorBuff;      // Major buff?
        public bool durTable;       // Does this follow a duration?

        public StatAlter(int type_, bool major_, bool dura_) {
            type = type_;
            majorBuff = major_;
            durTable = dura_;
        }
    }
}
