using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainScene : MonoBehaviour
{
    public Text FPS;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FPS.text = "FPS = " + (1.0f / Time.smoothDeltaTime);
    }
}
