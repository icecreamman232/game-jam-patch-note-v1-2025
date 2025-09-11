using SGGames.Scripts.Core;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class WinResultScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private CanvasGroup m_bgCanvasGroup;

        public void Show()
        {
            m_canvasGroup.Activate();
            m_bgCanvasGroup.Activate();
        }

        public void Hide()
        {
            m_canvasGroup.Deactivate();
            m_bgCanvasGroup.Deactivate();
        }
    }
}
