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
/// Base values of all the enemies
/// </summary>
namespace GameProject
{
    class Wolf : Enemy
    {

        private new int damage = 2;
        private new int health = 22;
        private new readonly string name = "Wolf";
        private int defense = 3;

        private int baseHealth;
        private int baseDamage;

        public Wolf()
        {
            baseHealth = health;
            baseDamage = damage;
        }

        public override int GetDamage()
        {
            return this.damage;
        }

        public override int GetHealth()
        {
            return this.health;
        }

        public override string GetName()
        {
            return this.name;
        }

        public override int GetDefense()
        {
            return defense;
        }
        public override void SetHealth(int value)
        {
            health = value;
        }
        public override void SetDamage(int value)
        {
            damage = value;
        }
        public override int GetBaseDamage()
        {
            return baseDamage;
        }

        public override int GetBasehealth()
        {
            return baseHealth;
        }

    }
}
