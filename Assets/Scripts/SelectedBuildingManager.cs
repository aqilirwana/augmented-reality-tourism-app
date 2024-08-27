using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNamespace
{
    public class SelectedBuildingManager : MonoBehaviour
    {
        // Define the delegate for the event
        public delegate void SelectedBuildingChangedEventHandler(string currentBuilding);

        // Define the event using the delegate
        public static event SelectedBuildingChangedEventHandler OnSelectedBuildingChanged;

        //Method trigger event
        public static void TriggerSelectedBuildingChanged(string currentBuilding)
        {
            if (OnSelectedBuildingChanged != null)
            {
                OnSelectedBuildingChanged(currentBuilding);
            }
        }
    }
}

