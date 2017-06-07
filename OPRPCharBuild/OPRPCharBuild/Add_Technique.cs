/*
 * Process should be:
 * 1) Add_Technique Ctor
 * 2) Form Load
 * 3) Copy Data to Form
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace OPRPCharBuild
{
	public partial class Add_Technique : Form
	{

		#region All Private/Public Member Variables

		private bool button_clicked;
		private int max_rank;
		private int min_rank;
		private Dictionary<string, Trait> traitsList;
		private Dictionary<string, SpTrait> spTraitsList;
        private Dictionary<string, Profession> profList;
		private const string EFFECT_LABEL_STRING = "Effect Encyclopedia\n" + 
			"- Weathermancy Effects require Weathermancy Trait\n" + 
			"- Pop Greens Effects require Horticultural Warfare Trait\n" +
			"- Stealth Effects require Assassin/Thief primary.\n" + 
            "- Doctor Effects require Doctor primary.\n" + 
            "- Carpenter Effects require Carpenter primary.";
		private int gen_effects;    // To keep track how many General Effects there currently are for Secondary Gen Effects
									// Updated when an Effect is added
									// Updated when an Effect is removed

        // Branching variables
        private bool branch;
        private bool editing;
        Technique replicating;  // This is the Technique we're either Editing or Branching off of

        // Tech List by names
        private List<string> techNames = new List<string>();

        // Effects variables
        private List<Effect> effList = new List<Effect>();

        // Stats into the Technique
        Stats techStats = new Stats();

        // Devil Fruit section
        DevilFruit DF;
        
		// This defines if the Form is being used for Rokushiki Customization.
		Rokushiki Roku = new Rokushiki();
        private int basePower = 0;
		private bool Has_RokuMaster = false;

		public Add_Technique(int MaxRank, Dictionary<string, Profession> pList, Dictionary<string, Trait> tList,
            Dictionary<string, SpTrait> SpList, DevilFruit DF_, bool branch_, bool edit_, Technique repl_) {
			InitializeComponent();
			button_clicked = false;
			max_rank = MaxRank;
			min_rank = 1;
			traitsList = tList;
			spTraitsList = SpList;
            profList = pList;
            DF = DF_;
            branch = branch_;
            editing = edit_;
            replicating = repl_;
			gen_effects = 0;
            if (traitsList.ContainsKey(Database.TR_ROKUMA)) {
                Has_RokuMaster = true;
            }
		}

		#endregion

		#region Helper Functions
        
		// This goes from the Add_Technique Form to Dictionary
		private void Add_Form_to_Dictionary(ref Dictionary<string, Technique> techList) {
			try {
				string name = textBox_Name.Text;
				// DF Options
				List<bool> DF_options = new List<bool>() {
				    checkBox_DFRank4.Checked,
				    checkBox_ZoanSig.Checked,
				    checkBox_Full.Checked,
				    checkBox_Hybrid.Checked,
				    checkBox_DFEffect.Checked
				};
				List<bool> Cyborg = new List<bool>() {
				    checkBox_Fuel1.Checked,
				    checkBox_Fuel2.Checked,
				    checkBox_Fuel3.Checked
				};
				// Now initialize TechInfo and add into TechList
				Technique techInfo = new Technique(name, Roku.name, 
                    (int)numericUpDown_Rank.Value,
					(int)numericUpDown_RegTP.Value, 
                    (int)numericUpDown_SpTP.Value,
					comboBox_AffectRank.Text, 
                    comboBox_SpTrait.Text,
                    checkBox_SigTech.Checked,
                    textBox_TechBranched.Text,
                    (int)numericUpDown_RankBranch.Value,
                    comboBox_Type.Text, 
                    comboBox_Range.Text, 
                    techStats, 
                    checkBox_AutoCalc.Checked, 
                    checkBox_NA.Checked, 
                    effList, 
                    textBox_Power.Text, 
                    DF_options, 
                    Cyborg, 
                    richTextBox_Note.Text, 
                    richTextBox_CustNotes.Text, 
                    richTextBox_Desc.Text);
				techList.Add(name, techInfo);
			}
			catch (Exception e) {
				MessageBox.Show("There was an error in adding TechInfo to TechList\nReason: " + e.Message, "Exception Thrown");
			}
		}

		// This goes from the Technique Class to Add_Technique Form
		// Take into account of Rokushiki here
		private void Copy_Data_To_Form(Technique Tech) {
			// Put the Tech being edited into the Dialog Box first. Take it from the Dictionary
			// ...This is going to suck.
			Roku = Database.getRoku(Tech.rokuName);		// Establish that this is indeed a Rokushiki
			if (Roku.name != Database.ROKU_NON) {
				// This means we're editing a Rokushiki Technique
				this.Text = "Technique Creator - [Rokushiki: " + Roku.name + "]";
				label_TechFormMsg.Visible = true;
				toolTips.Active = false;
				toolTip_Roku.Active = true;
				basePower = Roku.basePower;
				comboBox_AffectRank.Enabled = false;
			}
			textBox_Name.Text = Tech.name;
			numericUpDown_Rank.Value = Tech.rank;
			numericUpDown_RegTP.Value = Tech.regTP;
			numericUpDown_SpTP.Value = Tech.spTP;
			comboBox_SpTrait.Text = Tech.specialTrait;
			comboBox_AffectRank.Text = Tech.rankTrait;
			if (Tech.sigTech) { // Sig Tech
				numericUpDown_Rank.Value = max_rank;
				numericUpDown_Rank.Enabled = false;
				numericUpDown_RegTP.Value = 0;
				numericUpDown_SpTP.Value = 0;
				numericUpDown_RegTP.Enabled = false;
				numericUpDown_SpTP.Enabled = false;
			}
			if (string.IsNullOrWhiteSpace(Tech.branchTech)) {
				checkBox_Branched.Checked = false;
				textBox_TechBranched.Enabled = false;
				numericUpDown_RankBranch.Enabled = false;
			}
			else {
				checkBox_Branched.Checked = true;
				textBox_TechBranched.Enabled = true;
				numericUpDown_RankBranch.Enabled = true;
				textBox_TechBranched.Text = Tech.branchTech;
				numericUpDown_RankBranch.Value = Tech.branchRank;
			}
			comboBox_Type.Text = Tech.type;
			comboBox_Range.Text = Tech.range;
            // Stats
            techStats = Tech.stats;
            comboBox_StatOpt.Text = Tech.stats.statsName;
            textBox_Stats.Text = Tech.stats.getTechString();
            // Power/Effects
            checkBox_NA.Checked = Tech.NApower;
            checkBox_AutoCalc.Checked = Tech.autoCalc;
            if (!Tech.NApower) {
				// If N/A Power is not selected
				// Adding onto ListView for effects
				foreach (Effect effect in Tech.effects) {
                    // Adding onto EffectList dictionary
					effList.Add(effect);
					ListViewItem item = new ListViewItem();
					item.SubItems[0].Text = effect.name;
					item.SubItems.Add(effect.cost.ToString());
					if (effect.general) {
						item.SubItems.Add("Yes");
					}
					else {
						item.SubItems.Add("No");
					}
					listView_Effects.Items.Add(item);
                    if (effect.general) { gen_effects++; }
                }
                // Update Power and MinRank after Effects are added
                Update_Power_Value();
                Update_MinRank();
            }
            // DF Options
            if (Tech.note.Contains("Devil Fruit")) {
				checkBox_DFTechEnable.Enabled = true;
				checkBox_DFTechEnable.Checked = true;
			}
			checkBox_DFRank4.Checked = Tech.checkBoxDF[0];
			checkBox_ZoanSig.Checked = Tech.checkBoxDF[1];
			checkBox_Full.Checked = Tech.checkBoxDF[2];
			checkBox_Hybrid.Checked = Tech.checkBoxDF[3];
			checkBox_DFEffect.Checked = Tech.checkBoxDF[4];
			// Cyborg options
			checkBox_Fuel1.Checked = Tech.cyborgBoosts[0];
			checkBox_Fuel2.Checked = Tech.cyborgBoosts[1];
			checkBox_Fuel3.Checked = Tech.cyborgBoosts[2];
            // Checkboxes for the Signature Traits
            if (Tech.sigTech && checkBox_SigTech.Enabled) { checkBox_SigTech.Checked = true; }
            // Now put in the Tech.note
            richTextBox_Note.Text = Tech.note;
            // (Need Update Functions from Add_Technique_Load_Form first, and THEN edit it in)
            richTextBox_CustNotes.Text = Tech.customNote;
			richTextBox_Desc.Text = Tech.description;
			// Now proceed to edit it.
		}

        // If any Trait affects the Rank (by treating it 4 Ranks above i.e.)
        // Return true
        private bool isAffectRankTrait(string traitName) {
            if (traitName == Database.TR_MASTER || traitName == Database.TR_ADVMAS ||
                traitName == Database.TR_ADVCLA || traitName == Database.TR_STAMAS ||
                traitName == Database.TR_ADVSTA || traitName == Database.TR_ARTSTE ||
                traitName == Database.TR_ANTIST || traitName == Database.TR_DWARF) {
                return true;
            }
            return false;
        }

        // This is to simplify the function from the Database
        private Effect getEffect(string effName) {
            return Database.getEffect(effName, checkBox_Marksman.Checked,
                checkBox_Inventor.Checked, traitsList.ContainsKey(Database.TR_MASMIS));
        }

        // Returns an Effect if inside the List
        private Effect getEffInList(string effName) {
            foreach (Effect eff in effList) {
                if (eff.name == effName) {
                    return eff;
                }
            }
            return null;
        }

		#endregion

		#region Dialog Functions for Prompting Form

		public string NewDialog(ref ListView Main_Form, ref Dictionary<string, Technique> techList, int index) {
            // Save names of TechNames
            techNames.AddRange(techList.Keys.ToArray());
			this.ShowDialog();      // This calls the Add_Technique_Load function
            if (button_clicked) {
				// Add the Form into Dictionary
				Add_Form_to_Dictionary(ref techList);
				// Now add into ListView for display
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = textBox_Name.Text;              // Column 0: Tech Name
				item.SubItems.Add(numericUpDown_Rank.Value.ToString()); // Column 1: Rank
				item.SubItems.Add(numericUpDown_RegTP.Value.ToString());// Column 2: Reg TP
				item.SubItems.Add(numericUpDown_SpTP.Value.ToString()); // Column 3: Sp. TP
				item.SubItems.Add(comboBox_SpTrait.Text);               // Column 4: Sp. Trait
				item.SubItems.Add(textBox_TechBranched.Text);           // Column 5: Branched From
				item.SubItems.Add(comboBox_Type.Text);                  // Column 6: Type
				item.SubItems.Add(comboBox_Range.Text);                 // Column 7: Range
				item.SubItems.Add(textBox_Stats.Text);                  // Column 8: Stats
				item.SubItems.Add(textBox_Power.Text);                  // Column 9: Power
				if (Main_Form.Items.Count - 1 == index || Main_Form.Items.Count == 0) { Main_Form.Items.Add(item); } // That means we're inserting at the very end of the index
				else { Main_Form.Items.Insert(index + 1, item); }           // Add the entire damn thing
                return textBox_Name.Text;
			}
            return null;
		}

		// You can customize Rokushiki by Editing a Technique
		public string EditDialog(ref ListView Main_Form, ref Dictionary<string, Technique> techList) {
			button_AddTech.Text = "Edit";
			// Save copy of Technique
			Technique techInfo = techList[replicating.name];
			// Remove initial item from Dictionary first so we can avoid conflict of names
			// Why? Take a look at the Event Handler button_AddTech_Click()
			if (!techList.Remove(replicating.name)) {
				MessageBox.Show("Couldn't remove from the Dictionary because TechName was null!", "Report Bug");
			}
            // Save names of TechNames
            techNames.AddRange(techList.Keys.ToArray());
            this.ShowDialog();          // This calls the Add_Technique_Load function
            if (button_clicked) {
				// Now re-add the same item into the Dictionary
				Add_Form_to_Dictionary(ref techList);
				// Add into ListView for display
				Main_Form.SelectedItems[0].SubItems[0].Text = textBox_Name.Text;					// Column 0: Tech Name
				Main_Form.SelectedItems[0].SubItems[1].Text = numericUpDown_Rank.Value.ToString();	// Column 1: Rank
				Main_Form.SelectedItems[0].SubItems[2].Text = numericUpDown_RegTP.Value.ToString(); // Column 2: Reg TP
				Main_Form.SelectedItems[0].SubItems[3].Text = numericUpDown_SpTP.Value.ToString();  // Column 3: Sp. TP
				Main_Form.SelectedItems[0].SubItems[4].Text = comboBox_SpTrait.Text;                // Column 4: Sp. Trait
				Main_Form.SelectedItems[0].SubItems[5].Text = textBox_TechBranched.Text;            // Column 5: Branched From
				Main_Form.SelectedItems[0].SubItems[6].Text = comboBox_Type.Text;                   // Column 6: Type
				Main_Form.SelectedItems[0].SubItems[7].Text = comboBox_Range.Text;                  // Column 7: Range
				Main_Form.SelectedItems[0].SubItems[8].Text = textBox_Stats.Text;					// Column 8: Stats
				Main_Form.SelectedItems[0].SubItems[9].Text = textBox_Power.Text;					// Column 9: Power
                return textBox_Name.Text;
			}
			else {
				// This means we canceled changes, so add the Technique back.
				techList.Add(replicating.name, techInfo);
			}
            return null;
		}

		#endregion

		#region Update Functions

		private void Update_Branched_Check() {
			if (checkBox_Branched.Checked) {
				textBox_TechBranched.Enabled = true;
				numericUpDown_RankBranch.Enabled = true;
			}
			else {
				textBox_TechBranched.Enabled = false;
				textBox_TechBranched.Clear();
				numericUpDown_RankBranch.Enabled = false;
				numericUpDown_RankBranch.Value = 0;
			}
			// Used when Branched is checked/unchecked
		}

		private void Update_Note() {
			string message = "";
			// Rokushiki Message
			if (Roku.name != Database.ROKU_NON) {
				if (!checkBox_Branched.Checked) { message += "- [i]Basic " + Roku.name + "[/i]\n"; }
				else { message += "- [i]Branched " + Roku.name + "[/i]\n"; }
			}
			// Branch message
			if (checkBox_Branched.Checked) { message += "- Branched from [i]R" + numericUpDown_RankBranch.Value.ToString() + " " + textBox_TechBranched.Text + "[/i]\n"; }
			// Traits Affecting Tech
			if (checkBox_SigTech.Checked) { message += "- [bgcolor=gray][color=black][b][i]" + Database.TR_SIGTEC + "[/i][/b][/color][/bgcolor]\n"; }
			else if (checkBox_DFTechEnable.Checked) {
				message += "- [i]Devil Fruit Technique[/i]";
				if (checkBox_DFRank4.Checked) { message += " - [bgcolor=gray][color=black][b]Free R4 Tech[/b][/color][/bgcolor]"; }
				if (checkBox_ZoanSig.Checked) { message += " - [bgcolor=gray][color=black][b]Zoan Signature[/b][/color][/bgcolor]"; }
				if (checkBox_Hybrid.Checked) { message += " - [b]Hybrid Transformation[/b]"; }
				if (checkBox_Full.Checked) { message += " - [b]Full Transformation[/b]"; }
				if (checkBox_DFEffect.Checked) { message += " - [b]Free DF Effect applied[/b]"; }
				message += "\n";
			}
            // Stats names
            if (techStats.statsName == Database.BUF_WILLPO || techStats.statsName == Database.BUF_LIFRET ||
                techStats.statsName == Database.BUF_DRUG || techStats.statsName == Database.BUF_PERFOR ||
                techStats.statsName == Database.BUF_FOOD || techStats.statsName == Database.BUF_OBHAKI) {
                message += "- [i]" + techStats.statsName + " Buff[/i]\n";
            }
            else if (techStats.statsName == Database.BUF_POISON || techStats.statsName == Database.BUF_CQHAKI) {
                message += "- [i]" + techStats.statsName + " Debuff[/i]\n";
            }
            else if (techStats.statsName == Database.BUF_CRITHI || techStats.statsName == Database.BUF_ANASTR ||
                techStats.statsName == Database.BUF_QUICKS || techStats.statsName == Database.BUF_STANCE) {
                message += "- [i]" + techStats.statsName + " Technique[/i]\n";
            }
            // Stats Duration
            if (!string.IsNullOrWhiteSpace(techStats.duration)) {
                message = message.TrimEnd('\n');
                message += ", " + techStats.duration + '\n';
            }
            // Constructs Duration
            if (effList.Contains(getEffect(Database.EFF_CONST))) {
                int duration = 6;
                if (numericUpDown_Rank.Value < 14) { duration = 3; }
                else if (numericUpDown_Rank.Value < 28) { duration = 4; }
                else if (numericUpDown_Rank.Value < 44) { duration = 5; }
                message += "- " + duration + " Post Duration\n";
            }
			// Cyborg message
			if (checkBox_Fuel3.Checked) { message += "- [i]Cyborg Technique[/i] - uses 3 Fuel Charges (+12 Rank)\n"; }
			else if (checkBox_Fuel2.Checked) { message += "- [i]Cyborg Technique[/i] - uses 2 Fuel Charges (+8 Rank)\n"; }
			else if (checkBox_Fuel1.Checked) { message += "- [i]Cyborg Technique[/i] - uses 1 Fuel Charge (+4 Rank)\n"; }
			// Treated 4 Ranks higher
			if (!string.IsNullOrWhiteSpace(comboBox_AffectRank.Text)) { message += "- [i]" + comboBox_AffectRank.Text + " Technique*[/i]\n"; }
			// Special TP usage.
			if (numericUpDown_SpTP.Value > 0) { message += "- Special TP from [i]" + comboBox_SpTrait.Text + "[/i]\n"; }
			message = message.TrimEnd('\n'); // Remove the last \n
			richTextBox_Note.Text = message;
			// Used in every application above
		}

		private void Update_Power_Value() {
            if (checkBox_AutoCalc.Checked && !checkBox_NA.Checked) {
                int power = 0;
                if (Roku.name == Database.ROKU_NON) {
                    // No Rokushiki
                    power = (int)numericUpDown_Rank.Value;
                    if (isAffectRankTrait(comboBox_AffectRank.Text)) {
                        power += 4;
                    }
                    if (checkBox_Fuel3.Checked) {
                        power += 12;
                    }
                    else if (checkBox_Fuel2.Checked) {
                        power += 8;
                    }
                    else if (checkBox_Fuel1.Checked) {
                        power += 4;
                    }
                }
                else {
                    int offset = (int)(numericUpDown_Rank.Value - Roku.baseRank);
                    power = basePower + offset;
                }
                foreach (Effect effect in effList) {
                    if (!effect.general) {
                        power -= effect.cost;
                    }
                }
                textBox_Power.Text = power.ToString();
            }
            try {
                int power = int.Parse(textBox_Power.Text);
                // Flag it red if Power is below 0
                if (power < 0) {
                    textBox_Power.BackColor = Color.FromArgb(255, 128, 128);
                }
                else {
                    // if (!checkBox_NA.Checked) { textBox_Power.BackColor = SystemColors.Control; }
                    // else { textBox_Power.BackColor = SystemColors.Window; }
                    textBox_Power.BackColor = SystemColors.Window;
                }
            }
            catch {
                if (!string.IsNullOrWhiteSpace(textBox_Power.Text)) {
                    // Most likely a formatting issue. Flag it red anyways
                    textBox_Power.BackColor = Color.FromArgb(255, 128, 128);
                }
            }
			// Used when Trait Affecting Tech is changed
			// Used when Rank is changed
			// Used when an Effect is Added
			// Used when an Effect is Removed
		}

		private void Update_DFRank4() {
            int rank = (int)numericUpDown_Rank.Value;
            if (checkBox_DFRank4.Checked) {
                if (rank < 4) { numericUpDown_Rank.Value = 4; }
                int TPUsed = (int)numericUpDown_RegTP.Value;
                TPUsed -= 4;
				if (TPUsed < 0) {
					TPUsed = 0;
				}
				numericUpDown_RegTP.Value = TPUsed;
			}
			else {
				numericUpDown_RegTP.Value = rank;
			}
			// Used when DF Rank 4 is Checked
		}

		private void Update_MinRank() {
			string label = "[Min Rank: ";
			int min_cost = 0;
			int MinRank = 0;
			foreach (Effect effect in effList) {
				min_cost += effect.cost;
				if (MinRank < effect.minRank) {
					MinRank = effect.minRank;
				}
			}
			if (MinRank > min_cost) {
				label += MinRank;
				min_rank = MinRank;
			}
			else {
				label += min_cost;
				min_rank = min_cost;
			}
			label_MinRank.Text = label + ']' ;
			int curr_rank = (int)numericUpDown_Rank.Value;
			if (isAffectRankTrait(comboBox_AffectRank.Text)) {
				curr_rank += 4;
			}
			if (min_rank > curr_rank || min_cost > curr_rank) {
				label_MinRank.ForeColor = Color.Red;
			}
			else {
				label_MinRank.ForeColor = SystemColors.ControlText;
			}
			// Used when Rank value changes
			// Used when an Effect is Added
			// Used when an Effect is Removed
		}

		private void Update_Signature_Enable() {
			if (checkBox_SigTech.Checked || checkBox_ZoanSig.Checked) {
				numericUpDown_Rank.Value = max_rank;
				numericUpDown_Rank.Enabled = false;
				numericUpDown_RegTP.Value = 0;
				numericUpDown_SpTP.Value = 0;
				numericUpDown_RegTP.Enabled = false;
				numericUpDown_SpTP.Enabled = false;
			}
			else {
				numericUpDown_Rank.Enabled = true;
				numericUpDown_RegTP.Enabled = true;
				if (comboBox_SpTrait.Items.Count > 1) {
					numericUpDown_SpTP.Enabled = true;
				}
			}
			// Used when Signature Trait is Selected
			// Used when Zoan Signature is Checked
		}

		#endregion

		// This only occurs once before the form is displayed for the first time. (This occurs right when this.ShowDialog() is called)
        // The form Loading should only enable, initialize text, and add items into comboboxes!
		// Copy Data to Form should be called after everything is loaded
		private void Add_Technique_Load(object sender, EventArgs e) {
			// Set Maximum Values of NumericUpDown
			numericUpDown_Rank.Maximum = max_rank;
			label_MaxRank.Text = "[Max Rank: " + max_rank + ']';
			numericUpDown_RegTP.Maximum = max_rank;
			numericUpDown_SpTP.Maximum = max_rank;
			numericUpDown_RankBranch.Maximum = max_rank - 1;

			// Add Traits Affecting the Rank
            foreach (string traitName in traitsList.Keys) {
                if (isAffectRankTrait(traitName)) {
                    comboBox_AffectRank.Items.Add(traitName);
                }
            }
			comboBox_AffectRank.SelectedIndex = -1;

			// Add Special TP Traits
			foreach (string traitName in spTraitsList.Keys) {
                comboBox_SpTrait.Items.Add(traitName);
			}
			// Check if SpTrait_comboBox is empty (there's always 1 item which is the WhiteSpace)
			if (comboBox_SpTrait.Items.Count > 1) {
				numericUpDown_SpTP.Enabled = true;
				label_SpTraitUsed.Text = "Sp. TP Trait";
				comboBox_SpTrait.Enabled = true;
				comboBox_SpTrait.SelectedIndex = -1;
			}

            // Enable Marksman primary or Inventor primary
            if (profList.ContainsKey(Database.PROF_MS) && profList[Database.PROF_MS].primary) {
                checkBox_Marksman.Enabled = true;
            }
            if (profList.ContainsKey(Database.PROF_IN) && profList[Database.PROF_IN].primary) {
                checkBox_Inventor.Enabled = true;
            }

            // Add Stats into comboBox based on character
            comboBox_StatOpt.Items.Add(Database.BUF_WILLPO);
            if ((profList.ContainsKey(Database.PROF_WA) && profList[Database.PROF_WA].primary) ||
                (profList.ContainsKey(Database.PROF_MS) && profList[Database.PROF_MS].primary)) {
                comboBox_StatOpt.Items.Add(Database.BUF_STANCE);
            }
            if (traitsList.ContainsKey(Database.TR_LIFRET)) {
                comboBox_StatOpt.Items.Add(Database.BUF_LIFRET);
            }
            if ((profList.ContainsKey(Database.PROF_DO) && profList[Database.PROF_DO].primary) ||
                (profList.ContainsKey(Database.PROF_AS) && profList[Database.PROF_AS].primary) ||
                traitsList.ContainsKey(Database.TR_BAKBAD)) {
                comboBox_StatOpt.Items.Add(Database.BUF_POISON);
            }
            if (profList.ContainsKey(Database.PROF_DO) && profList[Database.PROF_DO].primary) {
                comboBox_StatOpt.Items.Add(Database.BUF_DRUG);
            }
            if (traitsList.ContainsKey(Database.TR_CRITHI)) {
                comboBox_StatOpt.Items.Add(Database.BUF_CRITHI);
            }
            if (traitsList.ContainsKey(Database.TR_ANASTR)) {
                comboBox_StatOpt.Items.Add(Database.BUF_ANASTR);
            }
            if (traitsList.ContainsKey(Database.TR_QUICKS)) {
                comboBox_StatOpt.Items.Add(Database.BUF_QUICKS);
            }
            if (profList.ContainsKey(Database.PROF_EN) && profList[Database.PROF_EN].primary) {
                comboBox_StatOpt.Items.Add(Database.BUF_PERFOR);
            }
            if (profList.ContainsKey(Database.PROF_CH) && profList[Database.PROF_CH].primary) {
                comboBox_StatOpt.Items.Add(Database.BUF_FOOD);
            }
            if (DF.type == "Zoan" || DF.type == "Ancient Zoan" || DF.type == "Myth Zoan") {
                comboBox_StatOpt.Items.Add(Database.BUF_ZOAN);
            }
            if (!string.IsNullOrWhiteSpace(DF.name)) {
                comboBox_StatOpt.Items.Add(Database.BUF_DFBUFF);
                comboBox_StatOpt.Items.Add(Database.BUF_DFDEBU);
            }
            if (traitsList.ContainsKey(Database.TR_AWHAKI)) {
                comboBox_StatOpt.Items.Add(Database.BUF_OBHAKI);
            }
            if (traitsList.ContainsKey(Database.TR_CQHAKI)) {
                comboBox_StatOpt.Items.Add(Database.BUF_CQHAKI);
            }

			// Fill in the ListView columns
			listView_Effects.View = View.Details;
			listView_Effects.FullRowSelect = true;
			listView_Effects.Columns.Add("Name", 100);  // Column [0]
			listView_Effects.Columns.Add("Cost", 38);   // Column [1]
			listView_Effects.Columns.Add("General", 55); // Column [2]

			// Reset back to normal Effects options (changed from Range earlier)
			comboBox_Effect.Text = "Effects";
			numericUpDown_Cost.Value = 0;
			label_EffectDesc.Text = EFFECT_LABEL_STRING;

            // Add Effects into the ComboBox based on certain criteria
            // Add Effects accessible for everyone
            List<string> effectsComboList = Database.getAccessEffects();
            // Add Stealth Effects
            if (profList.ContainsKey(Database.PROF_AS)) {
                if (profList[Database.PROF_AS].primary) {
                    effectsComboList.AddRange(Database.getStealthEffects());
                }
            }
            else if (profList.ContainsKey(Database.PROF_TH)) {
                if (profList[Database.PROF_TH].primary) {
                    effectsComboList.AddRange(Database.getStealthEffects());
                }
            }
            // Add Weathermancy
            if (traitsList.ContainsKey(Database.TR_WEATHR)) {
                effectsComboList.AddRange(Database.getWeatherEffects());
            }
            // Add Pop Greens
            if (traitsList.ContainsKey(Database.TR_HORTWA)) {
                effectsComboList.AddRange(Database.getPopGreensEffects());
            }
            // Add Doctor Effects (Primary or Secondary)
            if (profList.ContainsKey(Database.PROF_DO)) {
                effectsComboList.AddRange(Database.getDoctorEffects());
            }
            // Add Carpenter Effects (needs Carpenter primary)
            if (profList.ContainsKey(Database.PROF_CA)) {
                if (profList[Database.PROF_CA].primary) {
                    effectsComboList.AddRange(Database.getCarpenterEffects());
                }
            }
            // Add Battle Suits trait
            if (traitsList.ContainsKey(Database.TR_BATSUI)) {
                effectsComboList.Add(Database.EFF_BSUIT);
            }
            // now AddRange into the combobox
            comboBox_Effect.Items.AddRange(effectsComboList.ToArray());

			// DF Options
			try {
				if (traitsList.ContainsKey(Database.TR_SPECDF)) {
					richTextBox_DF.Text = "";
					richTextBox_DF.Text += DF.name + " [" + DF.type + "]\n\n";
					richTextBox_DF.Text += DF.description;
					if (!string.IsNullOrWhiteSpace(DF.freeEffect)) {
                        richTextBox_DF.Text += "\n\nEffect: (" + DF.freeEffect + ")";
                    }
					else { richTextBox_DF.Text += "(No T1/T2 Free Effect)"; }
					checkBox_DFTechEnable.Enabled = true;
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error in configuring DF Options.\nReason: " + ex.Message, "Error");
			}
			// Cyborg Options
			try {
				if (traitsList.ContainsKey(Database.TR_BASCYB)) {
					label_Cyborg.Text = "[Basic Cyborg]";
                    checkBox_Fuel1.Enabled = true;
				}
				else if (traitsList.ContainsKey(Database.TR_ADVCYB)) {
					label_Cyborg.Text = "[Advanced Cyborg]";
					checkBox_Fuel1.Enabled = true;
					checkBox_Fuel2.Enabled = true;
				}
				else if (traitsList.ContainsKey(Database.TR_NEWCYB)) {
					label_Cyborg.Text = "[New World Cyborg]";
					checkBox_Fuel1.Enabled = true;
					checkBox_Fuel2.Enabled = true;
					checkBox_Fuel3.Enabled = true;
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error in configuring Cyborg Options.\nReason: " + ex.Message, "Error");
			}
			// Signature Technique Trait
			if (traitsList.ContainsKey(Database.TR_SIGTEC)) { checkBox_SigTech.Enabled = true; }

            // Now everything is loaded: We can call Copy_Dict_To_Form
            // If we're branching a Technique, we want to duplicate, and then modify.
            if (branch) {
                try {
                    // TODO: Move this around properly
                    Copy_Data_To_Form(replicating);
                    // Now modify
                    textBox_Name.Clear();
                    int rank = replicating.rank;
                    checkBox_Branched.Checked = true;
                    numericUpDown_Rank.Value = rank + 4;
                    textBox_TechBranched.Text = replicating.name;
                    numericUpDown_RankBranch.Value = rank;
                }
                catch (Exception ex) {
                    MessageBox.Show("There was a problem branching Technique\nReason: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (editing) {
                try {
                    Copy_Data_To_Form(replicating);
                }
                catch (Exception ex) {
                    MessageBox.Show("There was an error copying from Dictionary to Tech Form.\nReason: " + ex.Message, "Exception Thrown");
                }
            }
        }

		#region Event Handlers

		private void button_AddTech_Click(object sender, EventArgs e) {
			// The "Add" Technique button.
			// Only want the appropriate changes to be made, so we add a bool
			int curr_rank = (int)numericUpDown_Rank.Value;
			int free_DF = 0;
			if (checkBox_DFRank4.Checked) { free_DF = 4; }
			try {
				if (isAffectRankTrait(comboBox_AffectRank.Text)) {
					curr_rank += 4;
				}
				if (string.IsNullOrWhiteSpace(textBox_Name.Text)) {
					MessageBox.Show("Technique needs a Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (techNames.Contains(textBox_Name.Text)) {
					MessageBox.Show("Can't add 2 Techniques with the same name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if (checkBox_Branched.Checked &&
					(numericUpDown_RankBranch.Value == 0 || string.IsNullOrWhiteSpace(textBox_TechBranched.Text))) {
					MessageBox.Show("Technique Branch incomplete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else if ((numericUpDown_Rank.Value - numericUpDown_RankBranch.Value) != (numericUpDown_RegTP.Value + numericUpDown_SpTP.Value + free_DF) &&
					(!checkBox_ZoanSig.Checked && !checkBox_SigTech.Checked)) {
					MessageBox.Show("TP Spent doesn't add up correctly.\n" +
						numericUpDown_Rank.Value + " (Rank) - " + numericUpDown_RankBranch.Value + " (Branch) = " + (numericUpDown_Rank.Value - numericUpDown_RankBranch.Value) + '\n' +
						numericUpDown_RegTP.Value + " (Reg TP) + " + numericUpDown_SpTP.Value + " (Sp TP) = " + (numericUpDown_RegTP.Value + numericUpDown_SpTP.Value),
						"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else {
					this.Close();
					button_clicked = true;
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error in adding Technique.\nReason: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button_ClearTech_Click(object sender, EventArgs e) {
			// "Clear" button
			DialogResult result = new DialogResult();
			result = DialogResult.No;
			result = MessageBox.Show("Are you sure you want to clear the technique?", "Clear Technique",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (result == DialogResult.Yes) {
				// Absolutely Undo all the Rokushiki things.
				this.Text = "Technique Creator";
                Roku = new Rokushiki();
				toolTip_Roku.Active = false;
				toolTips.Active = true;
				label_TechFormMsg.Visible = false;
				basePower = 0;
				comboBox_AffectRank.Enabled = true;
				// Basic Character
				textBox_Name.Clear();
				numericUpDown_Rank.Value = 1;
				comboBox_AffectRank.SelectedIndex = -1;
				checkBox_Branched.Checked = false;
				textBox_TechBranched.Clear();
				numericUpDown_RankBranch.Value = 0;
				numericUpDown_RegTP.Value = 0;
				numericUpDown_SpTP.Value = 0;
				comboBox_SpTrait.SelectedIndex = -1;
				comboBox_Type.SelectedIndex = -1;
				comboBox_Range.SelectedIndex = -1;
                // Stats
                techStats = new Stats();
                comboBox_StatOpt.SelectedIndex = -1;
                textBox_Stats.Text = "N/A";
				// Effects
				checkBox_NA.Checked = false;
				textBox_Power.Text = "0";
				listView_Effects.Items.Clear();
				effList.Clear();
				label_MinRank.Text = "Min Rank: 0";
				numericUpDown_Cost.Value = 0;
				comboBox_Effect.Text = "Effect";
				label_EffectDesc.Text = EFFECT_LABEL_STRING;
				label_EffectType.Visible = false;
				// Devil Fruit
				checkBox_DFRank4.Checked = false;
				checkBox_Full.Checked = false;
				checkBox_Hybrid.Checked = false;
				checkBox_Full.Checked = false;
                checkBox_DFRank4.Enabled = false;
                checkBox_Full.Enabled = false;
                checkBox_Hybrid.Enabled = false;
                checkBox_ZoanSig.Enabled = false;
                // App Traits
                checkBox_Fuel1.Checked = false;
                checkBox_Fuel2.Checked = false;
                checkBox_Fuel3.Checked = false;
                checkBox_SigTech.Checked = false;
                // The rest
                richTextBox_Note.Clear();
                richTextBox_CustNotes.Clear();
				richTextBox_Desc.Clear();
			}
		}

		private void checkBox_Branched_CheckedChanged(object sender, EventArgs e) {
			if (checkBox_Branched.Checked == false) {
				numericUpDown_RankBranch.Value = 0;
				textBox_TechBranched.Clear();
			}
			// Checkbox for Branched
			Update_Branched_Check();
			Update_Note();
		}

		private void textBox_TechBranched_TextChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void numericUpDown_Rank_ValueChanged(object sender, EventArgs e) {
			// Check Effects before Proceeding
			Update_MinRank();
			Update_Power_Value();
			// Set Maximum values.
			numericUpDown_RegTP.Maximum = numericUpDown_Rank.Value;
			numericUpDown_SpTP.Maximum = numericUpDown_Rank.Value;
			// This is a simple "patch", will have a real fix later
			int val = (int)(numericUpDown_Rank.Value - numericUpDown_RankBranch.Value - numericUpDown_SpTP.Value);
			if (val < 0) { numericUpDown_RegTP.Value = 0; }
			else { numericUpDown_RegTP.Value = val; }
			if (checkBox_DFRank4.Checked) { numericUpDown_RegTP.Value -= 4; }
			numericUpDown_RankBranch.Maximum = numericUpDown_Rank.Value - 1; // This is hella important
		}

		private void numericUpDown_SpTP_ValueChanged(object sender, EventArgs e) {
			// This is a simple "patch", will have a real fix later
			int val = (int)(numericUpDown_Rank.Value - numericUpDown_RankBranch.Value - numericUpDown_SpTP.Value);
			if (val < 0) { numericUpDown_RegTP.Value = 0; }
			else { numericUpDown_RegTP.Value = val; }
			Update_Note();
		}

		private void numericUpDown_RankBranch_ValueChanged(object sender, EventArgs e) {
			// This is a simple "patch", will have a real fix later
			int val = (int)(numericUpDown_Rank.Value - numericUpDown_RankBranch.Value - numericUpDown_SpTP.Value);
			if (val < 0) { numericUpDown_RegTP.Value = 0; }
			else { numericUpDown_RegTP.Value = val; }
			Update_Note();
		}

		private void comboBox_SpTrait_SelectedIndexChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void comboBox_AffectRank_SelectedIndexChanged(object sender, EventArgs e) {
			if (!string.IsNullOrWhiteSpace(comboBox_AffectRank.Text) && !label_MaxRank.Text.Contains('*')) {
				label_MaxRank.Text = label_MaxRank.Text.TrimEnd(']');
				label_MaxRank.Text += "*]";
			}
			else if (string.IsNullOrWhiteSpace(comboBox_AffectRank.Text)) {
				// Nothing selected.
				label_MaxRank.Text = label_MaxRank.Text.TrimEnd('*', ']');
				label_MaxRank.Text += ']';
			}
			// Update power from Mastery
			Update_Power_Value();
			Update_MinRank();
			Update_Note();
        }

        // Load Stats
        private void button_LoadStats_Click(object sender, EventArgs e) {
            int rank = (int)numericUpDown_Rank.Value;
            string statopt = comboBox_StatOpt.Text;
            if (Roku.name == Database.ROKU_ROK) { statopt = Database.BUF_ROKUOG; }
            if (string.IsNullOrWhiteSpace(statopt)) {
                MessageBox.Show("Stat Option not selected.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (statopt == Database.BUF_WILLPO && rank < 14) {
                MessageBox.Show("Can't have a Willpower Buff that is <R14", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Rank 1 Tech isn't allowed
            if (rank < 2) {
                MessageBox.Show("<R2 buffs/debuffs are not possible.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int power = 0;
            if (!checkBox_NA.Checked) { power = int.Parse(textBox_Power.Text); }
            string range = comboBox_Range.Text;
            Add_TechStats statsWin = new Add_TechStats(statopt, rank, power, range);
            textBox_Stats.Text = statsWin.LoadDialog(ref techStats, textBox_Stats.Text);
            Update_Note();
        }

        // Reset Stats
        private void button_ResetStats_Click(object sender, EventArgs e) {
            techStats = new Stats();
            textBox_Stats.Text = "N/A";
            Update_Note();
        }

        private void textBox_Power_TextChanged(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(textBox_Power.Text)) { return; } // Blank is fine.
            try { int.Parse(textBox_Power.Text); }
            catch {
                MessageBox.Show("Only numerical values are allowed.\nSetting Auto Calculate back on.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkBox_AutoCalc.Checked = true;
                Update_Power_Value();
            }
        }

        private void checkBox_AutoCalc_CheckedChanged(object sender, EventArgs e) {
            Update_Power_Value();
        }

        private void checkBox_NA_CheckedChanged(object sender, EventArgs e) {
			if (checkBox_NA.Checked) {
				listView_Effects.Enabled = false;
				listView_Effects.Clear();
                effList.Clear();
				button_EffectRemove.Enabled = false;
				button_AddEffect.Enabled = false;
				numericUpDown_Cost.Enabled = false;
				comboBox_Effect.Enabled = false;
				textBox_Power.Text = "";
				textBox_Power.Enabled = false;
				//textBox_Power.ReadOnly = false;
				//textBox_Power.BackColor = SystemColors.Window;
				label_MinRank.Text = "[Min Rank: 0]";
				min_rank = 0;
				label_EffectType.Visible = false;
				label_EffectDesc.Text = "(Effect Encyclopedia)";
			}
			else {
				textBox_Power.Text = numericUpDown_Rank.Value.ToString();
				//textBox_Power.ReadOnly = true;
				//textBox_Power.BackColor = SystemColors.Control;
				textBox_Power.Enabled = true;
				Update_Power_Value();
				listView_Effects.Enabled = true;
				button_EffectRemove.Enabled = true;
				button_AddEffect.Enabled = true;
				numericUpDown_Cost.Enabled = true;
				comboBox_Effect.Enabled = true;
				label_EffectType.Visible = true;
                // Reset Effects
                comboBox_Effect.Text = "Effects";
				label_EffectDesc.Text = EFFECT_LABEL_STRING;
			}
		}

		private void checkBox_DFTechEnable_CheckedChanged(object sender, EventArgs e) {
			if (checkBox_DFTechEnable.Checked) {
				checkBox_DFRank4.Enabled = true;
				if (DF.type == "Zoan" || DF.type == "Ancient Zoan" || DF.type == "Myth Zoan") {
					checkBox_ZoanSig.Enabled = true;
					checkBox_Full.Enabled = true;
					checkBox_Hybrid.Enabled = true;
				}
				if (!string.IsNullOrWhiteSpace(DF.freeEffect)) { checkBox_DFEffect.Enabled = true; }
			}
			else {
				checkBox_DFRank4.Enabled = false;
				checkBox_ZoanSig.Enabled = false;
				checkBox_Full.Enabled = false;
				checkBox_Hybrid.Enabled = false;
				checkBox_DFEffect.Enabled = false;
				checkBox_DFRank4.Checked = false;
				checkBox_ZoanSig.Checked = false;
				checkBox_Full.Checked = false;
				checkBox_Hybrid.Checked = false;
				checkBox_DFEffect.Checked = false;
			}
			Update_Note();
		}

		private void checkBox_DFRank4_CheckedChanged(object sender, EventArgs e) {
			Update_DFRank4();
			Update_Note();
		}

		private void checkBox_ZoanSig_CheckedChanged(object sender, EventArgs e) {
			Update_Signature_Enable();
			Update_Note();
		}

		private void checkBox_Full_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_Hybrid_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_DFEffect_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_Fuel1_CheckedChanged(object sender, EventArgs e) {
			Update_Power_Value();
			Update_Note();
		}

		private void checkBox_Fuel2_CheckedChanged(object sender, EventArgs e) {
			Update_Power_Value();
			Update_Note();
		}

		private void checkBox_Fuel3_CheckedChanged(object sender, EventArgs e) {
			Update_Power_Value();
			Update_Note();
		}

        private void checkBox_Marksman_CheckedChanged(object sender, EventArgs e) {
            // Checks if there's a Range loaded. Only adjust Cost
            Effect effect = getEffect(comboBox_Effect.Text);
            if (effect != null) {
                if (effect.name == Database.EFF_SHORT || effect.name == Database.EFF_MEDIU ||
                    effect.name == Database.EFF_LONG || effect.name == Database.EFF_VLONG) {
                    numericUpDown_Cost.Value = effect.cost;
                }
            }
		}

		private void checkBox_Inventor_CheckedChanged(object sender, EventArgs e) {
            // Checks if there's a AoE loaded. Only adjust Cost
            Effect effect = getEffect(comboBox_Effect.Text);
            if (effect != null) {
                if (effect.name == Database.EFF_SHAOE || effect.name == Database.EFF_MDAOE ||
                    effect.name == Database.EFF_LOAOE) {
                    numericUpDown_Cost.Value = effect.cost;
                }
            }
        }
        

		// Add into the EffectList dictionary, ListView, and Clear info
		// Make sure to check if the comboBox Effect is valid
		// And one last thing: Secondary General Effects is a *****
		private void button_AddEffect_Click(object sender, EventArgs e) {
			try {
				Effect effect = getEffect(comboBox_Effect.Text);
                if (effect == null) {
                    // Custom Effect: Add it in. Initialize everything
                    effect.name = comboBox_Effect.Text;
                    effect.minRank = (int)numericUpDown_Cost.Value;
                    effect.general = true;
                    effect.desc = "Custom";
                }
                effect.cost = (int)numericUpDown_Cost.Value;
				effList.Add(effect);
				// ListView
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = effect.name;
				item.SubItems.Add(numericUpDown_Cost.Value.ToString());
				if (effect.general) {
					item.SubItems.Add("Yes");
				}
				else {
					item.SubItems.Add("No");
				}
				listView_Effects.Items.Add(item);
				// Now check for Secondary General
				if (effect.general) {
					gen_effects++;
					if (gen_effects > 2) {
                        // That means we have to add the Secondary
						effList.Add(getEffect(Database.EFF_SECON));
						ListViewItem second = new ListViewItem();
                        second.SubItems[0].Text = Database.EFF_SECON;
						second.SubItems.Add("4");
						second.SubItems.Add("No");
						listView_Effects.Items.Add(second);
					}
				}
				// Clear info
				numericUpDown_Cost.Value = 0;
				comboBox_Effect.Text = "Effect";
				label_EffectDesc.Text = EFFECT_LABEL_STRING;
				label_EffectType.Visible = false;
				// Update functions
				Update_MinRank();
				Update_Power_Value();
                Update_Note();
			}
			catch (Exception ex) {
				MessageBox.Show("Error in adding Effects.\nReason: " + ex.Message, "Bug");
			}
		}

		// Remember this requirement: Secondary Elite Effect is ALWAYS below a General Effect
		// Also remember: EffectList.Remove(key) ONLY
		private void button_EffectRemove_Click(object sender, EventArgs e) {
			// "Remove" an Effect
			if (listView_Effects.SelectedIndices.Count > 0) {
				if (listView_Effects.SelectedItems[0].SubItems[0].Text == Database.EFF_SECON) {
					MessageBox.Show("Can't remove a Secondary General Effect cost!\nRemove a General Effect instead.", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				string effect = MainForm.Delete_ListViewItem(ref listView_Effects);
				if (!string.IsNullOrWhiteSpace(effect)) {
                    // That means we deleted an Effect from the ListView. 
                    // Now we must 1) Check for General and 2) Remove Effect from Dict.
                    try {
                        Effect remEff = getEffInList(effect);
                        if (remEff.general) {
                            if (gen_effects > 2) {
                                // If we're removing a General Effect at >2, then we have to remove Secondary
                                ListViewItem item = listView_Effects.FindItemWithText(Database.EFF_SECON);
                                listView_Effects.Items.Remove(item);                // ListView
                                effList.Remove(new Effect(Database.EFF_SECON));     // Dict
                            }
                            gen_effects--;
                        }
                        effList.Remove(remEff);
                        Update_MinRank();
                        Update_Power_Value();
                    }
                    catch {
                        MessageBox.Show("Error in EffList.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
			}
			label_EffectDesc.Text = EFFECT_LABEL_STRING;
			label_EffectDesc.ForeColor = SystemColors.ControlText;
            // Update Functions
            Update_Note();
		}

		private void comboBox_Effect_SelectedIndexChanged(object sender, EventArgs e) {
			// Due to selecting from a set List, we're going to assume everything is fine
			Effect effect = getEffect(comboBox_Effect.Text);
            if (effect != null) {
                label_EffectDesc.Text = effect.desc;
            }
            else {
                // Most likely a Custom Effect
                label_EffectDesc.Text = EFFECT_LABEL_STRING;
                return;
            }
            numericUpDown_Cost.Value = effect.cost;
			label_EffectDesc.ForeColor = SystemColors.ControlText;
			if (effect.general) {
				label_EffectType.Visible = true;
				label_EffectType.Text = "General Effect";
			}
			else {
				label_EffectType.Visible = true;
				label_EffectType.Text = "Effect requires Power";
			}

		}

		private void comboBox_Range_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBox_Range.Text == "Self") { return; }
            Effect rangeEff = getEffect(comboBox_Range.Text);
            if (rangeEff.name == Database.EFF_SHORT ||
                rangeEff.name == Database.EFF_MEDIU || rangeEff.name == Database.EFF_LONG ||
                rangeEff.name == Database.EFF_VLONG || rangeEff.name == Database.EFF_SHAOE ||
                rangeEff.name == Database.EFF_MDAOE || rangeEff.name == Database.EFF_LOAOE) {
				comboBox_Effect.Text = rangeEff.name;
				numericUpDown_Cost.Value = rangeEff.cost;
				label_EffectDesc.Text = rangeEff.desc;
				label_EffectType.Visible = true;
				label_EffectType.Text = "Effect requires Power";
			}
		}

		// Used to display the Effect description in a "Blue" Color when Selected from ListView
		private void listView_Effects_MouseClick(object sender, MouseEventArgs e) {
			try {
				string EffectName = listView_Effects.SelectedItems[0].SubItems[0].Text;
                Effect effect = getEffect(EffectName);
				label_EffectDesc.Text = effect.desc;
				label_EffectDesc.ForeColor = Color.Blue;
			}
			catch {
                // Custom Effect
				label_EffectDesc.Text = EFFECT_LABEL_STRING;
				label_EffectDesc.ForeColor = SystemColors.ControlText;
			}
		}

		private void checkBox_SigTech_CheckedChanged(object sender, EventArgs e) {
			Update_Signature_Enable();
			Update_Note();
		}
        
		// REMINDER: Rokushiki Techniques CANNOT be applied with Mastery. Therefore, it's Rank will always be what it is.
		private void button_Rokushiki_Click(object sender, EventArgs e) {
			DialogResult result = MessageBox.Show("This will overwrite some parts of the Form. Are you sure you want to Load a Rokushiki Technique?", "Reminder",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (result == DialogResult.Yes) {
				try {
					SelectOptions Roku_Window = new SelectOptions(2, Has_RokuMaster, max_rank);
					Rokushiki Sel_Roku = new Rokushiki();
					string Roku_Name = "";
					string selected_option = Roku_Window.Rokushiki_Load_Dialog(ref Sel_Roku, ref Roku_Name);
					if (!string.IsNullOrWhiteSpace(selected_option) && Sel_Roku.name != Database.ROKU_NON) {
                        // We picked a Rokushiki Technique!
                        Roku = Sel_Roku;
						if (selected_option == "Add") {
							// Close the Form and immediately add the Default Technique for Rokushiki
							this.Close();
							button_clicked = true;
							// Initialize into Form elements for transfer into Dict and ListView
							textBox_Name.Text = Roku_Name;
							numericUpDown_Rank.Value = Sel_Roku.baseRank;
							numericUpDown_RegTP.Value = Sel_Roku.baseRank;
							comboBox_AffectRank.SelectedIndex = -1;
							numericUpDown_SpTP.Value = 0;
							comboBox_SpTrait.SelectedIndex = -1;
							comboBox_Type.Text = Sel_Roku.type;
							comboBox_Range.Text = Sel_Roku.range;
							if (Roku.name == Database.ROKU_ROK) {
                                techStats = new Stats(Database.BUF_ROKUOG, "4 Post Duration", 0, 0, -22, -22);
                                textBox_Stats.Text = techStats.getTechString();
                            }
                            else {
                                textBox_Stats.Text = "N/A";
                            }
							checkBox_NA.Checked = false;
							checkBox_Branched.Checked = false;
							checkBox_DFTechEnable.Checked = false;
							checkBox_Fuel1.Checked = false;
							checkBox_Fuel2.Checked = false;
							checkBox_Fuel3.Checked = false;
							checkBox_SigTech.Checked = false;
							effList.Clear();
							listView_Effects.Items.Clear();
							textBox_Power.Text = Sel_Roku.basePower.ToString();
							richTextBox_Note.Clear();
							richTextBox_Note.Text = "- [i]Base " + Sel_Roku.name + " Technique[/i]";
                            richTextBox_CustNotes.Clear();
							richTextBox_Desc.Text = Sel_Roku.desc;
						}
						else if (selected_option == "Custom") {
							// Load the Information into the Technique Form, change "Form Type", and signify this is now Rokushiki
							this.Text = "Technique Creator - [Rokushiki: " + Sel_Roku.name + "]";
							label_TechFormMsg.Visible = true;
							toolTips.Active = false;
							toolTip_Roku.Active = true;
							basePower = Sel_Roku.basePower;
							// Now edit the Form accordingly.
							textBox_Name.Text = Roku_Name;
							numericUpDown_Rank.Value = Sel_Roku.baseRank;
							numericUpDown_RegTP.Value = Sel_Roku.baseRank;
							comboBox_Type.Text = Sel_Roku.type;
							comboBox_Range.Text = Sel_Roku.range;
                            comboBox_StatOpt.Enabled = false;
                            button_ResetStats.Enabled = false;
                            if (Roku.name == Database.ROKU_ROK) {
                                techStats = new Stats(Database.BUF_ROKUOG, "6 Post Duration", 0, 0, -22, -22);
                                textBox_Stats.Text = techStats.getTechString();
                            }
                            else {
                                textBox_Stats.Text = "N/A";
                                button_LoadStats.Enabled = false;
                            }
                            checkBox_Branched.Checked = false;
							effList.Clear();
							listView_Effects.Items.Clear();
							textBox_Power.Text = Sel_Roku.basePower.ToString();
							comboBox_AffectRank.Enabled = false;
							Update_Note();
                            richTextBox_CustNotes.Clear();
                            richTextBox_Desc.Text = Sel_Roku.desc;
						}
					}
				}
				catch (Exception ex) {
					MessageBox.Show("Error in Loading Rokushiki Tech.\nReason: " + ex.Message, "Error");
				}
			}
		}

        #endregion
    }
}
