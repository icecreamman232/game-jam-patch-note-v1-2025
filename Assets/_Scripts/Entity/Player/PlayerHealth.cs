
using UnityEngine;

public class PlayerHealth : Health
{
     [SerializeField] private UpdatePlayerHealthEvent m_updatePlayerHealthEvent;
     
     private UpdatePlayerHealthEventData m_updatePlayerHealthEventData = new UpdatePlayerHealthEventData();


     private void UpdateHealthBar()
     {
          m_updatePlayerHealthEventData.CurrentHealth = (int)m_currentHealth;
          m_updatePlayerHealthEventData.MaxHealth = (int)m_maxHealth;
          m_updatePlayerHealthEvent.Raise(m_updatePlayerHealthEventData);
     }
     
     protected override void Damage(float damage)
     {
          m_currentHealth -= 1;
          UpdateHealthBar();

     }
}
