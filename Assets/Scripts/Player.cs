using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class Player : MonoBehaviour{
    private Rigidbody2D rb;

    public GameObject explosionPrefab;
    
    public TMP_Text ScoreText;
    public TMP_Text HighScoreText;

    private Audio audios;

    public float moveSpeed = 10f;
    private float moveEvent;
    private float score;
    private float hscore;
    private float offset;
    private float x;

    private bool isdown = false;
    

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        score = 0f;
        hscore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreText.text = $"High Score: {hscore}";
        ScoreText.text = "Score: " + (int)score;
        offset = transform.position.y;
        audios = FindObjectOfType<Audio>();
           
    }

    void Update(){
        moveEvent = Input.GetAxis("Horizontal") * moveSpeed;

        // if (Input.GetAxis("Horizontal") != 0f){
        //    moveEvent = Input.GetAxis("Vertical") * moveSpeed;
        // }else if (Mathf.Abs(Input.gyro.attitude.z) > 0.2f){
        //    moveEvent = Input.acceleration.x * moveSpeed;
        // }

        // if (rb.velocity.y > 0){
        //     GetComponent<SpriteRenderer>();
        // }

        if(moveEvent > 0){
            GetComponent<SpriteRenderer>().flipX = true;
        }else if(moveEvent == 0){
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if(UnityEngine.Camera.main.WorldToScreenPoint(transform.position).y <= 0 && !isdown){
            x = transform.position.x;
            StartCoroutine(DelayThenExplode());
            
        }

        if((int)(transform.position.y - offset) > score){
            score = transform.position.y - offset;
            if (score < 0){
                return;
            }
            ScoreText.text = "Score: " + (int)score;
        }

        if ((int)score > hscore){
            PlayerPrefs.SetInt("HighScore", (int)score);
            HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        }

    }

    private IEnumerator DelayThenExplode()
    {
        isdown = true;
        audios.PlayDownSound();
        yield return new WaitForSeconds(2.5f);
        explosionPrefab.transform.position = new Vector3(x, explosionPrefab.transform.position.y, explosionPrefab.transform.position.z);
        Debug.Log("do");
        explosionPrefab.GetComponent<ParticleSystem>().Play();
    }

    private void FixedUpdate(){
        rb.velocity = new Vector2(moveEvent, rb.velocity.y);
    }

    private void Awake() {
        Input.gyro.enabled = true;
    }
}
