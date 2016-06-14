using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPRPCharBuild
{
	class Effects
	{
		// Default Constructor
		public Effects() { }

		public enum Effect_Name
		{
			DISPLACE,
			DISORI,
			GATLING,
			DEFLECT,
			RICO,
			UNPRED,
			DMG_TYPE,
			DISARM,
			SHOCK,
			CURVE,
			OMNI_DI,
			HAKI_ENH,
			ELE_DMG,
			FLAV,
			SPIRIT,
			SECONDARY_GEN,
			SPEED,
			PIERCE,
			AFT_IMG,
			ADD_AFT_IMG,
			REVERSE,
			DUR_DMG,
			DISABLE,
			SENSORY,
			SUP_SPE,
			DEF_BYP,
			SPEC_BLOCK,
			START_BREAK,
			MID_BREAK,
			HIGH_BREAK,
			ARM_HAKI,
			GRAND_MAST,
			ELE_COAT,
			FULLBODY,
			START_DEF,
			MID_DEF,
			HIGH_DEF,
			MELEE_RANGE,
			SHORT_RANGE,
			MED_RANGE,
			LONG_RANGE,
			V_LONG_RANGE,
			SHORT_AOE,
			MEDIUM_AOE,
			LONG_AOE,
			CLOUD,
			RAIN,
			FOG,
			ELE_DMG_WEAT,
			WIND,
			MILK,
			MIR_CLONE,
			MIR_CAMO,
			SENTIENCE,
			FLORAL,
			WOOD_DEF,
			ELE_DMG_POP,
			SMOKE,
			CROWD_BLEND,
			SILENT,
			SCENTLESS,
			DISGUISE,
			PICKPOCK,
			NAT_CAMO,
			OPEN_CAMO
		}

		private bool general;	// True if it doesn't need to trade Power.
		private int cost;
		private int min_rank;
		private string desc;

		static private Dictionary<string, Effect_Name> Effect_Dict = new Dictionary<string, Effect_Name>() {
			#region Dictionary of Effects
			{"Displacement", Effect_Name.DISPLACE},
			{"Disorient", Effect_Name.DISORI},
			{"Gatling", Effect_Name.GATLING},
			{"Deflecting", Effect_Name.DEFLECT},
			{"Ricochet", Effect_Name.RICO},
			{"Unpredictable", Effect_Name.UNPRED},
			{"Damage Type Change", Effect_Name.DMG_TYPE},
			{"Disarm", Effect_Name.DISARM},
			{"Shockwaves", Effect_Name.SHOCK},
			{"Curving Projectiles", Effect_Name.CURVE},
			{"Omni-directional", Effect_Name.OMNI_DI},
			{"Haki Enhancement", Effect_Name.HAKI_ENH},
			{"Elemental Damage", Effect_Name.ELE_DMG},
			{"Flavour", Effect_Name.FLAV},
			{"Spirit Generated Illusions", Effect_Name.SPIRIT},
			{"Extra Gen Effects", Effect_Name.SECONDARY_GEN},
			{"Speed", Effect_Name.SPEED},
			{"Piercing", Effect_Name.PIERCE},
			{"After-Image", Effect_Name.AFT_IMG},
			{"Additional After-Image", Effect_Name.ADD_AFT_IMG},
			{"Reversal", Effect_Name.REVERSE},
			{"Duration Damage", Effect_Name.DUR_DMG},
			{"Disables", Effect_Name.DISABLE},
			{"Sensory Overload", Effect_Name.SENSORY},
			{"Super-Speed", Effect_Name.SUP_SPE},
			{"Defense Bypassing", Effect_Name.DEF_BYP},
			{"Specific Type Block", Effect_Name.SPEC_BLOCK},
			{"Starter Tier Breaking", Effect_Name.START_BREAK},
			{"Mid Tier Breaking", Effect_Name.MID_BREAK},
			{"High Tier Breaking", Effect_Name.HIGH_BREAK},
			{"Armaments Haki", Effect_Name.ARM_HAKI},
			{"Grand Master", Effect_Name.GRAND_MAST},
			{"Elemental Coating", Effect_Name.ELE_COAT},
			{"Full Body Effects", Effect_Name.FULLBODY},
			{"Starter Tier", Effect_Name.START_DEF},
			{"Mid Tier", Effect_Name.MID_DEF},
			{"High Tier", Effect_Name.HIGH_DEF},
			{"Melee", Effect_Name.MELEE_RANGE},
			{"Short", Effect_Name.SHORT_RANGE},
			{"Medium", Effect_Name.MED_RANGE},
			{"Long", Effect_Name.LONG_RANGE},
			{"Very Long", Effect_Name.V_LONG_RANGE},
			{"Short AoE", Effect_Name.SHORT_AOE},
			{"Medium AoE", Effect_Name.MEDIUM_AOE},
			{"Long AoE", Effect_Name.LONG_AOE},
			{"Cloud", Effect_Name.CLOUD},
			{"Rain", Effect_Name.RAIN},
			{"Fog/Mist", Effect_Name.FOG},
			{"Elemental Damage (Weather)", Effect_Name.ELE_DMG_WEAT},
			{"Wind", Effect_Name.WIND},
			{"Milky Cloud", Effect_Name.MILK},
			{"Mirage (Clones)", Effect_Name.MIR_CLONE},
			{"Mirage (Camouflage)", Effect_Name.MIR_CAMO},
			{"Sentience", Effect_Name.SENTIENCE},
			{"Floral Structures", Effect_Name.FLORAL},
			{"Wooden Defences", Effect_Name.WOOD_DEF},
			{"Elemental Damage (Pop Green)", Effect_Name.ELE_DMG_POP},
			{"Smoke", Effect_Name.SMOKE},
			{"Crowd Blending", Effect_Name.CROWD_BLEND},
			{"Silent", Effect_Name.SILENT},
			{"Scentless", Effect_Name.SCENTLESS},
			{"Disguise", Effect_Name.DISGUISE},
			{"Pickpocket", Effect_Name.PICKPOCK},
			{"Natural Camouflage", Effect_Name.NAT_CAMO},
			{"Open Camouflage", Effect_Name.OPEN_CAMO}
			#endregion
		};

		public void Effect_info_load(Effect_Name effect) {
			#region Database of Effects based on Techniques
			switch (effect) {
				case Effect_Name.DISPLACE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.DISORI:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.GATLING:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.DEFLECT:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.RICO:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.UNPRED:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.DMG_TYPE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.DISARM:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SHOCK:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.CURVE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.OMNI_DI:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.HAKI_ENH:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.ELE_DMG:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.FLAV:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SPIRIT:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SECONDARY_GEN:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SPEED:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.PIERCE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.AFT_IMG:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.ADD_AFT_IMG:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.REVERSE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.DUR_DMG:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.DISABLE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SENSORY:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SUP_SPE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.DEF_BYP:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SPEC_BLOCK:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.START_BREAK:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.MID_BREAK:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.HIGH_BREAK:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.ARM_HAKI:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.GRAND_MAST:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.ELE_COAT:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.FULLBODY:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.START_DEF:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.MID_DEF:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.HIGH_DEF:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.MELEE_RANGE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SHORT_RANGE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.MED_RANGE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.LONG_RANGE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.V_LONG_RANGE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SHORT_AOE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.MEDIUM_AOE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.LONG_AOE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.CLOUD:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.RAIN:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.FOG:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.ELE_DMG_WEAT:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.WIND:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.MILK:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.MIR_CLONE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.MIR_CAMO:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SENTIENCE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.FLORAL:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.WOOD_DEF:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.ELE_DMG_POP:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SMOKE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.CROWD_BLEND:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SILENT:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.SCENTLESS:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.DISGUISE:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.PICKPOCK:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.NAT_CAMO:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				case Effect_Name.OPEN_CAMO:
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "";
					break;
				default:
					MessageBox.Show("Effects enumeration screwed up", "Error");
					break;
			}
			#endregion
		}
	}
}
