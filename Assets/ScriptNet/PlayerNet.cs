using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNet : NetworkBehaviour
{
    public GameObject SpiteSubmarine;
    public GameObject BattleShip;
    public GameObject BombPrefab;

    public Rigidbody2D rigidbody2D;
    public float speed = 30f;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        Debug.Log("isClientOnly" + isClientOnly);
        Debug.Log("isServerOnly" + isServerOnly);
        Debug.Log("isClient" + isClient);
        Debug.Log("SERVER"+ isServer);
        if (isServerOnly)
        {

            BattleShip.SetActive(true);
            SpiteSubmarine.SetActive(false);
            transform.position = new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z);
        }
        else {
            BattleShip.SetActive(false);
            SpiteSubmarine.SetActive(true);
            transform.position = new Vector3(transform.position.x, transform.position.y-2, transform.position.z);
        }

        //BattleShip.SetActive(false);isClientisServerOnly   isClientOnly
    }
    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(moveX, moveY,0);
            //transform.position = transform.position + move;
            //Debug.Log("START");
            rigidbody2D.MovePosition(rigidbody2D.position+Vector2.right* moveX* speed* Time.deltaTime);

            if (Input.GetKeyDown("space"))
            {
                var bombOne = Instantiate(BombPrefab, 
                    new Vector3(rigidbody2D.position.x,
                    rigidbody2D.position.y-1.0f,
                    0), Quaternion.identity); ;
                Destroy(bombOne,5);
                
            }
                
        }
    }
    void Update()
    {
        HandleMovement();
    }
    public override void OnStartLocalPlayer()
    {
        //BattleShip.SetActive(false);
        //SpiteSubmarine.SetActive(true);
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (isServer)
        {
            //BattleShip.SetActive(true);
            //SpiteSubmarine.SetActive(false);
            //transform.position = new Vector3(transform.position.x, transform.position.y + 6.7f, transform.position.z);
            //SpiteSubmarine.GetComponent<SpriteRenderer>().color = Color.red;
            base.OnStartLocalPlayer();
        } 
    } 

}
