using SGGames.Scripts.Core;
using SGGames.Scripts.Managers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private Vector2 m_moveDirection;

    private void Start()
    {
        ServiceLocator.GetService<InputManager>().OnMoveInputCallback += OnMoveInputCallback;
    }

    private void OnDestroy()
    {
        ServiceLocator.GetService<InputManager>().OnMoveInputCallback -= OnMoveInputCallback;
    }

    private void OnMoveInputCallback(Vector2 input)
    {
        m_moveDirection = input;
    }

    private void Update()
    {
        transform.Translate(m_moveDirection * (m_speed * Time.deltaTime));
    }
}
