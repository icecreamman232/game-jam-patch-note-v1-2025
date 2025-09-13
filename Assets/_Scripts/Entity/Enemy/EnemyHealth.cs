using System.Collections;
using SGGames.Scripts.Core;
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
        ServiceLocator.GetService<LevelManager>().NotifyEnemyDeath(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
