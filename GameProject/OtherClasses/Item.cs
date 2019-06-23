using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// simple item handler that takes in damage and name and prints out the name and damage in the tostring
/// </summary>


/// <summary>
/// By André Normann
/// C# 2 Game Project
/// 2019-06-04
/// </summary>
/// 
namespace GameProject
{
    [Serializable]
    public class Item
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public Item(string name, int damage)
        {
            Name = name;
            Damage = damage;
        }

        public override string ToString()
        {
            return string.Format($"{Name} Damage: {Damage}");
        }
    }
}
