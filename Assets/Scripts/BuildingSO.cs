using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingSO", menuName = "BuildingSO")]
public class BuildingSO : ScriptableObject
{
    public Sprite mainImage;

    public new string name;

    // List of custom building attributes
    public List<BuildingAttribute> attributes = new List<BuildingAttribute>();
}
