using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// By André Normann
/// C# 2 Game Project
/// 2019-06-04
/// </summary>
/// 
/// <summary>
/// Subclass that sets all the values 
/// </summary>
namespace GameProject
{
    public class Knight : Classes
    {
        private int baseDamage = 5;
        private int changedDamage;
        private new int health = 100;
        private int changedHealth;
        private int changedDefence;
        private new readonly string specialAbillity = "Retaliate";
        private new int defense = 5;
        private new readonly string name = "Knight";
        
        public Knight ()
        {
            changedHealth = health;
        }

        public override int GetDamage()
        {
            return baseDamage;
        }

        public override int GetHealth()
        {
            return health;
        }

        public override string GetSpecialAbillity()
        {
            return specialAbillity;
        }

        public override int GetDefense()
        {
            return defense;
        }
        public override string GetName()
        {
            return name;
        }

        public override void SetDamage(int value)
        {
            changedDamage = value;
        }

        public override int GetChangedDamage()
        {
            return changedDamage;
        }

        public override int GetChangedHealth()
        {
            return changedHealth;
        }

        public override void SetHealth(int value)
        {
            changedHealth = value;
        }
        public override int GetChangedDefence()
        {
            return changedDefence;
        }

        public override void SetDefence(int value)
        {
            changedDefence = value;
        }

    }
}
