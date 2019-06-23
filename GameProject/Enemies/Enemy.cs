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
    public abstract class Enemy : IClass
    {

        internal int health;
        internal int damage;
        internal string name;


        public Enemy()
        {

        }

        public Enemy(int health, int damage, string name)
        {
            this.health = health;
            this.damage = damage;
            this.name = name;
        }

        /// <summary>
        /// used to get all specific value of each class
        /// </summary>
        /// <returns></returns>
        public abstract int GetHealth();
        public abstract int GetDamage();

        public abstract string GetName();

        public abstract int GetDefense();

        public abstract void SetHealth(int value);

        public abstract void SetDamage(int value);

        public abstract int GetBasehealth();
        public abstract int GetBaseDamage();

    }
}
