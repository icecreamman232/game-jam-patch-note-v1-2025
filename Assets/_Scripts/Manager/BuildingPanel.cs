using System;
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
        [SerializeField] private ShipMovement m_shipMovement;
        [SerializeField] private ShipBuildingHandler m_shipBuildingHandler;
        [SerializeField] private GameObject m_buildingGroup;
        [SerializeField] private Ship.Building[] m_buildings;
        
        
        private bool m_isOpened;
        
        private void Start()
        {
            var inputManager = ServiceLocator.GetService<InputManager>();
            inputManager.OnClickBuildPanelButton += OnClickBuildPanelButton;
            foreach (var building in m_buildings)
            {
                building.InputHandler.Initialize(m_shipBuildingHandler.GridController);
            }
            
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
            m_shipMovement.SetMovementPermission(false);
            m_buildingGroup.SetActive(true);
            PlayZoomInTween(() =>
            {
                InputManager.SetActive(true);
            });
        }
        
        private void ClosePanel()
        {
            PlayZoomOutTween(() =>
            {
                InputManager.SetActive(true);
                m_buildingGroup.SetActive(false);
                foreach (var building in m_buildings)
                {
                    building.ResetPosition();
                }
                m_shipMovement.SetMovementPermission(true);
            });
        }

        private void PlayZoomInTween(Action onComplete)
        {
            var targetAssetPPU = 192;
            var startAssetPPU = m_pixelPerfectCamera.assetsPPU;
            var duration  = 0.5f;
            LeanTween.value(startAssetPPU, targetAssetPPU, duration)
                .setEase(LeanTweenType.easeOutCirc)
                .setOnUpdate((val) =>
                {
                    m_pixelPerfectCamera.assetsPPU = (int)val;
                })
                .setOnComplete(()=>
                {
                    m_pixelPerfectCamera.assetsPPU = targetAssetPPU;
                    onComplete?.Invoke();
                });
        }
        
        private void PlayZoomOutTween(Action onComplete)
        {
            var targetAssetPPU = 64;
            var startAssetPPU = m_pixelPerfectCamera.assetsPPU;
            var duration  = 0.2f;
            LeanTween.value(startAssetPPU, targetAssetPPU, duration)
                .setEase(LeanTweenType.easeInCirc)
                .setOnUpdate((val) =>
                {
                    m_pixelPerfectCamera.assetsPPU = (int)val;
                })
                .setOnComplete(()=>
                {
                    m_pixelPerfectCamera.assetsPPU = targetAssetPPU;
                    onComplete?.Invoke();
                });
        }
    }
}
