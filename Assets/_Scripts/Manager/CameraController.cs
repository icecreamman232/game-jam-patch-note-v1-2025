using System;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform m_followingTarget;
        [SerializeField] private bool m_canFollow;

        private Vector3 m_currentTargetPos;
        private float m_cameraDistance;

        private void Awake()
        {
            m_cameraDistance = transform.position.z;
        }

        private void Update()
        {
            if (!m_canFollow) return;
            if (m_followingTarget == null) return;
            m_currentTargetPos = m_followingTarget.position;
            m_currentTargetPos.z = m_cameraDistance;
            transform.position = m_currentTargetPos;
        }
    }
}
