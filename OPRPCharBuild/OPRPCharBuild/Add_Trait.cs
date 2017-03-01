using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OPRPCharBuild
{
	public partial class Add_Trait : Form
	{
		// Utilized for grabbing information from traits
		private bool button_clicked;
        
		public Add_Trait() {
			InitializeComponent();
			button_clicked = false;
            // Initialize with every Effect
            comboBox_TraitName.Items.AddRange(Database.getTraitNames().ToArray());
		}

		// Now functions

		public string NewDialog(ref ListView Main_Form, ref Dictionary<string, Trait> traitList) {
			this.ShowDialog();
			if (button_clicked) {
				// All the necessary information is in. We can update the ListView in Main_Form
				ListViewItem item = new ListViewItem();
				string name = comboBox_TraitName.Text;
				// Adding the specification to the string name, if there is one.
				name = name.Replace("SPEC", textBox_TraitSpec.Text);
                int gen = (int)numericUpDown_TraitGen.Value;
                int prof = (int)numericUpDown_TraitProf.Value;
                // Add to your Trait type here
                string traitType;
                if (gen > 0 && prof > 0) {
                    traitType = "General / Professional";
                }
                else if (prof > 0) {
                    traitType = "Professional";
                }
                else {
                    traitType = "General";
                }
                string desc = richTextBox_TraitDesc.Text;
                // Add in the Item into ListView
                item.SubItems[0].Text = name;       // First Column: Trait Name
				item.SubItems.Add(traitType);       // Second Column: Trait Type
				item.SubItems.Add(gen.ToString());  // Third Column: # Gen
				item.SubItems.Add(prof.ToString()); // Fourth Column: # Prof
				item.SubItems.Add(desc);            // Fifth Column: Description
				Main_Form.Items.Add(item);
				Main_Form.ListViewItemSorter = new ListViewItemSorter(1);
				Main_Form.Sort();
                // And lastly add to the TraitsList
                Trait addTrait = new Trait(name, gen, prof, desc);
                try { traitList.Add(name, addTrait); }
                catch {
                    MessageBox.Show("Can't add two Traits of the same name.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
				return name;
			}
			return null;
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
            string traitName = comboBox_TraitName.Text;
			if (traitName.Contains("SPEC")) {
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
            Trait selTrait = Database.getTrait(comboBox_TraitName.Text);
			if (selTrait != null) {
				numericUpDown_TraitGen.Value = selTrait.genNum;
				numericUpDown_TraitProf.Value = selTrait.profNum;
				richTextBox_TraitDesc.Text = selTrait.desc;
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
				string.IsNullOrWhiteSpace(richTextBox_TraitDesc.Text) ||
				(textBox_TraitSpec.Enabled && string.IsNullOrWhiteSpace(textBox_TraitSpec.Text)));
		}

		private bool Zero_Traits() {
			return ((numericUpDown_TraitGen.Enabled && numericUpDown_TraitGen.Value == 0) ||
				(numericUpDown_TraitProf.Enabled && numericUpDown_TraitProf.Value == 0));
		}

	}
}
