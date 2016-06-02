using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPRPCharBuild
{
	public partial class Add_Technique : Form
	{
		private bool button_clicked;
		private int max_rank;
		private ListView traits_list;
		private ListView SpTraits_list;
		Traits trait_ref;

		public Add_Technique(int MaxRank, ListView t_list, ListView Sp_list) {
			InitializeComponent();
			button_clicked = false;
			trait_ref = new Traits();
			max_rank = MaxRank;
			traits_list = t_list;
			SpTraits_list = Sp_list;
		}

		#region Dialog Functions

		public void NewDialog(ref ListView Main_Form) {
			this.ShowDialog();
			if (button_clicked) {
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = textBox_Name.Text;          // Column 0: Tech Name
				item.SubItems.Add(numericUpDown_Rank.Value.ToString());     // Column 1: Rank
				item.SubItems.Add(numericUpDown_RegTP.Value.ToString());    // Column 2: Reg TP
				item.SubItems.Add(numericUpDown_SpTP.Value.ToString());     // Column 3: Sp. TP
				item.SubItems.Add(comboBox_SpTrait.Text);           // Column 4: Sp. Trait
				item.SubItems.Add(comboBox_AffectRank.Text);        // Column 5: Rank Trait
				if (checkBox_Sig.Checked) {                         // Column 6: Signature (Yes/No)
					item.SubItems.Add("Yes");
				}
				else {
					item.SubItems.Add("No");
				}
				item.SubItems.Add(textBox_TechBranched.Text);       // Column 7: Branched From
				item.SubItems.Add(numericUpDown_PointsBranch.Value.ToString()); // Column 8: Points Branched
				item.SubItems.Add(comboBox_Type.Text);              // Column 9: Type
				item.SubItems.Add(comboBox_Range.Text);             // Column 10: Range
				item.SubItems.Add(numericUpDown_Power.Value.ToString());    // Column 11: Power
				string stats = "";                                  // Column 12: Stats
				if (!checkBox_Stats.Checked) {
					if (numericUpDown_PlusStats.Value == 0 && numericUpDown_MinusStats.Value == 0) {
						stats = "N/A";
					}
					else {
						if (numericUpDown_PlusStats.Value > 0) {
							stats += "+" + numericUpDown_PlusStats.Value.ToString() + " " + comboBox_PlusStat.Text;
						}
						if (numericUpDown_PlusStats.Value > 0 && numericUpDown_MinusStats.Value > 0) {
							stats += ", ";
						}
						if (numericUpDown_MinusStats.Value > 0) {
							stats += "-" + numericUpDown_MinusStats.Value.ToString() + " " + comboBox_MinusStat.Text;
						}
					}
				}
				item.SubItems.Add(stats);
				item.SubItems.Add(textBox_TPMsg.Text);              // Column 13: TP Usage Msg
				item.SubItems.Add(richTextBox_Desc.Text);           // Column 14: Description
																	// Add the entire damn thing
				Main_Form.Items.Add(item);
			}
		}

		// Returns value that the stat adds/minus
		public void Parse_Stats(string stat, ref NumericUpDown val, ref ComboBox type) {
			int index = 1; // We're looking at character after '+' or '-'.
						   // We are assuming safely that '+' or '-' is at index 0
			int space = stat.IndexOf(' ');
			int length = space - index;
			val.Value = Int32.Parse(stat.Substring(index, length));
			int type_ind = space + 1;
			type.Text = stat.Substring(type_ind, 3);    // Only set to 3 letters
		}

		public void EditDialog(ref ListView Main_Form) {
			button11.Text = "Edit";
			// Put the Tech being edited into the Dialog Box first.
			// ...This is going to massively suck.
			ListViewItem sel_item = Main_Form.SelectedItems[0];
			// With the above, we want to temporarily store a variable
			textBox_Name.Text = sel_item.SubItems[0].Text;                      // Column 0: Tech Name
			numericUpDown_Rank.Value = Int32.Parse(sel_item.SubItems[1].Text);  // Column 1: Rank
			numericUpDown_RegTP.Value = Int32.Parse(sel_item.SubItems[2].Text); // Column 2: Reg TP
			numericUpDown_SpTP.Value = Int32.Parse(sel_item.SubItems[3].Text);  // Column 3: Sp. TP
			comboBox_SpTrait.Text = sel_item.SubItems[4].Text;      // Column 4: Sp. Trait
			comboBox_AffectRank.Text = sel_item.SubItems[5].Text;   // Column 5: Rank Trait
			if (sel_item.SubItems[6].Text == "Yes") {               // Column 6: Signature (Yes/No)
				checkBox_Sig.Checked = true;
				numericUpDown_Rank.Value = max_rank;
				numericUpDown_Rank.Enabled = false;
				numericUpDown_RegTP.Value = 0;
				numericUpDown_SpTP.Value = 0;
				numericUpDown_RegTP.Enabled = false;
				numericUpDown_SpTP.Enabled = false;
			}
			if (string.IsNullOrWhiteSpace(sel_item.SubItems[7].Text)) {         // Column 7: Branched From
				checkBox_Branched.Checked = false;                              // Column 8: Points Branched
				textBox_TechBranched.Enabled = false;
				numericUpDown_PointsBranch.Enabled = false;
			}
			else {
				checkBox_Branched.Checked = true;
				textBox_TechBranched.Enabled = true;
				numericUpDown_PointsBranch.Enabled = true;
				textBox_TechBranched.Text = sel_item.SubItems[7].Text;
				numericUpDown_PointsBranch.Value = Int32.Parse(sel_item.SubItems[8].Text);
			}
			comboBox_Type.Text = sel_item.SubItems[9].Text;         // Column 9: Type
			comboBox_Range.Text = sel_item.SubItems[10].Text;       // Column 10: Range
			numericUpDown_Power.Value = Int32.Parse(sel_item.SubItems[11].Text);    // Column 11: Power
			string stat = sel_item.SubItems[12].Text;               // Column 12: Stats
			if (stat != "N/A") {
				// Ugh I hate parsing
				checkBox_Stats.Checked = false;
				int ind_plus = stat.IndexOf('+');
				if (ind_plus != -1) { // Plus stats
					Parse_Stats(stat, ref numericUpDown_PlusStats, ref comboBox_PlusStat);
				}
				int ind_minus = stat.IndexOf('-');
				if (ind_minus != -1) {
					// If there's both + and -, we have to remove the +
					if (ind_minus != 0) {
						stat = stat.Remove(0, ind_minus);
					}
					Parse_Stats(stat, ref numericUpDown_MinusStats, ref comboBox_MinusStat);
				}
			}
			else {
				checkBox_Stats.Checked = true;
				numericUpDown_PlusStats.Enabled = false;
				comboBox_PlusStat.Enabled = false;
				numericUpDown_MinusStats.Enabled = false;
				comboBox_MinusStat.Enabled = false;
			}
			textBox_TPMsg.Text = sel_item.SubItems[13].Text;        // Column 13: TP Usage Msg
			richTextBox_Desc.Text = sel_item.SubItems[14].Text; // Column 14: Description
																// Now proceed to edit it.
			this.ShowDialog();
			if (button_clicked) {
				Main_Form.SelectedItems[0].SubItems[0].Text = textBox_Name.Text;    // Column 0: Tech Name
				Main_Form.SelectedItems[0].SubItems[1].Text = numericUpDown_Rank.Value.ToString();  // Column 1: Rank
				Main_Form.SelectedItems[0].SubItems[2].Text = numericUpDown_RegTP.Value.ToString(); // Column 2: Reg TP
				Main_Form.SelectedItems[0].SubItems[3].Text = numericUpDown_SpTP.Value.ToString();  // Column 3: Sp. TP
				Main_Form.SelectedItems[0].SubItems[4].Text = comboBox_SpTrait.Text;            // Column 4: Sp. Trait
				Main_Form.SelectedItems[0].SubItems[5].Text = comboBox_AffectRank.Text;         // Column 5: Rank Trait
				if (checkBox_Sig.Checked) {                         // Column 6: Signature (Yes/No)
					Main_Form.SelectedItems[0].SubItems[6].Text = "Yes";
				}
				else {
					Main_Form.SelectedItems[0].SubItems[6].Text = "No";
				}
				Main_Form.SelectedItems[0].SubItems[7].Text = textBox_TechBranched.Text;        // Column 7: Branched From
				Main_Form.SelectedItems[0].SubItems[8].Text = numericUpDown_PointsBranch.Value.ToString();  // Column 8: Points Branched
				Main_Form.SelectedItems[0].SubItems[9].Text = comboBox_Type.Text;               // Column 9: Type
				Main_Form.SelectedItems[0].SubItems[10].Text = comboBox_Range.Text;             // Column 10: Range
				Main_Form.SelectedItems[0].SubItems[11].Text = numericUpDown_Power.Value.ToString();        // Column 11: Power
				string stats = "";                                  // Column 12: Stats
				if (!checkBox_Stats.Checked) {
					if (numericUpDown_PlusStats.Value == 0 && numericUpDown_MinusStats.Value == 0) {
						stats = "N/A";
					}
					else {
						if (numericUpDown_PlusStats.Value > 0) {
							stats += "+" + numericUpDown_PlusStats.Value.ToString() + " " + comboBox_PlusStat.Text;
						}
						if (numericUpDown_PlusStats.Value > 0 && numericUpDown_MinusStats.Value > 0) {
							stats += ", ";
						}
						if (numericUpDown_MinusStats.Value > 0) {
							stats += "-" + numericUpDown_MinusStats.Value.ToString() + " " + comboBox_MinusStat.Text;
						}
					}
				}
				Main_Form.SelectedItems[0].SubItems[12].Text = stats;
				Main_Form.SelectedItems[0].SubItems[13].Text = textBox_TPMsg.Text;              // Column 13: TP Usage Msg
				Main_Form.SelectedItems[0].SubItems[14].Text = richTextBox_Desc.Text;           // Column 14: Description
			}
		}

		#endregion

		#region Other member functions used

		// Copy and paste from MainForm
		private int Contains_Trait_AtIndex(Traits.Trait_Name ID, ListView listview) {
			int index = 0;
			foreach (ListViewItem eachItem in listview.Items) {
				string spec = ""; // trash variable
				string name = eachItem.SubItems[0].Text;
				// SubItems[0] always contains the Trait name
				// Need to check if this is a SPEC trait.
				Trait_Name_From_ListView(ref spec, ref name);
				if (trait_ref.get_TraitID(name) == ID) {
					return index;
				}
				index++;
			}
			return -1;
		}

		// Copy and paste from MainForm
		private void Trait_Name_From_ListView(ref string spec, ref string name) {
			int spec_index = name.IndexOf('[');
			if (spec_index != -1) {
				// That means there is a Specification
				int end_spec = name.IndexOf(']');
				int length = end_spec - spec_index - 1;
				spec = name.Substring(spec_index + 1, length);
				// Replace the name of spec with "SPEC"
				name = name.Replace(spec, "SPEC");
			}
		}

		private void Add_Trait_comboBox(ref ComboBox list, Traits.Trait_Name ID, ListView traits_list) {
			int index = Contains_Trait_AtIndex(ID, traits_list);
			string name = traits_list.Items[index].SubItems[0].Text;
			list.Items.Add(name);
		}

		#endregion

		#region Update Functions

		private void Update_Branched_Check() {
			if (checkBox_Branched.Checked) {
				textBox_TechBranched.Enabled = true;
				numericUpDown_PointsBranch.Enabled = true;
			}
			else {
				textBox_TechBranched.Enabled = false;
				textBox_TechBranched.Clear();
				numericUpDown_PointsBranch.Enabled = false;
				numericUpDown_PointsBranch.Value = 0;
			}
			// Used when Branched is checked/unchecked
		}

		private void Update_Stats_Check() {
			if (!checkBox_Stats.Checked) {
				numericUpDown_PlusStats.Enabled = true;
				numericUpDown_MinusStats.Enabled = true;
				comboBox_PlusStat.Enabled = true;
				comboBox_PlusStat.Text = "Str";
				comboBox_MinusStat.Enabled = true;
				comboBox_MinusStat.Text = "Str";
			}
			else {
				numericUpDown_PlusStats.Enabled = false;
				numericUpDown_PlusStats.Value = 0;
				numericUpDown_MinusStats.Enabled = false;
				numericUpDown_MinusStats.Value = 0;
				comboBox_PlusStat.Enabled = false;
				comboBox_PlusStat.Text = "";
				comboBox_MinusStat.Enabled = false;
				comboBox_MinusStat.Text = "";
			}
			// Used when Stats is checked/unchecked
		}

		private void Update_TPMsg() {
			string message = "";
			// Affect Rank techs
			if (checkBox_Sig.Checked) {
				message += "Signature Technique. ";
			}
			if (trait_ref.get_TraitID(comboBox_AffectRank.Text) == Traits.Trait_Name.DEV_FRUIT) {
				message += "Devil Fruit Technique. ";
			}
			if (trait_ref.get_TraitID(comboBox_AffectRank.Text) == Traits.Trait_Name.MARTIAL_MASTERY ||
				trait_ref.get_TraitID(comboBox_AffectRank.Text) == Traits.Trait_Name.ADV_MARTIAL_MASTERY ||
				trait_ref.get_TraitID(comboBox_AffectRank.Text) == Traits.Trait_Name.STANCE_MAST ||
				trait_ref.get_TraitID(comboBox_AffectRank.Text) == Traits.Trait_Name.ART_OF_STEALTH ||
				trait_ref.get_TraitID(comboBox_AffectRank.Text) == Traits.Trait_Name.ANTI_STEALTH ||
				trait_ref.get_TraitID(comboBox_AffectRank.Text) == Traits.Trait_Name.DWARF) {
				message += comboBox_AffectRank.Text + " Technique, treated 4 Ranks higher. ";
			}
			// Branch message
			if (checkBox_Branched.Checked) {
				message += numericUpDown_PointsBranch.Value.ToString() + " points branched from " + textBox_TechBranched.Text + ". ";
			}
			// Special TP usage.
			if (numericUpDown_SpTP.Value > 0) {
				message += numericUpDown_SpTP.Value + " Sp. TP used from " + comboBox_SpTrait.Text + ".";
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
			numericUpDown_Power.Maximum = max_rank;
			numericUpDown_RegTP.Maximum = max_rank;
			numericUpDown_SpTP.Maximum = max_rank;
			numericUpDown_PointsBranch.Maximum = max_rank - 1;
			// If Signature Tech, enable the button.
			if (Contains_Trait_AtIndex(Traits.Trait_Name.SIG_TECH, traits_list) != -1) {
				checkBox_Sig.Enabled = true;
			}
			// Add Traits Affecting Rank
			if (Contains_Trait_AtIndex(Traits.Trait_Name.DWARF, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.DWARF, traits_list);
			}
			if (Contains_Trait_AtIndex(Traits.Trait_Name.MARTIAL_MASTERY, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.MARTIAL_MASTERY, traits_list);
			}
			if (Contains_Trait_AtIndex(Traits.Trait_Name.ADV_MARTIAL_MASTERY, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.ADV_MARTIAL_MASTERY, traits_list);
			}
			if (Contains_Trait_AtIndex(Traits.Trait_Name.STANCE_MAST, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.STANCE_MAST, traits_list);
			}
			if (Contains_Trait_AtIndex(Traits.Trait_Name.ART_OF_STEALTH, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.ART_OF_STEALTH, traits_list);
			}
			if (Contains_Trait_AtIndex(Traits.Trait_Name.ANTI_STEALTH, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.ANTI_STEALTH, traits_list);
			}
			if (Contains_Trait_AtIndex(Traits.Trait_Name.DEV_FRUIT, traits_list) != -1) {
				Add_Trait_comboBox(ref comboBox_AffectRank, Traits.Trait_Name.DEV_FRUIT, traits_list);
			}
			// Add Special TP Traits
			foreach (ListViewItem eachItem in SpTraits_list.Items) {
				string name = eachItem.SubItems[0].Text;
				string spec = ""; // trash variable
				Trait_Name_From_ListView(ref spec, ref name);
				Add_Trait_comboBox(ref comboBox_SpTrait, trait_ref.get_TraitID(name), SpTraits_list);
			}
			// Check if SpTrait_comboBox is empty
			// We can do so by checking if the size of comboBox is 1, since that's just the blank item one.
			if (comboBox_SpTrait.Items.Count != 1) {
				numericUpDown_SpTP.Enabled = true;
				label_SpTPMsg.Visible = false;
				comboBox_SpTrait.Enabled = true;
			}
		}

		private void button11_Click(object sender, EventArgs e) {
			// Only want the appropriate changes to be made, so we add a bool
			this.Close();
			button_clicked = true;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e) {
			// Checkbox for Branched
			Update_Branched_Check();
			Update_TPMsg();
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e) {
			// Checkbox for Stats
			Update_Stats_Check();
		}

		private void checkBox_Sig_CheckedChanged(object sender, EventArgs e) {
			// When signature is checked.
			if (checkBox_Sig.Checked) {
				numericUpDown_Rank.Value = max_rank;
				numericUpDown_Rank.Enabled = false;
				numericUpDown_RegTP.Value = 0;
				numericUpDown_SpTP.Value = 0;
				numericUpDown_RegTP.Enabled = false;
				numericUpDown_SpTP.Enabled = false;
			}
			else {
				numericUpDown_Rank.Value = 1;
				numericUpDown_Rank.Enabled = true;
				numericUpDown_RegTP.Enabled = true;
				numericUpDown_SpTP.Enabled = true;
			}
			Update_TPMsg();
		}

		private void textBox_TechBranched_TextChanged(object sender, EventArgs e) {
			Update_TPMsg();
		}

		private void numericUpDown_PointsBranch_ValueChanged(object sender, EventArgs e) {
			Update_TPMsg();
		}

		private void numericUpDown_SpTP_ValueChanged(object sender, EventArgs e) {
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
				checkBox_Sig.Checked = false;
				checkBox_Branched.Checked = false;
				textBox_TechBranched.Clear();
				numericUpDown_PointsBranch.Value = 0;
				numericUpDown_RegTP.Value = 0;
				numericUpDown_SpTP.Value = 0;
				comboBox_SpTrait.Text = "";
				comboBox_Type.Text = "Offensive";
				comboBox_Range.Text = "Melee";
				numericUpDown_Power.Value = 0;
				checkBox_Stats.Checked = false;
				numericUpDown_PlusStats.Value = 0;
				comboBox_PlusStat.Text = "Str";
				numericUpDown_MinusStats.Value = 0;
				comboBox_MinusStat.Text = "Str";
				textBox_TPMsg.Clear();
				richTextBox_Desc.Clear();
			}
		}

		private void numericUpDown_Rank_ValueChanged(object sender, EventArgs e) {
			// Just to change it along for convenience.
			numericUpDown_RegTP.Value = numericUpDown_Rank.Value;
		}

		#endregion
	}
}
