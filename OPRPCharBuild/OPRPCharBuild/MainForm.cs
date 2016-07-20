using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		// PUBLIC / PRIVATE MEMBER STRUCTS, FUNCTIONS, AND VARIABLES
		// --------------------------------------------------------------------------------------------

		#region Member Variables and Structs

		public const string version = "1.0.2.2";
		public const string vers_type = "";
		public const string curr_proj = "Project2";
		public const string std_template_msg = "Standard Template";
		public static bool template_imported = false;
        private const string website = "https://github.com/mrdoowan/OPRPCharBuild/releases";
		private Traits Traits = new Traits();           // For enumerations of Traits
		private Project2 project = new Project2();      // State of save file
		private Project project1 = new Project();   // Used to import an old file and transfer to newest project
		private bool upgrading = false;				// Used when upgrading
		private int gen_cap = 0;					// To carry across Trait functions
		private int prof_cap = 0;
		private int gen_curr = 0;					// May need this eventually for "algorithmic" upgrade, but who cares
		private int prof_curr = 0;
		// Hold a list of Professions that is "bound" to the ListView. String is the name, and bool is if it's primary or not.
		// Dictionary is fine because string values SHOULD be unique
		public static Dictionary<string, bool> ProfList = new Dictionary<string, bool>();
		// Just a List of Trait_ID will suffice to bind to its ListView
		public static List<Traits.Trait_Name> TraitsList = new List<Traits.Trait_Name>();
		// Techniques. My favorite. The struct TechInfo is designed to load all the information to Add_Technique form
		public static Dictionary<string, TechInfo> TechList = new Dictionary<string, TechInfo>();
		public struct TechInfo
		{
			public Rokushiki.RokuName Roku;
			public int rank;
			public int regTP;
			public int spTP;
			public string rank_Trait;
			public string sp_Trait;
			public string app_Traits;
			public string tech_Branch;
			public int rank_Branch;
			public string type;
			public string range;
			public TechStats stats;
			public bool NA_power;
			public Dictionary<string, Add_Technique.EffectItem> effectList;
			public string power;
			public List<bool> DF_checkBox;
			public List<bool> Cyborg_Boosts;
			public string note;
			public string desc;

			// Constructor
			public TechInfo(Rokushiki.RokuName Roku_, int rank_, int regTP_, int spTP_, string rankTrait_, string spTrait_, string appTraits_,
				string techBranch_, int rankBranch_, string type_, string range_, TechStats stats_, bool NA_,
				Dictionary<string, Add_Technique.EffectItem> effectList_, string power_, List<bool> DF_, List<bool> Cyborg_,
				string note_, string desc_) {
				Roku = Roku_;
				rank = rank_;
				regTP = regTP_;
				spTP = spTP_;
				rank_Trait = rankTrait_;
				app_Traits = appTraits_;
				sp_Trait = spTrait_;
				tech_Branch = techBranch_;
				rank_Branch = rankBranch_;
				type = type_;
				range = range_;
				stats = stats_;
				NA_power = NA_;
				effectList = effectList_;
				power = power_;
				DF_checkBox = DF_;
				Cyborg_Boosts = Cyborg_;
				note = note_;
				desc = desc_;
			}
		}

		public struct TechStats
		{
			public int str;
			public int spe;
			public int sta;
			public int acc;

			// Constructor
			public TechStats(int str_, int spe_, int sta_, int acc_) {
				str = str_;
				spe = spe_;
				sta = sta_;
				acc = acc_;
			}
		}

		// Variables for Templates
		public static Dictionary<int, string> CustomTags = new Dictionary<int, string>();	// This will only be updated when Template is being generated

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
				string[] current = version.Split('.');
				// Get latest version from site
				string header_msg = "OPRPCharBuilder " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " UpdateCheck " + Environment.OSVersion;
				WC.Headers.Add("Content-Type", header_msg);
				string version_page = WC.DownloadString("https://raw.githubusercontent.com/mrdoowan/OPRPCharBuild/master/CurrentVer.txt");
				string[] latest = version_page.Split('.');
				// There should only be 3 numbers. (IMPLEMENT AFTER v1.3.0)
				/* if (current.Length != latest.Length) {
					MessageBox.Show("Current and Latest version Length are not equal.", "Report Bug", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				} */
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
					Process.Start(website);
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
					MessageBox.Show("Current bugs in v" + version + "\n\n" + message, "Bug Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				WC.Dispose();
			}
			catch (Exception e) {
				MessageBox.Show("Error in checking for a bug message\nReason: " + e.Message, "OPRP Char Builder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		// Returns true if Prof is in the List and it's Primary
		// Returns false otherwise
		public static bool Is_Prof_Primary(string name) {
			if (ProfList.ContainsKey(name)) {
				if (ProfList[name]) {
					return true;
				}
			}
			return false;
		}

		// Sets Primary bool variables for certain situations
		public static bool Is_Primary_Bool(string prof) {
			if (Is_Prof_Primary(prof)) {
				return true;
			}
			else {
				return false;
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

		#endregion

		#region Update Functions

		// Whenever a trait is added or deleted, we update the displayed number of Traits
		private void Update_Traits_Count_Label() {
			int gen = 0;    // 2nd Column
			int prof = 0;   // 3rd Column
			foreach (ListViewItem eachItem in listView_Traits.Items) {
				gen += int.Parse(eachItem.SubItems[2].Text);
				prof += int.Parse(eachItem.SubItems[3].Text);
			}
			gen_curr = gen;
			prof_curr = prof;
			label58_TraitsCurrent.Text = "You currently have " + gen + " General Trait(s) and " +
				prof + " Professional Trait(s)";
			if (gen_curr == gen_cap && prof_curr == prof_cap) {
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
			gen_cap = gen;
			prof_cap = prof;
			// Update Focus
			int focus = 1 + gen / 2;
			textBox_Focus.Text = focus.ToString();
			// Update Traits Message
			label59_TraitsCalc.Text = "Your current cap is " + gen +
				" General Trait(s) and " + prof + " Professional Trait(s)";
			if (gen_curr == gen_cap && prof_curr == prof_cap) {
				label58_TraitsCurrent.ForeColor = Color.Green;
			}
			else {
				label58_TraitsCurrent.ForeColor = Color.Red;
			}
		}

		private void Update_AP_Count() {
			int AP_num = checkedListBox1_AP.CheckedItems.Count;
			if (checkedListBox1_AP.CheckedIndices.Contains(1)) {
				AP_num++;
				// This is 2 AP
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
			else {
				// 351+ SD is 3:1
				int remain = SD_in - 150 - 100 - 100;
				int convert = remain / 3;
				SP += 150 + 66 + 50 + convert;
				calc += " + 150 + 66 (100/1.5) + 50 (100/2) + " + convert + " (" + remain + "/3)";
			}
			calc += ']';
			textBox_StatPoints.Text = SP.ToString();
			label_SDtoSPCalculations.Text = calc;
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
			int index = Traits.Contains_Trait_AtIndex(Traits.Trait_Name.FATE_EMP, listView_Traits);
			if (index != -1) {
				// # Gen Traits is Column 2
				int traits = int.Parse(listView_Traits.Items[index].SubItems[2].Text);
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
				// And for some reason I can't do arithmetic operations with two doubles or
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

		// This is only for stats though.
		private void Add_Fated_Stats(ref int stat, ref string calc, Traits.Trait_Name fated) {
			int index = Traits.Contains_Trait_AtIndex(fated, listView_Traits);
			if (index != -1) {
				int Traits = int.Parse(listView_Traits.Items[index].SubItems[2].Text);
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
			if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.MIGHT_STR, listView_Traits) != -1 ||
				(Traits.Contains_Trait_AtIndex(Traits.Trait_Name.FISHMAN, listView_Traits) != -1 && Traits.Contains_Trait_AtIndex(Traits.Trait_Name.SUP_STR, listView_Traits) == -1 && Traits.Contains_Trait_AtIndex(Traits.Trait_Name.MON_STR, listView_Traits) == -1) ||
				(Traits.Contains_Trait_AtIndex(Traits.Trait_Name.DWARF, listView_Traits) != -1) && Traits.Contains_Trait_AtIndex(Traits.Trait_Name.SUP_STR, listView_Traits) == -1 && Traits.Contains_Trait_AtIndex(Traits.Trait_Name.MON_STR, listView_Traits) == -1) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.2);

			}
			else if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.SUP_STR, listView_Traits) != -1) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.4);
			}
			else if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.MON_STR, listView_Traits) != -1) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.6);
			}
			// And then lastly, the Fated Trait.
			Add_Fated_Stats(ref final, ref calc, Traits.Trait_Name.FATE_STR);
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
			if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.GREAT_SPE, listView_Traits) != -1 ||
				(Traits.Contains_Trait_AtIndex(Traits.Trait_Name.MERFOLK, listView_Traits) != -1 && Traits.Contains_Trait_AtIndex(Traits.Trait_Name.SON_SPE, listView_Traits) == -1 && Traits.Contains_Trait_AtIndex(Traits.Trait_Name.LUD_SPE, listView_Traits) == -1)) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.2);

			}
			else if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.SON_SPE, listView_Traits) != -1) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.4);
			}
			else if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.LUD_SPE, listView_Traits) != -1) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.6);
			}
			// And then lastly, the Fated Trait.
			Add_Fated_Stats(ref final, ref calc, Traits.Trait_Name.FATE_SWIFT);
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
			if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.BEAR_STAM, listView_Traits) != -1) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.2);
			}
			else if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.MAM_STAM, listView_Traits) != -1) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.4);
			}
			else if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.GIANT_STAM, listView_Traits) != -1) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.6);
			}
			// And then lastly, the Fated Trait.
			Add_Fated_Stats(ref final, ref calc, Traits.Trait_Name.FATE_MIGHT);
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
			if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.KEEN_ACC, listView_Traits) != -1) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.2);

			}
			else if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.SUP_ACC, listView_Traits) != -1) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.4);
			}
			else if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.FLAW_ACC, listView_Traits) != -1) {
				Stat_Multiplier_Trait(ref final, ref calc, 1.6);
			}
			// And then lastly, the Fated Trait.
			Add_Fated_Stats(ref final, ref calc, Traits.Trait_Name.FATE_CUN);
			textBox_AccuracyFinal.Text = final.ToString();
			calc += ']';
			label_AccuracyCalc.Text = calc;
			// Used when Base Strength changes.
			// Used when Traits are added.
			// Used when Traits are edited
			// Used when Traits are removed.
		}

		private void Update_Used_RegTP() {
			int used = 0;
			foreach (ListViewItem eachitem in listView_Techniques.Items) {
				// Column 2 is regTP
				used += int.Parse(eachitem.SubItems[2].Text);
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
			else if (SD_Earned > 450) {
				multiplier = 4.0;
			}
			// Check AP
			if ((checkedListBox1_AP.CheckedIndices.Contains(0))) {
				multiplier += 0.5;
			}
			int total = (int)((double)fortune * multiplier);
			calc += multiplier;
			// Now check if we have any Traits that add to this.
			if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.TECH_MAST, listView_Traits) != -1) {
				// Increase by 100% of Fortune
				total += fortune;
				calc += " + " + fortune;
			}
			else if (Traits.Contains_Trait_AtIndex(Traits.Trait_Name.TECH_ADEPT, listView_Traits) != -1) {
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
			// Used when Traits are edited
			// Used when a Trait is removed.
		}

		private void Update_Used_SpTP() {
			int used = 0;
			// Use the Special Trait list instead
			foreach (ListViewItem eachitem in listView_SpTP.Items) {
				// Column 1 is Sp.TP used
				used += int.Parse(eachitem.SubItems[1].Text);
			}
			textBox_SpTPUsed.Text = used.ToString();
			// Use inside Update_SpTrait_Table() at the very end
		}

		private void Update_Total_SpTP() {
			int total_SP = 0;
			foreach (ListViewItem eachItem in listView_SpTP.Items) {
				// Total SpTP is in 3rd column
				total_SP += int.Parse(eachItem.SubItems[2].Text);
			}
			textBox_SpTPTotal.Text = total_SP.ToString();
			// Use inside Update_SpTrait_Table() at the very end
		}

		private void Add_Item_SpTrait_Table(Traits.Trait_Name trait, ListView traits_list, int fortune, int divisor) {
			// Add to Sp. Trait from the ListView of Traits.
			int index = Traits.Contains_Trait_AtIndex(trait, traits_list);
			ListViewItem item = new ListViewItem();
			item.SubItems[0].Text = traits_list.Items[index].SubItems[0].Text;  // 1st column: Trait Name
			item.SubItems.Add("0");                             // 2nd column: Used Sp. TP
			item.SubItems.Add((fortune / divisor).ToString());  // 3rd column: Total Sp. TP
																// Add item into the Sp. Table
			listView_SpTP.Items.Add(item);
		}

		private void Update_SpTrait_Table_Traits(Traits.Trait_Name ID) {
			// Need a parameter of what Special Trait was ADDED!!!
			// First check to see if the Sp. TP Trait is in the list
			// If so, add the trait into the ListView Special and add correspondingly.
			int fortune = int.Parse(textBox_Fortune.Text);
			if (ID == Traits.Trait_Name.SUP_SENSE) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.SUP_SENSE, listView_Traits, fortune, 4);
			}
			else if (ID == Traits.Trait_Name.STR_SPIRIT) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.STR_SPIRIT, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.ROK_SAV) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.ROK_SAV, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.ANTI_WEAPON) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.ANTI_WEAPON, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.ADV_MARTIAL_CLASS) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.ADV_MARTIAL_CLASS, listView_Traits, fortune, 4);
			}
			else if (ID == Traits.Trait_Name.UNCIV_ENG) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.UNCIV_ENG, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.SIEGE_WAR) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.SIEGE_WAR, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.BRILL_MIND) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.BRILL_MIND, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.L_O_R) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.L_O_R, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.FOOD_WAR) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.FOOD_WAR, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.COOK_FIGHT) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.COOK_FIGHT, listView_Traits, fortune, 4);
			}
			else if (ID == Traits.Trait_Name.DAZZLE_PERF) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.DAZZLE_PERF, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.SKILL_MED) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.SKILL_MED, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.MED_MAL) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.MED_MAL, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.POIS_KILL) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.POIS_KILL, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.FROM_SHAD) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.FROM_SHAD, listView_Traits, fortune, 2);
			}
			else if (ID == Traits.Trait_Name.SLEIGHT) {
				Add_Item_SpTrait_Table(Traits.Trait_Name.SLEIGHT, listView_Traits, fortune, 2);
			}
			// Likewise, if a trait is removed, we have to check each SpTrait.
			// For the parameter, just put CUSTOM
			int cur_ind = 0;
			foreach (ListViewItem eachItem in listView_SpTP.Items) {
				// Locate index of SpTraits first.
				string name = eachItem.SubItems[0].Text; // Name of Sp. Trait
				if (Traits.Contains_Trait_AtIndex(Traits.get_TraitID(name), listView_Traits) == -1) {
					// That means we couldn't find the SpTrait in the Trait listView
					// So we delete it from the SpTrait Table
					listView_SpTP.Items[cur_ind].Remove();
					break;
				}
				cur_ind++;
			}
			// Lastly update the Total SpTP
			Update_Total_SpTP();
			// Used when a Trait is added.
			// Used when a Trait is edited
			// Used when a Trait is removed.
		}

		private void Update_SpTrait_Table_Values() {
			// Update values inside the table based on Techniques or Fortune edited.
			// Nest loop this.
			foreach (ListViewItem Sp_Trait in listView_SpTP.Items) {
				string name1 = Sp_Trait.SubItems[0].Text;
				Traits.Trait_Name ID_Sp = Traits.get_TraitID(name1);
				int used = 0;
				foreach (ListViewItem Tech in listView_Techniques.Items) {
					// Column 4 is Special Trait
					string tech_trait = Tech.SubItems[4].Text;
					Traits.Trait_Name ID_Tech = Traits.get_TraitID(tech_trait);
					if (ID_Sp == ID_Tech) {
						used += int.Parse(Tech.SubItems[3].Text); // Column 3 is Sp. TP
					}
				}
				Sp_Trait.SubItems[1].Text = used.ToString();
			}
			Update_Total_SpTP();
			Update_Used_SpTP();
			// After update of those values, we will then check to see if Used > Total
			foreach (ListViewItem Sp_Trait in listView_SpTP.Items) {
				if (int.Parse(Sp_Trait.SubItems[1].Text) > int.Parse(Sp_Trait.SubItems[2].Text)) {
					Sp_Trait.SubItems[1].BackColor = Color.FromArgb(255, 128, 128);
				}
				else {
					Sp_Trait.SubItems[1].BackColor = SystemColors.Control;
				}
			}
			// Used when Fortune is updated.
			// Used when a Trait is added.
			// Used when a Trait is edited
			// Used when a Trait is removed.
			// Used when a Technique is added.
			// Used when a Technique is edited.
			// Used when a Technique is removed.
		}

		// Helper function for Update_CritAnatQuick_Msg()
		private void Print_Applied_Msg(ref string msg, Traits.Trait_Name trait_ID, string trait_Name) {
			int points = int.Parse(textBox_RegTPTotal.Text) / 4;
			int num = 0;
			msg += trait_Name + ": ";
			for (int i = 0; i < TraitsList.Count; ++i) {
				if (TraitsList[i] == trait_ID) {
					num++;
				}
			}
			int total = num * points;
			int used = 0;
			foreach (TechInfo Tech_Info in TechList.Values) {
				if (Tech_Info.app_Traits.Contains(trait_Name)) {
					used += Tech_Info.regTP;
				}
			}
			msg += used + " / " + total + '\n';
		}

		// This is only for Critical Hit, Anatomical Strike, and Quickstrike
		private void Update_CritAnatQuick_Msg() {
			string msg = "";
			int points = int.Parse(textBox_Fortune.Text) / 4;
			if (TraitsList.Contains(Traits.Trait_Name.CRIT_HIT)) {
				Print_Applied_Msg(ref msg, Traits.Trait_Name.CRIT_HIT, "Critical Hit");
			}
			if (TraitsList.Contains(Traits.Trait_Name.ANAT_STRIKE)) {
				Print_Applied_Msg(ref msg, Traits.Trait_Name.ANAT_STRIKE, "Anatomical Strike");
			}
			if (TraitsList.Contains(Traits.Trait_Name.QUICKSTRIKE)) {
				Print_Applied_Msg(ref msg, Traits.Trait_Name.QUICKSTRIKE, "Quickstrike");
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
			// Updated when a Trait is edited
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

		// --------------------------------------------------------------------------------------------
		// EVENT HANDLERS
		// --------------------------------------------------------------------------------------------

		// This only occurs once before the form is displayed for the first time.
		private void MainForm_Load(object sender, EventArgs e) {
			this.Text = "OPRP Character Builder";
			label_Title.Text = "OPRP Character Builder";
			label1.Text = "OPRP Character Builder v" + version + vers_type + " designed by Solo";

			// Check for updates of a New Version or Bug Messages
			Check_Update();

			// ------ Professions
			listView_Prof.View = View.Details;
			listView_Prof.FullRowSelect = true;

			listView_Prof.Columns.Add("Profession", 130);
			listView_Prof.Columns.Add("Mastery", 100);
			listView_Prof.Columns.Add("Description", 200);
			listView_Prof.Columns.Add("Primary Bonus", 200);

			// ------ Images
			listView_Images.View = View.Details;
			listView_Images.FullRowSelect = true;

			listView_Images.Columns.Add("Label", 150);
			listView_Images.Columns.Add("URL", 200);
			listView_Images.Columns.Add("FullRes", 30);
			listView_Images.Columns.Add("Width", 40);
			listView_Images.Columns.Add("Height", 40);

			// ------ Traits
			listView_Traits.View = View.Details;
			listView_Traits.FullRowSelect = true;

			listView_Traits.Columns.Add("Trait Name", 200);
			listView_Traits.Columns.Add("Type", 100);
			listView_Traits.Columns.Add("# Gen", 50);
			listView_Traits.Columns.Add("# Prof", 50);
			listView_Traits.Columns.Add("Description", 200);

			// ------ Special Techniques
			listView_SpTP.View = View.Details;
			listView_SpTP.FullRowSelect = true;

			listView_SpTP.Columns.Add("Sp. Trait Name", 250);
			listView_SpTP.Columns.Add("Used", 50);
			listView_SpTP.Columns.Add("Total", 50);

			// ------ Technique Table
			listView_Techniques.View = View.Details;
			listView_Techniques.FullRowSelect = true;
			label_CritAnatQuick.Text = "";

			listView_Techniques.Columns.Add("Tech Name", 250);      // 0
			listView_Techniques.Columns.Add("Rank", 50);            // 1
			listView_Techniques.Columns.Add("Reg TP", 50);          // 2
			listView_Techniques.Columns.Add("Sp. TP", 50);          // 3
			listView_Techniques.Columns.Add("Sp. Trait", 75);       // 4
			listView_Techniques.Columns.Add("Applied Traits", 150); // 5
			listView_Techniques.Columns.Add("Branched From", 100);  // 6
			listView_Techniques.Columns.Add("Type", 75);            // 7
			listView_Techniques.Columns.Add("Range", 75);           // 8
			listView_Techniques.Columns.Add("Stats", 75);           // 9
			listView_Techniques.Columns.Add("Power", 50);           // 10

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

			listView_Weaponry.Columns.Add("Name", 100);
			listView_Weaponry.Columns.Add("Description", 500);

			// ------ Items Table
			listView_Items.View = View.Details;
			listView_Items.FullRowSelect = true;

			listView_Items.Columns.Add("Name", 100);
			listView_Items.Columns.Add("Description", 500);

			// ------ Template
			richTextBox_Template.Text = Sheet.Basic_Template;

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
				textBox_Comm.Enabled = false;
				textBox_Comm.Clear();
				comboBox_MarineRank.Enabled = false;
				comboBox_MarineRank.SelectedIndex = -1;
				textBox_Threat.Enabled = false;
				textBox_Threat.Clear();
			}
			else if (affiliation == "Marine") {
				textBox_Bounty.Enabled = false;
				textBox_Bounty.Clear();
				textBox_Comm.Enabled = true;
				comboBox_MarineRank.Enabled = true;
				textBox_Threat.Enabled = false;
				textBox_Threat.Clear();
			}
			else if (affiliation == "Bounty Hunter" || affiliation == "Other") {
				textBox_Bounty.Enabled = false;
				textBox_Bounty.Clear();
				textBox_Comm.Enabled = false;
				textBox_Comm.Clear();
				comboBox_MarineRank.Enabled = false;
				comboBox_MarineRank.SelectedIndex = -1;
				textBox_Threat.Enabled = true;
			}
			else {
				textBox_Bounty.Enabled = false;
				textBox_Bounty.Clear();
				textBox_Comm.Enabled = false;
				textBox_Comm.Clear();
				comboBox_MarineRank.Enabled = false;
				comboBox_MarineRank.SelectedIndex = -1;
				textBox_Threat.Enabled = false;
				textBox_Threat.Clear();
			}
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
			ProfessionWin.NewDialog(ref listView_Prof);
		}

		private void button_ProfEdit_Click(object sender, EventArgs e) {
			// Profession "Edit" button from the MainForm
			// This is completely assuming that only one row can be selected (which we set MultiSelect = false)
			try {
				if (listView_Prof.SelectedItems.Count == 1) {
					Add_Profession ProfessionWin = new Add_Profession();
					ProfessionWin.EditDialog(ref listView_Prof);
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
					ProfList.Remove(Prof);
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
				image.SubItems[0].Text = textBox_ImageLabel.Text;
				image.SubItems.Add(textBox_ImageURL.Text);
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
				textBox_ImageLabel.Text = listView_Images.SelectedItems[0].SubItems[0].Text;
				textBox_ImageURL.Text = listView_Images.SelectedItems[0].SubItems[1].Text;
				if (listView_Images.SelectedItems[0].SubItems[2].Text == "No") {
					checkBox_FullRes.Checked = false;
					numericUpDown_Width.Value = int.Parse(listView_Images.SelectedItems[0].SubItems[3].Text);
					numericUpDown_Height.Value = int.Parse(listView_Images.SelectedItems[0].SubItems[4].Text);
				}
				else {
					checkBox_FullRes.Checked = true;
				}
				Delete_ListViewItem(ref listView_Images);
			}
		}

		private void button_ImageDelete_Click(object sender, EventArgs e) {
			// Image "Delete" button from the MainForm
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
			result = MessageBox.Show("Is your character scooping in the Blues?", "Beli Standardization", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			if (result != DialogResult.Cancel) {
				int SD = (int)numericUpDown_SDEarned.Value;
				uint beli = 500000;
				string message = "You have " + SD + " SD earned. Calculations:\n+ 500,000 (Starting Beli)";
				if (SD <= 50) {
					beli += (uint)(250000 * SD);
					message += "\n+ " + Commas_To_Value((uint)(250000 * SD)) + " (250,000/SD for first 50)";
				}
				else {
					beli += 250000 * 50;
					message += "\n+ " + Commas_To_Value((uint)(250000 * 50)) + " (250,000/SD for first 50)";
				}
				SD -= 50;
				if (result == DialogResult.No) {
					// In the GL, it's 500,000 per SD
					if (SD > 0) {
						beli += (uint)(500000 * SD);
						message += "\n+ " + Commas_To_Value((uint)(500000 * SD)) + " (500,000/SD in GL)";
					}
				}
				else {
					// In the Blues, it's 500,000 per SD
					if (SD > 0) {
						beli += (uint)(250000 * SD);
						message += "\n+ " + Commas_To_Value((uint)(250000 * SD)) + " (250,000/SD in GL)";
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
			Update_SpTrait_Table_Values();
		}

		private void textBox_UsedForStats_TextChanged(object sender, EventArgs e) {
			Update_BaseStats_Check();
		}

		#endregion


		// --------------------------------------------------------------------------------------------
		// "TRAITS" Tab
		// --------------------------------------------------------------------------------------------

		#region Traits Tab

		// Put ID as Custom if we're deleting
		private void All_Update_Functions_Traits(Traits.Trait_Name ID) {
			Update_Traits_Count_Label();
			Update_Fortune();
			Update_Strength_Final();
			Update_Stamina_Final();
			Update_Speed_Final();
			Update_Accuracy_Final();
			Update_Total_RegTP();
			Update_SpTrait_Table_Traits(ID);
			Update_SpTrait_Table_Values();
			Update_CritAnatQuick_Msg();
		}

		private void button11_TraitAdd_Click(object sender, EventArgs e) {
			// Traits "Add" button from the MainForm
			Add_Trait TraitWin = new Add_Trait();
			Traits.Trait_Name ID = TraitWin.NewDialog(ref listView_Traits);
			// And lastly all Update functions
			if (ID != Traits.Trait_Name.CUSTOM) { All_Update_Functions_Traits(ID); }
		}

		private void button_TraitsEdit_Click(object sender, EventArgs e) {
			// Traits "Edit" button from the MainForm
			// This is completely assuming that only one row can be selected (which we set MultiSelect = false)
			if (listView_Traits.SelectedItems.Count == 1) {
				Add_Trait TraitWin = new Add_Trait();
				Traits.Trait_Name ID = TraitWin.EditDialog(ref listView_Traits);
				// All corresponding Update functions (same as Add)
				if (ID != Traits.Trait_Name.CUSTOM) { All_Update_Functions_Traits(ID); }
            }
		}

		private void button10_TraitsDelete_Click(object sender, EventArgs e) {
			// Traits "Delete" button from the MainForm
			// This is completely assuming that only one row can be selected (which we set MultiSelect = false)
			if (listView_Traits.SelectedItems.Count == 0) { return; }
			string name = Delete_ListViewItem(ref listView_Traits);
			// Remove trait from TraitList
			TraitsList.Remove(Traits.get_TraitID(name));
			// All update Functions
			All_Update_Functions_Traits(Traits.Trait_Name.CUSTOM);
		}

		#endregion

		// --------------------------------------------------------------------------------------------
		// "TECHNIQUES" Tab
		// --------------------------------------------------------------------------------------------

		#region Techniques Tab

		private void All_Update_Functions_Techs() {
			Update_SpTrait_Table_Values();
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
			if (num_items == 0) { num_items++; } // What if it's empty?
			Add_Technique TechniqueWin = new Add_Technique(max_rank, listView_Traits, listView_SpTP, 
				textBox_DFName.Text, comboBox_DFType.Text, richTextBox_DFDesc.Text, textBox_DFEffect.Text);
			TechniqueWin.NewDialog(ref listView_Techniques, null, new TechInfo(), false, num_items - 1);
			// Update functions go below
			All_Update_Functions_Techs();
		}

		private void button_TechBranch_Click(object sender, EventArgs e) {
			// Technique "Branch" button from the MainForm
			if (listView_Techniques.SelectedItems.Count == 0) { return; }
			string TechName = listView_Techniques.SelectedItems[0].SubItems[0].Text;
			int index = listView_Techniques.SelectedIndices[0];
			if (!string.IsNullOrWhiteSpace(TechName)) {
				int max_rank = int.Parse(textBox_Fortune.Text) / 2;
				TechInfo Selected = TechList[TechName];
				if (Selected.Roku != Rokushiki.RokuName.NONE && !TraitsList.Contains(Traits.Trait_Name.ROK_MAST)) {
					// ^If the Selected Technique is Rokushiki and character does not have Rokushiki Master
					MessageBox.Show("You can't edit a Rokushiki Technique without the Rokushiki Master Trait!", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else if (Selected.rank + 4 > max_rank) {
					// A branched technique must have at least 4 ranks from the Technique they are branching from
					MessageBox.Show("A branched Technique must be 4 ranks higher than the Technique they are branching from\n" +
						"Branching this Technique would cause you to go over your Max Rank [" + max_rank + "]", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else {
					Add_Technique TechniqueWin = new Add_Technique(max_rank, listView_Traits, listView_SpTP,
					textBox_DFName.Text, comboBox_DFType.Text, richTextBox_DFDesc.Text, textBox_DFEffect.Text);
					TechniqueWin.NewDialog(ref listView_Techniques, TechName, Selected, true, index);
					// Update functions go below
					All_Update_Functions_Techs();
				}
			}
		}

		private void button_TechEdit_Click(object sender, EventArgs e) {
			// Technique "Edit" button from the MainForm
			if (listView_Techniques.SelectedItems.Count == 0) { return; }
			string TechName = listView_Techniques.SelectedItems[0].SubItems[0].Text;
			if (!string.IsNullOrWhiteSpace(TechName)) {
				int max_rank = int.Parse(textBox_Fortune.Text) / 2;
				TechInfo Selected = TechList[TechName];
				if (Selected.Roku != Rokushiki.RokuName.NONE && !TraitsList.Contains(Traits.Trait_Name.ROK_MAST)) {
					// ^If the Selected Technique is Rokushiki and character does not have Rokushiki Master
					MessageBox.Show("You can't edit a Rokushiki Technique without the Rokushiki Master Trait!", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else {
					Add_Technique TechniqueWin = new Add_Technique(max_rank, listView_Traits, listView_SpTP,
					textBox_DFName.Text, comboBox_DFType.Text, richTextBox_DFDesc.Text, textBox_DFEffect.Text);
					TechniqueWin.EditDialog(ref listView_Techniques, TechName, Selected);
					// Update functions go below
					All_Update_Functions_Techs();
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
					if (!string.IsNullOrWhiteSpace(TechName)) {
						TechList.Remove(TechName);
					}
					// Update functions go below
					All_Update_Functions_Techs();
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

		// --------------------------------------------------------------------------------------------
		// "TECHNIQUES" Tab
		// --------------------------------------------------------------------------------------------

		#region Template Tab

		private void button_ResetTemp_Click(object sender, EventArgs e) {
			template_imported = false;
			label_TemplateType.Text = std_template_msg;
			label_TemplateType.ForeColor = Color.Green;
			richTextBox_Template.Text = Sheet.Basic_Template;
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
			textBox_Comm.Clear();
			comboBox_MarineRank.SelectedIndex = -1;
			textBox_Threat.Clear();
			listBox_Achieve.Items.Clear();
			listView_Prof.Items.Clear();
			ProfList.Clear();
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
			// Combat
			richTextBox_Combat.Clear();
			listView_Weaponry.Items.Clear();
			listView_Items.Items.Clear();
			textBox_Beli.Clear();
			textBox_DFName.Clear();
			comboBox_DFType.SelectedIndex = -1;
			richTextBox_DFDesc.Clear();
			textBox_DFEffect.Clear();
			// Stats
			numericUpDown_SDEarned.Value = 0;
			numericUpDown_SDintoStats.Value = 0;
			for (int i = 0; i < checkedListBox1_AP.Items.Count; ++i) {
				checkedListBox1_AP.SetItemChecked(i, false);
			}
			textBox_AP.Text = "0";
			numericUpDown_UsedForFort.Value = 0;
			numericUpDown_StrengthBase.Value = 1;
			numericUpDown_SpeedBase.Value = 1;
			numericUpDown_StaminaBase.Value = 1;
			numericUpDown_AccuracyBase.Value = 1;
			// Traits & Techs
			listView_Traits.Items.Clear();
			TraitsList.Clear();
			listView_SpTP.Items.Clear();
			label_CritAnatQuick.Text = "";
			listView_Techniques.Items.Clear();
			Update_TechNum();
			TechList.Clear();
			listView_SubCat.Items.Clear();
			textBox_SubCat.Clear();
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
			Update_SpTrait_Table_Traits(Traits.Trait_Name.CUSTOM);

		}

		// Saves the form into a serialized object so that only the tool recognizes the Saved Form
		private void saveFormToProject() {
			project.SaveProject_Basic(
				textBox_CharacterName.Text,
				textBox_Nickname.Text,
				(int)numericUpDown_Age.Value,
				comboBox_Gender.Text,
				textBox_Race.Text,
				textBox_Position.Text,
				comboBox_Affiliation.Text,
				textBox_Bounty.Text,
				textBox_Comm.Text,
				comboBox_MarineRank.Text,
				textBox_Threat.Text,
				listBox_Achieve,
				listView_Prof
				);
			project.SaveProject_Physical(
				textBox_Height.Text,
				textBox_Weight.Text,
				richTextBox_Hair.Text,
				richTextBox_Eye.Text,
				richTextBox_Clothing.Text,
				richTextBox_GeneralAppear.Text,
				listView_Images
				);
			project.SaveProject_Background(
				textBox_Island.Text,
				comboBox_Region.Text,
				richTextBox_Personality.Text,
				richTextBox_History.Text
				);
			project.SaveProject_Combat(
				richTextBox_Combat.Text,
				listView_Weaponry,
				listView_Items,
				textBox_Beli.Text,
				textBox_DFName.Text,
				comboBox_DFType.Text,
				richTextBox_DFDesc.Text,
				textBox_DFEffect.Text
				);
			project.SaveProject_Stats(
				(int)numericUpDown_SDEarned.Value,
				(int)numericUpDown_SDintoStats.Value,
				checkedListBox1_AP,
				(int)numericUpDown_UsedForFort.Value,
				(int)numericUpDown_StrengthBase.Value,
				(int)numericUpDown_SpeedBase.Value,
				(int)numericUpDown_StaminaBase.Value,
				(int)numericUpDown_AccuracyBase.Value
				);
			project.SaveProject_Traits(listView_Traits);
			project.SaveProject_Tech(TechList, listView_Techniques);
		}

		// Loads from serialized object and puts it into form
		private void loadProjectToForm() {
			try {
				project.LoadProject_Basic(
			  ref textBox_CharacterName,
			  ref textBox_Nickname,
			  ref numericUpDown_Age,
			  ref comboBox_Gender,
			  ref textBox_Race,
			  ref textBox_Position,
			  ref comboBox_Affiliation,
			  ref textBox_Bounty,
			  ref textBox_Comm,
			  ref comboBox_MarineRank,
			  ref textBox_Threat,
			  ref listBox_Achieve,
			  ref listView_Prof
			  );
			}
			catch (Exception ex) { MessageBox.Show("Load Basic Character Error.\nReason: " + ex.Message); }
			try {
				project.LoadProject_Physical(
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
				project.LoadProject_Background(
			  ref textBox_Island,
			  ref comboBox_Region,
			  ref richTextBox_Personality,
			  ref richTextBox_History
			  );
			}
			catch (Exception ex) { MessageBox.Show("Load Character Background Error.\nReason: " + ex.Message); }
			try {
				project.LoadProject_Combat(
			  ref richTextBox_Combat,
			  ref listView_Weaponry,
			  ref listView_Items,
			  ref textBox_Beli,
			  ref textBox_DFName,
			  ref comboBox_DFType,
			  ref richTextBox_DFDesc,
			  ref textBox_DFEffect
			  );
			}
			catch (Exception ex) { MessageBox.Show("Load Combat and Abilities Error.\nReason: " + ex.Message); }
			try { project.LoadProject_Traits(ref listView_Traits); }
			catch (Exception ex) { MessageBox.Show("Load Traits Error.\nReason: " + ex.Message); }
			try { project.LoadProject_Tech(ref listView_Techniques); }
			catch (Exception ex) { MessageBox.Show("Load Techniques Error.\nReason: " + ex.Message); }
			// The two above need to be loaded first before Stats because some Update
			// functions aren't in an Exception Handle
			project.LoadProject_Stats(
				ref numericUpDown_SDEarned,
				ref numericUpDown_SDintoStats,
				ref checkedListBox1_AP,
				ref numericUpDown_UsedForFort,
				ref numericUpDown_StrengthBase,
				ref numericUpDown_SpeedBase,
				ref numericUpDown_StaminaBase,
				ref numericUpDown_AccuracyBase
				);
			// Some update functions in which it isn't in an Exception Handle
			Update_Traits_Count_Label();
			Update_AP_Count();
			Update_TotalSD();
			Update_Total_RegTP();
			Update_Used_RegTP();
			Update_TechNum();
			foreach (ListViewItem eachitem in listView_Traits.Items) {
				string name = eachitem.SubItems[0].Text;
				Traits.Trait_Name ID = Traits.get_TraitID(name);
				Update_SpTrait_Table_Traits(ID);
			}
			Update_SpTrait_Table_Values();
		}

		// This is where serialize our project.
		private void saveStreamProject() {
			FileStream fs = File.Open(project.location, FileMode.Create);
			BinaryFormatter formatter = new BinaryFormatter();
			try {
				formatter.Serialize(fs, project);
			}
			catch (SerializationException e) {
				MessageBox.Show("Failed to serialize.\nReason: " + e.Message);
			}
			finally {
				MessageBox.Show(project.filename + " successfully saved!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
				fs.Close();
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
				project.filename = null;
				project.location = null;
				resetForm();
			}
		}

		private void saveCharacter() {
			// If there is no filename (this means New), we save one.
			if (project.filename == null) {
				SaveFileDialog fileDialogSaveProject = new SaveFileDialog();
				fileDialogSaveProject.Filter = "OPRP files (*.oprp)|*.oprp";
				fileDialogSaveProject.Title = "Save New Project";
				fileDialogSaveProject.OverwritePrompt = true;
				if (fileDialogSaveProject.ShowDialog() == DialogResult.OK) {
					project.location = fileDialogSaveProject.FileName;
					project.filename = Path.GetFileNameWithoutExtension(fileDialogSaveProject.FileName);
					saveFormToProject();
					saveStreamProject();
					this.Text = project.filename;
				}
			}
			else {
				saveFormToProject();
				try {
					saveStreamProject();
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
						FileStream fs = File.Open(dlgFileOpen.FileName, FileMode.Open);
						BinaryFormatter formatter = new BinaryFormatter();
						project = (Project2)formatter.Deserialize(fs);
						fs.Close();
						// Holy moly you need to update the save location or else you lose work.
						project.location = dlgFileOpen.FileName;
						project.filename = Path.GetFileNameWithoutExtension(dlgFileOpen.FileName);
						resetForm();
						loadProjectToForm();
						this.Text = project.filename;
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
			CustomTags.Add(9, textBox_Comm.Text);
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
			CustomTags.Add(33, label_SDtoSPCalculations.Text);
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
			CustomTags.Add(88, textBox_Rank4.Text);
			CustomTags.Add(89, textBox_Focus.Text);
			// 90 is Tech AE
			// 91 is Image URL
			// 92 is Image Label
			// 93 is Image Width
			// 94 is Image Height
			CustomTags.Add(95, gen_cap.ToString());
			CustomTags.Add(96, prof_cap.ToString());
			// 97 is Tech Range
			CustomTags.Add(98, numericUpDown_SDintoStats.Value.ToString());
			// 99 is AP Description
		}

		private void button_Generate_Click(object sender, EventArgs e) {
			Sheet sheet = new Sheet(1, richTextBox_Template.Text);
			Load_CustomTags_Dict();
			Sheet.color_hex = textBox_Color.Text;
			sheet.Generate_Template(listBox_Achieve, listView_Prof, listView_Images, listView_Weaponry, listView_Items, checkedListBox1_AP,
				listView_Traits, listView_SpTP, listView_Techniques, listView_SubCat);
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

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
			SaveFileDialog fileDialogSaveProject = new SaveFileDialog();
			fileDialogSaveProject.Filter = "OPRP files (*.oprp)|*.oprp";
			fileDialogSaveProject.Title = "Save New Project";
			fileDialogSaveProject.OverwritePrompt = true;
			if (fileDialogSaveProject.ShowDialog() == DialogResult.OK) {
				project.location = fileDialogSaveProject.FileName;
				project.filename = Path.GetFileNameWithoutExtension(fileDialogSaveProject.FileName);
				saveFormToProject();
				saveStreamProject();
				this.Text = project.filename;
			}
		}

		private void olderVersionToolStripMenuItem_Click(object sender, EventArgs e) {
			DialogResult result = MessageBox.Show("Save your current work before importing a Character?", "Import Project",
			MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
			if (result == DialogResult.Yes) {
				saveCharacter();
			}
			if (result != DialogResult.Cancel) {
				SelectOptions Import_Window = new SelectOptions(1, false, 0);
				string selected_version = Import_Window.Import_Version_Dialog();
				if (!string.IsNullOrWhiteSpace(selected_version)) {
					OpenFileDialog dlgFileOpen = new OpenFileDialog();
					dlgFileOpen.Filter = "OPRP files (*.oprp)|*.oprp";
					dlgFileOpen.Title = "Open Project";
					dlgFileOpen.RestoreDirectory = true;
					if (dlgFileOpen.ShowDialog() == DialogResult.OK) {
						FileStream fs = File.Open(dlgFileOpen.FileName, FileMode.Open);
						BinaryFormatter formatter = new BinaryFormatter();
						// v1.1.0 (Project)
						if (selected_version == SelectOptions.ProjectStr) {
							try {
								project1 = (Project)formatter.Deserialize(fs);
								fs.Close();
								project1.location = dlgFileOpen.FileName;
								project1.filename = Path.GetFileNameWithoutExtension(dlgFileOpen.FileName);
								// Transfer from older project to newer project.
								project1.Transfer_v1010toNew(ref project);
								project.filename = null;
								project.location = null;
								resetForm();
								loadProjectToForm();
								this.Text = "OPRP Character Builder";
								MessageBox.Show("Character imported successfully!\n\nFew notes:\n" +
									"- Edit every single one of your Techniques for Effects, Range, Branching, DF Options, and new \"Traits Affect Tech\"\n" +
									"- Do not make Custom Trait names or the tool won't recognize them. Edit the custom name into the generated Sheet instead.\n" +
									"- In this new version, many dangerous coding techniques were used. Please report any bugs ASAP as it could potentially corrupt .oprp data." +
									" Please save backups as often as possible. Bugs will be MUCH MORE evident in this version.");
							}
							catch (Exception ex) {
								MessageBox.Show("Failed to import / deserialize into " + curr_proj + " due to Internal issue.\nReason: " + ex.Message);
							}
						}
						// v1.2.0 - v1.3.0 (Project2)
					}
				}
			}
		}

		private void characterTemplateToolStripMenuItem_Click(object sender, EventArgs e) {
			Import_Template();
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

		private void listOfCustomTagsToolStripMenuItem_Click(object sender, EventArgs e) {

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
}
