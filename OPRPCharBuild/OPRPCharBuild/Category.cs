using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild 
{
    public class Category 
    {
        // -------------------------------------------------------------------
        // MEMBER VARIABLES
        // -------------------------------------------------------------------
        public int rowBegin;
        public int rowEnd;
        public string name;

        // Default Constructor
        public Category(int beg_, int end_, string name_) {
            rowBegin = beg_;
            rowEnd = end_;
            name = name_;
        } 
    }
}
