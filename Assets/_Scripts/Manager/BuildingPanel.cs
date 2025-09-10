using System;
using System.Collections;
using SGGames.Scripts.Core;
using SGGames.Scripts.Ship;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SGGames.Scripts.Managers
{
    public class BuildingPanel : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private PixelPerfectCamera m_pixelPerfectCamera;
        [SerializeField] private Vector2Int m_cameraRefZoomSize;
        [Header("Buildings")]
        [SerializeField] private ShipBuildingHandler m_shipBuildingHandler;
        [SerializeField] private GameObject m_buildingGroup;
        [SerializeField] private Ship.Building[] m_buildings;
        
        private Vector2Int m_originalCameraRefSize;
        private Vector2 m_currentCameraRefSize;
        
        
        private bool m_isOpened;
        
        private void Start()
        {
            var inputManager = ServiceLocator.GetService<InputManager>();
            inputManager.OnClickBuildPanelButton += OnClickBuildPanelButton;
            foreach (var building in m_buildings)
            {
                building.InputHandler.Initialize(m_shipBuildingHandler.GridController);
            }
            m_originalCameraRefSize = new Vector2Int(m_pixelPerfectCamera.refResolutionX, m_pixelPerfectCamera.refResolutionY);
            
            ClosePanel();
        }

        private void OnDestroy()
        {
            var inputManager = ServiceLocator.GetService<InputManager>();
            inputManager.OnClickBuildPanelButton -= OnClickBuildPanelButton;
        }

        private void OnClickBuildPanelButton()
        {
            m_isOpened = !m_isOpened;
            if (m_isOpened)
            {
                OpenPanel();
            }
            else
            {
                ClosePanel();
            }
        }

        private void OpenPanel()
        {
            InputManager.SetActive(false);
            m_buildingGroup.SetActive(true);
            StartCoroutine(ZoomCameraCoroutine(m_cameraRefZoomSize, () =>
            {
                InputManager.SetActive(true);
            }));
            
        }

        private IEnumerator ZoomCameraCoroutine(Vector2 target, Action onFinished)
        {
            var lerp = 0f;
            while (lerp < 1f)
            {
                lerp += Time.deltaTime;
                m_currentCameraRefSize = Vector2.Lerp(m_originalCameraRefSize, target, lerp);
        
                // Apply the interpolated values to the pixel perfect camera
                m_pixelPerfectCamera.refResolutionX = Mathf.RoundToInt(m_currentCameraRefSize.x);
                m_pixelPerfectCamera.refResolutionY = Mathf.RoundToInt(m_currentCameraRefSize.y);
        
                yield return null;
            }
    
            // Ensure final values are set precisely
            m_pixelPerfectCamera.refResolutionX = (int)target.x;
            m_pixelPerfectCamera.refResolutionY = (int)target.y;

            onFinished?.Invoke();
        }
        
        private void ClosePanel()
        {
            m_buildingGroup.SetActive(false);
            foreach (var building in m_buildings)
            {
                building.ResetPosition();
            }
        }
    }
}
