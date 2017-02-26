/*
 * Database that stores information of Rules
 * Traits, Rokushiki, Effects
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
        // TRAITS
        // -------------------------------------------------------------------
        static private Dictionary<string, Trait> traitDict = new Dictionary<string, Trait>() {
            #region Traits Database
            { "\'Tis But A Scratch!", new Trait("\'Tis But A Scratch!", 0, 1, "Weapon specialists are often used to fighting against other weapon specialists in close combat. Thus their chances of being injured by such weapons increase drastically compared to others. These particular characters are used to fighting so much under those conditions that they can actually ignore the effects of pain much better than their sissy counterparts and fight on.")},
            { "[SPEC] Mastery", new Trait("[SPEC] Mastery", 0, 1, "The character has attained mastery in one specific weapon or form of attack that they have Martial Proficiency (can make techniques greater than rank 14) with. All techniques using this weapon or form of attack are always treated as being four ranks higher for purposes of calculating their technique rank benchmark, though their actual rank is not changed. This benefit ceases to apply for techniques higher than rank 28.")},
            { "Advanced [SPEC] Class Mastery", new Trait("Advanced [SPEC] Class Mastery", 0, 1, "The character has expanded their mastery from a single weapon or form of attack to encompass a broader selection. They now receive the benefit of their ____ Mastery or Advanced ____ Mastery to a second type of Weapon or form of attack. Additionally, they gain special technique points which may be used to create attacks that use both weapons or forms of attack at once, in the amount of 25% of their fortune.")},
            { "Advanced [SPEC] Mastery", new Trait("Advanced [SPEC] Mastery", 0, 2,"The character has attained supreme mastery in one specific weapon or form of attack that they have Martial Proficiency (can make techniques greater than rank 14) with. All techniques using this weapon or form of attack are always treated as being four ranks higher for purposes of calculating their technique rank benchmark, though their actual rank is not changed.")},
            { "Advanced Cyborg", new Trait("Advanced Cyborg", 3, 0, "This is a high-tech cyborg, rarely encountered anywhere but the Grand Line. A character must be within the Grand Line to access this kind of technology. Advanced Cyborgs begin with up to 12M Beli worth of modifications. They are made of steel and may be constructed of materials up to titanium in strength, and may have additional body parts added. Their size may vary from their original form at a cost. Each arm may conceal two weapons and each leg may conceal one. Additionally, they may externally mount cybernetics/weapons at this stage. Cyborgs of this type may have multiple special systems, though moderators may enforce a limit eventually. Advanced Cyborgs may also purchase additional fuel charges up to a limit of 5. Upgrades from Basic Cyborg modifications are maintained and do not count against Advanced maximums.\nnew Trait(" +
                "[list][*]Appendages - 4.5M Beli/per\n" +
                "[*]Additional Body Parts – 6M Beli/per\n" +
                "[*]Torso - 9M Beli\n" +
                "[*]Titanium upgrade at 60% of the sum of cybernetic body parts\n" +
                "[*]Additional Weapon Slots - 2M Beli/per\n" +
                "[*]Additional Fuel Charges - 10M Beli/per[/list]")},
            {"Advanced Stance Mastery", new Trait("Advanced Stance Mastery", 0, 1, "Through intense practice, the character has perfected their stance to a much greater degree. The character's stance techniques now only require a penalty to other stats equal to 50% of the total buff (fractional values rounded up for the penalty).")},
            {"Anatomical Strike", new Trait("Anatomical Strike", 0, 1, "By using their extensive knowledge of the human anatomy,  these doctors can aim specifically for areas that will cause very serious,  even fatal wounds. The character can use up to a quarter of their Technique Points in techniques which hit vital points in the body. Striking vital points allows characters to do damage and cause a stat debuff related to the area struck.")},
            {"Ansatsuken", new Trait("Ansatsuken", 0, 1, "The character has become so adept at striking vital areas and pressure points that they may do so passively in the course of their normal attacks. Any time the character lands a blow on an enemy, they may apply the one of their critical hit/quick strike technique debuffs. This debuff is half as strong as normal, but otherwise functions exactly like a normal critical hit/quick strike debuff, in terms of stacking and duration.")},
            {"Anti-Stealth", new Trait("Anti-Stealth", 0, 1, "Having become masters of stealth themselves, these thieves/assassins are capable of knowing when their opponents are using the very same tactics they specialise in. The detection techniques of these characters are treated as if they are four ranks higher for purposes of effectiveness.")},
            {"Anti-Weapon Specialist", new Trait("Anti-Weapon Specialist", 0, 1, "Through training and experience in fighting armed opponents when unarmed, the character gains special Technique Points, of the amount of half their fortune, for techniques which are aimed to disarm, disable or block the use of a weapon.")},
            {"Art of Stealth", new Trait("Art of Stealth", 0, 1, "After a lifetime of learning how to survey an area's or targets' weak points, this assassin has a particularly effective set of skills for the purposes of stealth. All stealth techniques are treated as if they are four ranks higher for purposes of effectiveness.")},
            {"Awakened Haki", new Trait("Awakened Haki", 4, 0, "The character has awakened their dormant ambition and is able to utilise this mysterious force in combat. They may specialise in one of the two basic colors (The Color of Armaments or The Color of Observation)or a Dormant Specialization. For their specialist color, the character's Haki maximum rank is 1/2 Fortune. For any other color, the maximum rank is 1/4 Fortune. They are also capped at rank 44 for their Specialist color techniques, and rank 28 for any other colors.")},
            {"Baking Bad", new Trait("Baking Bad", 0, 1, "Experienced both in the ways of science and the cook, the character has taken their skills to the next level by combining them. They may now make chef techniques that can be used in combat to apply debuffs to their enemies simply by contacting them and work the same as Poison debuffs. They may also make kitchen-related gadgets.")},
            {"Basic Cyborg", new Trait("Basic Cyborg", 1, 0, "This is the most basic level of cyborg, and the most commonly encountered type in the blues. A Basic Cyborg begins with up to 3M Beli worth of modifications. Their bodies are made out of Iron and do not vary from their original size. They may have one concealed weapon per arm or leg, and one basic system per other appendage. Note that these weapons or systems are very basic.\n" +
                "[list][*]Appendages - 1.5M Beli/per\n" +
                "[*]Torso - 3M Beli[/list]")},
            {"Bear Stamina", new Trait("Bear Stamina", 1, 0, "This character's stamina is boosted by 20%.")},
            {"Beast Handler", new Trait("Beast Handler", 1, 0, "Characters with this trait have a natural affinity for getting along with animals. Wild animals tend to behave either neutrally or friendly towards the character, unless they are very strongly aggressive. These animals, with proper approach and care, may be made to be merely distrustful and may sometimes refrain from attacking. The user also gains one NPC pet, who may possess humanlike intelligence, behaves as a crew NPC and is controlled by the player of the main character.")},
            {"Bizarrely Durable", new Trait("Bizarrely Durable", 0, 1, "Durability sometimes reaches the point where it goes so far past the point of excessiveness that it becomes truly bizarre. Passing the rare state of partial permanent hardening and the unusual state of completely permanent hardening, these characters have surpassed what could be considered reasonable for any human being in terms of natural staying power. As such, their natural armor is treated the same as that of zoans and fishmen and can be upgraded with a standalone technique.")},
            {"Born Leader", new Trait("Born Leader", 1, 0, "Characters with this trait are gifted with the ability to command respect and admiration from others. Sentient NPCs are easier to sway over to the side of those with this trait. They may gain information, calm down the angry, and sometimes even placate hostile adversaries. Additionally, those normally friendly to strangers find themselves easily enamoured with the character. The user also gains one NPC henchman, who behaves as a crew NPC and is controlled by the player of the main character. They are exceedingly loyal to their ‘master’.")},
            {"Brilliant Mind", new Trait("Brilliant Mind", 0, 1, "Creative and with an incredible knack for making all sorts of strange gadgets, these inventors gain special Technique Points which can only be used on creation techniques for weapons and gadgets, of the amount of half their fortune.")},
            {"Civilian Lawyer", new Trait("Civilian Lawyer", 0, 1, "These silver tongued devils have managed to work their ability to talk people into making the best financial deals for themselves into a practical ability. They are able to more easily influence those around them with their words.")},
            {"Combat Cuisine", new Trait("Combat Cuisine", 0, 1, "Learning to adapt his food for rapid service on the battlefield, the Chef is able to increase the beneficial effects gained by those who eat it, albeit temporarily. A chef may use any cooking technique during combat, as if it were a Doctor's medicine, doubling the total value of the stat bonus for the duration. All other chef buffs on the target are over-written while this is active, but resume when it expires.")},
            {"Conquering King Haki", new Trait("Conquering King Haki", 1, 0, "Those who chose to have a Dormant Specialization are able to take Conquerering King's Haki at 200 SD earned. This will allow them to overpower weaker enemies and combat their foes with sheer force of will. They will specialize in the Color of the Conqueror.")},
            {"Cooking Fighter", new Trait("Cooking Fighter", 0, 1, "Experts in the use of various kitchen utensils, a chef may become proficient enough in the use of their cookware as weapons in battle. A chef with this trait gains martial proficiency with one form of cookware, as well as special technique points equal to one quarter of their fortune that can be used on martial techniques involving the chosen utensil.")},
            {"Critical Hit", new Trait("Critical Hit", 0, 1, "Through training in how to hit vital points in the body, the character can use up to a quarter of their Technique Points in techniques which hit vital points in the body. Striking vital points allows characters to do damage and cause a stat debuff related to the area struck.")},
            {"Crowd Control", new Trait("Crowd Control", 0, 1, "Due to the nature of their skills, their performances will affect basically anyone who can see and hear them. Buff techniques used by these entertainers will have the same maximum effect as if they were affecting a group of targets one crowd tier smaller than the size of the crowd the technique was intended to effect.")},
            {"Cybertronic Genius", new Trait("Cybertronic Genius", 0, 1, "These inventors have studied their cybernetics especially well, and pay 20% less to upgrade their own Cyborg parts.")},
            {"Dazzling Performer", new Trait("Dazzling Performer [SPEC]", 0, 1, "These entertainers are so skilled in their chosen field that they can use their performance skills to actually bring about changes in others. These entertainers gain special Technique Points that can only be used on their performance support techniques, of the amount of half their Fortune.")},
            {"Disciplined Haki", new Trait("Disciplined Haki", 2, 0, "The character has trained and disciplined their ambition, allowing them to unlock their true potential. They are no longer limited by Rank caps, and their non-specialist maximum rank is 1/3 Fortune.")},
            {"Dwarf", new Trait("Dwarf", 2, 0, "Dwarves are extremely small humanoids with fluffy tails. Dwarves gain the benefits of the Mighty Strength trait and have a mastery benefit on all their movement techniques of +4 ranks. Their small sizes makes them harder to hit but they also take more damage when hit. They tend to be extremely gullible.")},
            {"Escape Artist", new Trait("Escape Artist", 0, 1, "Being a wily little bugger, the thief is a master of escaping from tight spots and difficult situations. Any debuff to the thief's speed is halved in duration, and they can easily escape most forms of restraint, such as being grappled, shackled, tied, etc.")},
            {"Fate of the Cunning", new Trait("Fate of the Cunning", 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your accuracy stat.")},
            {"Fate of the Emperor", new Trait("Fate of the Emperor", 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated) to your fortune score.")},
            {"Fate of the Mighty", new Trait("Fate of the Mighty", 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your stamina stat.")},
            {"Fate of the Strong", new Trait("Fate of the Strong", 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your strength stat.")},
            {"Fate of the Swift", new Trait("Fate of the Swift", 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. In the meantime, you get a temporary boost of (# of traits in fated, multiplied by 3) to your speed stat.")},
            {"Fate of the Whimsical", new Trait("Fate of the Whimsical", 3, 0, "By placing traits in Fated, you lock them until the next time you earn a trait. You may put as many traits as you have into fated. Unlike the other Fate traits, these place-holder traits may be cashed-in and replaced with other traits without having to reach the next trait mark.")},
            {"Fishman [SPEC]", new Trait("Fishman [SPEC]", 2, 0, "Fishmen gain the benefits of the Mighty Strength trait, and may breath underwater. Their movements in water are not hampered as greatly as normal humans, and they may swim at great speeds. They also gain attributes or natural weapons based on their species. The number of traits needed depends on the species.")},
            {"Flawless Accuracy", new Trait("Flawless Accuracy", 3, 0, "This character's accuracy is boosted by 60%.")},
            {"Food of Warriors", new Trait("Food of Warriors", 0, 1, "Years and years of experience in preparing the finest of meals, using all the right ingredients has led to these chefs being able to create foods which will make those who eat it stronger, faster, and better. They gain special Technique Points that can only be used on this food, of the amount of half their fortune.")},
            {"Forge Furnace", new Trait("Forge Furnace", 0, 1, "After spending that much time welding weapons and having to put up with the extreme temperatures of the furnace, the character gains immunity to heat-based attacks similar to having an iron body. Any heat-based attack that wouldn’t damage iron won’t harm them.")},
            {"Forging Master", new Trait("Forging Master", 0, 1, "All the experience the character has gained in making weapons allows them to produce them more cheaply. These smiths get a 20% discount for any weapons and armor they make for their own personal use.")},
            {"From the Shadows", new Trait("From the Shadows", 0, 1, "Unlike others who have merely adopted the dark, the thief/assassin was born in it, molded by it. The thief/assassin gains special technique points equal to half their fortune that can be used to create Camouflage techniques, or any technique hides or otherwise obscures the presence of the character.")},
            {"Funky Body", new Trait("Funky Body", 1, 0, "Whether gained by birth, training, modification by themselves or another person, prolonged exposure to something, or plain dumb luck, this character has a body that is less than typical. They have a single biological or physical difference to their bodies, canon examples including Kuramarimo's ability to rapidly grow and detach static afros and Don Chinjao's abnormally hard and pointy skull.")},
            {"Giant Stamina", new Trait("Giant Stamina", 3, 0, "This character's stamina is boosted by 60%.")},
            {"Great Speed", new Trait("Great Speed", 1, 0, "This character's speed is boosted by 20%.")},
            {"Grandmaster", new Trait("Grandmaster", 0, 2, "This character has achieved a legendary skill with his weapon/weapon class/form of attack. They are masters of their art beyond question, and are able to perform supernatural feats using their skill alone. Grandmaster ____'s mastery techniques allow them to cut/hit fluid and insubstantial substances, such as air, fire, water, or even light.")},
            {"Guru", new Trait("Guru", 1, 0, "This character gains special technique points equal to 1/2 of their fortune, which can only be used on Life Return techniques.")},
            {"Haggler of the Sea", new Trait("Haggler of the Sea", 0, 1, "There are financially crafty people out there and these carpenters are at the top of the game when it comes to the ship market. These carpenters are capable of negotiating the cost of 4 ranks off of any ship technique they may make for their ships. (Ship Techniques will be special technologies that a ship utilizes that can be quantified as a technique a sudden boost in speed, a weapon such as the Gaon cannon.")},
            {"Hammer of Rage", new Trait("Hammer of Rage", 0, 1, "Knowing how to make things also means knowing how to them. Some smiths come to revel in this destructive aspect of their trade even more than usual, becoming masters in ing down anything with a form. The character is able to freeform ing objects of up to iron hardness, and can er harder substances as if they were one tier lower in terms of technique rank. They remain unable to destroy unable quality materials, but may temporarily damage them (disabled for the duration of that SL) instead of breaking them.")},
            {"Hardened Fighter", new Trait("Hardened Fighter", 0, 1, "Through excessive training in ing planks, punching rocks, and all those durability-building exercises, the character’s arms, up to their elbows, and their legs, up to their knees, have gained flesh that is as hard as iron, making them much more resistant to physical damage.")},
            {"Horticultural Warfare", new Trait("Horticultural Warfare", 3, 0, "The character has become accustomed to using pop green seeds and as such is now able to utilise these seeds to bloom various flowers nearly instantly. The character can use these plants to make anything varying from a plant net to a large man-eating flower.")},
            {"In-Training", new Trait("In-Training", 0, 1, "This is the equivalent of Fated for the character’s Professional Skills, but no paths exist for this. General traits may not be spent to purchase In-Training.")},
            {"Jack of All Trades", new Trait("Jack of All Trades", 0, 1, "The character is able to master multiple skills, and has become adept at a number of things. This character gains two additional secondary professions.")},
            {"John of All Trades", new Trait("John of All Trades", 0, 2, "The character is able to master multiple skills, and has become adept at a number of things. This character gains three additional secondary professions.")},
            {"Keen Accuracy", new Trait("Keen Accuracy", 1, 0, "This character's accuracy is boosted by 20%.")},
            {"Lamp, Oil, Rope", new Trait("Lamp, Oil, Rope", 0, 1, "Certain minds have a better handle on ing things down than building them up. These inventors specialize in demolitions, and gain special technique points equal to a half of their fortune that may be spent on creation techniques for bombs and munitions.")},
            {"Life Return", new Trait("Life Return", 0, 2, "The character has the ability to use the mystic ability known as Life Return. They gain control over their bodily functions through their thoughts. Growing and controlling their hair, changing their body fat, body muscles and at higher levels even their bloodflow, these characters are truly masters of their own body and have access to special buffs.")},
            {"Living Stone", new Trait("Living Stone", 0, 1, "Through excessive training of their bodies they have gained a great deal of natural durability, these character's entire bodies can stand up to frightening amounts of abuse. As a result, they have gained flesh that is as hard as iron, making them much more resistant to physical damage.")},
            {"Ludicrous Speed", new Trait("Ludicrous Speed", 3, 0, "This character's speed is boosted by 60%.")},
            {"Mad Scientist", new Trait("Mad Scientist", 0, 1, "The epitome of madness. Cackling and gleeful hand-rubbing is a favourite pastime of this character, and every time they finish an experiment, there are obligatory flashes of lightning. The mad scientist has the ability to make one major biological modification to themselves or any NPCs they control.")},
            {"Mammoth Stamina", new Trait("Mammoth Stamina", 2, 0, "This character's stamina is boosted by 40%.")},
            {"Master of Misdirection", new Trait("Master of Misdirection", 0, 1, "Those who practice stealth to depths far greater than most, becoming far more proficient than even others of their trade. These characters may treat any environment as Natural Camouflage, and the Silent effect becomes a General Effect.")},
            {"Medical Malpractice", new Trait("Medical Malpractice", 0, 1, "Part of knowing what is required to keep somebody healthy is knowing what will cause others to become unhealthy. Doctors who put this knowledge to practical use may make especially potent poisons. They gain special Technique Points which can only be used on toxins that harm or debuff, of the amount of half their fortune.")},
            {"Merfolk", new Trait("Merfolk", 1, 0, "In place of legs these characters have a tail which splits down the middle while they are standing on land (For this RP's purposes, this is the at any age, not just 30+). They may breathe underwater and gain the Great Speed trait whilst submerged, and their movements underwater are completely unhindered. Spending too long out of water can fatigue them somewhat. In addition, merfolk can talk to most types of fish, but not seakings.")},
            {"Mighty Strength", new Trait("Mighty Strength", 1, 0, "This character's strength is boosted by 20%.")},
            {"Monster Strength", new Trait("Monster Strength", 3, 0, "This character's strength is boosted by 60%.")},
            {"Natural Armour", new Trait("Natural Armour", 0, 1, "Through all the time spent in the forge, working on weapons, the character’s arms, up to their elbows, and their legs, up to their knees, have gained flesh that is literally as tough as iron, making them much more resistant to physical damage.")},
            {"New World Cyborg", new Trait("New World Cyborg", 5, 0, "These cyborgs represent the very pinnacle of scientific achievement in the world of cybernetics, being by right more machine than human. A character must be in either the Grand Line or New World to access this kind of technology. They can be made of materials up to titanium in strength but may have built-in armor made from custom-materials. Alongside this, these cyborgs are given up to 21M Beli worth of modifications. New World Cyborgs pay 30% to upgrade weaker materials. These Cyborgs are able to create body systems that provide personal flight, self-repair, fire energy projectiles and more.\n" +
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
                "The special thing about all of these systems is that they are able to be input into the cyborg’s internal systems. An Inventor with access to R44 techniques can still build a laser, however they must buy a holder for the laser that will cost them money.")},
            {"Performance Assistant", new Trait("Performance Assistant", 0, 1, "Plenty of good entertainers need someone to help them carry out their routine. The character gains a special Performance Assistant NPC which is under their control.")},
            {"Pickpocket", new Trait("Pickpocket", 0, 1, "As a thief, the character is skilled at obtaining money by less-than-acceptable means. Thus, they will always earn 20% more Beli in their Storylines.")},
            {"Poison Killer", new Trait("Poison Killer", 0, 1, "With some basic training in the use of poisons to kill or debuff their targets, these assassins gain special Technique Points that can only be used on poisons, of the amount of half their fortune.")},
            {"Powerful Speaker", new Trait("Powerful Speaker", 0, 1, "This entertainer is particularly skilled at inspiring the masses. These entertainers are capable of, with a simple phrase, increasing the fighting abilities of their allies. These buffs last much shorter than normal but consume no AE and only need to be said once for it to come into effect, as opposed to a sustained 'performance'. They utilize Personal Buff measurements (see Techniques page)in terms of buff potency and have a duration rank/4 posts. This impromptu buff takes up a Personal Buff slot.")},
            {"Prop Performance", new Trait("Prop Performance", 0, 1, "An entertainer with this trait has learned how to integrate one of their performing props into their fighting style. They gain the ability to make martial techniques involving one item above rank 14. Additionally, they may perform freeform attacks with this item while performing at no additional expense of action economy.")},
            {"Quickstrike", new Trait("Quickstrike", 0, 1, "Because of their training in making stealth attacks and hitting vital organs, the character can use up to a quarter of their Technique Points in techniques which hit vital points in the body. Striking vital points allows characters to do damage and cause a stat debuff related to the area struck.")},
            {"Rare Find", new Trait("Rare Find", 0, 1, "A keen merchant always has his eyes and ears open for an exceptional deal. At the end of every SL, the character will be offered a rare and valuable item determined at random (weapons, armour, devices, dials, materials, etc.)This item will have double their normal discount applied if it is purchased.")},
            {"Rokushiki Master", new Trait("Rokushiki Master", 1, 0, "The character has completely mastered Rokushiki. Through vigorous training of pushing their body to the utmost limit, they are now able to make their own custom variations on any Rokushiki Technique. Additionally they gain access to the most powerful Rokushiki technique of all, Rokuogan.")},
            {"Rokushiki Savant", new Trait("Rokushiki Savant", 1, 0, "The character has a natural talent for Rokushiki techniques and as such, gains Special Technique Points equal to half their fortune to spend on their Rokushiki techniques.")},
            {"Siege Warfare", new Trait("Siege Warfare", 0, 1, "Carpenters after years of dedication to their craft have become masters of designing impressive war machines. They can create such beasts of war as catapults, battering rams, and various other tools of war quickly in the middle of a fight even. They gain Special Technique Points equal to half their Fortune for this purpose.")},
            {"Signature Technique", new Trait("Signature Technique", 1, 0, "This trait makes one particular technique special, a trademark move of the character. This signature technique is always of the natural maximum rank that the character is allowed, but does not cost any Technique Points. In addition, these techniques are allowed to have weaker variations, though these variations must follow the same path set by the main technique, and cannot be used to branch to techniques that do not branch into the main technique.")},
            {"Skill in Upgrades", new Trait("Skill in Upgrades", 0, 1, "These fellows are amazing at what they do, such that they can do it for a much lower cost. All technological upgrades that they make to weapons get a 20% discount. This discount applies only to the inventor themselves, and not weapons made for others.")},
            {"Skilled Use of Medicines", new Trait("Skilled Use of Medicines", 0, 1, "Because of their expertise, these doctors have become extremely adept at producing their own medicines to aid others. They gain special Technique Points which can only be used on medicines that aid or buff, of the amount of half their fortune.")},
            {"Sleight of Hand", new Trait("Sleight of Hand", 0, 1, "\"Always watch the hands\" is the rule when dealing with a professional thief such as this. These thieves are so good at stealing that they gain special TP equal to half their fortune for techniques that involve stealing the items and weaponry off their target even during combat.")},
            {"Sonic Speed", new Trait("Sonic Speed", 2, 0, "This character's speed is boosted by 40%.")},
            {"Specific Devil Fruit", new Trait("Specific Devil Fruit", 3, 0, "The player may choose a specific Devil Fruit, either canon or custom, for their character. These fruits are limited based on moderator discretion but can be of any of the three types.")},
            {"Stance Mastery", new Trait("Stance Mastery", 0, 1, "The character has mastered the use their stance techniques. All stances the character are always treated as being four ranks higher for purposes of calculating their technique rank benchmark and numerical benefit, though their actual rank is not changed.")},
            {"Strong Spirit", new Trait("Strong Spirit", 1, 0, "The character gains an amount of special technique points equal to 1/2 of their fortune that may only be used on Haki techniques of the colour they are specialized in.")},
            {"Super Sense", new Trait("Super Sense", 1, 0, "This character has achieved mastery over their heightened senses, gaining a single sense on par with Observation Haki. They gain special Technique Points that can only be used on this sense, of the amount of a quarter of their fortune.")},
            {"Super Strength", new Trait("Super Strength", 2, 0, "This character's strength is boosted by 40%.")},
            {"Superb Accuracy", new Trait("Superb Accuracy", 2, 0, "This character's accuracy is boosted by 40%.")},
            {"Squad Leader", new Trait("Squad Leader", 2, 0, "Characters with this trait are especially gifted with the ability to command respect and admiration from others. In combat, their fame mooks act a single group and are able to be used in techniques just like a trait NPC would. When using difference creatures/species for mooks, the number of mooks should be equivalent to human mooks in strength.")},
            {"Technical Mastery", new Trait("Technical Mastery", 2, 0, "Increases total technique points by 100% of your fortune score, but does not stack with Technically Adept (this benefit overrides the previous benefit). Requires Technically Adept and 100 SD earned.")},
            {"Technically Adept", new Trait("Technically Adept", 1, 0, "Increases total technique points by 40% of your fortune score.")},
            {"That Extra Special Ingredient", new Trait("That Extra Special Ingredient", 0, 1, "This is a chef who has traveled around the world searching for only the finest of ingredients to add their their foods. These ingredients are capable of giving profound supernatural effects to the consumer (for example, jalapenos that bestow fire breath).")},
            {"Top Gun", new Trait("Top Gun", 0, 1, "These marksmen are top of the line when it comes to being able to draw their weapon and fire. These characters are also always reloading their gun reflexively faster than those around them notice thus they never have to actually deal with taking the time to reload at a critical moment in battle.")},
            {"Tough as Nails", new Trait("Tough as Nails", 0, 1, "Through all the manual labour that they have done, the character’s arms, up to their elbows, and their legs, up to their knees, have gained flesh that is literally as tough as iron, making them much more resistant to physical damage.")},
            {"Tough Bargainer", new Trait("Tough Bargainer", 0, 1, "Ever the successful businessman, merchants know how to extract money from people and to keep as much as they can from leaving their pocket. Thus, they will always earn 20% more Beli in their Storylines.")},
            {"Training of the Kitchen", new Trait("Training of the Kitchen", 0, 1, "After spending so long in front of the open fires in the kitchen, the character gains immunity to heat-based attacks similar to having an iron body. Any heat-based attack that wouldn’t damage iron won’t harm them.")},
            {"Transcendent Haki", new Trait("Transcendent Haki", 1, 0, "The character has become exceptionally proficient in both their specialist and non-specialist haki. Their non-specialist haki no longer has a maximum rank. ")},
            {"Treasure Hunter", new Trait("Treasure Hunter", 0, 1, "With their knack for discovering hidden items that don’t necessarily belong to them, these thieves are very skilled in looking for treasures. Thus, in their SLs, they will always get an extra little item. It could be something quirky, or it could be something rare.")},
            {"Uncivil Engineering", new Trait("Treasure Hunter", 0, 1, "With their knack for discovering hidden items that don’t necessarily belong to them, these thieves are very skilled in looking for treasures. Thus, in their SLs, they will always get an extra little item. It could be something quirky, or it could be something rare.")},
            {"Weak Point Sighted", new Trait("Weak Point Sighted", 0, 1, "With all their experience in building things and taking them apart, these characters are able to pinpoint the weaknesses in architecture and can easily figure out how to do the most damage to weapons, ships, buildings and other large constructions.")},
            {"Weathermancy", new Trait("Weathermancy", 3, 0, "After the learning of skills of weather manipulation and creation, the character may - through the use of tools and gadgets - actively perform feats such as summoning lightning bolts, hail, rain, etc. They require an weapon/item to serve as a conduit for such techniques.")},
            {"NONE", new Trait("NONE", 0, 0, "BLANK")} // Default Blank
            #endregion
        };

        // Getter function of the Trait dictionary
        // Assume good input
        static public Trait getTrait(string name) {
            try { return traitDict[name]; }
            catch { return traitDict["NONE"]; }
        }

        // -------------------------------------------------------------------
        // ROKUSHIKI
        // -------------------------------------------------------------------
        static private Dictionary<string, Rokushiki> rokuDict = new Dictionary<string, Rokushiki>() {
            #region Rokushiki Database
            { "Shigan", new Rokushiki("Shigan", 22, "Offensive", "Melee", 4, 0, 0, 0, 0,
                "This technique is an improved melee strike. The damage done by the user's finger (or other small appendage) is treated as both blunt and piercing, generally using the more favourable of the two damage types when the other would not be as effective. Additionally, the attack is quick and nearly invisible to the naked eye, executed at bullet-like speeds. (Damage Type Change, Piercing, Super-Speed).") },
            { "Rankyaku", new Rokushiki("Rankyaku", 22, "Offensive", "Long", 8, 0, 0, 0, 0,
                "By kicking fast enough, the user literally \"cuts\" their air with their foot. The projectile is different from standard ranged melee, possessing a keen cutting edge and unusual longevity. A Rankyaku shockwave will continue moving until it reaches the edge of it's range or impacts with an obstacle harder than stone. (Damage Type Change, Piercing, Shockwave, Long Range)") },
            { "Soru", new Rokushiki("Soru", 28, "Support", "Self", 0, 0, 0, 0, 0,
                "Once per post, an individual can use this technique to put on momentary bursts of speed that make them invisible to the untrained naked eye. The user may move to any location that they could normally move to in a single post, but they do so near-instantaneously. During this movement, they cannot be seen by anybody who's accuracy is less than double this technique's rank, appearing as a blurred streak to anybody who's accuracy is above that point. If attacked while moving by a foe who's speed is less than this technique's rank, the attack can be passively dodged without any expenditure of Focus. Attacks cannot be made while moving with Soru. (Super-Speed, Perception Formula)") },
            { "Kami-E", new Rokushiki("Kami-E", 32, "Defensive", "Self", 20, 0, 0, 0, 0,
                "Using instinct-driven, reflexive movements and loosening up all of their muscles, the user dodges an attack much the way a piece of paper in the breeze does. While in use, this technique reduces the effective accuracy of any targeted attack made against the user by a value equal to this technique's power. If this would reduce the accuracy of that attack to 0, the attack can be passively dodged without any expenditure of Focus. Attacks can not be made while using Kami-E. (Paperlike, Fullbody, Dodging Formula)") },
            { "Tekkai", new Rokushiki("Tekkai", 32, "Defensive", "Self", 22, 0, 0, 0, 0,
                "By tensing their muscles and focusing, the user of this technique may drastically reduce the damage they take from physical attacks. While this technique is active, the user is treated as if their body has steel-level defenses. Bladed and piercing attacks that are not capable of cutting through steel do blunt damage for the technique's duration, and all blunt damage suffered while this technique is active is significantly reduced. The user of this technique may not move while it is active. (Mid Tier Material, Full Body)") },
            { "Geppo", new Rokushiki("Geppo", 36, "Support", "Self", 0, 0, 0, 0, 0,
                "By kicking off of the air itself, the user of this technique can seemingly fly or \"Air Walk.\" The user of this technique may remain airborn indefinitely, but must land in the following post if they attack or are attacked (if there is no land within range, they spend the next post falling until they can resume use of Geppo). If the technique's user is significantly encumbered, or if they lack use of one of their legs, they must land periodically, being able to sustain flight with this technique for no more than three consecutive rounds. (Personal 'Flight')") },
            { "Rokuogan", new Rokushiki("Rokuogan", 44, "Offensive", "Melee", 20, 0, 0, -22, -22,
                "Using principles of all six Rokushiki techniques, this technique executes a devastating close-range attack. Though it must be used on a target no more than a yard from the user, this technique creates a shockwave that bypasses almost any and all forms of physical defense, doing internal damage to it's victim as if they were unarmoured and made of ordinary flesh and blood (though it does not bypass some devil fruit abilities on it's own). Additionally, the shock of this attack is particularly incapacitating, weakening the target's constitution significantly. (Inbuilt Critical Hit, Defense Bypassing)") },
            { "None", new Rokushiki("None", 0, "None", "None", 0, 0, 0, 0, 0, "None") }
            #endregion
        };

        // Getter function of Rokushiki dictionary
        static public Rokushiki getRoku(string name) {
            try { return rokuDict[name]; }
            catch { return rokuDict["None"]; }
        }

        // -------------------------------------------------------------------
        // EFFECTS
        // -------------------------------------------------------------------
        static private Dictionary<string, Effect> effectDict = new Dictionary<string, Effect>() {
			#region Effects Database
			{"Displacement", new Effect("Displacement", true, 8, 8,
                "Common examples are knockback techniques. The effect of a displacement technique can be greater at higher ranks. A stronger opponent will be displaced less by a weaker character using a displacement technique on them.")},
            {"Disorient", new Effect("Disorient", true, 8, 8,
                "Techniques which unbalance or use visual tricks to hinder opponents. Effectiveness increases the higher the rank.")},
            {"Gatling", new Effect("Gatling", true, 8, 8,
                "A series of rapid attacks performed quickly. Each individual strike is weaker but the greater amount of attacks make them harder to avoid. Higher ranked gatlings have even more strikes.")},
            {"Deflecting", new Effect("Deflecting", true, 8, 8,
                "This is applicable for Defensive Type techniques only. Techniques which divert the direction of an attack. Different from Reversal techniques which attempt to send an opponent's attack back at them. With higher ranks things like bullets and even elements can be deflected.")},
            {"Ricochet", new Effect("Ricochet", true, 8, 8,
                "Techniques which use walls or other surfaces to bounce projectiles off. Higher ranks can have more complex ricochets, multiple 'bounces' etc.")},
            {"Unpredictable", new Effect("Unpredictable", true, 8, 14,
                "Techniques which even the user cannot anticipate the outcome of. Encompasses the realm of indirect attacks and others. The results are dependent on the situation they are used in. As the user does not know the outcome, anticipation techniques such as observation haki are less effective versus them.")},
            {"Damage Type Change", new Effect("Damage Type Change", true, 4, 14,
                "A punch that cuts or a blade with the sharp edge of a pillow. Higher ranked versions can have multiple damage types or supernatural types etc.")},
            {"Disarm", new Effect("Disarm", true, 8, 8,
                "Disable techniques which target a weapon or item of an opponent to temporarily remove or steal it in combat. Requires traits like Anti-Weapon Specialist. Higher rank has higher chance of success with respect to RP considerations.")},
            {"Shockwaves", new Effect("Shockwaves", true, 10, 14,
                "Ranged melee techniques generated by striking the air with a fist/sword swing etc. The cost of the effect is paid by the range table below. Larger shockwaves will also need to pay AoE.")},
            {"Curving Projectiles", new Effect("Curving Projectiles", true, 14, 20,
                "Bullets, arrows, shockwaves and other projectile techniques that do not follow a straight path but instead move in the air. At higher ranks, they can perform more unlikely turns in the air though the path they follow is always per-determined when fired.")},
            {"Omni-directional", new Effect("Omni-directional", true, 10, 20,
                "\"Perfect-sphere\" or techniques that attack/defend in all directions surrounding the user. They have a built in melee AoE around the user and pay range increases from the AoE table below. ")},
            {"Haki Enhancement", new Effect("Haki Enhancement", true, 12, 12,
                "User coats an invisible suit of armour around a pair of limbs or a weapon. When added to techniques at Rank 28 and beyond, they gain the ability to strike a devil fruit user's real body.")},
            {"Elemental Damage", new Effect("Elemental Damage", true, 14, 14,
                "Adds the benefits of a single element to a technique. If created through practical means (ie Inventions, Matches etc), the minimum rank is Rank 14. If created by shonen science or will, minimum rank is Rank 28. Can be generated in greater volumes at higher ranks.")},
            {"Flavor", new Effect("Flavor", true, 0, 0,
                "Technique flair that does not grant any advantage other than making things look fancier. The higher the rank, the more exotic.")},
            {"Spirit Generated Illusions", new Effect("Spirit Generated Illusions", true, 14, 28,
                "Eye catching illusions that have significance, lasting only momentarily in battle but can shock or intimidate foes. Asura is a good example for these effects.")},
            {"Secondary", new Effect("Secondary", false, 4, 4,
                "A cost when using more than two General Effects, paid each time an extra secondary general effect is added.")},
            {"Speed", new Effect("Speed", false, 4, 4,
                "Techniques that focus more on striking an opponent as quickly as possible rather than with sheer force thus they often don't hit as hard as techniques of similar rank but come out faster.")},
            {"Piercing", new Effect("Piercing", false, 8, 8,
                "Techniques that hold great penetrative force. These attacks are able to pass through an object with ease assuming they are not equivalent to a tier material. With each thing these attacks pass through they lose a significant amount of power.")},
            {"After-Image", new Effect("After-Image", false, 8, 14,
                "A singular after-image that looks like the user. The after image repeats an action performed at high speed by the user though at a slower pace to be perceived normally. It cannot do damage, nor has any physical form and will disperse after 1 post or after it is hit. The power of these techniques improves the authenticity of the after-image.")},
            {"Additional After-Image", new Effect("Additional After-Image", false, 4, 18,
                "NOTE: Requires After-Image as an effect!\nAllows the technique to gain an extra after-image. This effect can stack for each additional payment.")},
            {"Reversal", new Effect("Reversal", false, 16, 16,
                "This is applicable for Defensive Type techniques only. Similar but not limited to an upgraded version of deflecting. Reversal techniques are all attempts to repel a form of attack back at an opponent. With higher ranks, more obscure or supernatural techniques can be reversed.")},
            {"Duration Damage", new Effect("Duration Damage", false, 8, 8,
                "Encompasses all damage over time abilities. Deep cuts that cause bleeding or venom that cause persistent pain. Higher ranks can include other effects that persist for several turns.")},
            {"Disables", new Effect("Disables", false, 14, 14,
                "Techniques which target a specific part of the body to cripple temporarily, whether through dislocation or numbing etc.")},
            {"Sensory Overload (Single)", new Effect("Sensory Overload (Single)", false, 14, 14,
                "Disable techniques which target a specific sense or multiple senses. Examples can be blinding flashes or deafening noises.")},
            {"Sensory Overload (Multiple)", new Effect("Sensory Overload (Multiple)", false, 30, 30,
                "Disable techniques which target a specific sense or multiple senses. Examples can be blinding flashes or deafening noises.")},
            {"Super-Speed", new Effect("Super-Speed", false, 28, 28,
                "Techniques which move at speeds that do not fall under the Speed Statistic such as Lasers or Soru. They are not impossible to avoid, the more Speed/Accuracy, the less difficulty it will be with dealing with these types of attacks and abilities though they will always be perceived as blurred attacks or narrowly avoided.")},
            {"Defense Bypassing", new Effect("Defense Bypassing", false, 28, 28,
                "Techniques which bypass Defensive Type Techniques by transferring damage through blocks on contact or creating shockwaves that attack an opponent internally.")},
            {"Specific Type Block", new Effect("Specific Type Block", false, 28, 28,
                "Block techniques with special properties to defend against a specific type of attack. They significantly reduce damage against the chosen type regardless of the power behind them.")},
            {"Starter Tier Breaking", new Effect("Starter Tier Breaking", false, 14, 14,
                "A technique capable of breaking a single weapon or piece of armour of Starter Tier Material (i.e. Iron).")},
            {"Mid Tier Breaking", new Effect("Mid Tier Breaking", false, 28, 28,
                "A technique capable of breaking ta single weapon or piece of armour of Medium Tier Material (i.e. Steel).")},
            {"High Tier Breaking", new Effect("High Tier Breaking", false, 44, 44,
                "A technique capable of breaking a single weapon or piece of armour of High Tier Material (i.e. Titanium).")},
            {"Armaments Haki", new Effect("Armaments Haki", false, 6, 14,
                "User creates a coat of hardened armour around a pair of limbs or a weapon. Offers passive defensive benefit equal to technique power. At Rank 28 and beyond, Armaments Haki gains the passive ability to strike a devil fruit user's real body whilst this buff is active on them.")},
            {"Elemental Coating", new Effect("Elemental Coating", false, 14, 28,
                "User has developed a technique to generate and bind a type of element to a pair of limbs or a weapon. Grants the passive benefits of the element, and a passive offensive benefit equal to technique power.")},
            {"Full Body Effects", new Effect("Full Body Effects", false, 10, 20,
                "The cost of extending an effect of a technique from limbs to the entire body. Effects such as Tekkai, Armaments Haki amongst others use this.")},
            {"Starter Tier Defense", new Effect("Starter Tier Defense", false, 4, 10,
                "Reduces damage of Rank 13 and below techniques. NOTE THAT 1) Tier Defenses may require some special abilities, 2) Wearable armor requires Techniques with this Effect, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.")},
            {"Mid Tier Defense", new Effect("Mid Tier Defense", false, 12, 22,
                "Reduces damage of Rank 27 and below techniques. NOTE THAT 1) Tier Defenses may require some special abilities, 2) Wearable armor requires Techniques with this Effect, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.")},
            {"High Tier Defense", new Effect("High Tier Defense", false, 24, 36,
                "Reduces damage of Rank 43 and below techniques. NOTE THAT 1) Tier Defenses may require some special abilities, 2) Wearable armor requires Techniques with this Effect, and 3) Legendary Tier offers the defensive properties of High Tier (still unbreakable). Please refer to the Rules for more details.")},
            {"Melee", new Effect("Melee", false, 0, 0,
                "Range of direct physical combat.")},
            {"Short", new Effect("Short", false, 4, 4,
                "Range of a few meters.")},
            {"Medium", new Effect("Medium", false, 8, 28,
                "Range of half of a sport's stadium.")},
            {"Long", new Effect("Long", false, 16, 44,
                "Range of an entire sport's stadium.")},
            {"Very Long", new Effect("Very Long", false, 32, 66,
                "Range of a small city.")},
            {"Short AoE", new Effect("Short AoE", false, 8, 8,
                "A few meters in diameter.")},
            {"Medium AoE", new Effect("Medium AoE", false, 16, 28,
                "Half of a sport's stadium in diameter.")},
            {"Long AoE", new Effect("Long AoE", false, 32, 44,
                "An entire sport's stadium in diameter.")},
            {"Doping", new Effect("Doping", false, 4, 14,
                "(DOCTOR) - Doping allows characters to use pills, injections, or other similar means to effectively ignore injury for a set duration in order to allow their bodies to fight at full strength while they are in effect. As this is very taxing on the body, doping techs have a cool down equal to duration. The effectiveness of doping on a target halves after each use within the same topic regardless of the rank or source of the original doping technique used.")},
            {"Bandaging", new Effect("Bandaging", true, 4, 4,
                "(DOCTOR) - Bandages fix everything, at least to a certain extent. When applied, bandages function as a retroactive block on par with a block of similar power. Bandaging does not regenerate or work instantaneously, with excessive exertion running the risk of wounds reopening. ")},
            {"Stitches", new Effect("Stitches", false, 28, 28,
                "(DOCTOR) - Stitches are intended to undo cutting damage by means of suturing wounds quickly. In this case, the doctor performs the sutures with speed and effectiveness, making them much more effective than those typically used in free form. As such, they are treated as a specific block for cutting damage applied retroactively. ")},
            {"Splints", new Effect("Splints", false, 28, 28,
                "(DOCTOR) - Splints are used to remedy the effects of blunt damage to bodies, including fractures and bruising. Due to a high amount of skill, these splints have supernatural effectiveness, and are treated as a specific block for blunt damage applied retroactively.")},
            {"Salves", new Effect("Salves", false, 28, 28,
                "(DOCTOR) - Salves are special creams that are used to alleviate damage that is elemental in nature. They are able to effectively treat the likes of burns, shocks, and freezing, and are very effective in that regard, being treated as a specific block for elemental damage applied retroactively.")},
            {"Anti-Venom", new Effect("Anti-Venom", false, 28, 28,
                "(DOCTOR) - Anti-venoms are special medicines intended to purge the effects of damaging poisons from the affected individual. While they are not effective on debuffing poisons, anti-venoms are able to treat all other poisons incredibly well, being treated as a specific block for poison damage applied retroactively.")},
            {"Traps", new Effect("Traps", false, 4, 4,
                "(CARPENTER) - Traps come in all kinds of shapes and sizes, ranging from a simple mouse trap to even more elaborate contraptions such as swinging log traps, or booby-trapped household property. These creations are made to hinder and restrict, but are considered tierless in terms of their defenses. Higher ranks can have more complex set-ups, remote triggers or include more dangerous elements and be generally more difficult to detect.")},
            {"Structures", new Effect("Structures", true, 4, 4,
                "(CARPENTER) - Carpenters can craft different kinds of furniture for supportive purposes, such as chairs and tables. They can also make ladders, form temporary rafts, trampolines and other useful items that would normally need to be purchased. These creations have a base duration that follows the professional buff table. Additionally, weapons created in the image of a weapon that a player has techniques for may be used in place of the actual weapon. Structure Techniques cannot have Material defense tiers (unless DF). They are limited to General Effects, Extra General and Range/AoE. Full Body effect to be used when structure is covering user's body.")},
            {"Wooden Defenses", new Effect("Wooden Defenses", true, 4, 4,
                "(CARPENTER) - Wooden blockades that provide a defense against incoming attacks, paid for according to the material tier defense costs. They last one turn by default, but this duration may be extended for an additional cost.")},
            {"Siege Weapons", new Effect("Siege Weapons", true, 8, 14,
                "(CARPENTER) - These techniques are the pinnacle of a common carpenter's building expertise. They can create temporary contraptions of metal and wood in quick succession, used to serve a singular purpose. These include catapults, trebuchets, battering rams and other war machines. These creations have a base duration that follows the professional buff table and do not have a material tier.")},
            {"Battle Suits", new Effect("Battle Suits", false, 14, 28,
                "(CARPENTER) - These techniques are the pinnacle of both, an inventors and a carpenter's building expertise. Their ingenuity can create a singular, temporary shell made mostly of metal that can be utilised for a variety of complex and precise tasks. These creations have a base duration that follows the professional buff table and do not have a material tier.")},
            {"Cloud", new Effect("Cloud", true, 8, 8,
                "(WEATHERMANCY) - Techniques used in order to create clouds to empower other Weathermancy techniques. Clouds scale in size with rank allowing the caster a greater area to work with and control the field through their other techniques.")},
            {"Rain", new Effect("Rain", true, 8, 8,
                "(WEATHERMANCY) - Techniques that create rain along with any other effects that may or may not be present. At lower ranks the amount of water created is not much more than a drizzle, but at higher ranks these can scale into heavy downpours or water manipulation feats similar to users of fishman karate. May be combined with elemental damage for effects such as snow, hail and acid rain.")},
            {"Fog/Mist", new Effect("Fog/Mist", true, 14, 14,
                "(WEATHERMANCY) - A form of Sensory Overload that hinders vision of targets in an area.")},
            {"Elemental Damage (Weather)", new Effect("Elemental Damage (Weather)", true, 10, 10,
                "(WEATHERMANCY) - Almost all Weathermancy techniques are elemental-based, allowing Weathermancers access to elemental techniques for the reduced cost of 10. This can include lightning, frost or even heat waves. However, unless super speed is paid for in the technique, lightning will not travel at supersonic speeds.")},
            {"Wind", new Effect("Wind", true, 10, 14,
                "(WEATHERMANCY) - Blasts of wind fall under shockwave rules. They may also have effects such as Duration Damage and AOE added in order to form hurricanes.")},
            {"Milky Cloud", new Effect("Milky Cloud", true, 8, 8,
                "(WEATHERMANCY) - Techniques used to create Sea Clouds that are sustainable in the Blue Sea. These clouds are physically dense enough to be walked upon or used to form walls or other items, but they do not have a material tier and are rather useless as weapons. At higher ranks the user may create a larger quantity of this strange cloud.")},
            {"Mirage (Clones)", new Effect("Mirage (Clones)", false, 8, 28,
                "(WEATHERMANCY) - Techniques in which the user cast’s a mirage in order to make copies of themselves or something else in the vicinity. Due to the reflective nature the mirages move in unison with whatever they have replicated but of course remain intangible and cannot cause any harm even when attacking. The power of the technique indicates how accurate the mirage is. The size of objects that can be replicated scales with the minimum ranks of the AoE table.")},
            {"Mirage (Camouflage)", new Effect("Mirage (Camouflage)", false, 14, 28,
                "(WEATHERMANCY) - Techniques in which the user casts a mirage in order to hide their physical presence or the presence of something else in the vicinity. Effectiveness scales with power.")},
            {"Sentience", new Effect("Sentience", false, 10, 10,
                "(POP GREENS) - Plants are capable of attained a limited sentience, allowing them to attack targets and perform simple tasks. This could include biting targets within range, ensnaring them, or even something simple such as transportation and delivering messages.")},
            {"Floral Structures", new Effect("Floral Structures", true, 8, 8,
                "(POP GREENS) - Plants can be created for use as weapons, such as bamboo spears and thorny bludgeons. They can also be made into tools, forming rafts, trampolines and other useful items. These creations have durations according to the professional buff table. Additionally, weapons created in the image of a weapon that a player has techniques for may be used in place of the actual weapon. Floral structures do not have a material tier.")},
            {"Wooden Defenses", new Effect("Wooden Defenses", false, 4, 10,
                "(POP GREENS) - Wooden blockades that provide a defence against incoming attacks, paid for according to the material tier defence costs. They last one turn by default, but this duration may be extended for an additional cost.")},
            {"Elemental Damage (Pop Green)", new Effect("Elemental Damage (Pop Green)", true, 14, 14,
                "(POP GREENS) - Pop Greens may be used to easily cause elemental effects, such as damaging poisons and fire. They therefore have a minimum rank of 14, rather than 28.")},
            {"Smoke", new Effect("Smoke", false, 8, 8,
                "(STEALTH) - A rapidly dispersing gaseous substance used to reduce visibility of that which is encompassed. Usually contained within a pellet or bomb of some sort, it can mask the silhouette of one person by default but can be combined with AoE to affect larger areas.")},
            {"Crowd Blending", new Effect("Crowd Blending", false, 8, 8,
                "(STEALTH) - The ability to blend in with a crowd of people in order to avoid detection and shake off pursuers. Becomes more effective with higher rank. Consumes 1/2 AE in order to keep active, and is subject to common sense.")},
            {"Silent", new Effect("Silent", false, 8, 14,
                "(STEALTH) - Causing an attack or movement to produce very little sound, making it undetectable through hearing alone. More effective at higher ranks. Consumes 1/2 AE in order to keep active. This becomes a General Effect with the \"Master of Misdirection\" Trait.")},
            {"Scentless", new Effect("Scentless", false, 14, 14,
                "(STEALTH) - Removing scent from one's person. Doing so can throw off detection based around scent, making a character more difficult to track. More effective at higher ranks.")},
            {"Disguise", new Effect("Disguise", false, 4, 4,
                "(STEALTH) - Disguising oneself (or sometimes another), usually in the form of different clothing and make-up. Useful for infiltration. However, for a character with high fame, putting on a disguise can often not be enough to prevent them from being recognised. A disguise technique of rank less than roughly fame/4 will be less likely to convince others. Especially so if very noticeable physical traits are not masked. Keeping up a disguise does not consume upkeep.")},
            {"Pickpocket", new Effect("Pickpocket", false, 8, 8,
                "(STEALTH) - The act of taking an item from another, without their consent. Pickpocketing is a tricky art that requires masterful sleight of hand, and can therefore be noticed (but not necessarily prevented) if the target is aware enough. They may realise more quickly if items of noticeable size suddenly go missing, even if not immediately. May be combined with Material Breaker effects in order to effectively remove even items attached to things.")},
            {"Natural Camouflage", new Effect("Natural Camouflage", false, 14, 14,
                "(STEALTH) - Blending in to the environment through masterful knowledge of the surroundings and the perception of others. At lower ranks, this skill may be used to avoid detection in areas that provide a natural camouflage; shadows, night and foliage to name a few. Consumes 1/2 AE in order to keep active. With the \"Master of Misdirection\" Trait, any environment is treated as a Natural Camoflauge.")},
            {"Open Camouflage", new Effect("Open Camouflage", false, 28, 28,
                "(STEALTH) - Blending in to the environment through masterful knowledge of the surroundings and the perception of others. At this high rank, it becomes possible to blend in seemingly with mere open space. Consumes 1/2 AE in order to keep active.")},
            {"Error", new Effect("Error", true, 0, 0, "Report Bug")}
			#endregion 
		};

        // Getter function of the Effects dictionary
        // Assume good input
        static public Effect getEffect(string name) {
            try { return effectDict[name]; }
            catch { return effectDict["Error"]; }
        }

    }
}
