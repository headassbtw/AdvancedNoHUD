using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedNoHUD.CustomTypes
{
    public enum whereHUD
    {
        LIV = 1,
        HMD = 2,
        Pause = 3
    }
    public enum HUDelement
    {
        combo,
        score,
        rank,
        multiplier,
        progress,
        health
    }

    internal struct HudElements
    {
        public bool combo;
        public bool score;
        public bool rank;
        public bool multiplier;
        public bool progress;
        public bool health;

        public void SetAll(bool combo, bool score, bool rank, bool multiplier, bool progress, bool health)
        {
            this.combo = combo;
            this.score = score;
            this.rank = rank;
            this.multiplier = multiplier;
            this.progress = progress;
            this.health = health;
        }
        
        public HudElements(bool combo, bool score, bool rank, bool multiplier, bool progress, bool health)
        {
            this.combo = combo;
            this.score = score;
            this.rank = rank;
            this.multiplier = multiplier;
            this.progress = progress;
            this.health = health;
        }
    }

    

    internal struct LocationPreset
    {
        public whereHUD where;
        public HudElements elements;
        public bool everything;

        public void AllEnabled (bool yes)
        {
            this.everything = yes;
        }
        public void Where(whereHUD w)
        {
            this.where = w;
        }

        public LocationPreset(whereHUD where, HudElements elements, bool everything = true)
        {
            this.where = where;
            this.elements = elements;
            this.everything = everything;
        }
    }
}
