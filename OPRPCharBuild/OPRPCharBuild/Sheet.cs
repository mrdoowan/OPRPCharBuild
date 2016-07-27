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

	public partial class Sheet : Form
	{
		// The Basic Template for the Tool
		#region Const String Texts
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
			"[b]Weaponry:[/b] [list]<Weaponry>[*][b]<25>:[/b] <76>\n\n</Weaponry>[/list]\n" +
			"[b]Items:[/b][list]<Item>[*][b]<26>:[/b] <77>\n\n</Item>[/list]\n" +
			"<color>[b]Beli: [/b]</color> <27>\n" +
			"<color>[b]Advancement Points:[/b]</color> <28>\n" +
			"<color>[b]SD Earned:[/b]</color> <30><equal=30,74> / <74></equal>\n" +
			"<color>[b]SD Remaining:[/b]</color> <31>\n" +
			"<color>[b]Stat Points:[/b]</color> <32> <33>\n" +
			"<color>[list][*][i]Used for Stats:[/i] <34>\n" +
			"[*][i]Used for Fortune:[/i] <35>\n" +
			"[list][*][i]Strength:[/i] <37><equal=36,37> <38></equal>\n" +
			"[*][i]Speed:[/i] <40><equal=39,40> <41></equal>\n" +
			"[*][i]Stamina:[/i] <43><equal=42,43> <44></equal>\n" +
			"[*][i]Accuracy:[/i] <46><equal=45,46> <47></equal>\n" +
			"[*]<color>[b][i]Fortune:[/i][/b]</color> <48> <49>[/list][/list]\n" +
			"<color>[b]Traits:[/b]</color>\n\n" + 
			"<Trait_G><color>[b]<50> (<79> General Trait<plural=79>s</plural><zero=81>, <81> Professional</zero>)[/b]</color> - <80>\n\n</Trait_G>\n\n" +
			"<Trait_P><color>[b]<51> (<81> Professional Trait<plural=81>s</plural>)[/b]</color> - <82>\n\n</Trait_P>\n\n" +
			"<AP><color>[b]<29> (<78> AP)[/b]</color> - <99>\n\n</AP>\n\n" + 
			"[quote=Devil Fruit]<color>[b]Devil Fruit Name: [/b]</color> <52>\n" +
			"<color>[b]Devil Fruit Type:[/b]</color> <53>\n" +
			"<color>[b]Devil Fruit Ability:[/b]</color> <54>" +
			"<empty=55>\n<color>[b]Devil Fruit Free Effect:[/b]</color> <55></empty>[/quote]\n\n" +
			"[center][big][big][i][font=Century Gothic]Techniques[/font][/i][/big][/big][/center]\n" +
			"<color>[b]Regular Technique Points:[/b]</color> <57> <58>\n" + 
			"<color>[b]Used Regular Technique Points:[/b]</color> <56>\n" +
			"<color>[b]Special Technique Points:[/b]</color> <60>\n" +
			"<color>[b]Used Special Technique Points:[/b]</color> <59> <zero=60>[list]<SpTrait>[*]<61>: <83> / <84>\n</SpTrait>[/list]</zero>\n\n" +
			"<TechTable>[table=2, <63>, 1][center]<color>[b][u]Name/Type/Range/Power/Stats/TP[/u][/b]</color>[/center][c]\n" +
			"[center]<color>[b][u]Description/Notes[/u][/b]</color>[/center]\n" +
			"<Technique>[c]<color>[b]<64>[/b]</color> (<65>)\n" +
			"[u]Type:[/u] <66>\n" +
			"[u]Range:[/u] <97>\n" +
			"[u]Power:[/u] <67>\n" +
			"[u]Stats:[/u] <68>\n" +
			"[u]TP Spent:[/u] <69>R<zero=70> | <70>S</zero>\n" +
			"[c]<empty=71><71>\n\n[hr]\n</empty>" +
			"[u]Description:[/u] <72>\n\n" +
			"<empty=73>[u]Effects:[/u] <73>\n</empty></Technique>[/table]\n\n</TechTable>\n" +
			"[center][big][big][i][font=Century Gothic]Development History[/font][/i][/big][/big][/center]\n\n" +
			"<color>[b]Gains/Losses:[/b]</color> \n" +
			"[spoiler=Edit Log]Edit Log goes here[/spoiler]";

		#endregion

		private int option;
		private string Template;
		public static string color_hex = "";
		// This is constantly changing
		private Dictionary<int, string> RepeatTags = new Dictionary<int, string>();

		public Sheet(int option_, string text_ = "") {
			InitializeComponent();
			option = option_;
			if (option != 1) { label1.Visible = false; }
			else { Template = text_; }
			richTextBox_Template.Text = text_;
		}

		private void button_Close_Click(object sender, EventArgs e) {
			this.Close();
		}

		// Steps for the Template
		// 1) Read every Special Casting first and see if the Dictionary exists for it.
		//		a. If yes, check its value and see if it needs the proper action.
		// 2) One by one, check for Standard Custom Tags (not in Repeat Casting) and replace
		//		a. Check for <IMG0_URL> and increment as necessary 
		// 3) For Repeat Casting, take the substring, store them into variables based on Standard Custom Tags, handle Special Casting, and then replace and loop.
		//		NOTE: Make a Dictionary that constantly changes here
		// 4) Lastly, replace ALL <color> tags

		public void Generate_Template(ListBox Achievements, ListView Profs, ListView Images, ListView Weaponry, ListView Items, CheckedListBox AP,
			ListView Traitss, ListView SpTraits, ListView Techs, ListView Categories) {
			// ------- Step 1) 
			Template = Call_Special_Cast_Funcs(Template, MainForm.CustomTags);
			// ------- Step 2) 
			foreach (int key in MainForm.CustomTags.Keys) {
				string cast = "<" + key + ">";
				if (Template.Contains(cast)) { Template = Template.Replace(cast, Make_NA(MainForm.CustomTags[key])); }
			}
			// <IMG#_URL> casting
			for (int i = 0; i < Images.Items.Count; ++i) {
				ListViewItem Image = Images.Items[i];
				string cast = "<IMG" + i + "_URL>";
				if (!Template.Contains(cast)) { continue; }
				else { Template = Template.Replace(cast, Image.SubItems[1].Text); }
				cast = "<IMG" + i + "_LABEL>";
				if (Template.Contains(cast)) { Template = Template.Replace(cast, Image.SubItems[0].Text); }
				cast = "<IMG" + i + "_W>";
				if (Template.Contains(cast)) { Template = Template.Replace(cast, Image.SubItems[3].Text); }
				cast = "<IMG" + i + "_H>";
				if (Template.Contains(cast)) { Template = Template.Replace(cast, Image.SubItems[4].Text); }
			}
			// ------- Step 3) 
			if (Template.Contains("<Achievement>") && Template.Contains("</Achievement>")) {
				Template = Repeat_Casting(Template, "Achievement", Achievements);
			}
			if (Template.Contains("<Profession>") && Template.Contains("</Profession>")) {
				Template = Repeat_Casting(Template, "Profession", Profs);
			}
			if (Template.Contains("<Image>") && Template.Contains("</Image>")) {
				Template = Repeat_Casting(Template, "Image", Images);
			}
			if (Template.Contains("<Weaponry>") && Template.Contains("</Weaponry>")) {
				Template = Repeat_Casting(Template, "Weaponry", Weaponry);
			}
			if (Template.Contains("<Item>") && Template.Contains("</Item>")) {
				Template = Repeat_Casting(Template, "Item", Items);
			}
			if (Template.Contains("<AP>") && Template.Contains("</AP>")) {
				Template = Repeat_Casting(Template, "AP", AP);
			}
			if (Template.Contains("<Trait_G>") && Template.Contains("</Trait_G>")) {
				Template = Repeat_Casting(Template, "Trait_G", Traitss);
			}
			if (Template.Contains("<Trait_P>") && Template.Contains("</Trait_P>")) {
				Template = Repeat_Casting(Template, "Trait_P", Traitss);
			}
			if (Template.Contains("<SpTrait>") && Template.Contains("</SpTrait>")) {
				Template = Repeat_Casting(Template, "SpTrait", SpTraits);
			}
			if (Template.Contains("<TechTable>") && Template.Contains("</TechTable>")) {
				Template = Repeat_Casting(Template, "TechTable", Categories);
			}
			if (Template.Contains("<Technique>") && Template.Contains("</Technique>")) {
				if (Categories.Items.Count == 0) {  // 0 (or 1) Categories pretty much mean list out all the Techniques
					int end_row = Techs.Items.Count - 1;
					Template = Repeat_Casting(Template, "Technique", Techs, 0, end_row);
				}
				else {
					foreach (ListViewItem Category in Categories.Items) {
						// Repeat for each Category, which should tackle all <Technique>
						int beg_row = int.Parse(Category.SubItems[0].Text);
						int end_row = int.Parse(Category.SubItems[1].Text);
						// Just to double check.
						if (Template.Contains("<Technique>") && Template.Contains("</Technique>")) {
							Template = Repeat_Casting(Template, "Technique", Techs, beg_row, end_row);
						}
					}
				}
			}
			// ------- Step 4) 
			if (string.IsNullOrWhiteSpace(color_hex)) {
				Template = Template.Replace("<color>", "");
				Template = Template.Replace("</color>", "");
			}
			else {
				string BBCode_Color = "[color=#" + color_hex + "]";
				Template = Template.Replace("<color>", BBCode_Color);
				Template = Template.Replace("</color>", "[/color]");
			}
			// Last Note addition
			Template += "\n\n[center][small]This Character Template was generated by the [url=http://s1.zetaboards.com/One_Piece_RP/topic/6060583/1/]OPRP Character Builder[/url] v" + MainForm.version + MainForm.vers_type + '\n';
			Template += "[b]Note to Mods:[/b] Calculations should be done correctly if not bugged or altered by [me]\nRegardless, please double check.[/small][/center]";
            richTextBox_Template.Text = Template;
		}

		#region Generate_Template Helper
		// CODE DUPLICATION LEGGO :'(

		// The List object variable type depends on the List currently being used. Ensure that this is correct.
		private string Repeat_Casting(string Temp, string casting, object List, int beg_row = 0, int end_row = 0) {
			string start_casting = "<" + casting + ">"; // <REPEAT>
			string end_casting = "</" + casting + ">";  // </REPEAT>
			int beg_cast = Temp.IndexOf(start_casting);
			int beg_index = beg_cast + start_casting.Length;          // Index after '>'
			int end_index = Temp.IndexOf(end_casting); // Assume it won't be -1
			string old_str = Temp.Substring(beg_index, end_index - beg_index);   // String between <REPEAT></REPEAT>
			string new_str = "";
			switch (casting) {
				case "Achievement":
					ListBox Achievements = (ListBox)List;
					for (int i = 0; i < Achievements.Items.Count; ++i) {
						RepeatTags.Add(12, (string)Achievements.Items[i]); // Load Dictionary
						Repeat_String(old_str, ref new_str);
						RepeatTags.Clear();     // Clear Dict
					}
					break;
				case "Profession":
					ListView Profs = (ListView)List;
					foreach (ListViewItem Prof in Profs.Items) {
						RepeatTags.Add(13, Prof.SubItems[0].Text);  // Load Dictionary
						RepeatTags.Add(85, Prof.SubItems[1].Text);
						RepeatTags.Add(86, Prof.SubItems[2].Text);
						RepeatTags.Add(87, Prof.SubItems[3].Text);
						Repeat_String(old_str, ref new_str);
						RepeatTags.Clear();     // Clear Dict
					}
					break;
				case "Image":
					ListView Images = (ListView)List;
					foreach (ListViewItem Imagee in Images.Items) {
						RepeatTags.Add(91, Imagee.SubItems[1].Text); // Load Dictionary
						RepeatTags.Add(92, Imagee.SubItems[0].Text);
						RepeatTags.Add(93, Imagee.SubItems[3].Text);
						RepeatTags.Add(94, Imagee.SubItems[4].Text);
						Repeat_String(old_str, ref new_str);
						RepeatTags.Clear();     // Clear Dict
					}
					break;
				case "Weaponry":
					ListView Weaponry = (ListView)List;
					foreach (ListViewItem Weapon in Weaponry.Items) {
						RepeatTags.Add(25, Weapon.SubItems[0].Text); // Load Dictionary
						RepeatTags.Add(76, Weapon.SubItems[1].Text);
						Repeat_String(old_str, ref new_str);
						RepeatTags.Clear();     // Clear Dict
					}
					break;
				case "Item":
					ListView Itemss = (ListView)List;
					foreach (ListViewItem Itemm in Itemss.Items) {
						RepeatTags.Add(26, Itemm.SubItems[0].Text); // Load Dictionary
						RepeatTags.Add(77, Itemm.SubItems[1].Text);
						Repeat_String(old_str, ref new_str);
						RepeatTags.Clear();     // Clear Dict
					}
					break;
				case "AP":
					CheckedListBox AP = (CheckedListBox)List;
					foreach (int index in AP.CheckedIndices) {
						switch (index) {    // Load Dictionary
							case 0:
								RepeatTags.Add(29, "Technique");
								RepeatTags.Add(78, "1");
								RepeatTags.Add(99, "Permanently increase tech point multiplier by 0.5");
								break;
							case 1:
								RepeatTags.Add(29, "Trait");
								RepeatTags.Add(78, "2");
								RepeatTags.Add(99, "Trait cap raised by 1, can take Legacy Traits without training");
								break;
							case 2:
								RepeatTags.Add(29, "Prime Professional");
								RepeatTags.Add(78, "1");
								RepeatTags.Add(99, "May upgrade secondary profession to primary");
								break;
							case 3:
								RepeatTags.Add(29, "Multiskilled Professional");
								RepeatTags.Add(78, "1");
								RepeatTags.Add(99, "May take two more secondary professions");
								break;
							case 4:
								RepeatTags.Add(29, "Devil Fruit");
								RepeatTags.Add(78, "1");
								RepeatTags.Add(99, "Choose a Myth Zoan, gained without DF SL");
								break;
							default:
								break;
						}
						Repeat_String(old_str, ref new_str);
						RepeatTags.Clear();     // Clear Dict
					}
					break;
				case "Trait_G":
					ListView Traits_G = (ListView)List;
					foreach (ListViewItem trait in Traits_G.Items) {
						if (trait.SubItems[1].Text != "Professional") {
							RepeatTags.Add(50, trait.SubItems[0].Text);
							RepeatTags.Add(79, trait.SubItems[2].Text);
							RepeatTags.Add(81, trait.SubItems[3].Text);
							RepeatTags.Add(80, trait.SubItems[4].Text);
							Repeat_String(old_str, ref new_str);
							RepeatTags.Clear();     // Clear Dict
						}
					}
					break;
				case "Trait_P":
					ListView Traits_P = (ListView)List;
					foreach (ListViewItem trait in Traits_P.Items) {
						if (trait.SubItems[1].Text == "Professional") {
							RepeatTags.Add(51, trait.SubItems[0].Text);
							RepeatTags.Add(81, trait.SubItems[3].Text);
							RepeatTags.Add(82, trait.SubItems[4].Text);
							Repeat_String(old_str, ref new_str);
							RepeatTags.Clear();     // Clear Dict
						}
					}
					break;
				case "SpTrait":
					ListView SpTraits = (ListView)List;
					foreach (ListViewItem SpTrait in SpTraits.Items) {
						RepeatTags.Add(61, SpTrait.SubItems[0].Text); // Load Dictionary
						RepeatTags.Add(83, SpTrait.SubItems[1].Text);
						RepeatTags.Add(84, SpTrait.SubItems[2].Text);
						Repeat_String(old_str, ref new_str);
						RepeatTags.Clear();
					}
					break;
				case "TechTable":
					ListView Categories1 = (ListView)List;
					if (Categories1.Items.Count == 0) {     // Just use Standard Techniques
						RepeatTags.Add(63, "Techniques");
						Repeat_String(old_str, ref new_str);
						RepeatTags.Clear();
					}
					foreach (ListViewItem Category in Categories1.Items) {
						RepeatTags.Add(63, Category.SubItems[2].Text); // Load Dictionary
						Repeat_String(old_str, ref new_str);
						RepeatTags.Clear();
					}
					break;
				case "Technique":
					ListView Techniques = (ListView)List;
					for (int i = beg_row; i < end_row + 1; ++i) {
						string TechName = Techniques.Items[i].SubItems[0].Text;
						MainForm.TechInfo Technique = MainForm.TechList[TechName];
						// Load Dictionary
						RepeatTags.Add(64, TechName);
						string rank = Technique.rank.ToString();
						if (If_Treat_Rank4(Technique.rank_Trait)) { rank += "*"; }
						if (Technique.Cyborg_Boosts[2]) { rank += " + 12"; }
						else if (Technique.Cyborg_Boosts[1]) { rank += " + 8"; }
						else if (Technique.Cyborg_Boosts[0]) { rank += " + 4"; }
						RepeatTags.Add(65, rank);
						RepeatTags.Add(66, Technique.type);
						RepeatTags.Add(97, Technique.range);
						RepeatTags.Add(67, Technique.power);
						RepeatTags.Add(68, TechStats_Into_String(Technique.stats));
						RepeatTags.Add(69, Technique.regTP.ToString());
						RepeatTags.Add(70, Technique.spTP.ToString());
						RepeatTags.Add(71, Technique.note);
						RepeatTags.Add(72, Technique.desc);
						RepeatTags.Add(73, TechEffects_Into_String(Technique.effectList));
						// --------------
						Repeat_String(old_str, ref new_str);
						RepeatTags.Clear();
					}
					break;
				default:
					break;
			}
			new_str = new_str.TrimEnd('\n', ' ');   // Trim any newlines or spaces
			Temp = ReplaceFirst(Temp, old_str, new_str);  // Make replacement
			Temp = ReplaceFirst(Temp, start_casting, "");
			Temp = ReplaceFirst(Temp, end_casting, "");
			return Temp;
		}

		// Thanks Stackoverflow for ReplaceFirst()
		// Only replaces the first instance of the specified string "search"
		// Need ReplaceFirst() because of TechTable and Techniques is repeated. If we use Repeat, it replaces ALL <Technique> instead of separate
		public string ReplaceFirst(string text, string search, string replace) {
			int pos = text.IndexOf(search);
			if (pos < 0) {
				return text;
			}
			return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
		}

		// Modify the string between <REPEAT></REPEAT>
		private void Repeat_String(string repeat_str, ref string new_str) {
			repeat_str = Call_Special_Cast_Funcs(repeat_str, RepeatTags);   // Filter out Special Casting
			foreach (int key in RepeatTags.Keys) {  // Replace Tags
				if (repeat_str.Contains("<" + key + ">")) { repeat_str = repeat_str.Replace("<" + key + ">", Make_NA(RepeatTags[key])); }
			}
			new_str += repeat_str;  // Append appropriately
		}

		// Function that returns all the Special Casting already filtered through
		private string Call_Special_Cast_Funcs(string Temp, Dictionary<int, string> Dict_Tags) {
			Temp = Special_Casting_Single_Initial(Temp, "empty", Dict_Tags);
			Temp = Special_Casting_Single_Initial(Temp, "zero", Dict_Tags);
			Temp = Special_Casting_Single_Initial(Temp, "plural", Dict_Tags);
			Temp = Special_Casting_Double_Initial(Temp, "equal", Dict_Tags);
			return Temp;
		}

		private string Special_Casting_Single_Initial(string Temp, string TYPE, Dictionary<int, string> Dict_Tags) {
			if (!(TYPE == "empty" || TYPE == "zero" || TYPE == "plural")) {
				MessageBox.Show("Special Casting error type \"" + TYPE + "\". Report bug.", "Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return Temp;
			}
			try {
				// Find <TYPE=#>
				string casting = "<" + TYPE + "=";
				int curr_index = 0;
				int begin_cast = Temp.IndexOf(casting, curr_index);
				while (begin_cast != -1) {
					// We found a Repeat Casting
					int begin_index = begin_cast + casting.Length;
					// Find the '>'
					int end_index = Temp.IndexOf('>', begin_index);
					if (end_index == -1) { break; }
					int num_length = end_index - begin_index;
					// Retrieve the std tag
					string num_str = Temp.Substring(begin_index, num_length);
					int std_tag = 0;
					if (int.TryParse(num_str, out std_tag)) {
						// This means we found a number, we can continue
						if (Dict_Tags.ContainsKey(std_tag)) {
							// This means it's a valid std tag. Now we have to do the Special Cast
							string data = Dict_Tags[std_tag];
							// Find </TYPE>
							string end_casting = "</" + TYPE + ">";
							int end_cast = Temp.IndexOf(end_casting, begin_cast);
							if (end_cast == -1) { break; }
							end_cast += end_casting.Length;
							// This should be the entire string from <TYPE> to </TYPE>
							int cast_length = end_cast - begin_cast;
							string old_str = Temp.Substring(begin_cast, cast_length);
							string new_str = old_str;
							// This is now entirely dependent on what TYPE is
							switch (TYPE) {
								case "empty":
									if (string.IsNullOrWhiteSpace(data)) {
										// That means the data was Empty and we clear its content
										new_str = casting + num_str + '>' + end_casting;
									}
									break;
								case "zero":
									if (data == "0") {
										// That means data was '0' and we clear whatever content
										new_str = casting + num_str + '>' + end_casting;
									}
									break;
								case "plural":
									if (data == "1") {
										// If it's 1, we delete.
										new_str = casting + num_str + '>' + end_casting;
									}
									break;
								default:
									break;
							}
							// Remove <TYPE=> and </TYPE>
							new_str = new_str.Replace(casting + num_str + '>', "");
							new_str = new_str.Replace(end_casting, "");
							// Now make changes on the Template
							Temp = Temp.Replace(old_str, new_str);
							curr_index = begin_cast;
						}
						else {
							// If cond. failed, this std tag did not fit in the appropriate dictionary
							curr_index = end_index;
						}
					}
					else {
						// If cond. failed, skip this "trash" casting altogether 
						curr_index = end_index;
					}
					// Move onto next casting
					begin_cast = Temp.IndexOf(casting, curr_index);
				}
				return Temp;
			}
			catch (Exception e) {
				MessageBox.Show("Error in replacing casting \"" + TYPE + "\". Report bug.\nReason: " + e.Message, "Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return Temp;
			}
		}

		private string Special_Casting_Double_Initial(string Temp, string TYPE, Dictionary<int, string> Dict_Tags) {
			if (!(TYPE == "equal")) {
				MessageBox.Show("Special Casting error type. Report bug.", "Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return Temp;
			}
			try {
				// Find <TYPE=#>
				string casting = "<" + TYPE + "=";
				int curr_index = 0;
				int begin_cast = Temp.IndexOf(casting, curr_index);
				while (begin_cast != -1) {
					// We found a Repeat Casting
					int begin_index = begin_cast + casting.Length;	// begin_index now at the end of =
					// Find the '>'
					int end_index = Temp.IndexOf('>', begin_index);
					if (end_index == -1) { break; }
					int num_length = end_index - begin_index;
					// Retrieve the two std tags
					string num_str = Temp.Substring(begin_index, num_length);
					if (!num_str.Contains(',')) { begin_cast = Temp.IndexOf(casting, end_index); continue; }	// <equal=11> Invalid cast
					int comma_ind = num_str.IndexOf(',');
					if (comma_ind == num_str.Length - 1) { begin_cast = Temp.IndexOf(casting, end_index); continue; } // <equal=1,> Invalid cast
					string num1 = num_str.Substring(0, comma_ind);
					string num2 = num_str.Substring(comma_ind + 1, num_str.Length - (comma_ind + 1));
					int std_tag0 = 0, std_tag1 = 0;
					if (int.TryParse(num1, out std_tag0) && int.TryParse(num2, out std_tag1)) {
						// This means we found a number, we can continue
						if (Dict_Tags.ContainsKey(std_tag0) && Dict_Tags.ContainsKey(std_tag1)) {
							// This means it's a valid std tag. Now we have to do the Special Cast
							string data0 = Dict_Tags[std_tag0];
							string data1 = Dict_Tags[std_tag1];
							// Find </TYPE>
							string end_casting = "</" + TYPE + ">";
							int end_cast = Temp.IndexOf(end_casting, begin_cast);
							if (end_cast == -1) { break; }
							end_cast += end_casting.Length;
							// This should be the entire string from <TYPE> to </TYPE>
							int cast_length = end_cast - begin_cast;
							string old_str = Temp.Substring(begin_cast, cast_length);
							string new_str = old_str;
							// This is now entirely dependent on what TYPE is
							switch (TYPE) {
								case "equal":
									int val0 = 0, val1 = 0;
									if (int.TryParse(data0, out val0) && int.TryParse(data1, out val1)) {
										if (val0 == val1) {
											// That means both data are equal in value and we clear its content
											new_str = casting + num_str + '>' + end_casting;
										}
									}
									else {
										// The values containing the std_tag are not actual numbers
										begin_cast = Temp.IndexOf(casting, end_index); continue;
									}
									break;
								default:
									break;
							}
							// Remove <TYPE=> and </TYPE>
							new_str = new_str.Replace(casting + num_str + '>', "");
							new_str = new_str.Replace(end_casting, "");
							// Now make changes on the Template
							Temp = Temp.Replace(old_str, new_str);
							curr_index = begin_cast;
						}
						else {
							// If cond. failed, this is most likely a "Repeat Casting" std tag
							curr_index = end_index;
						}
					}
					else {
						// <equal=a,12> If cond. failed, skip this "trash" casting altogether 
						curr_index = end_index;
					}
					// Move onto next casting
					begin_cast = Temp.IndexOf(casting, curr_index);
				}
				return Temp;
			}
			catch (Exception e) {
				MessageBox.Show("Error in replacing casting \"" + TYPE + "\". Report bug.\nReason: " + e.Message, "Exception Thrown", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return Temp;
			}
		}


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
	}
}