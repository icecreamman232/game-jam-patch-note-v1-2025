using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class ButtonController : Selectable
    {
        public event Action OnClickCallback;
        
        public override void OnPointerUp(PointerEventData eventData)
        {
            OnClickCallback?.Invoke();
            base.OnPointerUp(eventData);
        }
    }
}
