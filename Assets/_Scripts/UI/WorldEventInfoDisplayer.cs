using TMPro;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class WorldEventInfoDisplayer : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private TextMeshProUGUI m_soulCostText;
        [SerializeField] private TextMeshProUGUI m_nameText;
        [SerializeField] private TextMeshProUGUI m_descText;

        public void FillData(WorldEventData data)
        {
            m_nameText.text = data.EventName;
            m_descText.text = data.EventDescription;
            m_soulCostText.text = data.Cost.ToString();
        }
        
        public void Show()
        {
            m_canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            m_canvasGroup.alpha = 0;
        }
    }
}
