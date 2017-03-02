/*
 * This will contain our entire Character information. 
 * It will also be useful for importing from earlier versions
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private int commendation;
        private string rank;
        private string threat;
        private List<string> achieveList = new List<string>();
        private List<Profession> profList = new List<Profession>();

        public void saveCharBasic(string charName_,
            string nickName_,
            int age_,
            string gender_,
            string race_,
            string pos_,
            string aff_,
            string bounty_,
            int comm_,
            string rank_,
            string threat_,
            ListBox achieve_,
            ListView prof_) {

        }

        // Physical Appearance Tab
        private string height;
        private string weight;
        private string hair;
        private string eye;
        private string clothing;
        private string appearance;
        private List<Image> images = new List<Image>();
       
        public void saveCharAppearance(string height_,
            string weight_,
            string hair_,
            string eye_,
            string clothing_,
            string appear_,
            ListView images_) {

        }

        // Backcground Tab
        private string island;
        private string region;
        private string personality;
        private string history;

        public void saveCharBackground(string island_,
            string region_,
            string pers_,
            string history_) {

        }

        // Combat Tab
        private string combat;
        private List<Equipment> weaponry = new List<Equipment>();
        private List<Equipment> items = new List<Equipment>();
        private string beli;
        private DevilFruit DF;

        public void saveCharCombat(string comb_,
            ListView weapon_,
            ListView items_,
            string beli_,
            string DFName_,
            string DFType_,
            string DFTier_,
            string DFDesc_,
            string DFEff_) {

        }

        // Stats Tab
        private int SD_Earned;
        private int SDtoSP;
        private bool[] AP = new bool[5] {
            false, false, false, false, false
        };
        private int usedFortune;
        private Stats stats;

        public void saveCharStats(int SDEarn_,
            int SD2SP_,
            CheckedListBox AP_,
            int usedFort_,
            int str_,
            int spe_,
            int sta_,
            int acc_) {

        }

        // Traits Tab
        private Dictionary<string, Trait> traitsList = new Dictionary<string, Trait>();

        public void saveCharTraits(Dictionary<string, Trait> traits_) {

        }

        // Technique Tab
        private Dictionary<string, Technique> techList = new Dictionary<string, Technique>();
        private Dictionary<string, Category> categoryList = new Dictionary<string, Category>();

        public void saveCharTechs(Dictionary<string, Technique> techs_,
            Dictionary<string, Category> cats_) {

        }

        // Template Tab
        private string template;
        private string color;
        private string masteryMsg;

        public void saveCharTemplate(string temp_,
            string color_,
            string msg_) {

        }

        // Default Constructor
        public Character() { }

        // #TODO: Import function

    }
}
