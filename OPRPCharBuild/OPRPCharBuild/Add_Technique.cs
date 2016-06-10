﻿using System;
using System.Windows.Forms;

namespace OPRPCharBuild
{
	public partial class Add_Technique : Form
	{
		private bool button_clicked;
		private int max_rank;
		private ListView traits_list;
		private ListView SpTraits_list;
		Traits traits_ref = new Traits();
		// Used when editing Dialogue for comboBox SpTrait and Rank Trait
		private string edit_SpTrait;
		private string edit_RankTrait;

		public Add_Technique(int MaxRank, ListView t_list, ListView Sp_list) {
			InitializeComponent();
			button_clicked = false;
			max_rank = MaxRank;
			traits_list = t_list;
			SpTraits_list = Sp_list;
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
				item.SubItems.Add(comboBox_AffectRank.Text);             // Column 5: Rank Trait
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
			traits_ref.Trait_Name_From_ListView(ref tech);
			if (traits_ref.get_TraitID(tech) == Traits.Trait_Name.SIG_TECH) { // Sig Tech
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
					Main_Form.SelectedItems[0].SubItems[5].Text = comboBox_AffectRank.Text;         // Column 5: Rank Trait
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

		private void Add_Trait_comboBox(ref ComboBox list, Traits.Trait_Name ID, ListView traits_list) {
			int index = traits_ref.Contains_Trait_AtIndex(ID, traits_list);
			string name = traits_list.Items[index].SubItems[0].Text;
			list.Items.Add(name);
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

		private void Update_TPMsg() {
			string message = "";
			// Affect Rank techs
			string tech = comboBox_AffectRank.Text;
			traits_ref.Trait_Name_From_ListView(ref tech);
			Traits.Trait_Name Trait_ID = traits_ref.get_TraitID(tech);
			if (Trait_ID == Traits.Trait_Name.SIG_TECH) {
				message += "[i]Signature Technique[/i]. ";
			}
			if (Trait_ID == Traits.Trait_Name.DEV_FRUIT) {
				message += "[i]Devil Fruit Technique[/i]. ";
			}
			if (Trait_ID == Traits.Trait_Name.MARTIAL_MASTERY || Trait_ID == Traits.Trait_Name.ADV_MARTIAL_MASTERY ||
				Trait_ID == Traits.Trait_Name.STANCE_MAST || Trait_ID == Traits.Trait_Name.ART_OF_STEALTH ||
				Trait_ID == Traits.Trait_Name.ANTI_STEALTH || Trait_ID == Traits.Trait_Name.DWARF) {
				message += "[i]" + comboBox_AffectRank.Text + " Technique[/i], treated 4 Ranks higher. ";
			}
			// Branch message
			if (checkBox_Branched.Checked) {
				message += numericUpDown_RegTP.Value.ToString() + " points branched from [i]" + textBox_TechBranched.Text + "[/i]. ";
			}
			// Special TP usage.
			if (numericUpDown_SpTP.Value > 0) {
				message += numericUpDown_SpTP.Value + " Sp. TP used from [i]" + comboBox_SpTrait.Text + "[/i].";
			}
			textBox_TPMsg.Text = message;
			// Used when Sig is checked/unchecked
			// Used when Branched From text is changed.
			// Used when Branched points is changed.
			// Used when Special Trait is selected.
			// Used when Sp. TP is changed.
		}

		#endregion

		#region Exception Handlers

		// This only occurs once before the form is displayed for the first time.
		// We'll need this to set some Maximums or comboBox lists based on the Main_Form
		private void Add_Technique_Load(object sender, EventArgs e) {
			numericUpDown_Rank.Maximum = max_rank;
			label_MaxRank.Text = "Max Rank is: " + max_rank;
			numericUpDown_Power.Maximum = max_rank + 4;
			numericUpDown_RegTP.Maximum = max_rank;
			numericUpDown_SpTP.Maximum = max_rank;
			numericUpDown_RankBranch.Maximum = max_rank - 1;
			// Add Traits Affecting Rank
			if (traits_ref.Contains_Trait_AtIndex(Traits.Trait_Name.DWARF, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.DWARF, traits_list);
			}
			if (traits_ref.Contains_Trait_AtIndex(Traits.Trait_Name.MARTIAL_MASTERY, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.MARTIAL_MASTERY, traits_list);
			}
			if (traits_ref.Contains_Trait_AtIndex(Traits.Trait_Name.ADV_MARTIAL_MASTERY, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.ADV_MARTIAL_MASTERY, traits_list);
			}
			if (traits_ref.Contains_Trait_AtIndex(Traits.Trait_Name.STANCE_MAST, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.STANCE_MAST, traits_list);
			}
			if (traits_ref.Contains_Trait_AtIndex(Traits.Trait_Name.ART_OF_STEALTH, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.ART_OF_STEALTH, traits_list);
			}
			if (traits_ref.Contains_Trait_AtIndex(Traits.Trait_Name.ANTI_STEALTH, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.ANTI_STEALTH, traits_list);
			}
			if (traits_ref.Contains_Trait_AtIndex(Traits.Trait_Name.DEV_FRUIT, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.DEV_FRUIT, traits_list);
			}
			if (traits_ref.Contains_Trait_AtIndex(Traits.Trait_Name.SIG_TECH, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.SIG_TECH, traits_list);
			}
			// Add Special TP Traits
			foreach (ListViewItem eachItem in SpTraits_list.Items) {
				string name = eachItem.SubItems[0].Text;
				traits_ref.Trait_Name_From_ListView(ref name);
				Add_Trait_comboBox(ref comboBox_SpTrait, traits_ref.get_TraitID(name), SpTraits_list);
			}
			comboBox_SpTrait.Text = edit_SpTrait;
			comboBox_AffectRank.Text = edit_RankTrait;
			// Check if SpTrait_comboBox is empty
			if (comboBox_SpTrait.Items.Count > 0) {
				numericUpDown_SpTP.Enabled = true;
				label_SpTraitUsed.Text = "Sp TP Trait";
				comboBox_SpTrait.Enabled = true;
			}
		}

		private void button11_Click(object sender, EventArgs e) {
			// Only want the appropriate changes to be made, so we add a bool
			if (string.IsNullOrWhiteSpace(textBox_Name.Text)) {
				MessageBox.Show("Technique needs a name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (checkBox_Branched.Checked && (numericUpDown_RankBranch.Value == 0 || 
				string.IsNullOrWhiteSpace(textBox_TechBranched.Text))) {
				MessageBox.Show("Technique Branch incomplete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else {
				this.Close();
				button_clicked = true;
			}
		}

		private void checkBox_Branched_CheckedChanged(object sender, EventArgs e) {
			// Checkbox for Branched
			Update_Branched_Check();
			Update_TPMsg();
		}

		private void textBox_TechBranched_TextChanged(object sender, EventArgs e) {
			Update_TPMsg();
		}

		private void numericUpDown_RankBranch_ValueChanged(object sender, EventArgs e) {
			// Update Rank value
			numericUpDown_RegTP.Value = numericUpDown_Rank.Value - numericUpDown_RankBranch.Value;
			// Reset Sp TP for simplicity sake.
			numericUpDown_SpTP.Value = 0;
			Update_TPMsg();
		}

		private void numericUpDown_SpTP_ValueChanged(object sender, EventArgs e) {
			// For simplicity sake, calculate regTP used for them.
			numericUpDown_RegTP.Value = numericUpDown_Rank.Value - numericUpDown_SpTP.Value;
            Update_TPMsg();
		}

		private void comboBox_SpTrait_SelectedIndexChanged(object sender, EventArgs e) {
			Update_TPMsg();
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
				comboBox_AffectRank.Text = "";
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
				numericUpDown_Power.Value = 0;
				textBox_Effects.Clear();
				textBox_TPMsg.Clear();
				richTextBox_Desc.Clear();
			}
		}

		private void numericUpDown_Rank_ValueChanged(object sender, EventArgs e) {
			// Set Maximum values.
			numericUpDown_RegTP.Maximum = numericUpDown_Rank.Value;
			numericUpDown_RegTP.Value = numericUpDown_Rank.Value - numericUpDown_RankBranch.Value;
			numericUpDown_SpTP.Maximum = numericUpDown_Rank.Value;
			numericUpDown_RankBranch.Maximum = numericUpDown_Rank.Value - 1; // This is hella important
			numericUpDown_Power.Maximum = numericUpDown_Rank.Value + 4;
        }

		private void comboBox_AffectRank_SelectedIndexChanged(object sender, EventArgs e) {
			// Where Signature tech comes into play
			string tech = comboBox_AffectRank.Text;
			traits_ref.Trait_Name_From_ListView(ref tech);
			if (traits_ref.get_TraitID(comboBox_AffectRank.Text) == Traits.Trait_Name.SIG_TECH) {
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
			Update_TPMsg();
		}

		// This is to avoid tedious repetition for when Stat buttons are pressed.
		private void Button_Effect(ref CheckBox changed_box, ref CheckBox other_box, ref NumericUpDown stat) {
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
			Button_Effect(ref checkBox_PlusStr, ref checkBox_MinusStr, ref numericUpDown_Str);
		}

		private void checkBox_MinusStr_CheckedChanged(object sender, EventArgs e) {
			Button_Effect(ref checkBox_MinusStr, ref checkBox_PlusStr, ref numericUpDown_Str);
		}

		private void checkBox_PlusSpe_CheckedChanged(object sender, EventArgs e) {
			Button_Effect(ref checkBox_PlusSpe, ref checkBox_MinusSpe, ref numericUpDown_Spe);
		}

		private void checkBox_MinusSpe_CheckedChanged(object sender, EventArgs e) {
			Button_Effect(ref checkBox_MinusSpe, ref checkBox_PlusSpe, ref numericUpDown_Spe);
		}

		private void checkBox_PlusSta_CheckedChanged(object sender, EventArgs e) {
			Button_Effect(ref checkBox_PlusSta, ref checkBox_MinusSta, ref numericUpDown_Sta);
		}

		private void checkBox_MinusSta_CheckedChanged(object sender, EventArgs e) {
			Button_Effect(ref checkBox_MinusSta, ref checkBox_PlusSta, ref numericUpDown_Sta);
		}

		private void checkBox_PlusAcc_CheckedChanged(object sender, EventArgs e) {
			Button_Effect(ref checkBox_PlusAcc, ref checkBox_MinusAcc, ref numericUpDown_Acc);
		}

		private void checkBox_MinusAcc_CheckedChanged(object sender, EventArgs e) {
			Button_Effect(ref checkBox_MinusAcc, ref checkBox_PlusAcc, ref numericUpDown_Acc);
		}

		#endregion
	}
}
