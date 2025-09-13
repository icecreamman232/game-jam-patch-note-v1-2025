using UnityEngine;

public class PlayerHealthSlot : MonoBehaviour
{
    [SerializeField] private RectTransform m_bgImage;
    [SerializeField] private RectTransform m_fgImage;
    
    public void PlayDisappearTween()
    {
        m_fgImage.LeanScale(Vector3.one * 1.5f, 0.5f)
            .setEase(LeanTweenType.punch)
            .setOnComplete(() =>
            {
                m_fgImage.gameObject.SetActive(false);
            });
    }
}
