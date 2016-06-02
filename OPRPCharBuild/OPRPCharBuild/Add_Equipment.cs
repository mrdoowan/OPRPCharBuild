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
	public partial class Add_Equipment : Form
	{
		private bool button_clicked;

		public Add_Equipment() {
			InitializeComponent();
			button_clicked = false;
		}

		private void button1_Click(object sender, EventArgs e) {
			// Only want the appropriate changes to be made, so we add a bool
			this.Close();
			button_clicked = true;
		}

		private void Add_ListItem(ref ListView Main_Form) {
			this.ShowDialog();
			if (button_clicked) {
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = textBox1.Text;      // First column: Name of Weapon/Item
				item.SubItems.Add(richTextBox1.Text);       // Second column: Description
				Main_Form.Items.Add(item);
			}
		}

		private void Edit_ListItem(ref ListView Main_Form) {
			button1.Text = "Edit";
			// Put what we're editing into the Dialog
			textBox1.Text = Main_Form.SelectedItems[0].SubItems[0].Text;
			richTextBox1.Text = Main_Form.SelectedItems[0].SubItems[1].Text;
			// Now we can edit the Dialog
			this.ShowDialog();
			if (button_clicked) {
				Main_Form.SelectedItems[0].SubItems[0].Text = textBox1.Text;
				Main_Form.SelectedItems[0].SubItems[1].Text = richTextBox1.Text;
			}
		}

		public void Add_Weapon(ref ListView Main_Form) {
			this.Text = "Add Weapon";
			Add_ListItem(ref Main_Form);
		}

		public void Add_Item(ref ListView Main_Form) {
			this.Text = "Add Item";
			Add_ListItem(ref Main_Form);
		}

		public void Edit_Weapon(ref ListView Main_Form) {
			this.Text = "Edit Weapon";
			Edit_ListItem(ref Main_Form);
		}

		public void Edit_Item(ref ListView Main_Form) {
			this.Text = "Edit Item";
			Edit_ListItem(ref Main_Form);
		}
	}
}
