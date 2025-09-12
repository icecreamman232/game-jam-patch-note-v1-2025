using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class AreaPivot
{
    public Transform Pivot;
    public Card OccupiedCard;
}

public class AreaController : MonoBehaviour
{
    [SerializeField] private AreaDeathCounter m_deathCounter;
    [SerializeField] private AreaPivot[] m_pivot;

    public bool AssignCard(Card card)
    {
        var emptyPivot = m_pivot.FirstOrDefault(pivot => pivot.OccupiedCard == null);
        if (emptyPivot != null)
        {
            card.transform.SetParent(emptyPivot.Pivot, false);
            card.transform.localPosition = Vector3.zero;
            emptyPivot.OccupiedCard = card;
            
            m_deathCounter.ApplyCard(card);
            return true;
        }

        return false;
    }
}