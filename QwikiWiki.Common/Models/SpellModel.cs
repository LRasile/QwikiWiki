using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace QwikiWiki.Common.Models
{
    public class SpellModel
    {
        public int Id { get; set; }
        public string Name { get; set; }// Tides of blood
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        //public string AdditionalInfo { get; set; }
        public string Cost { get; set; }// 70/80/90/100/110 mana
        //public List<string> Stats { get; set; }// Heal Per Second : 100/150/200(+ 60% AP)
        public string Cooldown { get; set; }// 9/8/7/6/5
        public string Range { get; set; }// 1250
        public bool IsAoe { get; set; }
        public int AimType { get; set; }
        public int CrowdControl { get; set; }
        public int DamageType { get; set; }
    }

    //public enum AimType
    //{
    //    None = 0,
    //    ClickOn = 1 ,
    //    Self = 2,
    //    SkillShot = 3
    //}

    //public enum CrowdControl
    //{
    //    None,
    //    Slow,
    //    Silence,
    //    Snare,
    //    Stun,
    //    Entangle,
    //    Polymorph,
    //    KnockUp,
    //    Fear,
    //    KnockBack 
    //}

    //public enum DamageType
    //{
    //    None,
    //    Physical,
    //    Magic,
    //    True
    //}
}