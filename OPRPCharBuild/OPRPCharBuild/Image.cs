using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild 
{
    public class Image 
    {
        // -------------------------------------------------------------------
        // MEMBER VARIABLES
        // -------------------------------------------------------------------
        public string label;
        public string url;
        public string full_res;     // Only "Yes" and "No" for ListView
        public string img_width;
        public string img_height;

        // Default Constructor
        public Image(string label_, string url_, string fullres_, string imgWidth_, string imgHeight_) {
            label = label_;
            url = url_;
            full_res = fullres_;
            img_width = imgWidth_;
            img_height = imgHeight_;
        }
    }
}
