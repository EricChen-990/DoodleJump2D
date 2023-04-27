using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour{
    private GameData gd;
    private Audio audios;
    private Animator animator;

    private void Awake() {
        gd = FindObjectOfType<GameData>();
        audios = FindObjectOfType<Audio>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (UnityEngine.Camera.main.WorldToScreenPoint(transform.position).y <= 0){
            if ((UnityEngine.Random.value * 10) > 5){
                ResetPos();
            }
        }
    }

    public void ResetPos() {
        int x = UnityEngine.Random.Range(gd.wDeviation, Screen.width - gd.wDeviation);
        int y = UnityEngine.Random.Range(Screen.height - gd.hDeviation, Screen.height + gd.hDeviation);

        Vector3 pos = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0f));
        transform.position = new Vector3(pos.x, pos.y, 0f);
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


