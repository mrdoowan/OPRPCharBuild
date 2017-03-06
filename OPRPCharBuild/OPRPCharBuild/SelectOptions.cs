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
	public partial class SelectOptions : Form
	{
		// OPTION 1 is ??? (used to be Importing)
		// OPTION 2 is Rokushiki
		private int option;
		private bool Has_Roku_Master;
		private int max_rank;                   // The current max rank the character is able to have. (if base rank of tech is above max rank, nope)
		private Rokushiki Sel_Roku;             // The selected Rokushiki for loading
		private string Custom_Name;
		private bool OK_clicked;
		private bool Left_clicked;
		private string Template_Name;

		// Default Constructor
		public SelectOptions(int option_, bool RokuMaster_ = false, int maxRank_ = 0, string template_ = null) {
			InitializeComponent();
			option = option_;
			Has_Roku_Master = RokuMaster_;
			max_rank = maxRank_;
			OK_clicked = false;
			Left_clicked = false;
			Template_Name = template_;
		}

		#region Dialog Functions

		public string Rokushiki_Load_Dialog(ref Rokushiki Selected, ref string Roku_Name) {
			this.ShowDialog();
			if (string.IsNullOrWhiteSpace(Custom_Name)) {
				// No Custom Name was selected
				Roku_Name = comboBox_Options.Text;
			}
			else {
				// That means there is a Custom Name.
				Roku_Name = Custom_Name;
			}
			if (OK_clicked) {
				Selected = Sel_Roku;
				return "Add";
			}
			if (Left_clicked) {
				Selected = Sel_Roku;
				return "Custom";
			}
			else {
				// Trash variable
				return null;
			}
		}

		#endregion

		#region Event Handlers

		// When this.ShowDialog() is called
		private void SelectOptions_Load(object sender, EventArgs e) {
			if (option == 2) {
				// Select Rokushiki option.
				this.Text = "Rokushiki Technique";
				label_Msg.Text = "Select the Rokushiki Technique you would like to Add.";

				comboBox_Options.Items.Add("Shigan");
				comboBox_Options.Items.Add("Rankyaku");
				comboBox_Options.Items.Add("Soru");
				comboBox_Options.Items.Add("Kami-E");
				comboBox_Options.Items.Add("Tekkai");
				comboBox_Options.Items.Add("Geppo");

				if (!Has_Roku_Master) {
					button_Left.Enabled = false;
				}
				else {
					comboBox_Options.Items.Add("Rokuogan");
				}
			}
			else {
				MessageBox.Show("Option " + option + " not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}

		// Button for Customization
		private void button_Left_Click(object sender, EventArgs e) {
			if (option == 2) {
				// Customize Rokushiki
				if (comboBox_Options.SelectedIndex == -1) { MessageBox.Show("Please select a Rokushiki Technique to customize before continuing.", "Error"); }
				else {
					Custom_Name = textBox_Name.Text;
					this.Close();
					Left_clicked = true;
				}
			}
		}

		// The button in the Middle, currently just serving as a purpose to reset the template
		private void button_Mid_Click(object sender, EventArgs e) {

		}

		// Button to Add/Import/Generate depending on Option
		private void button_OK_Click(object sender, EventArgs e) {
			if (option == 2) {
				// For Adding Rokushiki
				if (comboBox_Options.SelectedIndex == -1) { MessageBox.Show("Please select a Rokushiki Technique to Add before continuing.", "Error"); }
				else {
					Custom_Name = textBox_Name.Text;
					this.Close();
					OK_clicked = true;
				}
			}
		}

		private void comboBox_Options_SelectedIndexChanged(object sender, EventArgs e) {
			if (option == 2) {
				Rokushiki Roku = new Rokushiki();
				if (!string.IsNullOrWhiteSpace(comboBox_Options.Text)) {
					// That means we selected a Rokushiki Technique
					Sel_Roku = Database.getRoku(comboBox_Options.Text);
					if (Sel_Roku.baseRank > max_rank) {
						button_Left.Enabled = false;
						button_OK.Enabled = false;
						label_Msg.ForeColor = Color.Red;
						label_Msg.Text = Sel_Roku.name + "'s Base Rank is " + Sel_Roku.baseRank + '\n'
							+ "You can only make Techniques at Max Rank " + max_rank;
					}
					else {
						if (!Has_Roku_Master) {
							button_Left.Enabled = false;
						}
						button_OK.Enabled = true;
						label_Msg.ForeColor = SystemColors.ControlText;
						label_Msg.Text = Sel_Roku.name + "'s Base Rank is " + Sel_Roku.baseRank + '\n';
					}
				}
			}
		}

		#endregion

	}
}
