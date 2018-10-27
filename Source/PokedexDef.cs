using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Pokemon
{
    public class PokedexDef : Def
    {
        public Int32 HP, Atk, Def, SpA, SpD, Spe, Total, Nat, Per, Hoe, Sin, AI, 
            Total_EVs, EV_HP, EV_Atk, EV_Def, EV_SpA, EV_SpD, EV_Spe, EXPV;

        public float Mass;
        public PokemonTypeDef Type_I, Type_II, Type_III;
        public PokemonAbilityDef Ability_I, Ability_II, Hidden_Ability;
        
        public String Hatch, Gender, Egg_Group_I, Egg_Group_II, Evolve, Color;
    }

    public class PokemonAbilityDef : Def
    {
    }


    public class PokemonTypeDef : Def
    {
        public List<String> super, weak, none;
    }
}