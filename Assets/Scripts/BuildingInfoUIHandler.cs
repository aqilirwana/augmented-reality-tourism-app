using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyNamespace;

public class BuildingInfoUIHandler : MonoBehaviour
{
    // Initiate variables
    private string buildingName;
    private Sprite buildingImage;
    private string buildingLocation;
    private string buildingOperationHour;

    private GameObject selectedBuilding;
    private BuildingSO buildingSO;

    // Make gameobjects in Inspector
    [SerializeField] private GameObject panelObjectDetails;
    [SerializeField] private TextMeshProUGUI mainName;
    [SerializeField] private Image imageBuilding;
    [SerializeField] private TextMeshProUGUI locationText;
    [SerializeField] private TextMeshProUGUI operationHourText;

    [SerializeField] private GameObject attributeNamePrefab;
    [SerializeField] private GameObject attributeDetailsPrefab;
    [SerializeField] private Transform parentPanel;
    [SerializeField] private RectTransform parentPanelRectTransform;

    private void OnEnable()
    {
        SelectedBuildingManager.OnSelectedBuildingChanged += UpdatePanelInfoDisplay;
    }

    private void OnDisable()
    {
        SelectedBuildingManager.OnSelectedBuildingChanged -= UpdatePanelInfoDisplay;
    }


    private void UpdatePanelInfoDisplay(string currentBuilding)
    {
        //Debug.Log(currentBuilding);
        selectedBuilding = PlacementHandler.Instance.selectedBuilding;

        if (currentBuilding == selectedBuilding.name)
        {
            if (selectedBuilding.GetComponent<Building>() != null)
            {
                buildingSO = selectedBuilding.GetComponent<Building>().GetBuildingSO();

                //Debug.Log(buildingSO.name);

                // Assign the selected building data; name & image to the UI
                buildingName = buildingSO.name;
                mainName.text = buildingName;

                buildingImage = buildingSO.mainImage;
                imageBuilding.sprite = buildingImage;

                // Check each of attribute data since building data has few attribute names & details
                foreach (var attribute in buildingSO.attributes)
                {
                    // If attribute is "Location" do following
                    if (attribute.attributeName == "Location" || attribute.attributeName == "الموقع")
                    {
                        // Assign the data text of that attribute to the locationText UI
                        buildingLocation = attribute.attributeDetails;
                        locationText.text = buildingLocation;
                    }
                    // If attribute is "Operation Hours" do following
                    else if (attribute.attributeName == "Operation Hours" || attribute.attributeName == "Check In & Check Out" || attribute.attributeName == "دوام العمل" || attribute.attributeName == "تسجيل الوصول وتسجيل المغادرة")
                    {
                        // Assign the data text of that attribute to the operationHourText UI
                        buildingOperationHour = attribute.attributeDetails;
                        operationHourText.text = buildingOperationHour;
                    }
                    // If none of above, do following
                    else
                    {
                        //Debug.Log($"Attribute: {attribute.attributeName}, Details: {attribute.attributeDetails}");

                        string attributeNameString = attribute.attributeName;
                        string attributeDetailsString = attribute.attributeDetails;

                        // Instantiate attribute name prefab
                        GameObject newAttributeNamePrefab = InstantiateAttributeNamePrefab(attributeNameString);

                        // Instantiate attribute details prefab
                        GameObject newAttributeDetailsPrefab = InstantiateAttributeDetailsPrefab(attributeDetailsString);

                        // Setup PlusIcon button listener
                        SetupPlusButtonListener(newAttributeNamePrefab, newAttributeDetailsPrefab);

                        // Setup MinusIcon button listener
                        SetupMinusButtonListener(newAttributeNamePrefab, newAttributeDetailsPrefab);
                    }
                }

                panelObjectDetails.SetActive(true);

                // Force the layout rebuild
                LayoutRebuilder.ForceRebuildLayoutImmediate(parentPanelRectTransform);
            }
        }
    }

    // Function that create/clone attribute name prefab then set its parent & child's text. This function return the attribute name prefab object itself
    private GameObject InstantiateAttributeNamePrefab(string attributeName)
    {
        GameObject newAttributeName = Instantiate(attributeNamePrefab, parentPanel);
        Transform titleInfoElement = newAttributeName.transform;

        Transform textElement = titleInfoElement.GetChild(0);
        Transform textBox = textElement.GetChild(0);
        Transform textTMP = textBox.GetChild(0);

        TextMeshProUGUI attributeNameText = textTMP.GetComponent<TextMeshProUGUI>();
        if (attributeNameText != null)
        {
            attributeNameText.text = attributeName;
        }

        return newAttributeName;
    }

    // Function that create/clone attribute details prefab then set its parent & child's text. This function return the attribute details prefab object itself
    private GameObject InstantiateAttributeDetailsPrefab(string attributeDetails)
    {
        GameObject newAttributeDetails = Instantiate(attributeDetailsPrefab, parentPanel);
        newAttributeDetails.SetActive(false);
        Transform textElement = newAttributeDetails.transform;

        Transform textBox = textElement.GetChild(0);
        Transform textTMP = textBox.GetChild(0);

        TextMeshProUGUI attributeDetailsText = textTMP.GetComponent<TextMeshProUGUI>();
        if (attributeDetailsText != null)
        {
            attributeDetailsText.text = attributeDetails;
        }

        return newAttributeDetails;
    }

    // Setup listener of button onclick event for PlusIcon button in the attribute name UI
    private void SetupPlusButtonListener(GameObject attributeName, GameObject attributeDetails)
    {
        Transform titleInfoElement = attributeName.transform;

        Transform plusIcon = titleInfoElement.GetChild(1);
        GameObject plusIconObject = plusIcon.gameObject;

        Transform minusIcon = titleInfoElement.GetChild(2);
        GameObject minusIconObject = minusIcon.gameObject;

        Button buttonPlusIcon = plusIcon.GetComponentInChildren<Button>();
        if (buttonPlusIcon != null)
        {
            buttonPlusIcon.onClick.AddListener(() => OnPlusIconButtonClicked(plusIconObject, minusIconObject, attributeDetails));
        }
        //Debug.Log("Listener is setup to the PlusCircleButton!");
    }

    // Setup listener of button onclick event for MinusIcon button in the attribute name UI
    private void SetupMinusButtonListener(GameObject attributeName, GameObject attributeDetails)
    {
        Transform titleInfoElement = attributeName.transform;

        Transform plusIcon = titleInfoElement.GetChild(1);
        GameObject plusIconObject = plusIcon.gameObject;

        Transform minusIcon = titleInfoElement.GetChild(2);
        GameObject minusIconObject = minusIcon.gameObject;

        Button buttonMinusIcon = minusIcon.GetComponentInChildren<Button>();
        if (buttonMinusIcon != null)
        {
            buttonMinusIcon.onClick.AddListener(() => OnMinusIconButtonClicked(plusIconObject, minusIconObject, attributeDetails));
        }
        //Debug.Log("Listener is setup to the MinusCircleButton!");
    }

    // This function will do following once PlusIcon button is pressed
    private void OnPlusIconButtonClicked(GameObject plusIcon, GameObject minusIcon, GameObject attributeDetails)
    {
        //Debug.Log("PlusCircleButton, clicked!");

        plusIcon.SetActive(false);
        minusIcon.SetActive(true);

        attributeDetails.SetActive(true);

        // Optionally, force layout rebuild here if needed
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentPanelRectTransform);
    }

    // This function will do following once MinusIcon button is pressed
    private void OnMinusIconButtonClicked(GameObject plusIcon, GameObject minusIcon, GameObject attributeDetails)
    {
        //Debug.Log("MinusCircleButton, clicked!");

        plusIcon.SetActive(true);
        minusIcon.SetActive(false);

        attributeDetails.SetActive(false);

        // Optionally, force layout rebuild here if needed
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentPanelRectTransform);
    }
}
