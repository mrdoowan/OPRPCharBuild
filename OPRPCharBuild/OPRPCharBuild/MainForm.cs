/*
 * This MainForm should only contain Event Handlers
 * Everything else should be in its own file or class
 * Have a function that stores all Textbox into the Character class
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Linq;
using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace OPRPCharBuild
{
    public partial class MainForm : Form {
        public MainForm() {
            // Check for updates of a New Version or Bug Messages
            InitializeComponent();
            //this.Visible = false;
            //if (!Loading_Window()) { this.Visible = true; }
        }

        // --------------------------------------------------------------------------------------------
        // MEMBER VARIABLES
        // --------------------------------------------------------------------------------------------

        #region Member Variables

        public const string VERSION = "1.8.1";
        public const string STD_TEMPLATE_MSG = "Standard Template";
        private const string WEBSITE = "https://github.com/mrdoowan/OPRPCharBuild/releases";
        public static bool template_imported = false;
        private static bool saved = true;       // Used to keep track if saved
        // Character Class
        // The following will update the Character Class at real-time:
        // Professions, Traits, and Techniques
        Character profile = new Character();
        // Variables for Templates
        public static Dictionary<int, string> CustomTags = new Dictionary<int, string>();
        // Variables that need their data type here instead of in Character
        int genCurr, genCap, profCurr, profCap;
        private Dictionary<string, Profession> profList = new Dictionary<string, Profession>();
        private List<Trait> traitList = new List<Trait>();
        private Dictionary<string, Technique> techList = new Dictionary<string, Technique>();
        // Sp Table dictionary that won't be stored in Character
        private List<SpTrait> spTraitList = new List<SpTrait>();
        // Sources
        private Dictionary<string, Source> sourceList = new Dictionary<string, Source>();

        #endregion

        #region General Functions

        // General function for Deleting an Item from ListView
        // Returns the string Name of the Item, specifically for finding Key values in Dictionary
        public string Delete_ListViewItem(ref ListView list) {
            if (list.SelectedItems.Count == 1) {
                string key = list.SelectedItems[0].SubItems[0].Text;    // Typically the key value is always the name
                foreach (ListViewItem eachItem in list.SelectedItems) {
                    list.Items.Remove(eachItem);
                }
                notSavedStatus();
                return key;
            }
            return null;
        }

        // To move up or down the item in a ListView
        private void Move_List_Item(ref ListView list, string direction) {
            if (list.SelectedItems.Count == 0) { return; }
            int curr_ind = list.SelectedItems[0].Index;
            if (curr_ind < 0) {
                return;
            }
            else {
                ListViewItem item = list.Items[curr_ind];
                if (direction == "Up") {
                    if (curr_ind > 0) {
                        list.Items.RemoveAt(curr_ind);
                        list.Items.Insert(curr_ind - 1, item);
                        // Maintain selection
                        list.Items[curr_ind - 1].Selected = true;
                        notSavedStatus();
                    }
                }
                else if (direction == "Down") {
                    if (curr_ind < list.Items.Count - 1) {
                        list.Items.RemoveAt(curr_ind);
                        list.Items.Insert(curr_ind + 1, item);
                        // Maintain selection
                        list.Items[curr_ind + 1].Selected = true;
                        notSavedStatus();
                    }
                }
                else {
                    MessageBox.Show("There is a bug with this button!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        // To move up or down the selected item in a dataGridView
        // Requires direction to be "Up" or "Down"
        private void Move_DGV_Item(ref DataGridView dgv, string direction) {
            // Up
            try {
                int totalRows = dgv.Rows.Count;
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0 && direction == "Up") { return; }
                if (rowIndex == totalRows - 1 && direction == "Down") { return; }
                // get index of the column for the selected cell
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                if (direction == "Up") {
                    dgv.Rows.Insert(rowIndex - 1, selectedRow);
                    dgv.Rows[rowIndex - 1].Selected = true;
                }
                else {
                    dgv.Rows.Insert(rowIndex + 1, selectedRow);
                    dgv.Rows[rowIndex + 1].Selected = true;
                }
                notSavedStatus();
            }
            catch { }
        }

        // Start the Loading Window for cool dots (WORK ON THIS LATER)
        /*
        private bool Loading_Window() {
            // Implement PendingWindow for Coolness
            PendingWindow pendingWin = new PendingWindow();
            // Get the Timer loaded
            BackgroundWorker timerWorker = new BackgroundWorker();
            timerWorker.DoWork += new DoWorkEventHandler(pendingWin.start_Timer);
            timerWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(pendingWin.finished_Timer);
            timerWorker.RunWorkerAsync();
            // Now check for updates
            BackgroundWorker updateWorker = new BackgroundWorker();
            updateWorker.DoWork += new DoWorkEventHandler(Check_Update);
            return false;
        }
        */

        // Really don't want to use Microsoft's Click-Once application at this point, so I'll just
        // implement my own version.
        // Version now follows the following format:
        // Major.Minor.Revision (only 3)
        //private void Check_Update(object sender, DoWorkEventArgs e) {
        private void Check_Update() {
            // -------------------------------------
            // Version Check
            // -------------------------------------
            WebClient WC = new WebClient();
            try {
                string[] current = VERSION.Split('.');
                // Get latest version from site
                string header_msg = "OPRPCharBuilder " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " UpdateCheck " + Environment.OSVersion;
                WC.Headers.Add("Content-Type", header_msg);
                string version_page = WC.DownloadString("https://raw.githubusercontent.com/mrdoowan/OPRPCharBuild/master/CurrentVer.txt");
                string[] latest = version_page.Split('.');
                // Since we are looping through Current length, Current should not be bigger than Latest
                if (current.Length > latest.Length) {
                    MessageBox.Show("The current Length is greater than latest Length.", "Report Bug", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                for (int i = 0; i < current.Length; ++i) {
                    // We ask for update if latest > current
                    if (int.Parse(latest[i]) != int.Parse(current[i])) {
                        // Version # does not align
                        if (int.Parse(latest[i]) > int.Parse(current[i])) {
                            if (MessageBox.Show("An update to v" + version_page + " is available. Would you like to close this application and download the newest version?", "New Version",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
                                Process.Start(WEBSITE);
                                Process.Start("http://s1.zetaboards.com/One_Piece_RP/topic/6060583/1/");
                                saved = true;
                                Application.Exit();
                            }
                            return;
                        }
                    }
                }
            }
            catch { }
        }

        // Returns true if Prof is in the List and it's Primary
        // Returns false otherwise
        private bool Is_Prof_Primary(string name) {
            if (profList.ContainsKey(name)) {
                if (profList[name].primary) {
                    return true;
                }
            }
            return false;
        }

        // Makes the Devil Fruit class based on what's initialized
        private DevilFruit makeDFClass() {
            return new DevilFruit(textBox_DFName.Text,
                comboBox_DFType.Text,
                comboBox_DFTier.Text,
                richTextBox_DFDesc.Text);
        }

        #endregion

        #region Update Functions

        #region Update Trait Functions

        // Whenever a trait is added or deleted, we update the displayed number of Traits
        private void Update_Traits_Count_Label() {
            int gen = 0;    // 2nd Column
            int prof = 0;   // 3rd Column
            foreach (Trait trait in traitList) {
                gen += trait.genNum;
                prof += trait.profNum;
            }
            genCurr = gen;
            profCurr = prof;
            label58_TraitsCurrent.Text = "You currently have " + gen + " General Trait(s) and " +
                prof + " Professional Trait(s)";
            if (genCurr == genCap && profCurr == profCap) {
                label58_TraitsCurrent.ForeColor = Color.Green;
            }
            else {
                label58_TraitsCurrent.ForeColor = Color.Red;
            }
        }

        // Whenever SD Earned is updated, we have to update the max amount of Traits.
        private void Update_Traits_Cap() {
            int gen = 0, prof = 0;
            int SD_Earned = (int)numericUpDown_SDEarned.Value;
            if (SD_Earned < 50) {
                gen = 3;
                prof = 1;
            }
            else if (SD_Earned >= 50 && SD_Earned < 100) {
                gen = 4;
                prof = 2;
            }
            else if (SD_Earned >= 100 && SD_Earned < 150) {
                gen = 5;
                prof = 2;
            }
            else if (SD_Earned >= 150 && SD_Earned < 200) {
                gen = 6;
                prof = 3;
            }
            else if (SD_Earned >= 200 && SD_Earned < 250) {
                gen = 7;
                prof = 3;
            }
            else if (SD_Earned >= 250 && SD_Earned < 275) {
                gen = 7;
                prof = 4;
            }
            else if (SD_Earned >= 275 && SD_Earned < 350) {
                gen = 8;
                prof = 4;
            }
            else if (SD_Earned >= 350 && SD_Earned < 425) {
                gen = 9;
                prof = 5;
            }
            else if (SD_Earned >= 425 && SD_Earned < 500) {
                gen = 10;
                prof = 5;
            }
            else {
                gen = 11;
                prof = 6;
            }
            // Check AP Traits
            gen += (int)numericUpDown_APTrait.Value;
            // Update label and global variable.
            genCap = gen;
            profCap = prof;
            // Update Focus
            int focus = 1 + gen / 2;
            if (focus > 7) { focus = 7; }
            textBox_Focus.Text = focus.ToString();
            // Update Traits Message
            label59_TraitsCalc.Text = "Your current cap is " + gen +
                " General Trait(s) and " + prof + " Professional Trait(s)";
            if (genCurr == genCap && profCurr == profCap) {
                label58_TraitsCurrent.ForeColor = Color.Green;
            }
            else {
                label58_TraitsCurrent.ForeColor = Color.Red;
            }
        }

        #endregion

        #region Update Stats Functions

        // Used when an AP is checked.
        private void Update_AP_Count() {
            int AP_num = (int)(numericUpDown_APTech.Value +
                2 * numericUpDown_APTrait.Value +
                numericUpDown_APPrime.Value +
                numericUpDown_APMulti.Value +
                numericUpDown_APNPC.Value);
            AP_num += (checkBox_APHaki.Checked) ? 2 : 0;
            AP_num += (checkBox_APDF.Checked) ? 1 : 0;
            int SD = AP_num * 50;
            textBox_AP.Text = AP_num.ToString();
            label_SDonAP.Text = SD + " SD spent on ";
        }

        // For below function
        private const int CONV_CAP_1 = 200;
        private const int CONV_CAP_2 = 600;
        private const int CONV_CAP_3 = 1000;

        // Calculating Stat Points
        private void Update_Stat_Points() {
            int SD_in = (int)numericUpDown_SDintoStats.Value;
            string calc = "[32";
            int SP = 32;
            if (SD_in >= 0 && SD_in <= CONV_CAP_1) {
                // 1:1
                SP += SD_in;
                calc += " + " + SD_in;
            }
            else if (SD_in > CONV_CAP_1 && SD_in <= CONV_CAP_2) {
                // 2:1
                int remain = SD_in - CONV_CAP_1; // this is in SD
                int convert = remain / 2;
                SP += 200 + convert;
                calc += " + 200 + " + convert + " (" + remain + "/2)";
            }
            else if (SD_in > CONV_CAP_2 && SD_in <= CONV_CAP_3) {
                // 4:1
                int remain = SD_in - CONV_CAP_1 - CONV_CAP_2;
                int convert = remain / 3;
                SP += 200 + 200 + convert;
                calc += " + 200 + 200 (400/2) + " + convert + " (" + remain + "/4)";
            }
            else {
                // 5:1
                int remain = SD_in - CONV_CAP_1 - CONV_CAP_2 - CONV_CAP_3;
                int convert = remain / 5;
                SP += 200 + 200 + 100 + convert;
                calc += " + 200 + 200 (400/2) + 100 (400/4) " + convert + " (" + remain + "/5)";
            }
            calc += ']';
            textBox_StatPoints.Text = SP.ToString();
            textBox_SDtoSPCalc.Text = calc;
            // Used when SD into Stats is changed
        }

        private void Update_TotalSD() {
            textBox_TotalSD.Text = ((int)numericUpDown_SDEarned.Value + int.Parse(textBox_AP.Text) * 50).ToString();
            // Used when AP is Checked
            // Used when SD Earned is Changed
        }

        // Calculating SD Remaining after remnants of Stat Points
        private void Update_SD_Remaining() {
            textBox_SDRemain.Text = (numericUpDown_SDEarned.Value - numericUpDown_SDintoStats.Value).ToString();
            // Used when SD Earned is changed
            // Used when SD into Stats is changed
        }

        // Calculating Used for Stat Points
        private void Update_Used_for_Stats() {
            textBox_UsedForStats.Text = (int.Parse(textBox_StatPoints.Text) - numericUpDown_UsedForFort.Value).ToString();
            // Used when Stat Points is changed
            // Used when Used for Fortune is changed
        }

        private void Update_Fortune() {
            string calc = "[";
            // First Stat Points / 4
            int fortune = int.Parse(textBox_UsedForStats.Text) / 4;
            calc += textBox_UsedForStats.Text + " / 4";
            // Then Fortune from Used for Fortune
            int used_for = (int)numericUpDown_UsedForFort.Value / 5 * 3;
            if (used_for > 0) {
                fortune += used_for;
                calc += " + (" + numericUpDown_UsedForFort.Value + " / 5 * 3)";
            }
            // Then Fortune from Fate of Emperor
            if (traitList.Any(x => x.name == Database.TR_FATEEM)) {
                // # Gen Traits is Column 2
                int traits = traitList.Find(x => x.name == Database.TR_FATEEM).genNum;
                fortune += traits;
                calc += " + " + traits;
            }
            calc += ']';
            // Total Fortune display along with calculation
            label_FortuneCalc.Text = calc;
            textBox_Fortune.Text = fortune.ToString();
            // Used when Stat Points is changed
            // Used when Used for Fortune is changed
            // Used when Traits are added
            // Used when Traits are edited
            // Used when Traits are removed
        }

        private void Update_BaseStats_Check() {
            int total = (int)(numericUpDown_StrengthBase.Value +
                numericUpDown_SpeedBase.Value +
                numericUpDown_StaminaBase.Value +
                numericUpDown_AccuracyBase.Value);
            if (total == int.Parse(textBox_UsedForStats.Text)) {
                label_GenerateCheck.Text = "Base stats added correctly!";
                label_GenerateCheck.ForeColor = Color.Green;
            }
            else {
                label_GenerateCheck.Text = "Base Stat values do not add up!\n";
                label_GenerateCheck.Text += numericUpDown_StrengthBase.Value + " + ";
                label_GenerateCheck.Text += numericUpDown_SpeedBase.Value + " + ";
                label_GenerateCheck.Text += numericUpDown_StaminaBase.Value + " + ";
                label_GenerateCheck.Text += numericUpDown_AccuracyBase.Value + " = ";
                label_GenerateCheck.Text += total;
                label_GenerateCheck.ForeColor = Color.Red;
            }
            // Used for when any of the base Stats changes.
            // Used when Used for Stats is changed
        }

        private void Stat_Multiplier_Trait(ref int base_stat, ref string calc, double multiplier) {
            if (base_stat <= 75) {
                base_stat = (int)((double)base_stat * multiplier);
                calc += " * " + multiplier.ToString();
            }
            else {
                // Maxes out at base stat 75
                // I can't do arithmetic operations with two doubles or
                // it screws up by like 9 decimal places >_>
                if (multiplier == 1.2) {
                    multiplier = 0.2;
                }
                else if (multiplier == 1.4) {
                    multiplier = 0.4;
                }
                else if (multiplier == 1.6) {
                    multiplier = 0.6;
                }
                else {
                    MessageBox.Show("Stat Multipliers screwed up.", "Error");
                }
                int base_75 = (int)(75 * multiplier);
                base_stat += base_75;
                calc += " + " + base_75;
            }
        }

        // This is only for changing stats by Fated
        private void Add_Fated_Stats(ref int stat, ref string calc, string fated) {
            if (traitList.Any(x => x.name == fated)) {
                int Traits = traitList.Find(x => x.name == fated).genNum;
                stat += 3 * Traits;
                calc += " + (3 * " + Traits + ")";
            }
        }

        // Note: Stat Traits ONLY multiplies the Base Stat. And then you apply any other bonuses.
        private void Update_Strength_Final() {
            int base_stat = (int)numericUpDown_StrengthBase.Value;
            int final = base_stat;
            string calc = "[" + base_stat;
            // Do the base stats first.
            if (traitList.Any(x => x.name == Database.TR_STR3RD)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.6);
            }
            else if (traitList.Any(x => x.name == Database.TR_STR2ND)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.4);
            }
            else if (traitList.Any(x => x.name == Database.TR_STR1ST) ||
                traitList.Any(x => x.name == Database.TR_FISHMA) ||
                traitList.Any(x => x.name == Database.TR_DWARF)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.2);

            }
            // And then lastly, the Fated Trait.
            Add_Fated_Stats(ref final, ref calc, Database.TR_FATEST);
            textBox_StrengthFinal.Text = final.ToString();
            calc += ']';
            label_StrengthCalc.Text = calc;
            // Used when Base Strength changes.
            // Used when Traits are added.
            // Used when Traits are edited
            // Used when Traits are removed.
        }

        private void Update_Speed_Final() {
            // I hate copying pasting code...
            int base_stat = (int)numericUpDown_SpeedBase.Value;
            int final = base_stat;
            string calc = "[" + base_stat;
            // Do the base stats first.
            if (traitList.Any(x => x.name == Database.TR_SPE3RD)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.6);
            }
            else if (traitList.Any(x => x.name == Database.TR_SPE2ND)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.4);
            }
            else if (traitList.Any(x => x.name == Database.TR_SPE1ST) ||
                traitList.Any(x => x.name == Database.TR_MERFOL)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.2);

            }
            // And then lastly, the Fated Trait.
            Add_Fated_Stats(ref final, ref calc, Database.TR_FATESW);
            textBox_SpeedFinal.Text = final.ToString();
            calc += ']';
            label_SpeedCalc.Text = calc;
            // Used when Base Strength changes.
            // Used when Traits are added.
            // Used when Traits are edited
            // Used when Traits are removed.
        }

        private void Update_Stamina_Final() {
            // I hate copying pasting code...
            int base_stat = (int)numericUpDown_StaminaBase.Value;
            int final = base_stat;
            string calc = "[" + base_stat;
            // Do the base stats first.
            if (traitList.Any(x => x.name == Database.TR_STA3RD)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.6);
            }
            else if (traitList.Any(x => x.name == Database.TR_STA2ND)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.4);
            }
            else if (traitList.Any(x => x.name == Database.TR_STA1ST)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.2);
            }
            // And then lastly, the Fated Trait.
            Add_Fated_Stats(ref final, ref calc, Database.TR_FATEMI);
            textBox_StaminaFinal.Text = final.ToString();
            calc += ']';
            label_StaminaCalc.Text = calc;
            // Used when Base Strength changes.
            // Used when Traits are added.
            // Used when Traits are edited
            // Used when Traits are removed.
        }

        private void Update_Accuracy_Final() {
            // I hate copying pasting code...
            int base_stat = (int)numericUpDown_AccuracyBase.Value;
            int final = base_stat;
            string calc = "[" + base_stat;
            // Do the base stats first.
            if (traitList.Any(x => x.name == Database.TR_ACC3RD)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.6);
            }
            else if (traitList.Any(x => x.name == Database.TR_ACC2ND)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.4);
            }
            else if (traitList.Any(x => x.name == Database.TR_ACC1ST)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.2);

            }
            // And then lastly, the Fated Trait.
            Add_Fated_Stats(ref final, ref calc, Database.TR_FATECU);
            textBox_AccuracyFinal.Text = final.ToString();
            calc += ']';
            label_AccuracyCalc.Text = calc;
            // Used when Base Strength changes.
            // Used when Traits are added.
            // Used when Traits are edited
            // Used when Traits are removed.
        }

        #endregion 

        #region Update RegTP Functions

        private void Update_Used_RegTP() {
            int used = 0;
            foreach (Technique tech in techList.Values) {
                used += tech.regTP;
            }
            textBox_RegTPUsed.Text = used.ToString();
            // Used when Techniques are added.
            // Used when Techniques are edited.
            // Used when Techniques are removed.
        }

        private void Update_Total_RegTP() {
            double multiplier = 2.0;
            int fortune = int.Parse(textBox_Fortune.Text);
            string calc = "[" + fortune + " * ";
            // Modify multiplier if SD Earned is changed.
            int SD_Earned = (int)numericUpDown_SDEarned.Value;
            if (SD_Earned > 150 && SD_Earned <= 250) {
                multiplier = 2.5;
            }
            else if (SD_Earned > 250 && SD_Earned <= 350) {
                multiplier = 3.0;
            }
            else if (SD_Earned > 350 && SD_Earned <= 450) {
                multiplier = 3.5;
            }
            else if (SD_Earned > 450 && SD_Earned <= 600) {
                multiplier = 4.0;
            }
            else if (SD_Earned > 600 && SD_Earned <= 800) {
                multiplier = 4.5;
            }
            else if (SD_Earned > 800) {
                multiplier = 5.0;
            }
            // Check AP Tech
            multiplier += 0.5 * (double)(numericUpDown_APTech.Value);
            int total = (int)((double)fortune * multiplier);
            calc += multiplier;
            // Now check if we have any Traits that add to this.
            if (traitList.Any(x => x.name == Database.TR_TECHMA)) {
                // Increase by 100% of Fortune
                total += fortune;
                calc += " + " + fortune;
            }
            else if (traitList.Any(x => x.name == Database.TR_TECHAD)) {
                // Increase by 40% of Fortune
                total += (int)((double)fortune * 0.4);
                calc += " + (" + fortune + " * 0.4)";
            }
            // Update properly.
            textBox_RegTPTotal.Text = total.ToString();
            calc += ']';
            label_RegTPCalc.Text = calc;
            // Used when checkbox is listed.
            // Used when Fortune is updated.
            // Used when SD Earned is changed
            // Used when a Trait is added.
            // Used when a Trait is removed.
        }

        #endregion

        #region Update Tech Misc Functions

        // Helper function for Update_CritAnatQuick_Msg()
        private void Print_Applied_Msg(ref string msg, string name) {
            int points = int.Parse(textBox_RegTPTotal.Text) / 4;
            int num = 0;
            msg += name + ": ";
            // How many Traits of the there are of the same Trait
            if (traitList.Any(x => x.name == name)) {
                num += traitList.Find(x => x.name == name).getTotal();
            }
            int total = num * points;
            int used = 0;
            foreach (Technique tech in techList.Values) {
                if (tech.stats.statsName.Contains(name)) {
                    used += tech.regTP;
                }
            }
            msg += used + " / " + total + '\n';
        }

        // This is only for Critical Hit, Anatomical Strike, and Quickstrike
        private void Update_CritAnatQuick_Msg() {
            string msg = "";
            if (traitList.Any(x => x.name == Database.TR_CRITHI)) {
                Print_Applied_Msg(ref msg, Database.TR_CRITHI);
            }
            if (traitList.Any(x => x.name == Database.TR_ANASTR)) {
                Print_Applied_Msg(ref msg, Database.TR_ANASTR);
            }
            if (traitList.Any(x => x.name == Database.TR_QUICKS)) {
                Print_Applied_Msg(ref msg, Database.TR_QUICKS);
            }
            // Trim the newline
            msg = msg.TrimEnd('\n');
            if (string.IsNullOrWhiteSpace(msg)) {
                label_CritAnatQuick.Visible = false;
                label_CritAnatQuick.Text = msg;
            }
            else {
                label_CritAnatQuick.Visible = true;
                label_CritAnatQuick.Text = msg;
            }
            // Updated when Technique is Added
            // Updated when Technique is Edited
            // Updated when Technique is Branched
            // Updated when Technique is Removed
            // Updated when a Trait is added.
            // Updated when a Trait is removed.
            // Updated when Total Reg TP is Changed.

        }

        private void Update_TechNum() {
            // This will also properly set the Maximum Values of numericupdown_Row
            int num = dgv_Techniques.Rows.Count;
            label_TechCount.Text = "Total number of Techniques: " + num;
            if (num > 0) {
                numericUpDown_RowBegin.Maximum = num - 1;
                numericUpDown_RowEnd.Maximum = num - 1;
            }
            else if (num == 0) {
                numericUpDown_RowBegin.Maximum = 0;
                numericUpDown_RowEnd.Maximum = 0;
            }
            // Updated when Technique is Added
            // Updated when Technique is Edited
            // Updated when Technique is Branched
            // Updated when Technique is Removed
        }

        #endregion

        #region Update Category Functions

        // Checks to make sure that all Techniques are in Categories
        private void Update_SubCatWarning() {
            bool loop_break = false;
            for (int i = 0; i < listView_SubCat.Items.Count; ++i) {
                ListViewItem category = listView_SubCat.Items[i];
                if (i == 0 && int.Parse(category.SubItems[0].Text) != 0) {
                    loop_break = true;
                    break;
                }
                if (i == listView_SubCat.Items.Count - 1 &&
                    int.Parse(category.SubItems[1].Text) != dgv_Techniques.Rows.Count - 1) {
                    loop_break = true;
                    break;
                }
                if (i != listView_SubCat.Items.Count - 1) {
                    if (int.Parse(category.SubItems[1].Text) + 1 !=
                    int.Parse(listView_SubCat.Items[i + 1].SubItems[0].Text)) {
                        // Comparing this category's End Row with the next category's Begin Row
                        loop_break = true;
                        break;
                    }
                }
            }
            if (listView_SubCat.Items.Count == 0) {
                label_SubCatWarning.Text = "Note: Adding no Categories will automatically put all Techniques into one table.";
                label_SubCatWarning.ForeColor = Color.OrangeRed;
            }
            else if (loop_break) {
                // Display Warning Message 
                label_SubCatWarning.Text = "WARNING: Some Techniques aren't in Categories! Uncategorized Techniques will not be generated!";
                label_SubCatWarning.ForeColor = Color.Red;
            }
            else {
                // All Techniques accounted for!
                label_SubCatWarning.Text = "All Techniques are in Categories.";
                label_SubCatWarning.ForeColor = Color.Green;
            }
            // Updated when a SubCategory is Added
            // Updated when a SubCategory is Removed
        }

        #endregion

        #endregion

        #region Main Form Loading

        // This only occurs once before the form is displayed for the first time.
        private void MainForm_Load(object sender, EventArgs e) {
            Check_Update();

            this.Text = "OPRP Character Builder";
            label_Title.Text = "OPRP Character Builder";
            label1.Text = "OPRP Character Builder v" + VERSION + " designed by Solo";

            // ------ Images
            listView_Images.View = View.Details;
            listView_Images.FullRowSelect = true;

            listView_Images.Columns.Add("URL", 300);
            listView_Images.Columns.Add("Label", 200);
            listView_Images.Columns.Add("FullRes", 56);
            listView_Images.Columns.Add("Width", 55);
            listView_Images.Columns.Add("Height", 55);

            // ------ Traits
            Update_Traits_Cap();

            // ------ Tech Category Table
            label_SubCatMsg.Text = "No Valid Category Selected";
            listView_SubCat.View = View.Details;
            listView_SubCat.FullRowSelect = true;
            listView_SubCat.Sorting = SortOrder.Ascending;

            listView_SubCat.Columns.Add("Begin", 50);
            listView_SubCat.Columns.Add("End", 50);
            listView_SubCat.Columns.Add("Category Name", 166);

            // ------ Weaponry Table
            listView_Weaponry.View = View.Details;
            listView_Weaponry.FullRowSelect = true;

            listView_Weaponry.Columns.Add("Name", 150);
            listView_Weaponry.Columns.Add("Description", 546);

            // ------ Items Table
            listView_Items.View = View.Details;
            listView_Items.FullRowSelect = true;

            listView_Items.Columns.Add("Name", 150);
            listView_Items.Columns.Add("Description", 546);

            // ------ Template
            richTextBox_Template.Text = Sheet.BASIC_TEMPLATE;

            // ------ Adding Save Changes Controls
            // --- tabPage: Basic Information
            foreach (Control c in groupBox_BasicInfo.Controls) {
                addAnySaveEvent(c);
            }
            foreach (Control c in groupBox_Features.Controls) {
                addAnySaveEvent(c);
            }
            richTextBox_Clothing.TextChanged += notSavedEvent;
            richTextBox_GeneralAppear.TextChanged += notSavedEvent;
            // --- tabPage: Background
            foreach (Control c in groupBox_Background.Controls) {
                addAnySaveEvent(c);
            }
            // --- tabPage: Abilities
            richTextBox_Combat.TextChanged += notSavedEvent;
            foreach (Control c in groupBox_DevilFruit.Controls) {
                addAnySaveEvent(c);
            }
            // --- tabPage: RP Elements
            foreach (Control c in groupBox_Stats.Controls) {
                addAnySaveEvent(c);
            }
            foreach (Control c in groupBox_AP.Controls) {
                addAnySaveEvent(c);
            }
            // --- tabPage: Traits
            // --- tabPage: Techniques
            // --- tabPage: Sources
            // --- tabPage: Template
            textBox_Color.TextChanged += notSavedEvent;
            textBox_MasteryMsg.TextChanged += notSavedEvent;
        }

        // Helper Function: for TextBox, RichTextBox, ComboBox, NumericUpDown, CheckBox
        private void addAnySaveEvent(Control c) {
            if (c is TextBox) {
                if (((TextBox)c).ReadOnly) { return; }
                c.TextChanged += notSavedEvent;
            }
            else if (c is RichTextBox) {
                if (((RichTextBox)c).ReadOnly) { return; }
                c.TextChanged += notSavedEvent;
            }
            else if (c is ComboBox) {
                ((ComboBox)c).TextChanged += notSavedEvent;
                ((ComboBox)c).SelectedIndexChanged += notSavedEvent;
            }
            else if (c is NumericUpDown) {
                ((NumericUpDown)c).ValueChanged += notSavedEvent;
            }
            else if (c is CheckBox) {
                ((CheckBox)c).CheckedChanged += notSavedEvent;
            }
        }

        // The event firing for notSavedStatus()
        private void notSavedEvent(object sender, EventArgs e) {
            notSavedStatus();
        }

        // To indicate that the Form is not saved. Happens when something changes
        private void notSavedStatus() {
            this.Text = this.Text.TrimEnd('*');
            this.Text += "*";
            saved = false;
        }

        // To indicate that the Form is saved. Only happens when Save or Save As is pressed
        private void isSavedStatus() {
            this.Text = this.Text.TrimEnd('*');
            saved = true;
        }

        #endregion

        // --------------------------------------------------------------------------------------------
        // "BASIC INFORMATION" Tab
        // --------------------------------------------------------------------------------------------

        #region Basic Character Tab

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            // This is when we change the Affiliation ComboBox. Exception should only happen when we change.
            string affiliation = comboBox_Affiliation.Text;
            if (affiliation == "Pirate") {
                textBox_Bounty.Enabled = true;
                numericUpDown_Comm.Enabled = false;
                numericUpDown_Comm.Value = 0;
                comboBox_MarineRank.Enabled = false;
                comboBox_MarineRank.SelectedIndex = -1;
                textBox_Threat.Enabled = false;
                textBox_Threat.Clear();
            }
            else if (affiliation == "Marine") {
                textBox_Bounty.Enabled = false;
                textBox_Bounty.Clear();
                numericUpDown_Comm.Enabled = true;
                comboBox_MarineRank.Enabled = true;
                textBox_Threat.Enabled = false;
                textBox_Threat.Clear();
            }
            else if (affiliation == "Bounty Hunter" || affiliation == "Other") {
                textBox_Bounty.Enabled = false;
                textBox_Bounty.Clear();
                numericUpDown_Comm.Enabled = false;
                numericUpDown_Comm.Value = 0;
                comboBox_MarineRank.Enabled = false;
                comboBox_MarineRank.SelectedIndex = -1;
                textBox_Threat.Enabled = true;
            }
            else {
                textBox_Bounty.Enabled = false;
                textBox_Bounty.Clear();
                numericUpDown_Comm.Enabled = false;
                numericUpDown_Comm.Value = 0;
                comboBox_MarineRank.Enabled = false;
                comboBox_MarineRank.SelectedIndex = -1;
                textBox_Threat.Enabled = false;
                textBox_Threat.Clear();
            }
        }

        private void numericUpDown_Comm_ValueChanged(object sender, EventArgs e) {
            comboBox_MarineRank.Items.Clear();
            if (numericUpDown_Comm.Value >= 300) {
                string[] admiral = { "Vice Admiral", "Admiral", "Fleet Admiral" };
                comboBox_MarineRank.Items.AddRange(admiral);
            }
            else if (numericUpDown_Comm.Value >= 200) {
                comboBox_MarineRank.Items.Add("Rear Admiral");
            }
            else if (numericUpDown_Comm.Value >= 100) {
                comboBox_MarineRank.Items.Add("Commodore");
            }
            else if (numericUpDown_Comm.Value >= 65) {
                comboBox_MarineRank.Items.Add("Captain");
            }
            else if (numericUpDown_Comm.Value >= 35) {
                comboBox_MarineRank.Items.Add("Commander");
            }
            else if (numericUpDown_Comm.Value >= 20) {
                comboBox_MarineRank.Items.Add("Lieutenant Commander");
            }
            else if (numericUpDown_Comm.Value >= 15) {
                comboBox_MarineRank.Items.Add("Lieutenant");
            }
            else if (numericUpDown_Comm.Value >= 10) {
                comboBox_MarineRank.Items.Add("Junior Lieutenant");
            }
            else if (numericUpDown_Comm.Value >= 5) {
                comboBox_MarineRank.Items.Add("Ensign");
            }
            else {
                string[] begRanks = {"Chief Warrant Officer",
                    "Master Chief Petty Officer", "Chief Petty Officer",
                    "Petty Officer", "Seaman", "Seaman Apprentice",
                    "Seaman Recruit", "Apprentice", "Chore Boy"};
                comboBox_MarineRank.Items.AddRange(begRanks);
            }
            // Set to 0
            comboBox_MarineRank.SelectedIndex = 0;
        }

        private void button3_AchieveAdd_Click(object sender, EventArgs e) {
            // Achievement "Add" button from the MainForm
            Add_Achievement AchievementWin = new Add_Achievement();
            if (AchievementWin.NewDialog(ref listBox_Achieve)) { notSavedStatus(); }
            // Look above to show how to transfer data between forms. This uses references quite well.
        }

        private void button_AchieveEdit_Click(object sender, EventArgs e) {
            // Achievement "Edit" button from the MainForm
            int index = listBox_Achieve.SelectedIndex;
            if (index != -1) {
                Add_Achievement AchievementWin = new Add_Achievement();
                if (AchievementWin.EditDialogue(ref listBox_Achieve, index)) { notSavedStatus(); }
            }
        }

        private void button2_AchieveDelete_Click(object sender, EventArgs e) {
            // Achievement "Delete" button from the MainForm
            int index = listBox_Achieve.SelectedIndex;
            if (index != -1) {
                listBox_Achieve.Items.RemoveAt(index);
                notSavedStatus();
            }
        }

        private void MoveListBoxItem(int direction) {
            // Check selected item
            if (listBox_Achieve.SelectedItem == null || listBox_Achieve.SelectedIndex < 0) {
                return;
            }
            int newIndex = listBox_Achieve.SelectedIndex + direction;
            // Check bounds
            if (newIndex < 0 || newIndex >= listBox_Achieve.Items.Count) {
                return;
            }
            object selected = listBox_Achieve.SelectedItem;
            listBox_Achieve.Items.Remove(selected);
            listBox_Achieve.Items.Insert(newIndex, selected);
            listBox_Achieve.SetSelected(newIndex, true);
            notSavedStatus();
        }

        private void button_UpAchieve_Click(object sender, EventArgs e) {
            MoveListBoxItem(-1);
        }

        private void button_DownAchieve_Click(object sender, EventArgs e) {
            MoveListBoxItem(1);
        }

        private void button_ImageUp_Click(object sender, EventArgs e) {
            Move_List_Item(ref listView_Images, "Up");
        }

        private void button_ImageDown_Click(object sender, EventArgs e) {
            Move_List_Item(ref listView_Images, "Down");
        }

        private void button_ImageAdd_Click(object sender, EventArgs e) {
            // Image "Add" button from the MainForm
            if (string.IsNullOrWhiteSpace(textBox_ImageURL.Text)) {
                MessageBox.Show("Image needs URL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                ListViewItem image = new ListViewItem();
                image.SubItems[0].Text = textBox_ImageURL.Text;
                image.SubItems.Add(textBox_ImageLabel.Text);
                if (checkBox_FullRes.Checked) {
                    image.SubItems.Add("Yes");
                    image.SubItems.Add("");
                    image.SubItems.Add("");
                }
                else {
                    image.SubItems.Add("No");
                    image.SubItems.Add(numericUpDown_Width.Value.ToString());
                    image.SubItems.Add(numericUpDown_Height.Value.ToString());
                }
                // Add to ListView
                listView_Images.Items.Add(image);
                // Clear the information
                textBox_ImageLabel.Clear();
                textBox_ImageURL.Clear();
                checkBox_FullRes.Checked = true;
                notSavedStatus();
            }
        }

        private void button_ImageEdit_Click(object sender, EventArgs e) {
            // Traits "Edit" button from the MainForm
            // This is completely assuming that only one row can be selected (which we set MultiSelect = false)
            if (listView_Images.SelectedItems.Count == 1) {
                textBox_ImageURL.Text = listView_Images.SelectedItems[0].SubItems[0].Text;
                textBox_ImageLabel.Text = listView_Images.SelectedItems[0].SubItems[1].Text;
                if (listView_Images.SelectedItems[0].SubItems[2].Text == "No") {
                    checkBox_FullRes.Checked = false;
                    try { numericUpDown_Width.Value = int.Parse(listView_Images.SelectedItems[0].SubItems[3].Text); }
                    catch { }
                    try { numericUpDown_Height.Value = int.Parse(listView_Images.SelectedItems[0].SubItems[4].Text); }
                    catch { }
                }
                else {
                    checkBox_FullRes.Checked = true;
                }
                // Delete from ListView
                Delete_ListViewItem(ref listView_Images);
                notSavedStatus();
            }
        }

        private void button_ImageDelete_Click(object sender, EventArgs e) {
            // Image "Delete" button
            Delete_ListViewItem(ref listView_Images);
        }

        private void checkBox_FullRes_CheckedChanged(object sender, EventArgs e) {
            if (!checkBox_FullRes.Checked) {
                numericUpDown_Height.Enabled = true;
                numericUpDown_Width.Enabled = true;
            }
            else {
                numericUpDown_Height.Enabled = false;
                numericUpDown_Width.Enabled = false;
            }
        }

        #endregion

        // --------------------------------------------------------------------------------------------
        // "BACKGROUND" Tab
        // --------------------------------------------------------------------------------------------

        // Nothing

        // --------------------------------------------------------------------------------------------
        // "COMBAT" Tab
        // --------------------------------------------------------------------------------------------

        #region Combat Tab

        private void button6_WeaponAdd_Click(object sender, EventArgs e) {
            // Weapon "Add" button from the MainForm
            Add_Equipment EquipmentWin = new Add_Equipment();
            if (EquipmentWin.Add_Weapon(ref listView_Weaponry)) { notSavedStatus(); }
        }

        private void button_WeaponEdit_Click(object sender, EventArgs e) {
            // Weapon "Edit" button from the MainForm
            // This is completely assuming that only one row can be selected (which we set MultiSelect = false)
            if (listView_Weaponry.SelectedItems.Count == 1) {
                Add_Equipment EquipmentWin = new Add_Equipment();
                if (EquipmentWin.Edit_Weapon(ref listView_Weaponry)) { notSavedStatus(); }
            }
        }

        private void button7_WeaponDelete_Click(object sender, EventArgs e) {
            // Weapon "Delete" button from the MainForm
            // This is completely assuming that only one row can be selected (which we set MultiSelect = false)
            Delete_ListViewItem(ref listView_Weaponry);
        }

        private void button_UpWeapon_Click(object sender, EventArgs e) {
            Move_List_Item(ref listView_Weaponry, "Up");
        }

        private void button_DownWeapon_Click(object sender, EventArgs e) {
            Move_List_Item(ref listView_Weaponry, "Down");
        }

        private void button8_ItemsAdd_Click(object sender, EventArgs e) {
            // Item "Add" button from the MainForm
            Add_Equipment EquipmentWin = new Add_Equipment();
            if (EquipmentWin.Add_Item(ref listView_Items)) { notSavedStatus(); }
        }

        private void button_ItemsEdit_Click(object sender, EventArgs e) {
            // Item "Edit" button from the MainForm
            // This is completely assuming that only one row can be selected (which we set MultiSelect = false)
            if (listView_Items.SelectedItems.Count == 1) {
                Add_Equipment EquipmentWin = new Add_Equipment();
                if (EquipmentWin.Edit_Item(ref listView_Items)) { notSavedStatus(); }
            }
        }

        private void button9_ItemsDelete_Click(object sender, EventArgs e) {
            // Item "Delete" button from the MainForm
            // This is completely assuming that only one row can be selected (which we set MultiSelect = false)
            Delete_ListViewItem(ref listView_Items);
        }

        private void button_UpItem_Click(object sender, EventArgs e) {
            Move_List_Item(ref listView_Items, "Up");
        }

        private void button_DownItem_Click(object sender, EventArgs e) {
            Move_List_Item(ref listView_Items, "Down");
        }

        #endregion

        // --------------------------------------------------------------------------------------------
        // "RP ELEMENTS" Tab
        // --------------------------------------------------------------------------------------------

        #region RP Elements Tab

        private void numericUpDown_SDEarned_ValueChanged(object sender, EventArgs e) {
            // We have to adjust the max value of SD Earned into AP.
            numericUpDown_SDintoStats.Maximum = numericUpDown_SDEarned.Value;
            numericUpDown_SDintoStats.Value = numericUpDown_SDEarned.Value;
            Update_Traits_Cap(); // Trait Cap is based on SD Earned
            Update_SD_Remaining();
            Update_Total_RegTP();
            Update_TotalSD();
        }

        private void numericUpDown_SDintoStats_ValueChanged(object sender, EventArgs e) {
            Update_Stat_Points();
            Update_SD_Remaining();
        }

        private void textBox_StatPoints_TextChanged(object sender, EventArgs e) {
            Update_Used_for_Stats();
            // Also set the maximum value of Used for Fortune = Stat Points / 4
            numericUpDown_UsedForFort.Maximum = int.Parse(textBox_StatPoints.Text) / 4;
            Update_Fortune();
            // Also set the maximum value of all Base Stats = Stat Points / 2
            numericUpDown_StrengthBase.Maximum = int.Parse(textBox_StatPoints.Text) / 2;
            numericUpDown_SpeedBase.Maximum = int.Parse(textBox_StatPoints.Text) / 2;
            numericUpDown_StaminaBase.Maximum = int.Parse(textBox_StatPoints.Text) / 2;
            numericUpDown_AccuracyBase.Maximum = int.Parse(textBox_StatPoints.Text) / 2;
        }

        private void numericUpDown_UsedForFort_ValueChanged(object sender, EventArgs e) {
            // Turn current value into divisible by 5.
            int val = (int)numericUpDown_UsedForFort.Value;
            val = (val / 5) * 5;
            numericUpDown_UsedForFort.Value = val;
            Update_Used_for_Stats();
            Update_Fortune();
        }

        private void numericUpDown_StrengthBase_ValueChanged(object sender, EventArgs e) {
            Update_Strength_Final();
            Update_BaseStats_Check();
        }

        private void numericUpDown_SpeedBase_ValueChanged(object sender, EventArgs e) {
            Update_Speed_Final();
            Update_BaseStats_Check();
        }

        private void numericUpDown_StaminaBase_ValueChanged(object sender, EventArgs e) {
            Update_Stamina_Final();
            Update_BaseStats_Check();
        }

        private void numericUpDown_AccuracyBase_ValueChanged(object sender, EventArgs e) {
            Update_Accuracy_Final();
            Update_BaseStats_Check();
        }

        private void textBox_Fortune_TextChanged(object sender, EventArgs e) {
            Update_Total_RegTP();
            Update_SpTraitDGVAndList_Total();
        }

        private void textBox_UsedForStats_TextChanged(object sender, EventArgs e) {
            Update_BaseStats_Check();
        }

        // Kept track of AP
        private void numericUpDown_APTech_ValueChanged(object sender, EventArgs e) {
            Update_AP_Count();
            Update_TotalSD();
            Update_Total_RegTP(); // For Tech multiplier
        }

        private void numericUpDown_APTrait_ValueChanged(object sender, EventArgs e) {
            Update_AP_Count();
            Update_TotalSD();
            Update_Traits_Cap(); // For increasing Trait cap
        }

        private void numericUpDown_APPrime_ValueChanged(object sender, EventArgs e) {
            Update_AP_Count();
            Update_TotalSD();
        }

        private void numericUpDown_APMulti_ValueChanged(object sender, EventArgs e) {
            Update_AP_Count();
            Update_TotalSD();
        }

        private void numericUpDown_APNPC_ValueChanged(object sender, EventArgs e) {
            Update_AP_Count();
            Update_TotalSD();
        }

        private void checkBox_APHaki_CheckedChanged(object sender, EventArgs e) {
            Update_AP_Count();
            Update_TotalSD();
        }

        private void checkBox_APDF_CheckedChanged(object sender, EventArgs e) {
            Update_AP_Count();
            Update_TotalSD();
        }

        // Helper function to event handler button_Standardize
        private string Commas_To_Value(uint beli) {
            string val = beli.ToString();
            for (int i = val.Length - 3; i > 0; i -= 3) {
                // Inserting from right to left
                val = val.Insert(i, ",");
            }
            return val;
        }

        // Standardize beli button
        private void button_Standardize_Click(object sender, EventArgs e) {
            DialogResult result = new DialogResult();
            result = DialogResult.Yes;
            result = MessageBox.Show("Is your character scooping in the Grand Line?", "Beli Standardization", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result != DialogResult.Cancel) {
                int SD = (int)numericUpDown_SDEarned.Value;
                uint beli = 500000;
                string message = "You have " + SD + " SD earned. Calculations:\n+ 500,000 (Starting Beli)";
                if (SD <= 50) {
                    beli += (uint)(250000 * SD);
                    message += "\n+ " + Commas_To_Value((uint)(250000 * SD)) + " (250,000 / SD for first 50)";
                }
                else {
                    beli += 250000 * 50;
                    message += "\n+ " + Commas_To_Value((uint)(250000 * 50)) + " (250,000 / SD for first 50)";
                }
                SD -= 50;
                if (result == DialogResult.Yes) {
                    // In the GL, it's 500,000 per SD
                    if (SD > 0) {
                        beli += (uint)(500000 * SD);
                        message += "\n+ " + Commas_To_Value((uint)(500000 * SD)) + " (500,000 / SD in GL)";
                    }
                }
                else {
                    // In the Blues, it's 500,000 per SD
                    if (SD > 0) {
                        beli += (uint)(250000 * SD);
                        message += "\n+ " + Commas_To_Value((uint)(250000 * SD)) + " (250,000 / SD in Blues)";
                    }
                }
                // Apply beli trait / professional boosts (do not stack)
                // Look for Traits bonus of 20%
                if (traitList.Any(x => x.name == "Pickpocket") || traitList.Any(x => x.name == "Tough Bargainer")) {
                    uint addBeli = beli / 5;
                    beli += addBeli;
                    message += "\n+ " + Commas_To_Value(addBeli) + " (20% beli Trait Bonus)";
                }
                // Look for Thief primary bonus of 10%
                else if (Is_Prof_Primary("Thief")) {
                    uint addBeli = beli / 10;
                    beli += addBeli;
                    message += "\n+ " + Commas_To_Value(addBeli) + " (10% beli Thief Primary)";
                }
                // Show calculations
                string final_beli = Commas_To_Value(beli);
                message += "\n= " + final_beli + " (FINAL undeducted Value)";
                MessageBox.Show(message, "Beli Standardized");
                textBox_Beli.Text = final_beli;
            }
        }

        // Move a Profession up a row
        private void button_UpProf_Click(object sender, EventArgs e) {
            Move_DGV_Item(ref dgv_Professions, "Up");
        }

        // Move a Profession down a row
        private void button_DownProf_Click(object sender, EventArgs e) {
            Move_DGV_Item(ref dgv_Professions, "Down");
        }

        // Profession "Add" button from the MainForm
        private void button_AddProf_Click(object sender, EventArgs e) {
            Add_Profession ProfessionWin = new Add_Profession();
            if (ProfessionWin.NewDialog(ref dgv_Professions, ref profList)) { notSavedStatus(); }
        }

        // Profession "Edit" button from the MainForm
        // This is completely assuming that only one row can be selected (which we set MultiSelect = false)
        private void button_EditProf_Click(object sender, EventArgs e) {
            try {
                Add_Profession ProfessionWin = new Add_Profession();
                if (ProfessionWin.EditDialog(ref dgv_Professions, ref profList)) { notSavedStatus(); }
            }
            catch (Exception ex) {
                MessageBox.Show("Error in editing Profession.\nReason: " + ex.Message);
            }
        }

        // Profession "Delete" button from the MainForm
        private void button_DeleteProf_Click(object sender, EventArgs e) {
            try {
                if (dgv_Professions.SelectedCells.Count > 0) {
                    // Remove from Dict
                    string prof = dgv_Professions.SelectedRows[0].Cells[0].Value.ToString();
                    profList.Remove(prof);
                    // Remove from dgv
                    int remove_index = dgv_Professions.SelectedRows[0].Index;
                    dgv_Professions.Rows.RemoveAt(remove_index);
                    dgv_Professions.Refresh();
                    notSavedStatus();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error in deleting Profession.\nReason: " + ex.Message, "Exception Thrown");
            }
        }

        #endregion

        // --------------------------------------------------------------------------------------------
        // "TRAITS" Tab
        // --------------------------------------------------------------------------------------------

        #region Traits Tab

        #region Update SpTP Functions

        // HELPER FUNCTION
        private void Helper_SpTrait_UsedOverTotal() {
            // After update of those values, we will then check to see if Used > Total
            foreach (DataGridViewRow SpTraitRow in dgv_SpTraits.Rows) {
                if (int.Parse(SpTraitRow.Cells[1].Value.ToString()) > int.Parse(SpTraitRow.Cells[2].Value.ToString())) {
                    SpTraitRow.Cells[1].Style.BackColor = Color.FromArgb(255, 128, 128);
                }
                else {
                    SpTraitRow.Cells[1].Style.BackColor = SystemColors.Control;
                }
            }
        }

        private void Update_Used_SpTP_Textbox() {
            // Updates the Used SpTP Textbox
            int used = 0;
            foreach (SpTrait spTrait in spTraitList) {
                used += spTrait.usedTP;
            }
            textBox_SpTPUsed.Text = used.ToString();
        }

        private void Update_Total_SpTP_Textbox() {
            // Updates the Total SpTP Textbox
            int total_SP = 0;
            foreach (SpTrait spTrait in spTraitList) {
                total_SP += spTrait.totalTP;
            }
            textBox_SpTPTotal.Text = total_SP.ToString();
        }

        // Adds the Sp Trait into the list. 
        // param traitName is for all getName() purposes
        private void Add_SpTrait(string traitName) {
            // First check to see if the Sp. TP Trait is in the list
            // If so, add the trait into the ListView Special and add correspondingly.
            int fortune = int.Parse(textBox_Fortune.Text);
            Trait sel_Trait = traitList.Find(x => x.getName() == traitName);
            int divisor = Database.getSpTraitDiv(sel_Trait.name);
            string spTraitName = "";
            if (divisor > 0) {
                // Add Sp. Trait to the Dict
                int traitNum = sel_Trait.getTotal();
                string customName = sel_Trait.getName();
                int totTP = (fortune / divisor) * traitNum;
                SpTrait addSpTrait = new SpTrait(traitName, customName, 0, totTP);
                spTraitName = addSpTrait.getName();
                spTraitList.Add(addSpTrait);
                // Add Sp. Trait to the dgv
                dgv_SpTraits.Rows.Add(addSpTrait.getName(), 0, totTP);
            }
            // Lastly update the Total SpTP
            Update_SpTraitDGVAndList_Total();
            Update_SpTraitDGVAndList_Used(spTraitName);
            // Used when a Trait is added.
        }

        private void Remove_SpTrait(SpTrait removeTrait) {
            if (removeTrait == null) { return; }
            // Remove from Dict
            spTraitList.Remove(removeTrait);
            // Remove from ListView
            foreach (DataGridViewRow SpTraitRow in dgv_SpTraits.Rows) {
                if (SpTraitRow.Cells[0].Value.ToString() == removeTrait.getName()) {
                    dgv_SpTraits.Rows.Remove(SpTraitRow);
                    dgv_SpTraits.Refresh();
                    break;
                }
            }
            Update_SpTraitDGVAndList_Total();
            Update_SpTraitDGVAndList_Used(removeTrait.getName());

            // Used when a Trait is removed.
        }

        private void Update_SpTraitDGVAndList_Total() {
            // This updates all the Total Sp Traits
            int fortune = int.Parse(textBox_Fortune.Text);
            foreach (DataGridViewRow SpTraitRow in dgv_SpTraits.Rows) {
                string spName = SpTraitRow.Cells[0].Value.ToString(); // This is getting getTraitName()
                Trait traitName = traitList.Find(x => x.getName() == spName);
                int traitNum = traitName.getTotal();
                int divisor = Database.getSpTraitDiv(traitName.name); //#TODO: CAN'T USE SPNAME!!!
                int totTP = (fortune / divisor) * traitNum;
                // Edit spTraitList
                spTraitList.Find(x => x.getName() == spName).totalTP = totTP;
                // Edit ListView
                SpTraitRow.Cells[2].Value = totTP;
            }
            // Update the Total textboxes
            Update_Total_SpTP_Textbox();
            // After update of those values, we will then check to see if Used > Total
            Helper_SpTrait_UsedOverTotal();
            // Used when Fortune is updated.
            // Used when Trait is added/edited/removed.
        }

        // Update values inside the table based on Techniques or Fortune edited.
        private void Update_SpTraitDGVAndList_Used(string traitName) {
            // This is using traitName as from Trait.getName()
            // Update SpTraitList first, then the ListView
            if (string.IsNullOrWhiteSpace(traitName)) { return; }
            int used = 0;
            foreach (Technique tech in techList.Values) {
                if (tech.specialTrait == traitName) {
                    used += tech.spTP;
                }
            }
            // Edit spTraitList, if it still exists
            try {
                spTraitList.Find(x => x.getName() == traitName).usedTP = used;
                // Edit ListView
                foreach (DataGridViewRow SpTraitRow in dgv_SpTraits.Rows) {
                    if (SpTraitRow.Cells[0].Value.ToString() == traitName) {
                        SpTraitRow.Cells[1].Value = used.ToString();
                        break;
                    }
                }
            }
            catch { }
            // Update the Used textboxes
            Update_Used_SpTP_Textbox();
            // After update of those values, we will then check to see if Used > Total
            Helper_SpTrait_UsedOverTotal();
            // Used when a Technique is added/edited/branched/removed.
            // Used when Trait is added/edited/removed.
        }

        #endregion

        // Put name as None if we're deleting
        private void All_Update_Functions_Traits() {
			Update_Traits_Count_Label();
            Update_Fortune();
			Update_Strength_Final();
			Update_Stamina_Final();
			Update_Speed_Final();
			Update_Accuracy_Final();
			Update_Total_RegTP();
			Update_CritAnatQuick_Msg();
            notSavedStatus();
		}

        // Traits "Add" button from the MainForm
        private void button11_TraitAdd_Click(object sender, EventArgs e) {
			Add_Trait TraitWin = new Add_Trait();
			string name = TraitWin.NewDialog(ref dgv_Traits, ref traitList);
			// And lastly all Update functions
			if (name != null) {
                All_Update_Functions_Traits();
                Add_SpTrait(name);
            }
		}

        // Traits "Edit" button from the MainForm
        private void button_EditTrait_Click(object sender, EventArgs e) {
            if (dgv_Traits.SelectedRows.Count == 0) { return; }
            Add_Trait TraitWin = new Add_Trait();
            string oldName = dgv_Traits.SelectedRows[0].Cells[0].Value.ToString();
            string oldCustName = dgv_Traits.SelectedRows[0].Cells[1].Value.ToString();
            string newName = TraitWin.EditDialog(ref dgv_Traits, ref traitList);
            if (newName != null) {
                All_Update_Functions_Traits();
                SpTrait oldSpTrait = spTraitList.Find(x => x.name == oldName && x.custom == oldCustName);
                Remove_SpTrait(oldSpTrait);
                Add_SpTrait(newName);
            }
        }

        // Traits "Delete" button from the MainForm
        private void button10_TraitsDelete_Click(object sender, EventArgs e) {
			// This is completely assuming that only one row can be selected (which we set MultiSelect = false)
			if (dgv_Traits.SelectedRows.Count == 0) { return; }
            // Remove from List
            string traitName = dgv_Traits.SelectedRows[0].Cells[0].Value.ToString();
            string custName = dgv_Traits.SelectedRows[0].Cells[1].Value.ToString();
            try {
                Trait removeTrait = traitList.Find(x => x.name == traitName && x.custom == custName);
                traitList.Remove(removeTrait);
                // Remove from dgv
                int remove_index = dgv_Traits.SelectedRows[0].Index;
                dgv_Traits.Rows.RemoveAt(remove_index);
                dgv_Traits.Refresh();
                // All update Functions
                All_Update_Functions_Traits();
                SpTrait removeSpTrait = spTraitList.Find(x => x.name == traitName && x.custom == custName);
                Remove_SpTrait(removeSpTrait);
            }
            catch (Exception ex) {
                MessageBox.Show("Error in deleting Trait.\nReason: " + ex.Message, "Exception Thrown");
            }
		}

        // Move Trait row up
        private void button_TraitsUp_Click(object sender, EventArgs e) {
            Move_DGV_Item(ref dgv_Traits, "Up");
        }

        // Move Trait row down
        private void button_TraitsDown_Click(object sender, EventArgs e) {
            Move_DGV_Item(ref dgv_Traits, "Down");
        }

        // Sort by Trait Type
        private void dgv_Traits_SortCompare(object sender, DataGridViewSortCompareEventArgs e) {
            if (e.Column.Index == 2) {
                e.Handled = true;
                e.SortResult = compareTraitTypes(e.CellValue1, e.CellValue2);
            }
        }

        // Comparator for the above function
        private int compareTraitTypes(object o1, object o2) {
            int o1Worth = (o1.ToString() == "General") ? 1 :
                (o1.ToString() == "General / Professional") ? 0 : -1;
            int o2Worth = (o2.ToString() == "General") ? 1 :
                (o2.ToString() == "General / Professional") ? 0 : -1;
            return o1Worth.CompareTo(o2Worth);
        }

        #endregion

        // --------------------------------------------------------------------------------------------
        // "TECHNIQUES" Tab
        // --------------------------------------------------------------------------------------------

        #region Techniques Tab

        private void All_Update_Functions_Techs(string traitName) {
            Update_SpTraitDGVAndList_Used(traitName);
			Update_Used_RegTP();
			Update_CritAnatQuick_Msg();
			Update_TechNum();
            notSavedStatus();
        }

		private void listView3_SpTP_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) {
			// Prevents users from changing column width
			e.Cancel = true;
			e.NewWidth = dgv_SpTraits.Columns[e.ColumnIndex].Width;
		}

		private void button14_TechAdd_Click(object sender, EventArgs e) {
            // Technique "Add" button from the MainForm
            int fortune = int.Parse(textBox_Fortune.Text);
			int num_items = dgv_Techniques.Rows.Count;
			if (num_items == 0) { num_items++; } // What if empty?
			Add_Technique TechniqueWin = new Add_Technique(fortune, profList, 
                traitList, spTraitList, makeDFClass(), false, false, null);
			string newName = TechniqueWin.NewDialog(ref dgv_Techniques, ref techList, num_items - 1);
            // Update functions go below
            if (!string.IsNullOrWhiteSpace(newName)) {
                string spTrait = techList[newName].specialTrait;
                All_Update_Functions_Techs(spTrait);
            }
		}

		private void button_TechBranch_Click(object sender, EventArgs e) {
			// Technique "Branch" button from the MainForm
			if (dgv_Techniques.SelectedRows.Count == 0) { return; }
            string TechName = dgv_Techniques.SelectedRows[0].Cells[0].Value.ToString();
			int index = dgv_Techniques.SelectedRows[0].Index;
			if (!string.IsNullOrWhiteSpace(TechName)) {
                int fortune = int.Parse(textBox_Fortune.Text);
                int max_rank = fortune / 2;
				Technique selTech = techList[TechName];
				if (selTech.rokuName != Database.ROKU_NON && 
                    !traitList.Any(x => x.name == Database.TR_ROKUMA)) {
					// ^If the Selected Technique is Rokushiki and character does not have Rokushiki Master
					MessageBox.Show("You can't edit a Rokushiki Technique without the Rokushiki Master Trait.", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else if (selTech.rank + 4 > max_rank) {
					// A branched technique must have at least 4 ranks from the Technique they are branching from
					MessageBox.Show("A branched Technique must be 4 ranks higher than the Technique they are branching from\n" +
						"Branching this Technique would cause you to go over your Max Rank [" + max_rank + "]", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else {
					Add_Technique TechniqueWin = new Add_Technique(fortune, profList, traitList, 
                        spTraitList, makeDFClass(), true, false, selTech);
                    string newName = TechniqueWin.NewDialog(ref dgv_Techniques, ref techList, index);
                    // Update functions go below
                    if (!string.IsNullOrWhiteSpace(newName)) {
                        string spTrait = techList[newName].specialTrait;
                        All_Update_Functions_Techs(spTrait);
                    }
				}
			}
		}

		private void button_TechEdit_Click(object sender, EventArgs e) {
			// Technique "Edit" button from the MainForm
			if (dgv_Techniques.SelectedRows.Count == 0) { return; }
			string TechName = dgv_Techniques.SelectedRows[0].Cells[0].Value.ToString();
            if (!string.IsNullOrWhiteSpace(TechName)) {
                int fortune = int.Parse(textBox_Fortune.Text);
                Technique selTech = techList[TechName];
                if (selTech.rokuName != Database.ROKU_NON &&
                    !traitList.Any(x => x.name == Database.TR_ROKUMA)) {
					// ^If the Selected Technique is Rokushiki and character does not have Rokushiki Master
					MessageBox.Show("You can't edit a Rokushiki Technique without the Rokushiki Master Trait!", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else {
                    Add_Technique TechniqueWin = new Add_Technique(fortune, profList, traitList,
                        spTraitList, makeDFClass(), false, true, selTech);
                    string newName = TechniqueWin.EditDialog(ref dgv_Techniques, ref techList);
                    // Update functions go below
                    if (!string.IsNullOrWhiteSpace(newName)) {
                        string spTrait = techList[newName].specialTrait;
                        All_Update_Functions_Techs(spTrait);
                    }
                }
			}
		}

		private void button13_TechDelete_Click(object sender, EventArgs e) {
			// Technique "Delete" button from the MainForm
			if (dgv_Techniques.SelectedRows.Count == 0) { return; }
			DialogResult result = MessageBox.Show("Are you sure you want to delete \"" +
                dgv_Techniques.SelectedRows[0].Cells[0].Value.ToString() + "\"?", "Remove Tech",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes) {
                string techName = dgv_Techniques.SelectedRows[0].Cells[0].Value.ToString();
                try {
                    // Remove from Dict
                    string spTrait = "";
                    if (!string.IsNullOrWhiteSpace(techName)) {
                        spTrait = techList[techName].specialTrait;
                        techList.Remove(techName);
                    }
                    // Remove from dgv
                    int remove_index = dgv_Techniques.SelectedRows[0].Index;
                    dgv_Techniques.Rows.RemoveAt(remove_index);
                    dgv_Techniques.Refresh();
                    // Update functions go below
                    All_Update_Functions_Techs(spTrait);
                }
                catch (Exception ex) {
                    MessageBox.Show("Error in deleting Technique.\nReason: " + ex.Message, "Exception Thrown");
                }
			}
		}

		private void button_UpTech_Click(object sender, EventArgs e) {
			Move_DGV_Item(ref dgv_Techniques, "Up");
		}

		private void button_DownTech_Click(object sender, EventArgs e) {
            Move_DGV_Item(ref dgv_Techniques, "Down");
		}

		private void textBox_RegTPUsed_TextChanged(object sender, EventArgs e) {
			if (int.Parse(textBox_RegTPUsed.Text) > int.Parse(textBox_RegTPTotal.Text)) {
				textBox_RegTPUsed.BackColor = Color.FromArgb(255, 128, 128);
			}
			else {
				textBox_RegTPUsed.BackColor = SystemColors.Control;
			}
		}

		private void textBox_SpTPUsed_TextChanged(object sender, EventArgs e) {
			if (int.Parse(textBox_SpTPUsed.Text) > int.Parse(textBox_SpTPTotal.Text)) {
				textBox_SpTPUsed.BackColor = Color.FromArgb(255, 128, 128);
			}
			else {
				textBox_SpTPUsed.BackColor = SystemColors.Control;
			}
		}

		private void textBox_RegTPTotal_TextChanged(object sender, EventArgs e) {
			Update_CritAnatQuick_Msg();
		}

        // To indicate which Row is selected for Categories
        private void dgv_Techniques_SelectionChanged(object sender, EventArgs e) {
            try {
                int row = dgv_Techniques.SelectedRows[0].Index;
                label_RowNum.Text = "Row " + row + " Selected";
            }
            catch {
                label_RowNum.Text = "Select a Technique to see which Row it is in.";
            }
        }

		private void button_SubCatAdd_Click(object sender, EventArgs e) {
			if (numericUpDown_RowEnd.Value < numericUpDown_RowBegin.Value) {
				MessageBox.Show("Row Begin must be a lower value (or equal to) than Row End.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else if (string.IsNullOrWhiteSpace(textBox_SubCat.Text)) {
				MessageBox.Show("Category needs a name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else if (dgv_Techniques.Rows.Count < 1) {
				MessageBox.Show("You have no Techniques.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
            var rowBeg = numericUpDown_RowBegin.Value;
            var rowEnd = numericUpDown_RowEnd.Value;
            ListViewItem item = new ListViewItem();
			item.SubItems[0].Text = rowBeg.ToString();
			item.SubItems.Add(rowEnd.ToString());
			item.SubItems.Add(textBox_SubCat.Text);
			// Reset options
			numericUpDown_RowBegin.Value = 0;
			numericUpDown_RowEnd.Value = 0;
			textBox_SubCat.Clear();
			// Add to ListView
			item.Selected = true;
			listView_SubCat.Items.Add(item);
			listView_SubCat.ListViewItemSorter = new ListViewItemNumberSort(0);
			listView_SubCat.Sort();
            // Locate where it is, and then adjust numbers above & below row.
            int newIndex = listView_SubCat.Items.IndexOf(item);
            if (newIndex > 0) {
                listView_SubCat.Items[newIndex - 1].SubItems[1].Text = (rowBeg - 1).ToString();
            }
            if (newIndex < listView_SubCat.Items.Count - 1) {
                listView_SubCat.Items[newIndex + 1].SubItems[0].Text = (rowEnd + 1).ToString();
            }
			Update_SubCatWarning();
            notSavedStatus();
		}

		private void button_SubCatEdit_Click(object sender, EventArgs e) {
			if (listView_SubCat.SelectedItems.Count == 1) {
				try {
					int begin = int.Parse(listView_SubCat.SelectedItems[0].SubItems[0].Text);
					int end = int.Parse(listView_SubCat.SelectedItems[0].SubItems[1].Text);
					if (begin > numericUpDown_RowBegin.Maximum) { numericUpDown_RowBegin.Value = numericUpDown_RowBegin.Maximum; }
					else { numericUpDown_RowBegin.Value = begin; }
					if (end > numericUpDown_RowEnd.Maximum) { numericUpDown_RowEnd.Value = numericUpDown_RowEnd.Maximum; }
					else { numericUpDown_RowEnd.Value = end; }
					textBox_SubCat.Text = listView_SubCat.SelectedItems[0].SubItems[2].Text;
					Delete_ListViewItem(ref listView_SubCat);
					Update_SubCatWarning();
                    notSavedStatus();
				}
				catch {
					MessageBox.Show("Error in editing Category.", "Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void button_SubCatClear_Click(object sender, EventArgs e) {
			if (MessageBox.Show("Are you sure you want to clear the Categories?", "Clear Categories",
				MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
				listView_SubCat.Items.Clear();
				Update_SubCatWarning();
                notSavedStatus();
			}
		}

		private void listView_SubCat_SelectedIndexChanged(object sender, EventArgs e) {
			if (listView_SubCat.SelectedItems.Count == 1) {
				try {
					int begin_ind = int.Parse(listView_SubCat.SelectedItems[0].SubItems[0].Text);
					int end_ind = int.Parse(listView_SubCat.SelectedItems[0].SubItems[1].Text);
					string begin_str = dgv_Techniques.Rows[begin_ind].Cells[0].Value.ToString();
					string end_str = dgv_Techniques.Rows[end_ind].Cells[0].Value.ToString();
					string name = listView_SubCat.SelectedItems[0].SubItems[2].Text;
					label_SubCatMsg.Text = "Selected Category: (" + name + ")\n" +
						"FROM [" + begin_str + "] TO [" + end_str + ']';
                }
				catch {
					label_SubCatMsg.Text = "No Valid Category Selected";
				}
			}
		}

		private void listView_SubCat_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) {
			// Prevents users from changing column width
			e.Cancel = true;
			e.NewWidth = listView_SubCat.Columns[e.ColumnIndex].Width;
		}

        #endregion
        
        // --------------------------------------------------------------------------------------------
        // "SOURCES" Tab
        // --------------------------------------------------------------------------------------------

        #region Sources Tab
        
        // NOTE: IN V1.7.0 A LIST WILL BE BINDED TO A BINDINGSOURCE

        // Helper function for changing the SD numericupdown
        private void update_SD_Sources() {
            if (checkBox_CalcSD.Checked) {
                int totalSD = 0;
                foreach (Source source in sourceList.Values) {
                    totalSD += source.SD;
                }
                int AP_num = int.Parse(textBox_AP.Text);
                numericUpDown_SDEarned.Value = totalSD - (AP_num * 50);
            }
        }

        // Helper function for changing Beli textbox
        private void update_Beli_Sources() {
            if (checkBox_CalcBeli.Checked) {
                int totalBeli = 0;
                foreach (Source source in sourceList.Values) {
                    totalBeli += source.beli;
                }
                textBox_Beli.Text = totalBeli.ToString("N0");
            }
        }

        // Combining update_source functions
        private void update_All_Sources() {
            update_SD_Sources();
            update_Beli_Sources();
            notSavedStatus();
        }

        // Remembering the state of the dateTimeStamp check
        bool timeStampBool = false;

        // Adds a Source
        private void button_AddSource_Click(object sender, EventArgs e) {
            // Add Windows Dialog and check if confirmed
            Add_Source source_Win = new Add_Source();
            source_Win.new_Dialog(ref dgv_Sources, ref sourceList, 
                radioButton_DateNA.Checked, ref timeStampBool);
            // Update current SD if checked
            update_All_Sources();
        }

        // Edits a row
        private void dataGridView_Sources_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (dgv_Sources.SelectedRows.Count > 0 && e.ColumnIndex != 0) {
                Add_Source source_Win = new Add_Source();
                source_Win.edit_Dialog(ref dgv_Sources, ref sourceList, 
                    radioButton_DateNA.Checked, ref timeStampBool);
            }
            // Update current SD if checked
            update_All_Sources();
        }

        // Deletes a row
        private void dataGridView_Sources_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            try {
                DataGridView dgv = (DataGridView)sender;
                DataGridViewRow item = dgv.Rows[e.RowIndex];
                if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0) {
                    // Remove from Dict
                    string del_title = item.Cells[2].Value.ToString();
                    sourceList.Remove(del_title);
                    // Button Clicked for that row.
                    dgv.Rows.RemoveAt(item.Index);
                    dgv.Refresh();
                    // Update current SD if checked
                    update_All_Sources();
                }
            }
            catch { } // Any implicit clicking exception errors
        }

        private const string DEVH_KEY = "OPRPdevHMay2017";
        // Loads the devh file, overriding the dgv and sourceList
        private void button_LoadDevH_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure you want to overwrite the current existing Sources?", "Load DevH",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                return;
            }
            OpenFileDialog dlgFileOpen = new OpenFileDialog();
            dlgFileOpen.Filter = "DEVH files (*.devh)|*.devh";
            dlgFileOpen.Title = "Open Development History";
            dlgFileOpen.RestoreDirectory = true;
            if (dlgFileOpen.ShowDialog() == DialogResult.OK) {
                string devH_data;
                try {
                    string path = dlgFileOpen.FileName;
                    string encrypted = File.ReadAllText(path);
                    devH_data = Serialize.decryptData(encrypted, DEVH_KEY);
                    profile.loadCharSources(ref sourceList, ref dgv_Sources,
                        ref radioButton_DateNA, true, devH_data);
                    notSavedStatus();
                }
                catch (Exception ex) {
                    MessageBox.Show("Failed to load.\nReason: " + ex.Message,
                        "Failed to Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Saves the current sourceList into a devh file
        private void button_SaveDevH_Click(object sender, EventArgs e) {
            SaveFileDialog fileDialogSaveProject = new SaveFileDialog();
            fileDialogSaveProject.Filter = "DEVH files (*.devh)|*.devh";
            fileDialogSaveProject.Title = "Save New Development History";
            fileDialogSaveProject.OverwritePrompt = true;
            if (fileDialogSaveProject.ShowDialog() == DialogResult.OK) {
                try {
                    string data = profile.saveCharSources(dgv_Sources, sourceList, radioButton_DateNA, true);
                    string path = fileDialogSaveProject.FileName;
                    string saveData = Serialize.encryptData(data, DEVH_KEY);
                    using (StreamWriter newTask = new StreamWriter(path, false)) {
                        newTask.Write(saveData);
                    }
                    MessageBox.Show("Development History saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) {
                    MessageBox.Show("Failed to save.\nReason: " + ex.Message,
                        "Failed to Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Change SD numericupdown value when True
        private void checkBox_CalcSD_CheckedChanged(object sender, EventArgs e) {
            update_SD_Sources();
        }

        // Change Beli numericupdown value when True
        private void checkBox_CalcBeli_CheckedChanged(object sender, EventArgs e) {
            update_Beli_Sources();
        }

        // Changes date format to NA (MM/DD/YYYY) when True in dgv
        private void radioButton_DateNA_CheckedChanged(object sender, EventArgs e) {
            if (radioButton_DateNA.Checked) {
                foreach (DataGridViewRow row in dgv_Sources.Rows) {
                    string[] date = row.Cells[1].Value.ToString().Split('/');
                    // Standard swap
                    string temp = date[0];
                    date[0] = date[1];
                    date[1] = temp;
                    row.Cells[1].Value = string.Join("/", date);
                }
                notSavedStatus();
            }
        }

        // Changes date format to EU (DD/MM/YYYY) when True in dgv
        private void radioButton_DateEU_CheckedChanged(object sender, EventArgs e) {
            if (radioButton_DateEU.Checked) {
                foreach (DataGridViewRow row in dgv_Sources.Rows) {
                    string[] date = row.Cells[1].Value.ToString().Split('/');
                    // Standard swap
                    string temp = date[0];
                    date[0] = date[1];
                    date[1] = temp;
                    row.Cells[1].Value = string.Join("/", date);
                }
                notSavedStatus();
            }
        }

        // Moves Row up
        private void button_UpSource_Click(object sender, EventArgs e) {
            Move_DGV_Item(ref dgv_Sources, "Up");
        }

        // Moves Row down
        private void button_DownSource_Click(object sender, EventArgs e) {
            Move_DGV_Item(ref dgv_Sources, "Down");
        }

        // Sorts all the rows by date
        private void dataGridView_Sources_SortCompare(object sender, DataGridViewSortCompareEventArgs e) {
            if (e.Column.Index == 1) {
                e.Handled = true;
                e.SortResult = compareDates(e.CellValue1, e.CellValue2);
            }
        }

        // Comparator function for the sorting
        private int compareDates(object o1, object o2) {
            // Need to sort based on either NA or EU date
            if (string.IsNullOrWhiteSpace(o1.ToString())) { return -1; }
            if (string.IsNullOrWhiteSpace(o2.ToString())) { return 1; }
            int[] dateFormat = (radioButton_DateNA.Checked) ? new int[3] { 2, 0, 1 } : new int[3] { 2, 1, 0 };
            string[] date1 = o1.ToString().Split('/');
            string[] date2 = o2.ToString().Split('/');
            // 3 Dates to reresent Month, Day, Year
            int result = 0;
            for (int i = 0; i < 3; ++i) {
                int ind = dateFormat[i];
                result = int.Parse(date1[ind]).CompareTo(int.Parse(date2[ind]));
                // 0 means the number is the same
                if (result == 0) { continue; }
                else { break; }
            }
            return result;
        }

        #endregion

        // --------------------------------------------------------------------------------------------
        // "TEMPLATE" Tab
        // --------------------------------------------------------------------------------------------

        #region Template Tab

        private void button_LoadTemp_Click(object sender, EventArgs e) {
            Import_Template();
        }

        private void button_ResetTemp_Click(object sender, EventArgs e) {
            if (template_imported) { notSavedStatus(); }
			template_imported = false;
			label_TemplateType.Text = STD_TEMPLATE_MSG;
			label_TemplateType.ForeColor = Color.Green;
			richTextBox_Template.Text = Sheet.BASIC_TEMPLATE;
		}

		private void button_MoreTemplate_Click(object sender, EventArgs e) {
			try {
				if (MessageBox.Show("Would you like to open a browser to the Zetaboard page?", "Custom Templates",
					MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
					Process.Start("http://s1.zetaboards.com/One_Piece_RP/topic/6153426/1/");
                }
            }
			catch (Exception ex) {
				MessageBox.Show("Error in opening up Zetaboards site.\nReason: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


        private void richTextBox_Template_TextChanged(object sender, EventArgs e) {
            notSavedStatus();
        }

        #endregion

        // --------------------------------------------------------------------------------------------
        // Main Form Miscellaneous
        // --------------------------------------------------------------------------------------------

        #region Save, Open, Import, Load, Form, Generate Miscellaneous

        // This completely resets the form back to its Loaded state.
        private void resetForm() {
			// Basic Character
			textBox_CharacterName.Clear();
			textBox_Nickname.Clear();
			numericUpDown_Age.Value = 7;
			comboBox_Gender.SelectedIndex = -1;
			textBox_Race.Clear();
			textBox_Position.Clear();
			comboBox_Affiliation.SelectedIndex = -1;
			textBox_Bounty.Clear();
            numericUpDown_Comm.Value = 0;
			comboBox_MarineRank.SelectedIndex = -1;
			textBox_Threat.Clear();
			listBox_Achieve.Items.Clear();
			dgv_Professions.Rows.Clear();
            dgv_Professions.Refresh();
			profList.Clear();
			textBox_Height.Clear();
			textBox_Weight.Clear();
			richTextBox_Hair.Clear();
			richTextBox_Eye.Clear();
			richTextBox_Clothing.Clear();
			richTextBox_GeneralAppear.Clear();
			listView_Images.Items.Clear();
			textBox_ImageURL.Clear();
			textBox_ImageLabel.Clear();
			checkBox_FullRes.Checked = true;
            numericUpDown_Width.Value = 640;
            numericUpDown_Height.Value = 480;
			// Background
			textBox_Island.Clear();
			comboBox_Region.SelectedIndex = -1;
			richTextBox_Personality.Clear();
			richTextBox_History.Clear();
			// Abilities
			richTextBox_Combat.Clear();
			listView_Weaponry.Items.Clear();
			listView_Items.Items.Clear();
			textBox_Beli.Clear();
			textBox_DFName.Clear();
			comboBox_DFType.SelectedIndex = -1;
            comboBox_DFTier.SelectedIndex = -1;
			richTextBox_DFDesc.Clear();
			// Stats
			numericUpDown_SDEarned.Value = 0;
			numericUpDown_SDintoStats.Value = 0;
            numericUpDown_APTech.Value = 0;
            numericUpDown_APTrait.Value = 0;
            numericUpDown_APPrime.Value = 0;
            numericUpDown_APMulti.Value = 0;
            numericUpDown_APNPC.Value = 0;
            checkBox_APHaki.Checked = false;
            checkBox_APDF.Checked = false;
			textBox_AP.Text = "0";
            textBox_Focus.Text = "0";
			numericUpDown_UsedForFort.Value = 0;
			numericUpDown_StrengthBase.Value = 1;
			numericUpDown_SpeedBase.Value = 1;
			numericUpDown_StaminaBase.Value = 1;
			numericUpDown_AccuracyBase.Value = 1;
			// Traits
			dgv_Traits.Rows.Clear();
            dgv_Traits.Refresh();
			traitList.Clear();
            // Techniques
			dgv_SpTraits.Rows.Clear();
            dgv_SpTraits.Refresh();
            spTraitList.Clear();
            label_CritAnatQuick.Text = "";
			dgv_Techniques.Rows.Clear();
            dgv_Techniques.Refresh();
			Update_TechNum();
			techList.Clear();
			listView_SubCat.Items.Clear();
			textBox_SubCat.Clear();
            // Sources
            dgv_Sources.Rows.Clear();
            dgv_Sources.Refresh();
            sourceList.Clear();
            // Templates
            label_TemplateType.Text = "Standard Template";
            label_TemplateType.ForeColor = Color.Green;
            richTextBox_Template.Text = Sheet.BASIC_TEMPLATE;
            textBox_Color.Clear();
            textBox_MasteryMsg.Text = "* denotes +4 Rank Mastery";
            // Update Functions (that are still needed)
            Update_AP_Count();
			Update_Strength_Final();
			Update_Speed_Final();
			Update_Stamina_Final();
			Update_Accuracy_Final();
            Update_Traits_Cap();
            Update_Traits_Count_Label();
			Update_Total_RegTP();
			Update_Used_RegTP();
            textBox_SpTPTotal.Text = "0";
            textBox_SpTPUsed.Text = "0";
            // Reset save status
            isSavedStatus();
		}

		// Saves the form into a serialized object so that only the tool recognizes the Saved Form
		private void saveFormToCharacter() {
            profile.saveCharResetData();
            profile.saveCharBasic(
				textBox_CharacterName.Text,
				textBox_Nickname.Text,
				(int)numericUpDown_Age.Value,
				comboBox_Gender.Text,
				textBox_Race.Text,
				textBox_Position.Text,
				comboBox_Affiliation.Text,
				textBox_Bounty.Text,
				(int)numericUpDown_Comm.Value,
				comboBox_MarineRank.Text,
				textBox_Threat.Text,
				listBox_Achieve,
                dgv_Professions,
				profList
				);
			profile.saveCharAppearance(
				textBox_Height.Text,
				textBox_Weight.Text,
				richTextBox_Hair.Text,
				richTextBox_Eye.Text,
				richTextBox_Clothing.Text,
				richTextBox_GeneralAppear.Text,
				listView_Images
				);
			profile.saveCharBackground(
				textBox_Island.Text,
				comboBox_Region.Text,
				richTextBox_Personality.Text,
				richTextBox_History.Text
				);
			profile.saveCharCombat(
				richTextBox_Combat.Text,
				listView_Weaponry,
				listView_Items,
				textBox_Beli.Text,
                makeDFClass()
				);
			profile.saveCharStats(
				(int)numericUpDown_SDEarned.Value,
				(int)numericUpDown_SDintoStats.Value,
				(int)numericUpDown_UsedForFort.Value,
				(int)numericUpDown_StrengthBase.Value,
				(int)numericUpDown_SpeedBase.Value,
				(int)numericUpDown_StaminaBase.Value,
				(int)numericUpDown_AccuracyBase.Value,
                (int)numericUpDown_APTech.Value,
                (int)numericUpDown_APTrait.Value,
                (int)numericUpDown_APPrime.Value,
                (int)numericUpDown_APMulti.Value,
                (int)numericUpDown_APNPC.Value,
                checkBox_APHaki.Checked,
                checkBox_APDF.Checked
				);
			profile.saveCharTraits(dgv_Traits, traitList);
			profile.saveCharTechs(techList, dgv_Techniques, listView_SubCat);
            profile.saveCharSources(dgv_Sources, sourceList, radioButton_DateNA, false);
            profile.saveCharTemplate(
                label_TemplateType.Text,
                richTextBox_Template.Text,
                textBox_Color.Text,
                textBox_MasteryMsg.Text
                );
		}

		// Loads from serialized object and puts it into form
		private void loadCharacterToForm() {
			try {
				profile.loadCharBasic(
			  ref textBox_CharacterName,
			  ref textBox_Nickname,
			  ref numericUpDown_Age,
			  ref comboBox_Gender,
			  ref textBox_Race,
			  ref textBox_Position,
			  ref comboBox_Affiliation,
			  ref textBox_Bounty,
			  ref numericUpDown_Comm,
			  ref comboBox_MarineRank,
			  ref textBox_Threat,
			  ref listBox_Achieve
			  );
			}
			catch (Exception ex) { MessageBox.Show("Load Basic Character Error.\nReason: " + ex.Message); }
			try {
				profile.loadCharAppearance(
			  ref textBox_Height,
			  ref textBox_Weight,
			  ref richTextBox_Hair,
			  ref richTextBox_Eye,
			  ref richTextBox_Clothing,
			  ref richTextBox_GeneralAppear,
			  ref listView_Images
			  );
			}
			catch (Exception ex) { MessageBox.Show("Load Physical Appearance Error.\nReason: " + ex.Message); }
			try {
				profile.loadCharBackground(
			  ref textBox_Island,
			  ref comboBox_Region,
			  ref richTextBox_Personality,
			  ref richTextBox_History
			  );
			}
			catch (Exception ex) { MessageBox.Show("Load Character Background Error.\nReason: " + ex.Message); }
			try {
				profile.loadCharCombat(
			  ref richTextBox_Combat,
			  ref listView_Weaponry,
			  ref listView_Items,
			  ref textBox_DFName,
			  ref comboBox_DFType,
              ref comboBox_DFTier,
			  ref richTextBox_DFDesc
			  );
			}

			catch (Exception ex) { MessageBox.Show("Load Combat and Abilities Error.\nReason: " + ex.Message); }
            try {
                profile.loadCharRPElements(
                ref numericUpDown_SDEarned,
                ref numericUpDown_SDintoStats,
                ref numericUpDown_UsedForFort,
                ref numericUpDown_StrengthBase,
                ref numericUpDown_SpeedBase,
                ref numericUpDown_StaminaBase,
                ref numericUpDown_AccuracyBase,
                ref numericUpDown_APTech,
                ref numericUpDown_APTrait,
                ref numericUpDown_APPrime,
                ref numericUpDown_APMulti,
                ref numericUpDown_APNPC,
                ref checkBox_APHaki,
                ref checkBox_APDF,
                ref textBox_Beli,
                ref dgv_Professions,
                ref profList
                );
            }
            catch (Exception ex) { MessageBox.Show("Load Stats Error.\nReason: " + ex.Message); }
            try {
                profile.loadCharTraits(
                    ref traitList,
                    ref dgv_Traits,
                    ref spTraitList,
                    int.Parse(textBox_Fortune.Text)
                    );
            }
			catch (Exception ex) { MessageBox.Show("Load Traits Error.\nReason: " + ex.Message); }
            try {
                profile.loadCharTechs(
                    ref techList,
                    ref dgv_Techniques,
                    ref spTraitList,
                    ref dgv_SpTraits,
                    ref listView_SubCat
                    );
            }
            catch (Exception ex) { MessageBox.Show("Load Techniques Error.\nReason: " + ex.Message); }
            try {
                profile.loadCharSources(
                    ref sourceList,
                    ref dgv_Sources,
                    ref radioButton_DateNA,
                    false
                    );
            }
            catch (Exception ex) { MessageBox.Show("Load Techniques Error.\nReason: " + ex.Message); }
            profile.loadCharTemplate(
                ref label_TemplateType,
                ref richTextBox_Template,
                ref textBox_Color,
                ref textBox_MasteryMsg
                );
			// Some update functions in which it isn't in an Exception Handle
			Update_Traits_Count_Label();
			Update_AP_Count();
			Update_TotalSD();
            Update_Strength_Final();
            Update_Speed_Final();
            Update_Stamina_Final();
            Update_Accuracy_Final();
            Update_Fortune();
            Update_Total_RegTP();
			Update_Used_RegTP();
			Update_TechNum();
            Update_Total_SpTP_Textbox();
            Update_Used_SpTP_Textbox();
            Update_CritAnatQuick_Msg();
        }

		// This is where serialize our profile.
		private void saveCharactertoData() {
			try {
                profile.saveFile();
			}
			catch (SerializationException e) {
				MessageBox.Show("Failed to save.\nReason: " + e.Message);
			}
			finally {
				MessageBox.Show(profile.filename + " successfully saved!", "Saved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void newCharacter() {
			DialogResult result = new DialogResult();
			result = DialogResult.No;
            if (!saved) {
                result = MessageBox.Show("Save your current work before making a New Character?", "Make New Project",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) {
                    saveCharacter();
                }
            }
            // Make a new Character
			if (result != DialogResult.Cancel) {
				this.Text = "OPRP Character Builder";
				profile.filename = null;
				profile.path = null;
				resetForm();
			}
		}

		private void saveCharacter() {
			// If there is no filename (this means New), we save one.
			if (profile.filename == null) {
				SaveFileDialog fileDialogSaveProject = new SaveFileDialog();
				fileDialogSaveProject.Filter = "OPRP files (*.oprp)|*.oprp";
				fileDialogSaveProject.Title = "Save New Project";
				fileDialogSaveProject.OverwritePrompt = true;
				if (fileDialogSaveProject.ShowDialog() == DialogResult.OK) {
					profile.path = fileDialogSaveProject.FileName;
					profile.filename = Path.GetFileNameWithoutExtension(fileDialogSaveProject.FileName);
					saveFormToCharacter();
					saveCharactertoData();
					this.Text = profile.filename;
				}
			}
			else {
				saveFormToCharacter();
				try {
					saveCharactertoData();
                    isSavedStatus();
                }
				catch (Exception e) {
					MessageBox.Show("Failed to save.\nReason: " + e.Message);
				}
			}
		}

		private void openCharacter() {
			DialogResult result = new DialogResult();
			result = DialogResult.No;
            // Check if it's saved
            if (!saved) {
                result = MessageBox.Show("Save your current work before opening a Character?", "Open Project",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) {
                    saveCharacter();
                }
            }
            // Open the Character
			if (result != DialogResult.Cancel) {
				OpenFileDialog dlgFileOpen = new OpenFileDialog();
				dlgFileOpen.Filter = "OPRP files (*.oprp)|*.oprp";
				dlgFileOpen.Title = "Open Project";
				dlgFileOpen.RestoreDirectory = true;
				if (dlgFileOpen.ShowDialog() == DialogResult.OK) {
					try {
						profile.path = dlgFileOpen.FileName;
						profile.filename = Path.GetFileNameWithoutExtension(dlgFileOpen.FileName);
                        profile.openFile();
						resetForm();
						loadCharacterToForm();
						this.Text = profile.filename;
                        isSavedStatus();
                    }
					catch (Exception e) {
						MessageBox.Show("Failed to deserialize.\nReason: " + e.Message, 
							"Failed to Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void Load_CustomTags_Dict() {
			// This will add every single entry into the Dictionary
			CustomTags.Add(1, textBox_CharacterName.Text);
			CustomTags.Add(2, textBox_Nickname.Text);
			CustomTags.Add(3, numericUpDown_Age.Value.ToString());
			CustomTags.Add(4, comboBox_Gender.Text);
			CustomTags.Add(5, textBox_Race.Text);
			CustomTags.Add(6, comboBox_Affiliation.Text);
			CustomTags.Add(7, textBox_Bounty.Text);
			CustomTags.Add(8, comboBox_MarineRank.Text);
			CustomTags.Add(9, numericUpDown_Comm.Text);
			CustomTags.Add(10, textBox_Threat.Text);
			CustomTags.Add(11, textBox_Position.Text);
			// 12 is Achievements
			// 13 is Profession Name
			CustomTags.Add(14, textBox_Height.Text);
			CustomTags.Add(15, textBox_Weight.Text);
			CustomTags.Add(16, richTextBox_Hair.Text);
			CustomTags.Add(17, richTextBox_Eye.Text);
			CustomTags.Add(18, richTextBox_Clothing.Text);
			CustomTags.Add(19, richTextBox_GeneralAppear.Text);
			CustomTags.Add(20, richTextBox_Personality.Text);
			CustomTags.Add(21, textBox_Island.Text);
			CustomTags.Add(22, comboBox_Region.Text);
			CustomTags.Add(23, richTextBox_History.Text);
			CustomTags.Add(24, richTextBox_Combat.Text);
			// 25 is Weapon Name
			// 26 is Item Name
			CustomTags.Add(27, textBox_Beli.Text);
			CustomTags.Add(28, textBox_AP.Text);
			// 29 is AP Name
			CustomTags.Add(30, numericUpDown_SDEarned.Value.ToString());
			CustomTags.Add(31, textBox_SDRemain.Text);
			CustomTags.Add(32, textBox_StatPoints.Text);
			CustomTags.Add(33, textBox_SDtoSPCalc.Text);
			CustomTags.Add(34, textBox_UsedForStats.Text);
			CustomTags.Add(35, numericUpDown_UsedForFort.Value.ToString());
			CustomTags.Add(36, numericUpDown_StrengthBase.Value.ToString());
			CustomTags.Add(37, textBox_StrengthFinal.Text);
			CustomTags.Add(38, label_StrengthCalc.Text);
			CustomTags.Add(39, numericUpDown_SpeedBase.Value.ToString());
			CustomTags.Add(40, textBox_SpeedFinal.Text);
			CustomTags.Add(41, label_SpeedCalc.Text);
			CustomTags.Add(42, numericUpDown_StaminaBase.Value.ToString());
			CustomTags.Add(43, textBox_StaminaFinal.Text);
			CustomTags.Add(44, label_StaminaCalc.Text);
			CustomTags.Add(45, numericUpDown_AccuracyBase.Value.ToString());
			CustomTags.Add(46, textBox_AccuracyFinal.Text);
			CustomTags.Add(47, label_AccuracyCalc.Text);
			CustomTags.Add(48, textBox_Fortune.Text);
			CustomTags.Add(49, label_FortuneCalc.Text);
			// 50 is Trait Name (Gen)
			// 51 is Trait Name (Prof)
			CustomTags.Add(52, textBox_DFName.Text);
			CustomTags.Add(53, comboBox_DFType.Text);
			CustomTags.Add(54, richTextBox_DFDesc.Text);
			// 55 is Devil Fruit Free Effect [LEGACY]
			CustomTags.Add(56, textBox_RegTPUsed.Text);
			CustomTags.Add(57, textBox_RegTPTotal.Text);
			CustomTags.Add(58, label_RegTPCalc.Text);
			CustomTags.Add(59, textBox_SpTPUsed.Text);
			CustomTags.Add(60, textBox_SpTPTotal.Text);
			// 61 is Sp TP Trait Name
			CustomTags.Add(62, label_CritAnatQuick.Text);
			// 63 is Sp Tech Category Name
			// 64 is Tech Name
			// 65 is Tech Rank
			// 66 is Tech Tupe
			// 67 is Tech Power
			// 68 is Tech Stats
			// 69 is Tech Reg TP Spent
			// 70 is Tech Sp TP Spent
			// 71 is Tech Note
			// 72 is Tech Description
			// 73 is Tech Effects
			CustomTags.Add(74, textBox_TotalSD.Text);
			CustomTags.Add(75, comboBox_DFTier.Text);
			// 76 is Weapon Description
			// 77 is Item Description
			// 78 is AP Number
			// 79 is # Traits Spent (Gen)
			// 80 is Trait Desc (Gen)
			// 81 is # Traits Spent (Prof)
			// 82 is Trait Desc (Prof)
			// 83 is Sp Trait TP Used
			// 84 is Sp Trait TP Total
			// 85 is Profession Primary/Secondary
			// 86 is Profession Description
			// 87 is Profession Primary Bonus
			CustomTags.Add(88, textBox_MasteryMsg.Text);
			CustomTags.Add(89, textBox_Focus.Text);
			// 90 is Tech AE
			// 91 is Image URL
			// 92 is Image Label
			// 93 is Image Width
			// 94 is Image Height
			CustomTags.Add(95, genCap.ToString());
			CustomTags.Add(96, profCap.ToString());
			// 97 is Tech Range
			CustomTags.Add(98, numericUpDown_SDintoStats.Value.ToString());
			// 99 is AP Description
            // 100 is Tech Custom Note
            // 101 is DevH Date
            // 102 is DevH Title
            // 103 is DevH URL
            // 104 is DevH SD
            // 105 is DevH Beli
            // 106 is DevH Note
		}

		private void button_Generate_Click(object sender, EventArgs e) {
			Sheet sheet = new Sheet(1, richTextBox_Template.Text, techList);
			Load_CustomTags_Dict();
			Sheet.color_hex = textBox_Color.Text;
            List<int> AP_Numbers = new List<int>();
            AP_Numbers.Add((int)numericUpDown_APTech.Value);
            AP_Numbers.Add((int)numericUpDown_APTrait.Value * 2);
            AP_Numbers.Add((int)numericUpDown_APPrime.Value);
            AP_Numbers.Add((int)numericUpDown_APMulti.Value);
            AP_Numbers.Add((int)numericUpDown_APNPC.Value);
            AP_Numbers.Add((checkBox_APHaki.Checked) ? 2 : 0);
            AP_Numbers.Add((checkBox_APDF.Checked) ? 1 : 0);
			sheet.Generate_Template(listBox_Achieve, 
                profList, 
                listView_Images, 
                listView_Weaponry, 
                listView_Items, 
                AP_Numbers,
				traitList, 
                spTraitList, 
                dgv_Techniques, 
                listView_SubCat,
                dgv_Sources);
			sheet.ShowDialog();
			CustomTags.Clear(); // Clear Dictionary now that we're done.
		}

		private void button_ResetChar_Click(object sender, EventArgs e) {
			DialogResult result = new DialogResult();
			result = DialogResult.No;
			result = MessageBox.Show("Are you sure you want to reset this character?", "Reset Character",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (result == DialogResult.Yes) {
				resetForm();
			}
		}

		private void toolStripButton_New_Click(object sender, EventArgs e) {
			newCharacter();
		}

		private void toolStripButton_Open_Click(object sender, EventArgs e) {
			openCharacter();
		}

		private void toolStripButton_Save_Click(object sender, EventArgs e) {
			saveCharacter();
		}

        private void toolStripButton_SaveAs_Click(object sender, EventArgs e) {
            SaveFileDialog fileDialogSaveProject = new SaveFileDialog();
            fileDialogSaveProject.Filter = "OPRP files (*.oprp)|*.oprp";
            fileDialogSaveProject.Title = "Save New Project";
            fileDialogSaveProject.OverwritePrompt = true;
            if (fileDialogSaveProject.ShowDialog() == DialogResult.OK) {
                profile.path = fileDialogSaveProject.FileName;
                profile.filename = Path.GetFileNameWithoutExtension(fileDialogSaveProject.FileName);
                saveFormToCharacter();
                saveCharactertoData();
                this.Text = profile.filename;
                isSavedStatus();
            }
        }

		// Returns True if Template imported successfully, False otherwise
		private bool Import_Template() {
			OpenFileDialog dlgFileOpen = new OpenFileDialog();
			dlgFileOpen.Filter = "Text files (*.txt)|*.txt";
			dlgFileOpen.Title = "Import Template";
			dlgFileOpen.RestoreDirectory = true;
			if (dlgFileOpen.ShowDialog() == DialogResult.OK) {
				try {
					StreamReader sr = new StreamReader(dlgFileOpen.FileName);
					label_TemplateType.Text = Path.GetFileNameWithoutExtension(dlgFileOpen.FileName);
					label_TemplateType.ForeColor = Color.Blue;
					richTextBox_Template.Text = sr.ReadToEnd();
					template_imported = true;
					MessageBox.Show("Template \"" + label_TemplateType.Text + "\" imported successfully.", "Imported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    notSavedStatus();
					return true;
				}
				catch {
					MessageBox.Show("Error in importing Template.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}
			}
			else {
				return false;
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			// Check to save before closing the program
			if (!saved) {
				DialogResult result = new DialogResult();
				result = DialogResult.No;
				result = MessageBox.Show("Save changes before closing the tool?", "Save Changes",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (result == DialogResult.Yes) {
					saveCharacter();
				}
				else if (result == DialogResult.Cancel) {
					e.Cancel = true;
				}
			}
		}

        #endregion
    }

    // Class to sort ListView by number
    public class ListViewItemNumberSort : IComparer
    {
        private int col;
        public ListViewItemNumberSort(int column) { col = column; }
        public int Compare(object x, object y) {
            int nx = int.Parse((x as ListViewItem).SubItems[col].Text);
            int ny = int.Parse((y as ListViewItem).SubItems[col].Text);
            return nx.CompareTo(ny);
        }
    }

    // Class to sort ListView by Text based on selected Column
    public class ListViewItemSorter : IComparer
    {
        private int col;
        public ListViewItemSorter(int column) { col = column; }
        public int Compare(object x, object y) {
            return string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
    }
}
