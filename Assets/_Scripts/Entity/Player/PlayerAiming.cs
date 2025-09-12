using SGGames.Scripts.Managers;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] private Vector2 m_aimDirection;
    
    public Vector2 AimDirection => m_aimDirection;
    
    private void Update()
    {
        if (!this.gameObject.activeInHierarchy) return;
        
        m_aimDirection = (InputManager.GetWorldMousePosition() - transform.position).normalized;
    }
}
