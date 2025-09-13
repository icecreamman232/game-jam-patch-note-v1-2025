using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour
{
    [SerializeField] private Weapon m_currentWeapon;

    public void Shoot(Vector2 direction)
    {
        m_currentWeapon.Shoot(direction);
    }
}
