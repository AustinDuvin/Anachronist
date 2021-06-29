using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour
{
    public GameObject terrain;
    public GameObject playerObject;
    private bool shouldMove;
    private TravelLine travelLine;
    private bool isObstructed;

    // Start is called before the first frame update
    void Start()
    {
        shouldMove = true;
        travelLine = GameObject.Find("TravelLine").GetComponent<TravelLine>();
        isObstructed = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector2 tLoc = Input.mousePosition;

        // Raycasts from point on screen where first touch this frame is detected
        if (shouldMove)
        {
            Vector3 dir = Vector3.Normalize(gameObject.transform.position - playerObject.transform.position);

            if (Physics.Raycast(Camera.main.ScreenPointToRay(tLoc), out hit))// && hit.transform.gameObject == terrain)
            {
                gameObject.transform.position = hit.point;
            }

            if (Physics.Raycast(playerObject.transform.position, dir * 0.75f, out hit) && hit.transform.gameObject.tag == "Obstruction")
            {
                isObstructed = true;
            }

            else
            {
                isObstructed = false;
            }
        }
    }

    public Vector3 SetPosition(bool s)
    {
        shouldMove = s;

        return gameObject.transform.position;
    }

    public bool IsMoving()
    {
        return shouldMove;
    }

    public bool IsObstructed()
    {
        return isObstructed;
    }
}
