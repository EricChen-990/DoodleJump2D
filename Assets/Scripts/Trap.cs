using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private GameData gd;
    private Audio audios;

    private void Awake(){
        gd = FindObjectOfType<GameData>();
        audios = FindObjectOfType<Audio>();
    }

    private void Update(){
        if (UnityEngine.Camera.main.WorldToScreenPoint(transform.position).y <= 0){
            ResetPos();
        }
    }

    public void ResetPos(){
        int x = UnityEngine.Random.Range(gd.wDeviation, Screen.width - gd.wDeviation);
        int y = UnityEngine.Random.Range(Screen.height - gd.hDeviation, Screen.height + gd.hDeviation);

        Vector3 pos = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0f));
        transform.position = new Vector3(pos.x, pos.y, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        float bounrceForce = UnityEngine.Random.Range(gd.weakForce - gd.forceDev, gd.weakForce + gd.forceDev);
        if (collision.relativeVelocity.y <= 0){
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + bounrceForce);
            
            GameObject objL = Instantiate(gd.LeftTrap, new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z), Quaternion.identity);

            GameObject objR = Instantiate(gd.RightTrap, new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z), Quaternion.identity);

            Destroy(gameObject);
            audios.PlayTripSound();
        }
        
        
    }
}
