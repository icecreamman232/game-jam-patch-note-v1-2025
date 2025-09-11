using SGGames.Scripts.Core;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class LoseResultScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;

        public void Show()
        {
            m_canvasGroup.Activate();
        }

        public void Hide()
        {
            m_canvasGroup.Deactivate();
        }
    }
}
