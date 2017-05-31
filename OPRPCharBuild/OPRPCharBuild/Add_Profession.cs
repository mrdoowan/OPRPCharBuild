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
			if (string.IsNullOrWhiteSpace(comboBox1.Text) ||
				string.IsNullOrWhiteSpace(richTextBox1_Desc.Text) ||
				(string.IsNullOrWhiteSpace(richTextBox2_Primary.Text) && checkBox1.Checked)) {
				if (string.IsNullOrWhiteSpace(comboBox1.Text)) {
					comboBox1.BackColor = Color.FromArgb(255, 128, 128);
					red_name = true;
				}
				if (string.IsNullOrWhiteSpace(richTextBox1_Desc.Text)) {
					richTextBox1_Desc.BackColor = Color.FromArgb(255, 128, 128);
					red_desc = true;
				}
				if ((string.IsNullOrWhiteSpace(richTextBox2_Primary.Text) && checkBox1.Enabled)) {
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

		public void NewDialog(ref ListView Main_Form, ref Dictionary<string, Profession> profDict) {
			this.ShowDialog();
			if (button_clicked) {
				try {
					string name = comboBox1.Text;
					bool primary = false;
					if (checkBox1.Checked) {
						primary = true;
					}
                    Profession addProf = new Profession(name, primary, textBox1.Text,
                        richTextBox1_Desc.Text, richTextBox2_Primary.Text);
					profDict.Add(name, addProf);
					// Check above for any Exceptions
					ListViewItem item = new ListViewItem();
                    item.SubItems[0].Text = (string.IsNullOrWhiteSpace(textBox1.Text)) ? comboBox1.Text : textBox1.Text;
                                                                            // First Column: Profession Name
					item.SubItems.Add((primary) ? "Primary" : "Secondary"); // Second column: Primary/Secondary
                    item.SubItems.Add(richTextBox1_Desc.Text);              // Third column: Basic description
					item.SubItems.Add(richTextBox2_Primary.Text);           // Fourth column: Primary bonus
					Main_Form.Items.Add(item);
					// Now sort.
					Main_Form.ListViewItemSorter = new ListViewItemSorter(1);
					Main_Form.Sort();
				}
				catch (Exception e) {
					MessageBox.Show("Can't add the same profession twice.\nReason: " + e.Message, "Exception Thrown");
				}
			}
		}

		public void EditDialog(ref ListView Main_Form, ref Dictionary<string, Profession> profDict) {
			this.Text = "Edit Profession";
			button1.Text = "Edit";
			// Put what's Edited into the Dialog Box first.
			string prof_name = Main_Form.SelectedItems[0].SubItems[0].Text;
            Profession sel_prof = profDict[prof_name];
			comboBox1.Text = prof_name;
			checkBox1.Checked = sel_prof.primary;
            textBox1.Text = sel_prof.custom;
			richTextBox1_Desc.Text = sel_prof.desc;
			richTextBox2_Primary.Text = sel_prof.bonus;
			// Now proceed to edit it.
			this.ShowDialog();
			if (button_clicked) {
				// Remove initial item from Dictionary
				profDict.Remove(prof_name);
				string name = comboBox1.Text;
				bool primary = false;
				if (checkBox1.Checked) {
					primary = true;
				}
                Profession editProf = new Profession(name, primary, textBox1.Text,
                    richTextBox1_Desc.Text, richTextBox2_Primary.Text);
				try { profDict.Add(name, editProf); }
				catch (Exception e) {
					MessageBox.Show("Can't add the same profession twice.\nReason: " + e.Message, "Exception Thrown");
                    return;
				}
				Main_Form.SelectedItems[0].SubItems[0].Text = comboBox1.Text;
                Main_Form.SelectedItems[0].SubItems[1].Text = (primary) ? "Primary" : "Secondary";
				Main_Form.SelectedItems[0].SubItems[2].Text = richTextBox1_Desc.Text;
				Main_Form.SelectedItems[0].SubItems[3].Text = richTextBox2_Primary.Text;
				// Now sort.
				Main_Form.ListViewItemSorter = new ListViewItemSorter(1);
				Main_Form.Sort();
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
			// If any of the listed professions were selected, immediately give the Descriptions
			string Prof = comboBox1.Text;
            Profession selProf = Database.getProfession(Prof);
            richTextBox1_Desc.Text = selProf.desc;
			richTextBox2_Primary.Text = selProf.bonus;
			if (!checkBox1.Checked) {
				richTextBox2_Primary.Clear();
			}
			// To clear the possible red background
			if (red_name) {
				comboBox1.BackColor = Color.FromArgb(255, 255, 255);
				red_name = false;
			}
		}

		private void comboBox1_TextUpdate(object sender, EventArgs e) {
			if (red_name) {
				comboBox1.BackColor = Color.FromArgb(255, 255, 255);
				red_name = false;
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e) {
			// Only used to disable / enable the Primary Text Box
			if (checkBox1.Checked) {
				richTextBox2_Primary.Enabled = true;
                Profession prof = Database.getProfession(comboBox1.Text);
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
