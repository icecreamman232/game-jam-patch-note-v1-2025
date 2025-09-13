using System.Collections;
using SGGames.Scripts.Core;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] protected AmmoEvent m_ammoEvent;
    [SerializeField] protected ReloadingEvent m_reloadingEvent;
    [Header("Weapon")]
    [SerializeField] private AccuracyData m_accuracyData;
    [SerializeField] protected int m_accuracyLevel;
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

    protected Vector2 GetFinalShootingDirection(Vector2 inputDirection)
    {
        var differentAngle = m_accuracyData.GetAccuracy(m_accuracyLevel);
        var randomAngle = Random.Range(-differentAngle, differentAngle);
        var finalDirection = Quaternion.Euler(0, 0, randomAngle) * inputDirection;
        return finalDirection;
    }
    
    public virtual void Shoot(Vector2 direction)
    {
        if (!CanShoot()) return;
        
        var newProjectileGO = m_projectilePool.GetPooledGameObject();
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newProjectileGO.transform.up = GetFinalShootingDirection(direction);
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
