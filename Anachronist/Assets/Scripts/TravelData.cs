using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelData : MonoBehaviour
{
    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetVector("_PlayerPosition", transform.position);
    }
}
