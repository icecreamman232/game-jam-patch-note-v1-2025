using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    [SerializeField] protected float m_maxHealth;
    [SerializeField] protected float m_currentHealth;

    private MaterialPropertyBlock m_materialPropertyBlock;
    protected bool m_isInvulnerable;
    protected bool m_isDead;
    
    public float CurrentHealth => m_currentHealth;
    public float MaxHealth => m_maxHealth;
    
    private void Start()
    {
        m_materialPropertyBlock = new MaterialPropertyBlock();
        Initialize();
    }

    protected virtual void Initialize()
    {
        m_currentHealth = m_maxHealth;
    }

    protected virtual bool CanTakeDamage()
    {
        if (m_isDead) return false;
        if (m_isInvulnerable) return false;
        if (m_currentHealth <= 0) return false;
        
        return true;
    }

    protected virtual void Damage(float damage)
    {
        m_currentHealth -= damage;
    }

    protected virtual void AfterTakingDamage(float invulnerabilityTime)
    {
        if (m_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(OnInvulnerable(invulnerabilityTime));
        }
    }

    protected virtual IEnumerator OnInvulnerable(float duration)
    {
        m_isInvulnerable = true;
        var timeStop = Time.time + duration;
        
        while (Time.time < timeStop)
        {
            m_spriteRenderer.GetPropertyBlock(m_materialPropertyBlock);
            m_materialPropertyBlock.SetFloat("_BlendAmount", 1);
            m_spriteRenderer.SetPropertyBlock(m_materialPropertyBlock);
            yield return new WaitForSeconds(0.08f);
            m_spriteRenderer.GetPropertyBlock(m_materialPropertyBlock);
            m_materialPropertyBlock.SetFloat("_BlendAmount", 0);
            m_spriteRenderer.SetPropertyBlock(m_materialPropertyBlock);
            yield return new WaitForSeconds(0.08f);
        }
        
        m_spriteRenderer.GetPropertyBlock(m_materialPropertyBlock);
        m_materialPropertyBlock.SetFloat("_BlendAmount", 0);
        m_spriteRenderer.SetPropertyBlock(m_materialPropertyBlock);

        m_isInvulnerable = false;
    }
    
    protected virtual void Die()
    {
        m_isDead = true;
        this.gameObject.SetActive(false);
    }

    public virtual void TakeDamage(float damage, GameObject source, float invulnerabilityTime)
    {
        if (!CanTakeDamage()) return;

        Damage(damage);
        
        AfterTakingDamage(invulnerabilityTime);
    }
}
