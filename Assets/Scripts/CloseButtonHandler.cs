using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CloseButtonHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI locationText;
    [SerializeField] private TextMeshProUGUI operationHourText;

    [SerializeField] private GameObject panelInfoUI;
    [SerializeField] private Transform parentPanel;

    private GameObject selectedBuilding;

    public void OnCloseButtonClicked()
    {
        locationText.text = "";
        operationHourText.text = "";

        DeleteTitleInfoElement();
        panelInfoUI.SetActive(false);
        PlacementHandler.Instance.selectedBuilding = selectedBuilding;
        Destroy(selectedBuilding);
    }

    private void DeleteTitleInfoElement()
    {
        // Iterate through the children of the parent UI
        for (int i = parentPanel.childCount - 1; i >= 0; i--)
        {
            Transform child = parentPanel.GetChild(i);
            // Check if the child's name matches "TitleInfoElement"
            if (child.name == "TitleInfoElement(Clone)" || child.name == "TextElement(Clone)" || child.name == "TitleInfoElement - Arabic(Clone)" || child.name == "TextElement - Arabic(Clone)")
            {
                // Destroy the child GameObject
                Destroy(child.gameObject);
            }
        }
    }
}
