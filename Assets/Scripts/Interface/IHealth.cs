using UnityEngine;

namespace Assets.Scripts.Interface
{
    public interface IHealth
    {
        public void TakeDamage(float damage);
        public void BloodRecovery(float amount);

    }
}