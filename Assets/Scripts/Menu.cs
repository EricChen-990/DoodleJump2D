using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    private RectTransform countDownPanel;
    private RectTransform MenuPanel;
    private Canvas canvas;
    private TMP_Text countdowntext;
    private Audio audios;

    [System.Obsolete]
    private void Awake() {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        countDownPanel = canvas.transform.FindChild("CountDownPanel").GetComponent<RectTransform>();
        MenuPanel = canvas.transform.FindChild("MenuPanel").GetComponent<RectTransform>();
        countdowntext = countDownPanel.FindChild("CountDownText").GetComponent<TextMeshProUGUI>();
        countDownPanel.gameObject.SetActive(false);
        MenuPanel.gameObject.SetActive(true);
        audios = FindObjectOfType<Audio>();
    }
    

    public void PlayGame(){
        StartCoroutine(CountdownToPlay());
        
        
    }

    private IEnumerator CountdownToPlay(){
        yield return new WaitForSeconds(0.5f);
        MenuPanel.gameObject.SetActive(false);
        countDownPanel.gameObject.SetActive(true);
        audios.PlayCountDownSound();
        yield return new WaitForSeconds(1);
        countdowntext.text = "2";
        audios.PlayCountDownSound();
        yield return new WaitForSeconds(1);
        countdowntext.text = "1";
        audios.PlayCountDownSound();
        yield return new WaitForSeconds(1);
        countdowntext.text = "GO!";
        SceneManager.LoadScene("MainScenes");
    }

    public void exit(){
        Debug.Log("Exit");
        SceneManager.LoadScene("End");
    }
}
