using System;
using SGGames.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageHandler : MonoBehaviour
{
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
        OnDamageTaken?.Invoke(other.gameObject);
        if (health == null)
        {
            return;
        }
        health.TakeDamage(GetDamage(), gameObject, m_invulnerabilityTime);
    }
}
