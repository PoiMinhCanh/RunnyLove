using UnityEngine;

namespace Assets.Scripts.Interface
{
    public interface IThrowWeapon
    {
        public void throwProjectile(Vector2 direction, float force);
    }
}
