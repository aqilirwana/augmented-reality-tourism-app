using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingSO buildingSO;

    public BuildingSO GetBuildingSO()
    {
        return buildingSO;
    }
}
