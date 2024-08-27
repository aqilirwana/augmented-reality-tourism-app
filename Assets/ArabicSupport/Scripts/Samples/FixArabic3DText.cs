using UnityEngine;
using System.Collections;
using ArabicSupport;
using TMPro;
using UnityEngine.UI;

public class FixArabic3DText : MonoBehaviour {

    public bool showTashkeel = true;
    public bool useHinduNumbers = true;

    // Use this for initialization
    void Start () {
        //TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        TextMeshProUGUI textMesh = gameObject.GetComponent<TextMeshProUGUI>();

        string fixedText = ArabicFixer.Fix(textMesh.text, showTashkeel, useHinduNumbers);

        //gameObject.GetComponent<TextMesh>().text = fixedText;
        gameObject.GetComponent<TextMeshProUGUI>().text = fixedText;

        Debug.Log(fixedText);
    }

}
