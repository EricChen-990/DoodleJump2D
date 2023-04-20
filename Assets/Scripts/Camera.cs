using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera : MonoBehaviour{
    public Transform player;
    // Start is called before the first frame update
    void Start(){
        
    }

    
    // Update is called once per frame
    void Update(){
        if (transform.position.y < player.position.y){
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
    
}
