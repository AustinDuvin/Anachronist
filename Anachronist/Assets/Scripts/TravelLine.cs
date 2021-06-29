using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject waypoint;
    public Vector3 dir;
    public PlayerStats pStat;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, gameObject.transform.position);
        lineRenderer.SetPosition(1, waypoint.transform.position);
        dir = Vector3.Normalize(waypoint.transform.position - gameObject.transform.position);
        dir *= 0.75f;
        pStat = transform.parent.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = Vector3.Normalize(waypoint.transform.position - gameObject.transform.position);
        Vector3 startPointPos = dir * 0.75f;

        lineRenderer.SetPosition(0, gameObject.transform.position + new Vector3(startPointPos.x, 0.1f, startPointPos.z));
        lineRenderer.SetPosition(1, waypoint.transform.position + new Vector3(0.0f, 0.1f, 0.0f));

        RaycastHit hit;

        if (Physics.Raycast(gameObject.transform.position + startPointPos, dir, out hit, Vector3.Magnitude(waypoint.transform.position - gameObject.transform.position)) && hit.transform.gameObject.tag == "Obstruction" || Vector3.Magnitude(waypoint.transform.position - transform.position) > pStat.thisTurnRemainingSpeed)
        {
            ChangeColor(Color.red);//new Color(255, 122, 132));
        }

        else if (gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor") == Color.red)//new Color(255, 122, 132))
        {
            ChangeColor(Color.cyan);//new Color(121, 238, 255));
        }
    }

    public void ChangeColor(Color color)
    {
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }
}
