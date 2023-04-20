using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour{
    public int hDeviation = 80;
    public int wDeviation = 40;
    public float storngForce = 15f;
    public float mediumForce = 10f;
    public float weakForce = 5f;
    public float forceDev = 2.5f;
    public float moveSpeed = 1.5f;

    public GameObject spring;
    public GameObject platfrom;
    public GameObject LeftTrap;
    public GameObject RightTrap;
    public GameObject Monster;

    private void Awake() {
#if UNITY_STANDALONE
    Screen.SetResolution(540, 960, false);
    Screen.fullScreen = false;
#endif
    }
}
