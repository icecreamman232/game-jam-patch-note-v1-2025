using System.Collections;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    [Header("Events")]
    [SerializeField] protected AmmoEvent m_ammoEvent;
    [SerializeField] protected ReloadingEvent m_reloadingEvent;
    [Header("Player Weapon")]
    [SerializeField] private AccuracyData m_accuracyData;
    [SerializeField] protected int m_accuracyLevel;
    [SerializeField] protected int m_magazineSize;
    [SerializeField] protected int m_currentAmmo;
    [SerializeField] protected float m_reloadingTime;
    
    protected float m_reloadingTimer;
    protected bool m_isReloading;
    protected AmmoEventData m_ammoEventData;
    protected ReloadingEventData m_reloadingEventData;

    protected override void Start()
    {
        m_currentAmmo = m_magazineSize;
        m_ammoEventData = new AmmoEventData();
        UpdateAmmoEvent();
        
        m_reloadingEventData = new ReloadingEventData();
        base.Start();
    }

    protected override bool CanShoot()
    {
        if (m_isReloading) return false;
        return base.CanShoot();
    }
    
    private Vector2 GetFinalShootingDirection(Vector2 inputDirection)
    {
        var differentAngle = m_accuracyData.GetAccuracy(m_accuracyLevel);
        var randomAngle = Random.Range(-differentAngle, differentAngle);
        var finalDirection = Quaternion.Euler(0, 0, randomAngle) * inputDirection;
        return finalDirection;
    }

    public override void Shoot(Vector2 direction)
    {
        if (!CanShoot()) return;
        
        var newProjectileGO = m_projectilePool.GetPooledGameObject();
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
        
        StartCoroutine(OnCooldown());base.Shoot(direction);
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


    private void UpdateAmmoEvent()
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
}
