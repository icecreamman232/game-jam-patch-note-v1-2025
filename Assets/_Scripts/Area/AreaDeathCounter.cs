using System;
using UnityEngine;
using UnityEngine.UI;

public class AreaDeathCounter : MonoBehaviour
{
    [SerializeField] private float m_totalSpeed;
    [SerializeField] private float m_totalCount;
    [SerializeField] private float m_fillSpeed = 0;
    [SerializeField] private Image m_barFill;
    
    private const float k_speedExchange = 0.01f;
    
    public Action<float> OnDeathCountChanged;
    
    private void Update()
    {
        m_barFill.fillAmount += m_fillSpeed * Time.deltaTime;
        if (m_barFill.fillAmount >= 1)
        {
            m_barFill.fillAmount = 0;
            CountDeath();
        }
    }
    
    public void ApplyCard(Card card)
    {
        m_totalSpeed += card.currentDeathSpeed;
        m_totalCount += card.currentDeathCount;
        m_fillSpeed = m_totalSpeed * k_speedExchange;
    }

    private void CountDeath()
    {
        OnDeathCountChanged?.Invoke(m_totalCount);
    }
}
