using System.Collections;
using SGGames.Scripts.Core;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float m_cooldown;
    [SerializeField] protected Transform m_projectileSpawnPoint;
    [SerializeField] protected ObjectPooler m_projectilePool;
    
    protected bool m_isCooldown;

    protected virtual bool CanShoot()
    {
        if (m_isCooldown) return false;
        
        return true;
    }
    
    public virtual void Shoot(Vector2 direction)
    {
        if (!CanShoot()) return;
        
        var newProjectileGO = m_projectilePool.GetPooledGameObject();
        newProjectileGO.transform.position = m_projectileSpawnPoint.position;
        newProjectileGO.transform.up = direction;
        var projectile = newProjectileGO.GetComponent<Projectile>();
        projectile.Spawn();
        StartCoroutine(OnCooldown());
    }

    protected virtual IEnumerator OnCooldown()
    {
        m_isCooldown = true;
        yield return new WaitForSeconds(m_cooldown);
        m_isCooldown = false;
    }
}
