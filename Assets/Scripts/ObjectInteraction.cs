using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MyNamespace;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private GameObject panelObjectDetails;

    private TouchControls controls;
    private Camera arCamera;

    private bool objectPlaced;
    private bool isPressed;

    //private BuildingSO buildingSO;
    private GameObject selectedBuilding;

    private void Awake()
    {
        controls = new TouchControls();

        arCamera = Camera.main;

        //controls.control.touch.performed += _ => isPressed = true;
        //controls.control.touch.canceled += _ => isPressed = false;

        // If there is existing touch input, change the isPressed to true & call the HandleTouchPerform func
        controls.control.touch.performed += context =>
        {
            isPressed = true;
            HandleTouchPerformed();
        };

        // If the touch input stop, change the isPressed to false
        controls.control.touch.canceled += context =>
        {
            isPressed = false;
        };
    }

    private void OnEnable()
    {
        controls.control.Enable();
    }

    private void OnDisable()
    {
        controls.control.Disable();
    }

    public bool isButtonPressed()
    {
        if (EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void HandleTouchPerformed()
    {
        // Call PerformRaycast function when a touch is performed
        PerformRaycast();

        //Debug.Log("Touch performed");
    }

    private void PerformRaycast()
    {
        // Get objectPlaced bool instance from singleton 
        objectPlaced = PlacementHandler.Instance.objectPlaced;

        // Check if there is any pointer device device connected to the system or if there is existing touch input
        if (Pointer.current == null || isPressed == false || objectPlaced == false)
            return;

        // Store the current touch position
        var touchPosition = Pointer.current.position.ReadValue();

        // Define Ray & Raycasthit
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        RaycastHit hitObject;

        // If Raycast hit any object and any UI buttons are not pressed and no any UI Panel is active, do following
        if (Physics.Raycast(ray, out hitObject) && isButtonPressed() == false && !panelObjectDetails.activeInHierarchy)
        {
            // Debug.Log(hitObject.transform.gameObject.name);

            // Access the transform of the hitObject
            Transform building = hitObject.transform;

            // Check the hitObject is building or not by checking Building script component in it. If it's building, do following
            if (building.GetComponent<Building>() != null)
            {
                // Assign the hit object building to the variable, selectedBuilding
                // Give new variable to it for its name
                selectedBuilding = building.gameObject;
                string selectedBuildingName = selectedBuilding.name;

                // Assign the Singleton selected building instance with current hit selectedBuilding
                PlacementHandler.Instance.selectedBuilding = selectedBuilding;

                // Trigger the event where the selected building is changed to whoever wants to subscribe it
                SelectedBuildingManager.TriggerSelectedBuildingChanged(selectedBuildingName);

                //Debug.Log(selectedBuildingName);
            }
        }
    }
}