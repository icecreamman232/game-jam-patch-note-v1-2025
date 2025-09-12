using UnityEngine;

public class Card : MonoBehaviour
{
   [SerializeField] private CardData m_cardData;
   [SerializeField] private CardVisual m_cardVisual;
   [SerializeField] private float m_currentDeathSpeed;
   [SerializeField] private float m_currentDeathCount;
   
   public float currentDeathSpeed => m_currentDeathSpeed;
   public float currentDeathCount => m_currentDeathCount;
   
   public CardData CardData => m_cardData;

   public void Initialize(CardData data)
   {
      m_cardData = data;
      m_currentDeathSpeed = data.GenerateDeathSpeed;
      m_currentDeathCount = data.DeathCount;
      
      m_cardVisual.Initialize(data);
   }
}
