using System.Collections;
using SGGames.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

public class TeleportGun : PlayerWeapon
{
    [SerializeField] private AnnouncerEvent m_announcerEvent;
    [SerializeField] private BoxCollider2D m_playerCollider;
    [SerializeField] private bool m_drawDebug;
    [SerializeField] private float m_teleportRange;
    [SerializeField] private float m_chanceToTeleport;
    [SerializeField] private float m_cooldownTeleport;

    private readonly Color m_messagColor = new Color(0.9019608f,0.2784314f,0.1764706f);
    private AnnouncerEventData m_announcerEventData;
    private PlayerMovement m_playerMovement;
    private bool m_isCooldownTeleport;
    private bool m_isTeleporting;

    protected override void Start()
    {
        if (m_playerCollider == null)
        {
            m_playerCollider = GetComponentInParent<BoxCollider2D>();
        }

        if (m_playerMovement == null)
        {
            m_playerMovement = GetComponentInParent<PlayerMovement>();
        }
        
        m_announcerEventData = new AnnouncerEventData();
        base.Start();
    }

    private bool CanTeleport()
    {
        if(m_isCooldownTeleport) return false;
        return Random.Range(0, 100) < m_chanceToTeleport;
    }

    private Vector2 GetRandomTeleportPosition()
    {
        var randomPos = Random.insideUnitCircle * m_teleportRange;
        var collisionCheck = Physics2D.OverlapBox(randomPos, m_playerCollider.size, 0f, LayerMask.GetMask("Obstacle"));
        if (collisionCheck!=null && collisionCheck.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            var direction = (transform.position - collisionCheck.transform.position).normalized;
            var rayCast = Physics2D.Raycast(transform.position, direction, m_teleportRange, LayerMask.GetMask("Obstacle"));
            if (rayCast.collider != null)
            {
                return rayCast.point;
            }
        }
        
        return randomPos;
    }

    protected override bool CanShoot()
    {
        if (m_isTeleporting) return false;
        return base.CanShoot();
    }

    public override void Shoot(Vector2 direction)
    {
        base.Shoot(direction);
        
        if (!CanTeleport()) return;


        
        StartCoroutine(OnTeleporting());

    }

    private IEnumerator OnTeleporting()
    {
        m_isTeleporting = true;
        InputManager.SetActive(false);
        m_playerMovement.ResetMovement();
        CameraController.IsActivated = false;
        
        m_announcerEventData.Color = m_messagColor;
        m_announcerEventData.Message = "Error!";
        m_announcerEventData.Duration = 0.3f;
        m_announcerEvent.Raise(m_announcerEventData);
        yield return new WaitForSeconds(0.3f);
        
        
        yield return StartCoroutine(PlayTeleportDisappearTween());
        m_playerCollider.transform.position = GetRandomTeleportPosition();
        m_isCooldownTeleport = true;
        CameraController.IsActivated = true;
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(PlayTeleportAppearTween());
        InputManager.SetActive(true);
        StartCoroutine(OnCooldownTeleport());
        m_isTeleporting = false;
    }

    private IEnumerator PlayTeleportDisappearTween()
    {
        m_playerCollider.transform.LeanScale(Vector3.zero, 0.3f)
            .setEase(LeanTweenType.easeOutExpo);
        yield return new WaitForSeconds(0.5f);
    }
    
    private IEnumerator PlayTeleportAppearTween()
    {
        m_playerCollider.transform.LeanScale(Vector3.one, 0.3f)
            .setEase(LeanTweenType.easeInOutCirc);
        yield return new WaitForSeconds(0.5f);
    }
    
    private IEnumerator OnCooldownTeleport()
    {
        yield return new WaitForSeconds(m_cooldownTeleport);
        m_isCooldownTeleport = false;
    }


    private void OnDrawGizmos()
    {
        if (!m_drawDebug) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_teleportRange);
    }
}
