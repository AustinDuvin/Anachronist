using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGameplayUI : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        transform.position = Camera.main.WorldToScreenPoint(player.transform.position) + new Vector3(80, 150);
    }
}
