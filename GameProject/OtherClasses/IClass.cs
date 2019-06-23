using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Interface for the base enemy and classes class
/// </summary>
namespace GameProject
{
    public interface IClass
    {
        int GetHealth();
        int GetDamage();

        int GetDefense();

        string GetName();

        void SetDamage(int value);
        void SetHealth(int value);

    }
}
