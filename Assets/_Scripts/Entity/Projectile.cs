using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float m_speed;
    [SerializeField] protected float m_range;
    [SerializeField] protected DamageHandler m_damageHandler;
    
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
        m_travelledDistance = 0;
        m_startPos = transform.position;
    }
    
    protected virtual void UpdateMovement()
    {
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
        this.gameObject.SetActive(false);
    }
}
