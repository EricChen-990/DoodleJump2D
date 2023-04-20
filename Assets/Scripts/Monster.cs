using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private GameData gd;
    private bool moveLeft;
    private Audio audios;

    public GameObject Player;
    public GameObject ExplosionPS;

    private void Awake(){
        gd = FindObjectOfType<GameData>();
        audios = FindObjectOfType<Audio>();
    }

    private void Update(){
        if (UnityEngine.Camera.main.WorldToScreenPoint(transform.position).y <= 0){
            ResetPos();
        }

        if (UnityEngine.Camera.main.WorldToScreenPoint(transform.position).x > Screen.width - gd.wDeviation){
            moveLeft = true;
        }else if (UnityEngine.Camera.main.WorldToScreenPoint(transform.position).x < gd.wDeviation){
            moveLeft = false;
        }

        ExplosionPS.GetComponent<Transform>().position = transform.position;
    }

    public void ResetPos(){
        int x = UnityEngine.Random.Range(gd.wDeviation, Screen.width - gd.wDeviation);
        int y = UnityEngine.Random.Range(Screen.height - gd.hDeviation, Screen.height + gd.hDeviation);

        Vector3 pos = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0f));
        transform.position = new Vector3(pos.x, pos.y, 0f);
    }

    private void FixedUpdate() {
        transform.position += (moveLeft)? new Vector3(-gd.moveSpeed * Time.deltaTime, 0f, 0f) : new Vector3(gd.moveSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        collision.gameObject.transform.parent = transform;
        collision.gameObject.transform.position = transform.position;
        audios.PlayMonsterSound();
        ExplosionPS.GetComponent<ParticleSystem>().Play();
        Invoke("test",1);
        
    }

    private void test(){
        Player.SetActive(false);
        audios.PlayExplosionPSSound();
    }
}
