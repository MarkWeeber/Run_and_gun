using UnityEngine;

namespace Run_n_gun.Space
{
    public class Interfaces
    {

    }

    public interface IDamager
    {
        float DamageValue { get; set; }
        void DealDamage(IDamagable damagable);
    }

    public interface IDamagable
    {
        void TakeDamage(float damageValue);
    }
}