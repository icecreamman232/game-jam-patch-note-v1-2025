using System;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class ShipVisual : MonoBehaviour
    {
        [SerializeField] private UIEvent m_uiEvent;
        [SerializeField] private SpriteRenderer m_shipModelRenderer;

        private void Start()
        {
            m_uiEvent.AddListener(OnReceiveUIEvent);
        }

        private void OnDestroy()
        {
            m_uiEvent.RemoveListener(OnReceiveUIEvent);
        }

        private void OnReceiveUIEvent(UIEventState uiState)
        {
            if (uiState == UIEventState.OpenBuildMode)
            {
                var color = m_shipModelRenderer.color;
                color.a = 0f;
                m_shipModelRenderer.color = color;
            }
            else if(uiState == UIEventState.CloseBuildMode)
            {
                var color = m_shipModelRenderer.color;
                color.a = 1f;
                m_shipModelRenderer.color = color;
            }
        }
    }
}
