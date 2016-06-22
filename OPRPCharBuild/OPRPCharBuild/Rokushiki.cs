using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild
{
	public class Rokushiki
	{
		// Default Constructor
		public Rokushiki() { }

		public struct RokuInfo
		{
			public string name;
			public int baseRank;
			public string type;
			public string range;
			public int basePower;
			public int str;
			public int spe;
			public int sta;
			public int acc;
			public string desc;

			public RokuInfo(string name_, int rank_, string type_, string range_, int power_, 
				int str_, int spe_, int sta_, int acc_, string desc_) {
				name = name_;
				baseRank = rank_;
				type = type_;
				range = range_;
				basePower = power_;
				str = str_;
				spe = spe_;
				sta = sta_;
				acc = acc_;
				desc = desc_;
			}
		}

		public enum RokuName
		{
			SHIGAN,
			RANKYAKU,
			SORU,
			KAMI,
			TEKKAI,
			GEPPO,
			ROKUOGAN,
			NONE
		}

		static private Dictionary<string, RokuName> Roku_Enum = new Dictionary<string, RokuName>() {
			{ "Shigan", RokuName.SHIGAN },
			{ "Rankyaku", RokuName.RANKYAKU },
			{ "Soru", RokuName.SORU },
			{ "Kami-E", RokuName.KAMI },
			{ "Tekkai", RokuName.TEKKAI },
			{ "Geppo", RokuName.GEPPO },
			{ "Rokuogan", RokuName.ROKUOGAN },
		};

		static private Dictionary<RokuName, RokuInfo> Roku_Dict = new Dictionary<RokuName, RokuInfo>() {
			{ RokuName.SHIGAN, new RokuInfo("Shigan", 22, "Offensive", "Melee", 4, 0, 0, 0, 0,
				"This technique is an improved melee strike. The damage done by the user's finger (or other small appendage) is treated as both blunt and piercing, generally using the more favourable of the two damage types when the other would not be as effective. Additionally, the attack is quick and nearly invisible to the naked eye, executed at bullet-like speeds. (Damage Type Change, Piercing, Super-Speed).") },
			{ RokuName.RANKYAKU, new RokuInfo("Rankyaku", 22, "Offensive", "Long", 8, 0, 0, 0, 0,
				"By kicking fast enough, the user literally \"cuts\" their air with their foot. The projectile is different from standard ranged melee, possessing a keen cutting edge and unusual longevity. A Rankyaku shockwave will continue moving until it reaches the edge of it's range or impacts with an obstacle harder than stone. (Damage Type Change, Piercing, Shockwave, Long Range)") },
			{ RokuName.SORU, new RokuInfo("Soru", 28, "Support", "Self", 0, 0, 0, 0, 0,
				"Once per post, an individual can use this technique to put on momentary bursts of speed that make them invisible to the untrained naked eye. The user may move to any location that they could normally move to in a single post, but they do so near-instantaneously. During this movement, they cannot be seen by anybody who's accuracy is less than double this technique's rank, appearing as a blurred streak to anybody who's accuracy is above that point. If attacked while moving by a foe who's speed is less than this technique's rank, the attack can be passively dodged without any expenditure of Focus. Attacks cannot be made while moving with Soru. (Super-Speed, Perception Formula)") },
			{ RokuName.KAMI, new RokuInfo("Kami-E", 32, "Defensive", "Self", 20, 0, 0, 0, 0,
				"Using instinct-driven, reflexive movements and loosening up all of their muscles, the user dodges an attack much the way a piece of paper in the breeze does. While in use, this technique reduces the effective accuracy of any targeted attack made against the user by a value equal to this technique's power. If this would reduce the accuracy of that attack to 0, the attack can be passively dodged without any expenditure of Focus. Attacks can not be made while using Kami-E. (Paperlike, Fullbody, Dodging Formula)") },
			{ RokuName.TEKKAI, new RokuInfo("Tekkai", 32, "Defensive", "Self", 22, 0, 0, 0, 0,
				"By tensing their muscles and focusing, the user of this technique may drastically reduce the damage they take from physical attacks. While this technique is active, the user is treated as if their body has steel-level defenses. Bladed and piercing attacks that are not capable of cutting through steel do blunt damage for the technique's duration, and all blunt damage suffered while this technique is active is significantly reduced. The user of this technique may not move while it is active. (Mid Tier Material, Full Body)") },
			{ RokuName.GEPPO, new RokuInfo("Geppo", 36, "Support", "Self", 0, 0, 0, 0, 0,
				"By kicking off of the air itself, the user of this technique can seemingly fly or \"Air Walk.\" The user of this technique may remain airborn indefinitely, but must land in the following post if they attack or are attacked (if there is no land within range, they spend the next post falling until they can resume use of Geppo). If the technique's user is significantly encumbered, or if they lack use of one of their legs, they must land periodically, being able to sustain flight with this technique for no more than three consecutive rounds. (Personal 'Flight')") },
			{ RokuName.ROKUOGAN, new RokuInfo("Rokuogan", 44, "Offensive", "Melee", 20, 0, 0, -22, -22,
				"Using principles of all six Rokushiki techniques, this technique executes a devastating close-range attack. Though it must be used on a target no more than a yard from the user, this technique creates a shockwave that bypasses almost any and all forms of physical defense, doing internal damage to it's victim as if they were unarmoured and made of ordinary flesh and blood (though it does not bypass some devil fruit abilities on it's own). Additionally, the shock of this attack is particularly incapacitating, weakening the target's constitution significantly. (Inbuilt Critical Hit, Defense Bypassing)") },
			{ RokuName.NONE, new RokuInfo("None", 0, "None", "None", 0, 0, 0, 0, 0, "None") }
		};

		public RokuName Get_RokuEnum(string Name) {
			try {
				return Roku_Enum[Name];
			}
			catch {
				return RokuName.NONE;
			}
		}

		// Returns the Roku Information
		public RokuInfo Get_RokuInfo(RokuName Enum) {
			try {
				return Roku_Dict[Enum];
			}
			catch {
				return Roku_Dict[RokuName.NONE];
			}
		}
	}
}
