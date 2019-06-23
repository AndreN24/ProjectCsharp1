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
/// Abstract class since the classes inherited needs the methods.
/// </summary>
namespace GameProject
{
    public abstract class Classes : IClass
    {

        internal int health;
        internal int damage;
        internal int specialAbillity;
        internal int defense;
        internal string name;

        /// <summary>
        /// Constructors
        /// </summary>
        public Classes()
        {

        }
        public Classes(int health, int damage)
        {
            this.health = health;
            this.damage = damage;
        }

        /// <summary>
        /// abstract methods that are needed in the subclasses
        /// </summary>
        /// <returns></returns>
        /// Realized too late i wasn't using standard getters and setters properties, but there are to many places to change this so i decided to keep it like this.
        public abstract int GetHealth();
        public abstract int GetDamage();

        public abstract string GetSpecialAbillity();

        public abstract int GetDefense();

        public abstract string GetName();

        public abstract void SetDamage(int value);
        public abstract void SetHealth(int value);
        public abstract void SetDefence(int value);

        public abstract int GetChangedDamage();

        public abstract int GetChangedHealth();


        public abstract int GetChangedDefence();

    }
}
