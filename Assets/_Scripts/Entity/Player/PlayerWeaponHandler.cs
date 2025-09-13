using SGGames.Scripts.Core;
using SGGames.Scripts.Managers;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
   [SerializeField] private Weapon m_currentWeapon;
   [SerializeField] private PlayerAiming m_playerAiming;
   
   private void Start()
   {
      ServiceLocator.GetService<InputManager>().OnAttackInputCallback += OnAttackInputCallback;
   }

   private void OnDestroy()
   {
      ServiceLocator.GetService<InputManager>().OnAttackInputCallback -= OnAttackInputCallback;
   }

   private void Update()
   {
      UpdateWeaponRotation();
   }

   private void UpdateWeaponRotation()
   {
      m_currentWeapon.transform.right = m_playerAiming.AimDirection;
   }

   private void OnAttackInputCallback()
   {
      m_currentWeapon.Shoot(m_playerAiming.AimDirection);
   }
}
