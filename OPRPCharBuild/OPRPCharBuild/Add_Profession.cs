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
	public partial class Add_Profession : Form
	{
		private bool button_clicked;
		private bool red_name;
		private bool red_desc;
		private bool red_bon;

		private struct Professions
		{
			public string desc;
			public string bonus;

			public Professions(string desc_, string bonus_) {
				desc = desc_;
				bonus = bonus_;
			}
		}

		public Add_Profession() {
			InitializeComponent();
			button_clicked = false;
		}
        
		private void button1_Click(object sender, EventArgs e) {
			// Only want the appropriate changes to be made, so we add a bool
			if (string.IsNullOrWhiteSpace(comboBox_Prof.Text) ||
				string.IsNullOrWhiteSpace(richTextBox1_Desc.Text) ||
				(string.IsNullOrWhiteSpace(richTextBox2_Primary.Text) && checkBox_Primary.Checked)) {
				if (string.IsNullOrWhiteSpace(comboBox_Prof.Text)) {
					comboBox_Prof.BackColor = Color.FromArgb(255, 128, 128);
					red_name = true;
				}
				if (string.IsNullOrWhiteSpace(richTextBox1_Desc.Text)) {
					richTextBox1_Desc.BackColor = Color.FromArgb(255, 128, 128);
					red_desc = true;
				}
				if ((string.IsNullOrWhiteSpace(richTextBox2_Primary.Text) && checkBox_Primary.Enabled)) {
					richTextBox2_Primary.BackColor = Color.FromArgb(255, 128, 128);
					red_bon = true;
				}
				MessageBox.Show("Please do not enter in a blank Profession.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else {
				this.Close();
				button_clicked = true;
			}
		}

        // Returns true if a new Profession is added
		public bool NewDialog(ref DataGridView dgv, ref Dictionary<string, Profession> profDict) {
			this.ShowDialog();
			if (button_clicked) {
				try {
					string name = comboBox_Prof.Text;
					bool primary = checkBox_Primary.Checked;
                    Profession addProf = new Profession(name, primary, textBox_Custom.Text,
                        richTextBox1_Desc.Text, richTextBox2_Primary.Text);
					profDict.Add(name, addProf);
                    // Add into dgv
                    string bool_str = (primary) ? "Primary" : "Secondary";
                    dgv.Rows.Insert(0, name, textBox_Custom.Text, bool_str,
                        richTextBox1_Desc.Text, richTextBox2_Primary.Text);
                    return true;
				}
				catch (Exception e) {
					MessageBox.Show("Can't add the same profession twice.\nReason: " + e.Message, "Exception Thrown");
				}
			}
            return false;
		}

        // Returns true if a new Profession is edited
        public bool EditDialog(ref DataGridView dgv, ref Dictionary<string, Profession> profDict) {
			this.Text = "Edit Profession";
			button_Add.Text = "Edit";
            // Put what's Edited into the Dialog Box first.
            string prof_name = dgv.SelectedRows[0].Cells[0].Value.ToString();
            Profession sel_prof = profDict[prof_name];
			comboBox_Prof.Text = prof_name;
			checkBox_Primary.Checked = sel_prof.primary;
            textBox_Custom.Text = sel_prof.custom;
			richTextBox1_Desc.Text = sel_prof.desc;
			richTextBox2_Primary.Text = sel_prof.bonus;
			// Now proceed to edit it.
			this.ShowDialog();
			if (button_clicked) {
                try {
                    // Remove initial item from Dictionary
                    profDict.Remove(prof_name);
                    string name = comboBox_Prof.Text;
                    bool primary = checkBox_Primary.Checked;
                    // Add to dgv
                    dgv.SelectedRows[0].Cells[0].Value = (string.IsNullOrWhiteSpace(textBox_Custom.Text)) ? comboBox_Prof.Text : textBox_Custom.Text;
                    dgv.SelectedRows[0].Cells[1].Value = textBox_Custom.Text;
                    dgv.SelectedRows[0].Cells[2].Value = (primary) ? "Primary" : "Secondary";
                    dgv.SelectedRows[0].Cells[3].Value = richTextBox1_Desc.Text;
                    dgv.SelectedRows[0].Cells[4].Value = richTextBox2_Primary.Text;
                    // Add to Dict
                    Profession editProf = new Profession(name, primary, textBox_Custom.Text,
                        richTextBox1_Desc.Text, richTextBox2_Primary.Text);
                    profDict.Add(name, editProf);
                    return true;
                }
                catch (Exception e) {
                    profDict.Add(prof_name, sel_prof); // Re-Add
                    dgv.SelectedRows[0].Cells[0].Value = prof_name;
                    dgv.SelectedRows[0].Cells[1].Value = sel_prof.custom;
                    dgv.SelectedRows[0].Cells[2].Value = (sel_prof.primary) ? "Primary" : "Secondary";
                    dgv.SelectedRows[0].Cells[3].Value = sel_prof.desc;
                    dgv.SelectedRows[0].Cells[4].Value = sel_prof.bonus;
                    MessageBox.Show("Can't add the same profession twice.\nReason: " + e.Message, "Exception Thrown");
                }
            }
            return false;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
			// If any of the listed professions were selected, immediately give the Descriptions
			string Prof = comboBox_Prof.Text;
            Profession selProf = Database.getProfession(Prof);
            richTextBox1_Desc.Text = selProf.desc;
			richTextBox2_Primary.Text = selProf.bonus;
			if (!checkBox_Primary.Checked) {
				richTextBox2_Primary.Clear();
			}
			// To clear the possible red background
			if (red_name) {
				comboBox_Prof.BackColor = Color.FromArgb(255, 255, 255);
				red_name = false;
			}
		}

		private void comboBox1_TextUpdate(object sender, EventArgs e) {
			if (red_name) {
				comboBox_Prof.BackColor = Color.FromArgb(255, 255, 255);
				red_name = false;
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e) {
			// Only used to disable / enable the Primary Text Box
			if (checkBox_Primary.Checked) {
				richTextBox2_Primary.Enabled = true;
                Profession prof = Database.getProfession(comboBox_Prof.Text);
				if (prof != null)
					richTextBox2_Primary.Text = prof.bonus;
			}
			else {
				richTextBox2_Primary.Enabled = false;
				richTextBox2_Primary.Clear();
			}
		}

		private void richTextBox1_Desc_TextChanged(object sender, EventArgs e) {
			if (red_desc) {
				richTextBox1_Desc.BackColor = Color.FromArgb(255, 255, 255);
				red_desc = false;
			}
		}

		private void richTextBox2_Primary_TextChanged(object sender, EventArgs e) {
			if (red_bon) {
				richTextBox2_Primary.BackColor = Color.FromArgb(255, 255, 255);
				red_bon = false;
			}
		}
	}
}
