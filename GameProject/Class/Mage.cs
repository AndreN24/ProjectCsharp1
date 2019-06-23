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
    public class Mage : Classes
    {
        private int baseDamage = 10;
        private int changedDamage;
        private int changedHealth ;
        private new int health = 80;
        private new readonly string specialAbillity = "Polymorph";
        private new int defense = 4;
        private new readonly string name = "Mage";
        private int changedDefence;

        public Mage()
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
