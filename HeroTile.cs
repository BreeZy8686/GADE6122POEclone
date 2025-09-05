using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    // Concrete character the player controls.
    // Starts with 40 HP and 5 attack power.
    public class HeroTile : CharacterTile
    {
        // constructor: takes a position and passes fixed stats to the base
        public HeroTile(Position position)
            : base(position, hitPoints: 40, attackPower: 5)
        {
        }

        // 'V' when alive, 'X' when dead (you can use '▼' if your font supports it)
        public override char Display => IsDead ? 'X' : 'V';
    }
}