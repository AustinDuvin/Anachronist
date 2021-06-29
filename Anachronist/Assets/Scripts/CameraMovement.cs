using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        moveVector = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector.Set(0.0f, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.UpArrow))
        {;
            moveVector.z += 10.0f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVector.z -= 10.0f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector.x -= 10.0f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVector.x += 10.0f;
        }

        MoveCamera(moveVector);
    }

    private void MoveCamera(Vector3 Movement)
    {
        transform.position += Movement * Time.deltaTime;
    }
}
