using SGGames.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoulManager : MonoBehaviour, IGameService, IBootStrap
{
    [SerializeField] private float m_totalSoulHarvested;
    [SerializeField] private float m_targetSoulCount;
    [SerializeField] private Image m_soulBar;
    [SerializeField] private TextMeshProUGUI m_soulCountText;
    [SerializeField] private AreaDeathCounter[] m_areaDeathCounters;
    
    public float TotalSoulHarvested => m_totalSoulHarvested;
    public void Install()
    {
        m_totalSoulHarvested = 0;
        ServiceLocator.RegisterService<SoulManager>(this);
        foreach (var area in m_areaDeathCounters)
        {
            area.OnDeathCountChanged = OnDeathCountChanged;
        }

        m_totalSoulHarvested = 0;
        UpdateSoulBar();
    }
    
    public void Uninstall()
    {
        ServiceLocator.UnregisterService<SoulManager>();
    }

    public bool IsSoulEnough(float amount)
    {
        return m_totalSoulHarvested >= amount;
    }
    
    public void SpentSoul(float amount)
    {
        m_totalSoulHarvested -= amount;
        UpdateSoulBar();
    }

    private void OnDeathCountChanged(float count)
    {
        m_totalSoulHarvested += count;
        UpdateSoulBar();
    }

    private void UpdateSoulBar()
    {
        m_soulBar.fillAmount = MathHelpers.Remap(m_totalSoulHarvested, 0, m_targetSoulCount, 0, 1);
        m_soulCountText.text = $"{m_totalSoulHarvested}/{m_targetSoulCount}";
    }

    
}
