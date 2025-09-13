using System.Collections;
using SGGames.Scripts.Core;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] protected AmmoEvent m_ammoEvent;
    [SerializeField] protected ReloadingEvent m_reloadingEvent;
    [Header("Weapon")]
    [SerializeField] protected float m_delayBetweenTwoShot;
    [SerializeField] protected Transform m_projectileSpawnPoint;
    [SerializeField] protected ObjectPooler m_projectilePool;
    [SerializeField] protected int m_magazineSize;
    [SerializeField] protected int m_currentAmmo;
    [SerializeField] protected float m_reloadingTime;
    
    private float m_reloadingTimer;
    protected bool m_isReloading;
    protected bool m_isDelayBetween2Shot;
    protected AmmoEventData m_ammoEventData;
    protected ReloadingEventData m_reloadingEventData;

    protected virtual void Start()
    {
        m_currentAmmo = m_magazineSize;
        m_ammoEventData = new AmmoEventData();
        UpdateAmmoEvent();
        
        m_reloadingEventData = new ReloadingEventData();
    }

    protected void UpdateAmmoEvent()
    {
        m_ammoEventData.Ammo = m_currentAmmo;
        m_ammoEvent.Raise(m_ammoEventData);
    }

    private void UpdateReloadingEvent()
    {
        m_reloadingEventData.CurrentTime = m_reloadingTimer;
        m_reloadingEventData.MaxTime = m_reloadingTime;
        m_reloadingEvent.Raise(m_reloadingEventData);
    }

    protected virtual bool CanShoot()
    {
        if (m_isReloading) return false;
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
        m_currentAmmo--;
        UpdateAmmoEvent();
        if (m_currentAmmo <= 0)
        {
            StartCoroutine(OnReloading());
            return;
        }
        
        StartCoroutine(OnCooldown());
    }

    protected virtual IEnumerator OnReloading()
    {
        m_isReloading = true;
        m_reloadingTimer = 0;

        while (m_reloadingTimer < m_reloadingTime)
        {
            m_reloadingTimer += Time.deltaTime;
            UpdateReloadingEvent();
            yield return null;
        }
        
        m_currentAmmo = m_magazineSize;
        UpdateAmmoEvent();
        m_isReloading = false;
    }

    protected virtual IEnumerator OnCooldown()
    {
        m_isDelayBetween2Shot = true;
        yield return new WaitForSeconds(m_delayBetweenTwoShot);
        m_isDelayBetween2Shot = false;
    }
}
