using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public Vector3 destination;
    private bool shouldMove;
    private Rigidbody rb;
    private Vector3 move;
    public float distanceToDestination;
    public Waypoint waypoint;
    public PlayerStats pStat;
    public Projector pro;
    public Vector3 originalPos;
    private CharacterController cCon;
    private Vector3 moveVector;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        shouldMove = false;
        rb = gameObject.GetComponent<Rigidbody>();
        distanceToDestination = 0.0f;
        pStat = gameObject.GetComponent<PlayerStats>();
        originalPos = transform.position;
        cCon = gameObject.GetComponent<CharacterController>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector.Set(0.0f, 0.0f, 0.0f);// = new Vector3(0.0f, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.W))// && Vector3.Magnitude((transform.position + new Vector3(0.0f, transform.position.y, 10.0f)) - originalPos) < pStat.maxSpeed)
        {
            //Push(new Vector3(0.0f, 0.0f, 100.0f));
            //moveVector = new Vector3(0.0f, 0.0f, 10.0f);
            moveVector.z += 10.0f;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            //Push(new Vector3(0.0f, 0.0f, -100.0f));
            //moveVector = new Vector3(0.0f, 0.0f, -10.0f);
            moveVector.z -= 10.0f;
        }

        else if (Input.GetKey(KeyCode.A))// && Vector3.Magnitude((transform.position + new Vector3(-10.0f, transform.position.y, 0.0f)) - originalPos) < pStat.maxSpeed)
        {
            //Push(new Vector3(-100.0f, 0.0f, 0.0f));
            //moveVector = new Vector3(-10.0f, 0.0f, 0.0f);
            moveVector.x -= 10.0f;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            //Push(new Vector3(100.0f, 0.0f, 0.0f));
            //moveVector = new Vector3(10.0f, 0.0f, 0.0f);
            moveVector.x += 10.0f;
        }

        cCon.Move(moveVector * Time.deltaTime);

        if (Vector3.Magnitude(transform.position - originalPos) > pStat.maxSpeed)//new Vector3(originalPos.x, transform.position.y, originalPos.z)) > pStat.maxSpeed)
        {
            transform.position = originalPos + Vector3.Normalize(lastPosition - originalPos) * pStat.maxSpeed;
        }
        //pStat.remainingSpeed = pStat.maxSpeed - Vector3.Magnitude(originalPos - transform.position);
        //pro.orthographicSize = pStat.remainingSpeed;

        if (!waypoint.IsObstructed() && Vector3.Magnitude(waypoint.transform.position - transform.position) <= pStat.thisTurnRemainingSpeed && Input.GetMouseButtonDown(0) && waypoint.IsMoving())
        {
            destination = waypoint.SetPosition(false);
        }

        if (Input.GetKeyDown(KeyCode.Return) && !waypoint.IsMoving())//Input.GetMouseButtonDown(0))
        {
            //RaycastHit hit;
            //Vector2 tLoc = Input.mousePosition;

            // Raycasts from point on screen where first touch this frame is detected
            /*if (Physics.Raycast(Camera.main.ScreenPointToRay(tLoc), out hit))
            {
                destination = hit.point;
                shouldMove = true;
            }*/
            shouldMove = true;
        }

        distanceToDestination = Vector3.Magnitude(destination - gameObject.transform.position);

        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {
            MoveToPoint(destination, 30.0f);

            if (distanceToDestination < 0.5f)
            {
                //rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                rb.MovePosition(destination);
                shouldMove = false;
                waypoint.SetPosition(true);
                /*pStat.remainingSpeed = pStat.maxSpeed;
                pro.orthographicSize = pStat.remainingSpeed;*/
                pStat.thisTurnRemainingSpeed = pStat.remainingSpeed;
                originalPos = transform.position;
            }
        }
    }

    private void Push(Vector3 movement)
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(movement * Time.deltaTime);
        rb.velocity += movement * Time.deltaTime;
        pStat.remainingSpeed = pStat.maxSpeed - Vector3.Magnitude(originalPos - transform.position);
        pro.orthographicSize = pStat.remainingSpeed;
    }

    private void MoveToPoint(Vector3 point, float magnitude)
    {
        Vector3 direction = Vector3.Normalize(point - gameObject.transform.position);
        //gameObject.GetComponent<Rigidbody>().AddForce(direction * magnitude);
        rb.MovePosition(transform.position + (direction * magnitude * Time.deltaTime));

        Vector3.ClampMagnitude(gameObject.GetComponent<Rigidbody>().velocity, 100.0f);
        //pStat.remainingSpeed = Vector3.Magnitude(waypoint.transform.position - transform.position);
        //pro.orthographicSize = pStat.remainingSpeed;
        pStat.remainingSpeed = pStat.thisTurnRemainingSpeed - Vector3.Magnitude(originalPos - transform.position);
        pro.orthographicSize = pStat.remainingSpeed;
    }
}
