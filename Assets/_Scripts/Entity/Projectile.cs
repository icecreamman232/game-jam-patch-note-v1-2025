using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float m_speed;
    [SerializeField] protected float m_range;
    
    protected float m_travelledDistance;
    protected Vector2 m_startPos;
    
    private void Update()
    {
        UpdateMovement();
    }

    public void Spawn()
    {
        m_travelledDistance = 0;
        transform.position = m_startPos;
    }
    
    protected virtual void UpdateMovement()
    {
        transform.position += transform.up * (m_speed * Time.deltaTime);
        m_travelledDistance = Vector2.Distance(transform.position, Vector2.zero);
        if (m_travelledDistance >= m_range)
        {
            DestroyBullet();
        }
    }

    protected virtual void DestroyBullet()
    {
        this.gameObject.SetActive(false);
    }
}
