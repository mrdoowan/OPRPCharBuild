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
		private bool marksman_primary;	// Used to reduce the Cost of Range / AoE
		private bool inventor_primary;
		private static int short_cost;	// Huh static casting
		private static int short_min;
		private static int med_cost;
		private static int med_min;
		private static int long_cost;
		private static int long_min;
		private static int Vlong_cost;
		private static int Vlong_min;
		private static int shortAOE_cost;
		private static int shortAOE_min;
		private static int medAOE_cost;
		private static int medAOE_min;
		private static int longAOE_cost;
		private static int longAOE_min;
		// Dictionary might work, but you can add the same effect twice. We can work around this by 

		// Default Constructor
		public Effects() {
			MainForm.Set_Primary_Bool("Marksman", ref marksman_primary);
			MainForm.Set_Primary_Bool("Inventor", ref inventor_primary);
			if (marksman_primary) {
				short_cost = 0;
				short_min = 0;
				med_cost = 4;
				med_min = 4;
				long_cost = 8;
				long_min = 28;
				Vlong_cost = 16;
				Vlong_min = 44;
			}
			else {
				short_cost = 4;
				short_min = 4;
				med_cost = 8;
				med_min = 28;
				long_cost = 16;
				long_min = 44;
				Vlong_cost = 32;
				Vlong_min = 66;
			}
			if (inventor_primary) {
				shortAOE_cost = 0;
				shortAOE_min = 0;
				medAOE_cost = 8;
				medAOE_min = 8;
				longAOE_cost = 16;
				longAOE_min = 28;
			}
			else {
				shortAOE_cost = 8;
				shortAOE_min = 8;
				medAOE_cost = 16;
				medAOE_min = 28;
				longAOE_cost = 32;
				longAOE_min = 44;
			}
			// Then Add onto the Dictionary after being initialized
			EffectInfo_Dict.Add(Effect_Name.MELEE_RANGE, new EffectInfo("Melee", false, 0, 0,
				"Range of direct physical combat."));
			EffectInfo_Dict.Add(Effect_Name.SHORT_RANGE, new EffectInfo("Short", false, short_cost, short_min,
				   "Range of a few meters."));
			EffectInfo_Dict.Add(Effect_Name.MED_RANGE, new EffectInfo("Medium", false, med_cost, med_min,
				   "Range of half of a sport's stadium."));
			EffectInfo_Dict.Add(Effect_Name.LONG_RANGE, new EffectInfo("Long", false, long_cost, long_min,
				   "Range of an entire sport's stadium."));
			EffectInfo_Dict.Add(Effect_Name.V_LONG_RANGE, new EffectInfo("Very Long", false, Vlong_cost, Vlong_min,
				   "Range of a small city."));
			EffectInfo_Dict.Add(Effect_Name.SHORT_AOE, new EffectInfo("Short AoE", false, shortAOE_cost, shortAOE_min,
				   "A few meters in diameter."));
			EffectInfo_Dict.Add(Effect_Name.MEDIUM_AOE, new EffectInfo("Medium AoE", false, medAOE_cost, medAOE_min,
				   "Half of a sport's stadium in diameter."));
			EffectInfo_Dict.Add(Effect_Name.LONG_AOE, new EffectInfo("Long AoE", false, longAOE_cost, longAOE_min,
				   "An entire sport's stadium in diameter."));
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
			SENSORY_SING,
			SENSORY_MULT,
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

		public struct EffectInfo
		{
			public string name;
			public bool general;
			public int cost;
			public int MinRank;
			public string desc;

			// Default Constructor
			public EffectInfo(string name_, bool gen_, int cost_, int MinRank_, string desc_) {
				name = name_;
				general = gen_;
				cost = cost_;
				MinRank = MinRank_;
				desc = desc_;
			}
		}

		// -----------------------------------------------------------------
		// Main Member functions
		// -----------------------------------------------------------------

		// Dictionary: string -> enum
		static private Dictionary<string, Effect_Name> EffectID_Dict = new Dictionary<string, Effect_Name>() {
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
			{"Secondary General Effect", Effect_Name.SECONDARY_GEN},
			{"Speed", Effect_Name.SPEED},
			{"Piercing", Effect_Name.PIERCE},
			{"After-Image", Effect_Name.AFT_IMG},
			{"Additional After-Image", Effect_Name.ADD_AFT_IMG},
			{"Reversal", Effect_Name.REVERSE},
			{"Duration Damage", Effect_Name.DUR_DMG},
			{"Disables", Effect_Name.DISABLE},
			{"Sensory Overload (Single)", Effect_Name.SENSORY_SING},
			{"Sensory Overload (Multiple)", Effect_Name.SENSORY_MULT},
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

		// Inverse Dictionary: enum -> info
		private Dictionary<Effect_Name, EffectInfo> EffectInfo_Dict = new Dictionary<Effect_Name, EffectInfo>() {
			#region Database of Effect Information
			// The Ranges are added at a later part when Effects is initialized
			{Effect_Name.DISPLACE, new EffectInfo("Displacement", true, 8, 8,
				"Common examples are knockback techniques. The effect of a displacement technique can be greater at higher ranks. A stronger opponent will be displaced less by a weaker character using a displacement technique on them.")},
			{Effect_Name.DISORI, new EffectInfo("Disorient", true, 8, 8,
				"Techniques which unbalance or use visual tricks to hinder opponents. Effectiveness increases the higher the rank.")},
			{Effect_Name.GATLING, new EffectInfo("Gatling", true, 8, 8,
				"A series of rapid attacks performed quickly. Each individual strike is weaker but the greater amount of attacks make them harder to avoid. Higher ranked gatlings have even more strikes.")},
			{Effect_Name.DEFLECT, new EffectInfo("Deflecting", true, 8, 8,
				"This is applicable for Defensive Type techniques only. Techniques which divert the direction of an attack. Different from Reversal techniques which attempt to send an opponent's attack back at them. With higher ranks things like bullets and even elements can be deflected.")},
			{Effect_Name.RICO, new EffectInfo("Ricochet", true, 8, 8,
				"Techniques which use walls or other surfaces to bounce projectiles off. Higher ranks can have more complex ricochets, multiple 'bounces' etc.")},
			{Effect_Name.UNPRED, new EffectInfo("Unpredictable", true, 8, 14,
				"Techniques which even the user cannot anticipate the outcome of. Encompasses the realm of indirect attacks and others. The results are dependent on the situation they are used in. As the user does not know the outcome, anticipation techniques such as observation haki are less effective versus them.")},
			{Effect_Name.DMG_TYPE, new EffectInfo("Damage Type Change", true, 4, 14,
				"A punch that cuts or a blade with the sharp edge of a pillow. Higher ranked versions can have multiple damage types or supernatural types etc.")},
			{Effect_Name.DISARM, new EffectInfo("Disarm", true, 8, 8,
				"Disable techniques which target a weapon or item of an opponent to temporarily remove or steal it in combat. Requires traits like Anti-Weapon Specialist. Higher rank has higher chance of success with respect to RP considerations.")},
			{Effect_Name.SHOCK, new EffectInfo("Shockwaves", true, 10, 14,
				"Ranged melee techniques generated by striking the air with a fist/sword swing etc. The cost of the effect is paid by the range table below. Larger shockwaves will also need to pay AoE.")},
			{Effect_Name.CURVE, new EffectInfo("Curving Projectiles", true, 14, 20,
				"Bullets, arrows, shockwaves and other projectile techniques that do not follow a straight path but instead move in the air. At higher ranks, they can perform more unlikely turns in the air though the path they follow is always per-determined when fired.")},
			{Effect_Name.OMNI_DI, new EffectInfo("Omni-directional", true, 10, 20,
				"\"Perfect-sphere\" or techniques that attack/defend in all directions surrounding the user. They have a built in melee AoE around the user and pay range increases from the AoE table below. ")},
			{Effect_Name.HAKI_ENH, new EffectInfo("Haki Enhancement", true, 12, 12,
				"User coats an invisible suit of armour around a pair of limbs or a weapon. When added to techniques at Rank 28 and beyond, they gain the ability to strike a devil fruit user's real body.")},
			{Effect_Name.ELE_DMG, new EffectInfo("Elemental Damage", true, 14, 14,
				"Adds the benefits of a single element to a technique. If created through practical means (ie Inventions, Matches etc), the minimum rank is Rank 14. If created by shonen science or will, minimum rank is Rank 28. Can be generated in greater volumes at higher ranks.")},
			{Effect_Name.FLAV, new EffectInfo("Flavour", true, 0, 0,
				"Technique flair that does not grant any advantage other than making things look fancier. The higher the rank, the more exotic.")},
			{Effect_Name.SPIRIT, new EffectInfo("Spirit Generated Illusions", true, 14, 28,
				"Eye catching illusions that have significance, lasting only momentarily in battle but can shock or intimidate foes. Asura is a good example for these effects.")},
			{Effect_Name.SECONDARY_GEN, new EffectInfo("Secondary General Effect", false, 4, 4,
				"A cost when using more than two General Effects, paid each time an extra secondary general effect is added.")},
			{Effect_Name.SPEED, new EffectInfo("Speed", false, 4, 4,
				"Techniques that focus more on striking an opponent as quickly as possible rather than with sheer force thus they often don't hit as hard as techniques of similar rank but come out faster.")},
			{Effect_Name.PIERCE, new EffectInfo("Piercing", false, 8, 8,
				"Techniques that hold great penetrative force. These attacks are able to pass through an object with ease assuming they are not equivalent to a tier material. With each thing these attacks pass through they lose a significant amount of power.")},
			{Effect_Name.AFT_IMG, new EffectInfo("After-Image", false, 8, 14,
				"A singular after-image that looks like the user. The after image repeats an action performed at high speed by the user though at a slower pace to be perceived normally. It cannot do damage, nor has any physical form and will disperse after 1 post or after it is hit. The power of these techniques improves the authenticity of the after-image.")},
			{Effect_Name.ADD_AFT_IMG, new EffectInfo("Additional After-Image", false, 4, 18,
				"NOTE: Requires After Image as an effect. Allows the technique to gain an extra after-image. This effect can stack for each additional payment.")},
			{Effect_Name.REVERSE, new EffectInfo("Reversal", false, 16, 16,
				"This is applicable for Defensive Type techniques only. Similar but not limited to an upgraded version of deflecting. Reversal techniques are all attempts to repel a form of attack back at an opponent. With higher ranks, more obscure or supernatural techniques can be reversed.")},
			{Effect_Name.DUR_DMG, new EffectInfo("Duration Damage", false, 8, 8,
				"Encompasses all damage over time abilities. Deep cuts that cause bleeding or venom that cause persistent pain. Higher ranks can include other effects that persist for several turns.")},
			{Effect_Name.DISABLE, new EffectInfo("Disables", false, 14, 14,
				"Techniques which target a specific part of the body to cripple temporarily, whether through dislocation or numbing etc.")},
			{Effect_Name.SENSORY_SING, new EffectInfo("Sensory Overload (Single)", false, 14, 14,
				"Disable techniques which target a specific sense or multiple senses. Examples can be blinding flashes or deafening noises.")},
			{Effect_Name.SENSORY_MULT, new EffectInfo("Sensory Overload (Multiple)", false, 30, 30,
				"Disable techniques which target a specific sense or multiple senses. Examples can be blinding flashes or deafening noises.")},
			{Effect_Name.SUP_SPE, new EffectInfo("Super-Speed", false, 28, 28,
				"Techniques which move at speeds that do not fall under the Speed Statistic such as Lasers or Soru. They are not impossible to avoid, the more Speed/Accuracy, the less difficulty it will be with dealing with these types of attacks and abilities though they will always be perceived as blurred attacks or narrowly avoided.")},
			{Effect_Name.DEF_BYP, new EffectInfo("Defense Bypassing", false, 28, 28,
				"Techniques which bypass Defensive Type Techniques by transferring damage through blocks on contact or creating shockwaves that attack an opponent internally.")},
			{Effect_Name.SPEC_BLOCK, new EffectInfo("Specific Type Block", false, 28, 28,
				"Block techniques with special properties to defend against a specific type of attack. They significantly reduce damage against the chosen type regardless of the power behind them.")},
			{Effect_Name.START_BREAK, new EffectInfo("Starter Tier Breaking", false, 14, 14,
				"A technique capable of breaking a single weapon or piece of armour of Starter Tier Material (i.e. Iron).")},
			{Effect_Name.MID_BREAK, new EffectInfo("Mid Tier Breaking", false, 28, 28,
				"A technique capable of breaking ta single weapon or piece of armour of Medium Tier Material (i.e. Steel).")},
			{Effect_Name.HIGH_BREAK, new EffectInfo("High Tier Breaking", false, 44, 44,
				"A technique capable of breaking a single weapon or piece of armour of High Tier Material (i.e. Titanium).")},
			{Effect_Name.ARM_HAKI, new EffectInfo("Armaments Haki", false, 6, 14,
				"User creates a coat of hardened armour around a pair of limbs or a weapon. Offers passive defensive benefit equal to technique power. At Rank 28 and beyond, Armaments Haki gains the passive ability to strike a devil fruit user's real body whilst this buff is active on them.")},
			{Effect_Name.GRAND_MAST, new EffectInfo("Grand Master", false, 8, 14,
				"User has attained legendary skill in their mastery field and can perform supernatural feats such as striking fluid and insubstantial substances. Offers passive offensive benefit equal to technique power. At Rank 28 and beyond, Grand Master users gain the passive ability to strike a Logia user's real body with their mastery techniques.")},
			{Effect_Name.ELE_COAT, new EffectInfo("Elemental Coating", false, 14, 28,
				"User has developed a technique to generate and bind a type of element to a pair of limbs or a weapon. Grants the passive benefits of the element, and a passive offensive benefit equal to technique power.")},
			{Effect_Name.FULLBODY, new EffectInfo("Full Body Effects", false, 10, 20,
				"The cost of extending an effect of a technique from limbs to the entire body. Effects such as Tekkai, Armaments Haki amongst others use this.")},
			{Effect_Name.START_DEF, new EffectInfo("Starter Tier Defense", false, 4, 10,
				"Reduces damage of Rank 13 and below techniques. NOTE THAT 1) Tier Defenses require special abilities, 2) Wearable armor requires these techniques, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.")},
			{Effect_Name.MID_DEF, new EffectInfo("Mid Tier Defense", false, 12, 22,
				"Reduces damage of Rank 27 and below techniques. NOTE THAT 1) Tier Defenses require special abilities, 2) Wearable armor requires these techniques, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.")},
			{Effect_Name.HIGH_DEF, new EffectInfo("High Tier Defense", false, 24, 36,
				"Reduces damage of Rank 43 and below techniques. NOTE THAT 1) Tier Defenses require special abilities, 2) Wearable armor requires these techniques, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.")},
			{Effect_Name.CLOUD, new EffectInfo("Cloud", true, 8, 8,
				"(WEATHERMANCY) - Techniques used in order to create clouds to empower other Weathermancy techniques. Clouds scale in size with rank allowing the caster a greater area to work with and control the field through their other techniques.")},
			{Effect_Name.RAIN, new EffectInfo("Rain", true, 8, 8,
				"(WEATHERMANCY) - Techniques that create rain along with any other effects that may or may not be present. At lower ranks the amount of water created is not much more than a drizzle, but at higher ranks these can scale into heavy downpours or water manipulation feats similar to users of fishman karate. May be combined with elemental damage for effects such as snow, hail and acid rain.")},
			{Effect_Name.FOG, new EffectInfo("Fog/Mist", true, 14, 14,
				"(WEATHERMANCY) - A form of Sensory Overload that hinders vision of targets in an area.")},
			{Effect_Name.ELE_DMG_WEAT, new EffectInfo("Elemental Damage (Weather)", true, 10, 10,
				"(WEATHERMANCY) - Almost all Weathermancy techniques are elemental-based, allowing Weathermancers access to elemental techniques for the reduced cost of 10. This can include lightning, frost or even heat waves. However, unless super speed is paid for in the technique, lightning will not travel at supersonic speeds.")},
			{Effect_Name.WIND, new EffectInfo("Wind", true, 10, 14,
				"(WEATHERMANCY) - Blasts of wind fall under shockwave rules. They may also have effects such as Duration Damage and AOE added in order to form hurricanes.")},
			{Effect_Name.MILK, new EffectInfo("Milky Cloud", true, 8, 8,
				"(WEATHERMANCY) - Techniques used to create Sea Clouds that are sustainable in the Blue Sea. These clouds are physically dense enough to be walked upon or used to form walls or other items, but they do not have a material tier and are rather useless as weapons. At higher ranks the user may create a larger quantity of this strange cloud.")},
			{Effect_Name.MIR_CLONE, new EffectInfo("Mirage (Clones)", false, 8, 28,
				"(WEATHERMANCY) - Techniques in which the user cast’s a mirage in order to make copies of themselves or something else in the vicinity. Due to the reflective nature the mirages move in unison with whatever they have replicated but of course remain intangible and cannot cause any harm even when attacking. The power of the technique indicates how accurate the mirage is. The size of objects that can be replicated scales with the minimum ranks of the AoE table.")},
			{Effect_Name.MIR_CAMO, new EffectInfo("Mirage (Camouflage)", false, 14, 28,
				"(WEATHERMANCY) - Techniques in which the user casts a mirage in order to hide their physical presence or the presence of something else in the vicinity. Effectiveness scales with power.")},
			{Effect_Name.SENTIENCE, new EffectInfo("Sentience", false, 10, 10,
				"(POP GREENS) - Plants are capable of attained a limited sentience, allowing them to attack targets and perform simple tasks. This could include biting targets within range, ensnaring them, or even something simple such as transportation and delivering messages.")},
			{Effect_Name.FLORAL, new EffectInfo("Floral Structures", true, 8, 8,
				"(POP GREENS) - Plants can be created for use as weapons, such as bamboo spears and thorny bludgeons. They can also be made into tools, forming rafts, trampolines and other useful items. These creations have durations according to the professional buff table. Additionally, weapons created in the image of a weapon that a player has techniques for may be used in place of the actual weapon. Floral structures do not have a material tier.")},
			{Effect_Name.WOOD_DEF, new EffectInfo("Wooden Defenses", false, 4, 10,
				"(POP GREENS) - Wooden blockades that provide a defence against incoming attacks, paid for according to the material tier defence costs. They last one turn by default, but this duration may be extended for an additional cost.")},
			{Effect_Name.ELE_DMG_POP, new EffectInfo("Elemental Damage (Pop Green)", true, 14, 14,
				"(POP GREENS) - Pop Greens may be used to easily cause elemental effects, such as damaging poisons and fire. They therefore have a minimum rank of 14, rather than 28.")},
			{Effect_Name.SMOKE, new EffectInfo("Smoke", true, 8, 8,
				"(STEALTH) - A rapidly dispersing gaseous substance used to reduce visibility of that which is encompassed. Usually contained within a pellet or bomb of some sort, it can mask the silhouette of one person by default but can be combined with AoE to affect larger areas.")},
			{Effect_Name.CROWD_BLEND, new EffectInfo("Crowd Blending", true, 8, 8,
				"(STEALTH) - The ability to blend in with a crowd of people in order to avoid detection and shake off pursuers. Becomes more effective with higher rank. Consumes 1/2 AE in order to keep active, and is subject to common sense.")},
			{Effect_Name.SILENT, new EffectInfo("Silent", true, 8, 14,
				"(STEALTH) - Causing an attack or movement to produce very little sound, making it undetectable through hearing alone. More effective at higher ranks. Consumes 1/2 AE in order to keep active.")},
			{Effect_Name.SCENTLESS, new EffectInfo("Scentless", true, 14, 14,
				"(STEALTH) - Removing scent from one's person. Doing so can throw off detection based around scent, making a character more difficult to track. More effective at higher ranks.")},
			{Effect_Name.DISGUISE, new EffectInfo("Disguise", true, 4, 4,
				"(STEALTH) - Disguising oneself (or sometimes another), usually in the form of different clothing and make-up. Useful for infiltration. However, for a character with high fame, putting on a disguise can often not be enough to prevent them from being recognised. A disguise technique of rank less than roughly fame/4 will be less likely to convince others. Especially so if very noticeable physical traits are not masked. Keeping up a disguise does not consume upkeep.")},
			{Effect_Name.PICKPOCK, new EffectInfo("Pickpocket", true, 8, 8,
				"(STEALTH) - The act of taking an item from another, without their consent. Pickpocketing is a tricky art that requires masterful sleight of hand, and can therefore be noticed (but not necessarily prevented) if the target is aware enough. They may realise more quickly if items of noticeable size suddenly go missing, even if not immediately. May be combined with Material Breaker effects in order to effectively remove even items attached to things.")},
			{Effect_Name.NAT_CAMO, new EffectInfo("Natural Camouflage", true, 14, 14,
				"(STEALTH) - Blending in to the environment through masterful knowledge of the surroundings and the perception of others. At lower ranks, this skill may be used to avoid detection in areas that provide a natural camouflage; shadows, night and foliage to name a few.")},
			{Effect_Name.OPEN_CAMO, new EffectInfo("Open Camouflage", true, 28, 28,
				"(STEALTH) - Blending in to the environment through masterful knowledge of the surroundings and the perception of others. At this high rank, it becomes possible to blend in seemingly with mere open space. Consumes 1/2 AE in order to keep active.")},
			{Effect_Name.NONE, new EffectInfo("Error", true, 0, 0, "Report Bug")}
			#endregion 
		};

		public Effect_Name Get_EffectID(string effect) {
			try {
				return EffectID_Dict[effect];
			}
			catch {
				return Effect_Name.NONE;
			}
		}

		public EffectInfo Get_EffectInfo(Effect_Name ID) {
			try {
				return EffectInfo_Dict[ID];
			}
			catch {
				return new EffectInfo();
			}
		}
	}
}
