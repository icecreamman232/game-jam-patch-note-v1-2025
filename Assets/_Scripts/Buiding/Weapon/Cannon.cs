using UnityEngine;

namespace SGGames.Scripts.Building
{
    public class Cannon : Weapon
    {
        public void SetDirection(Vector2 direction)
        {
            m_shootingDirection = direction;
        }
    }
}
