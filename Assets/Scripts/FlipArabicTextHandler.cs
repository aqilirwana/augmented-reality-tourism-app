using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RTLTMPro;

public class FlipArabicTextHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        Debug.Log(textMesh.text);

        string arabicText = "مسجد بوترا";

        textMesh.text = arabicText;

        Debug.Log(textMesh.text);
    }

}
