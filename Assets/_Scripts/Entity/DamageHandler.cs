using System;
using SGGames.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] private float m_knockBackForce = 10;
    [SerializeField] private float m_minDamage;
    [SerializeField] private float m_maxDamage;
    [SerializeField] private float m_invulnerabilityTime;
    [SerializeField] private LayerMask m_targetMask;

    public Action<GameObject> OnDamageTaken;
    
    protected virtual float GetDamage()
    {
        return Random.Range(m_minDamage, m_maxDamage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask)) return;

        OnDealDamage(other);
    }

    protected virtual void OnDealDamage(Collider2D other)
    {
        var health = other.GetComponent<Health>();
        var movement = other.GetComponent<EnemyMovement>();
        OnDamageTaken?.Invoke(other.gameObject);

        if (movement != null)
        {
            var direction = other.transform.position - transform.position;
            movement.ApplyKnockback(direction.normalized, m_knockBackForce);
        }
        
        if (health != null)
        {
            health.TakeDamage(GetDamage(), gameObject, m_invulnerabilityTime);
        }
    }
}
