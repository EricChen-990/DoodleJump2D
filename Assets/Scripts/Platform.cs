using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour{
    private GameData gd;
    private Audio audios;
    private bool soundPlaying;

    private void Awake() {
        gd = FindObjectOfType<GameData>();
        audios = FindObjectOfType<Audio>();
        
    }

    private void Update() {
        if (UnityEngine.Camera.main.WorldToScreenPoint(transform.position).y <= 0){
            ResetPos();
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
            rbVelocity.y = gd.mediumForce;
            collision.collider.GetComponent<Rigidbody2D>().velocity = rbVelocity;
            audios.PlayPlatfromMusic();
        }
        if (collision.gameObject.tag == "Platform"){
            Debug.Log("Platform");
            ResetPos();
        }
        
    } 

    private IEnumerator PlaySoundThenReset(){
        soundPlaying = true;
        audios.PlayPlatfromMusic();
        yield return new WaitForSeconds(1.5f);
        soundPlaying = false;
    }
}
