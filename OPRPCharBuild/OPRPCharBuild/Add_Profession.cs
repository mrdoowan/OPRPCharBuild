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

		private Dictionary<string, Professions> Prof_Dict = new Dictionary<string, Professions>() {
			#region Dictionary on Professions
			{"Weapon Specialist", new Professions(
				"A much more general fighter-type profession, a Weapon Specialist" +
				" is someone who has to a large or small degree devoted his/her life to the mastery of a single weapon " +
				"or weapon type, such as Swords, Axes or Whips. This profession applies generally to melee weapons.",
				"Characters with this profession have access to certain additional traits" +
				" and may create \"Stance\" techniques.")},
			{"Martial Artist", new Professions(
				"A Martial Artist is someone who specializes in hand-to-hand combat, completely " +
				"or very close to unarmed. They usually have good insight in the aspects of weight, balance and movement of the body.",
				"Characters with this profession have access to certain additional traits" +
				" and may create \"Stance\" techniques.")},
			{"Marksman", new Professions(
				"The profession of Marksman provides sound experience and insight in calculating range," +
				" distance and wind elements and their impact on the path of a projectile. It is a general trait that applies to guns, " +
				"slingshots, cannons, rifles and the like. People without this profession will have little to no luck in firing cannonballs " +
				"where they are supposed to go under normal battle conditions.",
				"Characters with this profession have access to certain additional traits and their range " +
				"techniques will have the effect costs of the tier below with short range being free.")},
			{"Smith", new Professions(
				"A Smith is a man or woman who makes weapons and tools out of metal. Generally swords, since they " +
				"fetch the best price, but it is in no way limited to this. Making cannons and parts for inventors is also part of their business, " +
				"and they also get good knowledge within metallurgy which allows them to make custom materials or mix new ones together.",
				"A smith is proficient in any melee weapon they make, and have no maximum rank limit for martial " +
				"techniques involving these weapons. They have access to certain additional traits as well.")},
			{"Carpenter", new Professions(
				"A Carpenter is someone who makes a living crafting things such as ships, submarines, buildings and other " +
				"physical structures with expertise and materials not privy to non-carpenters. Carpenters are the only ones capable of creating ships with techniques.",
				"A carpenter is proficient in the use of the tools of their trade as weapons, and have no maximum rank " +
				"limit for martial techniques involving these tools. Furthermore, carpenters are able to use techniques that enable the rapid construction " +
				"of basic, temporary structures such as walls, bridges and ladders. Carpenters have access to certain additional traits.")},
			{"Inventor", new Professions(
				"An Inventor is a man or woman with knowledge in how various mechanical devices work. They have good " +
				"insight in how blueprints work, and can upgrade weapons, build gadgets and battle engines if they have enough materials.",
				"Inventors gain the ability to create explosives that they may use in battle and the AoE of their " +
				"explosive techniques will have the effect costs of the tier below with short AoE being free. Inventors have access to certain additional traits.")},
			{"Chef", new Professions(
				"A Chef, Cook, Bartender or any other distinct related profession gives kitchen skills and knowledge about " +
				"food, flavoring, ingredients, drink, and nutrients. They are masters at making the most out of whatever ingredients are available, and " +
				"know how to create a balanced diet for the crew that will keep everyone in fighting trim.",
				"Chefs can also create foods which strengthen those who eat it, granting buffs to their allies.")},
			{"Entertainer", new Professions(
				"An Entertainer makes his/her living through putting on shows. This profession is always specialized " +
				"towards a specific type, and includes Dancer, Musician, Juggler and many more. Knowledge granted is job-specific.",
				"Entertainers can use their performance skills, be they song, dance or anything else, to influence " +
				"others and use them as various buffs.")},
			{"Doctor", new Professions(
				"A Doctor, on a ship or otherwise, gains large medical knowledge. Suturing and cleaning wounds, " +
				"making bandages out of various materials and preparing drugs, is all part of a Doctor's job. Doctors are the only ones capable " +
				"of healing serious injuries on the crew.",
				"Doctors have the ability to create drugs and toxins that can buff allies or debuff enemies.")},
			{"Assassin", new Professions(
				"The Assassin profession gives stealth skills, the ability to move silently through areas without " +
				"being detected. It also gives general poison knowledge. An Assassin generally has little actual combat experience, since their " +
				"victims don't fight back, and as such don't fare that well in a true fight.",
				"Assassins are able to create poisons that debuff, and have access to stealth techniques. Additionally " +
				"they will be proficient in small weapons such as daggers or blow darts.")},
			{"Thief", new Professions(
				"The Thief is a job closely related to the assassin. They both provide stealth capabilities, though a " +
				"Thief gains lockpicking skills instead of poison knowledge, can generally know their way around the ways of barter and trade, and " +
				"pickpocketing. Thieves don't have it hard to find 'underground info' in most cities, either.",
				"Thieves will gain an extra 10% in Beli rewards in all Storylines they complete (for themselves). They " +
				"also have access to stealth techniques.")},
			{"Merchant", new Professions(
				"Merchants are fairly charismatic people who are skilled in both bargaining and selling things to others. " +
				"Their expertise means that they are very knowledgeable of the economy, and deal with all manner of merchandise. Additionally, " +
				"they they tend to be well-connected in trading circles.",
				"Merchants can get a 15% discount on any purchases they make, no matter what the item, for themselves. " +
				"Additionally, a merchant may purchase any item that is available to be purchased, regardless of the normal requirements.")}
			#endregion
		};

		private void button1_Click(object sender, EventArgs e) {
			// Only want the appropriate changes to be made, so we add a bool
			if (string.IsNullOrWhiteSpace(comboBox1.Text) ||
				string.IsNullOrWhiteSpace(richTextBox1_Desc.Text) ||
				(string.IsNullOrWhiteSpace(richTextBox2_Primary.Text) && checkBox1.Checked)) {
				if (string.IsNullOrWhiteSpace(comboBox1.Text)) {
					comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
					red_name = true;
				}
				if (string.IsNullOrWhiteSpace(richTextBox1_Desc.Text)) {
					richTextBox1_Desc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
					red_desc = true;
				}
				if ((string.IsNullOrWhiteSpace(richTextBox2_Primary.Text) && checkBox1.Enabled)) {
					richTextBox2_Primary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
					red_bon = true;
				}
				MessageBox.Show("Please do not enter in a blank Profession.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else {
				this.Close();
				button_clicked = true;
			}
		}

		public void NewDialog(ref ListView Main_Form) {
			this.ShowDialog();
			if (button_clicked) {
				try {
					string name = comboBox1.Text;
					bool primary = false;
					if (checkBox1.Checked) {
						primary = true;
					}
					MainForm.ProfList.Add(name, primary);
					// Check above for any Exceptions
					ListViewItem item = new ListViewItem();
					item.SubItems[0].Text = comboBox1.Text;     // First column: Profession (Why does it do this?)
					if (primary) {                    // Second column: Primary/Secondary
						item.SubItems.Add("Primary");
					}
					else {
						item.SubItems.Add("Secondary");
					}
					item.SubItems.Add(richTextBox1_Desc.Text);      // Third column: Basic description
					item.SubItems.Add(richTextBox2_Primary.Text);   // Fourth column: Primary bonus
					Main_Form.Items.Add(item);
				}
				catch (Exception e) {
					MessageBox.Show("Can't add the same profession twice.\nReason: " + e.Message, "Exception Thrown");
				}
			}
		}

		public void EditDialog(ref ListView Main_Form) {
			this.Text = "Edit Profession";
			button1.Text = "Edit";
			// Put what's Edited into the Dialog Box first.
			MainForm.ProfList.Remove(Main_Form.SelectedItems[0].SubItems[0].Text);
			comboBox1.Text = Main_Form.SelectedItems[0].SubItems[0].Text;
			if (Main_Form.SelectedItems[0].SubItems[1].Text == "Primary") {
				checkBox1.Checked = true;
			}
			else if (Main_Form.SelectedItems[0].SubItems[1].Text == "Secondary") {
				checkBox1.Checked = false;
			}
			richTextBox1_Desc.Text = Main_Form.SelectedItems[0].SubItems[2].Text;
			richTextBox2_Primary.Text = Main_Form.SelectedItems[0].SubItems[3].Text;
			// Now proceed to edit it.
			this.ShowDialog();
			if (button_clicked) {
				Main_Form.SelectedItems[0].SubItems[0].Text = comboBox1.Text;
				string name = comboBox1.Text;
				bool primary = false;
				if (checkBox1.Checked) {
					Main_Form.SelectedItems[0].SubItems[1].Text = "Primary";
					primary = true;
				}
				else {
					Main_Form.SelectedItems[0].SubItems[1].Text = "Secondary";
				}
				Main_Form.SelectedItems[0].SubItems[2].Text = richTextBox1_Desc.Text;
				Main_Form.SelectedItems[0].SubItems[3].Text = richTextBox2_Primary.Text;
				MainForm.ProfList.Add(name, primary);
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
			// If any of the listed professions were selected, immediately give the Descriptions
			string Prof = comboBox1.Text;
			richTextBox1_Desc.Text = Prof_Dict[Prof].desc;
			richTextBox2_Primary.Text = Prof_Dict[Prof].bonus;
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
				if (Prof_Dict.ContainsKey(comboBox1.Text))
					richTextBox2_Primary.Text = Prof_Dict[comboBox1.Text].bonus;
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
