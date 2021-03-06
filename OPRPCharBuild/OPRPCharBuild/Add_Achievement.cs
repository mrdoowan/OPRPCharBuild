﻿using System;
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
	public partial class Add_Achievement : Form
	{
		public Add_Achievement() {
			InitializeComponent();
			button_clicked = false;
			red_text = false;
		}

		private bool button_clicked;
		private bool red_text;

		private void button1_Click(object sender, EventArgs e) {
			// I only want this button to make the appropriate changes.
			if (string.IsNullOrWhiteSpace(richTextBox1.Text)) {
				// Prompt the user to make fill in something first.
				this.richTextBox1.BackColor = Color.FromArgb(255, 128, 128);
				red_text = true;
				MessageBox.Show("Please fill out an Achievement.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else {
				this.Close();
				button_clicked = true;
			}
		}

        // Adds a new item. Returns true if added, otherwise false
		public bool NewDialog(ref ListBox Main_Form) {
			this.ShowDialog();
			if (button_clicked) {
				Main_Form.Items.Add(richTextBox1.Text);
                return true;
			}
            return false;
		}

        // Edits a new item. Returns true if edited, otherwise false
        public bool EditDialogue(ref ListBox Main_Form, int index) {
			this.Text = "Edit Achievement";
			button1.Text = "Edit";
            string oldAch = Main_Form.Items[index].ToString();
            richTextBox1.Text = oldAch;
			this.ShowDialog();
			if (button_clicked) {
				Main_Form.Items[index] = richTextBox1.Text;
                return true;
			}
            return false;
		}

		private void richTextBox1_TextChanged(object sender, EventArgs e) {
			if (red_text) {
				this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
				red_text = false;
			}
		}
	}
}
