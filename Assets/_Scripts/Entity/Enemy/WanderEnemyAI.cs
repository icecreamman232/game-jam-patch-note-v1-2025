using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderEnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyMovement m_movement;
    [SerializeField] private float m_maxAngleFromUp = 45f; // Maximum angle deviation from up direction
    private void Start()
    {
        m_movement.OnHitCollide = OnHitCollide;
        m_movement.SetMoveDirection(GetRandomDirection().normalized);
    }

    private Vector2 GetRandomDirection()
    {
        // Get current up direction as Vector2
        Vector2 upDirection = transform.up;
        
        // Generate random angle within the specified range
        float randomAngle = Random.Range(-m_maxAngleFromUp, m_maxAngleFromUp);
        
        // Get the angle of the up direction
        float upAngle = Mathf.Atan2(upDirection.y, upDirection.x) * Mathf.Rad2Deg;
        
        // Add the random offset to the up angle
        float finalAngle = (upAngle + randomAngle) * Mathf.Deg2Rad;
        
        // Convert back to Vector2
        return new Vector2(Mathf.Cos(finalAngle), Mathf.Sin(finalAngle));
    }

    
    private void OnHitCollide(GameObject obj)
    {
        m_movement.SetMoveDirection(GetRandomDirection().normalized);
        
    }
}
