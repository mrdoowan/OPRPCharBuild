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
        // --------------------------------------------------------------------
        // MEMBER VARIABLES
        // --------------------------------------------------------------------
        public string version;

        // Basic Information Tab
        public string charName;
        public string nickName;
        public int age;
        public string gender;
        public string race;
        public string position;
        public string affiliation;
        public string bounty;
        public string commendation;
        public string rank;
        public string threat;
        public List<string> achievements = new List<string>();
        public Dictionary<string, bool> profDict = new Dictionary<string, bool>();

        // Physical Appearance Tab
        private string height;
        private string weight;
        private string hair;
        private string eye;
        private string clothing;
        private string appearance;
        public List<Image> images = new List<Image>();

        // Backcground Tab
        private string island;
        private string region;
        private string personality;
        private string history;

        // Combat & Stats Tab
        public string combat;
        public List<Equipment> weaponry = new List<Equipment>();
        public List<Equipment> items = new List<Equipment>();
        public string beli;
        public int SD_Earned;
        public int SDtoSP;
        public List<bool> AP = new List<bool>();
        public int usedFortune;
        public Stats stats;
        public DevilFruit DF;

        // Traits Tab
        public List<Trait> traitsList = new List<Trait>();

        // Technique Tab
        public List<Technique> techList = new List<Technique>();
        public List<Category> categoryList = new List<Category>();

        // Default Constructor
        public Character() { }

        // #TODO: Import function

    }
}
