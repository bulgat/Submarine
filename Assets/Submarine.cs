using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{

    private Rigidbody2D rigidbody;

    [Header("kol")]
    private float aSensor;
    private float bSensor;
    private float cSensor;
    float jumpPower = 0.0001f;
    float rayCheckAhead = 15.0f;
    LayerMask collisionMask;

    [Range(-1f, 1f)]
    public float a;
    public float t;

    public float timeSinceStart = 0f;
    private float totalDistanceTravelled;
    private float avgSpeed;

    private Vector3 startPosition;
    private Vector3 lastPosition;
    private Vector3 startRotation;

    [Header("Network Options")]
    public int LAYERS = 1;
    public int NEURONS = 10;
    private NNet network;

    [Header("Fitness")]
    public float overallFitness;

    public float distanceMultipler = 1.4f;
    public float avgSpeedMultiplier = 0.2f;
    public float sensorMultiplier = 0.1f;

    public Camera MainCamera;

    void Awake()
    {
        
        rigidbody = GetComponent<Rigidbody2D>();
        collisionMask = LayerMask.GetMask("BlockSea");
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        network = GetComponent<NNet>();
Debug.Log("Start    network = " + network);
    }
    public void ResetWithNetwork(NNet net)
    {
        network = net;
        Reset();
    }

    void Start()
    {
        
    }
    public void Reset()
    {

        timeSinceStart = 0f;
        totalDistanceTravelled = 0f;
        avgSpeed = 0f;
        lastPosition = startPosition;
        overallFitness = 0f;
        transform.position = startPosition;
        transform.eulerAngles = startRotation;
    }


    private void FixedUpdate()
    {

        InputSensors();
        lastPosition = transform.position;

      //  Debug.Log("  [" + aSensor + "]    " + bSensor + "===   network=[" + network + "]=======" + cSensor);
        (a, t) = network.RunNetwork(aSensor, bSensor, cSensor);


        MoveCar(a, t);

        timeSinceStart += Time.deltaTime;

        CalculateFitness();
    }
        // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Jump();
        }
       
    }



    private void Jump()
    {
        rigidbody.velocity = Vector2.zero;

        rigidbody.AddRelativeForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Death();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Death();
    }
    private void CalculateFitness()
    {

        totalDistanceTravelled += Vector3.Distance(transform.position, lastPosition);
        avgSpeed = totalDistanceTravelled / timeSinceStart;

        overallFitness = (totalDistanceTravelled * distanceMultipler) + (avgSpeed * avgSpeedMultiplier) + (((aSensor + bSensor + cSensor) / 3) * sensorMultiplier);

        if (timeSinceStart > 20 && overallFitness < 40)
        {
            Death();
        }

        if (overallFitness >= 1000)
        {
            Death();
        }

    }
    

  
    private void Death()
    {
        GameObject.FindObjectOfType<GeneticManager>().Death(overallFitness, network);
        Debug.Log("===001 __XXX__| weight =ZZZ== bi  Death=");
    }

    

    private void InputSensors()
    {
        /*
        Vector2 a = (transform.forward + transform.right);
        Vector2 b = (transform.forward+transform.up);
        Vector2 c = (transform.forward - transform.right);
        */

        Vector2 a = (transform.right);
        Vector2 b = (transform.right + transform.up);
        Vector2 c = (transform.right - transform.up);


        Ray r = new Ray(new Vector2(transform.position.x , transform.position.y), a);

        Vector3 directionA = this.transform.right;
      
        //RaycastHit2D hit2D = Physics2D.Raycast(r.origin, directionA, rayCheckAhead);
        RaycastHit2D hit2D = Physics2D.Raycast(r.origin, directionA, rayCheckAhead, collisionMask);
        Debug.DrawLine(r.origin, r.origin + (directionA * rayCheckAhead), Color.red);
        Debug.DrawLine(r.origin, r.origin + (directionA * hit2D.distance), Color.blue);
        aSensor = hit2D.distance;

        
        
        //------------------

        Ray r_c = new Ray(new Vector2(transform.position.x, transform.position.y), b);
        Vector3 directionC = transform.right + transform.up;
        RaycastHit2D hit2D_c = Physics2D.Raycast(r_c.origin, directionC, rayCheckAhead, collisionMask);
        Debug.DrawLine(r_c.origin, r_c.origin + (directionC * hit2D_c.distance), Color.magenta);
        bSensor = hit2D_c.distance;

        //------------------

        Ray r_n = new Ray(new Vector2(transform.position.x, transform.position.y), c);
        Vector3 directionN = transform.right - transform.up;
        RaycastHit2D hit2D_n = Physics2D.Raycast(r_n.origin, directionN, rayCheckAhead, collisionMask);
        Debug.DrawLine(r_n.origin, r_n.origin + (directionN * hit2D_n.distance), Color.yellow);
        cSensor = hit2D_n.distance;
        //------------------


        Debug.Log(hit2D.distance+"===000="+ hit2D_c.distance + " __XXX__ Deat =="+ hit2D_n.distance);


    }
    private Vector3 inp;
    public void MoveCar(float v, float h)
    {
        
        inp = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, v * 11.4f), 0.02f);
        //inp = Vector3.Lerp(new Vector3(0, 0, v * 11.4f), Vector3.zero, 0.02f);
        inp = transform.TransformDirection(inp);
        transform.position += inp;

       // transform.eulerAngles += new Vector3(0, (h * 90) * 0.02f, 0);
        transform.eulerAngles += new Vector3(0, 0, (h * 90) * 0.02f);
        
        float currentSpeed = 2.5f;
        transform.Translate(Vector2.right* currentSpeed * Time.deltaTime);
        MainCamera.transform.position = new Vector3(transform.position.x, transform.position.y,-10);
    }
}
