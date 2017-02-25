using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OPRPCharBuild
{
	public partial class Add_Trait : Form
	{
		// Utilized for grabbing information from traits
		private bool button_clicked;

        TraitClass Trait = new TraitClass();
		public Add_Trait() {
			InitializeComponent();
			button_clicked = false;
		}

		// Now functions

		public TraitName NewDialog(ref ListView Main_Form) {
			this.ShowDialog();
			if (button_clicked) {
				// All the necessary information is in. We can update the ListView in Main_Form
				ListViewItem item = new ListViewItem();
				string name = comboBox_TraitName.Text;
				TraitName ID = Trait.getTraitID(name);
				// Adding the specification to the string name, if there is one.
				if (Spec_Enabled(ID)) {
					name = name.Replace("SPEC", textBox_TraitSpec.Text);
				}
				// Add in the Item into ListView
				item.SubItems[0].Text = name;                   // First Column: Trait Name
				item.SubItems.Add(comboBox_TraitType.Text); // Second Column: Trait Type
				item.SubItems.Add(numericUpDown_TraitGen.Value.ToString()); // Third Column: # Gen
				item.SubItems.Add(numericUpDown_TraitProf.Value.ToString());// Fourth Column: # Prof
				item.SubItems.Add(richTextBox_TraitDesc.Text); // Fifth Column: Description
				Main_Form.Items.Add(item);
				Main_Form.ListViewItemSorter = new MainForm.ListViewItemSorter(1);
				Main_Form.Sort();
				// And lastly add to the TraitsList
				MainForm.TraitsList.Add(ID);
				return ID;
			}
			return TraitName.CUSTOM;
		}

        // With any Trait Names with [SPEC], we need to do the proper replace
        private string filterSpec(ref string name) {
            if (name.Contains('[') && name.Contains(']')) {
                int firstIndexSpec = name.IndexOf('[') + 1;
                string specName = name.Substring(firstIndexSpec, 
                    name.IndexOf(']') - firstIndexSpec);
                name.Replace(specName, "SPEC");
                return specName;
            }
            return "";
        }

		public TraitName EditDialog(ref ListView Main_Form) {
			this.Text = "Edit Trait";
			button_TraitAdd.Text = "Edit";
			// Put what's Edited into the Dialog Box first.
			string name = Main_Form.SelectedItems[0].SubItems[0].Text;
			string spec = filterSpec(ref name);
			comboBox_TraitName.Text = name;
			comboBox_TraitType.Text = Main_Form.SelectedItems[0].SubItems[1].Text;
			numericUpDown_TraitGen.Value = int.Parse(Main_Form.SelectedItems[0].SubItems[2].Text);
			numericUpDown_TraitProf.Value = int.Parse(Main_Form.SelectedItems[0].SubItems[3].Text);
			textBox_TraitSpec.Text = spec;
			richTextBox_TraitDesc.Text = Main_Form.SelectedItems[0].SubItems[4].Text;
			this.ShowDialog();
			if (button_clicked) {
				// Remove the initial Trait from the TraitsList first
				MainForm.TraitsList.Remove(Trait.getTraitID(name));
				// Put the information back into the ListView
				// Name first
				name = comboBox_TraitName.Text;
				TraitName ID = Trait.getTraitID(comboBox_TraitName.Text);
				if (Spec_Enabled(ID)) {
					name = name.Replace("SPEC", textBox_TraitSpec.Text);
				}
				Main_Form.SelectedItems[0].SubItems[0].Text = name;                   // First Column: Trait Name
				Main_Form.SelectedItems[0].SubItems[1].Text = comboBox_TraitType.Text; // Second Column: Trait Type
				Main_Form.SelectedItems[0].SubItems[2].Text = numericUpDown_TraitGen.Value.ToString(); // Third Column: # Gen
				Main_Form.SelectedItems[0].SubItems[3].Text = numericUpDown_TraitProf.Value.ToString();// Fourth Column: # Prof
				Main_Form.SelectedItems[0].SubItems[4].Text = richTextBox_TraitDesc.Text; // Fifth Column: Description
				// Sort it with the Edit
				Main_Form.ListViewItemSorter = new MainForm.ListViewItemSorter(1);
				Main_Form.Sort();
				// Now add the new Trait into the TraitList
				MainForm.TraitsList.Add(ID);
				return ID;
			}
			return TraitName.CUSTOM;
		}

		// To change the note and remind the users
		private void comboBox_TraitName_MouseMove(object sender, MouseEventArgs e) {
			label_CustMsg.TextAlign = ContentAlignment.MiddleCenter;
			label_CustMsg.Text = "NOTE: Custom Traits / Names will NOT be recognized by the tool when calculating.\n" +
				"You can change the Trait name after Generating the sheet.";
        }

		private void comboBox_TraitName_MouseLeave(object sender, EventArgs e) {
			label_CustMsg.TextAlign = ContentAlignment.MiddleRight;
			label_CustMsg.Text = "You can cancel changes by closing the form.";
		}

		private void comboBox_TraitName_SelectedIndexChanged(object sender, EventArgs e) {
			TraitName ID = Trait.getTraitID(comboBox_TraitName.Text);
			if (Spec_Enabled(ID)) {
				textBox_TraitSpec.Enabled = true;
			}
			else {
				textBox_TraitSpec.Enabled = false;
				textBox_TraitSpec.Clear();
			}
		}

		private void comboBox_TraitName_SelectionChangeCommitted(object sender, EventArgs e) {
			// When we close the comboBox, that means we selected a trait.
			// Utilize this to copy and display information.
			if (Trait.loadTrait(comboBox_TraitName.SelectedItem.ToString()) != TraitName.CUSTOM) {
				numericUpDown_TraitGen.Value = Trait.getGenNum();
				numericUpDown_TraitProf.Value = Trait.getProfNum();
				richTextBox_TraitDesc.Text = Trait.getTraitDesc();
			}
		}

		private bool Spec_Enabled(TraitName ID) {
			return (ID == TraitName.MARTIAL_MASTERY ||
				ID == TraitName.ADV_MARTIAL_CLASS ||
				ID == TraitName.ADV_MARTIAL_MASTERY ||
				ID == TraitName.FISHMAN ||
				ID == TraitName.DAZZLE_PERF ||
				ID == TraitName.GRAND_MARTIAL ||
				ID == TraitName.SIG_TECH);
		}

		private void comboBox_TraitType_SelectedIndexChanged(object sender, EventArgs e) {
			if (comboBox_TraitType.Text == "") {
				numericUpDown_TraitGen.Enabled = false;
				numericUpDown_TraitProf.Enabled = false;
				numericUpDown_TraitGen.Value = 0;
				numericUpDown_TraitProf.Value = 0;
			}
			else if (comboBox_TraitType.Text == "General") {
				numericUpDown_TraitGen.Enabled = true;
				numericUpDown_TraitProf.Enabled = false;
				numericUpDown_TraitProf.Value = 0;
			}
			else if (comboBox_TraitType.Text == "Professional") {
				numericUpDown_TraitGen.Enabled = false;
				numericUpDown_TraitProf.Enabled = true;
				numericUpDown_TraitGen.Value = 0;
			}
			else if (comboBox_TraitType.Text == "General / Professional") {
				numericUpDown_TraitGen.Enabled = true;
				numericUpDown_TraitProf.Enabled = true;
			}
		}

		private void button_TraitAdd_Click(object sender, EventArgs e) {
			if (Traits_Not_Filled()) {
				MessageBox.Show("Traits information incomplete!", "Trait Error");
			}
			if (Zero_Traits()) {
				MessageBox.Show("You can't add zero Traits!", "Trait Error");
			}
			else {
				this.Close();
				button_clicked = true;
			}
		}

		private bool Traits_Not_Filled() {
			// Checks if the Name, Type, or Description is filled in.
			return (string.IsNullOrWhiteSpace(comboBox_TraitName.Text) ||
				string.IsNullOrWhiteSpace(comboBox_TraitType.Text) ||
				string.IsNullOrWhiteSpace(richTextBox_TraitDesc.Text) ||
				(textBox_TraitSpec.Enabled && string.IsNullOrWhiteSpace(textBox_TraitSpec.Text)));
		}

		private bool Zero_Traits() {
			return ((numericUpDown_TraitGen.Enabled && numericUpDown_TraitGen.Value == 0) ||
				(numericUpDown_TraitProf.Enabled && numericUpDown_TraitProf.Value == 0));
		}

	}
}
