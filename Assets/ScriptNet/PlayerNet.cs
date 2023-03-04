using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNet : NetworkBehaviour
{
    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(moveX, moveY,0);
            transform.position = transform.position + move;
        }
    }
    void Update()
    {
        HandleMovement();
    }

}
