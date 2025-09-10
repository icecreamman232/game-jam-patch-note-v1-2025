using SGGames.Scripts.Core;
using UnityEngine;

namespace SGGames.Scripts.Building
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected Vector2 m_shootingDirection;
        [SerializeField] protected ObjectPooler m_objectPooler;
        
        [ContextMenu("Shoot")]
        protected virtual void Shoot()
        {
            var projectileGameObject = m_objectPooler.GetPooledGameObject();
            projectileGameObject.transform.position = transform.position;
            // var angle = Mathf.Atan2(m_shootingDirection.y, m_shootingDirection.x) * Mathf.Rad2Deg;
            // projectileGameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            var projectile = projectileGameObject.GetComponent<Projectile>();
            projectile.Spawn(m_shootingDirection);
        }
    }
}
