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
        Vector2 inverseDirection = transform.up * -1;
        
        // Generate random angle within the specified range
        float randomAngle = Random.Range(-m_maxAngleFromUp, m_maxAngleFromUp);
       
        var finalDirection = Quaternion.Euler(0, 0, randomAngle) * inverseDirection;
        
        return finalDirection;
    }

    
    private void OnHitCollide(GameObject obj)
    {
        m_movement.SetMoveDirection(GetRandomDirection().normalized);
    }
}
