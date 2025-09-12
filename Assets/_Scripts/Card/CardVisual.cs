using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_cardName;
    [SerializeField] private Image m_icon;
    [SerializeField] private TextMeshProUGUI m_cardValue;

    public void Initialize(CardData data)
    {
        m_cardName.text = data.CardName;
        m_icon.sprite = data.Icon;
        m_cardValue.text = $"+{data.DeathCount} souls";
    }
}
