using System;
using SGGames.Scripts.Core;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class ShipMovement : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private float m_steeringSpeed = 2f; // How fast the direction changes
        [SerializeField] private float m_rotationSpeed = 90f; // Degrees per second
        [SerializeField] private float m_minSpeedForRotation = 0.1f; // Minimum speed to rotate
        [SerializeField] private Vector2 m_movingDirection;
        [SerializeField] private Vector2 m_movingInput;
        [SerializeField] private Transform m_model;

        private void Start()
        {
            var inputManager = ServiceLocator.GetService<InputManager>();
            inputManager.OnMoveInputCallback += OnMoveInputCallback;
        }

        private void OnDestroy()
        {
            var inputManager = ServiceLocator.GetService<InputManager>();
            inputManager.OnMoveInputCallback -= OnMoveInputCallback;
        }

        private void OnMoveInputCallback(Vector2 input)
        {
            m_movingInput = input;
        }
        
        private void UpdateSteeringBehavior()
        {
            // If there's no input, gradually slow down
            if (m_movingInput.magnitude < 0.1f)
            {
                m_movingDirection = Vector2.Lerp(m_movingDirection, Vector3.zero, m_steeringSpeed * Time.deltaTime);
                return;
            }

            // Convert 2D input to 3D target direction (assuming XZ movement)
            Vector3 targetDirection = m_movingInput.normalized;

            // Smoothly rotate current direction towards target direction
            m_movingDirection = Vector2.Lerp(m_movingDirection, targetDirection, m_steeringSpeed * Time.deltaTime);
        }
        

        private void Update()
        {
            UpdateSteeringBehavior();
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            m_model.up = m_movingDirection;
            transform.Translate(m_movingDirection * (m_moveSpeed * Time.deltaTime));
        }
    }
}
