using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Background;
    public AudioSource Platfrom;
    public AudioSource Spring;
    public AudioSource Trip;
    public AudioSource Monster;
    public AudioSource DownSound;
    public AudioSource Explosion;
    public AudioSource ExplosionPS;

    public AudioSource CountDown;
    public static Audio _instance;

    private void Awake() {
        if(_instance == null){
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Background.loop = true;
        Background.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPlatfromMusic(){
        Platfrom.Play();
    }

    public void PlaySpringSound(){
        Spring.Play();
    }

    public void PlayTripSound(){
        Trip.Play();
    }

    public void PlayMonsterSound(){
        Monster.Play();
    }

    public void PlayDownSound(){
        DownSound.Play();
    }

    public void PlayExplosionSound(){
        
    }

    public void PlayExplosionPSSound(){
        ExplosionPS.Play();
    }

    public void PlayCountDownSound(){
        CountDown.Play();
    }
}
