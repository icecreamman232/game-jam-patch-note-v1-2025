using System;
using SGGames.Scripts.Managers;
using UnityEngine;

public class CardInput : MonoBehaviour
{
    [SerializeField] private BoxCollider2D m_collider;
    
    private Vector3 m_startPos;

    private void Awake()
    {
        m_startPos = transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = InputManager.GetWorldMousePosition();
    }

    private void OnMouseUp()
    {
        if (IsInDropArea(out var area))
        {
            var areaController = area.GetComponent<AreaController>();
            var card = GetComponent<Card>();
            if (!areaController.AssignCard(card))
            {
                transform.position = m_startPos;
            }
        } 
    }
    
    private bool IsInDropArea(out Collider2D area)
    {
        area  = Physics2D.OverlapBox(transform.position,m_collider.size,0, LayerMask.GetMask("Drop Area"));
        return area != null;
    }
}
