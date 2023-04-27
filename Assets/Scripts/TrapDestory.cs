using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDestory : MonoBehaviour{
    private PrefabPool prefabPool;

    private void Awake(){
        prefabPool = FindObjectOfType<PrefabPool>();
    }

    void Start(){
        Invoke(nameof(RecycleSelf), 1.5f);
    }

    void RecycleSelf(){
        if (gameObject.name == "TrapLeft(Clone)"){
            prefabPool.RecycleTrapLeft(gameObject);
        }
        if (gameObject.name == "TrapRight(Clone)"){
            prefabPool.RecycleTrapRight(gameObject);
        }
    }
}