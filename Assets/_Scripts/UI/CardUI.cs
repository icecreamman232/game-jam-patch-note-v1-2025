using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : Selectable
{
    [SerializeField] private float holdDuration = 1.0f; // Configurable hold duration
    
    private bool isPressed = false;
    private Coroutine holdCoroutine;
    
    // Events you can subscribe to
    public System.Action OnHoldComplete;
    public System.Action OnHoldCancelled;
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!IsInteractable()) return;
        
        isPressed = true;
        holdCoroutine = StartCoroutine(HoldTimer());
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (isPressed)
        {
            CancelHold();
        }
    }
    
    private IEnumerator HoldTimer()
    {
        yield return new WaitForSeconds(holdDuration);
        
        if (isPressed)
        {
            // Hold completed successfully
            isPressed = false;
            OnHoldComplete?.Invoke();
            Debug.Log("Hold completed!");
        }
    }
    
    private void CancelHold()
    {
        isPressed = false;
        
        if (holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            holdCoroutine = null;
            OnHoldCancelled?.Invoke();
            Debug.Log("Hold cancelled!");
        }
    }
    
    // Public method to change hold duration at runtime
    public void SetHoldDuration(float duration)
    {
        holdDuration = Mathf.Max(0.1f, duration); // Minimum 0.1 seconds
    }
    
    // Clean up when object is disabled
    private void OnDisable()
    {
        if (isPressed)
        {
            CancelHold();
        }
    }

}
