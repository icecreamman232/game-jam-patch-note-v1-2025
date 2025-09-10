
using UnityEngine;

namespace SGGames.Scripts.Building
{
    public class NormalProjectile : Projectile
    {
        [SerializeField] protected float m_range;
        
        private Vector2 m_startPosition;
        private float m_distanceTravelled;

        public override void Spawn(Vector2 direction)
        {
            m_distanceTravelled = 0;
            m_startPosition = transform.position;
            base.Spawn(direction);
        }

        protected override void UpdateMovement()
        {
            base.UpdateMovement();
            m_distanceTravelled = Vector2.Distance(m_startPosition, transform.position);
        }


        protected override bool ShouldDestroy()
        {
            return m_distanceTravelled >= m_range;
        }
    }
}
