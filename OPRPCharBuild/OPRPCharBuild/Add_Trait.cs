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

        // Returns the getName() of the new Trait. If no Trait (or invalid) was selected, return null
		public string NewDialog(ref DataGridView dgv, ref List<Trait> traitList) {
			this.ShowDialog();
			if (button_clicked) {
				// All the necessary information is in. We can update the ListView in Main_Form
				ListViewItem item = new ListViewItem();
				string name = comboBox_TraitName.Text;
                string custom = textBox_CustomName.Text;
                int gen = (int)numericUpDown_TraitGen.Value;
                int prof = (int)numericUpDown_TraitProf.Value;
                // Add Trait type here
                string traitType = (gen > 0 && prof > 0) ? "General / Professional" :
                    (prof > 0) ? "Professional" : "General";
                string desc = richTextBox_TraitDesc.Text;
                // Add to the TraitsList
                if (traitList.Any(x => x.name == name && x.custom == custom)) {
                    MessageBox.Show("Can't add two Traits of the same custom name.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                Trait addTrait = new Trait(name, custom, gen, prof, desc);
                traitList.Add(addTrait);
                // Add into dgv
                dgv.Rows.Insert(dgv.Rows.Count, name, custom, traitType, gen, prof, desc);
				return addTrait.getName();
			}
			return null;
		}

        // Returns the name of the new Trait. If no Trait (or invalid) was selected, return null
        public string EditDialog(ref DataGridView dgv, ref List<Trait> traitList) {
            this.Text = "Edit Trait";
            button_TraitAdd.Text = "Edit";
            // Load Edit from row
            string name = dgv.SelectedRows[0].Cells[0].Value.ToString();
            string custom = dgv.SelectedRows[0].Cells[1].Value.ToString();
            Trait edit_Trait = traitList.Find(x => x.name == name);
            comboBox_TraitName.Text = edit_Trait.name;
            textBox_CustomName.Text = edit_Trait.custom;
            numericUpDown_TraitGen.Value = edit_Trait.genNum;
            numericUpDown_TraitProf.Value = edit_Trait.profNum;
            richTextBox_TraitDesc.Text = edit_Trait.desc;
            this.ShowDialog();
            if (button_clicked) {
                // Remove the item
                traitList.Remove(edit_Trait);
                try {
                    string new_name = comboBox_TraitName.Text;
                    string new_cust = textBox_CustomName.Text;
                    int gen = (int)numericUpDown_TraitGen.Value;
                    int prof = (int)numericUpDown_TraitProf.Value;
                    string new_desc = richTextBox_TraitDesc.Text;
                    dgv.SelectedRows[0].Cells[0].Value = new_name;
                    dgv.SelectedRows[0].Cells[1].Value = new_cust;
                    dgv.SelectedRows[0].Cells[2].Value = (gen > 0 && prof > 0) ? "General / Professional" :
                        (prof > 0) ? "Professional" : "General";
                    dgv.SelectedRows[0].Cells[3].Value = gen;
                    dgv.SelectedRows[0].Cells[4].Value = prof;
                    dgv.SelectedRows[0].Cells[5].Value = richTextBox_TraitDesc.Text;
                    // Add to Dict
                    Trait new_Trait = new Trait(new_name, new_cust, gen, prof, new_desc);
                    if (traitList.Any(x => x.name == name && x.custom == custom)) {
                        throw new Exception(); // There is a duplicate
                    }
                    traitList.Add(new_Trait);
                    return new_Trait.getName();
                }
                catch {
                    traitList.Add(edit_Trait); // Re-Add
                    dgv.SelectedRows[0].Cells[0].Value = edit_Trait.name;
                    dgv.SelectedRows[0].Cells[1].Value = edit_Trait.custom;
                    dgv.SelectedRows[0].Cells[2].Value = (edit_Trait.genNum > 0 && edit_Trait.profNum > 0) 
                        ? "General / Professional" : (edit_Trait.profNum > 0) ? "Professional" : "General";
                    dgv.SelectedRows[0].Cells[3].Value = edit_Trait.genNum;
                    dgv.SelectedRows[0].Cells[4].Value = edit_Trait.profNum;
                    dgv.SelectedRows[0].Cells[5].Value = edit_Trait.desc;
                    MessageBox.Show("Can't add two Traits of the same custom name.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            return null;
        }

		private void comboBox_TraitName_SelectedIndexChanged(object sender, EventArgs e) {
            string traitName = comboBox_TraitName.Text;
            Trait selTrait = Database.getTrait(traitName);
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
				string.IsNullOrWhiteSpace(richTextBox_TraitDesc.Text));
		}

		private bool Zero_Traits() {
			return (numericUpDown_TraitGen.Value == 0 && numericUpDown_TraitProf.Value == 0);
		}
    }
}
