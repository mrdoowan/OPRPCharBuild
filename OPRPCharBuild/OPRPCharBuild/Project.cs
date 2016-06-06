using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

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

		/* public delegate void NewProjectEventHandler(object sender, NewProjectEventArgs e);

		[field: NonSerialized]
		public event NewProjectEventHandler OnNewProjectHandler;

		protected void OnNewProject(NewProjectEventArgs e) {
			if (OnNewProjectHandler != null) {
				OnNewProjectHandler(this, e);
			}
		} */

		// --- Save Functions

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
				Professions profession = new Professions();
				profession.name = eachitem.SubItems[0].Text;
				profession.primary = eachitem.SubItems[1].Text;
				profession.desc = eachitem.SubItems[2].Text;
				profession.bonus = eachitem.SubItems[3].Text;
				professions.Add(profession);
			}
		}

		public void SaveProject_Physical(string height_, string weight_, string hair_, string eye_, string clothing_, string appearance_,
			string url_, bool fullres_, int imgwidth_, int imgheight_) {
			height = height_;
			weight = weight_;
			hair = hair_;
			eye = eye_;
			clothing = clothing_;
			appearance = appearance_;
			img_url = url_;
			full_res = fullres_;
			img_width = imgwidth_;
			img_height = imgheight_;
		}

		public void SaveProject_Background(string island_, string region_, string personality_, string history_) {
			island = island_;
			region = region_;
			personality = personality_;
			history = history_;
		}

		public void SaveProject_Combat(string combat_, ListView weaponry_, ListView items_, string beli_,
			string DFname_, string DFtype_, string DFdesc_) {
			combat = combat_;
			weaponry.Clear();
			foreach (ListViewItem eachitem in weaponry_.Items) {
				Equipment weapon = new Equipment();
				weapon.name = eachitem.SubItems[0].Text;
				weapon.desc = eachitem.SubItems[1].Text;
				weaponry.Add(weapon);
			}
			items.Clear();
			foreach (ListViewItem eachitem in items_.Items) {
				Equipment item = new Equipment();
				item.name = eachitem.SubItems[0].Text;
				item.desc = eachitem.SubItems[1].Text;
				items.Add(item);
			}
			beli = beli_;
			DF_name = DFname_;
			DF_type = DFtype_;
			DF_desc = DFdesc_;
		}

		public void SaveProject_Stats(int SDEarned_, int SDtoSP_, CheckedListBox AP_, int usedFort_,
			int str_, int spe_, int sta_, int acc_) {
			SD_Earned = SDEarned_;
			SDtoSP = SDtoSP_;
			foreach (CheckBox chkBox in AP_.Controls) {
				AP.Add(chkBox.Checked);
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
				Trait trait = new Trait();
				trait.name = eachitem.SubItems[0].Text;
				trait.type = eachitem.SubItems[1].Text;
				trait.gen_num = eachitem.SubItems[2].Text;
				trait.prof_num = eachitem.SubItems[3].Text;
				trait.desc = eachitem.SubItems[4].Text;
				traitsList.Add(trait);
			}
		}

		public void SaveProject_Tech(ListView techList_) {
			techniques.Clear();
			foreach (ListViewItem eachitem in techList_.Items) {
				Tech tech = new Tech();
				tech.name = eachitem.SubItems[0].Text;
				tech.rank = eachitem.SubItems[1].Text;
				tech.reg_TP = eachitem.SubItems[2].Text;
				tech.sp_TP = eachitem.SubItems[3].Text;
				tech.sp_trait = eachitem.SubItems[4].Text;
				tech.rank_trait = eachitem.SubItems[5].Text;
				tech.branched = eachitem.SubItems[6].Text;
				tech.branch_TP = eachitem.SubItems[7].Text;
				tech.type = eachitem.SubItems[8].Text;
				tech.range = eachitem.SubItems[9].Text;
				tech.stats = eachitem.SubItems[10].Text;
				tech.power = eachitem.SubItems[11].Text;
				tech.effect = eachitem.SubItems[12].Text;
				tech.TP_note = eachitem.SubItems[13].Text;
				tech.desc = eachitem.SubItems[14].Text;
				techniques.Add(tech);
			}
		}

		// --- Load Functions (This is only to retract from the save file onto here)

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
			_prof.Items.Clear();
			for (int i = 0; i < professions.Count; ++i) {
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = professions[i].name;
				item.SubItems.Add(professions[i].primary);
				item.SubItems.Add(professions[i].desc);
				item.SubItems.Add(professions[i].bonus);
				_prof.Items.Add(item);
			}
		}

		public void LoadProject_Physical(ref TextBox _height, ref TextBox _weight, ref RichTextBox _hair, ref RichTextBox _eye,
			ref RichTextBox _clothing, ref RichTextBox _appearance, ref TextBox _url, ref CheckBox _fullres,
			ref NumericUpDown _imgwidth, ref NumericUpDown _imgheight) {
			_height.Text = height;
			_weight.Text = weight;
			_hair.Text = hair;
			_eye.Text = eye;
			_clothing.Text = clothing;
			_appearance.Text = appearance;
			_url.Text = img_url;
			_fullres.Checked = full_res;
			_imgwidth.Value = img_width;
			_imgheight.Value = img_height;
		}

		public void LoadProject_Background(ref TextBox _island, ref ComboBox _region, ref RichTextBox _personality, ref RichTextBox _history) {
			_island.Text = island;
			_region.Text = region;
			_personality.Text = personality;
			_history.Text = history;
		}

		public void LoadProject_Combat(ref RichTextBox _combat, ref ListView _weaponry, ref ListView _items, ref TextBox _beli,
			ref TextBox _DFname, ref ComboBox _DFtype, ref RichTextBox _DFdesc) {
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
		}

		public void LoadProject_Stats(ref NumericUpDown _SDEarned, ref NumericUpDown _SDtoSP, ref CheckedListBox _AP, ref NumericUpDown _usedFort,
			ref NumericUpDown _str, ref NumericUpDown _spe, ref NumericUpDown _sta, ref NumericUpDown _acc) {
			_SDEarned.Value = SD_Earned;
			_SDtoSP.Value = SDtoSP;
			int index = 0;
			foreach (CheckBox chkBox in _AP.Controls) {
				chkBox.Checked = AP[index];
				index++;
			}
			_usedFort.Value = used_fort;
			_str.Value = str;
			_spe.Value = spe;
			_sta.Value = sta;
			_acc.Value = acc;
		}

		public void LoadProject_Traits(ref ListView _traitsList) {
			_traitsList.Items.Clear();
			for (int i = 0; i < traitsList.Count; ++i) {
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = traitsList[i].name;
				item.SubItems.Add(traitsList[i].type);
				item.SubItems.Add(traitsList[i].gen_num);
				item.SubItems.Add(traitsList[i].prof_num);
				item.SubItems.Add(traitsList[i].desc);
				_traitsList.Items.Add(item);
			}
		}

		public void LoadProject_Tech(ref ListView _techList) {
			_techList.Items.Clear();
			for (int i = 0; i < techniques.Count; ++i) {
				ListViewItem item = new ListViewItem();
				item.SubItems[0].Text = techniques[i].name;
				item.SubItems.Add(techniques[i].rank);
				item.SubItems.Add(techniques[i].reg_TP);
				item.SubItems.Add(techniques[i].sp_TP);
				item.SubItems.Add(techniques[i].sp_trait);
				item.SubItems.Add(techniques[i].rank_trait);
				item.SubItems.Add(techniques[i].branched);
				item.SubItems.Add(techniques[i].branch_TP);
				item.SubItems.Add(techniques[i].type);
				item.SubItems.Add(techniques[i].range);
				item.SubItems.Add(techniques[i].stats);
				item.SubItems.Add(techniques[i].power);
				item.SubItems.Add(techniques[i].effect);
				item.SubItems.Add(techniques[i].TP_note);
				item.SubItems.Add(techniques[i].desc);
				_techList.Items.Add(item);
			}
		}
	}
}
