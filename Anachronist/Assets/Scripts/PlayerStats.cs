using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxSpeed;
    public float thisTurnRemainingSpeed;
    public float remainingSpeed;
    public Projector pro;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 10;
        thisTurnRemainingSpeed = remainingSpeed = maxSpeed;
        pro.orthographicSize = remainingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayer();
        }
    }

    private void ResetPlayer()
    {
        thisTurnRemainingSpeed = remainingSpeed = maxSpeed;
        pro.orthographicSize = maxSpeed;//remainingSpeed;
        //gameObject.GetComponent<AddForce>().originalPos = transform.position;
    }
}
