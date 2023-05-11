using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(exit), 3f);
    }

    void exit(){
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
