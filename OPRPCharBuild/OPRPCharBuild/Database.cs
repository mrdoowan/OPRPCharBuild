/*
 * Database that stores information of Rules
 * This will serve as a Static call
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPRPCharBuild
{
    public static class Database
    {
        // -------------------------------------------------------------------
        // PROFESSION
        // -------------------------------------------------------------------
        #region Profession String Consts
        public const string PROF_WA = "Weapon Specialist",
            PROF_MA = "Martial Artist",
            PROF_MS = "Marksman",
            PROF_SM = "Smith",
            PROF_CA = "Carpenter",
            PROF_IN = "Inventor",
            PROF_CH = "Chef",
            PROF_EN = "Entertainer",
            PROF_DO = "Doctor",
            PROF_AS = "Assassin",
            PROF_TH = "Thief",
            PROF_ME = "Merchant";
        #endregion

        static private Dictionary<string, Profession> profDict = new Dictionary<string, Profession>() {
			{PROF_WA, new Profession(PROF_WA,
                "A much more general fighter-type profession, a Weapon Specialist" +
                " is someone who has to a large or small degree devoted his/her life to the mastery of a single weapon " +
                "or weapon type, such as Swords, Axes or Whips. This profession applies generally to melee weapons.",
                "Characters with this profession have access to certain additional traits" +
                " and may create \"Stance\" techniques.")},
            {PROF_MA, new Profession(PROF_MA,
                "A Martial Artist is someone who specializes in hand-to-hand combat, completely " +
                "or very close to unarmed. They usually have good insight in the aspects of weight, balance and movement of the body.",
                "Characters with this profession have access to certain additional traits" +
                " and may create \"Stance\" techniques.")},
            {PROF_MS, new Profession(PROF_MS,
                "The profession of Marksman provides sound experience and insight in calculating range," +
                " distance and wind elements and their impact on the path of a projectile. It is a general trait that applies to guns, " +
                "slingshots, cannons, rifles and the like. People without this profession will have little to no luck in firing cannonballs " +
                "where they are supposed to go under normal battle conditions.",
                "Characters with this profession have access to certain additional traits and their range " +
                "techniques will have the effect costs of the tier below with short range being free.")},
            {PROF_SM, new Profession(PROF_SM,
                "A Smith is a man or woman who makes weapons and tools out of metal. Generally swords, since they " +
                "fetch the best price, but it is in no way limited to this. Making cannons and parts for inventors is also part of their business, " +
                "and they also get good knowledge within metallurgy which allows them to make custom materials or mix new ones together.",
                "A smith is proficient in any melee weapon they make, and have no maximum rank limit for martial " +
                "techniques involving these weapons. They have access to certain additional traits as well.")},
            {PROF_CA, new Profession(PROF_CA,
                "A Carpenter is someone who makes a living crafting things such as ships, submarines, buildings and other " +
                "physical structures with expertise and materials not privy to non-carpenters. Carpenters are the only ones capable of creating ships with techniques.",
                "A carpenter is proficient in the use of the tools of their trade as weapons, and have no maximum rank " +
                "limit for martial techniques involving these tools. Furthermore, carpenters are able to use techniques that enable the rapid construction " +
                "of basic, temporary structures such as walls, bridges and ladders. Carpenters have access to certain additional traits.")},
            {PROF_IN, new Profession(PROF_IN,
                "An Inventor is a man or woman with knowledge in how various mechanical devices work. They have good " +
                "insight in how blueprints work, and can upgrade weapons, build gadgets and battle engines if they have enough materials.",
                "Inventors gain the ability to create explosives that they may use in battle and the AoE of their " +
                "explosive techniques will have the effect costs of the tier below with short AoE being free. Inventors have access to certain additional traits.")},
            {PROF_CH, new Profession(PROF_CH,
                "A Chef, Cook, Bartender or any other distinct related profession gives kitchen skills and knowledge about " +
                "food, flavoring, ingredients, drink, and nutrients. They are masters at making the most out of whatever ingredients are available, and " +
                "know how to create a balanced diet for the crew that will keep everyone in fighting trim.",
                "Chefs can also create foods which strengthen those who eat it, granting buffs to their allies.")},
            {PROF_EN, new Profession(PROF_EN,
                "An Entertainer makes his/her living through putting on shows. This profession is always specialized " +
                "towards a specific type, and includes Dancer, Musician, Juggler and many more. Knowledge granted is job-specific.",
                "Entertainers can use their performance skills, be they song, dance or anything else, to influence " +
                "others and use them as various buffs.")},
            {PROF_DO, new Profession(PROF_DO,
                "A Doctor, on a ship or otherwise, gains large medical knowledge. Suturing and cleaning wounds, " +
                "making bandages out of various materials and preparing drugs, is all part of a Doctor's job. Doctors are the only ones capable " +
                "of healing serious injuries on the crew.",
                "Doctors have the ability to create drugs and toxins that can buff allies or debuff enemies.")},
            {PROF_AS, new Profession(PROF_AS,
                "The Assassin profession gives stealth skills, the ability to move silently through areas without " +
                "being detected. It also gives general poison knowledge. An Assassin generally has little actual combat experience, since their " +
                "victims don't fight back, and as such don't fare that well in a true fight.",
                "Assassins are able to create poisons that debuff, and have access to stealth techniques. Additionally " +
                "they will be proficient in small weapons such as daggers or blow darts.")},
            {PROF_TH, new Profession(PROF_TH,
                "The Thief is a job closely related to the assassin. They both provide stealth capabilities, though a " +
                "Thief gains lockpicking skills instead of poison knowledge, can generally know their way around the ways of barter and trade, and " +
                "pickpocketing. Thieves don't have it hard to find 'underground info' in most cities, either.",
                "Thieves will gain an extra 10% in Beli rewards in all Storylines they complete (for themselves). They " +
                "also have access to stealth techniques.")},
            {PROF_ME, new Profession(PROF_ME,
                "Merchants are fairly charismatic people who are skilled in both bargaining and selling things to others. " +
                "Their expertise means that they are very knowledgeable of the economy, and deal with all manner of merchandise. Additionally, " +
                "they they tend to be well-connected in trading circles.",
                "Merchants can get a 15% discount on any purchases they make, no matter what the item, for themselves. " +
                "Additionally, a merchant may purchase any item that is available to be purchased, regardless of the normal requirements.")}
		};

        static public Profession getProfession(string name) {
            try { return profDict[name]; }
            catch { return null; }
        }

        // -------------------------------------------------------------------
        // TRAITS
        // -------------------------------------------------------------------
        #region Traits String Consts
        public const string TR_STR1ST = "Mighty Strength",
            TR_SPE1ST = "Great Speed",
            TR_STA1ST = "Bear Stamina",
            TR_ACC1ST = "Keen Accuracy",
            TR_STR2ND = "Super Strength",
            TR_SPE2ND = "Sonic Speed",
            TR_STA2ND = "Mammoth Stamina",
            TR_ACC2ND = "Superb Accuracy",
            TR_STR3RD = "Monster Strength",
            TR_SPE3RD = "Ludicrous Speed",
            TR_STA3RD = "Giant Stamina",
            TR_ACC3RD = "Flawless Accuracy",
            TR_FATEST = "Fate of the Strong",
            TR_FATESW = "Fate of the Swift",
            TR_FATEMI = "Fate of the Mighty",
            TR_FATECU = "Fate of the Cunning",
            TR_FATEEM = "Fate of the Emperor",
            TR_FATEWH = "Fate of the Whimsical",
            TR_TISCRA = "\'Tis But A Scratch!",
            TR_MASTER = "[SPEC] Mastery",
            TR_ADVCLA = "Advanced [SPEC] Class Mastery",
            TR_ADVMAS = "Advanced [SPEC] Mastery",
            TR_ADVCYB = "Advanced Cyborg",
            TR_ADVSTA = "Advanced Stance Mastery",
            TR_ANASTR = "Anatomical Strike",
            TR_ANSATS = "Ansatsuken",
            TR_ANTIST = "Anti-Stealth",
            TR_ANTIWS = "Anti-Weapon Specialist",
            TR_ARTSTE = "Art of Stealth",
            TR_AWHAKI = "Awakened Haki",
            TR_BAKBAD = "Baking Bad",
            TR_BASCYB = "Basic Cyborg",
            TR_BATSUI = "Battle Suits",
            TR_BEAHAN = "Beast Handler",
            TR_BIZDUR = "Bizarrely Durable",
            TR_BOLEAD = "Born Leader",
            TR_BRILMI = "Brilliant Mind",
            TR_CIVLAW = "Civilian Lawyer",
            TR_COMCUI = "Combat Cuisine",
            TR_CQHAKI = "Conquering King Haki",
            TR_COOKFI = "Cooking Fighter",
            TR_CRITHI = "Critical Hit",
            TR_CROWDC = "Crowd Control",
            TR_CYBGEN = "Cybertronic Genius",
            TR_DAZPER = "Dazzling Performer",
            TR_DIHAKI = "Disciplined Haki",
            TR_DWARF = "Dwarf",
            TR_ESCART = "Escape Artist",
            TR_FISHMA = "Fishman",
            TR_FOODWA = "Food of Warriors",
            TR_FORFUR = "Forge Furnace",
            TR_FORMAS = "Forging Master",
            TR_FRSHAD = "From the Shadows",
            TR_FUNKBO = "Funky Body",
            TR_GRANDM = "Grandmaster",
            TR_GURU = "Guru",
            TR_HAGSEA = "Haggler of the Sea",
            TR_HAMRAG = "Hammer of Rage",
            TR_HARDFI = "Hardened Fighter",
            TR_HORTWA = "Horticultural Warfare",
            TR_INTRAI = "In-Training",
            TR_JACKTR = "Jack of All Trades",
            TR_JOHNTR = "John of All Trades",
            TR_LAOIRO = "Lamp, Oil, Rope",
            TR_LIFRET = "Life Return",
            TR_LIVSTO = "Living Stone",
            TR_MADSCI = "Mad Scientist",
            TR_MASMIS = "Master of Misdirection",
            TR_MEDMAL = "Medical Malpractice",
            TR_MERFOL = "Merfolk",
            TR_NATARM = "Natural Armour",
            TR_NEWCYB = "New World Cyborg",
            TR_PERFAS = "Performance Assistant",
            TR_PICKPO = "Pickpocket",
            TR_POISON = "Poison Killer",
            TR_POWSPE = "Powerful Speaker",
            TR_PROPPE = "Prop Performance",
            TR_QUICKS = "Quickstrike",
            TR_RAREFI = "Rare Find",
            TR_ROKUMA = "Rokushiki Master",
            TR_ROKUSA = "Rokushiki Savant",
            TR_SIEWAR = "Siege Warfare",
            TR_SIGTEC = "Signature Technique",
            TR_SKILUP = "Skill in Upgrades",
            TR_SKILME = "Skilled Use of Medicines",
            TR_SLEIGH = "Sleight of Hand",
            TR_SPECDF = "Specific Devil Fruit",
            TR_STAMAS = "Stance Mastery",
            TR_STRSPI = "Strong Spirit",
            TR_SUPSEN = "Super Sense",
            TR_SQLEAD = "Squad Leader",
            TR_TECHMA = "Technical Mastery",
            TR_TECHAD = "Technically Adept",
            TR_EXTRSP = "That Extra Special Ingredient",
            TR_TOPGUN = "Top Gun",
            TR_TUFFNA = "Tough as Nails",
            TR_TUFFBA = "Tough Bargainer",
            TR_TRAKIT = "Training of the Kitchen",
            TR_TRHAKI = "Transcendent Haki",
            TR_TRHUNT = "Treasure Hunter",
            TR_UNCENG = "Uncivil Engineering",
            TR_WEAKSI = "Weak Point Sighted",
            TR_WEATHR = "Weathermancy";
        #endregion

        static private Dictionary<string, Trait> traitDict = new Dictionary<string, Trait>() {
            { TR_STR1ST, new Trait(TR_STR1ST, 1, 0, "This character's strength is boosted by 20%.") },
            { TR_STR2ND, new Trait(TR_STR2ND, 2, 0, "This character's strength is boosted by 40%.") },
            { TR_STR3RD, new Trait(TR_STR3RD, 3, 0, "This character's strength is boosted by 60%.") },
            { TR_SPE1ST, new Trait(TR_SPE1ST, 1, 0, "This character's speed is boosted by 20%.") },
            { TR_SPE2ND, new Trait(TR_SPE2ND, 2, 0, "This character's speed is boosted by 40%.") },
            { TR_SPE3RD, new Trait(TR_SPE3RD, 3, 0, "This character's speed is boosted by 60%.") },
            { TR_STA1ST, new Trait(TR_STA1ST, 1, 0, "This character's stamina is boosted by 20%.") },
            { TR_STA2ND, new Trait(TR_STA2ND, 2, 0, "This character's stamina is boosted by 40%.") },
            { TR_STA3RD, new Trait(TR_STA3RD, 3, 0, "This character's stamina is boosted by 60%.") },
            { TR_ACC1ST, new Trait(TR_ACC1ST, 1, 0, "This character's accuracy is boosted by 20%.") },
            { TR_ACC2ND, new Trait(TR_ACC2ND, 2, 0, "This character's accuracy is boosted by 40%.") },
            { TR_ACC3RD, new Trait(TR_ACC3RD, 3, 0, "This character's accuracy is boosted by 60%.") },
            { TR_FATEST, new Trait(TR_FATEST, 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your strength stat.") },
            { TR_FATESW, new Trait(TR_FATESW, 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your speed stat.") },
            { TR_FATEMI, new Trait(TR_FATEMI, 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your stamina stat.") },
            { TR_FATECU, new Trait(TR_FATECU, 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your accuracy stat.") },
            { TR_FATEEM, new Trait(TR_FATEEM, 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated) to your fortune score.") },
            { TR_FATEWH, new Trait(TR_FATEWH, 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. Unlike the other Fate traits, these place-holder traits may be cashed-in and replaced with other traits without having to reach the next trait mark.") },
            { TR_TISCRA, new Trait(TR_TISCRA, 0, 1, "Weapon specialists are often used to fighting against other weapon specialists in close combat. Thus their chances of being injured by such weapons increase drastically compared to others. These particular characters are used to fighting so much under those conditions that they can actually ignore the effects of pain much better than their sissy counterparts and fight on.") },
            { TR_MASTER, new Trait(TR_MASTER, 0, 1, "The character has attained mastery in one specific weapon or form of attack that they have Martial Proficiency (can make techniques greater than rank 14) with. All techniques using this weapon or form of attack are always treated as being four ranks higher for purposes of calculating their technique rank benchmark, though their actual rank is not changed. This benefit ceases to apply for techniques higher than rank 28.") },
            { TR_ADVCLA, new Trait(TR_ADVCLA, 0, 1, "The character has expanded their mastery from a single weapon or form of attack to encompass a broader selection. They now receive the benefit of their ____ Mastery or Advanced ____ Mastery to a second type of Weapon or form of attack. Additionally, they gain special technique points which may be used to create attacks that use both weapons or forms of attack at once, in the amount of 25% of their fortune.") },
            { TR_ADVMAS, new Trait(TR_ADVMAS, 0, 2,"The character has attained supreme mastery in one specific weapon or form of attack that they have Martial Proficiency (can make techniques greater than rank 14) with. All techniques using this weapon or form of attack are always treated as being four ranks higher for purposes of calculating their technique rank benchmark, though their actual rank is not changed.") },
            { TR_ADVCYB, new Trait(TR_ADVCYB, 3, 0, "This is a high-tech cyborg, rarely encountered anywhere but the Grand Line. A character must be within the Grand Line to access this kind of technology. Advanced Cyborgs begin with up to 12M Beli worth of modifications. They are made of steel and may be constructed of materials up to titanium in strength, and may have additional body parts added. Their size may vary from their original form at a cost. Each arm may conceal two weapons and each leg may conceal one. Additionally, they may externally mount cybernetics/weapons at this stage. Cyborgs of this type may have multiple special systems, though moderators may enforce a limit eventually. Advanced Cyborgs may also purchase additional fuel charges up to a limit of 5. Upgrades from Basic Cyborg modifications are maintained and do not count against Advanced maximums.\n" +
                "[list][*]Appendages - 4.5M Beli/per\n" +
                "[*]Additional Body Parts – 6M Beli/per\n" +
                "[*]Torso - 9M Beli\n" +
                "[*]Titanium upgrade at 60% of the sum of cybernetic body parts\n" +
                "[*]Additional Weapon Slots - 2M Beli/per\n" +
                "[*]Additional Fuel Charges - 10M Beli/per[/list]") },
            { TR_ADVSTA, new Trait(TR_ADVSTA, 0, 1, "Through intense practice, the character has perfected their stance to a much greater degree. The character's stance techniques now only require a penalty to other stats equal to 50% of the total buff (fractional values rounded up for the penalty).") },
            { TR_ANASTR, new Trait(TR_ANASTR, 0, 1, "By using their extensive knowledge of the human anatomy,  these doctors can aim specifically for areas that will cause very serious,  even fatal wounds. The character can use up to a quarter of their Technique Points in techniques which hit vital points in the body. Striking vital points allows characters to do damage and cause a stat debuff related to the area struck.") },
            { TR_ANSATS, new Trait(TR_ANSATS, 0, 1, "The character has become so adept at striking vital areas and pressure points that they may do so passively in the course of their normal attacks. Any time the character lands a blow on an enemy, they may apply the one of their critical hit/quick strike technique debuffs. This debuff is half as strong as normal, but otherwise functions exactly like a normal critical hit/quick strike debuff, in terms of stacking and duration.") },
            { TR_ANTIST, new Trait(TR_ANTIST, 0, 1, "Having become masters of stealth themselves, these thieves/assassins are capable of knowing when their opponents are using the very same tactics they specialise in. The detection techniques of these characters are treated as if they are four ranks higher for purposes of effectiveness.") },
            { TR_ANTIWS, new Trait(TR_ANTIWS, 0, 1, "Through training and experience in fighting armed opponents when unarmed, the character gains special Technique Points, of the amount of half their fortune, for techniques which are aimed to disarm, disable or block the use of a weapon.") },
            { TR_ARTSTE, new Trait(TR_ARTSTE, 0, 1, "After a lifetime of learning how to survey an area's or targets' weak points, this assassin has a particularly effective set of skills for the purposes of stealth. All stealth techniques are treated as if they are four ranks higher for purposes of effectiveness.") },
            { TR_AWHAKI, new Trait(TR_AWHAKI, 4, 0, "The character has awakened their dormant ambition and is able to utilise this mysterious force in combat. They may specialise in one of the two basic colors (The Color of Armaments or The Color of Observation)or a Dormant Specialization. For their specialist color, the character's Haki maximum rank is 1/2 Fortune. For any other color, the maximum rank is 1/4 Fortune. They are also capped at rank 44 for their Specialist color techniques, and rank 28 for any other colors.") },
            { TR_BAKBAD, new Trait(TR_BAKBAD, 0, 1, "Experienced both in the ways of science and the cook, the character has taken their skills to the next level by combining them. They may now make chef techniques that can be used in combat to apply debuffs to their enemies simply by contacting them and work the same as Poison debuffs. They may also make kitchen-related gadgets.") },
            { TR_BASCYB, new Trait(TR_BASCYB, 1, 0, "This is the most basic level of cyborg, and the most commonly encountered type in the blues. A Basic Cyborg begins with up to 3M Beli worth of modifications. Their bodies are made out of Iron and do not vary from their original size. They may have one concealed weapon per arm or leg, and one basic system per other appendage. Note that these weapons or systems are very basic.\n" +
                "[list][*]Appendages - 1.5M Beli/per\n" +
                "[*]Torso - 3M Beli[/list]") },
            { TR_BATSUI, new Trait(TR_BATSUI, 0, 1, "Having mastered the usage of siege weapons, the Carpenter has now moved on to greater and much more impressive tools. With this trait, the user is able to create and utilise temporary Battle Suits which further enhance the user's combat potential in a variety of different ways.") },
            { TR_BEAHAN, new Trait(TR_BEAHAN, 1, 0, "Characters with this trait have a natural affinity for getting along with animals. Wild animals tend to behave either neutrally or friendly towards the character, unless they are very strongly aggressive. These animals, with proper approach and care, may be made to be merely distrustful and may sometimes refrain from attacking. The user also gains one NPC pet, who may possess humanlike intelligence, behaves as a crew NPC and is controlled by the player of the main character.") },
            { TR_BIZDUR, new Trait(TR_BIZDUR, 0, 1, "Durability sometimes reaches the point where it goes so far past the point of excessiveness that it becomes truly bizarre. Passing the rare state of partial permanent hardening and the unusual state of completely permanent hardening, these characters have surpassed what could be considered reasonable for any human being in terms of natural staying power. As such, their natural armor is treated the same as that of zoans and fishmen and can be upgraded with a standalone technique.") },
            { TR_BOLEAD, new Trait(TR_BOLEAD, 1, 0, "Characters with this trait are gifted with the ability to command respect and admiration from others. Sentient NPCs are easier to sway over to the side of those with this trait. They may gain information, calm down the angry, and sometimes even placate hostile adversaries. Additionally, those normally friendly to strangers find themselves easily enamoured with the character. The user also gains one NPC henchman, who behaves as a crew NPC and is controlled by the player of the main character. They are exceedingly loyal to their ‘master’.") },
            { TR_BRILMI, new Trait(TR_BRILMI, 0, 1, "Creative and with an incredible knack for making all sorts of strange gadgets, these inventors gain special Technique Points which can only be used on creation techniques for weapons and gadgets, of the amount of half their fortune.") },
            { TR_CIVLAW, new Trait(TR_CIVLAW, 0, 1, "These silver tongued devils have managed to work their ability to talk people into making the best financial deals for themselves into a practical ability. They are able to more easily influence those around them with their words.") },
            { TR_COMCUI, new Trait(TR_COMCUI, 0, 1, "Learning to adapt his food for rapid service on the battlefield, the Chef is able to increase the beneficial effects gained by those who eat it, albeit temporarily. A chef may use any cooking technique during combat, as if it were a Doctor's medicine, doubling the total value of the stat bonus for the duration. All other chef buffs on the target are over-written while this is active, but resume when it expires.") },
            { TR_CQHAKI, new Trait(TR_CQHAKI, 1, 0, "Those who chose to have a Dormant Specialization are able to take Conquerering King's Haki at 200 SD earned. This will allow them to overpower weaker enemies and combat their foes with sheer force of will. They will specialize in the Color of the Conqueror.") },
            { TR_COOKFI, new Trait(TR_COOKFI, 0, 1, "Experts in the use of various kitchen utensils, a chef may become proficient enough in the use of their cookware as weapons in battle. A chef with this trait gains martial proficiency with one form of cookware, as well as special technique points equal to one quarter of their fortune that can be used on martial techniques involving the chosen utensil.") },
            { TR_CRITHI, new Trait(TR_CRITHI, 0, 1, "Through training in how to hit vital points in the body, the character can use up to a quarter of their Technique Points in techniques which hit vital points in the body. Striking vital points allows characters to do damage and cause a stat debuff related to the area struck.") },
            { TR_CROWDC, new Trait(TR_CROWDC, 0, 1, "Due to the nature of their skills, their performances will affect basically anyone who can see and hear them. Buff techniques used by these entertainers will have the same maximum effect as if they were affecting a group of targets one crowd tier smaller than the size of the crowd the technique was intended to effect.") },
            { TR_CYBGEN, new Trait(TR_CYBGEN, 0, 1, "These inventors have studied their cybernetics especially well, and pay 20% less to upgrade their own Cyborg parts.") },
            { TR_DAZPER, new Trait(TR_DAZPER, 0, 1, "These entertainers are so skilled in their chosen field that they can use their performance skills to actually bring about changes in others. These entertainers gain special Technique Points that can only be used on their performance support techniques, of the amount of half their Fortune.") },
            { TR_DIHAKI, new Trait(TR_DIHAKI, 2, 0, "The character has trained and disciplined their ambition, allowing them to unlock their true potential. They are no longer limited by Rank caps, and their non-specialist maximum rank is 1/3 Fortune.") },
            { TR_DWARF, new Trait(TR_DWARF, 2, 0, "Dwarves are extremely small humanoids with fluffy tails. Dwarves gain the benefits of the Mighty Strength trait and have a mastery benefit on all their movement techniques of +4 ranks. Their small sizes makes them harder to hit but they also take more damage when hit. They tend to be extremely gullible.") },
            { TR_ESCART, new Trait(TR_ESCART, 0, 1, "Being a wily little bugger, the thief is a master of escaping from tight spots and difficult situations. Any debuff to the thief's speed is halved in duration, and they can easily escape most forms of restraint, such as being grappled, shackled, tied, etc.") },
            { TR_FISHMA, new Trait(TR_FISHMA, 2, 0, "Fishmen gain the benefits of the Mighty Strength trait, and may breath underwater. Their movements in water are not hampered as greatly as normal humans, and they may swim at great speeds. They also gain attributes or natural weapons based on their species. The number of traits needed depends on the species.") },
            { TR_FOODWA, new Trait(TR_FOODWA, 0, 1, "Years and years of experience in preparing the finest of meals, using all the right ingredients has led to these chefs being able to create foods which will make those who eat it stronger, faster, and better. They gain special Technique Points that can only be used on this food, of the amount of half their fortune.") },
            { TR_FORFUR, new Trait(TR_FORFUR, 0, 1, "After spending that much time welding weapons and having to put up with the extreme temperatures of the furnace, the character gains immunity to heat-based attacks similar to having an iron body. Any heat-based attack that wouldn’t damage iron won’t harm them.") },
            { TR_FORMAS, new Trait(TR_FORMAS, 0, 1, "All the experience the character has gained in making weapons allows them to produce them more cheaply. These smiths get a 20% discount for any weapons and armor they make for their own personal use.") },
            { TR_FRSHAD, new Trait(TR_FRSHAD, 0, 1, "Unlike others who have merely adopted the dark, the thief/assassin was born in it, molded by it. The thief/assassin gains special technique points equal to half their fortune that can be used to create Camouflage techniques, or any technique hides or otherwise obscures the presence of the character.") },
            { TR_FUNKBO, new Trait(TR_FUNKBO, 1, 0, "Whether gained by birth, training, modification by themselves or another person, prolonged exposure to something, or plain dumb luck, this character has a body that is less than typical. They have a single biological or physical difference to their bodies, canon examples including Kuramarimo's ability to rapidly grow and detach static afros and Don Chinjao's abnormally hard and pointy skull.") },
            { TR_GRANDM, new Trait(TR_GRANDM, 0, 2, "This character has achieved a legendary skill with his weapon/weapon class/form of attack. They are masters of their art beyond question, and are able to perform supernatural feats using their skill alone. Grandmaster ____'s mastery techniques allow them to cut/hit fluid and insubstantial substances, such as air, fire, water, or even light.") },
            { TR_GURU, new Trait(TR_GURU, 1, 0, "This character gains special technique points equal to 1/2 of their fortune, which can only be used on Life Return techniques.") },
            { TR_HAGSEA, new Trait(TR_HAGSEA, 0, 1, "There are financially crafty people out there and these carpenters are at the top of the game when it comes to the ship market. These carpenters are capable of negotiating the cost of 4 ranks off of any ship technique they may make for their ships. (Ship Techniques will be special technologies that a ship utilizes that can be quantified as a technique a sudden boost in speed, a weapon such as the Gaon cannon.") },
            { TR_HAMRAG, new Trait(TR_HAMRAG, 0, 1, "Knowing how to make things also means knowing how to break them. Some smiths come to revel in this destructive aspect of their trade even more than usual, becoming masters in breaking down anything with a form. The character is able to freeform breaking objects of up to iron hardness, and can break harder substances as if they were one tier lower in terms of technique rank.") },
            { TR_HARDFI, new Trait(TR_HARDFI, 0, 1, "Through excessive training in ing planks, punching rocks, and all those durability-building exercises, the character’s arms, up to their elbows, and their legs, up to their knees, have gained flesh that is as hard as iron, making them much more resistant to physical damage.") },
            { TR_HORTWA, new Trait(TR_HORTWA, 3, 0, "The character has become accustomed to using pop green seeds and as such is now able to utilise these seeds to bloom various flowers nearly instantly. The character can use these plants to make anything varying from a plant net to a large man-eating flower.") },
            { TR_INTRAI, new Trait(TR_INTRAI, 0, 1, "This is the equivalent of Fated for the character’s Professional Skills, but no paths exist for this. General traits may not be spent to purchase In-Training.") },
            { TR_JACKTR, new Trait(TR_JACKTR, 0, 1, "The character is able to master multiple skills, and has become adept at a number of things. This character gains two additional secondary professions.") },
            { TR_JOHNTR, new Trait(TR_JOHNTR, 0, 2, "The character is able to master multiple skills, and has become adept at a number of things. This character gains three additional secondary professions.") },
            { TR_LAOIRO, new Trait(TR_LAOIRO, 0, 1, "Certain minds have a better handle on ing things down than building them up. These inventors specialize in demolitions, and gain special technique points equal to a half of their fortune that may be spent on creation techniques for bombs and munitions.") },
            { TR_LIFRET, new Trait(TR_LIFRET, 3, 0, "The character has the ability to use the mystic ability known as Life Return. They gain control over their bodily functions through their thoughts. Growing and controlling their hair, changing their body fat, body muscles and at higher levels even their bloodflow, these characters are truly masters of their own body and have access to special buffs.") },
            { TR_LIVSTO, new Trait(TR_LIVSTO, 0, 1, "Through excessive training of their bodies they have gained a great deal of natural durability, these character's entire bodies can stand up to frightening amounts of abuse. As a result, they have gained flesh that is as hard as iron, making them much more resistant to physical damage.") },
            { TR_MADSCI, new Trait(TR_MADSCI, 0, 1, "The epitome of madness. Cackling and gleeful hand-rubbing is a favourite pastime of this character, and every time they finish an experiment, there are obligatory flashes of lightning. The mad scientist has the ability to make one major biological modification to themselves or any NPCs they control.") },
            { TR_MASMIS, new Trait(TR_MASMIS, 0, 1, "Those who practice stealth to depths far greater than most, becoming far more proficient than even others of their trade. These characters may treat any environment as Natural Camouflage, and the Silent effect becomes a General Effect.") },
            { TR_MEDMAL, new Trait(TR_MEDMAL, 0, 1, "Part of knowing what is required to keep somebody healthy is knowing what will cause others to become unhealthy. Doctors who put this knowledge to practical use may make especially potent poisons. They gain special Technique Points which can only be used on toxins that harm or debuff, of the amount of half their fortune.") },
            { TR_MERFOL, new Trait(TR_MERFOL, 1, 0, "In place of legs these characters have a tail which splits down the middle while they are standing on land (For this RP's purposes, this is the at any age, not just 30+). They may breathe underwater and gain the Great Speed trait whilst submerged, and their movements underwater are completely unhindered. Spending too long out of water can fatigue them somewhat. In addition, merfolk can talk to most types of fish, but not seakings.") },
            { TR_NATARM, new Trait(TR_NATARM, 0, 1, "Through all the time spent in the forge, working on weapons, the character’s arms, up to their elbows, and their legs, up to their knees, have gained flesh that is literally as tough as iron, making them much more resistant to physical damage.") },
            { TR_NEWCYB, new Trait(TR_NEWCYB, 5, 0, "These cyborgs represent the very pinnacle of scientific achievement in the world of cybernetics, being by right more machine than human. A character must be in either the Grand Line or New World to access this kind of technology. They can be made of materials up to titanium in strength but may have built-in armor made from custom-materials. Alongside this, these cyborgs are given up to 21M Beli worth of modifications. New World Cyborgs pay 30% to upgrade weaker materials. These Cyborgs are able to create body systems that provide personal flight, self-repair, fire energy projectiles and more.\n" +
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
                "The special thing about all of these systems is that they are able to be input into the cyborg’s internal systems. An Inventor with access to R44 techniques can still build a laser, however they must buy a holder for the laser that will cost them money.") },
            { TR_PERFAS, new Trait(TR_PERFAS, 0, 1, "Plenty of good entertainers need someone to help them carry out their routine. The character gains a special Performance Assistant NPC which is under their control.") },
            { TR_PICKPO, new Trait(TR_PICKPO, 0, 1, "As a thief, the character is skilled at obtaining money by less-than-acceptable means. Thus, they will always earn 20% more Beli in their Storylines.") },
            { TR_POISON, new Trait(TR_POISON, 0, 1, "With some basic training in the use of poisons to kill or debuff their targets, these assassins gain special Technique Points that can only be used on poisons, of the amount of half their fortune.") },
            { TR_POWSPE, new Trait(TR_POWSPE, 0, 1, "This entertainer is particularly skilled at inspiring the masses. These entertainers are capable of, with a simple phrase, increasing the fighting abilities of their allies. These buffs last much shorter than normal but consume no AE and only need to be said once for it to come into effect, as opposed to a sustained 'performance'. They utilize Personal Buff measurements (see Techniques page)in terms of buff potency and have a duration rank/4 posts. This impromptu buff takes up a Personal Buff slot.") },
            { TR_PROPPE, new Trait(TR_PROPPE, 0, 1, "An entertainer with this trait has learned how to integrate one of their performing props into their fighting style. They gain the ability to make martial techniques involving one item above rank 14. Additionally, they may perform freeform attacks with this item while performing at no additional expense of action economy.") },
            { TR_QUICKS, new Trait(TR_QUICKS, 0, 1, "Because of their training in making stealth attacks and hitting vital organs, the character can use up to a quarter of their Technique Points in techniques which hit vital points in the body. Striking vital points allows characters to do damage and cause a stat debuff related to the area struck.") },
            { TR_RAREFI, new Trait(TR_RAREFI, 0, 1, "A keen merchant always has his eyes and ears open for an exceptional deal. At the end of every SL, the character will be offered a rare and valuable item determined at random (weapons, armour, devices, dials, materials, etc.)This item will have double their normal discount applied if it is purchased.") },
            { TR_ROKUMA, new Trait(TR_ROKUMA, 1, 0, "The character has completely mastered Rokushiki. Through vigorous training of pushing their body to the utmost limit, they are now able to make their own custom variations on any Rokushiki Technique. Additionally they gain access to the most powerful Rokushiki technique of all, Rokuogan.") },
            { TR_ROKUSA, new Trait(TR_ROKUSA, 1, 0, "The character has a natural talent for Rokushiki techniques and as such, gains Special Technique Points equal to half their fortune to spend on their Rokushiki techniques.") },
            { TR_SIEWAR, new Trait(TR_SIEWAR, 0, 1, "Carpenters after years of dedication to their craft have become masters of designing impressive war machines. They can create such beasts of war as catapults, battering rams, and various other tools of war quickly in the middle of a fight even. They gain Special Technique Points equal to half their Fortune for this purpose.") },
            { TR_SIGTEC, new Trait(TR_SIGTEC, 1, 0, "This trait makes one particular technique special, a trademark move of the character. This signature technique is always of the natural maximum rank that the character is allowed, but does not cost any Technique Points. In addition, these techniques are allowed to have weaker variations, though these variations must follow the same path set by the main technique, and cannot be used to branch to techniques that do not branch into the main technique.") },
            { TR_SKILUP, new Trait(TR_SKILUP, 0, 1, "These fellows are amazing at what they do, such that they can do it for a much lower cost. All technological upgrades that they make to weapons get a 20% discount. This discount applies only to the inventor themselves, and not weapons made for others.") },
            { TR_SKILME, new Trait(TR_SKILME, 0, 1, "Because of their expertise, these doctors have become extremely adept at producing their own medicines to aid others. They gain special Technique Points which can only be used on medicines that aid or buff, of the amount of half their fortune.") },
            { TR_SLEIGH, new Trait(TR_SLEIGH, 0, 1, "\"Always watch the hands\" is the rule when dealing with a professional thief such as this. These thieves are so good at stealing that they gain special TP equal to half their fortune for techniques that involve stealing the items and weaponry off their target even during combat.") },
            { TR_SPECDF, new Trait(TR_SPECDF, 3, 0, "The player may choose a specific Devil Fruit, either canon or custom, for their character. These fruits are limited based on moderator discretion but can be of any of the three types.") },
            { TR_STAMAS, new Trait(TR_STAMAS, 0, 1, "The character has mastered the use their stance techniques. All stances the character are always treated as being four ranks higher for purposes of calculating their technique rank benchmark and numerical benefit, though their actual rank is not changed.") },
            { TR_STRSPI, new Trait(TR_STRSPI, 1, 0, "The character gains an amount of special technique points equal to 1/2 of their fortune that may only be used on Haki techniques of the colour they are specialized in.") },
            { TR_SUPSEN, new Trait(TR_SUPSEN, 1, 0, "This character has achieved mastery over their heightened senses, gaining a single sense on par with Observation Haki. They gain special Technique Points that can only be used on this sense, of the amount of a quarter of their fortune.") },
            { TR_SQLEAD, new Trait(TR_SQLEAD, 2, 0, "Characters with this trait are especially gifted with the ability to command respect and admiration from others. In combat, their fame mooks act a single group and are able to be used in techniques just like a trait NPC would. When using difference creatures/species for mooks, the number of mooks should be equivalent to human mooks in strength.") },
            { TR_TECHMA, new Trait(TR_TECHMA, 2, 0, "Increases total technique points by 100% of your fortune score, but does not stack with Technically Adept (this benefit overrides the previous benefit). Requires Technically Adept and 100 SD earned.") },
            { TR_TECHAD, new Trait(TR_TECHAD, 1, 0, "Increases total technique points by 40% of your fortune score.") },
            { TR_EXTRSP, new Trait(TR_EXTRSP, 0, 1, "This is a chef who has traveled around the world searching for only the finest of ingredients to add their their foods. These ingredients are capable of giving profound supernatural effects to the consumer (for example, jalapenos that bestow fire breath).") },
            { TR_TOPGUN, new Trait(TR_TOPGUN, 0, 1, "These marksmen are top of the line when it comes to being able to draw their weapon and fire. These characters are also always reloading their gun reflexively faster than those around them notice thus they never have to actually deal with taking the time to reload at a critical moment in battle.") },
            { TR_TUFFNA, new Trait(TR_TUFFNA, 0, 1, "Through all the manual labour that they have done, the character’s arms, up to their elbows, and their legs, up to their knees, have gained flesh that is literally as tough as iron, making them much more resistant to physical damage.") },
            { TR_TUFFBA, new Trait(TR_TUFFBA, 0, 1, "Ever the successful businessman, merchants know how to extract money from people and to keep as much as they can from leaving their pocket. Thus, they will always earn 20% more Beli in their Storylines.") },
            { TR_TRAKIT, new Trait(TR_TRAKIT, 0, 1, "After spending so long in front of the open fires in the kitchen, the character gains immunity to heat-based attacks similar to having an iron body. Any heat-based attack that wouldn’t damage iron won’t harm them.") },
            { TR_TRHAKI, new Trait(TR_TRHAKI, 1, 0, "The character has become exceptionally proficient in both their specialist and non-specialist haki. Their non-specialist haki no longer has a maximum rank. ") },
            { TR_TRHUNT, new Trait(TR_TRHUNT, 0, 1, "With their knack for discovering hidden items that don’t necessarily belong to them, these thieves are very skilled in looking for treasures. Thus, in their SLs, they will always get an extra little item. It could be something quirky, or it could be something rare.") },
            { TR_UNCENG, new Trait(TR_UNCENG, 0, 1, "Characters with this trait are undisputed masters of rapidly fabricating objects. They get Special Technique Points equal to half their Fortune for this specific purpose. ") },
            { TR_WEAKSI, new Trait(TR_WEAKSI, 0, 1, "With all their experience in building things and taking them apart, these characters are able to pinpoint the weaknesses in architecture and can easily figure out how to do the most damage to weapons, ships, buildings and other large constructions.") },
            { TR_WEATHR, new Trait(TR_WEATHR, 3, 0, "After the learning of skills of weather manipulation and creation, the character may - through the use of tools and gadgets - actively perform feats such as summoning lightning bolts, hail, rain, etc. They require an weapon/item to serve as a conduit for such techniques.") }
        };

        // Getter function of the Trait dictionary
        // Assume good input.
        static public Trait getTrait(string traitName) {
            try { return traitDict[traitName]; }
            catch { return null; }
        }

        // Puts all the Trait names into a List for comboBox
        static public List<string> getTraitNames() {
            List<string> listTraits = new List<string>();
            foreach (string name in traitDict.Keys) {
                listTraits.Add(name);
            }
            return listTraits;
        }

        // -------------------------------------------------------------------
        // Special Traits
        // -------------------------------------------------------------------

        static private Dictionary<string, int> spTraitDiv = new Dictionary<string, int>() {
            { TR_SUPSEN, 4 },
            { TR_STRSPI, 2 },
            { TR_ROKUSA, 2 },
            { TR_ANTIWS, 2 },
            { TR_ADVCLA, 4 },
            { TR_UNCENG, 2 },
            { TR_SIEWAR, 2 },
            { TR_BRILMI, 2 },
            { TR_LAOIRO, 2 },
            { TR_FOODWA, 2 },
            { TR_COOKFI, 4 },
            { TR_DAZPER, 2 },
            { TR_SKILME, 2 },
            { TR_MEDMAL, 2 },
            { TR_POISON, 2 },
            { TR_FRSHAD, 2 },
            { TR_SLEIGH, 2 },
            { TR_GURU, 2 }
        };

        // Getter function of spTraitDiv dictionary
        static public int getSpTraitDiv(string traitName) {
            try { return spTraitDiv[traitName]; }
            catch { return 0; }
        }

        // -------------------------------------------------------------------
        // ROKUSHIKI
        // -------------------------------------------------------------------
        #region Rokushiki String Consts
        public const string ROKU_SHI = "Shigan",
            ROKU_RAN = "Rankyaku",
            ROKU_SOR = "Soru",
            ROKU_KAM = "Kami-E",
            ROKU_TEK = "Tekkai",
            ROKU_GEP = "Geppo",
            ROKU_ROK = "Rokuogan",
            ROKU_NON = "NONE";
        #endregion

        static private Dictionary<string, Rokushiki> rokuDict = new Dictionary<string, Rokushiki>() {
            #region Rokushiki Database
            { ROKU_SHI, new Rokushiki(ROKU_SHI, 20, "Offensive", "Melee", 4, 0, 0, 0, 0,
                "This technique is an improved melee strike. The damage done by the user's finger (or other small appendage) is treated as both blunt and piercing, generally using the more favourable of the two damage types when the other would not be as effective. Additionally, the attack is quick and difficult to track, executed at bullet-like speeds. (Damage Type Change, Piercing, Quick)") },
            { ROKU_RAN, new Rokushiki(ROKU_RAN, 22, "Offensive", "Long", 8, 0, 0, 0, 0,
                "By kicking fast enough, the user literally \"cuts\" their air with their foot. The projectile is different from standard ranged melee, possessing a keen cutting edge and unusual longevity. A Rankyaku shockwave will continue moving until it reaches the edge of it's range or impacts with an obstacle harder than stone. (Damage Type Change, Piercing, Shockwave, Long Range)") },
            { ROKU_SOR, new Rokushiki(ROKU_SOR, 28, "Support", "Self", 20, 0, 0, 0, 0,
                "Once per post, an individual can use this technique to put on momentary bursts of speed. The user may move to any location that they could normally move to in a single post, but they do so near-instantaneously. During this movement, they cannot be seen by anybody who's accuracy is less than double this technique's rank, appearing as a blurred streak to anybody who's accuracy is above that point. If attacked while moving by a foe who's speed is less than this technique's rank, the attack can be passively dodged without any expenditure of Focus. Attacks cannot be made while moving with Soru. (Quick, Perception Formula)") },
            { ROKU_KAM, new Rokushiki(ROKU_KAM, 32, "Defensive", "Self", 20, 0, 0, 0, 0,
                "Using instinct-driven, reflexive movements and loosening up all of their muscles, the user dodges an attack much the way a piece of paper in the breeze does. While in use, this technique reduces the effective accuracy of any targeted attack made against the user by a value equal to this technique's power. If this would reduce the accuracy of that attack to 0, the attack can be passively dodged without any expenditure of Focus. Attacks can not be made while using Kami-E. (Paperlike, Fullbody, Dodging Formula)") },
            { ROKU_TEK, new Rokushiki(ROKU_TEK, 32, "Defensive", "Self", 22, 0, 0, 0, 0,
                "By tensing their muscles and focusing, the user of this technique may drastically reduce the damage they take from physical attacks. While this technique is active, the user is treated as if their body has steel-level defenses. Bladed and piercing attacks that are not capable of cutting through steel do blunt damage for the technique's duration, and all blunt damage suffered while this technique is active is significantly reduced. The user of this technique may not move while it is active. (Mid Tier Material, Full Body)") },
            { ROKU_GEP, new Rokushiki(ROKU_GEP, 36, "Support", "Self", 0, 0, 0, 0, 0,
                "By kicking off of the air itself, the user of this technique can seemingly fly or \"Air Walk.\" The user of this technique may remain airborn indefinitely, but must land in the following post if they attack or are attacked (if there is no land within range, they spend the next post falling until they can resume use of Geppo). If the technique's user is significantly encumbered, or if they lack use of one of their legs, they must land periodically, being able to sustain flight with this technique for no more than three consecutive rounds. (Personal 'Flight')") },
            { ROKU_ROK, new Rokushiki(ROKU_ROK, 44, "Offensive", "Melee", 20, 0, 0, -22, -22,
                "Using principles of all six Rokushiki techniques, this technique executes a devastating close-range attack. Though it must be used on a target no more than a yard from the user, this technique creates a shockwave that bypasses almost any and all forms of physical defense, doing internal damage to it's victim as if they were unarmoured and made of ordinary flesh and blood (though it does not bypass some devil fruit abilities on it's own). Additionally, the shock of this attack is particularly incapacitating, weakening the target's constitution significantly. (Inbuilt Critical Hit, Defense Bypassing)") },
            { ROKU_NON, new Rokushiki() }
            #endregion
        };

        // Getter function of Rokushiki dictionary
        static public Rokushiki getRoku(string name) {
            try { return rokuDict[name]; }
            catch { return rokuDict[ROKU_NON]; }
        }

        // -------------------------------------------------------------------
        // EFFECTS
        // -------------------------------------------------------------------
        #region Effects String Consts
        public const string EFF_DISPL = "Displacement",
            EFF_DISOR = "Disorient",
            EFF_GATLI = "Gatling",
            EFF_DEFLE = "Deflecting",
            EFF_RICOC = "Ricochet",
            EFF_UNPRE = "Unpredictable",
            EFF_DMGTC = "Damage Type Change",
            EFF_DISAR = "Disarm",
            EFF_SHOCK = "Shockwaves",
            EFF_CURVE = "Curving Projectiles",
            EFF_OMNID = "Omni-Directional",
            EFF_HAKEN = "Haki Enhancement",
            EFF_ELDMG = "Elemental Damage",
            EFF_FLAVR = "Flavor",
            EFF_SPRIT = "Spirit Generated Illusions",
            EFF_SECON = "Secondary",
            EFF_QUICK = "Quick",
            EFF_PIERC = "Piercing",
            EFF_AFTER = "After-Image",
            EFF_ADAFT = "Additional After-Image",
            EFF_CONST = "Construct",
            EFF_REVER = "Reversal",
            EFF_DURAT = "Duration Damage",
            EFF_DISAB = "Disables",
            EFF_SENSI = "Sensory Overload (Single)",
            EFF_SENMU = "Sensory Overload (Multiple)",
            EFF_LASER = "Laser",
            EFF_DEFBY = "Defense Bypassing",
            EFF_SPEBL = "Specific Type Block",
            EFF_STBRE = "Starter Tier Breaking",
            EFF_MIBRE = "Mid Tier Breaking",
            EFF_HIBRE = "High Tier Breaking",
            EFF_ARHAK = "Armaments Haki",
            EFF_ELCOA = "Elemental Coating",
            EFF_FULLB = "Full Body Effects",
            EFF_STDEF = "Starter Tier Defense",
            EFF_MIDEF = "Mid Tier Defense",
            EFF_HIDEF = "High Tier Defense",
            EFF_MELEE = "Melee",
            EFF_SHORT = "Short",
            EFF_MEDIU = "Medium",
            EFF_LONG = "Long",
            EFF_VLONG = "Very Long",
            EFF_SHAOE = "Short AoE",
            EFF_MDAOE = "Medium AoE",
            EFF_LOAOE = "Long AoE",
            EFF_DOPIN = "Doping",
            EFF_BANDA = "Bandaging",
            EFF_STITC = "Stitches",
            EFF_SPLIN = "Splints",
            EFF_SALVE = "Salves",
            EFF_ANTIV = "Anti-Venom",
            EFF_TRAPS = "Traps",
            EFF_STRUC = "Structures",
            EFF_WDEFC = "Wooden Defenses (Carpenter)",
            EFF_SIEGE = "Siege Weapons",
            EFF_BSUIT = "Battle Suits",
            EFF_CLOUD = "Cloud",
            EFF_RAIN = "Rain",
            EFF_FOGMI = "Fog/Mist",
            EFF_ELEWE = "Elemental Damage (Weather)",
            EFF_WIND = "Wind",
            EFF_MILCL = "Milky Cloud",
            EFF_MIRCL = "Mirage (Clones)",
            EFF_MIRCA = "Mirage (Camouflage)",
            EFF_SENTI = "Sentience",
            EFF_FLSTR = "Floral Structures",
            EFF_WDEFP = "Wooden Defenses (Pop Greens)",
            EFF_ELEPG = "Elemental Damage (Pop Green)",
            EFF_SMOKE = "Smoke",
            EFF_CROWD = "Crowd Blending",
            EFF_SILEN = "Silent",
            EFF_SCENT = "Scentless",
            EFF_DISGU = "Disguise",
            EFF_PICKP = "Pickpocket",
            EFF_NATCA = "Natural Camouflage",
            EFF_OPECA = "Open Camouflage",
            MELEE_DESC = "Range of direct physical combat.",
            SHORT_DESC = "Range of a few meters.",
            MEDIU_DESC = "Range of half of a sport's stadium.",
            LONG_DESC = "Range of an entire sport's stadium.",
            VLONG_DESC = "Range of a small city.",
            SHAOE_DESC = "A few meters in diameter.",
            MDAOE_DESC = "Half of a sport's stadium in diameter.",
            LOAOE_DESC = "An entire sport's stadium in diameter.";
        #endregion

        static private Dictionary<string, Effect> effectDict = new Dictionary<string, Effect>() {
            { EFF_MELEE, new Effect(EFF_MELEE, false, 0, 0, MELEE_DESC) },
            { EFF_SHORT, new Effect(EFF_SHORT, false, 4, 4, SHORT_DESC) },
            { EFF_MEDIU, new Effect(EFF_MEDIU, false, 8, 16, MEDIU_DESC) },
            { EFF_LONG, new Effect(EFF_LONG, false, 16, 28, LONG_DESC) },
            { EFF_VLONG, new Effect(EFF_VLONG, false, 32, 66, VLONG_DESC) },
            { EFF_SHAOE, new Effect(EFF_SHAOE, false, 8, 8, SHAOE_DESC) },
            { EFF_MDAOE, new Effect(EFF_MDAOE, false, 16, 28, MDAOE_DESC) },
            { EFF_LOAOE, new Effect(EFF_LOAOE, false, 32, 44, LOAOE_DESC) },
            { EFF_DISPL, new Effect(EFF_DISPL, true, 8, 8,
                "Common examples are knockback techniques. The effect of a displacement technique can be greater at higher ranks. A stronger opponent will be displaced less by a weaker character using a displacement technique on them.") },
            { EFF_DISOR, new Effect(EFF_DISOR, true, 8, 8,
                "Techniques which unbalance or use visual tricks to hinder opponents. Effectiveness increases the higher the rank.") },
            { EFF_GATLI, new Effect(EFF_GATLI, true, 8, 8,
                "A series of rapid attacks performed quickly. Each individual strike is weaker but the greater amount of attacks make them harder to avoid. Higher ranked gatlings have even more strikes.") },
            { EFF_DEFLE, new Effect(EFF_DEFLE, true, 8, 8,
                "This is applicable for Defensive Type techniques only. Techniques which divert the direction of an attack. Different from Reversal techniques which attempt to send an opponent's attack back at them. With higher ranks things like bullets and even elements can be deflected.") },
            { EFF_RICOC, new Effect(EFF_RICOC, true, 8, 8,
                "Techniques which use walls or other surfaces to bounce projectiles off. Higher ranks can have more complex ricochets, multiple 'bounces' etc.") },
            { EFF_UNPRE, new Effect(EFF_UNPRE, true, 8, 14,
                "Techniques which even the user cannot anticipate the outcome of. Encompasses the realm of indirect attacks and others. The results are dependent on the situation they are used in. As the user does not know the outcome, anticipation techniques such as observation haki are less effective versus them.") },
            { EFF_DMGTC, new Effect(EFF_DMGTC, true, 4, 14,
                "A punch that cuts or a blade with the sharp edge of a pillow. Higher ranked versions can have multiple damage types or supernatural types etc.") },
            { EFF_DISAR, new Effect(EFF_DISAR, true, 8, 8,
                "Disable techniques which target a weapon or item of an opponent to temporarily remove or steal it in combat. Requires traits like Anti-Weapon Specialist. Higher rank has higher chance of success with respect to RP considerations.") },
            { EFF_SHOCK, new Effect(EFF_SHOCK, true, 10, 14,
                "Ranged melee techniques generated by striking the air with a fist/sword swing etc. The cost of the effect is paid by the range table below. Larger shockwaves will also need to pay AoE.") },
            { EFF_CURVE, new Effect(EFF_CURVE, true, 14, 20,
                "Bullets, arrows, shockwaves and other projectile techniques that do not follow a straight path but instead move in the air. At higher ranks, they can perform more unlikely turns in the air though the path they follow is always per-determined when fired.") },
            { EFF_OMNID, new Effect(EFF_OMNID, true, 10, 20,
                "\"Perfect-sphere\" or techniques that attack/defend in all directions surrounding the user. They have a built in melee AoE around the user and pay range increases from the AoE table below. ") },
            { EFF_HAKEN, new Effect(EFF_HAKEN, true, 12, 12,
                "User coats an invisible suit of armour around a pair of limbs or a weapon. When added to techniques at Rank 28 and beyond, they gain the ability to strike a devil fruit user's real body.") },
            { EFF_ELDMG, new Effect(EFF_ELDMG, true, 14, 14,
                "Adds the benefits of a single element to a technique. If created through practical means (ie Inventions, Matches etc), the minimum rank is Rank 14. If created by shonen science or will, minimum rank is Rank 28. Can be generated in greater volumes at higher ranks.") },
            { EFF_FLAVR, new Effect(EFF_FLAVR, true, 0, 0,
                "Technique flair that does not grant any advantage other than making things look fancier. The higher the rank, the more exotic.") },
            { EFF_SPRIT, new Effect(EFF_SPRIT, true, 14, 28,
                "Eye catching illusions that have significance, lasting only momentarily in battle but can shock or intimidate foes. Asura is a good example for these effects.") },
            { EFF_QUICK, new Effect(EFF_QUICK, false, 8, 8,
                "Techniques that focus more on striking an opponent as quickly as possible at the expense of power thus they often don't hit as hard as techniques of similar rank but come out faster.") },
            { EFF_PIERC, new Effect(EFF_PIERC, false, 8, 8,
                "Techniques that hold great penetrative force. These attacks are able to pass through an object with ease assuming they are not equivalent to a tier material. With each thing these attacks pass through they lose a significant amount of power.") },
            { EFF_AFTER, new Effect(EFF_AFTER, false, 8, 14,
                "A singular after-image that looks like the user. The after image repeats an action performed at high speed by the user though at a slower pace to be perceived normally. It cannot do damage, nor has any physical form and will disperse after 1 post or after it is hit. The power of these techniques improves the authenticity of the after-image.") },
            { EFF_ADAFT, new Effect(EFF_ADAFT, false, 4, 14,
                "NOTE: Requires After-Image as an effect!\nAllows the technique to gain an extra after-image. This effect can stack for each additional payment.") },
            { EFF_CONST, new Effect(EFF_CONST, false, 10, 14, 
                "Used when creating a semi-permanent construct out of a resource available to the user which they can control. Requires full AE to activate and half AE for each round after to maintain, duration follows the professional buff table. Applicable to Life Return Hair, Production Paramecia & Logias.") },
            { EFF_REVER, new Effect(EFF_REVER, false, 16, 16,
                "This is applicable for Defensive Type techniques only. Similar but not limited to an upgraded version of deflecting. Reversal techniques are all attempts to repel a form of attack back at an opponent. With higher ranks, more obscure or supernatural techniques can be reversed.") },
            { EFF_DURAT, new Effect(EFF_DURAT, false, 8, 8,
                "Encompasses all damage over time abilities. Deep cuts that cause bleeding or venom that cause persistent pain. Higher ranks can include other effects that persist for several turns.") },
            { EFF_DISAB, new Effect(EFF_DISAB, false, 14, 14,
                "Techniques which target a specific part of the body to cripple temporarily, whether through dislocation or numbing etc.") },
            { EFF_SENSI, new Effect(EFF_SENSI, false, 14, 14,
                "Disable techniques which target a specific sense or multiple senses. Examples can be blinding flashes or deafening noises.") },
            { EFF_SENMU, new Effect(EFF_SENMU, false, 30, 30,
                "Disable techniques which target a specific sense or multiple senses. Examples can be blinding flashes or deafening noises.") },
            { EFF_LASER, new Effect(EFF_LASER, false, 28, 44,
                " Techniques which are beams fired at the speed of light. They shoot in straight lines and tend to only be narrowly avoided. New World Cyborg lasers get this effect for free. This effect is restricted to Lasers from Cyborgs & Inventors.") },
            { EFF_DEFBY, new Effect(EFF_DEFBY, false, 28, 28,
                "Techniques which bypass Defensive Type Techniques by transferring damage through blocks on contact or creating shockwaves that attack an opponent internally.") },
            { EFF_SPEBL, new Effect(EFF_SPEBL, false, 28, 28,
                "Block techniques with special properties to defend against a specific type of attack. They significantly reduce damage against the chosen type regardless of the power behind them.") },
            { EFF_STBRE, new Effect(EFF_STBRE, false, 14, 14,
                "A technique capable of breaking a single weapon or piece of armour of Starter Tier Material (i.e. Iron).") },
            { EFF_MIBRE, new Effect(EFF_MIBRE, false, 28, 28,
                "A technique capable of breaking ta single weapon or piece of armour of Medium Tier Material (i.e. Steel).") },
            { EFF_HIBRE, new Effect(EFF_HIBRE, false, 44, 44,
                "A technique capable of breaking a single weapon or piece of armour of High Tier Material (i.e. Titanium).") },
            { EFF_ARHAK, new Effect(EFF_ARHAK, false, 6, 14,
                "User creates a coat of hardened armour around a pair of limbs or a weapon. Offers passive defensive benefit equal to technique power. At Rank 28 and beyond, Armaments Haki gains the passive ability to strike a devil fruit user's real body whilst this buff is active on them.") },
            { EFF_ELCOA, new Effect(EFF_ELCOA, false, 28, 28,
                "User has developed a technique to generate and bind a type of element to a pair of limbs or a weapon. Grants the passive benefits of the element, and a passive offensive benefit equal to technique power.") },
            { EFF_FULLB, new Effect(EFF_FULLB, false, 10, 20,
                "The cost of extending an effect of a technique from limbs to the entire body. Effects such as Tekkai, Armaments Haki amongst others use this.") },
            { EFF_STDEF, new Effect(EFF_STDEF, false, 4, 4,
                "Reduces damage of Rank 13 and below techniques. NOTE THAT 1) Tier Defenses may require some special abilities, 2) Wearable armor requires Techniques with this Effect, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.") },
            { EFF_MIDEF, new Effect(EFF_MIDEF, false, 12, 12,
                "Reduces damage of Rank 27 and below techniques. NOTE THAT 1) Tier Defenses may require some special abilities, 2) Wearable armor requires Techniques with this Effect, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.") },
            { EFF_HIDEF, new Effect(EFF_HIDEF, false, 24, 24,
                "Reduces damage of Rank 43 and below techniques. NOTE THAT 1) Tier Defenses may require some special abilities, 2) Wearable armor requires Techniques with this Effect, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.") },
            { EFF_DOPIN, new Effect(EFF_DOPIN, false, 4, 14,
                "(DOCTOR) - Doping allows characters to use pills, injections, or other similar means to effectively ignore injury for a set duration in order to allow their bodies to fight at full strength while they are in effect. As this is very taxing on the body, doping techs have a cool down equal to duration. The effectiveness of doping on a target halves after each use within the same topic regardless of the rank or source of the original doping technique used.") },
            { EFF_BANDA, new Effect(EFF_BANDA, true, 4, 4,
                "(DOCTOR) - Bandages fix everything, at least to a certain extent. When applied, bandages function as a retroactive block on par with a block of similar power. Bandaging does not regenerate or work instantaneously, with excessive exertion running the risk of wounds reopening. ") },
            { EFF_STITC, new Effect(EFF_STITC, false, 28, 28,
                "(DOCTOR) - Stitches are intended to undo cutting damage by means of suturing wounds quickly. In this case, the doctor performs the sutures with speed and effectiveness, making them much more effective than those typically used in free form. As such, they are treated as a specific block for cutting damage applied retroactively. ") },
            { EFF_SPLIN, new Effect(EFF_SPLIN, false, 28, 28,
                "(DOCTOR) - Splints are used to remedy the effects of blunt damage to bodies, including fractures and bruising. Due to a high amount of skill, these splints have supernatural effectiveness, and are treated as a specific block for blunt damage applied retroactively.") },
            { EFF_SALVE, new Effect(EFF_SALVE, false, 28, 28,
                "(DOCTOR) - Salves are special creams that are used to alleviate damage that is elemental in nature. They are able to effectively treat the likes of burns, shocks, and freezing, and are very effective in that regard, being treated as a specific block for elemental damage applied retroactively.") },
            { EFF_ANTIV, new Effect(EFF_ANTIV, false, 28, 28,
                "(DOCTOR) - Anti-venoms are special medicines intended to purge the effects of damaging poisons from the affected individual. While they are not effective on debuffing poisons, anti-venoms are able to treat all other poisons incredibly well, being treated as a specific block for poison damage applied retroactively.") },
            { EFF_TRAPS, new Effect(EFF_TRAPS, false, 4, 4,
                "(CARPENTER) - Traps come in all kinds of shapes and sizes, ranging from a simple mouse trap to even more elaborate contraptions such as swinging log traps, or booby-trapped household property. These creations are made to hinder and restrict, but are considered tierless in terms of their defenses. Higher ranks can have more complex set-ups, remote triggers or include more dangerous elements and be generally more difficult to detect.") },
            { EFF_STRUC, new Effect(EFF_STRUC, true, 4, 4,
                "(CARPENTER) - Carpenters can craft different kinds of furniture for supportive purposes, such as chairs and tables. They can also make ladders, form temporary rafts, trampolines and other useful items that would normally need to be purchased. These creations have a base duration that follows the professional buff table. Additionally, weapons created in the image of a weapon that a player has techniques for may be used in place of the actual weapon. Structure Techniques cannot have Material defense tiers (unless DF). They are limited to General Effects, Extra General and Range/AoE. Full Body effect to be used when structure is covering user's body.") },
            { EFF_WDEFC, new Effect(EFF_WDEFC, true, 4, 4,
                "(CARPENTER) - Wooden blockades that provide a defense against incoming attacks, paid for according to the material tier defense costs. They last one turn by default, but this duration may be extended for an additional cost.") },
            { EFF_SIEGE, new Effect(EFF_SIEGE, true, 8, 14,
                "(CARPENTER) - These techniques are the pinnacle of a common carpenter's building expertise. They can create temporary contraptions of metal and wood in quick succession, used to serve a singular purpose. These include catapults, trebuchets, battering rams and other war machines. These creations have a base duration that follows the professional buff table and do not have a material tier.") },
            { EFF_BSUIT, new Effect(EFF_BSUIT, false, 14, 28,
                "(CARPENTER) - These techniques are the pinnacle of both, an inventors and a carpenter's building expertise. Their ingenuity can create a singular, temporary shell made mostly of metal that can be utilised for a variety of complex and precise tasks. These creations have a base duration that follows the professional buff table and do not have a material tier.") },
            { EFF_CLOUD, new Effect(EFF_CLOUD, true, 8, 8,
                "(WEATHERMANCY) - Techniques used in order to create clouds to empower other Weathermancy techniques. Clouds scale in size with rank allowing the caster a greater area to work with and control the field through their other techniques.") },
            { EFF_RAIN, new Effect(EFF_RAIN, true, 8, 8,
                "(WEATHERMANCY) - Techniques that create rain along with any other effects that may or may not be present. At lower ranks the amount of water created is not much more than a drizzle, but at higher ranks these can scale into heavy downpours or water manipulation feats similar to users of fishman karate. May be combined with elemental damage for effects such as snow, hail and acid rain.") },
            { EFF_FOGMI, new Effect(EFF_FOGMI, true, 14, 14,
                "(WEATHERMANCY) - A form of Sensory Overload that hinders vision of targets in an area.") },
            { EFF_ELEWE, new Effect(EFF_ELEWE, true, 10, 10,
                "(WEATHERMANCY) - Almost all Weathermancy techniques are elemental-based, allowing Weathermancers access to elemental techniques for the reduced cost of 10. This can include lightning, frost or even heat waves. However, unless super speed is paid for in the technique, lightning will not travel at supersonic speeds.") },
            { EFF_WIND, new Effect(EFF_WIND, true, 10, 14,
                "(WEATHERMANCY) - Blasts of wind fall under shockwave rules. They may also have effects such as Duration Damage and AOE added in order to form hurricanes.") },
            { EFF_MILCL, new Effect(EFF_MILCL, true, 8, 8,
                "(WEATHERMANCY) - Techniques used to create Sea Clouds that are sustainable in the Blue Sea. These clouds are physically dense enough to be walked upon or used to form walls or other items, but they do not have a material tier and are rather useless as weapons. At higher ranks the user may create a larger quantity of this strange cloud.") },
            { EFF_MIRCL, new Effect(EFF_MIRCL, false, 8, 28,
                "(WEATHERMANCY) - Techniques in which the user cast’s a mirage in order to make copies of themselves or something else in the vicinity. Due to the reflective nature the mirages move in unison with whatever they have replicated but of course remain intangible and cannot cause any harm even when attacking. The power of the technique indicates how accurate the mirage is. The size of objects that can be replicated scales with the minimum ranks of the AoE table.") },
            { EFF_MIRCA, new Effect(EFF_MIRCA, false, 14, 28,
                "(WEATHERMANCY) - Techniques in which the user casts a mirage in order to hide their physical presence or the presence of something else in the vicinity. Effectiveness scales with power.") },
            { EFF_SENTI, new Effect(EFF_SENTI, false, 10, 10,
                "(POP GREENS) - Plants are capable of attained a limited sentience, allowing them to attack targets and perform simple tasks. This could include biting targets within range, ensnaring them, or even something simple such as transportation and delivering messages.") },
            { EFF_FLSTR, new Effect(EFF_FLSTR, true, 8, 8,
                "(POP GREENS) - Plants can be created for use as weapons, such as bamboo spears and thorny bludgeons. They can also be made into tools, forming rafts, trampolines and other useful items. These creations have durations according to the professional buff table. Additionally, weapons created in the image of a weapon that a player has techniques for may be used in place of the actual weapon. Floral structures do not have a material tier.") },
            { EFF_WDEFP, new Effect(EFF_WDEFP, false, 4, 10,
                "(POP GREENS) - Wooden blockades that provide a defence against incoming attacks, paid for according to the material tier defence costs. They last one turn by default, but this duration may be extended for an additional cost.") },
            { EFF_ELEPG, new Effect(EFF_ELEPG, true, 14, 14,
                "(POP GREENS) - Pop Greens may be used to easily cause elemental effects, such as damaging poisons and fire. They therefore have a minimum rank of 14, rather than 28.") },
            { EFF_SMOKE, new Effect(EFF_SMOKE, false, 8, 8,
                "(STEALTH) - A rapidly dispersing gaseous substance used to reduce visibility of that which is encompassed. Usually contained within a pellet or bomb of some sort, it can mask the silhouette of one person by default but can be combined with AoE to affect larger areas.") },
            { EFF_CROWD, new Effect(EFF_CROWD, false, 8, 8,
                "(STEALTH) - The ability to blend in with a crowd of people in order to avoid detection and shake off pursuers. Becomes more effective with higher rank. Consumes 1/2 AE in order to keep active, and is subject to common sense.") },
            { EFF_SILEN, new Effect(EFF_SILEN, false, 8, 14,
                "(STEALTH) - Causing an attack or movement to produce very little sound, making it undetectable through hearing alone. More effective at higher ranks. Consumes 1/2 AE in order to keep active. This becomes a General Effect with the \"Master of Misdirection\" Trait.") },
            { EFF_SCENT, new Effect(EFF_SCENT, false, 14, 14,
                "(STEALTH) - Removing scent from one's person. Doing so can throw off detection based around scent, making a character more difficult to track. More effective at higher ranks.") },
            { EFF_DISGU, new Effect(EFF_DISGU, false, 4, 4,
                "(STEALTH) - Disguising oneself (or sometimes another), usually in the form of different clothing and make-up. Useful for infiltration. However, for a character with high fame, putting on a disguise can often not be enough to prevent them from being recognised. A disguise technique of rank less than roughly fame/4 will be less likely to convince others. Especially so if very noticeable physical traits are not masked. Keeping up a disguise does not consume upkeep.") },
            { EFF_PICKP, new Effect(EFF_PICKP, false, 8, 8,
                "(STEALTH) - The act of taking an item from another, without their consent. Pickpocketing is a tricky art that requires masterful sleight of hand, and can therefore be noticed (but not necessarily prevented) if the target is aware enough. They may realise more quickly if items of noticeable size suddenly go missing, even if not immediately. May be combined with Material Breaker effects in order to effectively remove even items attached to things.") },
            { EFF_NATCA, new Effect(EFF_NATCA, false, 14, 14,
                "(STEALTH) - Blending in to the environment through masterful knowledge of the surroundings and the perception of others. At lower ranks, this skill may be used to avoid detection in areas that provide a natural camouflage; shadows, night and foliage to name a few. Consumes 1/2 AE in order to keep active. With the \"Master of Misdirection\" Trait, any environment is treated as a Natural Camoflauge.") },
            { EFF_OPECA, new Effect(EFF_OPECA, false, 28, 28,
                "(STEALTH) - Blending in to the environment through masterful knowledge of the surroundings and the perception of others. At this high rank, it becomes possible to blend in seemingly with mere open space. Consumes 1/2 AE in order to keep active.") },
            { EFF_SECON, new Effect(EFF_SECON, false, 4, 4,
                "A cost when using more than two General Effects, paid each time an extra secondary general effect is added.") }
		};

        // Getter function of the Effects dictionary, taking into account of bonuses
        // Assume good input
        static public Effect getEffect(string name, bool markPri, bool invenPri, bool mastMisd) {
            if (markPri) {
                // Marksman Primary
                switch (name) {
                    case EFF_SHORT:
                        return new Effect(EFF_SHORT, false, 0, 0, SHORT_DESC);
                    case EFF_MEDIU:
                        return new Effect(EFF_MEDIU, false, 4, 4, MEDIU_DESC);
                    case EFF_LONG:
                        return new Effect(EFF_LONG, false, 8, 16, LONG_DESC);
                    case EFF_VLONG:
                        return new Effect(EFF_VLONG, false, 16, 44, VLONG_DESC);
                    default:
                        break;
                }
            }
            if (invenPri) {
                // Inventor Primary
                switch (name) {
                    case EFF_SHAOE:
                        return new Effect(EFF_SHAOE, false, 0, 0, SHAOE_DESC);
                    case EFF_MDAOE:
                        return new Effect(EFF_MDAOE, false, 8, 8, MDAOE_DESC);
                    case EFF_LOAOE:
                        return new Effect(EFF_LOAOE, false, 16, 28, LOAOE_DESC);
                    default:
                        break;
                }
            }
            if (mastMisd) {
                switch (name) {
                    case EFF_OPECA:
                        return new Effect(EFF_OPECA, false, 14, 14,
                            "(STEALTH) - Blending in to the environment through" +
                            " masterful knowledge of the surroundings and the perception" +
                            " of others. At this high rank, it becomes possible to blend in" +
                            " seemingly with mere open space. Consumes 1/2 AE in order to keep" +
                            " active.");
                    case EFF_SILEN:
                        return new Effect(EFF_SILEN, true, 8, 14,
                            "(STEALTH) - Causing an attack or movement to produce very" +
                            " little sound, making it undetectable through hearing alone." +
                            " More effective at higher ranks. Consumes 1/2 AE in order to keep" + 
                            " active. This becomes a General Effect with the \"Master of Misdirection\" Trait.");
                    default:
                        break;
                }
            }
            try { return effectDict[name]; }
            catch { return null; }
        }

        // Getter function of a string Array of all Effects acessible
        static public List<string> getAccessEffects() {
            List<string> effList = new List<string>() {
                EFF_MELEE,
                EFF_SHORT,
                EFF_MEDIU,
                EFF_LONG,
                EFF_VLONG,
                EFF_SHAOE,
                EFF_MDAOE,
                EFF_LOAOE,
                EFF_DISPL,
                EFF_DISOR,
                EFF_GATLI,
                EFF_DEFLE,
                EFF_RICOC,
                EFF_UNPRE,
                EFF_DMGTC,
                EFF_DISAR,
                EFF_SHOCK,
                EFF_CURVE,
                EFF_OMNID,
                EFF_HAKEN,
                EFF_ELDMG,
                EFF_FLAVR,
                EFF_SPRIT,
                EFF_QUICK,
                EFF_PIERC,
                EFF_AFTER,
                EFF_ADAFT,
                EFF_CONST,
                EFF_REVER,
                EFF_DURAT,
                EFF_DISAB,
                EFF_SENSI,
                EFF_SENMU,
                EFF_LASER,
                EFF_DEFBY,
                EFF_SPEBL,
                EFF_STBRE,
                EFF_MIBRE,
                EFF_HIBRE,
                EFF_ARHAK,
                EFF_ELCOA,
                EFF_FULLB,
                EFF_STDEF,
                EFF_MIDEF,
                EFF_HIDEF,
                EFF_SMOKE,
                EFF_CROWD,
                EFF_DISGU,
                EFF_PICKP,
                EFF_TRAPS
            };
            return effList;
        }

        // Getter function of a string Array of Stealth Effects acessible
        static public List<string> getStealthEffects() {
            List<string> effList = new List<string>() {
                EFF_SILEN,
                EFF_SCENT,
                EFF_NATCA,
                EFF_OPECA
            };
            return effList;
        }

        // Getter function of a string Array of Weathermancy Effects acessible
        static public List<string> getWeatherEffects() {
            List<string> effList = new List<string>() {
                EFF_CLOUD,
                EFF_RAIN,
                EFF_FOGMI,
                EFF_ELEWE,
                EFF_WIND,
                EFF_MILCL,
                EFF_MIRCL,
                EFF_MIRCA
            };
            return effList;
        }

        // Getter function of a string Array of Pop Greens Effects acessible
        static public List<string> getPopGreensEffects() {
            List<string> effList = new List<string>() {
                EFF_SENTI,
                EFF_FLSTR,
                EFF_WDEFP,
                EFF_ELEPG
            };
            return effList;
        }

        // Getter function of a string Array of Doctor Effects acessible
        static public List<string> getDoctorEffects() {
            List<string> effList = new List<string>() {
                EFF_DOPIN,
                EFF_BANDA,
                EFF_STITC,
                EFF_SPLIN,
                EFF_SALVE,
                EFF_ANTIV
            };
            return effList;
        }

        // Getter function of a string Array of Carpetner Effects acessible
        // Excluding "Battle Suits"
        static public List<string> getCarpenterEffects() {
            List<string> effList = new List<string>() {
                EFF_STRUC,
                EFF_WDEFC,
                EFF_SIEGE,
            };
            return effList;
        }

        // -------------------------------------------------------------------
        // BUFF/DEBUFFS
        // -------------------------------------------------------------------
        #region Buffs/Debuffs Consts
        public const string BUF_WILLPO = "Willpower",
            BUF_STANCE = "Stances",
            BUF_LIFRET = "Life Return",
            BUF_POISON = "Poison",
            BUF_DRUG = "Drug",
            BUF_CRITHI = TR_CRITHI,
            BUF_ANASTR = TR_ANASTR,
            BUF_QUICKS = TR_QUICKS,
            BUF_PERFOR = "Performance",
            BUF_FOOD = "Food (Long AoE)",
            BUF_ROKUOG = "Rokuogan",
            BUF_ZOAN = "Zoan",
            BUF_DFBUFF = "Other DF (Buff)",
            BUF_DFDEBU = "Other DF (Debuff)",
            BUF_OBHAKI = "Observation Haki",
            BUF_CQHAKI = "King's Haki";
        #endregion

        static private Dictionary<string, StatAlter> statDict = new Dictionary<string, StatAlter>() {
            { BUF_WILLPO, new StatAlter(StatAlter.BUFF, true, false, false) },
            { BUF_STANCE, new StatAlter(StatAlter.STANCE, false, true, false) },
            { BUF_LIFRET, new StatAlter(StatAlter.STANCE, false, true, false) },
            { BUF_POISON, new StatAlter(StatAlter.DEBUFF, false, true, true) },
            { BUF_DRUG, new StatAlter(StatAlter.BUFF, true, true, true) },
            { BUF_CRITHI, new StatAlter(StatAlter.DEBUFF, false, true, true) },
            { BUF_ANASTR, new StatAlter(StatAlter.DEBUFF, false, true, true) },
            { BUF_QUICKS, new StatAlter(StatAlter.DEBUFF, false, true, true) },
            { BUF_PERFOR, new StatAlter(StatAlter.BUFF, true, true, false) },
            { BUF_FOOD, new StatAlter(StatAlter.BUFF, false, true, true) },
            { BUF_ROKUOG, new StatAlter(StatAlter.DEBUFF, false, true, true) },
            { BUF_ZOAN, new StatAlter(StatAlter.BUFF, true, false, false) },
            { BUF_DFBUFF, new StatAlter(StatAlter.BUFF, true, true, true) },
            { BUF_DFDEBU, new StatAlter(StatAlter.DEBUFF, false, true, true) },
            { BUF_OBHAKI, new StatAlter(StatAlter.BUFF, true, true, false) },
            { BUF_CQHAKI, new StatAlter(StatAlter.DEBUFF, false, true, true) }
        };

        // Getter function of the StatAlter dictionary
        static public StatAlter getStatAlter(string name) {
            try { return statDict[name]; }
            catch { return null; }
        }
    }
}
