using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Option : MonoBehaviour{

    private RectTransform countDownPanel;
    private RectTransform OptionPanel;
    private Canvas canvas;
    private TMP_Text countdowntext;
    private Audio audios;

    [System.Obsolete]
    private void Awake() {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        countDownPanel = canvas.transform.FindChild("CountDownPanel").GetComponent<RectTransform>();
        OptionPanel = canvas.transform.FindChild("OptionPanel").GetComponent<RectTransform>();
        countdowntext = countDownPanel.FindChild("CountDownText").GetComponent<TextMeshProUGUI>();
        countDownPanel.gameObject.SetActive(false);
        OptionPanel.gameObject.SetActive(true);
        audios = FindObjectOfType<Audio>();
    }

    private IEnumerator CountdownToPlay(){
        yield return new WaitForSeconds(0.5f);
        OptionPanel.gameObject.SetActive(false);
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

    public void playagain(){
        StartCoroutine(CountdownToPlay());
    }

    public void exit(){
        Debug.Log("Exit");
        SceneManager.LoadScene("End");
    }
}
