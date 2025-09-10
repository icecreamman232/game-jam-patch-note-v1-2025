using UnityEngine;

namespace SGGames.Scripts.Building
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected float m_moveSpeed;

        private void Update()
        {
            UpdateMovement();
        }
        
        public virtual void Spawn(Vector2 direction)
        {
            transform.up = direction;
        }

        protected virtual void UpdateMovement()
        {
            transform.position += transform.up * (m_moveSpeed * Time.deltaTime);
            if(ShouldDestroy())
            {
                DestroyBullet();
            }
        }

        protected virtual bool ShouldDestroy()
        {
            return false;
        }
        

        /// <summary>
        /// Meaning to deactivate the bullet, waiting for next spawn
        /// </summary>
        private void DestroyBullet()
        {
            this.gameObject.SetActive(false);
        }
    }
}
