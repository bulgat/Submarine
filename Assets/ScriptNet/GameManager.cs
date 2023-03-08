using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour 
{
    [SyncVar]
    public int GlobalMoney;
    [SyncVar]
    public SeaModel seaModel;

    public Text TextGlobalMoney;
    public Text TextSeaModel;
    void Start()
    {
        seaModel = new SeaModel();
    }

    // Update is called once per frame
    void Update()
    {
        TextGlobalMoney.text = "Score :" + GlobalMoney;
        TextSeaModel.text = "Step :" + seaModel.SeaStep;
    }
}
