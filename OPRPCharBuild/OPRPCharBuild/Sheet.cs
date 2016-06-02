using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OPRPCharBuild
{
	// Also put Help Document in here as well.

	public partial class Sheet : Form
	{

		private StringWriter template = new StringWriter();

		public Sheet() {
			InitializeComponent();
		}

		private void button_Close_Click(object sender, EventArgs e) {
			this.Close();
		}

		public void Set_Help_Form() {
			this.Text = "Help";
			label_Title.Text = "Help Documentation";
			label1.Visible = false;

			string help = "Write stuff here\n";

			richTextBox_Template.Text = help;
		}

		// ---------------------------------------------------------------------------
		public void Basic_Generate(string name, string nick, int age, string gender, string race, string aff, string bounty, string rank,
			string comm, string threat, string pos, ListBox achieve, ListView profs) {
			template.Write("[center][big][big][big][big][font=Garamond]Character Template[/font][/big][/big][/big][/big]\n");
			template.Write('\n');
			template.Write("[-----][/center]\n");
			template.Write('\n');
			template.Write('\n');
			template.Write("[b]Account Name:[/b] [me]\n");
			template.Write('\n');
			template.Write("[center][big][big][i][font=Century Gothic]Basic Character Information[/font][/i][/big][/big][/center]\n");
			template.Write("[b]Name:[/b] " + name + '\n');
			template.Write("[b]Nickname:[/b] " + nick + '\n');
			template.Write("[b]Age:[/b] " + age + '\n');
			template.Write("[b]Gender:[/b] " + gender + '\n');
			template.Write("[b]Race:[/b] " + race + '\n');
			template.Write("[b]Affiliation:[/b] " + aff + '\n');
			if (aff == "Pirate") {
				template.Write("[b]Bounty:[/b] " + bounty + '\n');
			}
			else if (aff == "Marine") {
				template.Write("[b]Rank (Commendations):[/b] " + rank + " (" + comm + ")\n");
			}
			else {
				template.Write("[b]Threat:[/b] " + threat + '\n');
			}
			template.Write("[b]Position:[/b] " + pos + '\n');
			template.Write("[b]Achievements:[/b][list]");
			if (achieve.Items.Count == 0) {
				template.Write("[*]None");
			}
			else {
				for (int i = 0; i < achieve.Items.Count; ++i) {
					template.Write("[*]" + achieve.Items[i] + '\n');
				}
			}
			template.Write("[/list]\n");
			template.Write("[b]Profession:[/b][list]");
			foreach (ListViewItem prof in profs.Items) {
				template.Write("[*][b][u]" + prof.SubItems[0].Text + "[/u] - [i]");
				template.Write(prof.SubItems[1].Text + "[/i]:[/b] ");
				template.Write(prof.SubItems[2].Text);
				if (prof.SubItems[1].Text == "Primary") {
					template.Write(" [i]" + prof.SubItems[3].Text + "[/i]\n");
				}
			}
			template.Write("[/list]\n");
		}
		// ---------------------------------------------------------------------------
		public void Physical_Background_Generate(string height, string weight, string hair, string eye, string clothing, string appear, bool full_res, string url,
			int imgWidth, int imgHeight, string person, string island, string region, string history) {
			template.Write("[center][i][big][big][font=Century Gothic]Physical Appearance[/font][/big][/big][/i][/center]\n");
			template.Write("[b]Height:[/b] " + height + '\n');
			template.Write("[b]Weight:[/b] " + weight + '\n');
			template.Write("[b]Hair:[/b] " + hair + '\n');
			template.Write("[b]Eyes:[/b] " + eye + '\n');
			template.Write("[b]Clothing/Accessories:[/b] " + clothing + '\n');
			template.Write("[b]General Appearance:[/b] " + appear + '\n');
			template.Write('\n');
			template.Write("[spoiler][img");
			if (!full_res) {
				template.Write('=' + imgWidth + ',' + imgHeight);
			}
			template.Write(']' + url + "[/img][/spoiler]\n");
			template.Write('\n');
			template.Write("[center][big][big][i][font=Century Gothic]The Character[/font][/i][/big][/big][/center]\n");
			template.Write("[b]Personality:[/b] " + person + '\n');
			template.Write('\n');
			template.Write("[b]Hometown:[/b] " + island + ", " + region + '\n');
			template.Write("[b]History:[/b] " + history + '\n');
			template.Write('\n');
			template.Write('\n');
		}
		// ---------------------------------------------------------------------------
		public void Combat_Generate(string combat, ListView weapons, ListView items, string beli) {
			template.Write("[big][big][center][i][font=Century Gothic]Abilities and Possessions[/font][/i][/center][/big][/big]\n");
			template.Write("[b]Combat:[/b] " + combat + '\n');
			template.Write('\n');
			template.Write("[b]Weaponry:[/b][list]");
			if (weapons.Items.Count == 0) {
				template.Write("[*]None");
			}
			else {
				foreach (ListViewItem weapon in weapons.Items) {
					template.Write("[*][b]" + weapon.SubItems[0].Text + ": [/b]" + weapon.SubItems[1].Text + '\n');
				}
			}
			template.Write("[/list]\n");
			template.Write("[b]Items:[/b][list]");
			if (items.Items.Count == 0) {
				template.Write("[*]None");
			}
			else {
				foreach (ListViewItem item in items.Items) {
					template.Write("[*][b]" + item.SubItems[0].Text + ": [/b]" + item.SubItems[1].Text + '\n');
				}
			}
			template.Write("[/list]\n");
			template.Write("[b]Beli:[/b] " + beli + '\n');
			template.Write('\n');
		}
		// ---------------------------------------------------------------------------
		public void Stats_Generate(int AP_num, CheckedListBox AP, int SD_Earned, string SD_Remain, string SP, string SP_calc, string used_stat, int used_fort,
			int str_base, string str_fin, string str_calc, int spe_base, string spe_fin, string spe_calc, int sta_base, string sta_fin, string sta_calc,
			int acc_base, string acc_fin, string acc_calc, string fort, string fort_calc) {
			template.Write("[b]Advancement Points:[/b] " + AP_num);
			if (AP_num != 0) {
				template.Write("[list]");
				foreach (CheckBox checkedAP in AP.CheckedItems) {
					template.Write("[*]" + checkedAP.Text + '\n');
				}
				template.Write("[/list]");
			}
			template.Write('\n');
			template.Write("[b]SD Earned:[/b] " + SD_Earned + '\n');
			template.Write("[b]SD Remaining:[/b] " + SD_Remain + '\n');
			template.Write("[b]Stat Points:[/b] " + SP + ' ' + SP_calc + '\n');
			template.Write("[list][*][i]Used for Stats:[/i] " + used_stat + '\n');
			template.Write("[*][i]Used for Fortune:[/i] " + used_fort + '\n');
			template.Write("[list][*][i]Strength:[/i] " + str_fin);
			if (str_base != Int32.Parse(str_fin)) {
				template.Write(' ' + str_calc);
			}
			template.Write('\n');
			template.Write("[*][i]Speed:[/i] " + spe_fin);
			if (spe_base != Int32.Parse(spe_fin)) {
				template.Write(' ' + spe_calc);
			}
			template.Write('\n');
			template.Write("[*][i]Stamina:[/i] " + sta_fin);
			if (sta_base != Int32.Parse(sta_fin)) {
				template.Write(' ' + sta_calc);
			}
			template.Write('\n');
			template.Write("[*][i]Accuracy:[/i] " + acc_fin);
			if (acc_base != Int32.Parse(acc_fin)) {
				template.Write(' ' + acc_calc);
			}
			template.Write('\n');
			template.Write("[*][b][i]Fortune:[/i][/b] " + fort + ' ' + fort_calc + "[/list][/list]\n");
		}
		// ---------------------------------------------------------------------------
		public void Traits_DF_Generate(ListView traits, string DF_name, string DF_type, string DF_desc) {
			List<string> gen = new List<string>();
			List<string> prof = new List<string>();
			int gen_num = 0;
			int prof_num = 0;
			foreach (ListViewItem trait in traits.Items) {
				if (trait.SubItems[1].Text == "Professional") {
					// Belongs in professional
					string desc = "[b]" + trait.SubItems[0].Text + " (" + trait.SubItems[3].Text + " Trait";
					if (Int32.Parse(trait.SubItems[3].Text) > 1) {
						desc += "s";
					}
					desc += ") -[/b] " + trait.SubItems[4].Text;
					prof.Add(desc);
				}
				else {
					// Traits using both General/Professional would be in "General"
					string desc = "[b]" + trait.SubItems[0].Text + " (" + trait.SubItems[2].Text + " Trait";
					if (Int32.Parse(trait.SubItems[2].Text) > 1) {
						desc += "s";
					}
					if (Int32.Parse(trait.SubItems[3].Text) > 0) {
						// There's a professional trait too.
						desc += ", " + trait.SubItems[3].Text + " Professional";
					}
					desc += ") -[/b] " + trait.SubItems[4].Text;
					gen.Add(desc);
				}
				gen_num += Int32.Parse(trait.SubItems[2].Text);
				prof_num += Int32.Parse(trait.SubItems[3].Text);
			}
			template.Write("[table][b]Traits:[/b] " + gen_num + " General, " + prof_num + " Professional[/table]");
			template.Write("[table=2][b][u]General[/u][/b][c]");
			for (int i = 0; i < gen.Count; ++i) {
				template.Write(gen[i]);
				if (i + 1 != gen.Count) {
					template.Write("\n\n");
				}
			}
			template.Write("[c][b][u]Professional[/u][/b][c]");
			for (int i = 0; i < prof.Count; ++i) {
				template.Write(prof[i]);
				if (i + 1 != prof.Count) {
					template.Write("\n\n");
				}
				else {
					template.Write("[/table]\n");
				}
			}
			template.Write("[quote=Devil Fruit][b]Devil Fruit Name:[/b] " + DF_name + '\n');
			template.Write("[b]Devil Fruit Type:[/b] " + DF_type + '\n');
			template.Write("[b]Devil Fruit Ability:[/b] " + DF_desc + "[/quote]\n");
			template.Write('\n');
		}
		// ---------------------------------------------------------------------------
		public void Techs_Generate(string usedRegTP, string totRegTP, string regTPCalc, string usedSpTP, string totSpTP, ListView techs, ListView SpTraits) {
			template.Write("[center][big][big][i][font=Century Gothic]Techniques[/font][/i][/big][/big][/center]\n");
			template.Write("[b]Used/Total Regular Technique Points:[/b] " + usedRegTP + '/' + totRegTP + ' ' + regTPCalc + '\n');
			template.Write("[b]Used/Total Special Technique Points:[/b] " + usedSpTP + '/' + totSpTP);
			if (Int32.Parse(totSpTP) > 0) {
				template.Write("[list]");
				foreach (ListViewItem SpTrait in SpTraits.Items) {
					template.Write("[*]" + SpTrait.SubItems[0].Text + ": ");
					template.Write(SpTrait.SubItems[1].Text + "/" + SpTrait.SubItems[2].Text + '\n');
				}
				template.Write("[/list]");
			}
			template.Write('\n');
			template.Write("[table=2, Techniques]");
			if (Int32.Parse(usedRegTP) == 0 && Int32.Parse(usedSpTP) == 0) {
				template.Write("[b]TECHNIQUE NAME[/b] (#)\n");
				template.Write("[u]Type:[/u] \n");
				template.Write("[u]Range:[/u] \n");
				template.Write("[u]Power:[/u] \n");
				template.Write("[u]Stats:[/u] \n");
				template.Write("[c][u]Description:[/u] (Effects)\n[/table]\n");
			}
			else {
				int i = 0;
				foreach (ListViewItem Tech in techs.Items) {
					if (i > 0) {
						template.Write("[c]");
					}
					template.Write("[b]" + Tech.SubItems[0].Text + "[/b]"); // Name
					template.Write(" (" + Tech.SubItems[1].Text + ")\n");   // Rank
					template.Write("[u]Type:[/u] " + Tech.SubItems[9].Text + '\n'); // Type
					template.Write("[u]Range:[/u] " + Tech.SubItems[10].Text + '\n');// Range
					template.Write("[u]Power:[/u] " + Tech.SubItems[11].Text + '\n');// Power
					template.Write("[u]Stats:[/u] " + Tech.SubItems[12].Text + '\n');// Stats
					template.Write("[c]");
					if (!string.IsNullOrWhiteSpace(Tech.SubItems[13].Text)) {
						template.Write("[center][u]" + Tech.SubItems[13].Text + "[/u][/center]\n");
					}
					template.Write("[u]Description:[/u] " + Tech.SubItems[14].Text + '\n');
					i++;
				}
				template.Write("[/table]");
			}
		}

		public void Complete_Template_Generate(string version) {
			template.Write("[center][big][big][i][font=Century Gothic]Development History[/font][/i][/big][/big][/center]\n");
			template.Write("[b]Gains/Losses:[/b] \n");
			template.Write('\n');
			template.Write("[spoiler]Edit Log goes here[/spoiler]\n");
			template.Write('\n');
			template.Write("[small]This Character Template was created by the OPRP Character Builder " + version + '\n');
			template.Write("Calculations should be done correctly if not bugged or changed by the user.[/small]");
			// Transfer entire stream into readable textbox
			richTextBox_Template.Text = template.ToString();
			// Reset/Clear Stringwriter
			template.GetStringBuilder().Length = 0;
		}
	}
}