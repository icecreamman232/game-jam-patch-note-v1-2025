using System.Collections;
using SGGames.Scripts.Core;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] protected float m_delayBetweenTwoShot;
    [SerializeField] protected Transform m_projectileSpawnPoint;
    [SerializeField] protected ObjectPooler m_projectilePool;
    
    protected bool m_isDelayBetween2Shot;
    

    protected virtual void Start()
    {
        
    }

    
    protected virtual bool CanShoot()
    {
        if (m_isDelayBetween2Shot) return false;
        
        return true;
    }

    
    public virtual void Shoot(Vector2 direction)
    {
        if (!CanShoot()) return;
        
        var newProjectileGO = m_projectilePool.GetPooledGameObject();
        newProjectileGO.transform.up = direction;
        newProjectileGO.transform.position = m_projectileSpawnPoint.position;
        var projectile = newProjectileGO.GetComponent<Projectile>();
        projectile.Spawn();
        StartCoroutine(OnCooldown());
    }

    protected virtual IEnumerator OnCooldown()
    {
        m_isDelayBetween2Shot = true;
        yield return new WaitForSeconds(m_delayBetweenTwoShot);
        m_isDelayBetween2Shot = false;
    }
}
