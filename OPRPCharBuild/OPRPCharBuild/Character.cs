/*
 * This will contain our entire Character information. 
 * It will also be useful for importing from earlier versions
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPRPCharBuild
{
    public class Character
    {
        // --------------------------------------------------------------------
        // MEMBER VARIABLES
        // --------------------------------------------------------------------
        public string filename;
        public string path;
        private string data;
        #region Const Identifiers
        private const string
            // These should all be 5 characters
            // Basic Information Tab
            CHARNAME = "[NAMECH]",
            NICKNAME = "[NAMENI]",
            AGE = "[AGE]",
            GENDER = "[GENDE]",
            RACE = "[RACE]",
            POSITION = "[POSIT]",
            AFFILIATION = "[AFFIL]",
            BOUNTY = "[BOUNT]",
            COMMENDATION = "[COMME]",
            RANK = "[RANK]",
            THREAT = "[THREAT]",
            TAG_STA_ACHIEVE = "<ACHIEVE>",
            TAG_END_ACHIEVE = "</ACHIEVE>",
            TAG_STA_PROF = "<PROF>",
            TAG_END_PROF = "</PROF>",
            PROF_NAME = "[PR_NAME]",
            PROF_PRIM = "[PR_PRIM]",
            PROF_DESC = "[PR_DESC]",
            PROF_BONU = "[PR_BONU]",
            // Physical Appearance Tab
            HEIGHT = "[HEIGH]",
            WEIGHT = "[WEIGH]",
            HAIR = "[HAIR]",
            EYE = "[EYE]",
            CLOTHING = "[CLOTH]",
            APPEARANCE = "[APPEA]",
            TAG_STA_IMAG = "<IMAGE>",
            TAG_END_IMAG = "</IMAGE>",
            IMAG_LABEL = "[IMG_LABE]",
            IMAG_URL = "[IMG_URL]",
            IMAG_FULLRES = "[IMG_FRES]",
            IMAG_IMGWIDTH = "[IMG_WIDT]",
            IMAG_IMGHEIGHT = "[IMG_HEIG]",
            // Background Tab
            ISLAND = "[ISLAN]",
            REGION = "[REGIO]",
            PERSONALITY = "[PERSO]",
            HISTORY = "[HISTO]",
            // Combat Tab
            COMBAT = "[COMBA]",
            TAG_STA_WEAPON = "<WEAPON>",
            TAG_END_WEAPON = "</WEAPON>",
            WEAPON_NAME = "[WPON_NAME]",
            WEAPON_DESC = "[WPON_DESC]",
            TAG_STA_ITEM = "<ITEM>",
            TAG_END_ITEM = "</ITEM>",
            ITEM_NAME = "[ITEM_NAME]",
            ITEM_DESC = "[ITEM_DESC]",
            BELI = "[BELI]",
            DF_NAME = "[DF_NAME]",
            DF_TYPE = "[DF_TYPE]",
            DF_TIER = "[DF_TIER]",
            DF_DESC = "[DF_DESC]",
            DF_EFFE = "[DF_EFFE]",
            // Stats Tab
            SDEARNED = "[SDEAR]",
            SDTOSP = "[SD2SP]",
            APBOOL = "[APBOOL]",            // True,True,False, etc.
            USEDFORTUNE = "[SDFORT]",
            STRENGTH = "[STR]",
            SPEED = "[SPE]",
            STAMINA = "[STA]",
            ACCURACY = "[ACC]",
            // Traits Tab
            TAG_STA_TRAIT = "<TRAIT>",
            TAG_END_TRAIT = "</TRAIT>",
            TRAIT_NAME = "[TR_NAME]",
            TRAIT_GENE = "[TR_GENE]",
            TRAIT_PROF = "[TR_PROF]",
            TRAIT_DESC = "[TR_DESC]",
            // Technique Tab
            TAG_STA_TECH = "<TECH>",
            TAG_END_TECH = "</TECH>",
            TECH_NAME = "[TECH_NAME]",
            TECH_ROKU = "[TECH_ROKU]",
            TECH_RANK = "[TECH_RANK]",
            TECH_REGTP = "[TECH_REGTP]",
            TECH_SPTP = "[TECH_SPTP]",
            TECH_RANKTRAIT = "[TECH_RKTR]",
            TECH_SPTRAIT = "[TECH_SPTR]",
            TECH_SIGTECH = "[TECH_SIG]",
            TECH_BRANCHTECH = "[TECH_BRTECH]",
            TECH_BRANCHRANK = "[TECH_BRRANK]",
            TECH_TYPE = "[TECH_TYPE]",
            TECH_RANGE = "[TECH_RANGE]",
            TECH_STAT_NAME = "[TECH_STAT_NAME]",
            TECH_STAT_DURA = "[TECH_STAT_DURA]",
            TECH_STAT_STR = "[TECH_STAT_STR]",
            TECH_STAT_SPE = "[TECH_STAT_SPE]",
            TECH_STAT_STA = "[TECH_STAT_STA]",
            TECH_STAT_ACC = "[TECH_STAT_ACC]",
            TECH_AUTOCALC = "[TECH_AUTO]",
            TECH_NAPOWER = "[TECH_NA]",
            TAG_STA_EFFECT = "<EFFECT>",
            TAG_END_EFFECT = "</EFFECT>",
            EFFECT_NAME = "[EFF_NAME]",
            EFFECT_GEN = "[EFF_GEN]",
            EFFECT_COST = "[EFF_COST]",
            EFFECT_MINRANK = "[EFF_MIN]",
            EFFECT_DESC = "[EFF_DESC]",
            TECH_POWER = "[TECH_POW]",
            TECH_DF = "[TECH_DF]",          // True,True,False, etc.
            TECH_CYBORG = "[TECH_CYB]",     // True,True,False, etc.
            TECH_NOTE = "[TECH_NOTE]",
            TECH_CUST = "[TECH_CUST]",
            TECH_DESC = "[TECH_DESC]",
            // Categories
            TAG_STA_CAT = "<CATEGORY>",
            TAG_END_CAT = "</CATEGORY>",
            CAT_ROWBEG = "[CAT_ROWBEG]",
            CAT_ROWEND = "[CAT_ROWEND]",
            CAT_NAME = "[CAT_NAME]",
            // Template Tab
            TEMPLATE = "[TEMPLATE]",
            COLOR = "[COLOR]",
            MASTERYMSG = "[MASTMSG]";
        #endregion
        // Split Strings
        private const string
            SPLIT1 = "%%%",
            SPLIT2 = "@@@",
            SPLIT3 = "^^^";
        // Every [] inside ends with a %%%
        // For Tags <> inside, use the @@@ to separate
        // For Tags <> inside another <>, use the ^^^ to separate

        #region Saving Functions
        
        public void saveCharResetData() {
            data = "";
        }

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
            Dictionary<string, Profession> prof_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(CHARNAME + charName_ + SPLIT1);
            sb.Append(NICKNAME + nickName_ + SPLIT1);
            sb.Append(AGE + age_ + SPLIT1);
            sb.Append(GENDER + gender_ + SPLIT1);
            sb.Append(RACE + race_ + SPLIT1);
            sb.Append(POSITION + pos_ + SPLIT1);
            sb.Append(AFFILIATION + aff_ + SPLIT1);
            sb.Append(BOUNTY + bounty_ + SPLIT1);
            sb.Append(COMMENDATION + comm_ + SPLIT1);
            sb.Append(RANK + rank_ + SPLIT1);
            sb.Append(THREAT + threat_ + SPLIT1);
            sb.Append(TAG_STA_ACHIEVE);
            for (int i = 0; i < achieve_.Items.Count; ++i) {
                sb.Append(achieve_.Items[i].ToString());
                sb.Append(SPLIT2);
            }
            sb.Append(TAG_END_ACHIEVE);
            sb.Append(TAG_STA_PROF);
            foreach (Profession prof in prof_.Values) {
                sb.Append(PROF_NAME + prof.name + SPLIT1);
                sb.Append(PROF_PRIM + prof.primary + SPLIT1);
                sb.Append(PROF_DESC + prof.desc + SPLIT1);
                sb.Append(PROF_BONU + prof.bonus + SPLIT1);
                sb.Append(SPLIT2);
            }
            sb.Append(TAG_END_PROF);
            // Put into String
            data += sb.ToString();
        }

        public void saveCharAppearance(string height_,
            string weight_,
            string hair_,
            string eye_,
            string clothing_,
            string appear_,
            ListView images_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(HEIGHT + height_ + SPLIT1);
            sb.Append(WEIGHT + weight_ + SPLIT1);
            sb.Append(HAIR + hair_ + SPLIT1);
            sb.Append(EYE + eye_ + SPLIT1);
            sb.Append(CLOTHING + clothing_ + SPLIT1);
            sb.Append(APPEARANCE + appear_ + SPLIT1);
            sb.Append(TAG_STA_IMAG);
            foreach (ListViewItem img in images_.Items) {
                sb.Append(IMAG_URL + img.SubItems[0].Text + SPLIT1);
                sb.Append(IMAG_LABEL + img.SubItems[1].Text + SPLIT1);
                bool fullres = false;
                if (img.SubItems[2].Text == "Yes") { fullres = true; }
                sb.Append(IMAG_FULLRES + fullres + SPLIT1);
                sb.Append(IMAG_IMGWIDTH + img.SubItems[3].Text + SPLIT1);
                sb.Append(IMAG_IMGHEIGHT + img.SubItems[4].Text + SPLIT1);
                sb.Append(SPLIT2);
            }
            sb.Append(TAG_END_IMAG);
            // Put into String
            data += sb.ToString();
        }

        public void saveCharBackground(string island_,
            string region_,
            string pers_,
            string history_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(ISLAND + island_ + SPLIT1);
            sb.Append(REGION + region_ + SPLIT1);
            sb.Append(PERSONALITY + pers_ + SPLIT1);
            sb.Append(HISTORY + history_ + SPLIT1);
            // Put into String
            data += sb.ToString();
        }

        public void saveCharCombat(string comb_,
            ListView weapon_,
            ListView items_,
            string beli_,
            string DFName_,
            string DFType_,
            string DFTier_,
            string DFDesc_,
            string DFEffe_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(COMBAT + comb_ + SPLIT1);
            sb.Append(TAG_STA_WEAPON);
            foreach (ListViewItem item in weapon_.Items) {
                sb.Append(WEAPON_NAME + item.SubItems[0].Text + SPLIT1);
                sb.Append(WEAPON_DESC + item.SubItems[1].Text + SPLIT1);
                sb.Append(SPLIT2);
            }
            sb.Append(TAG_END_WEAPON);
            sb.Append(TAG_STA_ITEM);
            foreach (ListViewItem item in items_.Items) {
                sb.Append(ITEM_NAME + item.SubItems[0].Text + SPLIT1);
                sb.Append(ITEM_DESC + item.SubItems[1].Text + SPLIT1);
                sb.Append(SPLIT2);
            }
            sb.Append(TAG_END_ITEM);
            sb.Append(BELI + beli_ + SPLIT1);
            sb.Append(DF_NAME + DFName_ + SPLIT1);
            sb.Append(DF_TYPE + DFType_ + SPLIT1);
            sb.Append(DF_TIER + DFTier_ + SPLIT1);
            sb.Append(DF_DESC + DFDesc_ + SPLIT1);
            sb.Append(DF_EFFE + DFEffe_ + SPLIT1);
            // Put into String
            data += sb.ToString();
        }

        public void saveCharStats(int SDEarn_,
            int SD2SP_,
            CheckedListBox AP_,
            int usedFort_,
            int str_,
            int spe_,
            int sta_,
            int acc_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(SDEARNED + SDEarn_ + SPLIT1);
            sb.Append(SDTOSP + SD2SP_ + SPLIT1);
            sb.Append(APBOOL);
            for (int i = 0; i < AP_.Items.Count; ++i) {
                CheckState chk = AP_.GetItemCheckState(i);
                sb.Append(chk == CheckState.Checked);
                sb.Append(',');
            }
            sb.Append(SPLIT1);
            sb.Append(USEDFORTUNE + usedFort_ + SPLIT1);
            sb.Append(STRENGTH + str_ + SPLIT1);
            sb.Append(SPEED + spe_ + SPLIT1);
            sb.Append(STAMINA + sta_ + SPLIT1);
            sb.Append(ACCURACY + acc_ + SPLIT1);
            // Put into String
            data += sb.ToString();
        }

        public void saveCharTraits(Dictionary<string, Trait> traits_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(TAG_STA_TRAIT);
            foreach (Trait trait in traits_.Values) {
                sb.Append(TRAIT_NAME + trait.name + SPLIT1);
                sb.Append(TRAIT_GENE + trait.genNum + SPLIT1);
                sb.Append(TRAIT_PROF + trait.profNum + SPLIT1);
                sb.Append(TRAIT_DESC + trait.desc + SPLIT1);
                sb.Append(SPLIT2);
            }
            sb.Append(TAG_END_TRAIT);
            // Put into String
            data += sb.ToString();
        }

        public void saveCharTechs(Dictionary<string, Technique> techs_,
            ListView cats_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(TAG_STA_TECH);
            foreach (Technique tech in techs_.Values) {
                sb.Append(TECH_NAME + tech.name + SPLIT1);
                sb.Append(TECH_ROKU + tech.rokuName + SPLIT1);
                sb.Append(TECH_RANK + tech.rank + SPLIT1);
                sb.Append(TECH_REGTP + tech.regTP + SPLIT1);
                sb.Append(TECH_SPTP + tech.spTP + SPLIT1);
                sb.Append(TECH_RANKTRAIT + tech.rankTrait + SPLIT1);
                sb.Append(TECH_SPTRAIT + tech.specialTrait + SPLIT1);
                sb.Append(TECH_SIGTECH + tech.sigTech + SPLIT1);
                sb.Append(TECH_BRANCHTECH + tech.branchTech + SPLIT1);
                sb.Append(TECH_BRANCHRANK + tech.branchRank + SPLIT1);
                sb.Append(TECH_TYPE + tech.type + SPLIT1);
                sb.Append(TECH_RANGE + tech.range + SPLIT1);
                sb.Append(TECH_STAT_NAME + tech.stats.statsName + SPLIT1);
                sb.Append(TECH_STAT_DURA + tech.stats.duration + SPLIT1);
                sb.Append(TECH_STAT_STR + tech.stats.strength + SPLIT1);
                sb.Append(TECH_STAT_SPE + tech.stats.speed + SPLIT1);
                sb.Append(TECH_STAT_STA + tech.stats.stamina + SPLIT1);
                sb.Append(TECH_STAT_ACC + tech.stats.accuracy + SPLIT1);
                sb.Append(TECH_AUTOCALC + tech.autoCalc + SPLIT1);
                sb.Append(TECH_NAPOWER + tech.NApower + SPLIT1);
                sb.Append(TAG_STA_EFFECT);
                foreach (Effect eff in tech.effects) {
                    sb.Append(EFFECT_NAME + eff.name + SPLIT1);
                    sb.Append(EFFECT_GEN + eff.general + SPLIT1);
                    sb.Append(EFFECT_COST + eff.cost + SPLIT1);
                    sb.Append(EFFECT_MINRANK + eff.minRank + SPLIT1);
                    sb.Append(EFFECT_DESC + eff.desc + SPLIT1);
                    sb.Append(SPLIT3);
                }
                sb.Append(TAG_END_EFFECT);
                sb.Append(TECH_POWER + tech.power + SPLIT1);
                sb.Append(TECH_DF);
                foreach (bool dfbool in tech.checkBoxDF) {
                    sb.Append(dfbool + ",");
                }
                sb.Append(SPLIT1);
                sb.Append(TECH_CYBORG);
                foreach (bool cybbool in tech.cyborgBoosts) {
                    sb.Append(cybbool + ",");
                }
                sb.Append(SPLIT1);
                sb.Append(TECH_NOTE + tech.note + SPLIT1);
                sb.Append(TECH_CUST + tech.customNote + SPLIT1);
                sb.Append(TECH_DESC + tech.description + SPLIT1);
                sb.Append(SPLIT2);
            }
            sb.Append(TAG_END_TECH);
            sb.Append(TAG_STA_CAT);
            foreach (ListViewItem item in cats_.Items) {
                sb.Append(CAT_ROWBEG + item.SubItems[0].Text + SPLIT1);
                sb.Append(CAT_ROWEND + item.SubItems[1].Text + SPLIT1);
                sb.Append(CAT_NAME + item.SubItems[2].Text + SPLIT1);
                sb.Append(SPLIT2);
            }
            sb.Append(TAG_END_CAT);
            // Put into String
            data += sb.ToString();
        }

        public void saveCharTemplate(string temp_,
            string color_,
            string msg_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(TEMPLATE + temp_ + SPLIT1);
            sb.Append(COLOR + color_ + SPLIT1);
            sb.Append(MASTERYMSG + msg_ + SPLIT1);
            // Put into String
            data += sb.ToString();
        }

        #endregion
        
        // This takes the string between identifier and split
        private string getParse(string begStr, string endStr, string parsing = "") {
            if (parsing == "") { parsing = data; }
            int pFrom = parsing.IndexOf(begStr) + begStr.Length;
            if (pFrom == -1) { return ""; }
            int pTo = parsing.IndexOf(endStr, pFrom);
            if (pTo == -1) { return ""; }
            return parsing.Substring(pFrom, pTo - pFrom).TrimEnd('@');
        }

        // Predicate for RemoveAll.
        private static bool emptyString(string s) {
            return s == "";
        }

        private string[] splitStringbyString(string whole, string splitter) {
            string[] result = whole.Split(new string[] { splitter }, StringSplitOptions.None);
            List<string> listResult = result.ToList();
            listResult.RemoveAll(emptyString);
            return listResult.ToArray();
        }

        #region Load Functions

        public void loadCharBasic(ref TextBox charName,
            ref TextBox nickName,
            ref NumericUpDown age,
            ref ComboBox gender,
            ref TextBox race,
            ref TextBox pos,
            ref ComboBox aff,
            ref TextBox bounty,
            ref NumericUpDown comm,
            ref ComboBox rank,
            ref TextBox threat,
            ref ListBox achieve,
            ref ListView prof,
            ref Dictionary<string, Profession> profList) {
            charName.Text = getParse(CHARNAME, SPLIT1);
            nickName.Text = getParse(NICKNAME, SPLIT1);
            age.Value = int.Parse(getParse(AGE, SPLIT1));
            gender.Text = getParse(GENDER, SPLIT1);
            race.Text = getParse(RACE, SPLIT1);
            pos.Text= getParse(POSITION, SPLIT1);
            aff.Text = getParse(AFFILIATION, SPLIT1);
            bounty.Text = getParse(BOUNTY, SPLIT1);
            comm.Value = int.Parse(getParse(COMMENDATION, SPLIT1));
            rank.Text = getParse(RANK, SPLIT1);
            threat.Text = getParse(THREAT, SPLIT1);
            string achieveStr = getParse(TAG_STA_ACHIEVE, TAG_END_ACHIEVE);
            string[] achievements = splitStringbyString(achieveStr, SPLIT2);
            achieve.Items.Clear();
            for (int i = 0; i < achievements.Length; ++i) {
                achieve.Items.Add(achievements[i]);
            }
            string profStr = getParse(TAG_STA_PROF, TAG_END_PROF);
            string[] profArr = splitStringbyString(profStr, SPLIT2);
            prof.Items.Clear();
            profList.Clear();
            for (int i = 0; i < profArr.Length; ++i) {
                string profName = getParse(PROF_NAME, SPLIT1, profArr[i]);
                string profPrim = getParse(PROF_PRIM, SPLIT1, profArr[i]);
                bool primary = (profPrim == "True") ? true : false;
                string profDesc = getParse(PROF_DESC, SPLIT1, profArr[i]);
                string profBonu = getParse(PROF_BONU, SPLIT1, profArr[i]);
                profList.Add(profName, new Profession(profName, primary, profDesc, profBonu));
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = profName;
                profPrim = (primary) ? "Primary" : "Secondary";
                item.SubItems.Add(profPrim);
                item.SubItems.Add(profDesc);
                item.SubItems.Add(profBonu);
                prof.Items.Add(item);
            }
        }

        public void loadCharAppearance(ref TextBox height,
            ref TextBox weight,
            ref RichTextBox hair,
            ref RichTextBox eye,
            ref RichTextBox clothing,
            ref RichTextBox appear,
            ref ListView images) {
            height.Text = getParse(HEIGHT, SPLIT1);
            weight.Text = getParse(WEIGHT, SPLIT1);
            hair.Text = getParse(HAIR, SPLIT1);
            eye.Text = getParse(EYE, SPLIT1);
            clothing.Text = getParse(CLOTHING, SPLIT1);
            appear.Text = getParse(APPEARANCE, SPLIT1);
            string imgStr = getParse(TAG_STA_IMAG, TAG_END_IMAG);
            string[] imgArr = splitStringbyString(imgStr, SPLIT2);
            images.Items.Clear();
            for (int i = 0; i < imgArr.Length; ++i) {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = getParse(IMAG_URL, SPLIT1, imgArr[i]);
                item.SubItems.Add(getParse(IMAG_LABEL, SPLIT1, imgArr[i]));
                item.SubItems.Add(getParse(IMAG_FULLRES, SPLIT1, imgArr[i]));
                item.SubItems.Add(getParse(IMAG_IMGWIDTH, SPLIT1, imgArr[i]));
                item.SubItems.Add(getParse(IMAG_IMGHEIGHT, SPLIT1, imgArr[i]));
                images.Items.Add(item);
            }
        }
        
        public void loadCharBackground(ref TextBox island,
            ref ComboBox region,
            ref RichTextBox pers,
            ref RichTextBox history) {
            island.Text = getParse(ISLAND, SPLIT1);
            region.Text = getParse(REGION, SPLIT1);
            pers.Text = getParse(PERSONALITY, SPLIT1);
            history.Text = getParse(HISTORY, SPLIT1);
        }

        public void loadCharCombat(ref RichTextBox combat,
            ref ListView weapon,
            ref ListView items,
            ref TextBox beli,
            ref TextBox DFName,
            ref ComboBox DFType,
            ref ComboBox DFTier,
            ref RichTextBox DFDesc,
            ref TextBox DFEffe) {
            combat.Text = getParse(COMBAT, SPLIT1);
            string weaponStr = getParse(TAG_STA_WEAPON, TAG_END_WEAPON);
            string[] weaponArr = splitStringbyString(weaponStr, SPLIT2);
            weapon.Items.Clear();
            for (int i = 0; i < weaponArr.Length; ++i) {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = getParse(WEAPON_NAME, SPLIT1, weaponArr[i]);
                item.SubItems.Add(getParse(WEAPON_DESC, SPLIT1, weaponArr[i]));
                weapon.Items.Add(item);
            }
            string itemStr = getParse(TAG_STA_ITEM, TAG_END_ITEM);
            string[] itemArr = splitStringbyString(itemStr, SPLIT2);
            items.Items.Clear();
            for (int i = 0; i < itemArr.Length; ++i) {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = getParse(ITEM_NAME, SPLIT1, itemArr[i]);
                item.SubItems.Add(getParse(ITEM_DESC, SPLIT1, itemArr[i]));
                items.Items.Add(item);
            }
            beli.Text = getParse(BELI, SPLIT1);
            DFName.Text = getParse(DF_NAME, SPLIT1);
            DFType.Text = getParse(DF_TYPE, SPLIT1);
            DFTier.Text = getParse(DF_TIER, SPLIT1);
            DFDesc.Text = getParse(DF_DESC, SPLIT1);
            DFEffe.Text = getParse(DF_EFFE, SPLIT1);
        }
        
        public void loadCharStats(ref NumericUpDown SDEarn,
            ref NumericUpDown SD2SP,
            ref CheckedListBox AP,
            ref NumericUpDown usedFort,
            ref NumericUpDown str,
            ref NumericUpDown spe,
            ref NumericUpDown sta,
            ref NumericUpDown acc) {
            string[] APArr = getParse(APBOOL, SPLIT1).TrimEnd(',').Split(',');
            for (int i = 0 ; i < AP.Items.Count ; ++i) {
                try { AP.SetItemChecked(i, bool.Parse(APArr[i])); }
                catch { AP.SetItemChecked(i, false); }
                // In case APArr[i] goes out of index
            }
            SDEarn.Value = int.Parse(getParse(SDEARNED, SPLIT1));
            SD2SP.Value = int.Parse(getParse(SDTOSP, SPLIT1));
            usedFort.Value = int.Parse(getParse(USEDFORTUNE, SPLIT1));
            str.Value = int.Parse(getParse(STRENGTH, SPLIT1));
            spe.Value = int.Parse(getParse(SPEED, SPLIT1));
            sta.Value = int.Parse(getParse(STAMINA, SPLIT1));
            acc.Value = int.Parse(getParse(ACCURACY, SPLIT1));
        }
        
        public void loadCharTraits(ref Dictionary<string, Trait> traits,
            ref ListView traitsTbl, 
            ref Dictionary<string, SpTrait> spTraits,
            int fortune) {
            string traitsStr = getParse(TAG_STA_TRAIT, TAG_END_TRAIT);
            string[] traitsArr = splitStringbyString(traitsStr, SPLIT2);
            traits.Clear();
            traitsTbl.Items.Clear();
            for (int i = 0; i < traitsArr.Length; ++i) {
                string name = getParse(TRAIT_NAME, SPLIT1, traitsArr[i]);
                int gen = int.Parse(getParse(TRAIT_GENE, SPLIT1, traitsArr[i]));
                int prof = int.Parse(getParse(TRAIT_PROF, SPLIT1, traitsArr[i]));
                string desc = getParse(TRAIT_DESC, SPLIT1, traitsArr[i]);
                traits.Add(name, new Trait(name, gen, prof, desc));
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = name;
                string type = (gen > 0 && prof > 0) ? "General / Professional" :
                    (gen > 0) ? "General" : "Professional";
                item.SubItems.Add(type);
                item.SubItems.Add(gen.ToString());
                item.SubItems.Add(prof.ToString());
                item.SubItems.Add(desc);
                traitsTbl.Items.Add(item);
            }
            // Only add the Total for Sp Trait.
            // Edit theh Sp Trait later
            spTraits.Clear();
            foreach (Trait trait in traits.Values) {
                string traitName = trait.name;
                int divisor = Database.getSpTraitDiv(traitName);
                if (divisor > 0) {
                    int traitNum = trait.getTotalTraits();
                    spTraits.Add(trait.name, new SpTrait(traitName, 
                        (fortune / divisor) * traitNum));
                }
            }
        }
        
        public void loadCharTechs(ref Dictionary<string, Technique> techs,
            ref ListView techTbl,
            ref Dictionary<string, SpTrait> spTraits,
            ref ListView spTraitTbl,
            ref ListView catTbl) {
            string techStr = getParse(TAG_STA_TECH, TAG_END_TECH);
            string[] techArr = splitStringbyString(techStr, SPLIT2);
            techs.Clear();
            techTbl.Items.Clear();
            for (int i = 0; i < techArr.Length; ++i) {
                string name = getParse(TECH_NAME, SPLIT1, techArr[i]);
                string rokuName = getParse(TECH_ROKU, SPLIT1, techArr[i]);
                int rank = int.Parse(getParse(TECH_RANK, SPLIT1, techArr[i]));
                int regTP = int.Parse(getParse(TECH_REGTP, SPLIT1, techArr[i]));
                int spTP = int.Parse(getParse(TECH_SPTP, SPLIT1, techArr[i]));
                string rankTr = getParse(TECH_RANKTRAIT, SPLIT1, techArr[i]);
                string specTr = getParse(TECH_SPTRAIT, SPLIT1, techArr[i]);
                bool sigTech = bool.Parse(getParse(TECH_SIGTECH, SPLIT1, techArr[i]));
                string brTech = getParse(TECH_BRANCHTECH, SPLIT1, techArr[i]);
                int brRank = int.Parse(getParse(TECH_BRANCHRANK, SPLIT1, techArr[i]));
                string type = getParse(TECH_TYPE, SPLIT1, techArr[i]);
                string range = getParse(TECH_RANGE, SPLIT1, techArr[i]);
                string statName = getParse(TECH_STAT_NAME, SPLIT1, techArr[i]);
                string statDur = getParse(TECH_STAT_DURA, SPLIT1, techArr[i]);
                int statStr = int.Parse(getParse(TECH_STAT_STR, SPLIT1, techArr[i]));
                int statSpe = int.Parse(getParse(TECH_STAT_SPE, SPLIT1, techArr[i]));
                int statSta = int.Parse(getParse(TECH_STAT_STA, SPLIT1, techArr[i]));
                int statAcc = int.Parse(getParse(TECH_STAT_ACC, SPLIT1, techArr[i]));
                Stats techStats = new Stats(statName, statDur, statStr, statSpe, statSta, statAcc);
                bool auto = bool.Parse(getParse(TECH_AUTOCALC, SPLIT1, techArr[i]));
                bool NA = bool.Parse(getParse(TECH_NAPOWER, SPLIT1, techArr[i]));
                string effStr = getParse(TAG_STA_EFFECT, TAG_END_EFFECT, techArr[i]);
                string[] effArr = splitStringbyString(effStr, SPLIT3);
                List<Effect> effList = new List<Effect>();
                for (int j = 0; j < effArr.Length; ++j) {
                    string effName = getParse(EFFECT_NAME, SPLIT1, effArr[j]);
                    bool effGen = bool.Parse(getParse(EFFECT_GEN, SPLIT1, effArr[j]));
                    int effCost = int.Parse(getParse(EFFECT_COST, SPLIT1, effArr[j]));
                    int effMin = int.Parse(getParse(EFFECT_MINRANK, SPLIT1, effArr[j]));
                    string effDesc = getParse(EFFECT_DESC, SPLIT1, effArr[j]);
                    effList.Add(new Effect(effName, effGen, effCost, effMin, effDesc));
                }
                string power = getParse(TECH_POWER, SPLIT1, techArr[i]);
                string[] DFBoolArr = getParse(TECH_DF, SPLIT1, techArr[i]).TrimEnd(',').Split(',');
                List<bool> DFBoolList = new List<bool>();
                for (int j = 0; j < DFBoolArr.Length; ++j) {
                    try { DFBoolList.Add(bool.Parse(DFBoolArr[j])); }
                    catch { DFBoolList.Add(false); }
                    // In case APArr[i] goes out of index
                }
                string[] cyborgArr = getParse(TECH_CYBORG, SPLIT1, techArr[i]).TrimEnd(',').Split(',');
                List<bool> cyborgList = new List<bool>();
                for (int j = 0; j < cyborgArr.Length; ++j) {
                    try { cyborgList.Add(bool.Parse(cyborgArr[j])); }
                    catch { cyborgList.Add(false); }
                    // In case cyborgArr[i] goes out of index
                }
                string note = getParse(TECH_NOTE, SPLIT1, techArr[i]);
                string cust = getParse(TECH_CUST, SPLIT1, techArr[i]);
                string desc = getParse(TECH_DESC, SPLIT1, techArr[i]);
                // ADD IT ALL UP. WOW.
                techs.Add(name, new Technique(name, rokuName, rank,
                    regTP, spTP,
                    rankTr, specTr, sigTech,
                    brTech, brRank,
                    type, range,
                    techStats,
                    auto, NA,
                    effList,
                    power, DFBoolList,
                    cyborgList, note,
                    cust, desc));
                // Jokes on you, now you gotta do the TechTbl
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = name;
                item.SubItems.Add(rank.ToString());
                item.SubItems.Add(regTP.ToString());
                item.SubItems.Add(spTP.ToString());
                item.SubItems.Add(specTr);
                item.SubItems.Add(brTech);
                item.SubItems.Add(type);
                item.SubItems.Add(range);
                item.SubItems.Add(techStats.getTechString());
                item.SubItems.Add(power);
                techTbl.Items.Add(item);
            }
            // Update the SpTrait dictionary
            foreach (Technique tech in techs.Values) {
                try {
                    string traitName = tech.specialTrait;
                    spTraits[traitName].usedTP += tech.spTP;
                }
                catch { } // If key doesn't exist, just pass it
            }
            // Now add onto the SpTrait Listview
            spTraitTbl.Items.Clear();
            foreach (SpTrait spTrait in spTraits.Values) {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = spTrait.name;
                item.SubItems.Add(spTrait.usedTP.ToString());
                item.SubItems.Add(spTrait.totalTP.ToString());
                spTraitTbl.Items.Add(item);
            }
            // Do the CatTbl
            string catStr = getParse(TAG_STA_CAT, TAG_END_CAT);
            string[] catArr = splitStringbyString(catStr, SPLIT2);
            catTbl.Items.Clear();
            for (int i = 0; i < catArr.Length; ++i) {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = getParse(CAT_ROWBEG, SPLIT1, catArr[i]);
                item.SubItems.Add(getParse(CAT_ROWEND, SPLIT1, catArr[i]));
                item.SubItems.Add(getParse(CAT_NAME, SPLIT1, catArr[i]));
                catTbl.Items.Add(item);
            }
        }
        
        public void loadCharTemplate(ref RichTextBox template,
            ref TextBox color,
            ref TextBox msg) {
            template.Text = getParse(TEMPLATE, SPLIT1);
            color.Text = getParse(COLOR, SPLIT1);
            msg.Text = getParse(MASTERYMSG, SPLIT1);
        }

        #endregion

        #region Encrypting/Decrypting

        private const string INITVECTOR = "tu89geji340t89u2";
        private const int KEYSIZE = 256;
        private const string OPRPKEY = "OPRPxddddd2009";

        // Thank you to: http://stackoverflow.com/questions/17213851/an-easy-way-to-encrypt-and-decrypt-with-a-key
        // For something simple

        private string encryptData(string Text, string Key) {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(INITVECTOR);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(Text);
            PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
            byte[] keyBytes = password.GetBytes(KEYSIZE / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] Encrypted = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(Encrypted);
        }

        private string decryptData(string EncryptedText, string Key) {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(INITVECTOR);
            byte[] DeEncryptedText = Convert.FromBase64String(EncryptedText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(Key, null);
            byte[] keyBytes = password.GetBytes(KEYSIZE / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(DeEncryptedText);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[DeEncryptedText.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        #endregion

        public void saveFile() {
            string saveData = encryptData(data, OPRPKEY);
            using (StreamWriter newTask = new StreamWriter(path, false)) {
                newTask.Write(saveData);
            }
        }

        public void openFile() {
            string encrypted = File.ReadAllText(path);
            data = decryptData(encrypted, OPRPKEY);
        }

        // Default Constructor
        public Character() { }

    }
}
