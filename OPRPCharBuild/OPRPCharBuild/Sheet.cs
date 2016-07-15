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

	// Option 1: Generate Character Sheet
	// Option 2: Preview Template
	// Option 3: List of Custom Tags

	public partial class Sheet : Form
	{
		// The Basic Template for the Tool
		#region Basic Template
		public const string Basic_Template = "[center][big][big][big][big][font=Garamond]Character Template[/font][/big][/big][/big][/big]\n\n" +
			"[-----][/center]\n\n" +
			"<color>[b]Account Name:[/b]</color> [me]\n\n" +
			"[center][big][big][i][font=Century Gothic]Basic Character Information[/font][/i][/big][/big][/center]\n" +
			"<color>[b]Name:[/b]</color> <1>\n" +
			"<color>[b]Nickname:[/b]</color> <2>\n" +
			"<color>[b]Age:[/b]</color> <3>\n" +
			"<color>[b]Gender:[/b]</color> <4>\n" +
			"<color>[b]Race:[/b]</color> <5>\n" +
			"<color>[b]Affiliation:[/b]</color> <6>\n" +
			"<empty=7><color>[b]Bounty:[/b]</color> <7>\n</empty>" +
			"<empty=8><color>[b]Rank (Commendations):[/b]</color> <8> (<9>)\n</empty>" +
			"<empty=10><color>[b]Threat:[/b]</color> <10>\n</empty>" +
			"<color>[b]Position:[/b]</color> <11>\n" +
			"[list]<Achievement>[*]<12>\n</Achievement>[/list]\n" +
			"<color>[b]Professions:[/b]</color>[list]<Profession>[*]<color>[b][u]<13>[/u] - [i]<85>[/i]:[/b]</color> <86><empty=87> [i]<87>[/i]</empty>\n\n</Profession>[/list]\n" +
			"[center][i][big][big][font=Century Gothic]Physical Appearance[/font][/big][/big][/i][/center]\n" +
			"<color>[b]Height:[/b]</color> <14>\n" +
			"<color>[b]Height:[/b]</color> <15>\n" +
			"<color>[b]Hair:[/b]</color> <16>\n" +
			"<color>[b]Eyes:[/b]</color> <17>\n\n" +
			"<color>[b]Clothing/Accessories:[/b]</color> <18>\n\n" +
			"<color>[b]General Appearance:[/b]</color> <19>\n\n" +
			"<Image>[spoiler=<92>][img<empty=93>=<93>,<94></empty]<91>[/img][/spoiler]\n</Image>" +
			"[center][big][big][i][font=Century Gothic]The Character[/font][/i][/big][/big][/center]\n" +
			"<color>[b]Personality:[/b]</color> <20>\n\n" +
			"<color>[b]Hometown:[/b]</color> <21>, <22>\n\n" +
			"<color>[b]History:[/b]</color> <23>\n\n" +
			"[big][big][center][i][font=Century Gothic]Abilities and Possessions[/font][/i][/center][/big][/big]\n" +
			"<color>[b]Combat:[/b]</color> <24>\n\n" +
			"[table=2, Weaponry]<Weapon><empty=25><color>[b]<25>[/b]</color></empty>[c]<empty=76><76></empty>\n</Weapon>[/table]\n" +
			"[table=2, Items]<Item><empty=26><color>[b]<26>[/b]</color></empty>[c]<empty=77><77></empty>\n</Item>[/table]\n" +
			"<color>[b]Beli: [/b]</color> <27>\n" +
			"[quote=Advancement Points: <28>]<AP><29> (<78> AP) - <99>\n</AP>[/quote]\n" +
			"<color>[b]SD Earned:[/b]</color> <30><equal=30,74> / <74></empty>\n" +
			"<color>[b]SD Remaining: [/b]</color> <31>\n" +
			"<color>[b]Stat Points:[/b]</color> <32> <33>\n" +
			"<color>[b][list][*][i]Used for Stats:[/i] <34>\n" +
			"[*][i]Used for Fortune:[/i] <35>\n" +
			"[list][*][i]Strength:[/i] <36><equal=36,37> <38></equal>\n" +
			"[*][i]Speed:[/i] <39><equal=39,40> <41></equal>\n" +
			"[*][i]Stamina:[/i] <42><equal=42,43> <44></equal>\n" +
			"[*][i]Accuracy:[/i] <45><equal=45,46> <47></equal>\n" +
			"[*]<color>[b][i]Fortune:[/i][/b]</color> <48> <49>[/list][/list]\n" +
			"[table]<color>[b]Traits:[/b]</color> <95> General, <96> Professional[/table][table=2]<color>[b][u]General[/u][/b]</color>[c]<Traits_G><color>[b]<50> - (<79> Trait<plural=79>s</plural>):[/b]</color> - <80>\n\n</Traits_G>" +
			"[c]<color>[b][u]Professional[/u][/b]</color>[c]<Traits_P><color>[b]<51> - (<81> Trait<plural=81>s</plural>):[/b]</color> - <82>\n\n</Traits_P>[/table]\n" +
			"[quote=Devil Fruit]<color>[b]Devil Fruit Name: [/b]</color> <52>\n" +
			"<color>[b]Devil Fruit Type:[/b]</color> <53>\n" +
			"<color>[b]Devil Fruit Ability:[/b]</color> <54>[/quote]\n\n" +
			"[center][big][big][i][font=Century Gothic]Techniques[/font][/i][/big][/big][/center]\n" +
			"<color>[b]Used/Total Regular Technique Points:[/b]</color> <56> / <57> <58>\n" +
			"<color>[b]Used/Total Special Technique Points:[/b]</color> <59> / <60> <zero=60>[list]<SpTrait>[*]<61>: <83> / <84>\n</SpTrait>[/list]\n\n" +
			"[table]<88>\n\n<empty=62><62>\n\n</empty><empty=55>[u]DF Free Effect:[/u] <55>\n\n</empty>[/table]" +
			"<TechTable>[table=2, <63>, 1][center]<color>[b][u]Name/Type/Range/Power/Stats/TP[/u][/b]</color>[/center][c]\n" +
			"[center]<color>[b][u]Description/Notes[/u][/b]</color>[/center]\n" +
			"<Technique>[c]<color>[b]<64>[/b]</color> (<65>)\n" +
			"[u]Type:[/u] <66>\n" +
			"[u]Range:[/u] <97>\n" +
			"[u]Power:[/u] <67>\n" +
			"[u]Stats:[/u] <68>\n" +
			"[u]TP Spent:[/u] <69>R<zero=70> | <70>S</empty>\n" +
			"[c]<empty=71><71>\n\n[hr]\n</empty>" +
			"[u]Description:[/u] <72>\n" +
			"<empty=73>[u]Effects:[/u] <73>\n</Technique>[/table]</TechTable>\n" +
			"[center][big][big][i][font=Century Gothic]Development History[/font][/i][/big][/big][/center]\n" +
			"<color>[b]Gains/Losses:[/b]</color> \n" +
			"[spoiler=Edit Log]Edit Log goes here[/spoiler]\n";

		#endregion

		private int option;
		//StringBuilder template = new StringBuilder();
		private string Template;
		public static string color_hex = "";

		public Sheet(int option_, string text_ = "") {
			InitializeComponent();
			option = option_;
			if (option != 1) { label1.Visible = false; }
			else { Template = MainForm.template; }
			richTextBox_Template.Text = text_;
			if (option == 2) {
				this.Text = "Preview Template";
				label_Title.Text = "Template Source Code";
			}
		}

		private void button_Close_Click(object sender, EventArgs e) {
			this.Close();
		}

		// Steps for the Template
		// 1) Read every Special Casting first and see if the Dictionary exists for it.
		//		a. If yes, check its value and see if it needs the proper action.
		// 2) One by one, check for Standard Custom Tags (not in Repeat Casting) and replace
		//		a. Check for <IMG0_URL> and increment as necessary 
		// 4) For Repeat Casting, take the substring, store them into variables based on Standard Custom Tags, handle Special Casting, and then replace and loop.
		//		NOTE: Use Special Casting function as similar to 1)
		// 5) Lastly, replace ALL (use Regex) <color> tags

		private void Generate_Template(ListBox Achievements, ListView Profs, ListView Images, ListView Weaponry, ListView Items, CheckedListBox AP,
			ListView Traitss, ListView SpTraits, Dictionary<string, MainForm.TechInfo> techList) {

		}

		#region Generate_Template Helper

		#endregion

		#region General Helper Functions

		// Make it less tedious for me.
		private string Make_NA(string data) {
			if (string.IsNullOrWhiteSpace(data)) {
				return "N/A";
			}
			else {
				return data;
			}
		}

		private bool If_Treat_Rank4(string techTrait) {
			Traits Trait = new Traits();
			Traits.Trait_Name ID = Trait.get_TraitID(techTrait);
			return (ID == Traits.Trait_Name.MARTIAL_MASTERY || ID == Traits.Trait_Name.ADV_MARTIAL_MASTERY ||
					ID == Traits.Trait_Name.ADV_MARTIAL_CLASS || ID == Traits.Trait_Name.STANCE_MAST ||
					ID == Traits.Trait_Name.ADV_STANCE_MASTERY || ID == Traits.Trait_Name.ART_OF_STEALTH ||
					ID == Traits.Trait_Name.ANTI_STEALTH || ID == Traits.Trait_Name.DWARF);
		}

		private string TechStats_Into_String(MainForm.TechStats Stats) {
			string statsMsg = "";
			if (Stats.str != 0) {
				if (Stats.str > 0) { statsMsg += "+"; }
				statsMsg += Stats.str + " Str";
			}
			if (Stats.spe != 0) {
				if (!string.IsNullOrEmpty(statsMsg)) { statsMsg += ", "; }
				if (Stats.spe > 0) { statsMsg += "+"; }
				statsMsg += Stats.spe + " Spe";
			}
			if (Stats.sta != 0) {
				if (!string.IsNullOrEmpty(statsMsg)) { statsMsg += ", "; }
				if (Stats.spe > 0) { statsMsg += "+"; }
				statsMsg += Stats.sta + " Sta";
			}
			if (Stats.acc != 0) {
				if (!string.IsNullOrEmpty(statsMsg)) { statsMsg += ", "; }
				if (Stats.acc > 0) { statsMsg += "+"; }
				statsMsg += Stats.acc + " Acc";
			}
			if (string.IsNullOrEmpty(statsMsg)) { return "N/A"; }
			return statsMsg;
		}

		private string TechEffects_Into_String(Dictionary<string, Add_Technique.EffectItem> Effects) {
			string effectsMsg = "";
			int i = 0;
			foreach (string effectName in Effects.Keys) {
				if (i > 0) {
					effectsMsg += ", ";
				}
				Add_Technique.EffectItem effectInfo = Effects[effectName];
				effectsMsg += effectName;
				effectsMsg += " [";
				if (!effectInfo.gen) { effectsMsg += "-"; }
				effectsMsg += effectInfo.cost + "]";
				i++;
			}
			return effectsMsg;
		}

		#endregion

		#region Old Method
		/*
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
			template.Write("[b]Nickname:[/b] " + Make_NA(nick) + '\n');
			template.Write("[b]Age:[/b] " + age + '\n');
			template.Write("[b]Gender:[/b] " + gender + '\n');
			template.Write("[b]Race:[/b] " + race + '\n');
			template.Write("[b]Affiliation:[/b] " + aff + '\n');
			if (aff == "Pirate") {
				if (string.IsNullOrWhiteSpace(bounty)) {
					bounty = "0";
				}
				template.Write("[b]Bounty:[/b] " + bounty + '\n');
			}
			else if (aff == "Marine") {
				if (string.IsNullOrWhiteSpace(comm)) {
					comm = "0";
				}
				template.Write("[b]Rank (Commendations):[/b] " + rank + " (" + comm + ")\n");
			}
			else {
				if (string.IsNullOrWhiteSpace(threat)) {
					threat = "0";
				}
				template.Write("[b]Threat:[/b] " + threat + '\n');
			}
			template.Write("[b]Position:[/b] " + Make_NA(pos) + '\n');
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
			// Write primary first.
			foreach (ListViewItem prof in profs.Items) {
				if (prof.SubItems[1].Text == "Primary") {
					template.Write("[*][b][u]" + prof.SubItems[0].Text + "[/u] - [i]");
					template.Write(prof.SubItems[1].Text + "[/i]:[/b] ");
					template.Write(prof.SubItems[2].Text);
					template.Write(" [i]" + prof.SubItems[3].Text + "[/i]\n\n");
				}
			}
			// Then secondary
			foreach (ListViewItem prof in profs.Items) {
				if (prof.SubItems[1].Text == "Secondary") {
					template.Write("[*][b][u]" + prof.SubItems[0].Text + "[/u] - [i]");
					template.Write(prof.SubItems[1].Text + "[/i]:[/b] ");
					template.Write(prof.SubItems[2].Text + "\n\n");
				}
			}
			template.Write("[/list]\n");
		}
		// ---------------------------------------------------------------------------
		public void Physical_Background_Generate(string height, string weight, string hair, string eye, string clothing, string appear, ListView images, string person, string island, string region, string history) {
			template.Write("[center][i][big][big][font=Century Gothic]Physical Appearance[/font][/big][/big][/i][/center]\n");
			template.Write("[b]Height:[/b] " + height + '\n');
			template.Write("[b]Weight:[/b] " + weight + '\n');
			template.Write("[b]Hair:[/b] " + hair + '\n');
			template.Write("[b]Eyes:[/b] " + eye + '\n');
			template.Write('\n');
			template.Write("[b]Clothing/Accessories:[/b] " + clothing + '\n');
			template.Write('\n');
			template.Write("[b]General Appearance:[/b] " + appear + '\n');
			template.Write('\n');
			if (images.Items.Count > 0) {
				foreach (ListViewItem img in images.Items) {
					template.Write("[spoiler");
					if (!string.IsNullOrWhiteSpace(img.SubItems[0].Text)) {
						template.Write("=" + img.SubItems[0].Text);
					}
					template.Write("][img");
					if (img.SubItems[2].Text == "No") {
						template.Write("=" + img.SubItems[3].ToString() + "," + img.SubItems[4].ToString());
					}
					template.Write(']' + img.SubItems[1].Text + "[/img][/spoiler]\n");
				}
			}
			template.Write('\n');
			template.Write("[center][big][big][i][font=Century Gothic]The Character[/font][/i][/big][/big][/center]\n");
			template.Write("[b]Personality:[/b] " + person + '\n');
			template.Write('\n');
			template.Write("[b]Hometown:[/b] " + island + ", " + region + '\n');
			template.Write('\n');
			template.Write("[b]History:[/b] " + history + '\n');
			template.Write('\n');
			template.Write('\n');
		}
		// ---------------------------------------------------------------------------
		public void Combat_Generate(string combat, ListView weapons, ListView items, string beli) {
			template.Write("[big][big][center][i][font=Century Gothic]Abilities and Possessions[/font][/i][/center][/big][/big]\n");
			template.Write("[b]Combat:[/b] " + combat + '\n');
			template.Write('\n');
			template.Write("[table=2, Weaponry]");
			if (weapons.Items.Count == 0) {
				template.Write("[b]None[/b][c]N/A");
			}
			else {
				int i = 0; // Just for the first index
				foreach (ListViewItem weapon in weapons.Items) {
					if (i > 0) {
						template.Write("[c]");
					}
					template.Write("[b]" + weapon.SubItems[0].Text + "[/b]\n[c]" + weapon.SubItems[1].Text + '\n');
					i++;
				}
			}
			template.Write("[/table]\n");
			template.Write("[table=2, Items]");
			if (items.Items.Count == 0) {
				template.Write("[b]None[/b][c]N/A");
			}
			else {
				int i = 0; // Just for the first index
				foreach (ListViewItem item in items.Items) {
					if (i > 0) {
						template.Write("[c]");
					}
					template.Write("[b]" + item.SubItems[0].Text + "[/b]\n[c]" + item.SubItems[1].Text + '\n');
					i++;
				}
			}
			template.Write("[/table]\n");
			template.Write("[b]Beli:[/b] " + beli + '\n');
			template.Write('\n');
		}
		// ---------------------------------------------------------------------------
		public void Stats_Generate(int AP_num, CheckedListBox AP, int SD_Earned, string SD_Remain, string SP, string SP_calc, string used_stat, int used_fort,
			int str_base, string str_fin, string str_calc, int spe_base, string spe_fin, string spe_calc, int sta_base, string sta_fin, string sta_calc,
			int acc_base, string acc_fin, string acc_calc, string fort, string fort_calc) {
			template.Write("[quote=Advancement Points: " + AP_num + "]");
			if (AP_num > 0) {
				foreach (int index in AP.CheckedIndices) {
					switch (index) {
						case 0:
							template.Write("- [b]Technique (1 AP)[/b] - Permanently increase tech point multiplier by 0.5\n");
							break;
						case 1:
							template.Write("- [b]Trait (2 AP)[/b] - Trait cap raised by 1, can take Legacy Traits without training\n");
							break;
						case 2:
							template.Write("- [b]Prime Professional (1 AP)[/b] - May upgrade a secondary profession to primary\n");
							break;
						case 3:
							template.Write("- [b]Multiskilled Professional (1 AP)[/b] - May take two additional secondary professions\n");
							break;
						case 4:
							template.Write("- [b]Devil Fruit (1 AP)[/b] - Choose a Myth Zoan, gained without DF SL\n");
							break;
						default:
							break;
					}
				}
			}
			else {
				template.Write("None");
			}
			template.Write("[/quote]\n");
			template.Write("[b]SD Earned:[/b] " + SD_Earned);
			if (AP_num > 0) {
				template.Write(" / " + (SD_Earned + AP_num * 50));
			}
			template.Write('\n');
			template.Write("[b]SD Remaining:[/b] " + SD_Remain + '\n');
			template.Write("[b]Stat Points:[/b] " + SP + ' ' + SP_calc + '\n');
			template.Write("[list][*][i]Used for Stats:[/i] " + used_stat + '\n');
			template.Write("[*][i]Used for Fortune:[/i] " + used_fort + '\n');
			template.Write("[list][*][i]Strength:[/i] " + str_fin);
			if (str_base != int.Parse(str_fin)) {
				template.Write(' ' + str_calc);
			}
			template.Write('\n');
			template.Write("[*][i]Speed:[/i] " + spe_fin);
			if (spe_base != int.Parse(spe_fin)) {
				template.Write(' ' + spe_calc);
			}
			template.Write('\n');
			template.Write("[*][i]Stamina:[/i] " + sta_fin);
			if (sta_base != int.Parse(sta_fin)) {
				template.Write(' ' + sta_calc);
			}
			template.Write('\n');
			template.Write("[*][i]Accuracy:[/i] " + acc_fin);
			if (acc_base != int.Parse(acc_fin)) {
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
					if (int.Parse(trait.SubItems[3].Text) > 1) {
						desc += "s";
					}
					desc += ")[/b] - " + trait.SubItems[4].Text;
					prof.Add(desc);
				}
				else {
					// Traits using both General/Professional would be in "General"
					string desc = "[b]" + trait.SubItems[0].Text + " (" + trait.SubItems[2].Text + " Trait";
					if (int.Parse(trait.SubItems[2].Text) > 1) {
						desc += "s";
					}
					if (int.Parse(trait.SubItems[3].Text) > 0) {
						// There's a professional trait too.
						desc += ", " + trait.SubItems[3].Text + " Professional";
					}
					desc += ")[/b] - " + trait.SubItems[4].Text;
					gen.Add(desc);
				}
				gen_num += int.Parse(trait.SubItems[2].Text);
				prof_num += int.Parse(trait.SubItems[3].Text);
			}
			template.Write("[table][b]Traits:[/b] " + gen_num + " General, " + prof_num + " Professional[/table]");
			template.Write("[table=2][b][u]General[/u][/b][c]");
			for (int i = 0; i < gen.Count; ++i) {
				template.Write(gen[i]);
				if (i + 1 != gen.Count) {
					template.Write("\n\n");
				}
				else {
					template.Write('\n');
				}
			}
			template.Write("[c][b][u]Professional[/u][/b][c]");
			for (int i = 0; i < prof.Count; ++i) {
				template.Write(prof[i]);
				if (i + 1 != prof.Count) {
					template.Write("\n\n");
				}
			}
			template.Write("[/table]\n");
			template.Write("[quote=Devil Fruit][b]Devil Fruit Name:[/b] " + Make_NA(DF_name) + '\n');
			template.Write("[b]Devil Fruit Type:[/b] " + Make_NA(DF_type) + '\n');
			template.Write("[b]Devil Fruit Ability:[/b] " + Make_NA(DF_desc) + "[/quote]\n");
			template.Write('\n');
		}
		// ---------------------------------------------------------------------------
		public void Techs_Generate(string usedRegTP, string totRegTP, string regTPCalc, string usedSpTP, string totSpTP, string CritAnatQuick_Msg,
			Dictionary<string, MainForm.TechInfo> techList, ListView SpTraits, string DF_effect) {
			template.Write("[center][big][big][i][font=Century Gothic]Techniques[/font][/i][/big][/big][/center]\n");
			template.Write("[b]Used/Total Regular Technique Points:[/b] " + usedRegTP + " / " + totRegTP + ' ' + regTPCalc + '\n');
			template.Write("[b]Used/Total Special Technique Points:[/b] " + usedSpTP + " / " + totSpTP);
			if (int.Parse(totSpTP) > 0) {
				template.Write("[list]");
				foreach (ListViewItem SpTrait in SpTraits.Items) {
					template.Write("[*]" + SpTrait.SubItems[0].Text + ": ");
					template.Write(SpTrait.SubItems[1].Text + " / " + SpTrait.SubItems[2].Text + '\n');
				}
				template.Write("[/list]");
			}
			template.Write('\n');
			template.Write('\n');
			bool treat_rank4 = false;
			foreach (MainForm.TechInfo Tech in techList.Values) {
				if (If_Treat_Rank4(Tech.rank_Trait)) {
					treat_rank4 = true;
					break;
				}
			}
			if (treat_rank4 || !string.IsNullOrWhiteSpace(DF_effect) || !string.IsNullOrWhiteSpace(CritAnatQuick_Msg)) {
				template.Write("[table]");
				string preTable = "";
				if (treat_rank4) {
					preTable += "* Technique is treated as 4 ranks higher\n";
				}
				if (!string.IsNullOrWhiteSpace(CritAnatQuick_Msg)) {
					if (treat_rank4) { preTable += '\n'; }
					preTable += CritAnatQuick_Msg + '\n';
				}
				if (!string.IsNullOrWhiteSpace(DF_effect)) {
					if (treat_rank4 || !string.IsNullOrWhiteSpace(CritAnatQuick_Msg)) { preTable += '\n'; }
					preTable += "[u]Free DF Effect:[/u] " + DF_effect + '\n';
				}
				preTable = preTable.TrimEnd('\n'); // Trim newline
				template.Write(preTable + "[/table]");
			}
			template.Write("[table=2, Techniques, 1][center][b][u]Name/Type/Range/Power/Stats/TP[/u][/b][/center][c]\n");
			template.Write("[center][b][u]Description/Notes[/u][/b][/center]\n");
			if (int.Parse(usedRegTP) == 0 && int.Parse(usedSpTP) == 0) {
				// This is a zero Technique option
				template.Write("[c][b]TECHNIQUE NAME[/b] (#)\n");
				template.Write("[u]Type:[/u] \n");
				template.Write("[u]Range:[/u] \n");
				template.Write("[u]Power:[/u] \n");
				template.Write("[u]Stats:[/u] \n");
				template.Write("[u]TP Spent:[/u] \n");
				template.Write("[c][u]Description:[/u] (Effects)\n[/table]\n");
			}
			else {
				foreach (string TechName in techList.Keys) {
					MainForm.TechInfo Technique = MainForm.TechList[TechName];
					template.Write("[c][b]" + TechName + "[/b]");               // Name
					template.Write(" (" + Technique.rank);                      // Rank
					if (Technique.Cyborg_Boosts[2]) { template.Write(" + 12"); }
					else if (Technique.Cyborg_Boosts[1]) { template.Write(" + 8"); }
					else if (Technique.Cyborg_Boosts[0]) { template.Write(" + 4"); }
					if (If_Treat_Rank4(Technique.rank_Trait)) { template.Write("*"); }
					template.Write(")\n");
					template.Write("[u]Type:[/u] " + Technique.type + '\n');    // Type
					template.Write("[u]Range:[/u] " + Technique.range + '\n');  // Range
					template.Write("[u]Power:[/u] " + Make_NA(Technique.power) + '\n');					// Power
					template.Write("[u]Stats:[/u] " + TechStats_Into_String(Technique.stats) + '\n');   // Stats
					template.Write("[u]TP Spent:[/u] " + Technique.regTP + "R");
					if (Technique.spTP > 0) {
						template.Write(" | " + Technique.spTP + "S");
					}
					template.Write("\n[c]");
					if (!string.IsNullOrWhiteSpace(Technique.note)) {
						// TP Note on top
						template.Write(Technique.note + '\n');
					}
					if (!string.IsNullOrWhiteSpace(Technique.note)) {
						template.Write('\n');
						template.Write("[hr]\n");
					}
					template.Write("[u]Description:[/u] " + Technique.desc + '\n'); // Description
					if (Technique.effectList.Count > 0) {
						// Effects on bottom
						template.Write("\n[u]Effects:[/u] " + TechEffects_Into_String(Technique.effectList) + "\n\n");
					}
				}
				template.Write("[/table]\n");
				template.Write('\n');
			}
		}

		public void Complete_Template_Generate(string version, string vers_type) {
			template.Write("[center][big][big][i][font=Century Gothic]Development History[/font][/i][/big][/big][/center]\n");
			template.Write("[b]Gains/Losses:[/b] \n");
			template.Write('\n');
			template.Write("[spoiler=Edit Log]Edit Log goes here[/spoiler]\n");
			template.Write('\n');
			template.Write("[center][small]This Character Template was generated by the [url=http://s1.zetaboards.com/One_Piece_RP/topic/6060583/1/]OPRP Character Builder[/url] v" + version + vers_type + '\n');
			template.Write("[b]Note to Mods:[/b] Calculations should be done correctly if not bugged or altered by [me].\n");
			template.Write("Regardless, please double check.[/small][/center]");
			// Transfer entire stream into readable textbox
			richTextBox_Template.Text = template.ToString();
			// Reset/Clear Stringwriter
			template.GetStringBuilder().Length = 0;
			template.Dispose();
		}

		*/
		#endregion
	}
}