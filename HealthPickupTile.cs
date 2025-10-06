using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122
{
    /// <summary>
    /// Q4.2: Health pickup that restores 10 HP to the hero.
    /// </summary>
    public class HealthPickupTile : PickupTile
    {
        public HealthPickupTile(Position position) : base(position) { }

        public override char Display => '+';

        public override void ApplyEffect(CharacterTile target)
        {
            target.Heal(10);
        }
    }
}
