﻿/*
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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Net;
using System.Reflection;
using System.Diagnostics;

namespace OPRPCharBuild
{
	public partial class MainForm : Form
	{
		public MainForm() {
			InitializeComponent();
		}

		// --------------------------------------------------------------------------------------------
		// MEMBER VARIABLES
		// --------------------------------------------------------------------------------------------

		#region Member Variables

		public const string VERSION = "1.5.0";
		public const string VERS_TYPE = "";
		public const string STD_TEMPLATE_MSG = "Standard Template";
        private const string WEBSITE = "https://github.com/mrdoowan/OPRPCharBuild/releases";
        public static bool template_imported = false;
        private static bool upgrading = false;             // Used when upgrading
        // Character Class
        // The following will update the Character Class at real-time:
        // Professions, Traits, and Techniques
        Character profile = new Character();
        // Variables for Templates
        public static Dictionary<int, string> CustomTags = new Dictionary<int, string>();
        // Variables that need their data type here instead of in Character
        int genCurr, genCap, profCurr, profCap;
        private Dictionary<string, Profession> profList = new Dictionary<string, Profession>();
        private Dictionary<string, Trait> traitList = new Dictionary<string, Trait>();
        private Dictionary<string, Technique> techList = new Dictionary<string, Technique>();
        // Sp Table dictionary that won't be stored in Character
        private Dictionary<string, SpTrait> spTraitList = new Dictionary<string, SpTrait>();

        #endregion

        #region General Functions

        // General function for Deleting an Item from ListView
        // Returns the string Name of the Item, specifically for finding Key values in Dictionary
        public static string Delete_ListViewItem(ref ListView list) {
			if (list.SelectedItems.Count == 1) {
				string key = list.SelectedItems[0].SubItems[0].Text;	// Typically the key value is always the name
				foreach (ListViewItem eachItem in list.SelectedItems) {
					list.Items.Remove(eachItem);
				}
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
					}
				}
				else if (direction == "Down") {
					if (curr_ind < list.Items.Count - 1) {
						list.Items.RemoveAt(curr_ind);
						list.Items.Insert(curr_ind + 1, item);
						// Maintain selection
						list.Items[curr_ind + 1].Selected = true;
					}
				}
				else {
					MessageBox.Show("There is a bug with this button!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		// Really don't want to use Microsoft's Click-Once application at this point, so I'll just
		// implement my own version.
		// Version now follows the following format:
		// Major.Minor.Revision (only 3)
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
					// We do nothing if the current version is greater than latest version
					if (int.Parse(current[i]) > int.Parse(latest[i])) {
						return;
					}
					else if (i == current.Length - 1 && (int.Parse(current[i]) >= int.Parse(latest[i]))) { // Last number check.
						return;
					}
				}
				// If we've arrived at this point, that means it needs updating.
				if (MessageBox.Show("An update to v" + version_page + " is available. Would you like to close this application and download the newest version?", "New Version",
					MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
					Process.Start(WEBSITE);
					Process.Start("http://s1.zetaboards.com/One_Piece_RP/topic/6060583/1/");
					upgrading = true;
					Application.Exit();
				}
			}
			catch (Exception e) {
				MessageBox.Show("Error in checking for an update.\nReason: " + e.Message, "OPRP Char Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			// -------------------------------------
			// Displaying any Bug Messages
			// -------------------------------------
			try {
				string header_msg = "OPRPCharBuilder " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " BugMessage " + Environment.OSVersion;
				WC.Headers.Add("Content-Type", header_msg);
				string message = WC.DownloadString("https://raw.githubusercontent.com/mrdoowan/OPRPCharBuild/master/BugMessage.txt");
				if (!string.IsNullOrWhiteSpace(message)) {
					MessageBox.Show("Current Bugs:\n\n" + message, "Bug Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				WC.Dispose();
			}
			catch (Exception e) {
				MessageBox.Show("Error in checking for a bug message\nReason: " + e.Message, "OPRP Char Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
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
                richTextBox_DFDesc.Text,
                textBox_DFEffect.Text);
        }

        #endregion

        #region Update Functions

        #region Update Trait Functions

        // Whenever a trait is added or deleted, we update the displayed number of Traits
        private void Update_Traits_Count_Label() {
			int gen = 0;    // 2nd Column
			int prof = 0;   // 3rd Column
            foreach (Trait trait in traitList.Values) {
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
			// Check if AP checked
			if (checkedListBox1_AP.CheckedIndices.Contains(1)) {	// [1] is the Trait
				gen++;
			}
			// Update label and global variable.
			genCap = gen;
			profCap = prof;
			// Update Focus
			int focus = 1 + gen / 2;
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

        private void Update_AP_Count() {
			int AP_num = checkedListBox1_AP.CheckedItems.Count;
			if (checkedListBox1_AP.CheckedIndices.Contains(1) || 
                checkedListBox1_AP.CheckedIndices.Contains(5)) {
                // These are 2 AP
                AP_num++;
			}
			textBox_AP.Text = AP_num.ToString();
			int SD = AP_num * 50;
			label37_SDonAP.Text = SD + " SD spent on AP";
			// Used when an AP is checked.
		}

		// Calculating Stat Points
		private void Update_Stat_Points() {
			int SD_in = (int)numericUpDown_SDintoStats.Value;
			string calc = "[32";
			int SP = 32;
			if (SD_in >= 0 && SD_in <= 150) {
				// 0-150SD is 1:1
				SP += SD_in;
				calc += " + " + SD_in;
			}
			else if (SD_in > 150 && SD_in <= 250) {
				// 151-250SD is 1.5:1
				int remain = SD_in - 150;
				int convert = (int)((double)remain / 1.5);
				SP += 150 + convert;
				calc += " + 150 + " + convert + " (" + remain + "/1.5)";
				// i.e. Calculations: 32 + 150 + 66(100/1.5)
			}
			else if (SD_in > 250 && SD_in <= 350) {
				// 251-350SD is 2:1
				int remain = SD_in - 150 - 100; // this is in SD
				int convert = remain / 2;
				SP += 150 + 66 + convert;
				calc += " + 150 + 66 (100/1.5) + " + convert + " (" + remain + "/2)";
			}
			else if (SD_in > 350 && SD_in <= 800) {
				// 351-800 SD is 3:1
				int remain = SD_in - 150 - 100 - 100;
				int convert = remain / 3;
				SP += 150 + 66 + 50 + convert;
				calc += " + 150 + 66 (100/1.5) + 50 (100/2) + " + convert + " (" + remain + "/3)";
			}
            else if (SD_in > 800 && SD_in <= 1200) {
                // 801-1200 SD is 4:1
                int remain = SD_in - 150 - 100 - 100 - 450;
                int convert = remain / 4;
                SP += 150 + 66 + 50 + 150 + convert;
                calc += " + 150 + 66 (100/1.5) + 50 (100/2) + 150 (450/3) + " + convert + " (" + remain + "/4)";
            }
            else {
                // 1201+ SD is 5:1
                int remain = SD_in - 150 - 100 - 100 - 450 - 400;
                int convert = remain / 5;
                SP += 150 + 66 + 50 + 150 + 100 + convert;
                calc += " + 150 + 66 (100/1.5) + 50 (100/2) + 150 (450/3) + 100 (400/4) " + convert + " (" + remain + "/5)";
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
			if (traitList.ContainsKey(Database.TR_FATEEM)) {
                // # Gen Traits is Column 2
                int traits = traitList[Database.TR_FATEEM].genNum;
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
			if (traitList.ContainsKey(fated)) {
                int Traits = traitList[fated].genNum;
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
            if (traitList.ContainsKey(Database.TR_STR3RD)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.6);
            }
            else if (traitList.ContainsKey(Database.TR_STR2ND)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.4);
            }
			else if (traitList.ContainsKey(Database.TR_STR1ST) || 
                traitList.ContainsKey(Database.TR_FISHMA) ||
                traitList.ContainsKey(Database.TR_DWARF)) {
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
            if (traitList.ContainsKey(Database.TR_SPE3RD)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.6);
            }
            else if (traitList.ContainsKey(Database.TR_SPE2ND)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.4);
            }
			else if (traitList.ContainsKey(Database.TR_SPE1ST) ||
				traitList.ContainsKey(Database.TR_MERFOL)) {
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
            if (traitList.ContainsKey(Database.TR_STA3RD)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.6);
            }
			else if (traitList.ContainsKey(Database.TR_STA2ND)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.4);
			}
			else if (traitList.ContainsKey(Database.TR_STA1ST)) {
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
            if (traitList.ContainsKey(Database.TR_ACC3RD)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.6);
            }
            else if (traitList.ContainsKey(Database.TR_ACC2ND)) {
                Stat_Multiplier_Trait(ref final, ref calc, 1.4);
            }
			else if (traitList.ContainsKey(Database.TR_ACC1ST)) {
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
			// Check AP
			if ((checkedListBox1_AP.CheckedIndices.Contains(0))) {
				multiplier += 0.5;
			}
			int total = (int)((double)fortune * multiplier);
			calc += multiplier;
			// Now check if we have any Traits that add to this.
			if (traitList.ContainsKey(Database.TR_TECHMA)) {
				// Increase by 100% of Fortune
				total += fortune;
				calc += " + " + fortune;
			}
			else if (traitList.ContainsKey(Database.TR_TECHAD)) {
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

        #region Update SpTP Functions

        // HELPER FUNCTION
        private void Helper_SpTrait_UsedOverTotal() {
            // After update of those values, we will then check to see if Used > Total
            foreach (ListViewItem Sp_Trait in listView_SpTP.Items) {
                if (int.Parse(Sp_Trait.SubItems[1].Text) > int.Parse(Sp_Trait.SubItems[2].Text)) {
                    Sp_Trait.SubItems[1].BackColor = Color.FromArgb(255, 128, 128);
                }
                else {
                    Sp_Trait.SubItems[1].BackColor = SystemColors.Control;
                }
            }
        }
        
        private void Update_Used_SpTP_Textbox() {
            // Updates the Used SpTP Textbox
			int used = 0;
            foreach (SpTrait spTrait in spTraitList.Values) {
                used += spTrait.usedTP;
            }
			textBox_SpTPUsed.Text = used.ToString();
		}
        
        private void Update_Total_SpTP_Textbox() {
            // Updates the Total SpTP Textbox
            int total_SP = 0;
            foreach (SpTrait spTrait in spTraitList.Values) {
                total_SP += spTrait.totalTP;
            }
			textBox_SpTPTotal.Text = total_SP.ToString();
		}

		private void Add_SpTrait(string traitName) {
			// First check to see if the Sp. TP Trait is in the list
			// If so, add the trait into the ListView Special and add correspondingly.
			int fortune = int.Parse(textBox_Fortune.Text);
            int divisor = Database.getSpTraitDiv(traitName);
            if (divisor > 0) {
                // Add Sp. Trait to the Dict
                int traitNum = traitList[traitName].getTotalTraits();
                int totTP = (fortune / divisor) * traitNum;
                SpTrait addTrait = new SpTrait(traitName, 0, totTP);
                spTraitList.Add(traitName, addTrait);
                // Add Sp. Trait to the ListView of SpTraits
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = traitName;      // 1st column: Trait Name
                item.SubItems.Add("0");                 // 2nd column: Used Sp. TP
                item.SubItems.Add(totTP.ToString());    // 3rd column: Total Sp. TP
                listView_SpTP.Items.Add(item);
            }
			// Lastly update the Total SpTP
			Update_Total_SpTP_Textbox();
			// Used when a Trait is added.
		}

        private void Remove_SpTrait(string traitName) {
            // Remove from Dict
            spTraitList.Remove(traitName);
            // Remove from ListView
            foreach (ListViewItem spTrait in listView_SpTP.Items) {
                if (spTrait.SubItems[0].Text == traitName) {
                    spTrait.Remove();
                    break;
                }
            }
            Update_Total_SpTP_Textbox();
            Update_Used_SpTP_Textbox();
            // Used when a Trait is removed.
        }

        private void Update_SpTraitTableAndDict_Total() {
            // This updates all the Total Sp Traits
            int fortune = int.Parse(textBox_Fortune.Text);
            foreach (ListViewItem spTrait in listView_SpTP.Items) {
                string name = spTrait.SubItems[0].Text;
                int traitNum = traitList[name].getTotalTraits();
                int divisor = Database.getSpTraitDiv(name);
                int totTP = (fortune / divisor) * traitNum;
                // Edit spTraitList
                spTraitList[name].totalTP = totTP;
                // Edit ListView
                spTrait.SubItems[2].Text = totTP.ToString();
            }
            // Update the Total textboxes
            Update_Total_SpTP_Textbox();
            // After update of those values, we will then check to see if Used > Total
            Helper_SpTrait_UsedOverTotal();
            // Used when Fortune is updated.
        }

        private void Update_SpTraitTableAndDict_Used(string traitName) {
            // Update values inside the table based on Techniques or Fortune edited.
            // Update SpTraitList first, then the ListView
            if (string.IsNullOrWhiteSpace(traitName)) { return; }
            int used = 0;
            foreach (Technique tech in techList.Values) {
                if (tech.specialTrait == traitName) {
                    used += tech.spTP;
                }
            }
            // Edit spTraitList
            spTraitList[traitName].usedTP = used;
            // Edit ListView
            foreach (ListViewItem spTrait in listView_SpTP.Items) {
                if (spTrait.SubItems[0].Text == traitName) {
                    spTrait.SubItems[1].Text = used.ToString();
                    break;
                }
            }
            // Update the Used textboxes
			Update_Used_SpTP_Textbox();
            // After update of those values, we will then check to see if Used > Total
            Helper_SpTrait_UsedOverTotal();
            // Used when a Technique is added.
            // Used when a Technique is edited.
            // Used when a Technique is branched.
            // Used when a Technique is removed.
        }

        #endregion

        #region Update Tech Misc Functions

        // Helper function for Update_CritAnatQuick_Msg()
        private void Print_Applied_Msg(ref string msg, string name) {
			int points = int.Parse(textBox_RegTPTotal.Text) / 4;
			int num = 0;
			msg += name + ": ";
            // How many Traits of the there are of the same Trait
			if (traitList.ContainsKey(name)) {
                num += traitList[name].getTotalTraits();
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
			if (traitList.ContainsKey(Database.TR_CRITHI)) {
				Print_Applied_Msg(ref msg, Database.TR_CRITHI);
			}
			if (traitList.ContainsKey(Database.TR_ANASTR)) {
				Print_Applied_Msg(ref msg, Database.TR_ANASTR);
			}
			if (traitList.ContainsKey(Database.TR_QUICKS)) {
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
			int num = listView_Techniques.Items.Count;
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
					int.Parse(category.SubItems[1].Text) != listView_Techniques.Items.Count - 1) {
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

        // This only occurs once before the form is displayed for the first time.
        private void MainForm_Load(object sender, EventArgs e) {
            this.Text = "OPRP Character Builder";
			label_Title.Text = "OPRP Character Builder";
			label1.Text = "OPRP Character Builder v" + VERSION + VERS_TYPE + " designed by Solo";

			// Check for updates of a New Version or Bug Messages
			//Check_Update();

			// ------ Professions
			listView_Prof.View = View.Details;
			listView_Prof.FullRowSelect = true;

			listView_Prof.Columns.Add("Profession", 130);
			listView_Prof.Columns.Add("Type", 100);
			listView_Prof.Columns.Add("Description", 265);
			listView_Prof.Columns.Add("Primary Bonus", 265);

			// ------ Images
			listView_Images.View = View.Details;
			listView_Images.FullRowSelect = true;
            
            listView_Images.Columns.Add("URL", 300);
            listView_Images.Columns.Add("Label", 200);
			listView_Images.Columns.Add("FullRes", 60);
			listView_Images.Columns.Add("Width", 55);
			listView_Images.Columns.Add("Height", 55);

			// ------ Traits
			listView_Traits.View = View.Details;
			listView_Traits.FullRowSelect = true;

			listView_Traits.Columns.Add("Trait Name", 200);
			listView_Traits.Columns.Add("Type", 150);
			listView_Traits.Columns.Add("# Gen", 50);
			listView_Traits.Columns.Add("# Prof", 50);
			listView_Traits.Columns.Add("Description", 350);

			// ------ Special Techniques
			listView_SpTP.View = View.Details;
			listView_SpTP.FullRowSelect = true;

			listView_SpTP.Columns.Add("Sp. Trait Name", 305);
			listView_SpTP.Columns.Add("Used", 75);
			listView_SpTP.Columns.Add("Total", 75);

			// ------ Technique Table
			listView_Techniques.View = View.Details;
			listView_Techniques.FullRowSelect = true;
			label_CritAnatQuick.Text = "";

			listView_Techniques.Columns.Add("Tech Name", 160);      // 0
			listView_Techniques.Columns.Add("Rank", 50);            // 1
			listView_Techniques.Columns.Add("Reg TP", 50);          // 2
			listView_Techniques.Columns.Add("Sp. TP", 50);          // 3
			listView_Techniques.Columns.Add("Sp. Trait", 75);       // 4
			listView_Techniques.Columns.Add("Branched From", 100);  // 5
			listView_Techniques.Columns.Add("Type", 75);            // 6
			listView_Techniques.Columns.Add("Range", 75);           // 7
			listView_Techniques.Columns.Add("Stats", 75);           // 8
			listView_Techniques.Columns.Add("Power", 50);           // 9

			// ------ Tech Category Table
			label_SubCatMsg.Text = "No Valid Category Selected";
			listView_SubCat.View = View.Details;
			listView_SubCat.FullRowSelect = true;
			listView_SubCat.Sorting = SortOrder.Ascending;

			listView_SubCat.Columns.Add("Begin", 50);
			listView_SubCat.Columns.Add("End", 50);
			listView_SubCat.Columns.Add("Category Name", 170);

			// ------ Weaponry Table
			listView_Weaponry.View = View.Details;
			listView_Weaponry.FullRowSelect = true;

			listView_Weaponry.Columns.Add("Name", 150);
			listView_Weaponry.Columns.Add("Description", 550);

			// ------ Items Table
			listView_Items.View = View.Details;
			listView_Items.FullRowSelect = true;

			listView_Items.Columns.Add("Name", 150);
			listView_Items.Columns.Add("Description", 550);

			// ------ Template
			richTextBox_Template.Text = Sheet.BASIC_TEMPLATE;

		}

		// --------------------------------------------------------------------------------------------
		// "BASIC CHARACTER" Tab
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
                string[] admiral = { "Fleet Admiral", "Admiral", "Vice Admiral" };
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
			AchievementWin.NewDialog(ref listBox_Achieve);
			// Look above to show how to transfer data between forms. This uses references quite well.
		}

		private void button_AchieveEdit_Click(object sender, EventArgs e) {
			// Achievement "Edit" button from the MainForm
			int index = listBox_Achieve.SelectedIndex;
			if (index != -1) {
				Add_Achievement AchievementWin = new Add_Achievement();
				AchievementWin.EditDialogue(ref listBox_Achieve, index);
			}
		}

		private void button2_AchieveDelete_Click(object sender, EventArgs e) {
			// Achievement "Delete" button from the MainForm
			int index = listBox_Achieve.SelectedIndex;
			if (index != -1) {
				listBox_Achieve.Items.RemoveAt(index);
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
		}

		private void button_UpAchieve_Click(object sender, EventArgs e) {
			MoveListBoxItem(-1);
		}

		private void button_DownAchieve_Click(object sender, EventArgs e) {
			MoveListBoxItem(1);
		}

		private void button4_ProfAdd_Click(object sender, EventArgs e) {
			// Profession "Add" button from the MainForm
			Add_Profession ProfessionWin = new Add_Profession();
			ProfessionWin.NewDialog(ref listView_Prof, ref profList);
		}

		private void button_ProfEdit_Click(object sender, EventArgs e) {
			// Profession "Edit" button from the MainForm
			// This is completely assuming that only one row can be selected (which we set MultiSelect = false)
			try {
				if (listView_Prof.SelectedItems.Count == 1) {
					Add_Profession ProfessionWin = new Add_Profession();
					ProfessionWin.EditDialog(ref listView_Prof, ref profList);
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error in editing Profession.\nReason: " + ex.Message);
			}
		}

		private void button5_ProfDelete_Click(object sender, EventArgs e) {
			// Profession "Delete" button from the MainForm
			// This is completely assuming that only one row can be selected (which we set MultiSelect = false)
			try {
				string Prof = Delete_ListViewItem(ref listView_Prof);
				if (!string.IsNullOrWhiteSpace(Prof)) {
					profList.Remove(Prof);
				}
            }
			catch (Exception ex) {
				MessageBox.Show("Error in deleting Profession.\nReason: " + ex.Message, "Exception Thrown");
			}
		}

		private void button_UpProf_Click(object sender, EventArgs e) {
			Move_List_Item(ref listView_Prof, "Up");
		}

		private void button_DownProf_Click(object sender, EventArgs e) {
			Move_List_Item(ref listView_Prof, "Down");
		}

		#endregion

		// --------------------------------------------------------------------------------------------
		// "PHYSICAL APPEARANCE" Tab
		// --------------------------------------------------------------------------------------------

		#region Physical Appearance 

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
					numericUpDown_Width.Value = int.Parse(listView_Images.SelectedItems[0].SubItems[3].Text);
					numericUpDown_Height.Value = int.Parse(listView_Images.SelectedItems[0].SubItems[4].Text);
				}
				else {
					checkBox_FullRes.Checked = true;
				}
                // Delete from ListView
				Delete_ListViewItem(ref listView_Images);
			}
		}

		private void button_ImageDelete_Click(object sender, EventArgs e) {
			// Image "Delete" button
			Delete_ListViewItem(ref listView_Images);
		}

		private void checkBox1_FullRes_CheckedChanged(object sender, EventArgs e) {
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
		// "COMBAT & STATS" Tab
		// --------------------------------------------------------------------------------------------

		#region Combat Tab

		private void button6_WeaponAdd_Click(object sender, EventArgs e) {
			// Weapon "Add" button from the MainForm
			Add_Equipment EquipmentWin = new Add_Equipment();
			EquipmentWin.Add_Weapon(ref listView_Weaponry);
		}

		private void button_WeaponEdit_Click(object sender, EventArgs e) {
			// Weapon "Edit" button from the MainForm
			// This is completely assuming that only one row can be selected (which we set MultiSelect = false)
			if (listView_Weaponry.SelectedItems.Count == 1) {
				Add_Equipment EquipmentWin = new Add_Equipment();
				EquipmentWin.Edit_Weapon(ref listView_Weaponry);
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
			EquipmentWin.Add_Item(ref listView_Items);
		}

		private void button_ItemsEdit_Click(object sender, EventArgs e) {
			// Item "Edit" button from the MainForm
			// This is completely assuming that only one row can be selected (which we set MultiSelect = false)
			if (listView_Items.SelectedItems.Count == 1) {
				Add_Equipment EquipmentWin = new Add_Equipment();
				EquipmentWin.Edit_Item(ref listView_Items);
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

		private string Commas_To_Value(uint beli) {
			string val = beli.ToString();
			for (int i = val.Length - 3; i > 0; i -= 3) {
				// Inserting from right to left
				val = val.Insert(i, ",");
			}
			return val;
		}

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
						message += "\n+ " + Commas_To_Value((uint)(250000 * SD)) + " (250,000 / SD in GL)";
					}
				}
				// Apply beli trait / professional boosts (do not stack)
				bool perc_20 = false;
				// Look for Traits bonus of 20%
				foreach (ListViewItem eachitem in listView_Traits.Items) {
					if (eachitem.SubItems[0].Text == "Pickpocket" || eachitem.SubItems[0].Text == "Tough Bargainer") {
						beli = (uint)(beli * 1.2);
						message += "\n+ " + Commas_To_Value((uint)(beli * 0.2)) + " (20% beli Trait Bonus)";
						perc_20 = true;
						break;
					}
				}
				if (!perc_20) {
					// Look for Thief primary bonus of 10%
					if (Is_Prof_Primary("Thief")) {
						beli = (uint)(beli * 1.1);
						message += "\n+ " + Commas_To_Value((uint)(beli * 0.1)) + " (10% beli Thief Primary)";
					}
				}
				// Show calculations
				string final_beli = Commas_To_Value(beli);
				message += "\n= " + final_beli + " (FINAL undeducted Value)";
				MessageBox.Show(message, "Beli Standardized");
				textBox_Beli.Text = final_beli;
			}
		}

		#endregion

		#region Stats Tab

		private void checkedListBox1_AP_SelectedIndexChanged(object sender, EventArgs e) {
			// To keep track of AP
			Update_AP_Count();
			checkedListBox1_AP.ClearSelected();
			Update_TotalSD();
			Update_Traits_Cap(); // For increasing Trait cap
			Update_Total_RegTP(); // For Tech multiplier
		}

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
			Update_SpTraitTableAndDict_Total();
		}

		private void textBox_UsedForStats_TextChanged(object sender, EventArgs e) {
			Update_BaseStats_Check();
		}

		#endregion

		// --------------------------------------------------------------------------------------------
		// "TRAITS" Tab
		// --------------------------------------------------------------------------------------------

		#region Traits Tab

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
		}

		private void button11_TraitAdd_Click(object sender, EventArgs e) {
			// Traits "Add" button from the MainForm
			Add_Trait TraitWin = new Add_Trait();
			string name = TraitWin.NewDialog(ref listView_Traits, ref traitList);
			// And lastly all Update functions
			if (name != null) {
                All_Update_Functions_Traits();
                Add_SpTrait(name);
            }
		}

		private void button10_TraitsDelete_Click(object sender, EventArgs e) {
			// Traits "Delete" button from the MainForm
			// This is completely assuming that only one row can be selected (which we set MultiSelect = false)
			if (listView_Traits.SelectedItems.Count == 0) { return; }
			string name = Delete_ListViewItem(ref listView_Traits);
            // Remove trait from traitList
            traitList.Remove(name);
			// All update Functions
			All_Update_Functions_Traits();
            Remove_SpTrait(name);
		}

		#endregion

		// --------------------------------------------------------------------------------------------
		// "TECHNIQUES" Tab
		// --------------------------------------------------------------------------------------------

		#region Techniques Tab

		private void All_Update_Functions_Techs(string traitName) {
            Update_SpTraitTableAndDict_Used(traitName);
			Update_Used_RegTP();
			Update_CritAnatQuick_Msg();
			Update_TechNum();
        }

		private void listView3_SpTP_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) {
			// Prevents users from changing column width
			e.Cancel = true;
			e.NewWidth = listView_SpTP.Columns[e.ColumnIndex].Width;
		}

		private void button14_TechAdd_Click(object sender, EventArgs e) {
			// Technique "Add" button from the MainForm
			int max_rank = int.Parse(textBox_Fortune.Text) / 2;
			int num_items = listView_Techniques.Items.Count;
			if (num_items == 0) { num_items++; } // What if empty?
			Add_Technique TechniqueWin = new Add_Technique(max_rank, profList, 
                traitList, spTraitList, makeDFClass(), false, false, null);
			string newName = TechniqueWin.NewDialog(ref listView_Techniques, ref techList, num_items - 1);
            // Update functions go below
            if (!string.IsNullOrWhiteSpace(newName)) {
                string spTrait = techList[newName].specialTrait;
                All_Update_Functions_Techs(spTrait);
            }
		}

		private void button_TechBranch_Click(object sender, EventArgs e) {
			// Technique "Branch" button from the MainForm
			if (listView_Techniques.SelectedItems.Count == 0) { return; }
			string TechName = listView_Techniques.SelectedItems[0].SubItems[0].Text;
			int index = listView_Techniques.SelectedIndices[0];
			if (!string.IsNullOrWhiteSpace(TechName)) {
				int max_rank = int.Parse(textBox_Fortune.Text) / 2;
				Technique selTech = techList[TechName];
				if (selTech.rokuName != Database.ROKU_NON && 
                    !traitList.ContainsKey(Database.TR_ROKUMA)) {
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
					Add_Technique TechniqueWin = new Add_Technique(max_rank, profList, traitList, 
                        spTraitList, makeDFClass(), true, false, selTech);
                    string newName = TechniqueWin.NewDialog(ref listView_Techniques, ref techList, index);
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
			if (listView_Techniques.SelectedItems.Count == 0) { return; }
			string TechName = listView_Techniques.SelectedItems[0].SubItems[0].Text;
			if (!string.IsNullOrWhiteSpace(TechName)) {
				int max_rank = int.Parse(textBox_Fortune.Text) / 2;
                Technique selTech = techList[TechName];
                if (selTech.rokuName != Database.ROKU_NON &&
                    !traitList.ContainsKey(Database.TR_ROKUMA)) {
					// ^If the Selected Technique is Rokushiki and character does not have Rokushiki Master
					MessageBox.Show("You can't edit a Rokushiki Technique without the Rokushiki Master Trait!", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else {
                    Add_Technique TechniqueWin = new Add_Technique(max_rank, profList, traitList,
                        spTraitList, makeDFClass(), false, true, selTech);
                    string newName = TechniqueWin.EditDialog(ref listView_Techniques, ref techList);
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
			if (listView_Techniques.SelectedItems.Count == 0) { return; }
			DialogResult result = MessageBox.Show("Are you sure you want to delete \"" + 
				listView_Techniques.SelectedItems[0].SubItems[0].Text + "\"?", "Remove Tech",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes) {
				try {
					string TechName = Delete_ListViewItem(ref listView_Techniques);
                    string spTrait = "";
					if (!string.IsNullOrWhiteSpace(TechName)) {
                        spTrait = techList[TechName].specialTrait;
                        techList.Remove(TechName);
					}
					// Update functions go below
					All_Update_Functions_Techs(spTrait);
				}
				catch (Exception ex) {
					MessageBox.Show("Error in Deleting Technique\nReason: " + ex.Message, "Exception Thrown");
				}
			}
		}

		private void button_UpTech_Click(object sender, EventArgs e) {
			Move_List_Item(ref listView_Techniques, "Up");
		}

		private void button_DownTech_Click(object sender, EventArgs e) {
			Move_List_Item(ref listView_Techniques, "Down");
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

		private void listView_Techniques_SelectedIndexChanged(object sender, EventArgs e) {
			try {
				int row = listView_Techniques.SelectedIndices[0];
				label_RowNum.Text = "Row " + row + " Selected";
			}
			catch {
				label_RowNum.Text = "Select a Technique to see which Row it is in.";
			}
		}

		// Returns the Index of the listview_SubCat if the row is between any Row Begin and Row End
		// Returns -1 if the row is not between any Row Begin and Row End
		private int Is_Row_In_SubCatTable(int row) {
			foreach (ListViewItem item in listView_SubCat.Items) {
				int begin = int.Parse(item.SubItems[0].Text);
				int end = int.Parse(item.SubItems[1].Text);
				if (row >= begin && row <= end) {
					return item.Index;
				}
			}
			return -1;
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
			else if (listView_Techniques.Items.Count < 1) {
				MessageBox.Show("You have no Techniques.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else if (Is_Row_In_SubCatTable((int)numericUpDown_RowBegin.Value) != -1) {
				MessageBox.Show("Row Begin overlaps with another Category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else if (Is_Row_In_SubCatTable((int)numericUpDown_RowEnd.Value) != -1) {
				MessageBox.Show("Row End overlaps with another Category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			ListViewItem item = new ListViewItem();
			item.SubItems[0].Text = numericUpDown_RowBegin.Value.ToString();
			item.SubItems.Add(numericUpDown_RowEnd.Value.ToString());
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
			Update_SubCatWarning();
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
			}
		}

		private void listView_SubCat_SelectedIndexChanged(object sender, EventArgs e) {
			if (listView_SubCat.SelectedItems.Count == 1) {
				try {
					int begin_ind = int.Parse(listView_SubCat.SelectedItems[0].SubItems[0].Text);
					int end_ind = int.Parse(listView_SubCat.SelectedItems[0].SubItems[1].Text);
					string begin_str = listView_Techniques.Items[begin_ind].SubItems[0].Text;
					string end_str = listView_Techniques.Items[end_ind].SubItems[0].Text;
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

        #region Template Tab

        private void button_LoadTemp_Click(object sender, EventArgs e) {
            Import_Template();
        }

        private void button_ResetTemp_Click(object sender, EventArgs e) {
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
			listView_Prof.Items.Clear();
			profList.Clear();
			// Physical Appearance
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
			textBox_DFEffect.Clear();
			// Stats
			numericUpDown_SDEarned.Value = 0;
			numericUpDown_SDintoStats.Value = 0;
			for (int i = 0; i < checkedListBox1_AP.Items.Count; ++i) {
				checkedListBox1_AP.SetItemChecked(i, false);
			}
			textBox_AP.Text = "0";
            textBox_Focus.Text = "0";
			numericUpDown_UsedForFort.Value = 0;
			numericUpDown_StrengthBase.Value = 1;
			numericUpDown_SpeedBase.Value = 1;
			numericUpDown_StaminaBase.Value = 1;
			numericUpDown_AccuracyBase.Value = 1;
			// Traits
			listView_Traits.Items.Clear();
			traitList.Clear();
            // Techniques
			listView_SpTP.Items.Clear();
            spTraitList.Clear();
            label_CritAnatQuick.Text = "";
			listView_Techniques.Items.Clear();
			Update_TechNum();
			techList.Clear();
			listView_SubCat.Items.Clear();
			textBox_SubCat.Clear();
            // Templates
            richTextBox_Template.Text = Sheet.BASIC_TEMPLATE;
            textBox_Color.Clear();
            textBox_MasteryMsg.Text = "* denotes +4 Rank Mastery";
            // Update Functions (that are still needed)
            Update_AP_Count();
			Update_Strength_Final();
			Update_Speed_Final();
			Update_Stamina_Final();
			Update_Accuracy_Final();
			Update_Traits_Count_Label();
			Update_Traits_Cap();
			Update_Total_RegTP();
			Update_Used_RegTP();
            textBox_SpTPTotal.Text = "0";
            textBox_SpTPUsed.Text = "0";
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
				textBox_DFName.Text,
				comboBox_DFType.Text,
                comboBox_DFTier.Text,
				richTextBox_DFDesc.Text,
				textBox_DFEffect.Text
				);
			profile.saveCharStats(
				(int)numericUpDown_SDEarned.Value,
				(int)numericUpDown_SDintoStats.Value,
				checkedListBox1_AP,
				(int)numericUpDown_UsedForFort.Value,
				(int)numericUpDown_StrengthBase.Value,
				(int)numericUpDown_SpeedBase.Value,
				(int)numericUpDown_StaminaBase.Value,
				(int)numericUpDown_AccuracyBase.Value
				);
			profile.saveCharTraits(traitList);
			profile.saveCharTechs(techList, listView_SubCat);
            profile.saveCharTemplate(
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
			  ref listBox_Achieve,
			  ref listView_Prof,
              ref profList
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
			  ref textBox_Beli,
			  ref textBox_DFName,
			  ref comboBox_DFType,
              ref comboBox_DFTier,
			  ref richTextBox_DFDesc,
			  ref textBox_DFEffect
			  );
			}

			catch (Exception ex) { MessageBox.Show("Load Combat and Abilities Error.\nReason: " + ex.Message); }
            try {
                profile.loadCharStats(
                ref numericUpDown_SDEarned,
                ref numericUpDown_SDintoStats,
                ref checkedListBox1_AP,
                ref numericUpDown_UsedForFort,
                ref numericUpDown_StrengthBase,
                ref numericUpDown_SpeedBase,
                ref numericUpDown_StaminaBase,
                ref numericUpDown_AccuracyBase
                );
            }
            catch (Exception ex) { MessageBox.Show("Load Stats Error.\nReason: " + ex.Message); }
            try {
                profile.loadCharTraits(
                    ref traitList,
                    ref listView_Traits,
                    ref spTraitList,
                    int.Parse(textBox_Fortune.Text)
                    );
                listView_Traits.ListViewItemSorter = new ListViewItemSorter(1);
                listView_Traits.Sort();
            }
			catch (Exception ex) { MessageBox.Show("Load Traits Error.\nReason: " + ex.Message); }
            try {
                profile.loadCharTechs(
                    ref techList,
                    ref listView_Techniques,
                    ref spTraitList,
                    ref listView_SpTP,
                    ref listView_SubCat
                    );
            }
            catch (Exception ex) { MessageBox.Show("Load Techniques Error.\nReason: " + ex.Message); }
            profile.loadCharTemplate(
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
		}

		// This is where serialize our profile.
		private void savdCharactertoData() {
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
			result = MessageBox.Show("Save your current work before making a New Character?", "Make New Project",
				MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			if (result == DialogResult.Yes) {
				saveCharacter();
			}
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
					savdCharactertoData();
					this.Text = profile.filename;
				}
			}
			else {
				saveFormToCharacter();
				try {
					savdCharactertoData();
				}
				catch (Exception e) {
					MessageBox.Show("Failed to save.\nReason: " + e.Message);
				}
			}
		}

		private void openCharacter() {
			DialogResult result = new DialogResult();
			result = DialogResult.No;
			result = MessageBox.Show("Save your current work before opening a Character?", "Open Project",
				MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			if (result == DialogResult.Yes) {
				saveCharacter();
			}
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
					}
					catch (Exception e) {
						MessageBox.Show("Failed to deserialize. It may be because you loaded an older version." +
							"Please try to Import your older version file instead.\nReason: " + e.Message, 
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
			CustomTags.Add(55, textBox_DFEffect.Text);
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
		}

		private void button_Generate_Click(object sender, EventArgs e) {
			Sheet sheet = new Sheet(1, richTextBox_Template.Text, techList);
			Load_CustomTags_Dict();
			Sheet.color_hex = textBox_Color.Text;
			sheet.Generate_Template(listBox_Achieve, 
                profList, 
                listView_Images, 
                listView_Weaponry, 
                listView_Items, 
                checkedListBox1_AP,
				traitList, 
                spTraitList, 
                listView_Techniques, 
                listView_SubCat);
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
                savdCharactertoData();
                this.Text = profile.filename;
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
			if (!upgrading) {
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
