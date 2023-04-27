using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private GameData gd;
    private Audio audios;
    private PrefabPool prefabPool;
    private bool isRecycle;
    private bool stepped;

    private void Awake(){
        gd = FindObjectOfType<GameData>();
        audios = FindObjectOfType<Audio>();
        prefabPool = FindObjectOfType<PrefabPool>();
    }

    private void OnEnable() {
        isRecycle = false;
        stepped = false;
    }

    private void Update(){
        if (UnityEngine.Camera.main.WorldToScreenPoint(transform.position).y <= 0){
            if (isRecycle){
                return;
            }
            GetObjectFromPool();
        }
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
        float bounrceForce = UnityEngine.Random.Range(gd.weakForce - gd.forceDev, gd.weakForce + gd.forceDev);
        if (collision.relativeVelocity.y <= 0){

            if(stepped){
                return;
            }

            stepped = true;
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + bounrceForce);
            GameObject lgb = prefabPool.GetTrapLeftobject();
            lgb.SetActive(true);
            lgb.transform.position = new Vector3(transform.position.x - 0.4f, transform.position.y, transform.position.z);
            
            GameObject rgb = prefabPool.GetTrapRightobject();
            rgb.SetActive(true);
            rgb.transform.position = new Vector3(transform.position.x + 0.4f, transform.position.y, transform.position.z);

            Destroy(gameObject);
            audios.PlayTripSound();
            RunRecycle();
        }
        
        
    }
}
