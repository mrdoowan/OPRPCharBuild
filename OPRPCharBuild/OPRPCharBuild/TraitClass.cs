using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This will only be used to load Traits for the menu. 
// TraitName will be mainly used for outside functions

namespace OPRPCharBuild
{
	public class TraitClass
	{
        // -------------------------------------------------------------------
        // MEMBER VARIABLES
        // -------------------------------------------------------------------

        private string name;
        private TraitName enumName;
		private int genNum;
		private int profNum;
		private string desc;

		// Map the string to its corresponding ID
		static private Dictionary<string, TraitName> traitDict = new Dictionary<string, TraitName>() {
			#region Const Dictionary of Traits to its ID.
			{"\'Tis But A Scratch!", TraitName.TIS_BUT_SCRATCH},
			{"[SPEC] Mastery", TraitName.MARTIAL_MASTERY},
			{"Advanced [SPEC] Class Mastery", TraitName.ADV_MARTIAL_CLASS},
			{"Advanced [SPEC] Mastery", TraitName.ADV_MARTIAL_MASTERY},
			{"Advanced Cyborg", TraitName.ADV_CYBORG},
			{"Advanced Stance Mastery", TraitName.ADV_STANCE_MASTERY},
			{"Anatomical Strike", TraitName.ANAT_STRIKE},
			{"Ansatsuken", TraitName.ANSATSUKEN},
			{"Anti-Stealth", TraitName.ANTI_STEALTH},
			{"Anti-Weapon Specialist", TraitName.ANTI_WEAPON},
			{"Art of Stealth", TraitName.ART_OF_STEALTH},
			{"Awakened Haki", TraitName.AWAKE_HAKI},
			{"Baking Bad", TraitName.BAKING_BAD},
			{"Basic Cyborg", TraitName.BAS_CYBORG},
			{"Bear Stamina", TraitName.BEAR_STAM},
			{"Beast Handler", TraitName.BEAST_HANDLER},
			{"Born Leader", TraitName.BORN_LEADER},
			{"Brilliant Mind", TraitName.BRILL_MIND},
			{"Civilian Lawyer", TraitName.CIV_LAW},
			{"Combat Cuisine", TraitName.COMBAT_CUIS},
			{"Conquering King Haki", TraitName.CONQ_HAKI},
			{"Cooking Fighter", TraitName.COOK_FIGHT},
			{"Critical Hit", TraitName.CRIT_HIT},
			{"Crowd Control", TraitName.CROWD_CONT},
			{"Cybertronic Genius", TraitName.CYBER_GEN},
			{"Dazzling Performer [SPEC]", TraitName.DAZZLE_PERF},
			{"Disciplined Haki", TraitName.DISC_HAKI},
			{"Dwarf", TraitName.DWARF},
			{"Escape Artist", TraitName.ESC_ART},
			{"Fate of the Cunning", TraitName.FATE_CUN},
			{"Fate of the Emperor", TraitName.FATE_EMP},
			{"Fate of the Mighty", TraitName.FATE_MIGHT},
			{"Fate of the Strong", TraitName.FATE_STR},
			{"Fate of the Swift", TraitName.FATE_SWIFT},
			{"Fate of the Whimsical", TraitName.FATE_WHIM},
			{"Fishman [SPEC]", TraitName.FISHMAN},
			{"Flawless Accuracy", TraitName.FLAW_ACC},
			{"Food of Warriors", TraitName.FOOD_WAR},
			{"Forge Furnace", TraitName.FORG_FURN},
			{"Forging Master", TraitName.FORG_MAST},
			{"Form and Function", TraitName.F_AND_F},
			{"From the Shadows", TraitName.FROM_SHAD},
			{"Funky Body", TraitName.FUNKY_BODY},
			{"Giant Stamina", TraitName.GIANT_STAM},
			{"Grandmaster [SPEC]", TraitName.GRAND_MARTIAL},
			{"Great Speed", TraitName.GREAT_SPE},
			{"Haggler of the Sea", TraitName.HAG_SEA},
			{"Hammer of Rage", TraitName.HAMM_RAGE},
			{"Hardened Fighter", TraitName.HARD_FIGHT},
			{"Horticultural Warfare", TraitName.HORT_WAR},
			{"In-Training", TraitName.IN_TRAIN},
			{"Jack of All Trades", TraitName.JACK_TRADE},
			{"John of All Trades", TraitName.JOHN_TRADE},
			{"Keen Accuracy", TraitName.KEEN_ACC},
			{"Lamp, Oil, Rope", TraitName.L_O_R},
			{"Life Return", TraitName.LIFE_RET},
			{"Ludicrous Speed", TraitName.LUD_SPE},
			{"Mad Scientist", TraitName.MAD_SCI},
			{"Mammoth Stamina", TraitName.MAM_STAM},
			{"Master of Misdirection", TraitName.MAST_MISDI},
			{"Medical Malpractice", TraitName.MED_MAL},
			{"Mentally Fortified", TraitName.MENT_FORT},
			{"Merfolk", TraitName.MERFOLK},
			{"Mighty Strength", TraitName.MIGHT_STR},
			{"Monster Strength", TraitName.MON_STR},
			{"Natural Armour", TraitName.NAT_ARM},
			{"New World Cyborg", TraitName.NW_CYBORG},
			{"Performance Assistant", TraitName.PERF_ASS},
			{"Pickpocket", TraitName.PICKP},
			{"Poison Killer", TraitName.POIS_KILL},
			{"Powerful Speaker", TraitName.POW_SPEAK},
			{"Prop Performance", TraitName.PROP_PERF},
			{"Quickstrike", TraitName.QUICKSTRIKE},
			{"Rare Find", TraitName.RARE_FIND},
			{"Rokushiki Master", TraitName.ROK_MAST},
			{"Rokushiki Savant", TraitName.ROK_SAV},
			{"Siege Warfare", TraitName.SIEGE_WAR},
			{"Signature Technique", TraitName.SIG_TECH},
			{"Skill in Upgrades", TraitName.SKILL_UP},
			{"Skilled Use of Medicines", TraitName.SKILL_MED},
			{"Sleight of Hand", TraitName.SLEIGHT},
			{"Sonic Speed", TraitName.SON_SPE},
			{"Specific Devil Fruit", TraitName.DEV_FRUIT},
			{"Stance Mastery", TraitName.STANCE_MAST},
			{"Strong Spirit", TraitName.STR_SPIRIT},
			{"Super Sense", TraitName.SUP_SENSE},
			{"Super Strength", TraitName.SUP_STR},
			{"Superb Accuracy", TraitName.SUP_ACC},
			{"Technical Mastery", TraitName.TECH_MAST},
			{"Technically Adept", TraitName.TECH_ADEPT},
			{"That Extra Special Ingredient", TraitName.EXTRA_INGRED},
			{"Top Gun", TraitName.TOP_GUN},
			{"Tough as Nails", TraitName.TOUGH_NAIL},
			{"Tough Bargainer", TraitName.TOUGH_BARG},
			{"Training of the Kitchen", TraitName.TRAIN_KIT},
			{"Treasure Hunter", TraitName.TREAS_HUNT},
			{"Uncivil Engineering", TraitName.UNCIV_ENG},
			{"Weak Point Sighted", TraitName.WEAK_SIGHT},
			{"Weathermancy", TraitName.WEATHER}
			#endregion
		};
        
        // Constructor
        public TraitClass() { }

        // -------------------------------------------------------------------
        // Constructor & Functions
        // -------------------------------------------------------------------
        
		// Based on name of the Trait, we initialize everything for the box.
		// This is gonna be HUGE (99 traits), so fair warning here.
		public TraitName loadTrait(string name_) {
			#region Huge databank of traits from the site
			switch (name) {
				case "\'Tis But A Scratch!":
					genNum = 0;
					profNum = 1;
					desc = "Weapon specialists are often used to fighting against other weapon specialists in close combat. Thus their chances of being injured by such weapons increase drastically compared to others. These particular characters are used to fighting so much under those conditions that they can actually ignore the effects of pain much better than their sissy counterparts and fight on.";
					break;
				case "[SPEC] Mastery":
					genNum = 0;
					profNum = 1;
					desc = "The character has attained mastery in one specific weapon or form of attack that they have Martial Proficiency (can make techniques greater than rank 14) with. All techniques using this weapon or form of attack are always treated as being four ranks higher for purposes of calculating their technique rank benchmark, though their actual rank is not changed. This benefit ceases to apply for techniques higher than rank 28.";
					break;
				case "Advanced [SPEC] Class Mastery":
					genNum = 0;
					profNum = 1;
					desc = "The character has expanded their mastery from a single weapon or form of attack to encompass a broader selection. They now receive the benefit of their ____ Mastery or Advanced ____ Mastery to a second type of Weapon or form of attack. Additionally, they gain special technique points which may be used to create attacks that use both weapons or forms of attack at once, in the amount of 25% of their fortune.";
					break;
				case "Advanced [SPEC] Mastery":
					genNum = 0;
					profNum = 2;
					desc = "The character has attained supreme mastery in one specific weapon or form of attack that they have Martial Proficiency (can make techniques greater than rank 14) with. All techniques using this weapon or form of attack are always treated as being four ranks higher for purposes of calculating their technique rank benchmark, though their actual rank is not changed.";
					break;
				case "Advanced Cyborg":
					genNum = 3;
					profNum = 0;
					desc = "This is a high-tech cyborg, rarely encountered anywhere but the Grand Line. A character must be within the Grand Line to access this kind of technology. Advanced Cyborgs begin with up to 12M Beli worth of modifications. They are made of steel and may be constructed of materials up to titanium in strength, and may have additional body parts added. Their size may vary from their original form at a cost. Each arm may conceal two weapons and each leg may conceal one. Additionally, they may externally mount cybernetics/weapons at this stage. Cyborgs of this type may have multiple special systems, though moderators may enforce a limit eventually. Advanced Cyborgs may also purchase additional fuel charges up to a limit of 5. Upgrades from Basic Cyborg modifications are maintained and do not count against Advanced maximums.\n" + 
						"[list][*]Appendages - 4.5M Beli/per\n" + 
						"[*]Additional Body Parts – 6M Beli/per\n" + 
						"[*]Torso - 9M Beli\n" + 
						"[*]Titanium upgrade at 60% of the sum of cybernetic body parts\n" + 
						"[*]Additional Weapon Slots - 2M Beli/per\n" + 
						"[*]Additional Fuel Charges - 10M Beli/per[/list]";
					break;
				case "Advanced Stance Mastery":
					genNum = 0;
					profNum = 1;
					desc = "Through intense practice, the character has perfected their stance to a much greater degree. The character's stance techniques now only require a penalty to other stats equal to 50% of the total buff (fractional values rounded up for the penalty).";
					break;
				case "Anatomical Strike":
					genNum = 0;
					profNum = 1;
					desc = "By using their extensive knowledge of the human anatomy, these doctors can aim specifically for areas that will cause very serious, even fatal wounds. The character can use up to a quarter of their Technique Points in techniques which hit vital points in the body. Striking vital points allows characters to do damage and cause a stat debuff related to the area struck.";
					break;
				case "Ansatsuken":
					genNum = 0;
					profNum = 1;
					desc = "The character has become so adept at striking vital areas and pressure points that they may do so passively in the course of their normal attacks. Any time the character lands a blow on an enemy, they may apply the one of their critical hit/quick strike technique debuffs. This debuff is half as strong as normal, but otherwise functions exactly like a normal critical hit/quick strike debuff, in terms of stacking and duration.";
					break;
				case "Anti-Stealth":
					genNum = 0;
					profNum = 1;
					desc = "Having become masters of stealth themselves, these thieves/assassins are capable of knowing when their opponents are using the very same tactics they specialise in. The detection techniques of these characters are treated as if they are four ranks higher for purposes of effectiveness. ";
					break;
				case "Anti-Weapon Specialist":
					genNum = 0;
					profNum = 1;
					desc = "Through training and experience in fighting armed opponents when unarmed, the character gains special Technique Points, of the amount of half their fortune, for techniques which are aimed to disarm, disable or block the use of a weapon.";
					break;
				case "Art of Stealth":
					genNum = 0;
					profNum = 1;
					desc = "After a lifetime of learning how to survey an area's or targets' weak points, this assassin has a particularly effective set of skills for the purposes of stealth. All stealth techniques are treated as if they are four ranks higher for purposes of effectiveness.";
					break;
				case "Awakened Haki":
					genNum = 4;
					profNum = 0;
					desc = "The character has awakened their dormant ambition and is able to utilise this mysterious force in combat. They may specialise in one of the two basic colors (The Color of Armaments or The Color of Observation) or a Dormant Specialization. For their specialist color, the character's Haki maximum rank is 1/2 Fortune. For any other color, the maximum rank is 1/4 Fortune. They are also capped at rank 44 for their Specialist color techniques, and rank 28 for any other colors. ";
					break;
				case "Baking Bad":
					genNum = 0;
					profNum = 1;
					desc = "Experienced both in the ways of science and the cook, the character has taken their skills to the next level by combining them. They may now make chef techniques that can be used in combat to apply debuffs to their enemies simply by contacting them, much like a poison (these work exactly like Chef buffs, but have a duration of 1 post per 4 ranks). They may also make kitchen-related gadgets.";
					break;
				case "Basic Cyborg":
					genNum = 1;
					profNum = 0;
					desc = "This is the most basic level of cyborg, and the most commonly encountered type in the blues. A Basic Cyborg begins with up to 3M Beli worth of modifications. Their bodies are made out of Iron and do not vary from their original size. They may have one concealed weapon per arm or leg, and one basic system per other appendage. Note that these weapons or systems are very basic.\n" + 
						"[list][*]Appendages - 1.5M Beli/per\n" + 
						"[*]Torso - 3M Beli[/list]";
					break;
				case "Bear Stamina":
					genNum = 1;
					profNum = 0;
					desc = "This character's stamina is boosted by 20%.";
					break;
				case "Beast Handler":
					genNum = 1;
					profNum = 0;
					desc = "Characters with this trait have a natural affinity for getting along with animals. Wild animals tend to behave either neutrally or friendly towards the character, unless they are very strongly aggressive. These animals, with proper approach and care, may be made to be merely distrustful and may sometimes refrain from attacking. The user also gains one NPC pet, who may possess humanlike intelligence, behaves as a crew NPC and is controlled by the player of the main character.";
					break;
				case "Born Leader":
					genNum = 1;
					profNum = 0;
					desc = "Characters with this trait are gifted with the ability to command respect and admiration from others. Sentient NPCs are easier to sway over to the side of those with this trait. They may gain information, calm down the angry, and sometimes even placate hostile adversaries. Additionally, those normally friendly to strangers find themselves easily enamoured with the character. The user also gains one NPC henchman, who behaves as a crew NPC and is controlled by the player of the main character. They are exceedingly loyal to their ‘master’.";
					break;
				case "Brilliant Mind":
					genNum = 0;
					profNum = 1;
					desc = "Creative and with an incredible knack for making all sorts of strange gadgets, these inventors gain special Technique Points which can only be used on creation techniques for weapons and gadgets, of the amount of half their fortune.";
					break;
				case "Civilian Lawyer":
					genNum = 0;
					profNum = 1;
					desc = "These silver tongued devils have managed to work their ability to talk people into making the best financial deals for themselves into a practical ability. They are able to more easily influence those around them with their words.";
					break;
				case "Combat Cuisine":
					genNum = 0;
					profNum = 1;
					desc = "Learning to adapt his food for rapid service on the battlefield, the Chef is able to increase the beneficial effects gained by those who eat it, albeit temporarily. A chef may use any cooking technique during combat, as if it were a Doctor's medicine, doubling the total value of the stat bonus for the duration. All other chef buffs on the target are over-written while this is active, but resume when it expires.";
					break;
				case "Conquering King Haki":
					genNum = 1;
					profNum = 0;
					desc = "Those who chose to have a Dormant Specialization are able to take Conquerering King's Haki at 200 SD earned. This will allow them to overpower weaker enemies and combat their foes with sheer force of will. They will specialize in the Color of the Conqueror.";
					break;
				case "Cooking Fighter":
					genNum = 0;
					profNum = 1;
					desc = "Experts in the use of various kitchen utensils, a chef may become proficient enough in the use of their cookware as weapons in battle. A chef with this trait gains martial proficiency with one form of cookware, as well as special technique points equal to one quarter of their fortune that can be used on martial techniques involving the chosen utensil.";
					break;
				case "Critical Hit":
					genNum = 0;
					profNum = 1;
					desc = "Through training in how to hit vital points in the body, the character can use up to a quarter of their Technique Points in techniques which hit vital points in the body. Striking vital points allows characters to do damage and cause a stat debuff related to the area struck.";
					break;
				case "Crowd Control":
					genNum = 0;
					profNum = 1;
					desc = "Due to the nature of their skills, their performances will affect basically anyone who can see and hear them. Buff techniques used by these entertainers will have the same maximum effect as if they were affecting a group of targets one crowd tier smaller than the size of the crowd the technique was intended to effect.";
					break;
				case "Cybertronic Genius":
					genNum = 0;
					profNum = 1;
					desc = "These inventors have studied their cybernetics especially well, and pay 20% less to upgrade their own Cyborg parts.";
					break;
				case "Dazzling Performer [SPEC]":
					genNum = 0;
					profNum = 1;
					desc = "These entertainers are so skilled in their chosen field that they can use their performance skills to actually bring about changes in others. These entertainers gain special Technique Points that can only be used on their performance support techniques, of the amount of half their Fortune.";
					break;
				case "Disciplined Haki":
					genNum = 2;
					profNum = 0;
					desc = "The character has trained and disciplined their ambition, allowing them to unlock their true potential. They are no longer limited by Rank caps, and their non-specialist maximum rank is 1/3 Fortune.";
					break;
				case "Dwarf":
					genNum = 2;
					profNum = 0;
					desc = "Dwarves are extremely small humanoids with fluffy tails. Dwarves gain the benefits of the Mighty Strength trait and have a mastery benefit on all their movement techniques of +4 ranks. Their small sizes makes them harder to hit but they also take more damage when hit. They tend to be extremely gullible.";
					break;
				case "Escape Artist":
					genNum = 0;
					profNum = 1;
					desc = "Being a wily little bugger, the thief is a master of escaping from tight spots and difficult situations. Any debuff to the thief's speed is halved in duration, and they can easily escape most forms of restraint, such as being grappled, shackled, tied, etc.";
					break;
				case "Fate of the Cunning":
					genNum = 3;
					profNum = 0;
					desc = "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your accuracy stat.";
					break;
				case "Fate of the Emperor":
					genNum = 3;
					profNum = 0;
					desc = "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated) to your fortune score.";
					break;
				case "Fate of the Mighty":
					genNum = 3;
					profNum = 0;
					desc = "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your stamina stat.";
					break;
				case "Fate of the Strong":
					genNum = 3;
					profNum = 0;
					desc = "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your strength stat.";
					break;
				case "Fate of the Swift":
					genNum = 3;
					profNum = 0;
					desc = "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your speed stat.";
					break;
				case "Fate of the Whimsical":
					genNum = 3;
					profNum = 0;
					desc = "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. Unlike the other Fate traits, these place-holder traits may be cashed-in and replaced with other traits without having to reach the next trait mark.";
					break;
				case "Fishman [SPEC]":
					genNum = 2;
					profNum = 0;
					desc = "Fishmen gain the benefits of the Mighty Strength trait, and may breath underwater. Their movements in water are not hampered as greatly as normal humans, and they may swim at great speeds. They also gain attributes or natural weapons based on their species. The number of traits needed depends on the species.*";
					break;
				case "Flawless Accuracy":
					genNum = 3;
					profNum = 0;
					desc = "This character's accuracy is boosted by 60%.";
					break;
				case "Food of Warriors":
					genNum = 0;
					profNum = 1;
					desc = "Years and years of experience in preparing the finest of meals, using all the right ingredients has led to these chefs being able to create foods which will make those who eat it stronger, faster, and better. They gain special Technique Points that can only be used on this food, of the amount of half their fortune.";
					break;
				case "Forge Furnace":
					genNum = 0;
					profNum = 1;
					desc = "After spending that much time welding weapons and having to put up with the extreme temperatures of the furnace, the character gains immunity to heat-based attacks similar to having an iron body. Any heat-based attack that wouldn’t damage iron won’t harm them.";
					break;
				case "Forging Master":
					genNum = 0;
					profNum = 1;
					desc = "All the experience the character has gained in making weapons allows them to produce them more cheaply. These smiths get a 20% discount for any weapons and armor they make for their own personal use.";
					break;
				case "Form and Function":
					genNum = 1;
					profNum = 0;
					desc = "This character has achieved a mastery over every minute aspect of their body in all of it's possible forms. Any life return buff used in conjunction with a zoan form has all of it's penalties halved.";
					break;
				case "From the Shadows":
					genNum = 0;
					profNum = 1;
					desc = "Unlike others who have merely adopted the dark, the thief/assassin was born in it, molded by it. The thief/assassin gains special technique points equal to half their fortune that can be used to create Camouflage techniques, or any technique hides or otherwise obscures the presence of the character.";
					break;
				case "Funky Body":
					genNum = 1;
					profNum = 0;
					desc = "Whether gained by birth, training, modification by themselves or another person, prolonged exposure to something, or plain dumb luck, this character has a body that is less than typical. They have a single biological or physical difference to their bodies, canon examples including Kuramarimo's ability to rapidly grow and detach static afros and Don Chinjao's abnormally hard and pointy skull.";
					break;
				case "Giant Stamina":
					genNum = 3;
					profNum = 0;
					desc = "This character's stamina is boosted by 60%.";
					break;
				case "Grandmaster [SPEC]":
					genNum = 0;
					profNum = 2;
					desc = "This character has achieved a legendary skill with his weapon/weapon class/form of attack. They are masters of their art beyond question, and are able to perform supernatural feats using their skill alone. Grandmaster ____s are able to access techniques that allow them to cut/hit fluid and insubstantial substances, such as air, fire, water, or even light.";
					break;
				case "Great Speed":
					genNum = 1;
					profNum = 0;
					desc = "This character's speed is boosted by 20%.";
					break;
				case "Haggler of the Sea":
					genNum = 0;
					profNum = 1;
					desc = "There are financially crafty people out there and these carpenters are at the top of the game when it comes to the ship market. These carpenters are capable of negotiating the cost of 4 ranks off of any ship technique they may make for their ships. (Ship Techniques will be special technologies that a ship utilizes that can be quantified as a technique; a sudden boost in speed, a weapon such as the Gaon cannon.)";
					break;
				case "Hammer of Rage":
					genNum = 0;
					profNum = 1;
					desc = "Knowing how to make things also means knowing how to break them. Some smiths come to revel in this destructive aspect of their trade even more than usual, becoming masters in breaking down anything with a form. The character is able to freeform breaking objects of up to iron hardness, and can breaker harder substances as if they were one tier lower in terms of technique rank. They remain unable to destroy unbreakable quality materials, but may temporarily damage them (disabled for the duration of that SL) instead of breaking them.";
					break;
				case "Hardened Fighter":
					genNum = 0;
					profNum = 1;
					desc = "Through excessive training in breaking planks, punching rocks, and all those durability-building exercises, the character’s arms, up to their elbows, and their legs, up to their knees, have gained flesh that is as hard as iron, making them much more resistant to physical damage.";
					break;
				case "Horticultural Warfare":
					genNum = 3;
					profNum = 0;
					desc = "The character has become accustomed to using pop green seeds and as such is now able to utilise these seeds to bloom various flowers nearly instantly. The character can use these plants to make anything varying from a plant net to a large man-eating flower.";
					break;
				case "In-Training":
					genNum = 0;
					profNum = 1;
					desc = "This is the equivalent of Fated for the character’s Professional Skills, but no paths exist for this. General traits may not be spent to purchase In-Training.";
					break;
				case "Jack of All Trades":
					genNum = 0;
					profNum = 1;
					desc = "The character is able to master multiple skills, and has become adept at a number of things. This character gains two additional secondary professions.";
					break;
				case "John of All Trades":
					genNum = 0;
					profNum = 2;
					desc = "The character is able to master multiple skills, and has become adept at a number of things. This character gains three additional secondary professions";
					break;
				case "Keen Accuracy":
					genNum = 1;
					profNum = 0;
					desc = "This character's accuracy is boosted by 20%.";
					break;
				case "Lamp, Oil, Rope":
					genNum = 0;
					profNum = 1;
					desc = "Certain minds have a better handle on breaking things down than building them up. These inventors specialize in demolitions, and gain special technique points equal to a half of their fortune that may be spent on creation techniques for bombs and munitions.";
					break;
				case "Life Return":
					genNum = 0;
					profNum = 2;
					desc = "The character has the ability to use the mystic ability known as Life Return. They gain control over their bodily functions through their thoughts. Growing and controlling their hair, changing their body fat, body muscles and at higher levels even their bloodflow, these characters are truly masters of their own body.";
					break;
				case "Ludicrous Speed":
					genNum = 3;
					profNum = 0;
					desc = "This character's speed is boosted by 60%.";
					break;
				case "Mad Scientist":
					genNum = 0;
					profNum = 1;
					desc = "The epitome of madness. Cackling and gleeful hand-rubbing is a favourite pastime of this character, and every time they finish an experiment, there are obligatory flashes of lightning. The mad scientist has the ability to make one major biological modification to themselves or any NPCs they control.";
					break;
				case "Mammoth Stamina":
					genNum = 2;
					profNum = 0;
					desc = "This character's stamina is boosted by 40%.";
					break;
				case "Master of Misdirection":
					genNum = 0;
					profNum = 1;
					desc = "Those who practice stealth to depths far greater than most, becoming far more proficient than even others of their trade. These characters may treat any environment as Natural Camouflage, and the Silent effect becomes a General Effect.";
					break;
				case "Medical Malpractice":
					genNum = 0;
					profNum = 1;
					desc = "Part of knowing what is required to keep somebody healthy is knowing what will cause others to become unhealthy. Doctors who put this knowledge to practical use may make especially potent poisons. They gain special Technique Points which can only be used on toxins that harm or debuff, of the amount of half their fortune. ";
					break;
				case "Mentally Fortified":
					genNum = 0;
					profNum = 1;
					desc = "After Mastering hiding from the 5 Common Senses, the Thief/Assassin expands their Stealth skill to apply to the 6th sense. These characters gain access to making Anti-Observation Haki Techs, allowing them to conceal themselves from even this supernatural method of detection. These Techniques only work against the Aura Detection of Observation Haki and do not naturally apply to any other senses.";
					break;
				case "Merfolk":
					genNum = 1;
					profNum = 0;
					desc = "In place of legs these characters have a tail which splits down the middle while they are standing on land (For this RP's purposes, this is the case at any age, not just 30+). They may breathe underwater and gain the Great Speed trait whilst submerged, and their movements underwater are completely unhindered. Spending too long out of water can fatigue them somewhat. In addition, merfolk can talk to most types of fish, but not seakings.";
					break;
				case "Mighty Strength":
					genNum = 1;
					profNum = 0;
					desc = "This character's strength is boosted by 20%.";
					break;
				case "Monster Strength":
					genNum = 3;
					profNum = 0;
					desc = "This character's strength is boosted by 60%.";
					break;
				case "Natural Armour":
					genNum = 0;
					profNum = 1;
					desc = "Through all the time spent in the forge, working on weapons, the character’s arms, up to their elbows, and their legs, up to their knees, have gained flesh that is literally as tough as iron, making them much more resistant to physical damage.";
					break;
				case "New World Cyborg":
					genNum = 5;
					profNum = 0;
					desc = "These cyborgs represent the very pinnacle of scientific achievement in the world of cybernetics, being by right more machine than human. A character must be in either the Grand Line or New World to access this kind of technology. They can be made of materials up to titanium in strength but may have built-in armor made from custom-materials. Alongside this, these cyborgs are given up to 21M Beli worth of modifications. New World Cyborgs pay 30% to upgrade weaker materials. These Cyborgs are able to create body systems that provide personal flight, self-repair, fire energy projectiles and more.\n" +
						"[list][*]Appendages - 4.5M Beli/per\n" + 
						"[*]Additional Body Parts – 6M Beli/per\n" + 
						"[*]Torso - 9M Beli\n" + 
						"[*]Additional Armour - 3M beli/per limb + material cost\n" + 
						"[*]Additional Weapon Slots - 2M Beli/per\n" + 
						"[*]Additional Fuel Charges - 10M Beli/per\n" + 
						"[*]Laser techs start at rank 44. Rank 44 lasers travel extremely quickly, however they have a smaller blast radius. The base technique's range is long, it has built-in iron piercing and has a [Short] blast radius as well.\n" + 
						"[*]Self-Repair techs start at rank 14. These techniques are similar in strength to a Doctor’s Medicine however they only work on themselves. The other factor that goes in is that during the self-repair the Cybernetic systems cannot attack, however Cyborgs are able to run on the move. The repair takes one post.\n" + 
						"[*]Flight techniques start at rank 28. At this rank, it’s more of a Cybernetic-fueled Geppou. However, at rank 44 it is possible to fly for a constant AE cost.\n" + 
						"[*]Other advanced systems may be implemented into their bodies as well, so do not feel constricted to just these three systems. These will be funded by technique points.[/list]\n" + 
						"The special thing about all of these systems is that they are able to be input into the cyborg’s internal systems. An Inventor with access to R44 techniques can still build a laser, however they must buy a holder for the laser that will cost them money.";
					break;
				case "Performance Assistant":
					genNum = 0;
					profNum = 1;
					desc = "Plenty of good entertainers need someone to help them carry out their routine. The character gains a special Performance Assistant NPC which is under their control.";
					break;
				case "Pickpocket":
					genNum = 0;
					profNum = 1;
					desc = "As a thief, the character is skilled at obtaining money by less-than-acceptable means. Thus, they will always earn 20% more Beli in their Storylines.";
					break;
				case "Poison Killer":
					genNum = 0;
					profNum = 1;
					desc = "With some basic training in the use of poisons to kill or debuff their targets, these assassins gain special Technique Points that can only be used on poisons, of the amount of half their fortune.";
					break;
				case "Powerful Speaker":
					genNum = 0;
					profNum = 1;
					desc = "This entertainer is particularly skilled at inspiring the masses. These entertainers are capable of, with a simple phrase, increasing the fighting abilities of their allies. These buffs last much shorter than normal but consume no AE and only need to be said once for it to come into effect, as opposed to a sustained 'performance'. They utilize Personal Buff measurements (see Techniques page) in terms of buff potency and have a duration rank/4 posts. This impromptu buff takes up a Personal Buff slot. ";
					break;
				case "Prop Performance":
					genNum = 0;
					profNum = 1;
					desc = "An entertainer with this trait has learned how to integrate one of their performing props into their fighting style. They gain the ability to make martial techniques involving one item above rank 14. Additionally, they may perform freeform attacks with this item while performing at no additional expense of action economy.";
					break;
				case "Quickstrike":
					genNum = 0;
					profNum = 1;
					desc = "Because of their training in making stealth attacks and hitting vital organs, the character can use up to a quarter of their Technique Points in techniques which hit vital points in the body. Striking vital points allows characters to do damage and cause a stat debuff related to the area struck.";
					break;
				case "Rare Find":
					genNum = 0;
					profNum = 1;
					desc = "A keen merchant always has his eyes and ears open for an exceptional deal. At the end of every SL, the character will be offered a rare and valuable item determined at random (weapons, armour, devices, dials, materials, etc.) This item will have double their normal discount applied if it is purchased.";
					break;
                case "Rokushiki Master":
					genNum = 1;
					profNum = 0;
					desc = "The character has completely mastered Rokushiki. Through vigorous training of pushing their body to the utmost limit, they are now able to make their own custom variations on any Rokushiki Technique. Additionally they gain access to the most powerful Rokushiki technique of all, Rokuogan. ";
					break;
				case "Rokushiki Savant":
					genNum = 1;
					profNum = 0;
					desc = "The character has a natural talent for Rokushiki techniques and as such, gains Special Technique Points equal to half their fortune to spend on their Rokushiki techniques.";
					break;
				case "Siege Warfare":
					genNum = 0;
					profNum = 1;
					desc = "Carpenters after years of dedication to their craft have become masters of designing impressive war machines. They can create such beasts of war as catapults, battering rams, and various other tools of war quickly in the middle of a fight even. They gain Special Technique Points equal to half their Fortune for this purpose.";
					break;
				case "Signature Technique":
					genNum = 1;
					profNum = 0;
					desc = "This trait makes one particular technique special, a trademark move of the character. This signature technique is always of the natural maximum rank that the character is allowed, but does not cost any Technique Points. In addition, these techniques are allowed to have weaker variations, though these variations must follow the same path set by the main technique, and cannot be used to branch to techniques that do not branch into the main technique.";
					break;
				case "Skill in Upgrades":
					genNum = 0;
					profNum = 1;
					desc = "These fellows are amazing at what they do, such that they can do it for a much lower cost. All technological upgrades that they make to weapons get a 20% discount. This discount applies only to the inventor themselves, and not weapons made for others.";
					break;
				case "Skilled Use of Medicines":
					genNum = 0;
					profNum = 1;
					desc = "Because of their expertise, these doctors have become extremely adept at producing their own medicines to aid others. They gain special Technique Points which can only be used on medicines that aid or buff, of the amount of half their fortune. ";
					break;
				case "Sleight of Hand":
					genNum = 0;
					profNum = 1;
					desc = "\"Always watch the hands\" is the rule when dealing with a professional thief such as this. These thieves are so good at stealing that they gain special TP equal to half their fortune for techniques that involve stealing the items and weaponry off their target even during combat.";
					break;
				case "Sonic Speed":
					genNum = 2;
					profNum = 0;
					desc = "This character's speed is boosted by 40%.";
					break;
				case "Specific Devil Fruit":
					genNum = 3;
					profNum = 0;
					desc = "The player may choose a specific Devil Fruit, either canon or custom, for their character. These fruits are limited based on moderator discretion but can be of any of the three types.";
					break;
				case "Stance Mastery":
					genNum = 0;
					profNum = 1;
					desc = "The character has mastered the use their stance techniques. All stances the character are always treated as being four ranks higher for purposes of calculating their technique rank benchmark and numerical benefit, though their actual rank is not changed.";
					break;
				case "Strong Spirit":
					genNum = 1;
					profNum = 0;
					desc = "The character gains an amount of special technique points equal to 1/2 of their fortune that may only be used on Haki techniques of the colour they are specialized in.";
					break;
				case "Super Sense":
					genNum = 1;
					profNum = 0;
					desc = "This character has achieved mastery over their heightened senses, gaining a single sense on par with Observation Haki. They gain special Technique Points that can only be used on this sense, of the amount of a quarter of their fortune.";
					break;
				case "Super Strength":
					genNum = 2;
					profNum = 0;
					desc = "This character's strength is boosted by 40%.";
					break;
				case "Superb Accuracy":
					genNum = 2;
					profNum = 0;
					desc = "This character's accuracy is boosted by 40%.";
					break;
				case "Technical Mastery":
					genNum = 1;
					profNum = 0;
					desc = "Increases total technique points by 100% of your fortune score, but does not stack with Technically Adept (this benefit overrides the previous benefit). Requires Technically Adept and 100 SD earned.";
					break;
				case "Technically Adept":
					genNum = 1;
					profNum = 0;
					desc = "Increases total technique points by 40% of your fortune score.";
					break;
				case "That Extra Special Ingredient":
					genNum = 0;
					profNum = 1;
					desc = "This is a chef who has traveled around the world searching for only the finest of ingredients to add their their foods. These ingredients are capable of giving profound supernatural effects to the consumer (for example, jalapenos that bestow fire breath).";
					break;
				case "Top Gun":
					genNum = 0;
					profNum = 1;
					desc = "These marksmen are top of the line when it comes to being able to draw their weapon and fire. These characters are also always reloading their gun reflexively faster than those around them notice thus they never have to actually deal with taking the time to reload at a critical moment in battle.";
					break;
				case "Tough as Nails":
					genNum = 0;
					profNum = 1;
					desc = "Through all the manual labour that they have done, the character’s arms, up to their elbows, and their legs, up to their knees, have gained flesh that is literally as tough as iron, making them much more resistant to physical damage.";
					break;
				case "Tough Bargainer":
					genNum = 0;
					profNum = 1;
					desc = "Ever the successful businessman, merchants know how to extract money from people and to keep as much as they can from leaving their pocket. Thus, they will always earn 20% more Beli in their Storylines.";
					break;
				case "Training of the Kitchen":
					genNum = 0;
					profNum = 1;
					desc = "After spending so long in front of the open fires in the kitchen, the character gains immunity to heat-based attacks similar to having an iron body. Any heat-based attack that wouldn’t damage iron won’t harm them.";
					break;
				case "Treasure Hunter":
					genNum = 0;
					profNum = 1;
					desc = "With their knack for discovering hidden items that don’t necessarily belong to them, these thieves are very skilled in looking for treasures. Thus, in their SLs, they will always get an extra little item. It could be something quirky, or it could be something rare. ";
					break;
				case "Uncivil Engineering":
					genNum = 0;
					profNum = 1;
					desc = "Characters with this trait are undisputed masters of rapidly fabricating objects. They can hold Special Technique Points equal to half their Fortune for this specific purpose. ";
					break;
				case "Weak Point Sighted":
					genNum = 0;
					profNum = 1;
					desc = "With all their experience in building things and taking them apart, these characters are able to pinpoint the weaknesses in architecture and can easily figure out how to do the most damage to weapons, ships, buildings and other large constructions.";
					break;
				case "Weathermancy":
					genNum = 3;
					profNum = 0;
					desc = "After the learning of skills of weather manipulation and creation, the character may - through the use of tools and gadgets - actively perform feats such as summoning lightning bolts, hail, rain, etc. They require an weapon/item to serve as a conduit for such techniques.";
					break;
				default:
					// This assumes Custom
					break;
			}
            #endregion
            name = name_;
            enumName = traitDict[name];
            return enumName;
		}

		public int getGenNum() {
			return genNum;
		}

		public int getProfNum() {
			return profNum;
		}

		public string getTraitDesc() {
			return desc;
		}

        public TraitName getTraitID(string name) {
            return traitDict[name];
        }
	}
}
