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

        public int type;
        public bool majorBuff;
        public bool stdTable;
        public bool duration;       // Does this follow a duration?

        public StatAlter(int type_, bool major_, bool std_, bool prof_) {
            type = type_;
            majorBuff = major_;
            stdTable = std_;
            duration = prof_;
        }
    }
}
