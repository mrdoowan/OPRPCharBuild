using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace OPRPCharBuild
{
	public partial class Add_Technique : Form
	{
		private bool button_clicked;
		private int max_rank;
		private int min_rank;
		private ListView traits_list;
		private ListView SpTraits_list;
		private Traits traits = new Traits();
		private string effect_label_reset = "Effect Encyclopedia\n" + 
			"- Weathermancy Effects require Weathermancy Trait\n" + 
			"- Pop Greens Effects require Horticultural Warfare Trait\n" +
			"- Stealth Effect require Assassin/Thief primary.";
		private int gen_effects;    // To keep track how many General Effects there currently are for Secondary Gen Effects
									// Updated when an Effect is added
									// Updated when an Effect is removed
		// Bools for primary professions
		private bool assassin_primary;
		private bool thief_primary;
		// Used when editing Dialogue for comboBox SpTrait and Rank Trait
		private string edit_SpTrait;
		private string edit_RankTrait;
		// Devil Fruit section
		private string DF_name;
		private string DF_type;
		// Loading the current effect
		private Effects Effect = new Effects();
		// Item for the Dict for a ListView
		public struct EffectItem
		{
			public Effects.Effect_Name ID;
			public int cost;

			// Constructor
			public EffectItem(Effects.Effect_Name ID_, int cost_) {
				ID = ID_;
				cost = cost_;
			}
		}
		public static Dictionary<string, EffectItem> EffectList = new Dictionary<string, EffectItem>(); // Static variable for project usage

		public Add_Technique(int MaxRank, ListView t_list, ListView Sp_list, string DF_Name, string DF_Type) {
			InitializeComponent();
			button_clicked = false;
			max_rank = MaxRank;
			min_rank = 1;
			traits_list = t_list;
			SpTraits_list = Sp_list;
			DF_name = DF_Name;
			DF_type = DF_Type;
			MainForm.Set_Primary_Bool("Assassin", ref assassin_primary);
			MainForm.Set_Primary_Bool("Thief", ref thief_primary);
			gen_effects = 0;
		}

		#region Dialog Functions

		public void NewDialog(ref ListView Main_Form, bool branch) {
			if (branch) {
				// If we're branching a Technique, we want to duplicate, and then modify.
				try {
					Copy_Data_To_Form(Main_Form);
					textBox_Name.Clear();
					ListViewItem sel_item = Main_Form.SelectedItems[0];
					int rank = int.Parse(sel_item.SubItems[1].Text);
					checkBox_Branched.Checked = true;
                    numericUpDown_Rank.Value = rank + 1;
					textBox_TechBranched.Text = sel_item.SubItems[0].Text;
					numericUpDown_RankBranch.Value = rank;
				}
				catch (Exception e) {
					MessageBox.Show("There was a problem branching Technique\nReason: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			this.ShowDialog();
			if (button_clicked) {
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = textBox_Name.Text;    // Column 0: Tech Name
				item.SubItems.Add(numericUpDown_Rank.Value.ToString());  // Column 1: Rank
				item.SubItems.Add(numericUpDown_RegTP.Value.ToString()); // Column 2: Reg TP
				item.SubItems.Add(numericUpDown_SpTP.Value.ToString());  // Column 3: Sp. TP
				item.SubItems.Add(comboBox_SpTrait.Text);                // Column 4: Sp. Trait
				item.SubItems.Add(comboBox_AffectTech.Text);             // Column 5: Rank Trait
				item.SubItems.Add(textBox_TechBranched.Text);            // Column 6: Branched From
				item.SubItems.Add(numericUpDown_RankBranch.Value.ToString()); // Column 7: Points Branched
				item.SubItems.Add(comboBox_Type.Text);               // Column 8: Type
				item.SubItems.Add(comboBox_Range.Text);              // Column 9: Range
				string stats = "";                                   // Column 10: Stats
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
				item.SubItems.Add(stats);
				item.SubItems.Add(numericUpDown_Power.Value.ToString());    // Column 11: Power
				item.SubItems.Add(textBox_Effects.Text);            // Column 12: Effects
				item.SubItems.Add(textBox_TPMsg.Text);              // Column 13: TP Note
				item.SubItems.Add(richTextBox_Desc.Text);           // Column 14: Description
																								// Add the entire damn thing
				Main_Form.Items.Add(item);
			}
		}

		private void Copy_Data_To_Form(ListView Main_Form) {
			// Put the Tech being edited into the Dialog Box first.
			// ...This is going to massively suck.
			ListViewItem sel_item = Main_Form.SelectedItems[0];
			// With the above, we want to temporarily store a variable
			textBox_Name.Text = sel_item.SubItems[0].Text;                      // Column 0: Tech Name
			numericUpDown_Rank.Value = int.Parse(sel_item.SubItems[1].Text);  // Column 1: Rank
			numericUpDown_RegTP.Value = int.Parse(sel_item.SubItems[2].Text); // Column 2: Reg TP
			numericUpDown_SpTP.Value = int.Parse(sel_item.SubItems[3].Text);  // Column 3: Sp. TP
			edit_SpTrait = sel_item.SubItems[4].Text;                           // Column 4: Sp. Trait (Need items initialized FIRST)
			edit_RankTrait = sel_item.SubItems[5].Text;                         // Column 5: Rank Trait (Need items initialized FIRST)
																				// Columns 4 and 5 are done in Add_Technique_Load
			string tech = sel_item.SubItems[5].Text;
			traits.Trait_Name_From_ListView(ref tech);
			if (traits.get_TraitID(tech) == Traits.Trait_Name.SIG_TECH) { // Sig Tech
				numericUpDown_Rank.Value = max_rank;
				numericUpDown_Rank.Enabled = false;
				numericUpDown_RegTP.Value = 0;
				numericUpDown_SpTP.Value = 0;
				numericUpDown_RegTP.Enabled = false;
				numericUpDown_SpTP.Enabled = false;
			}
			if (string.IsNullOrWhiteSpace(sel_item.SubItems[6].Text)) {         // Column 6: Branched From
																				// Column 7: Points Branched
				checkBox_Branched.Checked = false;
				textBox_TechBranched.Enabled = false;
				numericUpDown_RankBranch.Enabled = false;
			}
			else {
				checkBox_Branched.Checked = true;
				textBox_TechBranched.Enabled = true;
				numericUpDown_RankBranch.Enabled = true;
				textBox_TechBranched.Text = sel_item.SubItems[6].Text;
				numericUpDown_RankBranch.Value = int.Parse(sel_item.SubItems[7].Text);
			}
			comboBox_Type.Text = sel_item.SubItems[8].Text;        // Column 8: Type
			comboBox_Range.Text = sel_item.SubItems[9].Text;       // Column 9: Range
			string stat = sel_item.SubItems[10].Text;              // Column 10: Stats
			if (stat != "N/A") {
				// Format example: +15 Str, -5 Sta
				while (stat != "") {
					// We're going to be progressively removing the Stat string.
					bool plus = true;
					if (stat[0] == '-') {
						plus = false;
					}
					int ind_space = stat.IndexOf(' ');
					int val = int.Parse(stat.Substring(1, ind_space - 1));
					string type = stat.Substring(ind_space + 1, 3);
					// After parsing it, we put it into the form.
					switch (type) {
						case "Str":
							if (plus) {
								checkBox_PlusStr.Checked = true;
							}
							else {
								checkBox_MinusStr.Checked = true;
							}
							numericUpDown_Str.Value = val;
							break;
						case "Spe":
							if (plus) {
								checkBox_PlusSpe.Checked = true;
							}
							else {
								checkBox_MinusSpe.Checked = true;
							}
							numericUpDown_Spe.Value = val;
							break;
						case "Sta":
							if (plus) {
								checkBox_PlusSta.Checked = true;
							}
							else {
								checkBox_MinusSta.Checked = true;
							}
							numericUpDown_Sta.Value = val;
							break;
						case "Acc":
							if (plus) {
								checkBox_PlusAcc.Checked = true;
							}
							else {
								checkBox_MinusAcc.Checked = true;
							}
							numericUpDown_Acc.Value = val;
							break;
						default:
							MessageBox.Show("There was a bug in transferring Stats.", "Bug Report");
							break;
					}
					// And now we want to remove that part.
					int ind_comm = stat.IndexOf(',');
					if (ind_comm != -1) {
						stat = stat.Remove(0, ind_comm + 2);
						// This should be the next stat
					}
					else {
						stat = "";
					}
				}
			}
			numericUpDown_Power.Value = int.Parse(sel_item.SubItems[11].Text);    // Column 11: Power
			textBox_Effects.Text = sel_item.SubItems[12].Text;  // Column 12: Effects
			textBox_TPMsg.Text = sel_item.SubItems[13].Text;    // Column 13: TP Note
			richTextBox_Desc.Text = sel_item.SubItems[14].Text; // Column 14: Description
																// Now proceed to edit it.
		}

		public void EditDialog(ref ListView Main_Form) {
			try {
				button11.Text = "Edit";
				Copy_Data_To_Form(Main_Form);
				this.ShowDialog();
				if (button_clicked) {
					Main_Form.SelectedItems[0].SubItems[0].Text = textBox_Name.Text;    // Column 0: Tech Name
					Main_Form.SelectedItems[0].SubItems[1].Text = numericUpDown_Rank.Value.ToString();  // Column 1: Rank
					Main_Form.SelectedItems[0].SubItems[2].Text = numericUpDown_RegTP.Value.ToString(); // Column 2: Reg TP
					Main_Form.SelectedItems[0].SubItems[3].Text = numericUpDown_SpTP.Value.ToString();  // Column 3: Sp. TP
					Main_Form.SelectedItems[0].SubItems[4].Text = comboBox_SpTrait.Text;            // Column 4: Sp. Trait
					Main_Form.SelectedItems[0].SubItems[5].Text = comboBox_AffectTech.Text;         // Column 5: Rank Trait
					Main_Form.SelectedItems[0].SubItems[6].Text = textBox_TechBranched.Text;        // Column 6: Branched From
					Main_Form.SelectedItems[0].SubItems[7].Text = numericUpDown_RankBranch.Value.ToString(); // Column 7: Points Branched
					Main_Form.SelectedItems[0].SubItems[8].Text = comboBox_Type.Text;               // Column 8: Type
					Main_Form.SelectedItems[0].SubItems[9].Text = comboBox_Range.Text;              // Column 9: Range
					string stats = "";                                                              // Column 10: Stats
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
					Main_Form.SelectedItems[0].SubItems[10].Text = stats;
					Main_Form.SelectedItems[0].SubItems[11].Text = numericUpDown_Power.Value.ToString();    // Column 11: Power
					Main_Form.SelectedItems[0].SubItems[12].Text = textBox_Effects.Text;            // Column 12: Effects
					Main_Form.SelectedItems[0].SubItems[13].Text = textBox_TPMsg.Text;              // Column 13: TP Note
					Main_Form.SelectedItems[0].SubItems[14].Text = richTextBox_Desc.Text;           // Column 14: Description
				}
			}
			catch (Exception e) {
				MessageBox.Show("You didn't select a Technique\nException Handled: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		#region Other member functions used

		// If trait is in Traits List, add it to the ComboBox. Returns true if so.
		private bool Add_Trait_comboBox(ref ComboBox combobox, Traits.Trait_Name ID, ListView traits_list) {
			int index = traits.Contains_Trait_AtIndex(ID, traits_list);
			if (index != -1) {
				string name = traits_list.Items[index].SubItems[0].Text;
				combobox.Items.Add(name);
				return true;
			}
			return false;
		}

		// One exception to the Effect_info_load function because we're adding items into the ComboBox
		private void Add_Effect_comboBox(ref ComboBox combobox, Effects.Effect_Name ID) {
			if (Effect.Get_EffectInfo(ID).MinRank < max_rank) {
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
			// Traits Affecting Tech
			string tech = comboBox_AffectTech.Text;
			traits.Trait_Name_From_ListView(ref tech);
			Traits.Trait_Name Trait_ID = traits.get_TraitID(tech);
			if (Trait_ID == Traits.Trait_Name.SIG_TECH) {
				message += "[i]Signature Technique[/i]. ";
			}
			else if (Trait_ID == Traits.Trait_Name.DEV_FRUIT) {
				message += "[i]DF Technique";
				if (checkBox_DFRank4.Checked) {
					message += " - Free R4 Tech";
				}
				if (checkBox_ZoanSig.Checked) {
					message += " - Zoan Signature";
				}
				if (checkBox_Hybrid.Checked) {
					message += " - Hybrid Transformation";
				}
				if (checkBox_Full.Checked) {
					message += " - Full Transformation";
				}
				message += ".[/i]";
			}
			else if (Trait_ID == Traits.Trait_Name.MARTIAL_MASTERY || Trait_ID == Traits.Trait_Name.ADV_MARTIAL_MASTERY ||
				Trait_ID == Traits.Trait_Name.STANCE_MAST || Trait_ID == Traits.Trait_Name.ART_OF_STEALTH ||
				Trait_ID == Traits.Trait_Name.ANTI_STEALTH || Trait_ID == Traits.Trait_Name.DWARF) {
				message += "[i]" + comboBox_AffectTech.Text + " Technique[/i], treated 4 Ranks higher. ";
			}
			else { // This means some kind of technique is being used.
				message += "[i]" + comboBox_AffectTech.Text + " Technique[/i]. ";
			}
			// Branch message
			if (checkBox_Branched.Checked) {
				message += "Branched from [i]R" + numericUpDown_RankBranch.Value.ToString() + " " + textBox_TechBranched.Text + "[/i]. ";
			}
			// Special TP usage.
			if (numericUpDown_SpTP.Value > 0) {
				message += numericUpDown_SpTP.Value + " Sp. TP used for [i]" + comboBox_SpTrait.Text + "[/i].";
			}
			textBox_TPMsg.Text = message;
			// Used when Sig is checked/unchecked
			// Used when Branched From text is changed.
			// Used when Branched points is changed.
			// Used when Special Trait is selected.
			// Used when Sp. TP is changed.
		}

		private void Update_Power_Value() {

			// Used when Trait Affecting Tech is changed
			// Used when Rank is changed
			// Used when an Effect is Added
			// Used when an Effect is Removed
		}

		private void Update_DFRank4() {
			int rank = (int)numericUpDown_Rank.Value;
			if (checkBox_DFRank4.Checked) {
				rank -= 4;
				if (rank < 0) {
					rank = 0;
				}
				numericUpDown_RegTP.Value = rank;
			}
			else {
				numericUpDown_RegTP.Value = rank;
			}
			// Used when DF Rank 4 is Checked
			// Used when Rank Value is changed
		}

		private void Update_MinRank() {

			// Used when an Effect is Added
			// Used when an Effect is Removed
		}

		private void Update_Signature_Enable() {
			if (traits.get_TraitID(comboBox_AffectTech.Text) == Traits.Trait_Name.SIG_TECH || checkBox_ZoanSig.Checked) {
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

		#region Exception Handlers

		// This only occurs once before the form is displayed for the first time.
		// We'll need this to set some Maximums or comboBox lists based on the Main_Form
		private void Add_Technique_Load(object sender, EventArgs e) {
			// Set Maximum Values of NumericUpDown
			numericUpDown_Rank.Maximum = max_rank;
			label_MaxRank.Text = "Max Rank is: " + max_rank;
			numericUpDown_RegTP.Maximum = max_rank;
			numericUpDown_SpTP.Maximum = max_rank;
			numericUpDown_RankBranch.Maximum = max_rank - 1;

			// Add Traits Affecting the Tech
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.DWARF, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.ART_OF_STEALTH, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.ANTI_STEALTH, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.DEV_FRUIT, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.SIG_TECH, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.BAS_CYBORG, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.ADV_CYBORG, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.NW_CYBORG, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.F_AND_F, traits_list);
			if (!Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.DISC_HAKI, traits_list)) {
				Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.AWAKE_HAKI, traits_list);
			}
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.CONQ_HAKI, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.LIFE_RET, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.ROK_MAST, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.HORT_WAR, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.WEATHER, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.CRIT_HIT, traits_list);
			if (!Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.ADV_STANCE_MASTERY, traits_list)) {
				Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.STANCE_MAST, traits_list);
			}
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.GRAND_MARTIAL, traits_list);
			if (!Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.ADV_MARTIAL_MASTERY, traits_list)) {
				Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.MARTIAL_MASTERY, traits_list);
			}
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.CROWD_CONT, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.ANAT_STRIKE, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.QUICKSTRIKE, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.POW_SPEAK, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.BAKING_BAD, traits_list);
			Add_Trait_comboBox(ref comboBox_AffectTech, Traits.Trait_Name.MAST_MISDI, traits_list);
			

			// Add Special TP Traits
			foreach (ListViewItem eachItem in SpTraits_list.Items) {
				string name = eachItem.SubItems[0].Text;
				traits.Trait_Name_From_ListView(ref name);
				Add_Trait_comboBox(ref comboBox_SpTrait, traits.get_TraitID(name), SpTraits_list);
			}
			comboBox_SpTrait.Text = edit_SpTrait;
			comboBox_AffectTech.Text = edit_RankTrait;

			// Check if SpTrait_comboBox is empty
			if (comboBox_SpTrait.Items.Count > 0) {
				numericUpDown_SpTP.Enabled = true;
				label_SpTraitUsed.Text = "Sp TP Trait";
				comboBox_SpTrait.Enabled = true;
			}

			// Fill in the ListView columns
			listView_Effects.View = View.Details;
			listView_Effects.FullRowSelect = true;
			listView_Effects.Columns.Add("Name", 100);  // Column [0]
			listView_Effects.Columns.Add("Cost", 50);   // Column [1]
			listView_Effects.Columns.Add("Power?", 50); // Column [2]

			// Add ALL Effects into the ComboBox (based on max rank)
			#region Effects ComboBox
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
			if (traits.Contains_Trait_AtIndex(Traits.Trait_Name.WEATHER, traits_list) != -1) {
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.CLOUD);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.RAIN);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.FOG);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.ELE_DMG_WEAT);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.WIND);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.MILK);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.MIR_CLONE);
				Add_Effect_comboBox(ref comboBox_Effect, Effects.Effect_Name.MIR_CAMO);
			}
			if (traits.Contains_Trait_AtIndex(Traits.Trait_Name.HORT_WAR, traits_list) != -1) {
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
			#endregion
		}

		private void button11_Click(object sender, EventArgs e) {
			// Only want the appropriate changes to be made, so we add a bool
			if (string.IsNullOrWhiteSpace(textBox_Name.Text)) {
				MessageBox.Show("Technique needs a name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (checkBox_Branched.Checked && 
				(numericUpDown_RankBranch.Value == 0 || string.IsNullOrWhiteSpace(textBox_TechBranched.Text))) {
				MessageBox.Show("Technique Branch incomplete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (int.Parse(textBox_Power.Text) < 0) {
				MessageBox.Show("Power is below 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (min_rank > numericUpDown_Rank.Value) {
				MessageBox.Show("Effect costs are ineligible at its current rank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else {
				this.Close();
				button_clicked = true;
			}
		}

		private void checkBox_Branched_CheckedChanged(object sender, EventArgs e) {
			// Checkbox for Branched
			Update_Branched_Check();
			Update_Note();
		}

		private void textBox_TechBranched_TextChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void numericUpDown_RankBranch_ValueChanged(object sender, EventArgs e) {
			// Update Rank value
			numericUpDown_RegTP.Value = numericUpDown_Rank.Value - numericUpDown_RankBranch.Value;
			// Reset Sp TP for simplicity sake.
			numericUpDown_SpTP.Value = 0;
			Update_Note();
		}

		private void numericUpDown_SpTP_ValueChanged(object sender, EventArgs e) {
			// For simplicity sake, calculate regTP used for them.
			numericUpDown_RegTP.Value = numericUpDown_Rank.Value - numericUpDown_SpTP.Value;
            Update_Note();
		}

		private void comboBox_SpTrait_SelectedIndexChanged(object sender, EventArgs e) {
			Update_Note();
		}

		private void button12_Click(object sender, EventArgs e) {
			// Clear button
			DialogResult result = new DialogResult();
			result = DialogResult.No;
			result = MessageBox.Show("Are you sure you want to clear the technique?", "Clear Technique",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			if (result == DialogResult.Yes) {
				textBox_Name.Clear();
				numericUpDown_Rank.Value = 1;
				comboBox_AffectTech.Text = "";
				checkBox_Branched.Checked = false;
				textBox_TechBranched.Clear();
				numericUpDown_RankBranch.Value = 0;
				numericUpDown_RegTP.Value = 0;
				numericUpDown_SpTP.Value = 0;
				comboBox_SpTrait.Text = "";
				comboBox_Type.Text = "";
				comboBox_Range.Text = "";
				// Stats
				checkBox_PlusStr.Checked = false;
				checkBox_MinusStr.Checked = false;
				checkBox_PlusSta.Checked = false;
				checkBox_MinusSta.Checked = false;
				checkBox_PlusSta.Checked = false;
				checkBox_MinusSta.Checked = false;
				checkBox_PlusAcc.Checked = false;
				checkBox_MinusAcc.Checked = false;
				numericUpDown_Str.Value = 1;
				numericUpDown_Spe.Value = 1;
				numericUpDown_Sta.Value = 1;
				numericUpDown_Acc.Value = 1;
				// The rest
				textBox_TPMsg.Clear();
				richTextBox_Desc.Clear();
			}
		}

		private void numericUpDown_Rank_ValueChanged(object sender, EventArgs e) {
			// Check Effects before Proceeding
			
			Update_Power_Value();
			// Set Maximum values.
			numericUpDown_RegTP.Maximum = numericUpDown_Rank.Value;
			numericUpDown_RegTP.Value = numericUpDown_Rank.Value - numericUpDown_RankBranch.Value;
			numericUpDown_SpTP.Maximum = numericUpDown_Rank.Value;
			numericUpDown_RankBranch.Maximum = numericUpDown_Rank.Value - 1; // This is hella important
			// Update Functions
			Update_DFRank4();
		}

		private void comboBox_AffectTech_SelectedIndexChanged(object sender, EventArgs e) {
			// Where Signature Tech Enable
			Update_Signature_Enable();
			// Specific Devil Fruit Enable
			if (traits.get_TraitID(comboBox_AffectTech.Text) == Traits.Trait_Name.DEV_FRUIT) {
				label_DF.Text = "Devil Fruit: " + DF_name + "\nType: " + DF_type;
				checkBox_DFRank4.Enabled = true;
				checkBox_Full.Enabled = true;
				checkBox_Hybrid.Enabled = true;
				checkBox_ZoanSig.Enabled = true;
			}
			else {
				label_DF.Text = "No Devil Fruit Option";
				checkBox_DFRank4.Enabled = false;
				checkBox_Full.Enabled = false;
				checkBox_Hybrid.Enabled = false;
				checkBox_ZoanSig.Enabled = false;
			}
			// Update Functions
			Update_Note();
			Update_Power_Value();
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
				stat.Value = 1;
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

		#endregion

		private void checkBox_NA_CheckedChanged(object sender, EventArgs e) {
			if (checkBox_NA.Checked) {
				listView_Effects.Enabled = false;
				button_EffectRemove.Enabled = false;
				button_AddEffect.Enabled = false;
				numericUpDown_Cost.Enabled = false;
				comboBox_Effect.Enabled = false;
				textBox_Power.Text = "0";
				listView_Effects.Items.Clear();
				label_MinRank.Text = "Min Rank is: 1";
				min_rank = 1;
				label_EffectDesc.Text = effect_label_reset;
			}
			else {
				listView_Effects.Enabled = true;
				button_EffectRemove.Enabled = true;
				button_AddEffect.Enabled = true;
				numericUpDown_Cost.Enabled = true;
				numericUpDown_Cost.ReadOnly = true;
				comboBox_Effect.Enabled = true;
				// Fill in Effect Description if there's one pending
				Effects.Effect_Name ID = Effect.Get_EffectID(comboBox_Effect.Text);
				label_EffectDesc.Text = Effect.Get_EffectInfo(ID).desc;
			}
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

		// Returns false if we come across a problem.
		// Returns true if successfully added.
		// This adds a specified Effect to our Dict
		private bool Add_to_EffectList(Effects.Effect_Name ID, int cost) {
			try { EffectList.Add(Effect.Get_EffectInfo(ID).name, new EffectItem(ID, cost)); }
			catch (Exception ex) {
				// Exception thrown for a Duplicate key. If null, then that's a problem.
				if (ex is ArgumentNullException) {
					MessageBox.Show("Error in adding Effect.\nReason: " + ex.Message, "Exception Thrown");
					return false;
				}
				else {
					int i = 2;
					// What we're doing here is trying to give this a unique key by adding a number that increments
					// Infinite Loop danger btw if EffectList.Add keeps adding a null
					while (true) {
						try {
							string new_name = Effect.Get_EffectInfo(ID).name + i.ToString();
							EffectList.Add(new_name, new EffectItem(ID, cost));
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
					MessageBox.Show("Effect name is not legit. Please pick a proper Effect.", "Error");
				}
				else {
					// Dict Data Structure
					Add_to_EffectList(ID, (int)numericUpDown_Cost.Value);
					// ListView
					ListViewItem item = new ListViewItem();
					item.SubItems[0].Text = Effect.Get_EffectInfo(ID).name;
					item.SubItems.Add(numericUpDown_Cost.Value.ToString());
					if (Effect.Get_EffectInfo(ID).general) {
						item.SubItems.Add("No");
					}
					else {
						item.SubItems.Add("Yes");
					}
					// Now check for Secondary General
					if (Effect.Get_EffectInfo(ID).general) {
						gen_effects++;
						if (gen_effects > 2) {
							// That means we have to add the Secondary
							Add_to_EffectList(Effects.Effect_Name.SECONDARY_GEN, 4);
							ListViewItem second = new ListViewItem();
							second.SubItems[0].Text = Effect.Get_EffectInfo(Effects.Effect_Name.SECONDARY_GEN).name;
							second.SubItems.Add("4");
							second.SubItems.Add("Yes");
						}
					}
					// Clear info
					numericUpDown_Cost.Value = 0;
					numericUpDown_Cost.ReadOnly = true;
					comboBox_Effect.Text = "Effect";
					label_EffectDesc.Text = effect_label_reset;
					label_EffectType.Visible = false;
					// Update functions
					Update_MinRank();
					Update_Power_Value();
				}
			}
			catch (Exception ex) {
				MessageBox.Show("Error in adding Effect.\nReason: " + ex.Message, "Bug");
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
			string effect = MainForm.Delete_ListViewItem(ref listView_Effects);
			if (effect == Effect.Get_EffectInfo(Effects.Effect_Name.SECONDARY_GEN).name) {
				MessageBox.Show("Can't remove a Secondary General Effect cost!\nRemove a General Effect instead.", "Error");
			}
            else if (!string.IsNullOrWhiteSpace(effect)) {
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
						EffectList.Remove(secondary_key);		// Dict
					}
					gen_effects--;
				}
				EffectList.Remove(key);
			}
			Update_MinRank();
			Update_Power_Value();
		}

		private void comboBox_Effect_SelectionChangeCommitted(object sender, EventArgs e) {
			// Due to selecting from a set List, we're going to assume everything is fine
			Effects.Effect_Name ID = Effect.Get_EffectID(comboBox_Effect.Text);
			numericUpDown_Cost.Value = Effect.Get_EffectInfo(ID).cost;
			if (ID == Effects.Effect_Name.MIR_CLONE || ID == Effects.Effect_Name.WOOD_DEF) {
				// These are effects that can change in Cost.
				numericUpDown_Cost.ReadOnly = false;
			}
			label_EffectDesc.Text = Effect.Get_EffectInfo(ID).desc;
			if (Effect.Get_EffectInfo(ID).general) {
				label_EffectType.Visible = true;
				label_EffectType.Text = "General Effect";
			}
			else {
				label_EffectType.Visible = true;
				label_EffectType.Text = "Effect requires Power";
			}
		}

		private void comboBox_Range_SelectionChangeCommitted(object sender, EventArgs e) {
			Effects.Effect_Name ID = Effect.Get_EffectID(comboBox_Range.Text);
			comboBox_Effect.Text = Effect.Get_EffectInfo(ID).name;
			numericUpDown_Cost.Value = Effect.Get_EffectInfo(ID).cost;
			if (ID == Effects.Effect_Name.MIR_CLONE || ID == Effects.Effect_Name.WOOD_DEF) {
				// These are effects that can change in Cost.
				numericUpDown_Cost.ReadOnly = false;
			}
			label_EffectDesc.Text = Effect.Get_EffectInfo(ID).desc;
		}

		// Used to display the Effect description in a "Blue" Color
		private void listView_Effects_SelectedIndexChanged(object sender, EventArgs e) {

		}
	}
}
