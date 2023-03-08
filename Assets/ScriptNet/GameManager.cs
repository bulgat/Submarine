using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour 
{
    [SyncVar]
    public int GlobalMoney;
    public Text TextGlobalMoney;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextGlobalMoney.text = "Score :" + GlobalMoney;
    }
}
