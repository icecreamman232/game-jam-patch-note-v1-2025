using System;
using SGGames.Scripts.Core;
using SGGames.Scripts.Managers;
using UnityEngine;

namespace SGGames.Scripts.Ship
{
    public class ShipMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float forwardSpeed = 10f;
        [SerializeField] private float reverseSpeed = 5f;
        [SerializeField] private float rotationSpeed = 90f;
        [SerializeField] private float acceleration = 2f;
        [SerializeField] private float deceleration = 3f;
        [Header("Collision Settings")]
        [SerializeField] private LayerMask wallLayerMask = -1;
        [SerializeField] private float collisionCheckDistance = 0.5f;
        [SerializeField] private float shipRadius = 0.5f;

        
        private Vector2 movementInput;
        private float currentForwardSpeed;
        private float currentRotationVelocity;
        private InputManager inputManager;

        private void Start()
        {
            // Subscribe to input events
            inputManager = ServiceLocator.GetService<InputManager>();
            if (inputManager != null)
            {
                inputManager.OnMoveInputCallback += OnMoveInput;
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from input events
            if (inputManager != null)
            {
                inputManager.OnMoveInputCallback -= OnMoveInput;
            }
        }

        private void OnMoveInput(Vector2 input)
        {
            movementInput = input;
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            float verticalInput = movementInput.y;
            float targetSpeed = 0f;

            if (verticalInput > 0)
            {
                // Forward movement
                targetSpeed = forwardSpeed * verticalInput;
            }
            else if (verticalInput < 0)
            {
                // Reverse movement (negative input means moving backwards)
                targetSpeed = reverseSpeed * verticalInput;
            }

            // Smoothly accelerate/decelerate to target speed
            if (Mathf.Abs(targetSpeed) > Mathf.Abs(currentForwardSpeed))
            {
                currentForwardSpeed = Mathf.MoveTowards(currentForwardSpeed, targetSpeed, acceleration * Time.deltaTime);
            }
            else
            {
                currentForwardSpeed = Mathf.MoveTowards(currentForwardSpeed, targetSpeed, deceleration * Time.deltaTime);
            }

            // Apply movement in the forward direction of the ship
            Vector2 forwardDirection = transform.up; // Assuming ship faces up in sprite
            Vector2 movement = forwardDirection * (currentForwardSpeed * Time.deltaTime);
            
            // Check for wall collision before moving
            if (CheckWallCollision(movement))
            {
                // Stop movement if collision detected
                currentForwardSpeed = 0f;
                return;
            }

            
            // Move the transform directly
            transform.position += (Vector3)movement;
        }

        private void HandleRotation()
        {
            float horizontalInput = movementInput.x;
            float targetRotationVelocity = 0f;
            
            if (Mathf.Abs(horizontalInput) > 0.1f && Mathf.Abs(movementInput.y) > 0.1f)
            {
                // Only rotate when there's horizontal input AND the ship is moving
                targetRotationVelocity = -horizontalInput * rotationSpeed;
                
                // Adjust rotation speed based on movement direction
                // When moving in reverse, steering feels more natural if slightly reduced
                if (currentForwardSpeed < 0)
                {
                    targetRotationVelocity *= 0.7f; // Reduce steering sensitivity when reversing
                }
            }

            // Smoothly change rotation velocity
            currentRotationVelocity = Mathf.MoveTowards(currentRotationVelocity, targetRotationVelocity, deceleration * 60f * Time.deltaTime);

            // Apply rotation
            if (Mathf.Abs(currentRotationVelocity) > 0.1f)
            {
                transform.Rotate(0, 0, currentRotationVelocity * Time.deltaTime);
            }
        }
        
        /// <summary>
        /// Checks if the ship will collide with a wall in the given movement direction
        /// </summary>
        /// <param name="movement">The intended movement vector</param>
        /// <returns>True if collision detected, false otherwise</returns>
        private bool CheckWallCollision(Vector2 movement)
        {
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = currentPosition + movement;
        
            // Use CircleCast to check for collision along the movement path
            RaycastHit2D hit = Physics2D.CircleCast(
                currentPosition,
                shipRadius,
                movement.normalized,
                collisionCheckDistance + movement.magnitude,
                wallLayerMask
            );
        
            // If we hit something, check if it's close enough to stop movement
            if (hit.collider != null)
            {
                float distanceToWall = hit.distance - shipRadius;
                return distanceToWall <= movement.magnitude;
            }
        
            return false;
        }


        // Optional: Visual debug information
        private void OnDrawGizmosSelected()
        {
            // Draw forward direction
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, transform.up * 2f);
            
            // Draw current velocity direction
            if (currentForwardSpeed != 0)
            {
                Vector2 velocityDirection = transform.up * Mathf.Sign(currentForwardSpeed);
                Gizmos.color = currentForwardSpeed > 0 ? Color.green : Color.red;
                Gizmos.DrawRay(transform.position, velocityDirection * 1.5f);
            }
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, shipRadius);
        }
    }
}
