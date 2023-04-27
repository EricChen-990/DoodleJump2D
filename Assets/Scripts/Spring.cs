using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour{
    private GameData gd;
    private Audio audios;
    private Animator animator;
    private bool isRecycle;

    private PrefabPool prefabPool;

    private void Awake() {
        gd = FindObjectOfType<GameData>();
        audios = FindObjectOfType<Audio>();
        animator = GetComponent<Animator>();
        prefabPool = FindObjectOfType<PrefabPool>();
    }

    private void Update() {
        if (UnityEngine.Camera.main.WorldToScreenPoint(transform.position).y <= 0){
            if (isRecycle){
                return;
            }
            GetObjectFromPool();
        }
    }

    private void OnEnable() {
        isRecycle = false;
    }
    private void ResetPos(GameObject obj){
        int heightoffset = 0;
        Vector3 pos;
        
        pos = GeneratePos(heightoffset);
        GameObject gb = prefabPool.GetChickBoxObject();

        gb.SetActive(true);
        pos = GeneratePos(heightoffset);
        gb.transform.position = pos;
        while(!gb.GetComponent<OverlapCheck>().NoObjectsInCollider()){
            heightoffset += 1;
            pos = GeneratePos(heightoffset);
            gb.transform.position = pos;
        }
        prefabPool.RecycleCheckBox(gb);
        Debug.Log("Check ok new pos = " + pos);
        obj.transform.position = new Vector3(pos.x, pos.y, 0f);
        RunRecycle();
    }

    private Vector3 GeneratePos(int hoffset){
        int posX = UnityEngine.Random.Range(gd.wDeviation, Screen.width - gd.wDeviation);
        int posY = UnityEngine.Random.Range(Screen.height - gd.hDeviation, Screen.height + gd.hDeviation);
        posY+= hoffset;

        Vector3 pos = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(posX, posY, 0f));
        pos = new Vector3(pos.x, pos.y, 0f);

        return pos;
    }
    
    private void GetObjectFromPool(){
        isRecycle = true;
        GameObject gobj = prefabPool.GetObject();
        if (gobj != null){
            gobj.SetActive(true);
        }else{
            return;
        }
        ResetPos(gobj);
    }

    private void RunRecycle(){
        prefabPool.Recycle(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.relativeVelocity.y <= 0){
            Vector2 rbVelocity = collision.collider.GetComponent<Rigidbody2D>().velocity;
            rbVelocity.y = gd.storngForce;
            collision.collider.GetComponent<Rigidbody2D>().velocity = rbVelocity;
            animator.SetTrigger("Touched");
            audios.PlaySpringSound();
        }
    }
}


