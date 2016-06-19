﻿using System;
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

			string help = "--------------- One Piece RP Character Builder Tool\n";
			help += "--------------- Updated as of 6/7/2016\n";
			help += "--------------- For v1.0.1.0\n";
			help += '\n';
			help += "It is heavily preferred you copy and paste this Documentation into a separate Text Editor and Ctrl + F many of its features\n";
			help += '\n';
			help += "--------------- Overview\n";
			help += '\n';
			help += "One Piece RP: http://s1.zetaboards.com/One_Piece_RP/index/ \n";
			help += "Official Topic: http://s1.zetaboards.com/One_Piece_RP/topic/6060583/1/ \n";
			help += '\n';
			help += "One Piece RP hosted by Zetaboards has one of the most sophisticated rules for an interactive text based role play. The complexity and its elaborate progressive design of the Rules and resources allows members to diversify many of their options when creating characters. However, what comes with intricacy comes with cumbersome tasks to make these characters.\n";
			help += '\n';
			help += "This tool's objective is the following:\n";
			help += "1) Automatically calculate many of the complex formulas for the user.\n";
			help += "2) Provide a smooth and efficient method to make characters.\n";
			help += "3) Speed up the checking process for staff (Tentative goal)\n";
			help += '\n';
			help += "This tool allows data input into their respective areas by the user. Features here organize the data and keeps track much of the complex formulas and calculations so that the users can focus more on the development of their character. Data is more organized, orderly, and sticks to the rules for a smoother Character Registration process.\n";
            help += '\n';
			help += "The point of this documentation is to help walk through many of its ambiguous features. It will explain some parts in which its usage does not come off as obvious. If you are confused about a certain feature, please use Ctrl+F to find it. The documentation is split into its separate tabs for easier navigation.\n";
            help += '\n';
			help += "There are some parts you may want to pay attention to. They will always be capitalized in the documentation and are further described below:\n";
			help += '\n';
			help += "1) CLEAR: There are many parts of this application in which pressing a button could clear its content if you are not careful. It shouldn't happen intentionally in some areas, but please proceed with caution when handling such events.\n";
			help += "2) ENABLE/DISABLE/READ-ONLY: Some Text Boxes and Drop Boxes are unusable. This happens because a prerequisite isn't completed in order to Enable the feature. Either that or it's to prevent the user from interfering with Calculations or content automatically generated by the tool.\n";
            help += "3) CALCULATE: Many parts of the tool that involves numbers are typically already calculated and will be done for the user. This could happen when typing in a number, and then another Text Box's value changes correspondingly.\n";
			help += "4) ENTER: There are many areas where in order for the calculation to process, it requires you pressing the <ENTER> key. However for some, just pressing the increment/decrement arrows will automatically process the calculation.\n";
			help += "5) CUSTOM: Some parts allow customizable features that the user can freely edit.\n";
            help += '\n';
			help += "--------------- Saving & Opening Filese \n";
            help += '\n';
			help += "-\tThis tool allows you to save all of your work into a unique file extension (.oprp) file. It is incredibly important that you save your work and the tool will always remind you to save before you close your Application or before you Generate your sheet.\n";
            help += '\n';
			help += "-\tWARNING: When the tool is patched for future updates, there may be instances when opening an older version of the .oprp file into a newer version of the tool may only partially (or completely) restore data. I say this with great emphasis: PLEASE MAKE SURE YOU SAVE A BACKUP OF YOUR WORK OR YOU MAY LOSE IT!!! This tool is not meant to save everything as that is not its objective.\n";
			help += '\n';
			help += "--------------- Basic Information \n";
			help += '\n';
			help += "-\tThe Affiliation is a drop box that isn't editable and will only allow you to choose from \"Pirate\", \"Marine\", \"Bounty Hunter\", and \"Other\". By selecting an Affiliation, it will ENABLE its respective Fame Text Box. Selecting another Affiliation will disable the Text Box and CLEAR its content.";
			help += '\n';
			help += "-\tAchievements is merely a simple function allowing you to Add your feats by prompting a new Window.\n";
			help += '\n';
			help += "-\tBy pressing the Add button, a new window will pop up. Professions allow you to select from the Rules and by selecting its Profession, it will automatically fill in the Description and its Primary Bonus. By checking the Primary, you ENABLE its Text Box. However, by unchecking the Primary, it will DISABLE its Text Box and CLEAR its content. All parts are editable for CUSTOM professions.\n";
            help += '\n';
			help += "-\tAchievements and Professions are in a List. There are up and down arrows that allow you to select a row and change its order around.\n";
			help += '\n';
			help += "--------------- Physical Appearance \n";
			help += '\n';
			help += "-\tImage: The tool for now can only handle one image. By unchecking Full Resolution, you DISABLE and CLEAR both the Height and Width dimensions, and the template will be generated by using the image's full Resolution dimensions. By checking Full Resolution, you ENABLE Height and Width dimensions for edit.";
			help += '\n';
			help += "--------------- Background \n";
			help += '\n';
			help += "-\tNothing special to note here.\n";
			help += '\n';
			help += "--------------- Abilities & Posessions \n";
			help += '\n';
			help += "-\tBoth Weaponry and Items prompt a new Window and follow the same Form for adding onto a List. Otherwise, its features are as simple as Achievements and include the ordering change by pressing the arrows.";
			help += '\n';
			help += "-\tThe Beli Standardization will CALCULATE how much Beli you have when scooping your character based on your character's current location and how much SD Earned. Calculations will show in a separate Message Box. By pressing the button, you will be prompted if your character is in the Blues or not, in which pressing Yes/No will CLEAR the current Beli and replace it with the new calculated value.\n";
			help += '\n';
			help += "-\tUpon not filling out anything for Devil Fruit, the sheet will automatically put N/A for you. Otherwise, it does not have a list of approved Devil Fruits (and its effects) and requires the user to manually enter in the data.\n";
			help += '\n';
			help += "--------------- Stats \n";
			help += '\n';
			help += "-\tTyping in how much SD Earned and pressing ENTER will CALCULATE many other features that are affected by SD Earned (i.e. Trait Cap). It will mainly set the value of how much SD you put into SP into its max.\n";
			help += '\n';
			help += "-\tYou can change how much SD you put into SP. After changing values, you can press ENTER and it will CALCULATE how much SD is Remaining if you are planning to save any. It will mainly CALCULATE how much Stat Points you have from SD and display it in a READ-ONLY Stat Points and Used for Stats.\n";
			help += '\n';
			help += "-\tYou can CALCULATE Used for Stats by changing the value of Used for Fortune and pressing ENTER. Remember that investing SP into Fortune can only be divisible by 5, so it will automatically do it for you. It will also increment/decrement by 5 and max out at 25% of Stat Points.\n";
			help += '\n';
			help += "-\tYou can change the Base values of all 4 stats as freely as you wish by changing the value and pressing ENTER. However, there is a red label message to the left informing if your current CALCULATIONS are correct (which is all 4 Base stats summed to equal Used for Stats). It will turn green once CALCULATIONS are correct.\n";
			help += '\n';
			help += "-\tThe Final Stat values and Fortune are READ-ONLY and are CALCULATED by the tool based on your selected Traits. The Final Stat values will always change whenever its corresponding Base Stat is changed or when a Trait is added/removed. Calculations are displayed to the right for confirmation.\n";
			help += '\n';
			help += "-\tIf your character has Advancement Points, just check its correct description and it will CALCULATE how much SD you invested into it (SD Earned is not affected). In addition, if Traits or Techniques were checked, the tool will do its respective CALCULATION. There is no feature done for the other options.\n";
			help += '\n';
			help += "--------------- Traits \n";
			help += '\n';
			help += "-\tUnlike Achievements, Professions, Weaponry, Items, and Techniques, Traits do not prompt a new Window and is all done in the same tab.\n";
			help += '\n';
			help += "-\tYou can add a Trait by selecting from an approved list from the Rules. You can Drop down the list to see all traits alphabetized. However, you can type in the Trait Name while the list is dropped to find it more easily. Selecting a trait will fill in its Type, set/suggested number of Traits, and Description. All parts of it is editable for CUSTOM.\n";
			help += '\n';
			help += "-\tWARNING: If you do make a CUSTOM Trait Name (or a Custom trait in general), the tool will not recognize it when doing CALCULATIONS as it only recognizes the name.\n";
			help += '\n';
			help += "-\tSome traits require a Specification to be filled (has [SPEC] in it), in which the box will be ENABLED if that Trait is picked. Fill in the Specification and it will be added into the Trait name. Once all the information is filled (it will do a check), the Trait is added into the List above. Once the Trait is added, the proper CALCULATIONS affecting other parts of the character (like Stats, Techniques, etc.) will be done.\n";
			help += '\n';
			help += "-\tIf you want to edit a Trait, it will transfer all the information back into its corresponding boxes. From there, you can edit it however you like and then press Add again. Keep in mind that once you edit, it will CLEAR that row from the List.\n";
			help += '\n';
			help += "--------------- Techniques \n";
			help += '\n';
			help += "-\tUsed and Regular Technique Points are all CALCULATED by the tool and is READ-ONLY. Used and Regular Special Technique Points follow the same agenda, but is slightly more special because there is a dedicated READ-ONLY List that shows all Special Technique Points and what Trait affects it.\n";
			help += '\n';
			help += "-\tAdding a Technique will prompt a (rather large) new window that will require to fill out all the necessary information for Techniques.\n";
			help += '\n';
			help += "-\tChanging the value of Rank and pressing ENTER will immediately set Reg TP Used to its Max. It will also define a Maximum range for both Used boxes, Branched box, and Power box.\n";
			help += '\n';
			help += "-\tTrait Affecting Rank is a list of all the Traits that affect Rank in any way (i.e. Mastery by treating 4 Ranks higher, or Free Tech). Special Traits are traits that utilize Special TP. By selecting any of these Traits, it will update the TP Notes below. The Drop Box will only display a list that you have already added into the Traits List.\n";
			help += '\n';
			help += "-\tSpecial Traits and its Used TP box will only be enabled if a Trait that uses Special TP is added into the Traits List.\n";
			help += '\n';
			help += "-\tSelecting Signature Technique for Trait Affecting Rank will Disable the Used Boxes as it is a Free Technique.\n";
			help += '\n';
			help += "-\tYou can enable branching by checking the Branched box. Though, it is easier to do so by pressing the Branch button in the Main Form. Select a Technique you want to branch off and most of its information will already be filled in for you.\n";
			help += '\n';
			help += "-\tWhile there is a list of Type and Ranges already displayed for you, it is editable for CUSTOM preference.\n";
			help += '\n';
			help += "-\tThere is a special section for Technique Effects which is only a user manual output feature as of now. By filling this in, it will make a special section within your Technique Table when generating a sheet.\n";
			help += '\n';
			help += "Most of TP Note will be generated automatically based on previous selections, however it is editable to the user's liking.\n";
			help += '\n';
			help += "-\tAll Techniques are in a List. There are up and down arrows that allow you to select a row and change its order around, which is convenient for organizing Branching.\n";
			help += '\n';
			help += "--------------- Generating a Basic Template \n";
			help += '\n';
			help += "Once everything is inputted, you can press the Generate Sheet button and it will standardize all inputs into the Basic Character Template and make a sheet. It will NOT check for any empty boxes and proceed as normally. Generating a Sheet will always ask if you want to save before generating, in which pressing No will not generate a sheet.\n";
			help += '\n';
			help += "NOTE: The sheet is typically never the Final product and only serves as a Base Foundation to whatever decorations or additions you like to put on the Final Template.\n";
			help += '\n';
			help += "--------------- Bug Reports and Suggestions \n";
			help += '\n';
			help += "Because of the tool's young age (and the fact that it was only developed by one person), it is prone to many bugs and inefficient features. Please report all bugs and suggestions to the Official Topic: http://s1.zetaboards.com/One_Piece_RP/topic/6060583/1/ \n";
			help += '\n';
			help += "--------------- Credits \n";
			help += '\n';
			help += "Developer(s): Soloo\n";
			help += "Basic Idea: VackTavish for his original OPRP Character Calculator\n";
			help += "Beta Testers: Drift, Rayne, Masa, Linx, Bright, Cevian, Davy Jones, Arche, Spunky\n";
			help += "Icon: Masa\n";
			help += '\n';
			help += "Thanks goes to the University of Michigan for their educational resources and Microsoft's environment and documentation.\n";
			help += '\n';
			help += "Copyright (C)2016 Solo\n";
			help += "This tool is open-sourced, found here: https://github.com/mrdoowan/OPRPCharBuild \n";
			help += "You can modify under the terms of the GNU General Public License as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version\n";

			richTextBox_Template.Text = help;
		}

		#region Helper Functions

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
		public void Physical_Background_Generate(string height, string weight, string hair, string eye, string clothing, string appear, ListView images, string person,	    string island, string region, string history) {
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
					if (string.IsNullOrWhiteSpace(img.SubItems[0].Text)) {
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
				template.Write("[list]");
				foreach (string checkedAP in AP.CheckedItems) {
					template.Write("[*]" + checkedAP + '\n');
				}
				template.Write("[/list][/quote]");
			}
			template.Write('\n');
			template.Write("[b]SD Earned:[/b] " + SD_Earned);
			if (AP_num > 0) {
				template.Write("/" + (SD_Earned + AP_num * 50));
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
		public void Techs_Generate(string usedRegTP, string totRegTP, string regTPCalc, string usedSpTP, string totSpTP, 
			Dictionary<string, MainForm.TechInfo> techList, ListView SpTraits) {
			template.Write("[center][big][big][i][font=Century Gothic]Techniques[/font][/i][/big][/big][/center]\n");
			template.Write("[b]Used/Total Regular Technique Points:[/b] " + usedRegTP + '/' + totRegTP + ' ' + regTPCalc + '\n');
			template.Write("[b]Used/Total Special Technique Points:[/b] " + usedSpTP + '/' + totSpTP);
			if (int.Parse(totSpTP) > 0) {
				template.Write("[list]");
				foreach (ListViewItem SpTrait in SpTraits.Items) {
					template.Write("[*]" + SpTrait.SubItems[0].Text + ": ");
					template.Write(SpTrait.SubItems[1].Text + "/" + SpTrait.SubItems[2].Text + '\n');
				}
				template.Write("[/list]");
			}
			template.Write('\n');
			template.Write('\n');
			bool treat_rank4 = false;
			foreach (MainForm.TechInfo Tech in techList.Values) {
				if (If_Treat_Rank4(Tech.tech_Trait)) {
					treat_rank4 = true;
					break;
				}
			}
			if (treat_rank4) {
				template.Write("[table]* Technique is treated as 4 ranks higher[/table]");
			}
			template.Write("[table=2, Techniques]");
			if (int.Parse(usedRegTP) == 0 && int.Parse(usedSpTP) == 0) {
				// This is a zero Technique option
				template.Write("[b]TECHNIQUE NAME[/b] (#)\n");
				template.Write("[u]Type:[/u] \n");
				template.Write("[u]Range:[/u] \n");
				template.Write("[u]Power:[/u] \n");
				template.Write("[u]Stats:[/u] \n");
				template.Write("[c][u]Description:[/u] (Effects)\n[/table]\n");
			}
			else {
				int i = 0;
				foreach (string TechName in techList.Keys) {
					MainForm.TechInfo Technique = MainForm.TechList[TechName];
					if (i > 0) { template.Write("[c]"); }
					template.Write("[b]" + TechName + "[/b]");		// Name
					template.Write(" (" + Technique.rank);			// Rank
					if (If_Treat_Rank4(Technique.tech_Trait)) { template.Write("*"); }
					template.Write(")\n");
					template.Write("[u]Type:[/u] " + Technique.type + '\n'); // Type
					template.Write("[u]Range:[/u] " + Technique.range + '\n');// Range
					template.Write("[u]Power:[/u] " + Technique.power + '\n');// Power
					template.Write("[u]Stats:[/u] " + TechStats_Into_String(Technique.stats) + '\n');   // Stats
					template.Write("[u]TP Spent:[/u] " + Technique.regTP + "R");
					if (Technique.spTP > 0) {
						template.Write(" | " + Technique.spTP + "S\n");
					}
					template.Write("[c]");
					if (!string.IsNullOrWhiteSpace(Technique.note)) {
						// TP Note on top
						template.Write("[u]TP Note:[/u] " + Technique.note + '\n');
					}
					if (Technique.effectList.Count > 0) {
						// Effects on bottom
						template.Write("[u]Effects:[/u] " + TechEffects_Into_String(Technique.effectList) + '\n');
					}
					if (!string.IsNullOrWhiteSpace(Technique.note) || Technique.effectList.Count > 0) {
						template.Write('\n');
						template.Write("[hr]\n");
					}
					template.Write("[u]Description:[/u] " + Technique.desc + '\n');	// Description
					i++;
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
			template.Write("[small]This Character Template was created by the [url=http://s1.zetaboards.com/One_Piece_RP/topic/6060583/1/]OPRP Character Builder[/url] v" + version + vers_type + '\n');
			template.Write("Note to Mods: Calculations should be done correctly if not bugged or changed by [me][/small]");
			// Transfer entire stream into readable textbox
			richTextBox_Template.Text = template.ToString();
			// Reset/Clear Stringwriter
			template.GetStringBuilder().Length = 0;
		}

		private void richTextBox_Template_MouseEnter(object sender, EventArgs e) {
			label1.Text = "Click on the box and press Ctrl + A to copy and paste";
		}

		private void richTextBox_Template_MouseLeave(object sender, EventArgs e) {
			label1.Text = "Reminder: This template only serves as a Base Foundation";
		}
	}
}