using System;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private UpdatePlayerHealthEvent m_updatePlayerHealthEvent;
    [SerializeField] private PlayerHealthSlot[] m_healthSlots;

    private void Awake()
    {
        m_updatePlayerHealthEvent.AddListener(UpdateHealthBar);
    }

    private void OnDestroy()
    {
        m_updatePlayerHealthEvent.RemoveListener(UpdateHealthBar);
    }


    private void UpdateHealthBar(UpdatePlayerHealthEventData eventData)
    {
        m_healthSlots[eventData.CurrentHealth].PlayDisappearTween();
    }
}
