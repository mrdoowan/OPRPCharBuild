/*
 * This will contain our entire Character information. 
 * It will also be useful for importing from earlier versions
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild
{
    [Serializable()]
    public class Character
    {
        // ----------------------------------------------------------------------------------------
        // MEMBER VARIABLES
        // ----------------------------------------------------------------------------------------

        public string version;      // What version this is
        public int genTraits;       // Current Gen count
        public int profTraits;      // Current Prof count
        // Hold a list of Professions that is "bound" to the ListView. 
        // String is the name, and bool is if it's primary or not.
        public Dictionary<string, bool> profDict = new Dictionary<string, bool>();
        public List<Trait> traitsList = new List<Trait>();
        // #TODO: TECHNIQUE LIST


        // Constructor
        public Character() {

        }


    }
}
