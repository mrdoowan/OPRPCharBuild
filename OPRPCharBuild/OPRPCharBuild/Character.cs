﻿/*
 * This will contain our entire Character information. 
 * It will also be useful for importing from earlier versions
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OPRPCharBuild
{
    public class Character
    {
        // --------------------------------------------------------------------
        // MEMBER VARIABLES
        // --------------------------------------------------------------------
        public string filename { get; set; }
        public string path { get; set; }
        private string data { get; set; }
        #region Const Identifiers
        private const string
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
            PROF_CUST = "[PR_CUST]",
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
            // Stats Tab
            SDEARNED = "[SDEAR]",
            SDTOSP = "[SD2SP]",
            USEDFORTUNE = "[SDFORT]",
            STRENGTH = "[STR]",
            SPEED = "[SPE]",
            STAMINA = "[STA]",
            ACCURACY = "[ACC]",
            AP_TECH = "[AP_TECH]",
            AP_TRAIT = "[AP_TRAIT]",
            AP_PRIME = "[AP_PRIME]",
            AP_MULTI = "[AP_MULTI]",
            AP_NPC = "[AP_NPC]",
            AP_HAKI = "[AP_HAKI]",
            AP_DF = "[AP_DF]",
            // Traits Tab
            TAG_STA_TRAIT = "<TRAIT>",
            TAG_END_TRAIT = "</TRAIT>",
            TRAIT_NAME = "[TR_NAME]",
            TRAIT_CUST = "[TR_CUST]",
            TRAIT_GENE = "[TR_GENE]",
            TRAIT_PROF = "[TR_PROF]",
            TRAIT_DESC = "[TR_DESC]",
            // Technique Tab
            TAG_STA_TECH = "<TECH>",
            TAG_END_TECH = "</TECH>",
            TECH_NAME = "[TECH_NAME]",
            TECH_ROKU = "[TECH_ROKU]",
            TECH_RANK = "[TECH_RANK]",
            TECH_AE = "[TECH_AE]",
            TECH_REGTP = "[TECH_REGTP]",
            TECH_SPTP = "[TECH_SPTP]",
            TECH_RANKTRAIT = "[TECH_RKTR]",
            TECH_SPTRAIT = "[TECH_SPTR]",
            TECH_SIGTECH = "[TECH_SIG]",
            TECH_MMPRIM = "[TECH_MM]",
            TECH_INPRIM = "[TECH_IN]",
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
            // Sources Tab
            TAG_STA_SOURCE = "<SOURCE>",
            TAG_END_SOURCE = "</SOURCE>",
            SOURCE_YYYY = "[SOU_YYYY]", // year
            SOURCE_MM = "[SOU_MM]",     // month
            SOURCE_DD = "[SOU_DD]",     // day
            SOURCE_HASDATE = "[SOURCE_HAS]",
            SOURCE_DATEFORMAT = "[SOU_FORMAT]", // true if NA
            SOURCE_TITLE = "[SOU_TITLE]",
            SOURCE_URL = "[SOU_URL]",
            SOURCE_SD = "[SOU_SD]",
            SOURCE_BELI = "[SOU_BELI]",
            SOURCE_NOTE = "[SOU_NOTE]",
            // Template Tab
            TEMPLATE_NAME = "[TEMP_NAME]",
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

        // Store everything about the Character in member variables
        public string charName { get; set; }
        public string charNickname { get; set; }
        public int charAge { get; set; }
        public string charGender { get; set; }
        public string charRace { get; set; }
        public string charPosition { get; set; }
        public string charAffiliation { get; set; }
        public string charBounty { get; set; }
        public int charCommend { get; set; }
        public string charRank { get; set; }
        public string charThreat { get; set; }
        public List<string> charAchieves { get; set; }
        public Dictionary<string, Profession> charName2Profs { get; set; }
        public string charHeight { get; set; }
        public string charWeight { get; set; }
        public string charHair { get; set; }
        public string charEye { get; set; }
        public string charClothing { get; set; }
        public string charAppear { get; set; }
        public List<Image> charImages { get; set; }
        public string charIsland { get; set; }
        public string charRegion { get; set; }
        public string charPersonality { get; set; }
        public string charHistory { get; set; }
        public string charCombat { get; set; }
        public List<Equipment> charWeapons { get; set; }
        public List<Equipment> charItems { get; set; }
        public string charBeli { get; set; }
        public DevilFruit charDF { get; set; }
        public int charSDEarned { get; set; }
        public int charSDConverted { get; set; } // into SP
        public int charSDUsedFortune { get; set; }
        public Stats charStats { get; set; }
        public List<int> charAPs { get; set; }
        public Dictionary<string, Trait> charName2Traits { get; set; }
        public Dictionary<string, Technique> charName2Techs { get; set; }
        public List<Category> charTechCategories { get; set; }
        public Dictionary<string, Source> charName2Sources { get; set; }
        public string charTemplateName { get; set; }
        public string charTemplateStr { get; set; }
        public string charTemplateColor { get; set; }
        public string charTemplate4RankMsg { get; set; }

        #region Helper Functions
        // This takes the string between identifier and split
        private string getParse(string begStr, string endStr, string parsing = "")
        {
            if (parsing == "") { parsing = data; }
            int pFrom = parsing.IndexOf(begStr) + begStr.Length;
            if (pFrom == -1) { return ""; }
            int pTo = parsing.IndexOf(endStr, pFrom);
            if (pTo == -1) { return ""; }
            return parsing.Substring(pFrom, pTo - pFrom).TrimEnd('@');
        }

        // Predicate for RemoveAll.
        private static bool emptyString(string s)
        {
            return s == "";
        }

        // Splitting into an array of strings
        private string[] splitStringbyString(string whole, string splitter)
        {
            string[] result = whole.Split(new string[] { splitter }, StringSplitOptions.None);
            List<string> listResult = result.ToList();
            listResult.RemoveAll(emptyString);
            return listResult.ToArray();
        }

        // Our version of int.TryParse that returns zero upon failure of parsing
        private int TryParseInt(string parsing)
        {
            int num = 0;
            if (!int.TryParse(parsing, out num)) { return 0; }
            return num;
        }

        // Our version of bool.TryParse that automatically returns false upon failure
        private bool TryParseBool(string parsing)
        {
            bool boolean = false;
            if (!bool.TryParse(parsing, out boolean)) { return false; }
            return boolean;
        }
        #endregion

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
            DataGridView dgvProf_,
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
            foreach (DataGridViewRow profRow in dgvProf_.Rows) {
                string profName = profRow.Cells[0].Value.ToString();
                Profession prof = prof_[profName];
                sb.Append(PROF_NAME + prof.name + SPLIT1);
                sb.Append(PROF_PRIM + prof.primary + SPLIT1);
                sb.Append(PROF_CUST + prof.custom + SPLIT1);
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
            DevilFruit DF_) {
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
            sb.Append(DF_NAME + DF_.name + SPLIT1);
            sb.Append(DF_TYPE + DF_.type + SPLIT1);
            sb.Append(DF_TIER + DF_.tier + SPLIT1);
            sb.Append(DF_DESC + DF_.description + SPLIT1);
            // Put into String
            data += sb.ToString();
        }

        public void saveCharStats(int SDEarn_,
            int SD2SP_,
            int usedFort_,
            int str_,
            int spe_,
            int sta_,
            int acc_,
            int APTech_,
            int APTrait_,
            int APPrime_,
            int APMulti_,
            int APNPC_,
            bool APHaki_,
            bool APDF_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(SDEARNED + SDEarn_ + SPLIT1);
            sb.Append(SDTOSP + SD2SP_ + SPLIT1);
            sb.Append(SPLIT1);
            sb.Append(USEDFORTUNE + usedFort_ + SPLIT1);
            sb.Append(STRENGTH + str_ + SPLIT1);
            sb.Append(SPEED + spe_ + SPLIT1);
            sb.Append(STAMINA + sta_ + SPLIT1);
            sb.Append(ACCURACY + acc_ + SPLIT1);
            sb.Append(AP_TECH + APTech_ + SPLIT1);
            sb.Append(AP_TRAIT + APTrait_ + SPLIT1);
            sb.Append(AP_PRIME + APPrime_ + SPLIT1);
            sb.Append(AP_MULTI + APMulti_ + SPLIT1);
            sb.Append(AP_NPC + APNPC_ + SPLIT1);
            sb.Append(AP_HAKI + APHaki_ + SPLIT1);
            sb.Append(AP_DF + APDF_ + SPLIT1);
            // Put into String
            data += sb.ToString();
        }

        public void saveCharTraits(DataGridView dgvTraits_,
            List<Trait> traits_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(TAG_STA_TRAIT);
            foreach (DataGridViewRow traitRow in dgvTraits_.Rows) {
                string traitName = traitRow.Cells[0].Value.ToString();
                Trait trait = traits_.Find(x => x.name == traitName);
                sb.Append(TRAIT_NAME + trait.name + SPLIT1);
                sb.Append(TRAIT_CUST + trait.custom + SPLIT1);
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
            DataGridView dgvTechs_,
            ListView cats_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(TAG_STA_TECH);
            // We want all the Techniques by order
            foreach (DataGridViewRow techRow in dgvTechs_.Rows) {
                string techName = techRow.Cells[0].Value.ToString();
                Technique tech = techs_[techName];
                sb.Append(TECH_NAME + tech.name + SPLIT1);
                sb.Append(TECH_ROKU + tech.rokuName + SPLIT1);
                sb.Append(TECH_RANK + tech.rank + SPLIT1);
                sb.Append(TECH_AE + tech.AE + SPLIT1);
                sb.Append(TECH_REGTP + tech.regTP + SPLIT1);
                sb.Append(TECH_SPTP + tech.spTP + SPLIT1);
                sb.Append(TECH_RANKTRAIT + tech.rankTrait + SPLIT1);
                sb.Append(TECH_SPTRAIT + tech.specialTrait + SPLIT1);
                sb.Append(TECH_SIGTECH + tech.sigTech + SPLIT1);
                sb.Append(TECH_MMPRIM + tech.mmPrimary + SPLIT1);
                sb.Append(TECH_INPRIM + tech.inPrimary + SPLIT1);
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

        // bool devH if we're saving on devH instead
        public string saveCharSources(DataGridView dgvSources_,
            Dictionary<string, Source> sources_,
            RadioButton NADateFormat_, 
            bool devH) {
            StringBuilder sb = new StringBuilder();
            sb.Append(SOURCE_DATEFORMAT + NADateFormat_.Checked + SPLIT1);
            sb.Append(TAG_STA_SOURCE);
            foreach (DataGridViewRow sourceRow in dgvSources_.Rows) {
                string sourceTitle = sourceRow.Cells[2].Value.ToString();
                Source source = sources_[sourceTitle];
                sb.Append(SOURCE_YYYY + source.date.Year + SPLIT1);
                sb.Append(SOURCE_MM + source.date.Month + SPLIT1);
                sb.Append(SOURCE_DD + source.date.Day + SPLIT1);
                sb.Append(SOURCE_HASDATE + source.noDate + SPLIT1);
                sb.Append(SOURCE_TITLE + source.title + SPLIT1);
                sb.Append(SOURCE_URL + source.URL + SPLIT1);
                sb.Append(SOURCE_SD + source.SD + SPLIT1);
                sb.Append(SOURCE_BELI + source.beli + SPLIT1);
                sb.Append(SOURCE_NOTE + source.notes + SPLIT1);
                sb.Append(SPLIT2);
            }
            sb.Append(TAG_END_SOURCE);
            // Put into String
            if (!devH) { data += sb.ToString(); return ""; }
            else { return sb.ToString(); }
        }

        public void saveCharTemplate(string tempName_,
            string temp_,
            string color_,
            string msg_) {
            StringBuilder sb = new StringBuilder();
            sb.Append(TEMPLATE_NAME + tempName_ + SPLIT1);
            sb.Append(TEMPLATE + temp_ + SPLIT1);
            sb.Append(COLOR + color_ + SPLIT1);
            sb.Append(MASTERYMSG + msg_ + SPLIT1);
            // Put into String
            data += sb.ToString();
        }

        #endregion

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
            ref ListBox achieve) {
            charName.Text = getParse(CHARNAME, SPLIT1);
            nickName.Text = getParse(NICKNAME, SPLIT1);
            age.Value = TryParseInt(getParse(AGE, SPLIT1));
            gender.Text = getParse(GENDER, SPLIT1);
            race.Text = getParse(RACE, SPLIT1);
            pos.Text= getParse(POSITION, SPLIT1);
            aff.Text = getParse(AFFILIATION, SPLIT1);
            bounty.Text = getParse(BOUNTY, SPLIT1);
            comm.Value = TryParseInt(getParse(COMMENDATION, SPLIT1));
            rank.Text = getParse(RANK, SPLIT1);
            threat.Text = getParse(THREAT, SPLIT1);
            string achieveStr = getParse(TAG_STA_ACHIEVE, TAG_END_ACHIEVE);
            string[] achievements = splitStringbyString(achieveStr, SPLIT2);
            achieve.Items.Clear();
            for (int i = 0; i < achievements.Length; ++i) {
                achieve.Items.Add(achievements[i]);
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
                item.SubItems.Add((getParse(IMAG_FULLRES, SPLIT1, imgArr[i]) == "True") ? "Yes" : "No");
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
            ref TextBox DFName,
            ref ComboBox DFType,
            ref ComboBox DFTier,
            ref RichTextBox DFDesc) {
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
            DFName.Text = getParse(DF_NAME, SPLIT1);
            DFType.Text = getParse(DF_TYPE, SPLIT1);
            DFTier.Text = getParse(DF_TIER, SPLIT1);
            DFDesc.Text = getParse(DF_DESC, SPLIT1);
        }
        
        public void loadCharRPElements(ref NumericUpDown SDEarn,
            ref NumericUpDown SD2SP,
            ref NumericUpDown usedFort,
            ref NumericUpDown str,
            ref NumericUpDown spe,
            ref NumericUpDown sta,
            ref NumericUpDown acc,
            ref NumericUpDown APTech,
            ref NumericUpDown APTrait,
            ref NumericUpDown APPrime,
            ref NumericUpDown APMulti,
            ref NumericUpDown APNPC,
            ref CheckBox APHaki,
            ref CheckBox APDF,
            ref TextBox beli,
            ref DataGridView prof,
            ref Dictionary<string, Profession> profList) {
            SDEarn.Value = TryParseInt(getParse(SDEARNED, SPLIT1));
            SD2SP.Value = TryParseInt(getParse(SDTOSP, SPLIT1));
            usedFort.Value = TryParseInt(getParse(USEDFORTUNE, SPLIT1));
            str.Value = TryParseInt(getParse(STRENGTH, SPLIT1));
            spe.Value = TryParseInt(getParse(SPEED, SPLIT1));
            sta.Value = TryParseInt(getParse(STAMINA, SPLIT1));
            acc.Value = TryParseInt(getParse(ACCURACY, SPLIT1));
            APTech.Value = TryParseInt(getParse(AP_TECH, SPLIT1));
            APTrait.Value = TryParseInt(getParse(AP_TRAIT, SPLIT1));
            APPrime.Value = TryParseInt(getParse(AP_PRIME, SPLIT1));
            APMulti.Value = TryParseInt(getParse(AP_MULTI, SPLIT1));
            APNPC.Value = TryParseInt(getParse(AP_NPC, SPLIT1));
            APHaki.Checked = TryParseBool(getParse(AP_HAKI, SPLIT1));
            APDF.Checked = TryParseBool(getParse(AP_DF, SPLIT1));
            beli.Text = getParse(BELI, SPLIT1);
            string profStr = getParse(TAG_STA_PROF, TAG_END_PROF);
            string[] profArr = splitStringbyString(profStr, SPLIT2);
            prof.Rows.Clear();
            prof.Refresh();
            profList.Clear();
            for (int i = 0; i < profArr.Length; ++i) {
                string profName = getParse(PROF_NAME, SPLIT1, profArr[i]);
                string profPrim = getParse(PROF_PRIM, SPLIT1, profArr[i]);
                string profCust = getParse(PROF_CUST, SPLIT1, profArr[i]);
                bool primary = (profPrim == "True") ? true : false;
                string profDesc = getParse(PROF_DESC, SPLIT1, profArr[i]);
                string profBonu = getParse(PROF_BONU, SPLIT1, profArr[i]);
                profList.Add(profName, new Profession(profName, primary, profCust, profDesc, profBonu));
                string pri_Str = (primary) ? "Primary" : "Secondary";
                prof.Rows.Add(profName, profCust, pri_Str, profDesc, profBonu);
            }
        }
        
        public void loadCharTraits(ref List<Trait> traits,
            ref DataGridView dgv_traits, 
            ref List<SpTrait> spTraits,
            int fortune) {
            string traitsStr = getParse(TAG_STA_TRAIT, TAG_END_TRAIT);
            string[] traitsArr = splitStringbyString(traitsStr, SPLIT2);
            traits.Clear();
            dgv_traits.Rows.Clear();
            for (int i = 0; i < traitsArr.Length; ++i) {
                string name = getParse(TRAIT_NAME, SPLIT1, traitsArr[i]);
                string custom = getParse(TRAIT_CUST, SPLIT1, traitsArr[i]);
                int gen = TryParseInt(getParse(TRAIT_GENE, SPLIT1, traitsArr[i]));
                int prof = TryParseInt(getParse(TRAIT_PROF, SPLIT1, traitsArr[i]));
                string desc = getParse(TRAIT_DESC, SPLIT1, traitsArr[i]);
                traits.Add(new Trait(name, custom, gen, prof, desc));
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = name;
                string type = (gen > 0 && prof > 0) ? "General / Professional" :
                    (gen > 0) ? "General" : "Professional";
                item.SubItems.Add(type);
                item.SubItems.Add(gen.ToString());
                item.SubItems.Add(prof.ToString());
                item.SubItems.Add(desc);
                dgv_traits.Rows.Add(name, custom, type, gen, prof, desc);
            }
            // Only add the Total for Sp Trait.
            // Edit theh Sp Trait later
            spTraits.Clear();
            foreach (Trait trait in traits) {
                string traitName = trait.name;
                string customName = trait.getName();
                int divisor = Database.getSpTraitDiv(traitName);
                if (divisor > 0) {
                    int traitNum = trait.getTotal();
                    spTraits.Add(new SpTrait(traitName, customName,
                        (fortune / divisor) * traitNum));
                }
            }
        }
        
        public void loadCharTechs(ref Dictionary<string, Technique> techs,
            ref DataGridView techTbl,
            ref List<SpTrait> spTraits,
            ref DataGridView spTraitTbl,
            ref ListView catTbl) {
            string techStr = getParse(TAG_STA_TECH, TAG_END_TECH);
            string[] techArr = splitStringbyString(techStr, SPLIT2);
            techs.Clear();
            techTbl.Rows.Clear();
            for (int i = 0; i < techArr.Length; ++i) {
                string name = getParse(TECH_NAME, SPLIT1, techArr[i]);
                string rokuName = getParse(TECH_ROKU, SPLIT1, techArr[i]);
                int rank = TryParseInt(getParse(TECH_RANK, SPLIT1, techArr[i]));
                int AE = TryParseInt(getParse(TECH_AE, SPLIT1, techArr[i]));
                int regTP = TryParseInt(getParse(TECH_REGTP, SPLIT1, techArr[i]));
                int spTP = TryParseInt(getParse(TECH_SPTP, SPLIT1, techArr[i]));
                string rankTr = getParse(TECH_RANKTRAIT, SPLIT1, techArr[i]);
                string specTr = getParse(TECH_SPTRAIT, SPLIT1, techArr[i]);
                bool sigTech = TryParseBool(getParse(TECH_SIGTECH, SPLIT1, techArr[i]));
                bool mmPrim = TryParseBool(getParse(TECH_MMPRIM, SPLIT1, techArr[i]));
                bool inPrim = TryParseBool(getParse(TECH_INPRIM, SPLIT1, techArr[i]));
                string brTech = getParse(TECH_BRANCHTECH, SPLIT1, techArr[i]);
                int brRank = TryParseInt(getParse(TECH_BRANCHRANK, SPLIT1, techArr[i]));
                string type = getParse(TECH_TYPE, SPLIT1, techArr[i]);
                string range = getParse(TECH_RANGE, SPLIT1, techArr[i]);
                string statName = getParse(TECH_STAT_NAME, SPLIT1, techArr[i]);
                string statDur = getParse(TECH_STAT_DURA, SPLIT1, techArr[i]);
                int statStr = TryParseInt(getParse(TECH_STAT_STR, SPLIT1, techArr[i]));
                int statSpe = TryParseInt(getParse(TECH_STAT_SPE, SPLIT1, techArr[i]));
                int statSta = TryParseInt(getParse(TECH_STAT_STA, SPLIT1, techArr[i]));
                int statAcc = TryParseInt(getParse(TECH_STAT_ACC, SPLIT1, techArr[i]));
                Stats techStats = new Stats(statName, statDur, statStr, statSpe, statSta, statAcc);
                bool auto = TryParseBool(getParse(TECH_AUTOCALC, SPLIT1, techArr[i]));
                bool NA = TryParseBool(getParse(TECH_NAPOWER, SPLIT1, techArr[i]));
                string effStr = getParse(TAG_STA_EFFECT, TAG_END_EFFECT, techArr[i]);
                string[] effArr = splitStringbyString(effStr, SPLIT3);
                List<Effect> effList = new List<Effect>();
                for (int j = 0; j < effArr.Length; ++j) {
                    string effName = getParse(EFFECT_NAME, SPLIT1, effArr[j]);
                    bool effGen = TryParseBool(getParse(EFFECT_GEN, SPLIT1, effArr[j]));
                    int effCost = TryParseInt(getParse(EFFECT_COST, SPLIT1, effArr[j]));
                    int effMin = TryParseInt(getParse(EFFECT_MINRANK, SPLIT1, effArr[j]));
                    string effDesc = getParse(EFFECT_DESC, SPLIT1, effArr[j]);
                    effList.Add(new Effect(effName, effGen, effCost, effMin, effDesc));
                }
                string power = getParse(TECH_POWER, SPLIT1, techArr[i]);
                string[] DFBoolArr = getParse(TECH_DF, SPLIT1, techArr[i]).TrimEnd(',').Split(',');
                List<bool> DFBoolList = new List<bool>();
                for (int j = 0; j < DFBoolArr.Length; ++j) {
                    try { DFBoolList.Add(TryParseBool(DFBoolArr[j])); }
                    catch { DFBoolList.Add(false); }
                    // In case APArr[i] goes out of index
                }
                string[] cyborgArr = getParse(TECH_CYBORG, SPLIT1, techArr[i]).TrimEnd(',').Split(',');
                List<bool> cyborgList = new List<bool>();
                for (int j = 0; j < cyborgArr.Length; ++j) {
                    try { cyborgList.Add(TryParseBool(cyborgArr[j])); }
                    catch { cyborgList.Add(false); }
                    // In case cyborgArr[i] goes out of index
                }
                string note = getParse(TECH_NOTE, SPLIT1, techArr[i]);
                string cust = getParse(TECH_CUST, SPLIT1, techArr[i]);
                string desc = getParse(TECH_DESC, SPLIT1, techArr[i]);
                // ADD IT ALL UP. WOW.
                techs.Add(name, new Technique(name, rokuName, rank,
                    AE, regTP, spTP,
                    rankTr, specTr,
                    sigTech, mmPrim, inPrim,
                    brTech, brRank,
                    type, range,
                    techStats,
                    auto, NA,
                    effList,
                    power, DFBoolList,
                    cyborgList, note,
                    cust, desc));
                // Jokes on you, now you gotta do the TechTbl
                techTbl.Rows.Add(name, rank, regTP, spTP, specTr, brTech,
                    type, range, techStats.getTechString(), power);
            }
            // Update the SpTrait List
            foreach (Technique tech in techs.Values) {
                try {
                    string spTraitName = tech.specialTrait;
                    spTraits.Find(x => x.getName() == spTraitName).usedTP += tech.spTP;
                }
                catch { } // If key doesn't exist, just pass it
            }
            // Now add onto the SpTrait Listview
            spTraitTbl.Rows.Clear();
            foreach (SpTrait spTrait in spTraits) {
                spTraitTbl.Rows.Add(spTrait.getName(), spTrait.usedTP, spTrait.totalTP);
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
            catTbl.ListViewItemSorter = new ListViewItemNumberSort(0);
            catTbl.Sort();
        }

        // bool devH if we're saving on devH instead
        public void loadCharSources(ref Dictionary<string, Source> sources,
            ref DataGridView dgv_Sources,
            ref RadioButton NADate, 
            bool devH,
            string data_in = "") {
            if (devH) { data = data_in; }
            bool NAcheck = TryParseBool(getParse(SOURCE_DATEFORMAT, SPLIT1));
            NADate.Checked = NAcheck;
            string sourceStr = getParse(TAG_STA_SOURCE, TAG_END_SOURCE);
            string[] sourceArr = splitStringbyString(sourceStr, SPLIT2);
            sources.Clear();
            dgv_Sources.Rows.Clear();
            dgv_Sources.Refresh();
            for (int i = 0; i < sourceArr.Length; ++i) {
                int year = TryParseInt(getParse(SOURCE_YYYY, SPLIT1, sourceArr[i]));
                int month = TryParseInt(getParse(SOURCE_MM, SPLIT1, sourceArr[i]));
                int day = TryParseInt(getParse(SOURCE_DD, SPLIT1, sourceArr[i]));
                bool noDate = TryParseBool(getParse(SOURCE_HASDATE, SPLIT1, sourceArr[i]));
                string title = getParse(SOURCE_TITLE, SPLIT1, sourceArr[i]);
                string URL = getParse(SOURCE_URL, SPLIT1, sourceArr[i]);
                int SD = TryParseInt(getParse(SOURCE_SD, SPLIT1, sourceArr[i]));
                int beli = TryParseInt(getParse(SOURCE_BELI, SPLIT1, sourceArr[i]));
                string notes = getParse(SOURCE_NOTE, SPLIT1, sourceArr[i]);
                DateTime date = new DateTime(year, month, day);
                Source add_Source = new Source(date, noDate, title, URL, SD, beli, notes);
                sources.Add(title, add_Source);
                // add to dgv
                CultureInfo culture = NAcheck ? new CultureInfo("en-US") : new CultureInfo("en-GB");
                string date_str = (!noDate) ? date.ToString("d", culture) : "";
                string SD_str = (SD > 0) ? SD.ToString() : "";
                string beli_str = (beli != 0) ? beli.ToString("N0") : "";
                DataGridViewButtonColumn button = new DataGridViewButtonColumn();
                dgv_Sources.Rows.Add(button, date_str, title, URL,
                    SD_str, beli_str, notes);
                dgv_Sources.Rows[i].Cells[0].Value = "X";
            }
        }

        public void loadCharTemplate(ref Label tempName,
            ref RichTextBox template,
            ref TextBox color,
            ref TextBox msg) {
            tempName.Text = getParse(TEMPLATE_NAME, SPLIT1);
            if (tempName.Text == "Standard Template") { tempName.ForeColor = Color.Green; }
            else { tempName.ForeColor = Color.Blue; }
            template.Text = getParse(TEMPLATE, SPLIT1);
            color.Text = getParse(COLOR, SPLIT1);
            msg.Text = getParse(MASTERYMSG, SPLIT1);
        }

        #endregion

        // Use of unique key for Character
        private const string OPRPKEY = "OPRPxddddd2009";

        public void saveFile() {
            string saveData = Serialize.encryptData(data, OPRPKEY);
            using (StreamWriter newTask = new StreamWriter(path, false)) {
                newTask.Write(saveData);
            }
        }

        public void openFile() {
            string encrypted = File.ReadAllText(path);
            data = Serialize.decryptData(encrypted, OPRPKEY);
        }

        // Default Constructor
        public Character() { }

    }
}
