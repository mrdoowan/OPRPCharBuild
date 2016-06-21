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
		List<Traits.Trait_Name> TraitsList;
		private int max_rank;
		Rokushiki Roku = new Rokushiki();

		private const string ProjectStr = "v1.0.1.0 (Project)";

		// Default Constructor
		public SelectOptions(int option_, List<Traits.Trait_Name> Traits_, int maxRank_) {
			InitializeComponent();
			option = option_;
			TraitsList = Traits_;
			max_rank = maxRank_;
		}

		#region Dialog Functions



		#endregion 

		// When this.ShowDialog() is called
		private void SelectOptions_Load(object sender, EventArgs e) {
			if (option == 1) {
				// Import previous version option.
				this.Text = "Import Previous Version";
				textBox_Name.Visible = false;
				label_Name.Visible = false;
				button_Custom.Visible = false;
				label_MaxRank.Visible = false;

				// Add previous versions.
				comboBox_Options.Items.Add(ProjectStr);

			}
			else if (option == 2) {
				// Select Rokushiki option.
				this.Text = "Rokushiki Technique";

				comboBox_Options.Items.Add("Shigan");
				comboBox_Options.Items.Add("Rankyaku");
				comboBox_Options.Items.Add("Soru");
				comboBox_Options.Items.Add("Kami-E");
				comboBox_Options.Items.Add("Tekkai");
				comboBox_Options.Items.Add("Geppou");

				if (!TraitsList.Contains(Traits.Trait_Name.ROK_MAST)) {
					button_Custom.Enabled = false;
				}
				else {
					comboBox_Options.Items.Add("Rokugan");
				}

			}
			else {
				MessageBox.Show("Option " + option + " not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}

		private void button_Custom_Click(object sender, EventArgs e) {

		}

		private void button_OK_Click(object sender, EventArgs e) {

		}

		private void comboBox_Options_SelectedIndexChanged(object sender, EventArgs e) {
			if (option == 2) {
				Rokushiki.RokuInfo Info = Roku.Get_RokuInfo(comboBox_Options.Text);
			}
		}
	}
}
