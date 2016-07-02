using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This contains files of older revisions.

#pragma warning disable CS0649

// MOST CURRENT VERSION: v1.0.2.0
namespace OPRPCharBuild
{
	[Serializable()]
	public class Project2
	{
		// Default constructor
		public Project2() { }

		public string filename;
		public string location;

		// List of structs

		[Serializable()]
		public struct Professions
		{
			public string name;
			public string primary;
			public string desc;
			public string bonus;

			public Professions(string name_, string pri_, string desc_, string bonus_) {
				name = name_;
				primary = pri_;
				desc = desc_;
				bonus = bonus_;
			}
		}

		[Serializable()]
		public struct Image
		{
			public string label;
			public string url;
			public string full_res;
			public string img_width;
			public string img_height;

			public Image(string label_, string url_, string fullres_, string imgWidth_, string imgHeight_) {
				label = label_;
				url = url_;
				full_res = fullres_;
				img_width = imgWidth_;
				img_height = imgHeight_;
			}
		}

		[Serializable()]
		public struct Equipment
		{
			public string name;
			public string desc;

			public Equipment(string name_, string desc_) {
				name = name_;
				desc = desc_;
			}
		}

		[Serializable()]
		public struct Trait
		{
			public string name;
			public string type;
			public string gen_num;
			public string prof_num;
			public string desc;

			public Trait(string name_, string type_, string gen_, string prof_, string desc_) {
				name = name_;
				type = type_;
				gen_num = gen_;
				prof_num = prof_;
				desc = desc_;
			}
		}

		[Serializable()]
		public struct Effect
		{
			public string name;
			public bool gen;
			public int cost;
			public int minRank;

			public Effect(string name_, bool gen_, int cost_, int minRank_) {
				name = name_;
				gen = gen_;
				cost = cost_;
				minRank = minRank_;
			}
		}

		[Serializable()]
		public struct Tech
		{
			public string name;
			public Rokushiki.RokuName Roku;
			public int rank;
			public int reg_TP;
			public int sp_TP;
			public string rank_trait;
			public string sp_trait;
			public string app_traits;
			public string branch_tech;
			public int branch_rank;
			public string type;
			public string range;
			public int str;
			public int spe;
			public int sta;
			public int acc;
			public bool NA_power;
			public List<Effect> effects;
			public string power;
			public List<bool> DF_options;
			public List<bool> Cyborg;
			public string notes;
			public string desc;

			public Tech(string name_, Rokushiki.RokuName Roku_, int rank_, int regTP_, int spTP_, string rankTrait_, string spTrait_, string appTraits_, 
				string branchTech_, int branchRank_, string type_, string range_, int str_, int spe_, int sta_, int acc_, bool NA_, List<Effect> effects_,
				string power_, List<bool> DF_, List<bool> Cyborg_, string notes_, string desc_) {
				name = name_;
				Roku = Roku_;
				rank = rank_;
				reg_TP = regTP_;
				sp_TP = spTP_;
				rank_trait = rankTrait_;
				sp_trait = spTrait_;
				app_traits = appTraits_;
				branch_tech = branchTech_;
				branch_rank = branchRank_;
				type = type_;
				range = range_;
				str = str_;
				spe = spe_;
				sta = sta_;
				acc = acc_;
				NA_power = NA_;
				effects = effects_;
				power = power_;
				DF_options = DF_;
				Cyborg = Cyborg_;
				notes = notes_;
				desc = desc_;
			}

		}

		// Basic Information Tab
		public string char_name;
		public string nickname;
		public int age;
		public string gender;
		public string race;
		public string position;
		public string affiliation;
		public string bounty;
		public string commendation;
		public string rank;
		public string threat;
		public List<string> achievements = new List<string>();
		public List<Professions> professions = new List<Professions>();

		// Physical Appearance Tab
		public string height;
		public string weight;
		public string hair;
		public string eye;
		public string clothing;
		public string appearance;
		public List<Image> images = new List<Image>();

		// Backcground Tab
		public string island;
		public string region;
		public string personality;
		public string history;

		// Combat & Stats Tab
		public string combat;
		public List<Equipment> weaponry = new List<Equipment>();
		public List<Equipment> items = new List<Equipment>();
		public string beli;
		public int SD_Earned;
		public int SDtoSP;
		public List<bool> AP = new List<bool>();
		public int used_fort;
		public int str;
		public int spe;
		public int sta;
		public int acc;
		public string DF_name;
		public string DF_type;
		public string DF_desc;
		public string DF_effect;

		// Traits Tab
		public List<Trait> traitsList = new List<Trait>();

		// Techniques Tab
		public List<Tech> techniques = new List<Tech>();

		#region Save Functions

		public void SaveProject_Basic(string name_, string nick_, int age_, string gender_, string race_, string position_,
			string affiliation_, string bounty_, string commendation_, string rank_, string threat_, ListBox achieve_, ListView prof_) {
			char_name = name_;
			nickname = nick_;
			age = age_;
			gender = gender_;
			race = race_;
			position = position_;
			affiliation = affiliation_;
			bounty = bounty_;
			commendation = commendation_;
			rank = rank_;
			threat = threat_;
			achievements.Clear();
			for (int i = 0; i < achieve_.Items.Count; ++i) {
				achievements.Add(achieve_.Items[i].ToString());
			}
			professions.Clear();
			foreach (ListViewItem eachitem in prof_.Items) {
				Professions profession = new Professions(eachitem.SubItems[0].Text, eachitem.SubItems[1].Text,
					eachitem.SubItems[2].Text, eachitem.SubItems[3].Text);
				professions.Add(profession);
			}
		}

		public void SaveProject_Physical(string height_, string weight_, string hair_, string eye_, string clothing_, string appearance_,
			ListView images_) {
			height = height_;
			weight = weight_;
			hair = hair_;
			eye = eye_;
			clothing = clothing_;
			appearance = appearance_;
			images.Clear();
			foreach (ListViewItem eachitem in images_.Items) {
				Image image = new Image(eachitem.SubItems[0].Text, eachitem.SubItems[1].Text,
					eachitem.SubItems[2].Text, eachitem.SubItems[3].Text, eachitem.SubItems[4].Text);
				images.Add(image);
			}
		}

		public void SaveProject_Background(string island_, string region_, string personality_, string history_) {
			island = island_;
			region = region_;
			personality = personality_;
			history = history_;
		}

		public void SaveProject_Combat(string combat_, ListView weaponry_, ListView items_, string beli_,
			string DFname_, string DFtype_, string DFdesc_, string DFeffect_) {
			combat = combat_;
			weaponry.Clear();
			foreach (ListViewItem eachitem in weaponry_.Items) {
				Equipment weapon = new Equipment(eachitem.SubItems[0].Text, eachitem.SubItems[1].Text);
				weaponry.Add(weapon);
			}
			items.Clear();
			foreach (ListViewItem eachitem in items_.Items) {
				Equipment item = new Equipment(eachitem.SubItems[0].Text, eachitem.SubItems[1].Text);
				items.Add(item);
			}
			beli = beli_;
			DF_name = DFname_;
			DF_type = DFtype_;
			DF_desc = DFdesc_;
			DF_effect = DFeffect_;
		}

		public void SaveProject_Stats(int SDEarned_, int SDtoSP_, CheckedListBox AP_, int usedFort_,
			int str_, int spe_, int sta_, int acc_) {
			SD_Earned = SDEarned_;
			SDtoSP = SDtoSP_;
			AP.Clear();
			for (int i = 0; i < AP_.Items.Count; ++i) {
				CheckState chkBox = AP_.GetItemCheckState(i);
				if (chkBox == CheckState.Checked) {
					AP.Add(true);
				}
				else {
					AP.Add(false);
				}
			}
			used_fort = usedFort_;
			str = str_;
			spe = spe_;
			sta = sta_;
			acc = acc_;
		}

		public void SaveProject_Traits(ListView traitsList_) {
			traitsList.Clear();
			foreach (ListViewItem eachitem in traitsList_.Items) {
				// ListView
				Trait trait = new Trait(eachitem.SubItems[0].Text, eachitem.SubItems[1].Text,
					eachitem.SubItems[2].Text, eachitem.SubItems[3].Text, eachitem.SubItems[4].Text);
				traitsList.Add(trait);
			}
		}

		public void SaveProject_Tech(Dictionary<string, MainForm.TechInfo> techList_, ListView techListView_) {
			techniques.Clear();
			// Grabbing from TechInfo AND ListView (to maintain order)
			foreach (ListViewItem techItem in techListView_.Items) {
				string techName = techItem.SubItems[0].Text;
				MainForm.TechInfo techInfo = techList_[techName];
				List<Effect> effects = new List<Effect>();
				foreach (string effectName in techInfo.effectList.Keys) {
					Add_Technique.EffectItem effectInfo = techInfo.effectList[effectName];
					Effect Effect_ = new Effect(effectName, effectInfo.gen, effectInfo.cost, effectInfo.minRank);
					effects.Add(Effect_);
				}
				Tech tech = new Tech(techName, techInfo.Roku, techInfo.rank, techInfo.regTP, techInfo.spTP, techInfo.rank_Trait, techInfo.sp_Trait, 
					techInfo.app_Traits, techInfo.tech_Branch, techInfo.rank_Branch, techInfo.type, techInfo.range, techInfo.stats.str, techInfo.stats.spe,
					techInfo.stats.sta, techInfo.stats.acc, techInfo.NA_power, effects, techInfo.power, techInfo.DF_checkBox, techInfo.Cyborg_Boosts,
					techInfo.note, techInfo.desc);
				techniques.Add(tech);
			}
		}

		#endregion

		#region Load Functions

		public void LoadProject_Basic(ref TextBox _name, ref TextBox _nick, ref NumericUpDown _age, ref ComboBox _gender, ref TextBox _race, ref TextBox _position,
			ref ComboBox _affiliation, ref TextBox _bounty, ref TextBox _commendation, ref ComboBox _rank, ref TextBox _threat, ref ListBox _achieve, ref ListView _prof) {
			_name.Text = char_name;
			_nick.Text = nickname;
			_age.Value = age;
			_gender.Text = gender;
			_race.Text = race;
			_position.Text = position;
			_affiliation.Text = affiliation;
			_bounty.Text = bounty;
			_commendation.Text = commendation;
			_rank.Text = rank;
			_threat.Text = threat;
			_achieve.Items.Clear();
			for (int i = 0; i < achievements.Count; ++i) {
				_achieve.Items.Add(achievements[i]);
			}
			MainForm.ProfList.Clear();
			_prof.Items.Clear();
			for (int i = 0; i < professions.Count; ++i) {
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = professions[i].name;
				item.SubItems.Add(professions[i].primary);
				item.SubItems.Add(professions[i].desc);
				item.SubItems.Add(professions[i].bonus);
				_prof.Items.Add(item);
				// Now add onto static variable ProfList
				bool pri = false;
				if (professions[i].primary == "Primary") {
					pri = true;
				}
				MainForm.ProfList.Add(professions[i].name, pri);
			}
		}

		public void LoadProject_Physical(ref TextBox _height, ref TextBox _weight, ref RichTextBox _hair, ref RichTextBox _eye,
			ref RichTextBox _clothing, ref RichTextBox _appearance, ref ListView _images) {
			_height.Text = height;
			_weight.Text = weight;
			_hair.Text = hair;
			_eye.Text = eye;
			_clothing.Text = clothing;
			_appearance.Text = appearance;
			_images.Items.Clear();
			for (int i = 0; i < images.Count; ++i) {
				ListViewItem img = new ListViewItem();
				img.SubItems[0].Text = images[i].label;
				img.SubItems.Add(images[i].url);
				img.SubItems.Add(images[i].full_res);
				img.SubItems.Add(images[i].img_width);
				img.SubItems.Add(images[i].img_height);
				_images.Items.Add(img);
			}
		}

		public void LoadProject_Background(ref TextBox _island, ref ComboBox _region, ref RichTextBox _personality, ref RichTextBox _history) {
			_island.Text = island;
			_region.Text = region;
			_personality.Text = personality;
			_history.Text = history;
		}

		public void LoadProject_Combat(ref RichTextBox _combat, ref ListView _weaponry, ref ListView _items, ref TextBox _beli,
			ref TextBox _DFname, ref ComboBox _DFtype, ref RichTextBox _DFdesc, ref TextBox _DFeffect) {
			_combat.Text = combat;
			_weaponry.Items.Clear();
			for (int i = 0; i < weaponry.Count; ++i) {
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = weaponry[i].name;
				item.SubItems.Add(weaponry[i].desc);
				_weaponry.Items.Add(item);
			}
			_items.Items.Clear();
			for (int i = 0; i < items.Count; ++i) {
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = items[i].name;
				item.SubItems.Add(items[i].desc);
				_items.Items.Add(item);
			}
			_beli.Text = beli;
			_DFname.Text = DF_name;
			_DFtype.Text = DF_type;
			_DFdesc.Text = DF_desc;
			_DFeffect.Text = DF_effect;
		}

		public void LoadProject_Stats(ref NumericUpDown _SDEarned, ref NumericUpDown _SDtoSP, ref CheckedListBox _AP, ref NumericUpDown _usedFort,
			ref NumericUpDown _str, ref NumericUpDown _spe, ref NumericUpDown _sta, ref NumericUpDown _acc) {
			_SDEarned.Value = SD_Earned;
			_SDtoSP.Value = SDtoSP;
			if (AP.Count == 5) {
				for (int i = 0; i < _AP.Items.Count; ++i) {
					_AP.SetItemChecked(i, AP[i]);
				}
			}
			_usedFort.Value = used_fort;
			_str.Value = str;
			_spe.Value = spe;
			_sta.Value = sta;
			_acc.Value = acc;
		}

		public void LoadProject_Traits(ref ListView _traitsList) {
			MainForm.TraitsList.Clear();
			_traitsList.Items.Clear();
			for (int i = 0; i < traitsList.Count; ++i) {
				Traits Trait = new Traits();
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = traitsList[i].name;
				item.SubItems.Add(traitsList[i].type);
				item.SubItems.Add(traitsList[i].gen_num);
				item.SubItems.Add(traitsList[i].prof_num);
				item.SubItems.Add(traitsList[i].desc);
				_traitsList.Items.Add(item);
				// Add into static TraitsList
				MainForm.TraitsList.Add(Trait.get_TraitID(traitsList[i].name));
			}
		}

		private string Filter_EffectName(string effect) {
			// We know from our earlier implementation that copies of the same effect just have a number attached to the end.
			// If for some odd reason a user has more than 10 of the same effects, this will crash...yeah...not fixing that.
			char num = effect[effect.Length - 1];
			if (char.IsNumber(num)) {
				// We're removing that number
				return effect.TrimEnd(num);
			}
			return effect;
		}

		private string Copy_StatInts_to_ListView(int str, int spe, int sta, int acc) {
			string stats = "";
			if (str == 0 && spe == 0 && sta == 0 && acc == 0) {
				return "N/A";
			}
			if (str != 0) {
				if (str > 0) { stats += "+"; }
				stats += str + " Str";
			}
			if (spe != 0) {
				if (!string.IsNullOrEmpty(stats)) { stats += ", "; }
				if (spe > 0) { stats += "+"; }
				stats += spe + " Spe";
			}
			if (sta != 0) {
				if (!string.IsNullOrEmpty(stats)) { stats += ", "; }
				if (sta > 0) { stats += "+"; }
				stats += sta + " Sta";
			}
			if (acc != 0) {
				if (!string.IsNullOrEmpty(stats)) { stats += ", "; }
				if (acc > 0) { stats += "+"; }
				stats += acc + " Acc";
			}
			return stats;
		}

		private string Generate_AppTraits_String(string rankTrait, string techNote) {
			string AppTraits = rankTrait;
			if (!string.IsNullOrWhiteSpace(AppTraits)) { AppTraits += ", "; }
			if (techNote.Contains(Add_Technique.DevFruit)) { AppTraits += Add_Technique.DevFruit + ", "; }
			if (techNote.Contains(Add_Technique.SigTech)) { AppTraits += Add_Technique.SigTech + ", "; }
			if (techNote.Contains(Add_Technique.Cyborg)) { AppTraits += Add_Technique.Cyborg + ", "; }
			if (techNote.Contains(Add_Technique.CritHit)) { AppTraits += Add_Technique.CritHit + ", "; }
			if (techNote.Contains(Add_Technique.AnatStrike)) { AppTraits += Add_Technique.AnatStrike + ", "; }
			if (techNote.Contains(Add_Technique.Quickstrike)) { AppTraits += Add_Technique.Quickstrike + ", "; }
			if (techNote.Contains(Add_Technique.HakiTech)) { AppTraits += Add_Technique.HakiTech + ", "; }
			if (techNote.Contains(Add_Technique.FormFunc)) { AppTraits += Add_Technique.FormFunc + ", "; }
			if (techNote.Contains(Add_Technique.LifeRet)) { AppTraits += Add_Technique.LifeRet + ", "; }
			if (techNote.Contains(Add_Technique.MentFort)) { AppTraits += Add_Technique.MentFort + ", "; }
			if (techNote.Contains(Add_Technique.CrowdCont)) { AppTraits += Add_Technique.CrowdCont + ", "; }
			if (techNote.Contains(Add_Technique.PowSpeak)) { AppTraits += Add_Technique.PowSpeak + ", "; }
			if (techNote.Contains(Add_Technique.BakeBad)) { AppTraits += Add_Technique.BakeBad + ", "; }
			if (techNote.Contains(Add_Technique.ExtraIngred)) { AppTraits += Add_Technique.ExtraIngred + ", "; }
			// Now trim the ", "
			AppTraits = AppTraits.TrimEnd(',', ' ');
			return AppTraits;
		}

		public void LoadProject_Tech(ref ListView _techList) {
			MainForm.TechList.Clear();
			_techList.Items.Clear();
			for (int i = 0; i < techniques.Count; ++i) {
				Effects Effectss = new Effects();
				// Store into static TechList
				MainForm.TechStats Stats = new MainForm.TechStats(techniques[i].str, techniques[i].spe,
					techniques[i].sta, techniques[i].acc);
				Dictionary<string, Add_Technique.EffectItem> effectList = new Dictionary<string, Add_Technique.EffectItem>();
				// Remember the save file only has a string of Effects, in which we can just simply load its info.
				// Exception is: Wooden Defences and Mirage (Clones)
				for (int j = 0; j < techniques[i].effects.Count; ++j) {
					string effectName = techniques[i].effects[j].name;		// WARNING: There could be copies of an effect!
					string FilteredeffectName = Filter_EffectName(effectName);
					Effects.Effect_Name ID = Effectss.Get_EffectID(FilteredeffectName);
					// An error when you have 9 or more of the same effects
					if (ID == Effects.Effect_Name.NONE) { MessageBox.Show("Wrong Effect name loaded or Why the heck do you have more than 9 of the same effects?", "Error"); return; }
					effectList.Add(effectName, new Add_Technique.EffectItem(ID, techniques[i].effects[j].gen,
						techniques[i].effects[j].cost, techniques[i].effects[j].minRank));
				}
				MainForm.TechInfo Tech_Info = new MainForm.TechInfo(techniques[i].Roku, techniques[i].rank, techniques[i].reg_TP,
					techniques[i].sp_TP, techniques[i].rank_trait, techniques[i].sp_trait, techniques[i].app_traits, techniques[i].branch_tech,
					techniques[i].branch_rank, techniques[i].type, techniques[i].range, Stats, techniques[i].NA_power,
					effectList, techniques[i].power, techniques[i].DF_options, techniques[i].Cyborg, 
					techniques[i].notes, techniques[i].desc);
				MainForm.TechList.Add(techniques[i].name, Tech_Info);
				// Now add into ListView
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = techniques[i].name;			// [0]: Name
				item.SubItems.Add(techniques[i].rank.ToString());	// [1]: Rank
				item.SubItems.Add(techniques[i].reg_TP.ToString());	// [2]: RegTP
				item.SubItems.Add(techniques[i].sp_TP.ToString());	// [3]: SpTP
				item.SubItems.Add(techniques[i].sp_trait);          // [4]: SpTrait
				string AppTraits = Generate_AppTraits_String(techniques[i].rank_trait, techniques[i].notes);
                item.SubItems.Add(AppTraits);						// [5]: App. Traits
				item.SubItems.Add(techniques[i].branch_tech);		// [6]: BranchTech
				item.SubItems.Add(techniques[i].type);				// [7]: Type
				item.SubItems.Add(techniques[i].range);             // [8]: Range
				string stats = Copy_StatInts_to_ListView(techniques[i].str,
					techniques[i].spe, techniques[i].sta, techniques[i].acc);
				item.SubItems.Add(stats);							// [9]: Stats
				item.SubItems.Add(techniques[i].power);				// [10]: Power
				_techList.Items.Add(item);
			}

			#endregion
		
		}
	}
}

// Last Version used: v1.0.1.0 (Project)
namespace OPRPCharBuild
{
	[Serializable()]
	public class Project
	{
		// Default constructor
		public Project() { }

		public string filename;
		public string location;

		// List of structs

		[Serializable()]
		public struct Professions
		{
			public string name;
			public string primary;
			public string desc;
			public string bonus;
		}

		[Serializable()]
		public struct Equipment
		{
			public string name;
			public string desc;
		}

		[Serializable()]
		public struct Trait
		{
			public string name;
			public string type;
			public string gen_num;
			public string prof_num;
			public string desc;
		}

		[Serializable()]
		public struct Tech
		{
			public string name;
			public string rank;
			public string reg_TP;
			public string sp_TP;
			public string sp_trait;
			public string rank_trait;
			public string branched;
			public string branch_TP;
			public string type;
			public string range;
			public string stats;
			public string power;
			public string effect;
			public string TP_note;
			public string desc;
		}

		// Basic Information Tab
		private string char_name;
		private string nickname;
		private int age;
		private string gender;
		private string race;
		private string position;
		private string affiliation;
		private string bounty;
		private string commendation;
		private string rank;
		private string threat;
		private List<string> achievements = new List<string>();
		private List<Professions> professions = new List<Professions>();

		// Physical Appearance Tab
		private string height;
		private string weight;
		private string hair;
		private string eye;
		private string clothing;
		private string appearance;
		private string img_url;
		private bool full_res;
		private int img_width;
		private int img_height;

		// Backcground Tab
		private string island;
		private string region;
		private string personality;
		private string history;

		// Combat & Stats Tab
		private string combat;
		private List<Equipment> weaponry = new List<Equipment>();
		private List<Equipment> items = new List<Equipment>();
		private string beli;
		private int SD_Earned;
		private int SDtoSP;
		private List<bool> AP = new List<bool>();
		private int used_fort;
		private int str;
		private int spe;
		private int sta;
		private int acc;
		private string DF_name;
		private string DF_type;
		private string DF_desc;

		// Traits Tab
		private List<Trait> traitsList = new List<Trait>();

		// Techniques Tab
		private List<Tech> techniques = new List<Tech>();

		// Transfer from Project to Project2 (Newest)
		public void Transfer_v1010toNew(ref Project2 project) {
			try {
				// Basic Information Tab
				project.char_name = char_name;
				project.nickname = nickname;
				project.age = age;
				project.gender = gender;
				project.race = race;
				project.position = position;
				project.affiliation = affiliation;
				project.bounty = bounty;
				project.commendation = commendation;
				project.rank = rank;
				project.threat = threat;
				project.achievements = achievements;
				project.professions.Clear();
				for (int i = 0; i < professions.Count; ++i) {
					Project2.Professions prof = new Project2.Professions();
					prof.name = professions[i].name;
					prof.primary = professions[i].primary;
					prof.desc = professions[i].desc;
					prof.bonus = professions[i].bonus;
					project.professions.Add(prof);
				}
			}
			catch (Exception e) { MessageBox.Show("v1.0.1.0 Transfer: Basic Information Error\nReason: " + e.Message, "Error"); }

			// Physical Appearance Tab
			try {
				project.height = height;
				project.weight = weight;
				project.hair = hair;
				project.eye = eye;
				project.clothing = clothing;
				project.appearance = appearance;
				// Only load one image into the list.
				project.images.Clear();
				Project2.Image img = new Project2.Image();
				img.label = "";
				img.url = img_url;
				if (full_res) {
					img.full_res = "Yes";
				}
				else {
					img.full_res = "No";
				}
				img.img_width = img_width.ToString();
				img.img_height = img_height.ToString();
				project.images.Add(img);

				// Backcground Tab
				project.island = island;
				project.region = region;
				project.personality = personality;
				project.history = history;
			}
			catch (Exception e) { MessageBox.Show("v1.0.1.0 Transfer: Physical Appearance / Background Error\nReason: " + e.Message, "Error"); }

			// Combat Tab
			try {
				project.combat = combat;
				project.weaponry.Clear();
				for (int i = 0; i < weaponry.Count; ++i) {
					Project2.Equipment weapon = new Project2.Equipment();
					weapon.name = weaponry[i].name;
					weapon.desc = weaponry[i].desc;
					project.weaponry.Add(weapon);
				}
				project.items.Clear();
				for (int i = 0; i < items.Count; ++i) {
					Project2.Equipment item = new Project2.Equipment();
					item.name = items[i].name;
					item.desc = items[i].desc;
					project.items.Add(item);
				}
				project.beli = beli;
				project.DF_name = DF_name;
				project.DF_type = DF_type;
				project.DF_desc = DF_desc;
				project.DF_effect = "";
			}
			catch (Exception e) { MessageBox.Show("v1.0.1.0 Transfer: Combat Error\nReason: " + e.Message, "Error"); }

			// Stats Tab
			try {
				project.SD_Earned = SD_Earned;
				project.SDtoSP = SDtoSP;
				project.AP = AP;
				project.used_fort = used_fort;
				project.str = str;
				project.spe = spe;
				project.sta = sta;
				project.acc = acc;
			}
			catch (Exception e) { MessageBox.Show("v1.0.1.0 Transfer: Stats Error\nReason: " + e.Message, "Error"); }

			// Traits Tab
			
				project.traitsList.Clear();
			for (int i = 0; i < traitsList.Count; ++i) {
				Project2.Trait trait = new Project2.Trait();
				trait.name = traitsList[i].name;
				try {
					trait.type = traitsList[i].type;
					trait.gen_num = traitsList[i].gen_num;
					trait.prof_num = traitsList[i].prof_num;
					trait.desc = traitsList[i].desc;
					project.traitsList.Add(trait);
				}
				catch (Exception e) { MessageBox.Show("v1.0.1.0 Transfer: Couldn't add Trait \"" + trait.name + "\"\nReason: " + e.Message, "Error"); }
			}

			// Technique Tab
			project.techniques.Clear();
			MainForm.TechList.Clear();
			for (int i = 0; i < techniques.Count; ++i) {
				Project2.Tech tech = new Project2.Tech();
				tech.name = techniques[i].name;
				try {
					tech.Roku = Rokushiki.RokuName.NONE;
					tech.rank = int.Parse(techniques[i].rank);
					tech.reg_TP = int.Parse(techniques[i].reg_TP);
					tech.sp_TP = int.Parse(techniques[i].sp_TP);
					tech.sp_trait = techniques[i].sp_trait;
					tech.rank_trait = techniques[i].rank_trait;
					tech.branch_tech = techniques[i].branched;
					tech.app_traits = "";
					tech.branch_rank = 0;
					tech.type = techniques[i].type;
					tech.range = techniques[i].range;
					Parse_String_to_StatsInt(ref tech, techniques[i].stats);
					tech.NA_power = false;
					tech.effects = new List<Project2.Effect>();
					tech.power = techniques[i].power;
					tech.DF_options = new List<bool>() { false, false, false, false, false };
					tech.Cyborg = new List<bool>() { false, false, false };
					tech.notes = techniques[i].TP_note;
					tech.desc = techniques[i].desc;
					project.techniques.Add(tech);
				}
				catch (Exception e) { MessageBox.Show("v1.0.1.0 Transfer: Couldn't add Technique \"" + tech.name + "\"\nReason: " + e.Message, "Error"); }
			}
		}

		// Returns true if there are no more commas
		// Returns false if there are still commas
		private bool Parse_Stat(ref string stat_string, ref int stat_val) {
			int ind_space = stat_string.IndexOf(' ');
			int val = int.Parse(stat_string.Substring(1, ind_space - 1));
			if (stat_string[0] == '-') { val *= -1; }
			stat_val = val;
			int comma_ind = stat_string.IndexOf(',');
			if (comma_ind == -1) { return true; }
			else { stat_string = stat_string.Remove(0, comma_ind + 2); } // We want to remove the space after the comma as well
			return false;
		}

		// Not gonna bother commenting this, I'm just mindlessly doing it.
		private void Parse_String_to_StatsInt(ref Project2.Tech tech, string stats) {
			try {
				if (!stats.Contains("Str")) { tech.str = 0; }
				else if (Parse_Stat(ref stats, ref tech.str)) { return; }
				if (!stats.Contains("Spe")) { tech.spe = 0; }
				else if (Parse_Stat(ref stats, ref tech.spe)) { return; }
				if (!stats.Contains("Sta")) { tech.sta = 0; }
				else if (Parse_Stat(ref stats, ref tech.sta)) { return; }
				if (!stats.Contains("Acc")) { tech.acc = 0; }
				else if (Parse_Stat(ref stats, ref tech.acc)) { return; }
			}
			catch (Exception e) { MessageBox.Show("Couldn't Parse Stats correctly for Technique \"" + tech.name + "\"\nReason: " + e.Message, "Error"); }
		}
	}
}