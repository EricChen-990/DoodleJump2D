using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    private GameData gd;
    private bool moveLeft;
    private Audio audios;
    private bool isRecycle;

    public GameObject Player;
    public GameObject ExplosionPS;

    private PrefabPool prefabPool;
    private bool isEat = false;

    private void Awake(){
        gd = FindObjectOfType<GameData>();
        audios = FindObjectOfType<Audio>();
        prefabPool = FindObjectOfType<PrefabPool>();
    }

    private void Update(){
        if (UnityEngine.Camera.main.WorldToScreenPoint(transform.position).y <= 0){
            if (isRecycle){
                return;
            }
            GetObjectFromPool();
        }

        if (UnityEngine.Camera.main.WorldToScreenPoint(transform.position).x > Screen.width - gd.wDeviation){
            moveLeft = true;
        }else if (UnityEngine.Camera.main.WorldToScreenPoint(transform.position).x < gd.wDeviation){
            moveLeft = false;
        }

        ExplosionPS.GetComponent<Transform>().position = transform.position;
    }

    private void OnEnable() {
        isRecycle = false;
    }

    public void ResetPos(GameObject obj){
        int highoffset = 0;
        Vector3 pos;
        pos = GeneratePos(highoffset);
        GameObject gb = prefabPool.GetChickBoxObject();
        gb.SetActive(true);
        // pos = GeneratePos(highoffset);
        // gb.transform.position = pos;

        while(!gb.GetComponent<OverlapCheck>().NoObjectsInCollider()){
            highoffset += 1;
            pos = GeneratePos(highoffset);
            gb.transform.position = pos;
        }

        prefabPool.RecycleCheckBox(gb);
        Debug.Log("Check ok new pos = " + pos);
        obj.transform.position = new Vector3(pos.x, pos.y, 0f);
        RunRecycle();
    }

    private Vector3 GeneratePos(int hoffset){
        int x = UnityEngine.Random.Range(gd.wDeviation, Screen.width - gd.wDeviation);
        int y = UnityEngine.Random.Range(Screen.height - gd.hDeviation, Screen.height + gd.hDeviation);
        y+= hoffset;

        Vector3 pos = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0f));
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

    private void FixedUpdate() {
        transform.position += (moveLeft)? new Vector3(-gd.moveSpeed * Time.deltaTime, 0f, 0f) : new Vector3(gd.moveSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        
        if(isEat){
            return;
        }
        isEat = true;

        StartCoroutine(DelayThen(collision));        
    }

    private IEnumerator DelayThen(Collision2D collision){
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        collision.gameObject.transform.parent = transform;
        collision.gameObject.transform.position = transform.position;
        audios.PlayMonsterSound();
        ExplosionPS.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        Player.SetActive(false);
        audios.PlayExplosionPSSound();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("OptionScene");
    }
}
