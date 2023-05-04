using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    

    public void PlayGame(){
        SceneManager.LoadScene("MainScenes");
    }

    public void ExitGame(){
        Debug.Log("Exit");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
}
