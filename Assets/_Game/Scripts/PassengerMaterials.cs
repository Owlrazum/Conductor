using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerMaterials : MonoBehaviour
{
    [SerializeField]
    private List<Material> rndMaterials;

    private List<int> rnds;
    private List<int> initial;


    private void Awake()
    {
        initial = new List<int> { 0, 1, 2, 3 };
        rnds = new List<int>(initial);
    }

    public Material GetRandomMaterial()
    {
        int rndIndex = Random.Range(0, rnds.Count);
        int rnd = rnds[rndIndex];
        rnds.RemoveAt(rndIndex);
        if (rnds.Count == 0)
        {
            rnds = new List<int>(initial);
        }
        return rndMaterials[rnd];
    }
}
