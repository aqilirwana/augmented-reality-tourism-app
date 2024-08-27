using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

public class PlacementHandler : MonoBehaviour
{
    // Singleton instance
    private static PlacementHandler instance;

    [SerializeField] GameObject placementIndicator;

    [SerializeField] private GameObject placedMap;

    [SerializeField] InputAction touchInput;

    [SerializeField] GameObject movePhoneUI;
    [SerializeField] GameObject moreInfoUI;

    private ObjectInteraction objectInteraction;

    public bool objectPlaced = false;
    //public BuildingSO buildingData;
    public GameObject selectedBuilding;

    ARRaycastManager aRRaycastManager;
    ARPlaneManager arPlaneManager;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        objectInteraction = GetComponent<ObjectInteraction>();

        if(objectInteraction != null)
        {
            objectInteraction.enabled = false;
        }
        else
        {
            Debug.LogError("ScriptComponent not found on the same GameObject.");
        }
    }

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();

        arPlaneManager = FindObjectOfType<ARPlaneManager>();

        // Subscribe the PlaceObject method to the performed event of the touchInput
        touchInput.performed += OnTouchPerformed;

        placementIndicator.SetActive(false);

    }

    private void OnEnable()
    {
        touchInput.Enable();

        // Enable plane detection when the script is enabled
        arPlaneManager.enabled = true;
    }

    private void OnDisable()
    {
        touchInput.Disable();

        // Disable plane detection when the script is disabled
        arPlaneManager.enabled = false;
    }

    private void OnTouchPerformed(InputAction.CallbackContext context)
    {
        // Call the PlaceObject method when the touchInput action is performed
        PlaceObject();
    }

    // Public property to access the instance
    public static PlacementHandler Instance
    {
        get
        {
            // If the instance doesn't exist, find or create it
            if (instance == null)
            {
                instance = FindObjectOfType<PlacementHandler>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<PlacementHandler>();
                }
            }
            return instance;
        }
    }

    private void Update()
    {
        // If object is placed, there's no more AR indicator
        if (!objectPlaced && aRRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            placementIndicator.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);

            if (!placementIndicator.activeInHierarchy)
                placementIndicator.SetActive(true);
            movePhoneUI.SetActive(false);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    public void PlaceObject()
    {
        if (!placementIndicator.activeInHierarchy)
            return;

        GameObject _object = Instantiate(placedMap);
        _object.transform.position = placementIndicator.transform.position;
        _object.transform.rotation = placementIndicator.transform.rotation;

        // Make the object face the camera
        Vector3 cameraPosition = Camera.main.transform.position;
        _object.transform.LookAt(new Vector3(cameraPosition.x, _object.transform.position.y, cameraPosition.z));

        objectPlaced = true;

        // Disable plane detection after object placement
        arPlaneManager.enabled = false;

        moreInfoUI.SetActive(true);

        foreach (var planes in arPlaneManager.trackables)
        {
            planes.gameObject.SetActive(false);
        }

        if (objectInteraction != null)
        {
            // Invoke the method to enable the script component after 3 seconds
            Invoke("EnableObjectInteraction", 1f);
        }
        else
        {
            Debug.LogError("ScriptComponent not found on the same GameObject.");
        }
    }

    void EnableObjectInteraction()
    {
        objectInteraction.enabled = true;
    }
}
