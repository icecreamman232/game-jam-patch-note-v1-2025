using UnityEngine;

public class Card : MonoBehaviour
{
   [SerializeField] private float m_generateDeathSpeed;
   [SerializeField] private float m_deathCount;
   
   public float GenerateDeathSpeed => m_generateDeathSpeed;
   public float DeathCount => m_deathCount;
}
