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
	public partial class Add_Achievement : Form
	{
		public Add_Achievement() {
			InitializeComponent();
			button_clicked = false;
		}

		private bool button_clicked;

		private void button1_Click(object sender, EventArgs e) {
			// I only want this button to make the appropriate changes.
			this.Close();
			button_clicked = true;
		}

		public void NewDialog(ref ListBox Main_Form) {
			this.ShowDialog();
			if (button_clicked) {
				Main_Form.Items.Add(richTextBox1.Text);
			}
		}

		public void EditDialogue(ref ListBox Main_Form, int index) {
			this.Text = "Edit Achievement";
			button1.Text = "Edit";
			richTextBox1.Text = Main_Form.Items[index].ToString();
			this.ShowDialog();
			if (button_clicked) {
				Main_Form.Items[index] = richTextBox1.Text;
			}
		}
	}
}
