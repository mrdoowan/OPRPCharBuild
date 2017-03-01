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
        private string version;

        // Basic Information Tab
        private string charName;
        private string nickName;
        private int age;
        private string gender;
        private string race;
        private string position;
        private string affiliation;
        private string bounty;
        private string commendation;
        private string rank;
        private string threat;
        private List<string> achievements = new List<string>();
        private List<Profession> profDict = new List<Profession>();

        // Physical Appearance Tab
        private string height;
        private string weight;
        private string hair;
        private string eye;
        private string clothing;
        private string appearance;
        private List<Image> images = new List<Image>();
       
        // Backcground Tab
        private string island;
        private string region;
        private string personality;
        private string history;

        // Combat & Stats Tab
        private string combat;
        private List<Equipment> weaponry = new List<Equipment>();
        private List<Equipment> items = new List<Equipment>();
        private string beli;
        private int SD_Earned;
        private int SDtoSP;
        private bool[] AP = new bool[5] {
            false, false, false, false, false
        };
        private int usedFortune;
        private Stats stats;
        private DevilFruit DF;

        // Traits Tab
        private Dictionary<string, Trait> traitsList = new Dictionary<string, Trait>();

        // Technique Tab
        private Dictionary<string, Technique> techList = new Dictionary<string, Technique>();
        private Dictionary<string, Category> categoryList = new Dictionary<string, Category>();

        // Template Tab
        private string template;
        private string hexColor;
        private string masteryMsg;

        // Default Constructor
        public Character() { }

        // #TODO: Import function

    }
}
