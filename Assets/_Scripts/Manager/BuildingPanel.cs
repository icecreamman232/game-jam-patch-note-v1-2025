using SGGames.Scripts.Core;
using SGGames.Scripts.Ship;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public class BuildingPanel : MonoBehaviour
    {
        [SerializeField] private ShipBuildingHandler m_shipBuildingHandler;
        [SerializeField] private GameObject m_buildingGroup;
        [SerializeField] private Building[] m_buildings;
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
            m_buildingGroup.SetActive(true);
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
