using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QwikiWiki.Common.Dtos;
using QwikiWiki.Common.LocalDtos;

namespace QwikiWiki.Common.Models
{
    public class SpellModelBuilder
    {
        public SpellModel BuildFromSpellDto(SpellDto spellDto)
        {
            SpellModel model = new SpellModel();


            model.Name = spellDto.name;
            model.Range = spellDto.rangeBurn;
            model.Cooldown = spellDto.cooldownBurn;
            model.Cost = spellDto.costBurn + " " + spellDto.costType;
            model.ImgUrl = spellDto.image.full;

            model.Description = spellDto.sanitizedTooltip;

            AddEffectBurns(spellDto, model);

            AddVariables(spellDto, model);

            if (model.Description.IndexOf("{{") > 0)
            {
                model.Description = RepairDescription(model.Description, spellDto);
                AddEffectBurns(spellDto, model);
            }

            model.Description = AddAbbr(model.Description);

            return model;
        }

        private string AddAbbr(string description)
        {
            description = description.Replace("bonusattackdamage", "Bonus AD");
            description = description.Replace("spelldamage", "AP");
            description = description.Replace("attackdamage", "AD");
            description = description.Replace("maxmana", "Max. Mana");
            return description;
        }

        private void AddVariables(SpellDto spellDto, SpellModel model)
        {
            if (spellDto.vars != null && spellDto.vars.Length > 0)
            {
                foreach (Variables variable in spellDto.vars)
                {
                    //need to include type of scaling
                    string variableCode = string.Format("{{{{ {0} }}}}", variable.key);
                    string coeffAndType = string.Format("{0} {1}", CoeffToString(variable.coeff), variable.link);
                    model.Description = model.Description.Replace(variableCode, coeffAndType);
                }
            }
        }

        private static void AddEffectBurns(SpellDto spellDto, SpellModel model)
        {
            if (spellDto.effectBurn != null)
            {
                for (int i = 0; i < spellDto.effectBurn.Length; i++)
                {
                    string effectCode = string.Format("{{{{ e{0} }}}}", i);
                    model.Description = model.Description.Replace(effectCode, spellDto.effectBurn[i]);
                }  
            }
        }

        private string CoeffToString(float[] coeffs){
            string result = "";
            foreach (float coeff in coeffs){
                result = result + coeff + "/";
            }
            result = result.Substring(0,result.Length-1);
            return result;
        }

        public SpellModel BuildFromPassiveDto(PassiveDto passiveDto)
        {
            SpellModel model = new SpellModel();

            model.Description = passiveDto.SanitizedDescription;
            model.Name = passiveDto.Name;

            return model;
        }

        /// <summary>
        /// This is because rito cant follow their own api rules GG WP
        /// </summary>
        /// <param name="p"></param>
        /// <param name="spellDto"></param>
        /// <returns></returns>
        private string RepairDescription(string description, SpellDto spellDto)
        {
            string effectCode = "{{{{ e{0} }}}}";
            string fCode = "{{{{ f{0} }}}}";

            //Aatrox W
            if (spellDto.name == "Blood Thirst / Blood Price")
            {
                description = description.Replace(string.Format(fCode, "4"), "15 / 23.75 / 32.5 / 41.25 / 50 (+0.25 bonusattackdamage)");
                description = description.Replace(string.Format(fCode, "5"), "(+0.5 bonusattackdamage)");
            }

            //Aatrox R
            if (spellDto.name == "Massacre")
            {
                description = description.Replace(string.Format(effectCode, "7"), string.Format(effectCode, "6"));
            }

            //Ahri W
            if (spellDto.name == "Fox-Fire")
            {
                description = description.Replace(string.Format(fCode, "1"), string.Format(effectCode, "3") + "(+ 0.64 spelldamage)");
            }

            //Ashe Q
            if (spellDto.name == "Ranger's Focus")
            {
                description = description.Replace(string.Format(fCode, "1"), string.Format(effectCode, "5") + "Attack damage");
            }

            //Azir W
            if (spellDto.name == "Arise!")
            {
                description = description.Replace(string.Format(fCode, "1"), "(45 + 5 / 10 at each level)");
                description = description.Replace(string.Format(fCode, "2"), "12 / 11 / 10 / 9 / 8");
                description = description.Replace(string.Format(fCode, "4"), "(50 + 10 × Azir's level)");
            }

            //Azir E
            //TODO Smells iffy Emperor 
            if (spellDto.name == "Shifting Sands")
            {
                description = description.Replace("(+{{ f1 }})", "");
            }
            
            //Bard W
            if (spellDto.name == "Caretaker's Shrine")
            {
                description = description.Replace("Active Shrines: {{ f1 }} / {{ f2 }}", "");
            }

            //Brand Q
            if (spellDto.name == "Sear")
            {
                description = description.Replace(string.Format(fCode, "1"), "2");
            }

            //Braum Q
            if (spellDto.name == "Winter's Bite")
            {
                description = description.Replace("(+{{ f1 }}) ", "");
            }

            //Cassiopeia E
            if (spellDto.name == "Twin Fang")
            {
                description = description.Replace(string.Format(fCode, "2"), "0.55 spelldamage");
                description = description.Replace("({{ f3 }})", "");
            }

            //Corki Q
            if (spellDto.name == "Phosphorus Bomb")
            {
                description = description.Replace(string.Format(fCode, "1"), "0.5 bonusattackdamage");
            }

            //Mundo R
            if (spellDto.name == "Sadism")
            {
                description = description.Replace("{{ f1 }} health", "");
            }

            //Evelynn Q
            //TODO bonus physical damage doesnt look too right on her 
            if (spellDto.name == "Hate Spike")
            {
                description = description.Replace(string.Format(fCode, "2"), "(+ 0.35 / 0.40 / 0.45 / 0.50 / 0.55 spelldamage) ");
            }

            //Ezreal Q
            if (spellDto.name == "Mystic Shot")
            {
                description = description.Replace(string.Format(fCode, "3"), "1.1 attackdamage");
            }

            //Ganagplank Q
            if (spellDto.name == "Parrrley")
            {
                description = description.Replace("Total Gold Plundered: {{ f2 }} gold.", "");
            }

            //Garen E
            if (spellDto.name == "Judgment")
            {
                description = description.Replace("(+{{ f1 }})", "damage");
            }

            //Gnar Q
            if (spellDto.name == "Boomerang Throw / Boulder Toss")
            {
                description = description.Replace(string.Format(fCode, "1") + "%", "45% + (5% × GNAR!'s rank)");
            }

            //Gnar W
            if (spellDto.name == "Hyper / Wallop")
            {
                description = description.Replace(string.Format(fCode, "1") + "%", "30% + (15% × GNAR!'s rank)");
            }

            //Gnar E
            if (spellDto.name == "Hop / Crunch")
            {
                description = description.Replace("(+{{ f1 }})", "");
            }

            //Gragas E
            if (spellDto.name == "Body Slam")
            {
                description = description.Replace(string.Format(fCode, "1"), "3");
            }

            //Kalista E
            if (spellDto.name == "Rend")
            {
                description = description.Replace(string.Format(fCode, "1"), "(+ 0.20 / 0.225 / 0.25 / 0.275 / 0.30 attackdamage)");
            }

            //Kassadin R
            if (spellDto.name == "Riftwalk")
            {
                description = description.Replace(string.Format(fCode, "1"), "0.02 maxmana");
                description = description.Replace(string.Format(fCode, "2"), "0.02 maxmana");
            }

            //Kha'Zix Q
            if (spellDto.name == "Taste Their Fear")
            {
                description = description.Replace(string.Format(fCode, "3"), string.Format(effectCode,"6"));
                description = description.Replace(string.Format(fCode, "2"), "(+ 2.6 bonusattackdamage) (+ 10  × Kha'Zix's level)");
            }

            //Master Yi E
            if (spellDto.name == "Wuju Style")
            {
                description = description.Replace(string.Format(fCode, "1"), "");
            }

            //Miss Fortune Q
            if (spellDto.name == "Double Up")
            {
                description = description.Replace(string.Format(fCode, "1"), "0.85 attackdamage");
                description = description.Replace(string.Format(fCode, "2"), "1 attackdamage");
            }

            //Miss Fortune W
            if (spellDto.name == "Impure Shots")
            {
                description = description.Replace(string.Format(fCode, "1"), "0.06 attackdamage");
                description = description.Replace("{{ f3 }} times for a maximum of {{ f2 }} bonus Magic damage.", "5 + (1 × Bullet Time's rank) times per target.");
            }

            //Nami W
            if (spellDto.name == "Ebb and Flow")
            {
                description = description.Replace(string.Format(fCode + "%", "1"), "-15% (+ 7.5% per 100 AP)");
            }

            //Nunu Q
            if (spellDto.name == "Consume")
            {
                description = description.Replace(string.Format(fCode, "3"), string.Format(effectCode, "5"));
                description = description.Replace(string.Format(fCode, "4"), string.Format(effectCode, "6"));
                description = description.Replace(string.Format(fCode, "5"), string.Format(effectCode, "4"));
            }

            //Nunu R
            if (spellDto.name == "Absolute Zero")
            {
                description = description.Replace(string.Format(fCode, "2"), "78.125 / 109.375 / 140.625 (+ 0.3125 spelldamage)");
            }

            //Rek'sai W
            if (spellDto.name == "Burrow / Un-burrow")
            {
                description = description.Replace(string.Format(fCode, "1"), string.Format(effectCode, "3"));
            }

            //Rek'sai E
            if (spellDto.name == "Furious Bite / Tunnel")
            {
                description = description.Replace(string.Format(fCode, "1"), string.Format(effectCode, "1") + " AD");
                description = description.Replace(string.Format(fCode, "2"), "1.6/1.8/2.0/2.2/2.4 attackdamage");
            }

            //Renekton Q
            //TODO Very broken HALP!
            if (spellDto.name == "Cull the Meek")
            {
                description = description.Replace(string.Format(fCode, "3"), "");
                description = description.Replace(string.Format(fCode, "4"), "");
                description = description.Replace(string.Format(fCode, "5"), "");
                description = description.Replace(string.Format(fCode, "6"), "");
            }

            //Renekton W
            if (spellDto.name == "Ruthless Predator")
            {
                description = description.Replace(string.Format(fCode, "3"), "2.25 attackdamage");
            }

            //Rengar Q
            if (spellDto.name == "Savagery")
            {
                description = description.Replace(string.Format(fCode, "2"), "30 - 240 (+ 0.5 attackdamage)");
                description = description.Replace("(" + string.Format(fCode, "3") + ")", "");
                description = description.Replace(string.Format(fCode, "4"), "50 - 102%");
            }

            //Rengar W
            if (spellDto.name == "Battle Roar")
            {
                description = description.Replace(string.Format(fCode, "1"), "12 - 80");
                description = description.Replace(string.Format(fCode, "2"), "40 - 250");
                description = description.Replace(string.Format(fCode, "3"), "75 - 500");
            }

            //Rengar E
            if (spellDto.name == "Bola Strike")
            {
                description = description.Replace(string.Format(fCode, "1"), "50 - 340");
            }

            //Ryze Q
            if (spellDto.name == "Overload")
            {
                description = description.Replace(string.Format(fCode, "1"), string.Format(effectCode,"2") + " maxmana");
            }

            //Ryze W
            if (spellDto.name == "Rune Prison")
            {
                description = description.Replace(string.Format(fCode, "1"), string.Format(effectCode, "3") + " maxmana");
            }

            //Ryze E
            if (spellDto.name == "Spell Flux")
            {
                description = description.Replace(string.Format(fCode, "1"), string.Format(effectCode, "4") + " maxmana");
                description = description.Replace(string.Format(fCode, "2"), string.Format(effectCode, "5") + " maxmana");
            }

            //Sion W
            if (spellDto.name == "Soul Furnace")
            {
                description = description.Replace(string.Format(fCode, "2"), "");
            }

            //Sona Q
            if (spellDto.name == "Hymn of Valor")
            {
                description = description.Replace(string.Format(fCode, "1"), string.Format(effectCode, "1"));
            }

            //Sona W
            if (spellDto.name == "Aria of Perseverance")
            {
                description = description.Replace(string.Format(fCode, "1"), string.Format(effectCode, "3") + " spelldamage");
                description = description.Replace(string.Format(fCode, "2"), string.Format(effectCode, "1"));
            }

            //Sona E
            //TODO investigate
            if (spellDto.name == "Song of Celerity")
            {
                description = description.Replace(string.Format(fCode, "2"), "(+ 7.5% per 100 AP) (+ 2% x Crescendo's rank)");
                description = description.Replace(string.Format(fCode, "3"), string.Format(effectCode, "1"));
                description = description.Replace(string.Format(fCode, "4"), string.Format(effectCode, "2"));
                description = description.Replace(string.Format(fCode, "5"), "(+ 3.5% per 100 AP) (+ 2% x Crescendo's rank)");
            }

            //Soraka W
            if (spellDto.name == "Astral Infusion")
            {
                description = description.Replace(string.Format(fCode, "1"), string.Format(effectCode, "4"));
            }

            //Taric Q
            if (spellDto.name == "Imbue")
            {
                description = description.Replace(string.Format(fCode, "1"), "");
            }

            //Thresh E
            if (spellDto.name == "Flay")
            {
                description = description.Replace("{{ f1 }}-{{ f2 }}", "bonus");
            }

            //Tristana E
            if (spellDto.name == "Explosive Charge")
            {
                description = description.Replace(string.Format(fCode, "1"), "50 / 65 / 80 / 95 / 110% bonus AD");
            }

            //Twitch E
            if (spellDto.name == "Contaminate")
            {
                description = description.Replace(string.Format(fCode, "1"), "25% bonus AD");
            }

            //Urgot W
            if (spellDto.name == "Terror Capacitor")
            {
                description = description.Replace(string.Format(fCode, "1"), "");
            }

            //Vikor Q
            if (spellDto.name == "Siphon Power")
            {
                description = description.Replace(string.Format(fCode, "3"), "0.2 spelldamage");
                description = description.Replace(string.Format(fCode, "2"), "20 - 210 (+1 attackdamage)");
            }

            //Xerath W
            if (spellDto.name == "Eye of Destruction")
            {
                description = description.Replace(string.Format(fCode, "1"), "90 / 135 / 180 / 225 / 270");
                description = description.Replace(string.Format(fCode, "2"), "0.9 spelldamage");
            }

            //Zed W
            if (spellDto.name == "Living Shadow")
            {
                description = description.Replace(string.Format(fCode, "3"), "");
            }

            //Zyra Q
            if (spellDto.name == "Deadly Bloom")
            {
                description = description.Replace(string.Format(fCode, "3"), "23 + (6.5 × Zyra's level)");
            }

            //Zyra E
            if (spellDto.name == "Grasping Roots")
            {
                description = description.Replace(string.Format(fCode, "3"), "23 + (6.5 × Zyra's level)");
            }

            return description;

        }
    }
}
