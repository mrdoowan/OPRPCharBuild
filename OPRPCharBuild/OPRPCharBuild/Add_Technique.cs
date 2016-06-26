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
		private ListView traits_list;
		private ListView SpTraits_list;
		Traits Traitss = new Traits();
		Effects Effect = new Effects();
		private string effect_label_reset = "Effect Encyclopedia\n" + 
			"- Weathermancy Effects require Weathermancy Trait\n" + 
			"- Pop Greens Effects require Horticultural Warfare Trait\n" +
			"- Stealth Effects require Assassin/Thief primary.";
		private int gen_effects;    // To keep track how many General Effects there currently are for Secondary Gen Effects
									// Updated when an Effect is added
									// Updated when an Effect is removed
		// private bool branching;		// To signify if we are branching
		
		// Bools for primary professions
		private bool assassin_primary;
		private bool thief_primary;

		// Used when editing Dialogue for comboBox SpTrait, Rank Trait, and Note
		private string edit_SpTrait;
		private string edit_RankTrait;
		private string edit_Note;
		private string edit_power;

		// Devil Fruit section
		private string DF_name;
		private string DF_type;
		private string DF_desc;
		private string DF_effect;

		// Strings for the Applied Traits
		public const string DevFruit = "Devil Fruit";
		public const string SigTech = "Signature Technique";
		public const string RokuTech = "Rokushiki Master";
		public const string Cyborg = "Cyborg";
		public const string CritHit = "Critical Hit";
		public const string AnatStrike = "Anatomical Strike";
		public const string Quickstrike = "Quickstrike";
		public const string HakiTech = "Haki";
		public const string FormFunc = "Form and Function";
		public const string LifeRet = "Life Return";
		public const string MentFort = "Mentally Fortified";
		public const string CrowdCont = "Crowd Control";
		public const string PowSpeak = "Powerful Speaker";
		public const string BakeBad = "Baking Bad";
		public const string ExtraIngred = "That Extra Special Ingredient";

		// Item for the Dict for a ListView
		public struct EffectItem
		{
			public Effects.Effect_Name ID;
			public bool gen;
			public int cost;
			public int minRank;

			// Constructor
			public EffectItem(Effects.Effect_Name ID_, bool gen_, int cost_, int minRank_) {
				ID = ID_;
				gen = gen_;
				cost = cost_;
				minRank = minRank_;
			}
		}
		private Dictionary<string, EffectItem> EffectList = new Dictionary<string, EffectItem>();

		// This defines if the Form is being used for Rokushiki Customization.
		Rokushiki Roku = new Rokushiki();
		private Rokushiki.RokuName Roku_Form_Type;
		private int base_power; // This is only for Rokushiki Techniques. Otherwise, this is always 0.
		private bool Has_RokuMaster = false;

		public Add_Technique(int MaxRank, ListView t_list, ListView Sp_list, string DF_Name, string DF_Type, string DF_Desc, string DF_Eff) {
			InitializeComponent();
			button_clicked = false;
			//branching = false;
			max_rank = MaxRank;
			min_rank = 1;
			traits_list = t_list;
			SpTraits_list = Sp_list;
			DF_name = DF_Name;
			DF_type = DF_Type;
			DF_desc = DF_Desc;
			DF_effect = DF_Eff;
			MainForm.Set_Primary_Bool("Assassin", ref assassin_primary);
			MainForm.Set_Primary_Bool("Thief", ref thief_primary);
			gen_effects = 0;
			Roku_Form_Type = Rokushiki.RokuName.NONE;
			foreach (ListViewItem item in t_list.Items) {
				if (item.SubItems[0].Text == "Rokushiki Master") {
					Has_RokuMaster = true;
					break;
				}
			}
		}

		#endregion

		#region Helper Dialog Functions

		private void Stats_Number(CheckBox statCheckMinus, NumericUpDown statVal, ref int statInt) {
			if (statVal.Enabled) {
				if (statCheckMinus.Checked) {
					statInt = -1 * (int)statVal.Value;
				}
				else {
					statInt = (int)statVal.Value;
				}
			}
		}

		// This goes from the Add_Technique Form to Dictionary
		private void Add_Form_to_Dictionary() {
			string name = textBox_Name.Text;
			int str = 0, spe = 0, sta = 0, acc = 0;
			Stats_Number(checkBox_MinusStr, numericUpDown_Str, ref str);
			Stats_Number(checkBox_MinusSpe, numericUpDown_Spe, ref spe);
			Stats_Number(checkBox_MinusSta, numericUpDown_Sta, ref sta);
			Stats_Number(checkBox_MinusAcc, numericUpDown_Acc, ref acc);
			MainForm.TechStats Techstats = new MainForm.TechStats(str, spe, sta, acc);
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
			string App_Traits_Str = Generate_AppTraits_String(comboBox_AffectRank.Text);
			// Now initialize TechInfo and add into TechList
			MainForm.TechInfo Tech_Info = new MainForm.TechInfo(Roku_Form_Type, (int)numericUpDown_Rank.Value,
				(int)numericUpDown_RegTP.Value, (int)numericUpDown_SpTP.Value,
				comboBox_AffectRank.Text, comboBox_SpTrait.Text, App_Traits_Str, textBox_TechBranched.Text,
				(int)numericUpDown_RankBranch.Value, comboBox_Type.Text, comboBox_Range.Text,
				Techstats, checkBox_NA.Checked, EffectList, textBox_Power.Text, DF_options, Cyborg,
				richTextBox_Note.Text, richTextBox_Desc.Text);
			try { MainForm.TechList.Add(name, Tech_Info); }
			catch (Exception e) {
				MessageBox.Show("There was an error in adding Info to TechList." +
					"WARNING: This is a massive bug that could cause corruption.\n" +
					"I would highly encourage you to exit without saving.\nReason: " + e.Message, "Exception Thrown");
			}
		}

		// Helper function for all 4 stats below.
		private void From_TechStats_to_Form(int stat, ref NumericUpDown StatVal, ref CheckBox StatPlus, ref CheckBox StatMinus) {
			StatVal.Value = Math.Abs(stat);
			if (stat != 0) {
				if (stat > 0) {
					StatPlus.Checked = true;
				}
				else {
					StatMinus.Checked = true;
				}
			}
		}

		// This goes from the Dictionary to Add_Technique Form
		// Take into account of Rokushiki here
		private void Copy_Data_To_Form(string name, MainForm.TechInfo Tech) {
			// Put the Tech being edited into the Dialog Box first. Take it from the Dictionary
			// ...This is going to massively suck.
			Roku_Form_Type = Tech.Roku;						// Establish that this is indeed a Rokushiki
			if (Roku_Form_Type != Rokushiki.RokuName.NONE) {
				// This means we're editing a Rokushiki Technique
				Rokushiki.RokuInfo Info_Roku = Roku.Get_RokuInfo(Roku_Form_Type);
				this.Text = "Technique Creator - [Rokushiki: " + Info_Roku.name + "]";
				label_TechFormMsg.Visible = true;
				toolTips.Active = false;
				toolTip_Roku.Active = true;
				base_power = Info_Roku.basePower;
				comboBox_AffectRank.Enabled = false;
			}
			textBox_Name.Text = name;
			numericUpDown_Rank.Value = Tech.rank;
			numericUpDown_RegTP.Value = Tech.regTP;
			numericUpDown_SpTP.Value = Tech.spTP;
			edit_SpTrait = Tech.sp_Trait;				// (Need items initialized in comboBox FIRST, then Add_Technique Form initializes)
			edit_RankTrait = Tech.rank_Trait;           // (Need items initialized in comboBox FIRST, then Add_Technique Form initializes)
			string tech = edit_RankTrait;
			if (Traitss.get_TraitID(tech) == Traits.Trait_Name.SIG_TECH) { // Sig Tech
				numericUpDown_Rank.Value = max_rank;
				numericUpDown_Rank.Enabled = false;
				numericUpDown_RegTP.Value = 0;
				numericUpDown_SpTP.Value = 0;
				numericUpDown_RegTP.Enabled = false;
				numericUpDown_SpTP.Enabled = false;
			}
			if (string.IsNullOrWhiteSpace(Tech.tech_Branch)) {
				checkBox_Branched.Checked = false;
				textBox_TechBranched.Enabled = false;
				numericUpDown_RankBranch.Enabled = false;
			}
			else {
				checkBox_Branched.Checked = true;
				textBox_TechBranched.Enabled = true;
				numericUpDown_RankBranch.Enabled = true;
				textBox_TechBranched.Text = Tech.tech_Branch;
				numericUpDown_RankBranch.Value = Tech.rank_Branch;
			}
			comboBox_Type.Text = Tech.type;
			comboBox_Range.Text = Tech.range;
			MainForm.TechStats Stats = Tech.stats;
			From_TechStats_to_Form(Stats.str, ref numericUpDown_Str, ref checkBox_PlusStr, ref checkBox_MinusStr);
			From_TechStats_to_Form(Stats.spe, ref numericUpDown_Spe, ref checkBox_PlusSpe, ref checkBox_MinusSpe);
			From_TechStats_to_Form(Stats.sta, ref numericUpDown_Sta, ref checkBox_PlusSta, ref checkBox_MinusSta);
			From_TechStats_to_Form(Stats.acc, ref numericUpDown_Acc, ref checkBox_PlusAcc, ref checkBox_MinusAcc);
			checkBox_NA.Checked = Tech.NA_power;
			edit_power = Tech.power.ToString();				// (If Power was set customized, we want to keep that number.)
			if (!Tech.NA_power) {
				// If N/A Power is not selected
				// Adding onto ListView for effects
				foreach (string effectName in Tech.effectList.Keys) {
					EffectItem effectInfo = Tech.effectList[effectName];
					// Adding onto EffectList dictionary
					EffectList.Add(effectName, effectInfo);
					ListViewItem item = new ListViewItem();
					item.SubItems[0].Text = Effect.Get_EffectInfo(effectInfo.ID).name;
					item.SubItems.Add(effectInfo.cost.ToString());
					if (effectInfo.gen) {
						item.SubItems.Add("Yes");
					}
					else {
						item.SubItems.Add("No");
					}
					listView_Effects.Items.Add(item);
				}
			}
			// DF Options
			for (int i = 0; i < Tech.DF_checkBox.Count; ++i) {
				if (Tech.DF_checkBox[i] == true) {
					checkBox_DFTechEnable.Enabled = true;
					checkBox_DFTechEnable.Checked = true;
					break;
				}
			}
			checkBox_DFRank4.Checked = Tech.DF_checkBox[0];
			checkBox_ZoanSig.Checked = Tech.DF_checkBox[1];
			checkBox_Full.Checked = Tech.DF_checkBox[2];
			checkBox_Hybrid.Checked = Tech.DF_checkBox[3];
			checkBox_DFEffect.Checked = Tech.DF_checkBox[4];
			// Cyborg options
			checkBox_Fuel1.Checked = Tech.Cyborg_Boosts[0];
			checkBox_Fuel2.Checked = Tech.Cyborg_Boosts[1];
			checkBox_Fuel3.Checked = Tech.Cyborg_Boosts[2];
			edit_Note = Tech.note;                              // (Need Update Functions from Add_Technique_Load_Form first, and THEN edit it in)
			richTextBox_Desc.Text = Tech.desc;
			// Now proceed to edit it.
		}

		// Converting Add_Form stats into readable string
		private string Copy_Form_to_ListView_Stats() {
			string stats = "";
			int type_checked = 0;
			if (numericUpDown_Str.Enabled) {
				type_checked++;
			}
			if (numericUpDown_Spe.Enabled) {
				type_checked++;
			}
			if (numericUpDown_Sta.Enabled) {
				type_checked++;
			}
			if (numericUpDown_Acc.Enabled) {
				type_checked++;
			}
			if (type_checked == 0) {
				stats = "N/A";
			}
			else {
				if (numericUpDown_Str.Enabled) {
					if (checkBox_PlusStr.Checked) {
						stats += "+";
					}
					else {
						stats += "-";
					}
					stats += numericUpDown_Str.Value + " Str";
					type_checked--;
					if (type_checked > 0) {
						stats += ", ";
					}
				}
				if (numericUpDown_Spe.Enabled) {
					if (checkBox_PlusSpe.Checked) {
						stats += "+";
					}
					else {
						stats += "-";
					}
					stats += numericUpDown_Spe.Value + " Spe";
					type_checked--;
					if (type_checked > 0) {
						stats += ", ";
					}
				}
				if (numericUpDown_Sta.Enabled) {
					if (checkBox_PlusSta.Checked) {
						stats += "+";
					}
					else {
						stats += "-";
					}
					stats += numericUpDown_Sta.Value + " Sta";
					type_checked--;
					if (type_checked > 0) {
						stats += ", ";
					}
				}
				if (numericUpDown_Acc.Enabled) {
					if (checkBox_PlusAcc.Checked) {
						stats += "+";
					}
					else {
						stats += "-";
					}
					stats += numericUpDown_Acc.Value + " Acc";
					// Last stat, so no type_checked
				}
				// Jesus that is so repetitive ugh.
			}
			return stats;
		}

		private string Generate_AppTraits_String(string AffectRank) {
			string AppTraits = AffectRank;
			if (!string.IsNullOrWhiteSpace(AppTraits)) { AppTraits += ", "; }
			if (checkBox_DFTechEnable.Checked) { AppTraits += DevFruit + ", "; }
			if (checkBox_Fuel3.Checked || checkBox_Fuel2.Checked || checkBox_Fuel1.Checked) { AppTraits += Cyborg + ", "; }
			if (checkBox_SigTech.Checked) { AppTraits += SigTech + ", "; }
			if (checkBox_CritHit.Checked) { AppTraits += CritHit + ", "; }
			if (checkBox_AnatStrike.Checked) { AppTraits += AnatStrike + ", "; }
			if (checkBox_QuickStrike.Checked) { AppTraits += Quickstrike + ", "; }
			if (checkBox_Haki.Checked) { AppTraits += HakiTech + ", "; }
			if (checkBox_FormAndFunc.Checked) { AppTraits += FormFunc + ", "; }
			if (checkBox_LifeReturn.Checked) { AppTraits += LifeRet + ", "; }
			if (checkBox_MentFort.Checked) { AppTraits += MentFort + ", "; }
			if (checkBox_CrowdCont.Checked) { AppTraits += CrowdCont + ", "; }
			if (checkBox_PowSpeak.Checked) { AppTraits += PowSpeak + ", "; }
			if (checkBox_BakingBad.Checked) { AppTraits += BakeBad + ", "; }
			if (checkBox_SpIngred.Checked) { AppTraits += ExtraIngred + ", "; }
			// Now trim the ", "
			if (!string.IsNullOrWhiteSpace(AppTraits)) { AppTraits = AppTraits.Remove(AppTraits.Length - 2, 2); }
			return AppTraits;
		}

		private bool Has_4RanksHigher_Trait(Traits.Trait_Name ID) {
			return (ID == Traits.Trait_Name.MARTIAL_MASTERY || ID == Traits.Trait_Name.ADV_MARTIAL_MASTERY ||
				ID == Traits.Trait_Name.ADV_MARTIAL_CLASS || ID == Traits.Trait_Name.STANCE_MAST ||
				ID == Traits.Trait_Name.ADV_STANCE_MASTERY || ID == Traits.Trait_Name.ART_OF_STEALTH ||
				ID == Traits.Trait_Name.ANTI_STEALTH || ID == Traits.Trait_Name.DWARF);
        }

		#endregion

		#region Dialog Functions for Prompting Form

		public void NewDialog(ref ListView Main_Form, string TechName, MainForm.TechInfo Tech, bool branch, int index) {
			if (branch) {
				// If we're branching a Technique, we want to duplicate, and then modify.
				try {
					//branching = true;
					Copy_Data_To_Form(TechName, Tech);
					// Now edit as necessary
					textBox_Name.Clear();
					ListViewItem sel_item = Main_Form.SelectedItems[0];
					int rank = int.Parse(sel_item.SubItems[1].Text);
					checkBox_Branched.Checked = true;
                    numericUpDown_Rank.Value = rank + 1;
					textBox_TechBranched.Text = sel_item.SubItems[0].Text;
					numericUpDown_RankBranch.Value = rank;
					// Only Update Note here since we're making a Tech that's Branched.
					Update_Note();
					edit_Note = richTextBox_Note.Text;
					//branching = false;
				}
				catch (Exception e) {
					MessageBox.Show("There was a problem branching Technique\nReason: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			this.ShowDialog();
			if (button_clicked) {
				// Add into Dictionary with TechInfo
				Add_Form_to_Dictionary();
				// Now add into ListView for display
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = textBox_Name.Text;              // Column 0: Tech Name
				item.SubItems.Add(numericUpDown_Rank.Value.ToString()); // Column 1: Rank
				item.SubItems.Add(numericUpDown_RegTP.Value.ToString());// Column 2: Reg TP
				item.SubItems.Add(numericUpDown_SpTP.Value.ToString()); // Column 3: Sp. TP
				item.SubItems.Add(comboBox_SpTrait.Text);               // Column 4: Sp. Trait
				string AppTraits = Generate_AppTraits_String(comboBox_AffectRank.Text);
				item.SubItems.Add(AppTraits);                           // Column 5: App. Traits
				item.SubItems.Add(textBox_TechBranched.Text);           // Column 6: Branched From
				item.SubItems.Add(comboBox_Type.Text);                  // Column 7: Type
				item.SubItems.Add(comboBox_Range.Text);                 // Column 8: Range
				string stats = Copy_Form_to_ListView_Stats();
				item.SubItems.Add(stats);                               // Column 9: Stats
				item.SubItems.Add(textBox_Power.Text);                  // Column 10: Power
				if (Main_Form.Items.Count - 1 == index || Main_Form.Items.Count == 0) { Main_Form.Items.Add(item); } // That means we're inserting at the very end of the index
				else { Main_Form.Items.Insert(index + 1, item); }           // Add the entire damn thing
			}
		}

		// You can customize Rokushiki by Editing a Technique
		public void EditDialog(ref ListView Main_Form, string TechName, MainForm.TechInfo Tech) {
			try {
				button_AddTech.Text = "Edit";
				try { Copy_Data_To_Form(TechName, Tech); }
				catch (Exception ex) {
					MessageBox.Show("There was an error copying from Dictionary to Tech Form.\nReason: " + ex.Message, "Exception Thrown");
					return;
				}
				this.ShowDialog();
				if (button_clicked) {
					// Remove initial item from Dictionary first.
					if (!MainForm.TechList.Remove(TechName)) {
						MessageBox.Show("Couldn't remove from the Dictionary! Data could be corrupted.", "Report Bug");
					}
					// Now re-add the same item into the Dictionary
					Add_Form_to_Dictionary();
					// Add into ListView for display
					Main_Form.SelectedItems[0].SubItems[0].Text = textBox_Name.Text;					// Column 0: Tech Name
					Main_Form.SelectedItems[0].SubItems[1].Text = numericUpDown_Rank.Value.ToString();	// Column 1: Rank
					Main_Form.SelectedItems[0].SubItems[2].Text = numericUpDown_RegTP.Value.ToString(); // Column 2: Reg TP
					Main_Form.SelectedItems[0].SubItems[3].Text = numericUpDown_SpTP.Value.ToString();  // Column 3: Sp. TP
					Main_Form.SelectedItems[0].SubItems[4].Text = comboBox_SpTrait.Text;                // Column 4: Sp. Trait
					string AppTraits = Generate_AppTraits_String(comboBox_AffectRank.Text);
					Main_Form.SelectedItems[0].SubItems[5].Text = AppTraits;							// Column 5: App. Traits
					Main_Form.SelectedItems[0].SubItems[6].Text = textBox_TechBranched.Text;            // Column 6: Branched From
					Main_Form.SelectedItems[0].SubItems[7].Text = comboBox_Type.Text;                   // Column 7: Type
					Main_Form.SelectedItems[0].SubItems[8].Text = comboBox_Range.Text;                  // Column 8: Range
					string stats = Copy_Form_to_ListView_Stats();
					Main_Form.SelectedItems[0].SubItems[9].Text = stats;								// Column 9: Stats
					Main_Form.SelectedItems[0].SubItems[10].Text = textBox_Power.Text;					// Column 10: Power
				}
			}
			catch (Exception e) {
				MessageBox.Show("A technique wasn't selected.\nException Handled: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		#region Helper Functions

		// If trait is in Traits List, add it to the ComboBox. Returns true if so.
		private bool Add_Trait_comboBox(ref ComboBox combobox, Traits.Trait_Name ID, ListView TraitsList) {
			int index = Traitss.Contains_Trait_AtIndex(ID, TraitsList);
			if (index != -1) {
				string name = TraitsList.Items[index].SubItems[0].Text;
				combobox.Items.Add(name);
				return true;
			}
			return false;
		}

		// One exception to the Effect_info_load function because we're adding items into the ComboBox
		private void Add_Effect_comboBox(ref ComboBox combobox, Effects.Effect_Name ID) {
			if (Effect.Get_EffectInfo(ID).MinRank <= max_rank) {
				combobox.Items.Add(Effect.Get_EffectInfo(ID).name);
			}
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
			if (Roku_Form_Type != Rokushiki.RokuName.NONE) {
				if (!checkBox_Branched.Checked) { message += "- [i]Base " + Roku.Get_RokuInfo(Roku_Form_Type).name + " Technique[/i]"; }
			}
			// Branch message
			if (checkBox_Branched.Checked) { message += "- Branched from [i]R" + numericUpDown_RankBranch.Value.ToString() + " " + textBox_TechBranched.Text + "[/i]\n"; }
			// Traits Affecting Tech
			string tech = comboBox_AffectRank.Text;
			Traits.Trait_Name Trait_ID = Traitss.get_TraitID(tech);
			if (checkBox_SigTech.Checked) { message += "- [b][i]" + SigTech + "[/i][/b]\n"; }
			else if (checkBox_DFTechEnable.Checked) {
				message += "- [i]" + DevFruit + " Technique[/i]";
				if (checkBox_DFRank4.Checked) { message += " - [b]Free R4 Tech[/b]"; }
				if (checkBox_ZoanSig.Checked) { message += " - [b]Zoan Signature[/b]"; }
				if (checkBox_Hybrid.Checked) { message += " - [b]Hybrid Transformation[/b]"; }
				if (checkBox_Full.Checked) { message += " - [b]Full Transformation[/b]"; }
				if (checkBox_DFEffect.Checked) { message += " - [b]Free DF Effect applied[/b]"; }
				message += "\n";
			}
			// Cyborg message
			if (checkBox_Fuel3.Checked) { message += "- [i]" + Cyborg + " Technique[/i] - uses 3 Fuel Charges (+12 Rank)\n"; }
			else if (checkBox_Fuel2.Checked) { message += "- [i]" + Cyborg + " Technique[/i] - uses 2 Fuel Charges (+8 Rank)\n"; }
			else if (checkBox_Fuel1.Checked) { message += "- [i]" + Cyborg + " Technique[/i] - uses 1 Fuel Charge (+4 Rank)\n"; }
			// Treated 4 Ranks higher
			if (!string.IsNullOrWhiteSpace(comboBox_AffectRank.Text)) { message += "- [i]" + comboBox_AffectRank.Text + " Technique*[/i]\n"; }
			// Special TP usage.
			if (numericUpDown_SpTP.Value > 0) { message += "- Special TP from [i]" + comboBox_SpTrait.Text + "[/i]\n"; }
			// Any other Technique
			if (checkBox_CritHit.Checked) { message += "- [i]" + CritHit + " Technique[/i]\n"; }
			if (checkBox_AnatStrike.Checked) { message += "- [i]" + AnatStrike + " Technique[/i]\n"; }
			if (checkBox_QuickStrike.Checked) { message += "- [i]" + Quickstrike + " Technique[/i]\n"; }
			if (checkBox_Haki.Checked) { message += "- [i]" + HakiTech + " Technique[/i]\n"; }
			if (checkBox_FormAndFunc.Checked) { message += "- [i]" + FormFunc + " Technique[/i]\n"; }
			if (checkBox_LifeReturn.Checked) { message += "- [i]" + LifeRet + " Technique[/i]\n"; }
			if (checkBox_MentFort.Checked) { message += "- [i]" + MentFort + " Technique[/i]\n"; }
			if (checkBox_CrowdCont.Checked) { message += "- [i]" + CrowdCont + " Technique[/i]\n"; }
			if (checkBox_PowSpeak.Checked) { message += "- [i]" + PowSpeak + " Technique[/i]\n"; }
			if (checkBox_BakingBad.Checked) { message += "- [i]" + BakeBad + " Technique[/i]\n"; }
			if (checkBox_SpIngred.Checked) { message += "- [i]" + ExtraIngred + " Technique[/i]\n"; }
			if (!string.IsNullOrWhiteSpace(message)) { message = message.Remove(message.Length - 1, 1); } // Remove the last \n
			richTextBox_Note.Text = message;
			// Used when Sig is checked/unchecked
			// Used when Branched From text is changed.
			// Used when Branched points is changed.
			// Used when Special Trait is selected.
			// Used when Sp. TP is changed.
		}

		private void Update_Power_Value() {
			int power = 0;
			if (Roku_Form_Type == Rokushiki.RokuName.NONE) {
				power = (int)numericUpDown_Rank.Value;
				Traits.Trait_Name ID = Traitss.get_TraitID(comboBox_AffectRank.Text);
				if (Has_4RanksHigher_Trait(ID)) {
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
				int offset = (int)(numericUpDown_Rank.Value - Roku.Get_RokuInfo(Roku_Form_Type).baseRank);
				power = base_power + offset;
			}
			foreach (EffectItem effect in EffectList.Values) {
				if (!effect.gen) {
					power -= effect.cost;
				}
			}
			textBox_Power.Text = power.ToString();
			// Flag it red if Power is below 0
			if (power < 0) {
				textBox_Power.BackColor = Color.FromArgb(255, 128, 128);
			}
			else {
				// if (!checkBox_NA.Checked) { textBox_Power.BackColor = SystemColors.Control; }
				// else { textBox_Power.BackColor = SystemColors.Window; }
				textBox_Power.BackColor = SystemColors.Window;
			}
			// Used when Trait Affecting Tech is changed
			// Used when Rank is changed
			// Used when an Effect is Added
			// Used when an Effect is Removed
		}

		private void Update_DFRank4() {
			int TPUsed = (int)numericUpDown_Rank.Value;
			if (checkBox_DFRank4.Checked) {
				TPUsed -= 4;
				if (TPUsed < 0) {
					TPUsed = 0;
				}
				numericUpDown_RegTP.Value = TPUsed;
			}
			else {
				numericUpDown_RegTP.Value = TPUsed;
			}
			// Used when DF Rank 4 is Checked
		}

		private void Update_MinRank() {
			string label = "[Min Rank: ";
			int min_cost = 0;
			int MinRank = 0;
			foreach (EffectItem effect in EffectList.Values) {
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
			Traits.Trait_Name ID = Traitss.get_TraitID(comboBox_AffectRank.Text);
			if (Has_4RanksHigher_Trait(ID)) {
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
		// We'll need this to set some Maximums or comboBox lists based on the Main_Form
		private void Add_Technique_Load(object sender, EventArgs e) {
			// Set Maximum Values of NumericUpDown
			numericUpDown_Rank.Maximum = max_rank;
			label_MaxRank.Text = "[Max Rank: " + max_rank + ']';
			numericUpDown_RegTP.Maximum = max_rank;
			numericUpDown_SpTP.Maximum = max_rank;
			numericUpDown_RankBranch.Maximum = max_rank - 1;

			// Add Traits Affecting the Rank
			try {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.DWARF, traits_list);
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.ART_OF_STEALTH, traits_list);
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.ANTI_STEALTH, traits_list);
				if (!Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.ADV_STANCE_MASTERY, traits_list)) {
					Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.STANCE_MAST, traits_list);
				}
				if (!Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.ADV_MARTIAL_MASTERY, traits_list)) {
					Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.MARTIAL_MASTERY, traits_list);
				}
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.ADV_MARTIAL_CLASS, traits_list);

				if (string.IsNullOrWhiteSpace(edit_RankTrait)) { comboBox_AffectRank.SelectedIndex = -1; }
				else { comboBox_AffectRank.Text = edit_RankTrait; }
			}
			catch (Exception ex) {
				MessageBox.Show("Error in Adding Traits Affecting Rank to comboBox.\nReason: " + ex.Message, "Error");
			}

			// Add Special TP Traits
			try {
				foreach (ListViewItem eachItem in SpTraits_list.Items) {
					string name = eachItem.SubItems[0].Text;
					Add_Trait_comboBox(ref comboBox_SpTrait, Traitss.get_TraitID(name), SpTraits_list);
				}
				// Check if SpTrait_comboBox is empty (there's always 1 item which is the WhiteSpace)
				if (comboBox_SpTrait.Items.Count > 1) {
					numericUpDown_SpTP.Enabled = true;
					label_SpTraitUsed.Text = "Sp. TP Trait";
					comboBox_SpTrait.Enabled = true;
					if (string.IsNullOrWhiteSpace(edit_SpTrait)) { comboBox_SpTrait.SelectedIndex = -1; }
					else { comboBox_SpTrait.Text = edit_SpTrait; }
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error in Adding Special TP Traits to comboBox.\nReason: " + ex.Message, "Error");
			}

			// Put in power value from before
			textBox_Power.Text = edit_power;

			// Fill in the ListView columns
			listView_Effects.View = View.Details;
			listView_Effects.FullRowSelect = true;
			listView_Effects.Columns.Add("Name", 100);  // Column [0]
			listView_Effects.Columns.Add("Cost", 38);   // Column [1]
			listView_Effects.Columns.Add("General", 55); // Column [2]

			// Reset back to normal Effects options (changed from Range earlier)
			comboBox_Effect.Text = "Effects";
			numericUpDown_Cost.Value = 0;
			label_EffectDesc.Text = effect_label_reset;

			// Add ALL Effects into the ComboBox (based on max rank)
			#region Effects ComboBox
			try {
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.DISPLACE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.DISORI);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.GATLING);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.DEFLECT);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.RICO);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.UNPRED);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.DMG_TYPE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.DISARM);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SHOCK);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.CURVE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.OMNI_DI);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.HAKI_ENH);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.ELE_DMG);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.FLAV);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SPIRIT);
				// Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SECONDARY_GEN);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SPEED);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.PIERCE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.AFT_IMG);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.ADD_AFT_IMG);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.REVERSE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.DUR_DMG);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.DISABLE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SENSORY_SING);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SENSORY_MULT);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SUP_SPE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.DEF_BYP);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SPEC_BLOCK);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.START_BREAK);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.MID_BREAK);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.HIGH_BREAK);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.ARM_HAKI);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.GRAND_MAST);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.ELE_COAT);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.FULLBODY);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.START_DEF);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.MID_DEF);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.HIGH_DEF);
				/* Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.MELEE_RANGE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SHORT_RANGE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.MED_RANGE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.LONG_RANGE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.V_LONG_RANGE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SHORT_AOE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.MEDIUM_AOE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.LONG_AOE);*/
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.WEATHER, traits_list) != -1) {
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.CLOUD);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.RAIN);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.FOG);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.ELE_DMG_WEAT);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.WIND);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.MILK);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.MIR_CLONE);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.MIR_CAMO);
				}
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.HORT_WAR, traits_list) != -1) {
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SENTIENCE);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.FLORAL);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.WOOD_DEF);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.ELE_DMG_POP);
				}
				if (assassin_primary || thief_primary) {
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SMOKE);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.CROWD_BLEND);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SILENT);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.SCENTLESS);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.DISGUISE);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.PICKPOCK);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.NAT_CAMO);
					Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.OPEN_CAMO);
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error in Effects to comboBox.\nReason: " + ex.Message, "Error");
			}
			// DF Options
			try {
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.DEV_FRUIT, traits_list) != -1) {
					label_DF.Text = "";
					label_DF.Text += DF_name + " [" + DF_type + "]\n";
					label_DF.Text += DF_desc + '\n';
					if (!string.IsNullOrWhiteSpace(DF_effect)) { label_DF.Text += "(" + DF_effect + ")"; }
					else { label_DF.Text += "(No T1/T2 Free Effect)"; }
					checkBox_DFTechEnable.Enabled = true;
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error in configuring DF Options.\nReason: " + ex.Message, "Error");
			}
			// Cyborg Options
			try {
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.BAS_CYBORG, traits_list) != -1) {
					label_Cyborg.Text = "[Basic Cyborg]";
					checkBox_SigTech.Enabled = true;
				}
				else if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.ADV_CYBORG, traits_list) != -1) {
					label_Cyborg.Text = "[Advanced Cyborg]";
					checkBox_Fuel1.Enabled = true;
					checkBox_Fuel2.Enabled = true;
				}
				else if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.NW_CYBORG, traits_list) != -1) {
					label_Cyborg.Text = "[New World Cyborg]";
					checkBox_Fuel1.Enabled = true;
					checkBox_Fuel2.Enabled = true;
					checkBox_Fuel3.Enabled = true;
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error in configuring Cyborg Options.\nReason: " + ex.Message, "Error");
			}
			// Applicable Traits & Note
			try {
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.SIG_TECH, traits_list) != -1) { checkBox_SigTech.Enabled = true; }
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.CRIT_HIT, traits_list) != -1) { checkBox_CritHit.Enabled = true; }
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.ANAT_STRIKE, traits_list) != -1) { checkBox_AnatStrike.Enabled = true; }
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.QUICKSTRIKE, traits_list) != -1) { checkBox_QuickStrike.Enabled = true; }
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.AWAKE_HAKI, traits_list) != -1 ||
					Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.DISC_HAKI, traits_list) != -1 ||
					Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.CONQ_HAKI, traits_list) != -1) { checkBox_Haki.Enabled = true; }
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.F_AND_F, traits_list) != -1) { checkBox_FormAndFunc.Enabled = true; }
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.LIFE_RET, traits_list) != -1) { checkBox_LifeReturn.Enabled = true; }
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.MENT_FORT, traits_list) != -1) { checkBox_MentFort.Enabled = true; }
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.EXTRA_INGRED, traits_list) != -1) { checkBox_SpIngred.Enabled = true; }
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.CROWD_CONT, traits_list) != -1) { checkBox_CrowdCont.Enabled = true; }
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.POW_SPEAK, traits_list) != -1) { checkBox_PowSpeak.Enabled = true; }
				if (Traitss.Contains_Trait_AtIndex(Traits.Trait_Name.BAKING_BAD, traits_list) != -1) { checkBox_BakingBad.Enabled = true; }
				// Because "Note" is something we need to initialize before the Checks, we save a copy and load it here.
				richTextBox_Note.Text = edit_Note;
				// Now we check the Box if "Note" contains the String
				if (string.IsNullOrWhiteSpace(edit_Note)) { edit_Note = ""; } // To prevent unhandled Exception
				if (edit_Note.Contains(SigTech) && checkBox_SigTech.Enabled) { checkBox_SigTech.Checked = true; }
				if (edit_Note.Contains(CritHit) && checkBox_CritHit.Enabled) { checkBox_CritHit.Checked = true; }
				if (edit_Note.Contains(AnatStrike) && checkBox_AnatStrike.Enabled) { checkBox_AnatStrike.Checked = true; }
				if (edit_Note.Contains(Quickstrike) && checkBox_QuickStrike.Enabled) { checkBox_QuickStrike.Checked = true; }
				if (edit_Note.Contains(HakiTech) && checkBox_Haki.Enabled) { checkBox_Haki.Checked = true; }
				if (edit_Note.Contains(FormFunc) && checkBox_FormAndFunc.Enabled) { checkBox_FormAndFunc.Checked = true; }
				if (edit_Note.Contains(LifeRet) && checkBox_LifeReturn.Enabled) { checkBox_LifeReturn.Checked = true; }
				if (edit_Note.Contains(MentFort) && checkBox_MentFort.Enabled) { checkBox_MentFort.Checked = true; }
				if (edit_Note.Contains(ExtraIngred) && checkBox_SpIngred.Enabled) { checkBox_SpIngred.Checked = true; }
				if (edit_Note.Contains(CrowdCont) && checkBox_CrowdCont.Enabled) { checkBox_CrowdCont.Checked = true; }
				if (edit_Note.Contains(PowSpeak) && checkBox_PowSpeak.Enabled) { checkBox_PowSpeak.Checked = true; }
				if (edit_Note.Contains(BakeBad) && checkBox_BakingBad.Enabled) { checkBox_BakingBad.Checked = true; }
			}
			catch (Exception ex) {
				MessageBox.Show("Error in configuring Applicable Traits.\nReason: " + ex.Message, "Error");
			}
			#endregion
		}

		#region Event Handlers

		// Check if a string has all numbers
		private bool All_Numbers(string power) {
			for (int i = 0; i < power.Length; ++i) {
				if (!char.IsNumber(power[i])) {
					return false;
				}
			}
			return true;
		}

		private void button_AddTech_Click(object sender, EventArgs e) {
			// The "Add" Technique button.
			// Only want the appropriate changes to be made, so we add a bool
			int curr_rank = (int)numericUpDown_Rank.Value;
			int free_DF = 0;
			if (checkBox_DFRank4.Checked) { free_DF = 4; }
			Traits.Trait_Name ID = Traitss.get_TraitID(comboBox_AffectRank.Text);
			if (Has_4RanksHigher_Trait(ID)) {
				curr_rank += 4;
			}
			if (string.IsNullOrWhiteSpace(textBox_Name.Text)) {
				MessageBox.Show("Technique needs a Name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (MainForm.TechList.ContainsKey(textBox_Name.Text) && button_AddTech.Text == "Add") {
				MessageBox.Show("Can't add 2 Techniques with the same name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (checkBox_Branched.Checked &&
				(numericUpDown_RankBranch.Value == 0 || string.IsNullOrWhiteSpace(textBox_TechBranched.Text))) {
				MessageBox.Show("Technique Branch incomplete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if ((numericUpDown_Rank.Value - numericUpDown_RankBranch.Value) != (numericUpDown_RegTP.Value + numericUpDown_SpTP.Value + free_DF)) {
				MessageBox.Show("TP Spent doesn't add up correctly.\n" + 
					numericUpDown_Rank.Value + " (Rank) - " + numericUpDown_RankBranch.Value + " (Branch) = " + (numericUpDown_Rank.Value - numericUpDown_RankBranch.Value) + '\n' +
					numericUpDown_RegTP.Value + " (Reg TP) + " + numericUpDown_SpTP.Value + " (Sp TP) = " + (numericUpDown_RegTP.Value + numericUpDown_SpTP.Value),
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (string.IsNullOrWhiteSpace(comboBox_Type.Text)) {
				MessageBox.Show("Technique needs a Type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (string.IsNullOrWhiteSpace(comboBox_Range.Text)) {
				MessageBox.Show("Technique needs a Range.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (int.Parse(textBox_Power.Text) < 0) {
				MessageBox.Show("Power is below 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (!All_Numbers(textBox_Power.Text)) {
				MessageBox.Show("Power should only have numbers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (min_rank > curr_rank) {
				MessageBox.Show("Effect costs are ineligible at its current rank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else {
				this.Close();
				button_clicked = true;
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
				Roku_Form_Type = Rokushiki.RokuName.NONE;
				toolTip_Roku.Active = false;
				toolTips.Active = true;
				label_TechFormMsg.Visible = false;
				base_power = 0;
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
				checkBox_PlusStr.Checked = false;
				checkBox_MinusStr.Checked = false;
				checkBox_PlusSta.Checked = false;
				checkBox_MinusSta.Checked = false;
				checkBox_PlusSta.Checked = false;
				checkBox_MinusSta.Checked = false;
				checkBox_PlusAcc.Checked = false;
				checkBox_MinusAcc.Checked = false;
				numericUpDown_Str.Value = 0;
				numericUpDown_Spe.Value = 0;
				numericUpDown_Sta.Value = 0;
				numericUpDown_Acc.Value = 0;
				// Effects
				checkBox_NA.Checked = false;
				textBox_Power.Text = "1";
				listView_Effects.Items.Clear();
				EffectList.Clear();
				label_MinRank.Text = "Min Rank: 0";
				numericUpDown_Cost.Value = 0;
				comboBox_Effect.Text = "Effect";
				label_EffectDesc.Text = effect_label_reset;
				label_EffectType.Visible = false;
				// Devil Fruit
				checkBox_DFRank4.Checked = false;
				checkBox_Full.Checked = false;
				checkBox_Hybrid.Checked = false;
				checkBox_Full.Checked = false;
				label_DF.Text = "No Devil Fruit Option";
				checkBox_DFRank4.Enabled = false;
				checkBox_Full.Enabled = false;
				checkBox_Hybrid.Enabled = false;
				checkBox_ZoanSig.Enabled = false;
				// The rest
				richTextBox_Note.Clear();
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

		private void numericUpDown_RankBranch_ValueChanged(object sender, EventArgs e) {
			numericUpDown_RegTP.Value = numericUpDown_Rank.Value - numericUpDown_RankBranch.Value - numericUpDown_SpTP.Value;
			Update_Note();
		}

		private void numericUpDown_SpTP_ValueChanged(object sender, EventArgs e) {
			// For simplicity sake, calculate regTP used for them.
			numericUpDown_RegTP.Value = numericUpDown_Rank.Value - numericUpDown_SpTP.Value - numericUpDown_RankBranch.Value;
            Update_Note();
		}

		private void comboBox_SpTrait_SelectedIndexChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void numericUpDown_Rank_ValueChanged(object sender, EventArgs e) {
			// Check Effects before Proceeding
			Update_MinRank();
			Update_Power_Value();
			// Set Maximum values.
			numericUpDown_RegTP.Maximum = numericUpDown_Rank.Value;
			numericUpDown_RegTP.Value = numericUpDown_Rank.Value - numericUpDown_RankBranch.Value;
			if (checkBox_DFRank4.Checked) { numericUpDown_RegTP.Value -= 4; }
			numericUpDown_SpTP.Maximum = numericUpDown_Rank.Value;
			numericUpDown_RankBranch.Maximum = numericUpDown_Rank.Value - 1; // This is hella important
		}

		private void comboBox_AffectRank_SelectedIndexChanged(object sender, EventArgs e) {
			if (string.IsNullOrWhiteSpace(comboBox_AffectRank.Text)) { label_PowerNote.Visible = false; }
			else { label_PowerNote.Visible = true; }
			// Update power from Mastery
			Update_Power_Value();
			Update_MinRank();
			Update_Note();
        }

		// This is to avoid tedious repetition for when Stat buttons are pressed.
		private void Button_Stat(ref CheckBox changed_box, ref CheckBox other_box, ref NumericUpDown stat) {
			if (changed_box.Checked) {
				// When the button is checked.
				if (other_box.Checked) {
					// When the opposite button is checked, we want to uncheck it.
					other_box.Checked = false;
				}
				else {
					// This implies that both buttons weren't pressed previously.
					stat.Enabled = true;
				}
			}
			else if (!other_box.Checked) {
				// When the button is unchecked, it means no longer needed.
				stat.Enabled = false;
				stat.Value = 0;
			}
		}

		private void checkBox_PlusStr_CheckedChanged(object sender, EventArgs e) {
			Button_Stat(ref checkBox_PlusStr, ref checkBox_MinusStr, ref numericUpDown_Str);
		}

		private void checkBox_MinusStr_CheckedChanged(object sender, EventArgs e) {
			Button_Stat(ref checkBox_MinusStr, ref checkBox_PlusStr, ref numericUpDown_Str);
		}

		private void checkBox_PlusSpe_CheckedChanged(object sender, EventArgs e) {
			Button_Stat(ref checkBox_PlusSpe, ref checkBox_MinusSpe, ref numericUpDown_Spe);
		}

		private void checkBox_MinusSpe_CheckedChanged(object sender, EventArgs e) {
			Button_Stat(ref checkBox_MinusSpe, ref checkBox_PlusSpe, ref numericUpDown_Spe);
		}

		private void checkBox_PlusSta_CheckedChanged(object sender, EventArgs e) {
			Button_Stat(ref checkBox_PlusSta, ref checkBox_MinusSta, ref numericUpDown_Sta);
		}

		private void checkBox_MinusSta_CheckedChanged(object sender, EventArgs e) {
			Button_Stat(ref checkBox_MinusSta, ref checkBox_PlusSta, ref numericUpDown_Sta);
		}

		private void checkBox_PlusAcc_CheckedChanged(object sender, EventArgs e) {
			Button_Stat(ref checkBox_PlusAcc, ref checkBox_MinusAcc, ref numericUpDown_Acc);
		}

		private void checkBox_MinusAcc_CheckedChanged(object sender, EventArgs e) {
			Button_Stat(ref checkBox_MinusAcc, ref checkBox_PlusAcc, ref numericUpDown_Acc);
		}

		private void checkBox_NA_CheckedChanged(object sender, EventArgs e) {
			if (checkBox_NA.Checked) {
				listView_Effects.Enabled = false;
				listView_Effects.Clear();
				button_EffectRemove.Enabled = false;
				button_AddEffect.Enabled = false;
				numericUpDown_Cost.Enabled = false;
				comboBox_Effect.Enabled = false;
				textBox_Power.Text = "0";
				//textBox_Power.ReadOnly = false;
				//textBox_Power.BackColor = SystemColors.Window;
				label_MinRank.Text = "Min Rank: 1";
				min_rank = 1;
				label_EffectType.Visible = false;
				label_EffectDesc.Text = effect_label_reset;
			}
			else {
				textBox_Power.Text = numericUpDown_Rank.Value.ToString();
				//textBox_Power.ReadOnly = true;
				//textBox_Power.BackColor = SystemColors.Control;
				listView_Effects.Enabled = true;
				button_EffectRemove.Enabled = true;
				button_AddEffect.Enabled = true;
				numericUpDown_Cost.Enabled = true;
				comboBox_Effect.Enabled = true;
				label_EffectType.Visible = true;
				// Fill in Effect Description if there's one pending
				Effects.Effect_Name ID = Effect.Get_EffectID(comboBox_Effect.Text);
				if (ID != Effects.Effect_Name.NONE) { label_EffectDesc.Text = Effect.Get_EffectInfo(ID).desc; }
				else { label_EffectDesc.Text = effect_label_reset; }
			}
		}

		private void checkBox_DFTechEnable_CheckedChanged(object sender, EventArgs e) {
			if (checkBox_DFTechEnable.Checked) {
				checkBox_DFRank4.Enabled = true;
				if (DF_type == "Zoan" || DF_type == "Ancient Zoan" || DF_type == "Myth Zoan") {
					checkBox_ZoanSig.Enabled = true;
					checkBox_Full.Enabled = true;
					checkBox_Hybrid.Enabled = true;
				}
				if (!string.IsNullOrWhiteSpace(DF_effect)) { checkBox_DFEffect.Enabled = true; }
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

		// Returns false if we come across a problem.
		// Returns true if successfully added.
		// This adds a specified Effect to our Dict
		private bool Add_to_EffectList(Effects.Effect_Name ID, int cost) {
			Effects.EffectInfo effect = Effect.Get_EffectInfo(ID);
			try { EffectList.Add(effect.name, new EffectItem(ID, effect.general, cost, effect.MinRank)); }
			catch (Exception ex) {
				// Exception thrown for a Duplicate key. If null, then that's a problem.
				if (ex is ArgumentNullException) {
					MessageBox.Show("Error in adding a null Effects.\nReason: " + ex.Message, "Exception Thrown");
					return false;
				}
				else {
					int i = 2;
					// What we're doing here is trying to give this a unique key by adding a number that increments
					// Infinite Loop danger btw if EffectList.Add keeps adding a null
					while (true) {
						try {
							string new_name = Effect.Get_EffectInfo(ID).name + i.ToString();
							EffectList.Add(new_name, new EffectItem(ID, effect.general, cost, effect.MinRank));
							break;
						}
						catch {
							i++;
						}
					}
				}
			}
			return true;
		}

		// Add into the EffectList dictionary, ListView, and Clear info
		// Make sure to check if the comboBox Effect is valid
		// And one last thing: Secondary General Effects is a *****
		private void button_AddEffect_Click(object sender, EventArgs e) {
			try {
				Effects.Effect_Name ID = Effect.Get_EffectID(comboBox_Effect.Text);
				if (ID == Effects.Effect_Name.NONE) {
					MessageBox.Show("Effect name is not legit. Please pick a proper Effects.", "Error");
				}
				else {
					// Dict Data Structure
					Add_to_EffectList(ID, (int)numericUpDown_Cost.Value);
					// ListView
					ListViewItem item = new ListViewItem();
					item.SubItems[0].Text = Effect.Get_EffectInfo(ID).name;
					item.SubItems.Add(numericUpDown_Cost.Value.ToString());
					if (Effect.Get_EffectInfo(ID).general) {
						item.SubItems.Add("Yes");
					}
					else {
						item.SubItems.Add("No");
					}
					listView_Effects.Items.Add(item);
					// Now check for Secondary General
					if (Effect.Get_EffectInfo(ID).general) {
						gen_effects++;
						if (gen_effects > 2) {
							// That means we have to add the Secondary
							Add_to_EffectList(Effects.Effect_Name.SECONDARY_GEN, 4);
							ListViewItem second = new ListViewItem();
							second.SubItems[0].Text = Effect.Get_EffectInfo(Effects.Effect_Name.SECONDARY_GEN).name;
							second.SubItems.Add("4");
							second.SubItems.Add("No");
							listView_Effects.Items.Add(second);
						}
					}
					// Clear info
					numericUpDown_Cost.Value = 0;
					comboBox_Effect.Text = "Effect";
					label_EffectDesc.Text = effect_label_reset;
					label_EffectType.Visible = false;
					// Update functions
					Update_MinRank();
					Update_Power_Value();
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error in adding Effects.\nReason: " + ex.Message, "Bug");
			}
		}

		// Returns the Key containing the ID
		private string EffectList_Key(Effects.Effect_Name ID) {
			try { return EffectList.FirstOrDefault(x => x.Value.ID == ID).Key; }
			catch (Exception e) {
				MessageBox.Show("Could not find Key in Effect List\nReason: " + e.Message, "Exception Thrown");
				return "";
			}
		}

		// Remember this requirement: Secondary Elite Effect is ALWAYS below a General Effect
		// Also remember: EffectList.Remove(key) ONLY
		private void button_EffectRemove_Click(object sender, EventArgs e) {
			// "Remove" an Effect
			if (listView_Effects.SelectedIndices.Count > 0) {
				if (listView_Effects.SelectedItems[0].SubItems[0].Text == Effect.Get_EffectInfo(Effects.Effect_Name.SECONDARY_GEN).name) {
					MessageBox.Show("Can't remove a Secondary General Effect cost!\nRemove a General Effect instead.", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				string effect = MainForm.Delete_ListViewItem(ref listView_Effects);
				if (!string.IsNullOrWhiteSpace(effect)) {
					// That means we deleted an Effect from the ListView. 
					// Now we must 1) Check for General, and then Remove Effect from Dict.
					Effects.Effect_Name ID = Effect.Get_EffectID(effect);
					string key = EffectList_Key(ID); // This should only be used to remove
					if (Effect.Get_EffectInfo(ID).general) {
						if (gen_effects > 2) {
							// If we're removing a General Effect at >2, then we have to remove Secondary
							string secondary = Effect.Get_EffectInfo(Effects.Effect_Name.SECONDARY_GEN).name;
							ListViewItem item = listView_Effects.FindItemWithText(secondary);
							listView_Effects.Items.Remove(item);    // ListView
							string secondary_key = EffectList_Key(Effects.Effect_Name.SECONDARY_GEN);
							EffectList.Remove(secondary_key);       // Dict
						}
						gen_effects--;
					}
					EffectList.Remove(key);
					Update_MinRank();
					Update_Power_Value();
				}
			}
			label_EffectDesc.Text = effect_label_reset;
			label_EffectDesc.ForeColor = SystemColors.ControlText;
		}
		private void comboBox_Effect_SelectedIndexChanged(object sender, EventArgs e) {
			// Due to selecting from a set List, we're going to assume everything is fine
			Effects.Effect_Name ID = Effect.Get_EffectID(comboBox_Effect.Text);
			numericUpDown_Cost.Value = Effect.Get_EffectInfo(ID).cost;
			if (ID != Effects.Effect_Name.NONE) { label_EffectDesc.Text = Effect.Get_EffectInfo(ID).desc; }
			else { label_EffectDesc.Text = effect_label_reset; }
			label_EffectDesc.ForeColor = SystemColors.ControlText;
			if (Effect.Get_EffectInfo(ID).general) {
				label_EffectType.Visible = true;
				label_EffectType.Text = "General Effect";
			}
			else {
				label_EffectType.Visible = true;
				label_EffectType.Text = "Effect requires Power";
			}

		}

		private void comboBox_Range_SelectedIndexChanged(object sender, EventArgs e) {
			Effects.Effect_Name ID = Effect.Get_EffectID(comboBox_Range.Text);
			if (ID == Effects.Effect_Name.MELEE_RANGE || ID == Effects.Effect_Name.SHORT_RANGE ||
				ID == Effects.Effect_Name.MED_RANGE || ID == Effects.Effect_Name.LONG_RANGE ||
				ID == Effects.Effect_Name.V_LONG_RANGE || ID == Effects.Effect_Name.SHORT_AOE ||
				ID == Effects.Effect_Name.MEDIUM_AOE || ID == Effects.Effect_Name.LONG_AOE) {
				comboBox_Effect.Text = Effect.Get_EffectInfo(ID).name;
				numericUpDown_Cost.Value = Effect.Get_EffectInfo(ID).cost;
				label_EffectDesc.Text = Effect.Get_EffectInfo(ID).desc;
				label_EffectType.Visible = true;
				label_EffectType.Text = "Effect requires Power";
			}
		}

		// Used to display the Effect description in a "Blue" Color when Selected from ListView
		private void listView_Effects_MouseClick(object sender, MouseEventArgs e) {
			try {
				string EffectName = listView_Effects.SelectedItems[0].SubItems[0].Text;
				Effects.Effect_Name ID = Effect.Get_EffectID(EffectName);
				label_EffectDesc.Text = Effect.Get_EffectInfo(ID).desc;
				label_EffectDesc.ForeColor = Color.Blue;
			}
			catch {
				label_EffectDesc.Text = effect_label_reset;
				label_EffectDesc.ForeColor = SystemColors.ControlText;
			}
		}

		private void checkBox_SigTech_CheckedChanged(object sender, EventArgs e) {
			Update_Signature_Enable();
			Update_Note();
		}

		private void checkBox_MentFort_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_BakingBad_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_PowSpeak_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_CrowdCont_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_LifeReturn_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_FormAndFunc_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_Haki_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_AnatStrike_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_CritHit_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_QuickStrike_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void checkBox_SpIngred_CheckedChanged(object sender, EventArgs e) {
			Update_Note();
		}

		// REMINDER: Rokushiki Techniques CANNOT be applied with Mastery. Therefore, it's Rank will always be what it is.
		private void button_Rokushiki_Click(object sender, EventArgs e) {
			DialogResult result = MessageBox.Show("This will overwrite some parts of the Form. Are you sure you want to Load a Rokushiki Technique?", "Reminder",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (result == DialogResult.Yes) {
				try {
					SelectOptions Roku_Window = new SelectOptions(2, Has_RokuMaster, max_rank);
					Rokushiki.RokuName Sel_Roku = Rokushiki.RokuName.NONE;
					string Roku_Name = "";
					string selected_option = Roku_Window.Rokushiki_Load_Dialog(ref Sel_Roku, ref Roku_Name);
					if (!string.IsNullOrWhiteSpace(selected_option) && Sel_Roku != Rokushiki.RokuName.NONE) {
						Rokushiki.RokuInfo Info_Roku = Roku.Get_RokuInfo(Sel_Roku);
						// We picked a Rokushiki Technique!
						if (selected_option == "Add") {
							// Close the Form and immediately add the Default Technique for Rokushiki
							this.Close();
							button_clicked = true;
							Roku_Form_Type = Sel_Roku;
							// Initialize into Form elements for transfer into Dict and ListView
							textBox_Name.Text = Roku_Name;
							numericUpDown_Rank.Value = Info_Roku.baseRank;
							numericUpDown_RegTP.Value = Info_Roku.baseRank;
							comboBox_AffectRank.SelectedIndex = -1;
							numericUpDown_SpTP.Value = 0;
							comboBox_SpTrait.SelectedIndex = -1;
							comboBox_Type.Text = Info_Roku.type;
							comboBox_Range.Text = Info_Roku.range;
							if (Info_Roku.str != 0) {
								if (Info_Roku.str > 0) { checkBox_PlusStr.Checked = true; }
								if (Info_Roku.str < 0) { checkBox_MinusStr.Checked = true; Info_Roku.str *= -1; }
								numericUpDown_Str.Value = Info_Roku.str;
							}
							else { checkBox_PlusStr.Checked = false; checkBox_MinusStr.Checked = false; numericUpDown_Str.Value = 0; }
							if (Info_Roku.spe != 0) {
								if (Info_Roku.spe > 0) { checkBox_PlusSpe.Checked = true; }
								if (Info_Roku.spe < 0) { checkBox_MinusSpe.Checked = true; Info_Roku.spe *= -1; }
								numericUpDown_Spe.Value = Info_Roku.spe;
							}
							else { checkBox_PlusSpe.Checked = false; checkBox_MinusSpe.Checked = false; numericUpDown_Spe.Value = 0; }
							if (Info_Roku.sta != 0) {
								if (Info_Roku.sta > 0) { checkBox_PlusSta.Checked = true; }
								if (Info_Roku.sta < 0) { checkBox_MinusSta.Checked = true; Info_Roku.sta *= -1; }
								numericUpDown_Sta.Value = Info_Roku.sta;
							}
							else { checkBox_PlusSta.Checked = false; checkBox_MinusSta.Checked = false; numericUpDown_Sta.Value = 0; }
							if (Info_Roku.acc != 0) {
								if (Info_Roku.acc > 0) { checkBox_PlusAcc.Checked = true; }
								if (Info_Roku.acc < 0) { checkBox_MinusAcc.Checked = true; Info_Roku.acc *= -1; }
								numericUpDown_Acc.Value = Info_Roku.acc;
							}
							else { checkBox_PlusAcc.Checked = false; checkBox_MinusAcc.Checked = false; numericUpDown_Acc.Value = 0; }
							checkBox_NA.Checked = false;
							checkBox_Branched.Checked = false;
							checkBox_DFTechEnable.Checked = false;
							checkBox_Fuel1.Checked = false;
							checkBox_Fuel2.Checked = false;
							checkBox_Fuel3.Checked = false;
							checkBox_SigTech.Checked = false;
							checkBox_CritHit.Checked = false;
							checkBox_AnatStrike.Checked = false;
							checkBox_QuickStrike.Checked = false;
							checkBox_Haki.Checked = false;
							checkBox_FormAndFunc.Checked = false;
							checkBox_LifeReturn.Checked = false;
							checkBox_MentFort.Checked = false;
							checkBox_CrowdCont.Checked = false;
							checkBox_PowSpeak.Checked = false;
							checkBox_BakingBad.Checked = false;
							checkBox_SpIngred.Checked = false;
							EffectList.Clear();
							listView_Effects.Items.Clear();
							textBox_Power.Text = Info_Roku.basePower.ToString();
							richTextBox_Note.Clear();
							richTextBox_Note.Text = "- [i]Base " + Info_Roku.name + " Technique[/i]";
							richTextBox_Desc.Text = Info_Roku.desc;
						}
						else if (selected_option == "Custom") {
							// Load the Information into the Technique Form, change "Form Type", and signify this is now Rokushiki
							this.Text = "Technique Creator - [Rokushiki: " + Info_Roku.name + "]";
							label_PowerNote.Visible = false;
							label_TechFormMsg.Visible = true;
							toolTips.Active = false;
							toolTip_Roku.Active = true;
							Roku_Form_Type = Sel_Roku;
							base_power = Info_Roku.basePower;
							// Now edit the Form accordingly.
							textBox_Name.Text = Roku_Name;
							numericUpDown_Rank.Value = Info_Roku.baseRank;
							numericUpDown_RegTP.Value = Info_Roku.baseRank;
							comboBox_Type.Text = Info_Roku.type;
							comboBox_Range.Text = Info_Roku.range;
							if (Info_Roku.str != 0) {
								if (Info_Roku.str > 0) { checkBox_PlusStr.Checked = true; }
								if (Info_Roku.str < 0) { checkBox_MinusStr.Checked = true; Info_Roku.str *= -1; }
								numericUpDown_Str.Value = Info_Roku.str;
							}
							else { checkBox_PlusStr.Checked = false; checkBox_MinusStr.Checked = false; numericUpDown_Str.Value = 0; }
							if (Info_Roku.spe != 0) {
								if (Info_Roku.spe > 0) { checkBox_PlusSpe.Checked = true; }
								if (Info_Roku.spe < 0) { checkBox_MinusSpe.Checked = true; Info_Roku.spe *= -1; }
								numericUpDown_Spe.Value = Info_Roku.spe;
							}
							else { checkBox_PlusSpe.Checked = false; checkBox_MinusSpe.Checked = false; numericUpDown_Spe.Value = 0; }
							if (Info_Roku.sta != 0) {
								if (Info_Roku.sta > 0) { checkBox_PlusSta.Checked = true; }
								if (Info_Roku.sta < 0) { checkBox_MinusSta.Checked = true; Info_Roku.sta *= -1; }
								numericUpDown_Sta.Value = Info_Roku.sta;
							}
							else { checkBox_PlusSta.Checked = false; checkBox_MinusSta.Checked = false; numericUpDown_Sta.Value = 0; }
							if (Info_Roku.acc != 0) {
								if (Info_Roku.acc > 0) { checkBox_PlusAcc.Checked = true; }
								if (Info_Roku.acc < 0) { checkBox_MinusAcc.Checked = true; Info_Roku.acc *= -1; }
								numericUpDown_Acc.Value = Info_Roku.acc;
							}
							else { checkBox_PlusAcc.Checked = false; checkBox_MinusAcc.Checked = false; numericUpDown_Acc.Value = 0; }
							checkBox_Branched.Checked = false;
							EffectList.Clear();
							listView_Effects.Items.Clear();
							textBox_Power.Text = Info_Roku.basePower.ToString();
							comboBox_AffectRank.Enabled = false;
							richTextBox_Desc.Text = Info_Roku.desc;
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
