using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [SerializeField] protected float m_speed;
    [SerializeField] protected float m_range;
    [SerializeField] protected DamageHandler m_damageHandler;
    [SerializeField] private ParticleSystem m_hitParticle;
    
    private bool m_isAlive;
    protected float m_travelledDistance;
    protected Vector2 m_startPos;

    private void Awake()
    {
        if (m_damageHandler != null)
        {
            m_damageHandler.OnDamageTaken = OnDamageTaken;
        }
    }

    private void Update()
    {
        UpdateMovement();
    }

    public void Spawn()
    {
        m_isAlive = true;
        m_travelledDistance = 0;
        m_startPos = transform.position;
        m_hitParticle.gameObject.SetActive(true);
        m_spriteRenderer.enabled = true;
    }
    
    protected virtual void UpdateMovement()
    {
        if (!m_isAlive) return;
        transform.position += transform.up * (m_speed * Time.deltaTime);
        m_travelledDistance = Vector2.Distance(m_startPos, transform.position);
        if (m_travelledDistance >= m_range)
        {
            DestroyBullet();
        }
    }
    
    protected virtual void OnDamageTaken(GameObject obj)
    {
        DestroyBullet();
    }

    protected virtual void DestroyBullet()
    {
        StartCoroutine(OnDestroyBullet());
    }

    private IEnumerator OnDestroyBullet()
    {
        m_isAlive = false;
        m_spriteRenderer.enabled = false;
        
        if (m_hitParticle != null)
        {
            m_hitParticle.Play();
            yield return new WaitUntil(()=> !m_hitParticle.IsAlive());
        }
        this.gameObject.SetActive(false);
    }
}
