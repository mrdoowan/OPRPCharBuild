using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For development history
// Adding Gains/Losses

namespace OPRPCharBuild
{
    public class Source
    {
        public DateTime date;
        public bool noDate;    // To indicate if there's a date
        public string title;
        public string URL;
        public int SD;
        public int beli;
        public string notes;

        // Ctor
        public Source(DateTime date_, bool noDate_, string title_, string URL_, 
            int SD_, int beli_, string notes_) {
            date = date_;
            noDate = noDate_;
            title = title_;
            URL = URL_;
            SD = SD_;
            beli = beli_;
            notes = notes_;
        }
    }
}
