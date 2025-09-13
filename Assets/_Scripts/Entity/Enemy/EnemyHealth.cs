using System.Collections;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private ParticleSystem m_deathParticle;

    protected override void Die()
    {
        StartCoroutine(OnDying());
    }

    private IEnumerator OnDying()
    {
        m_isDead = true;
        m_spriteRenderer.enabled = false;
        m_deathParticle.Play();
        yield return new WaitUntil(() => !m_deathParticle.IsAlive());
        this.gameObject.SetActive(false);
    }
}
