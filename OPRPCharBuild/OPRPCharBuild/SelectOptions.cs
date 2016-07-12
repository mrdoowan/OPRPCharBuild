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
		private int option;
		private bool Has_Roku_Master;
		private int max_rank;                   // The current max rank the character is able to have. (if base rank of tech is above max rank, nope)
		private Rokushiki.RokuName Sel_Roku;    // The selected Rokushiki for loading
		private string Custom_Name;
		private string Sel_Version;
		private bool OK_clicked;
		private bool Left_clicked;
		private string Template_Name;

		public const string ProjectStr = "v1.1.0 (Project)";

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

		public string Import_Version_Dialog() {
			this.ShowDialog();
			if (OK_clicked) {
				return Sel_Version;
			}
			else {
				return null;
			}
		}

		public string Rokushiki_Load_Dialog(ref Rokushiki.RokuName Selected, ref string Roku_Name) {
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
			if (option == 1) {
				// Import previous version option.
				this.Text = "Import Older Files";
				textBox_Name.Visible = false;
				label_Name.Visible = false;
				button_Left.Visible = false;
				toolTip1.Active = false;

				// Add previous versions.
				comboBox_Options.Items.Add(ProjectStr);

				// Set other variables
				label_Msg.Text = "Current .oprp File Extension Form is: " + MainForm.curr_proj + "\nSelect version of the file you want to import.";
				button_OK.Text = "Import";
			}
			else if (option == 2) {
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
			else if (option == 3) {
				// Generate Character Template
				this.Text = "Select Character Template";
				label_Msg.Text = "No imported character template detected.";
				button_Left.Text = "Import";
				button_OK.Text = "Generate";
				textBox_Name.Visible = false;
				label_Name.Visible = false;
				toolTip1.Active = false;
			}
			else {
				MessageBox.Show("Option " + option + " not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}

		// Button for Customization
		private void button_Left_Click(object sender, EventArgs e) {
			if (option == 2) {
				if (comboBox_Options.SelectedIndex == -1) { MessageBox.Show("Please select a Rokushiki Technique to customize before continuing.", "Error"); }
				else {
					Custom_Name = textBox_Name.Text;
					this.Close();
					Left_clicked = true;
				}
			}
		}

		// Button to Add/Import depending on Option
		private void button_OK_Click(object sender, EventArgs e) {
			if (option == 1) {
				// For Importing
				string Version_Selected = comboBox_Options.Text;
				if (comboBox_Options.SelectedIndex == -1) { MessageBox.Show("Please select a version before continuing.", "Error"); }
				else {
					Sel_Version = comboBox_Options.Text;
					this.Close();
					OK_clicked = true;
				}
			}
			else if (option == 2) {
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
					Sel_Roku = Roku.Get_RokuEnum(comboBox_Options.Text);
					Rokushiki.RokuInfo Info_Roku = Roku.Get_RokuInfo(Sel_Roku);
					if (Info_Roku.baseRank > max_rank) {
						button_Left.Enabled = false;
						button_OK.Enabled = false;
						label_Msg.ForeColor = Color.Red;
						label_Msg.Text = Info_Roku.name + "'s Base Rank is " + Info_Roku.baseRank + '\n'
							+ "You can only make Techniques at Max Rank " + max_rank;
					}
					else {
						if (!Has_Roku_Master) {
							button_Left.Enabled = false;
						}
						button_OK.Enabled = true;
						label_Msg.ForeColor = SystemColors.ControlText;
						label_Msg.Text = Info_Roku.name + "'s Base Rank is " + Info_Roku.baseRank + '\n';
					}
				}
			}
		}

		#endregion

	}
}
