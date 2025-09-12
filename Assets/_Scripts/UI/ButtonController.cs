using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : Selectable
{
   public Action OnClickButton;
   
   public override void OnPointerUp(PointerEventData eventData)
   {
      OnClickButton?.Invoke();
      base.OnPointerUp(eventData);
   }
}
