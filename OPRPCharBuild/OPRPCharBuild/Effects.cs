using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPRPCharBuild
{
	public class Effects
	{
		// -----------------------------------------------------------------
		// Main Member variables and struct
		// -----------------------------------------------------------------
		private string name;
		private bool general;   // True if it doesn't need to trade Power.
		private int cost;
		private int min_rank;
		private string desc;
		private bool marksman_primary;	// Used to reduce the Cost of Range / AoE
		private bool inventor_primary;
		public static List<EffectItem> list = new List<EffectItem>();	// Static variable for project usage

		// Default Constructor
		public Effects() {
			MainForm.Set_Primary_Bool("Marksman", ref marksman_primary);
			MainForm.Set_Primary_Bool("Inventor", ref inventor_primary);
		}

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
			OPEN_CAMO,
			NONE
		}

		public struct EffectItem
		{
			public string name;
			public bool general;
			public int cost;
			public int min_rank;

			// Default Constructor
			public EffectItem(string name_, bool gen_, int cost_, int MinRank_) {
				name = name_;
				general = gen_;
				cost = cost_;
				min_rank = MinRank_;
			}
		}

		// -----------------------------------------------------------------
		// Main Member functions
		// -----------------------------------------------------------------

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
			{"Secondary General Effect Cost", Effect_Name.SECONDARY_GEN},
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
			{"Starter Tier Defense", Effect_Name.START_DEF},
			{"Mid Tier Defense", Effect_Name.MID_DEF},
			{"High Tier Defense", Effect_Name.HIGH_DEF},
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
			{"Wooden Defenses", Effect_Name.WOOD_DEF},
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

		public Effect_Name Get_EffectID(string effect) {
			try {
				return Effect_Dict[effect];
			}
			catch {
				return Effect_Name.NONE;
			}
		}

		// WARNING: This is ONLY used when comboBox_Effect.Text is changed
		// If used anywhere else, you will get MASSIVE BUGS
		public void Effect_info_load(Effect_Name effect) {
			switch (effect) {
				case Effect_Name.DISPLACE:
					name = "Displacement";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "Common examples are knockback techniques. The effect of a displacement technique can be greater at higher ranks. A stronger opponent will be displaced less by a weaker character using a displacement technique on them.";
					break;
				case Effect_Name.DISORI:
					name = "Disorient";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "Techniques which unbalance or use visual tricks to hinder opponents. Effectiveness increases the higher the rank.";
					break;
				case Effect_Name.GATLING:
					name = "Gatling";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "A series of rapid attacks performed quickly. Each individual strike is weaker but the greater amount of attacks make them harder to avoid. Higher ranked gatlings have even more strikes.";
					break;
				case Effect_Name.DEFLECT:
					name = "Deflecting";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "This is applicable for Defensive Type techniques only. Techniques which divert the direction of an attack. Different from Reversal techniques which attempt to send an opponent's attack back at them. With higher ranks things like bullets and even elements can be deflected.";
					break;
				case Effect_Name.RICO:
					name = "Ricochet";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "Techniques which use walls or other surfaces to bounce projectiles off. Higher ranks can have more complex ricochets, multiple 'bounces' etc.";
					break;
				case Effect_Name.UNPRED:
					name = "Unpredictable";
					general = true;
					cost = 8;
					min_rank = 14;
					desc = "Techniques which even the user cannot anticipate the outcome of. Encompasses the realm of indirect attacks and others. The results are dependent on the situation they are used in. As the user does not know the outcome, anticipation techniques such as observation haki are less effective versus them.";
					break;
				case Effect_Name.DMG_TYPE:
					name = "Damage Type Change";
					general = true;
					cost = 4;
					min_rank = 14;
					desc = "A punch that cuts or a blade with the sharp edge of a pillow. Higher ranked versions can have multiple damage types or supernatural types etc.";
					break;
				case Effect_Name.DISARM:
					name = "Disarm";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "Disable techniques which target a weapon or item of an opponent to temporarily remove or steal it in combat. Requires traits like Anti-Weapon Specialist. Higher rank has higher chance of success with respect to RP considerations.";
					break;
				case Effect_Name.SHOCK:
					name = "Shockwaves";
					general = true;
					cost = 10;
					min_rank = 14;
					desc = "Ranged melee techniques generated by striking the air with a fist/sword swing etc. The cost of the effect is paid by the range table below. Larger shockwaves will also need to pay AoE. ";
					break;
				case Effect_Name.CURVE:
					name = "Curving Projectiles";
					general = true;
					cost = 14;
					min_rank = 20;
					desc = "Bullets, arrows, shockwaves and other projectile techniques that do not follow a straight path but instead move in the air. At higher ranks, they can perform more unlikely turns in the air though the path they follow is always per-determined when fired.";
					break;
				case Effect_Name.OMNI_DI:
					name = "Omni-directional";
					general = true;
					cost = 10;
					min_rank = 20;
					desc = "\"Perfect-sphere\" or techniques that attack/defend in all directions surrounding the user. They have a built in melee AoE around the user and pay range increases from the AoE table below. ";
					break;
				case Effect_Name.HAKI_ENH:
					name = "Haki Enhancement";
					general = true;
					cost = 12;
					min_rank = 12;
					desc = "User coats an invisible suit of armour around a pair of limbs or a weapon. When added to techniques at Rank 28 and beyond, they gain the ability to strike a devil fruit user's real body.";
					break;
				case Effect_Name.ELE_DMG:
					name = "Elemental Damage";
					general = true;
					cost = 14;
					min_rank = 14;
					desc = "Adds the benefits of a single element to a technique. If created through practical means (ie Inventions, Matches etc), the minimum rank is Rank 14. If created by shonen science or will, minimum rank is Rank 28. Can be generated in greater volumes at higher ranks.";
					break;
				case Effect_Name.FLAV:
					name = "Flavour";
					general = true;
					cost = 0;
					min_rank = 0;
					desc = "Technique flair that does not grant any advantage other than making things look fancier. The higher the rank, the more exotic.";
					break;
				case Effect_Name.SPIRIT:
					name = "Spirit Generated Illusions";
					general = true;
					cost = 14;
					min_rank = 28;
					desc = "Eye catching illusions that have significance, lasting only momentarily in battle but can shock or intimidate foes. Asura is a good example for these effects.";
					break;
				case Effect_Name.SECONDARY_GEN:
					name = "Secondary General Effect Cost";
					general = false;
					cost = 4;
					min_rank = 4;
					desc = "A cost when using more than two General Effects, paid each time an extra secondary general effect is added.";
					break;
				case Effect_Name.SPEED:
					name = "Speed";
					general = false;
					cost = 4;
					min_rank = 4;
					desc = "Techniques that focus more on striking an opponent as quickly as possible rather than with sheer force thus they often don't hit as hard as techniques of similar rank but come out faster.";
					break;
				case Effect_Name.PIERCE:
					name = "Piercing";
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "Techniques that hold great penetrative force. These attacks are able to pass through an object with ease assuming they are not equivalent to a tier material. With each thing these attacks pass through they lose a significant amount of power.";
					break;
				case Effect_Name.AFT_IMG:
					name = "After-Image";
					general = false;
					cost = 8;
					min_rank = 14;
					desc = "A singular after-image that looks like the user. The after image repeats an action performed at high speed by the user though at a slower pace to be perceived normally. It cannot do damage, nor has any physical form and will disperse after 1 post or after it is hit. The power of these techniques improves the authenticity of the after-image.";
					break;
				case Effect_Name.ADD_AFT_IMG:
					name = "Additional After-Image";
					general = false;
					cost = 4;
					min_rank = 18;
					desc = "NOTE: Requires After Image as an effect. Allows the technique to gain an extra after-image. This effect can stack for each additional payment.";
					break;
				case Effect_Name.REVERSE:
					name = "Reversal";
					general = false;
					cost = 16;
					min_rank = 16;
					desc = "This is applicable for Defensive Type techniques only. Similar but not limited to an upgraded version of deflecting. Reversal techniques are all attempts to repel a form of attack back at an opponent. With higher ranks, more obscure or supernatural techniques can be reversed.";
					break;
				case Effect_Name.DUR_DMG:
					name = "Duration Damage";
					general = false;
					cost = 8;
					min_rank = 8;
					desc = "Encompasses all damage over time abilities. Deep cuts that cause bleeding or venom that cause persistent pain. Higher ranks can include other effects that persist for several turns.";
					break;
				case Effect_Name.DISABLE:
					name = "Disables";
					general = false;
					cost = 14;
					min_rank = 14;
					desc = "Techniques which target a specific part of the body to cripple temporarily, whether through dislocation or numbing etc.";
					break;
				case Effect_Name.SENSORY:
					name = "Sensory Overload";
					general = false;
					cost = 14;
					min_rank = 14;
					desc = "Disable techniques which target a specific sense or multiple senses. Examples can be blinding flashes or deafening noises.";
					break;
				case Effect_Name.SUP_SPE:
					name = "Super-Speed";
					general = false;
					cost = 28;
					min_rank = 28;
					desc = "Techniques which move at speeds that do not fall under the Speed Statistic such as Lasers or Soru. They are not impossible to avoid, the more Speed/Accuracy, the less difficulty it will be with dealing with these types of attacks and abilities though they will always be perceived as blurred attacks or narrowly avoided.";
					break;
				case Effect_Name.DEF_BYP:
					name = "Defense Bypassing";
					general = false;
					cost = 28;
					min_rank = 28;
					desc = "Techniques which bypass Defensive Type Techniques by transferring damage through blocks on contact or creating shockwaves that attack an opponent internally. ";
					break;
				case Effect_Name.SPEC_BLOCK:
					name = "Specific Type Block";
					general = false;
					cost = 28;
					min_rank = 28;
					desc = "Block techniques with special properties to defend against a specific type of attack. They significantly reduce damage against the chosen type regardless of the power behind them.";
					break;
				case Effect_Name.START_BREAK:
					name = "Starter Tier Breaking";
					general = false;
					cost = 14;
					min_rank = 14;
					desc = "A technique capable of breaking a single weapon or piece of armour of Starter Tier Material (i.e. Iron). ";
					break;
				case Effect_Name.MID_BREAK:
					name = "Mid Tier Breaking";
					general = false;
					cost = 28;
					min_rank = 28;
					desc = "A technique capable of breaking ta single weapon or piece of armour of Medium Tier Material (i.e. Steel). ";
					break;
				case Effect_Name.HIGH_BREAK:
					name = "High Tier Breaking";
					general = false;
					cost = 44;
					min_rank = 44;
					desc = "A technique capable of breaking a single weapon or piece of armour of High Tier Material (i.e. Titanium).";
					break;
				case Effect_Name.ARM_HAKI:
					name = "Armaments Haki";
					general = false;
					cost = 6;
					min_rank = 14;
					desc = "User creates a coat of hardened armour around a pair of limbs or a weapon. Offers passive defensive benefit equal to technique power. At Rank 28 and beyond, Armaments Haki gains the passive ability to strike a devil fruit user's real body whilst this buff is active on them. ";
					break;
				case Effect_Name.GRAND_MAST:
					name = "Grand Master";
					general = false;
					cost = 8;
					min_rank = 14;
					desc = "User has attained legendary skill in their mastery field and can perform supernatural feats such as striking fluid and insubstantial substances. Offers passive offensive benefit equal to technique power. At Rank 28 and beyond, Grand Master users gain the passive ability to strike a Logia user's real body with their mastery techniques.";
					break;
				case Effect_Name.ELE_COAT:
					name = "Elemental Coating";
					general = false;
					cost = 14;
					min_rank = 28;
					desc = "User has developed a technique to generate and bind a type of element to a pair of limbs or a weapon. Grants the passive benefits of the element, and a passive offensive benefit equal to technique power. ";
					break;
				case Effect_Name.FULLBODY:
					name = "Full Body Effects";
					general = false;
					cost = 10;
					min_rank = 20;
					desc = "The cost of extending an effect of a technique from limbs to the entire body. Effects such as Tekkai, Armaments Haki amongst others use this.";
					break;
				case Effect_Name.START_DEF:
					name = "Starter Tier Defense";
					general = false;
					cost = 4;
					min_rank = 10;
					desc = "Reduces damage of Rank 13 and below techniques. NOTE THAT 1) Tier Defenses require special abilities, 2) Wearable armor requires these techniques, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.";
					break;
				case Effect_Name.MID_DEF:
					name = "Mid Tier Defense";
					general = false;
					cost = 12;
					min_rank = 22;
					desc = "Reduces damage of Rank 27 and below techniques. NOTE THAT 1) Tier Defenses require special abilities, 2) Wearable armor requires these techniques, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.";
					break;
				case Effect_Name.HIGH_DEF:
					name = "High Tier Defense";
					general = false;
					cost = 24;
					min_rank = 36;
					desc = "Reduces damage of Rank 43 and below techniques. NOTE THAT 1) Tier Defenses require special abilities, 2) Wearable armor requires these techniques, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.";
					break;
				case Effect_Name.MELEE_RANGE:
					name = "Melee";
					general = false;
					cost = 0;
					min_rank = 0;
					desc = "Range of direct physical combat.";
					break;
				case Effect_Name.SHORT_RANGE:
					name = "Short";
					general = false;
					if (marksman_primary) {
						cost = 0;
						min_rank = 0;
					}
					else {
						cost = 4;
						min_rank = 4;
					}
					desc = "Range of a few meters.";
					break;
				case Effect_Name.MED_RANGE:
					name = "Medium";
					general = false;
					if (marksman_primary) {
						cost = 4;
						min_rank = 4;
					}
					else {
						cost = 8;
						min_rank = 28;
					}
					desc = "Range of half of a sport's stadium.";
					break;
				case Effect_Name.LONG_RANGE:
					name = "Long";
					general = false;
					if (marksman_primary) {
						cost = 8;
						min_rank = 28;
					}
					else {
						cost = 16;
						min_rank = 44;
					}
					desc = "Range of an entire sport's stadium.";
					break;
				case Effect_Name.V_LONG_RANGE:
					name = "Very Long";
					general = false;
					if (marksman_primary) {
						cost = 16;
						min_rank = 44;
					}
					else {
						cost = 32;
						min_rank = 66;
					}
					desc = "Range of a small city.";
					break;
				case Effect_Name.SHORT_AOE:
					name = "Short AoE";
					general = false;
					if (inventor_primary) {
						cost = 0;
						min_rank = 0;
					}
					else {
						cost = 8;
						min_rank = 8;
					}
					desc = "A few meters in diameter.";
					break;
				case Effect_Name.MEDIUM_AOE:
					name = "Medium AoE";
					general = false;
					if (inventor_primary) {
						cost = 8;
						min_rank = 8;
					}
					else {
						cost = 16;
						min_rank = 28;
					}
					desc = "Half of a sport's stadium in diameter.";
					break;
				case Effect_Name.LONG_AOE:
					name = "Long AoE";
					general = false;
					if (inventor_primary) {
						cost = 16;
						min_rank = 28;
					}
					else {
						cost = 32;
						min_rank = 44;
					}
					desc = "An entire sport's stadium in diameter.";
					break;
				case Effect_Name.CLOUD:
					name = "Cloud";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "(WEATHERMANCY) - Techniques used in order to create clouds to empower other Weathermancy techniques. Clouds scale in size with rank allowing the caster a greater area to work with and control the field through their other techniques.";
					break;
				case Effect_Name.RAIN:
					name = "Rain";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "(WEATHERMANCY) - Techniques that create rain along with any other effects that may or may not be present. At lower ranks the amount of water created is not much more than a drizzle, but at higher ranks these can scale into heavy downpours or water manipulation feats similar to users of fishman karate. May be combined with elemental damage for effects such as snow, hail and acid rain.";
					break;
				case Effect_Name.FOG:
					name = "Fog/Mist";
					general = true;
					cost = 14;
					min_rank = 14;
					desc = "(WEATHERMANCY) - A form of Sensory Overload that hinders vision of targets in an area.";
					break;
				case Effect_Name.ELE_DMG_WEAT:
					name = "Elemental Damage (Weather)";
					general = true;
					cost = 10;
					min_rank = 10;
					desc = "(WEATHERMANCY) - Almost all Weathermancy techniques are elemental-based, allowing Weathermancers access to elemental techniques for the reduced cost of 10. This can include lightning, frost or even heat waves. However, unless super speed is paid for in the technique, lightning will not travel at supersonic speeds.";
					break;
				case Effect_Name.WIND:
					name = "Wind";
					general = true;
					cost = 10;
					min_rank = 14;
					desc = "(WEATHERMANCY) - Blasts of wind fall under shockwave rules. They may also have effects such as Duration Damage and AOE added in order to form hurricanes. ";
					break;
				case Effect_Name.MILK:
					name = "Milky Cloud";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "(WEATHERMANCY) - Techniques used to create Sea Clouds that are sustainable in the Blue Sea. These clouds are physically dense enough to be walked upon or used to form walls or other items, but they do not have a material tier and are rather useless as weapons. At higher ranks the user may create a larger quantity of this strange cloud.";
					break;
				case Effect_Name.MIR_CLONE:
					name = "Mirage (Clones)";
					general = false;
					cost = 8;
					min_rank = 28;
					desc = "(WEATHERMANCY) - Techniques in which the user cast’s a mirage in order to make copies of themselves or something else in the vicinity. Due to the reflective nature the mirages move in unison with whatever they have replicated but of course remain intangible and cannot cause any harm even when attacking. The power of the technique indicates how accurate the mirage is. The size of objects that can be replicated scales with the minimum ranks of the AOE table.";
					break;
				case Effect_Name.MIR_CAMO:
					name = "Mirage (Camouflage)";
					general = false;
					cost = 14;
					min_rank = 28;
					desc = "(WEATHERMANCY) - Techniques in which the user casts a mirage in order to hide their physical presence or the presence of something else in the vicinity. Effectiveness scales with power.";
					break;
				case Effect_Name.SENTIENCE:
					name = "Sentience";
					general = false;
					cost = 10;
					min_rank = 10;
					desc = "(POP GREENS) - Plants are capable of attained a limited sentience, allowing them to attack targets and perform simple tasks. This could include biting targets within range, ensnaring them, or even something simple such as transportation and delivering messages.";
					break;
				case Effect_Name.FLORAL:
					name = "Floral Structures";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "(POP GREENS) - Plants can be created for use as weapons, such as bamboo spears and thorny bludgeons. They can also be made into tools, forming rafts, trampolines and other useful items. These creations have durations according to the professional buff table. Additionally, weapons created in the image of a weapon that a player has techniques for may be used in place of the actual weapon. Floral structures do not have a material tier.";
					break;
				case Effect_Name.WOOD_DEF:
					name = "Wooden Defenses";
					general = false;
					cost = 4;
					min_rank = 10;
					desc = "(POP GREENS) - Wooden blockades that provide a defence against incoming attacks, paid for according to the material tier defence costs. They last one turn by default, but this duration may be extended for an additional cost.";
					break;
				case Effect_Name.ELE_DMG_POP:
					name = "Elemental Damage (Pop Green)";
					general = false;
					cost = 14;
					min_rank = 14;
					desc = "(POP GREENS) - Pop Greens may be used to easily cause elemental effects, such as damaging poisons and fire. They therefore have a minimum rank of 14, rather than 28.";
					break;
				case Effect_Name.SMOKE:
					name = "Smoke";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "(STEALTH) - A rapidly dispersing gaseous substance used to reduce visibility of that which is encompassed. Usually contained within a pellet or bomb of some sort, it can mask the silhouette of one person by default but can be combined with AOE to affect larger areas.";
					break;
				case Effect_Name.CROWD_BLEND:
					name = "Crowd Blending";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "(STEALTH) - The ability to blend in with a crowd of people in order to avoid detection and shake off pursuers. Becomes more effective with higher rank. Consumes 1/2 AE in order to keep active, and is subject to common sense.";
					break;
				case Effect_Name.SILENT:
					name = "Silent";
					general = true;
					cost = 8;
					min_rank = 14;
					desc = "(STEALTH) - Causing an attack or movement to produce very little sound, making it undetectable through hearing alone. More effective at higher ranks. Consumes 1/2 AE in order to keep active.";
					break;
				case Effect_Name.SCENTLESS:
					name = "Scentless";
					general = true;
					cost = 14;
					min_rank = 14;
					desc = "(STEALTH) - Removing scent from one's person. Doing so can throw off detection based around scent, making a character more difficult to track. More effective at higher ranks.";
					break;
				case Effect_Name.DISGUISE:
					name = "Disguise";
					general = true;
					cost = 4;
					min_rank = 4;
					desc = "(STEALTH) - Disguising oneself (or sometimes another), usually in the form of different clothing and make-up. Useful for infiltration. However, for a character with high fame, putting on a disguise can often not be enough to prevent them from being recognised. A disguise technique of rank less than roughly fame/4 will be less likely to convince others. Especially so if very noticeable physical traits are not masked. Keeping up a disguise does not consume upkeep.";
					break;
				case Effect_Name.PICKPOCK:
					name = "Pickpocket";
					general = true;
					cost = 8;
					min_rank = 8;
					desc = "(STEALTH) - The act of taking an item from another, without their consent. Pickpocketing is a tricky art that requires masterful sleight of hand, and can therefore be noticed (but not necessarily prevented) if the target is aware enough. They may realise more quickly if items of noticeable size suddenly go missing, even if not immediately. May be combined with Material Breaker effects in order to effectively remove even items attached to things.";
					break;
				case Effect_Name.NAT_CAMO:
					name = "Natural Camouflage";
					general = true;
					cost = 14;
					min_rank = 14;
					desc = "(STEALTH) - Blending in to the environment through masterful knowledge of the surroundings and the perception of others. At lower ranks, this skill may be used to avoid detection in areas that provide a natural camouflage; shadows, night and foliage to name a few.";
					break;
				case Effect_Name.OPEN_CAMO:
					name = "Open Camouflage";
					general = true;
					cost = 28;
					min_rank = 28;
					desc = "(STEALTH) - Blending in to the environment through masterful knowledge of the surroundings and the perception of others. At this high rank, it becomes possible to blend in seemingly with mere open space. Consumes 1/2 AE in order to keep active.";
					break;
				default:
					// When None is selected
					name = "";
					general = false;
					cost = 0;
					min_rank = 0;
					desc = "";
					break;
			}
		}

		// -----------------------------------------------------------------
		// Getter variables
		// -----------------------------------------------------------------

		public string Get_Effect_Name() {
			return name;
		}

		public string Get_Effect_Desc() {
			return desc;
		}

		public bool Get_Effect_Gen() {
			return general;
		}

		public int Get_Effect_Cost() {
			return cost;
		}

		public int Get_Effect_MinRank() {
			return min_rank;
		}
	}
}
