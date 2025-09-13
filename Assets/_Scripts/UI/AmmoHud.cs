
using TMPro;
using UnityEngine;

public class AmmoHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ammoText;
    [SerializeField] private AmmoEvent m_ammoEvent;
    private void Awake()
    {
        m_ammoEvent.AddListener(OnUpdateAmmo);
    }

    private void OnDestroy()
    {
        m_ammoEvent.RemoveListener(OnUpdateAmmo);
    }

    private void OnUpdateAmmo(AmmoEventData eventData)
    {
        m_ammoText.text = $"x {eventData.Ammo}";
    }
}
