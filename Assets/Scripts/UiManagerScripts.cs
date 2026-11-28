using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]

public class UIManagerScripts : MonoBehaviour
{
    private string levelToLoad;
    public AudioClip beep;

    void playBtnSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = beep;
        audio.Play();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(levelToLoad);
    }

    public void PlayBtnClicked()
    {
        levelToLoad = "Level 1";
        playBtnSound();
        StartCoroutine(Wait());
        GameObject myLoading = GameObject.Find("Loading");
        myLoading.GetComponent<Animator>().SetBool("IsPressed", true);

        GameObject myMoveCamera = GameObject.Find("Main Camera");
        myMoveCamera.GetComponent<Animator>().SetBool("IsPress", true);

        GameObject myTitle = GameObject.Find("Title");
        myTitle.GetComponent<Animator>().SetBool("IsPress", true);

        GameObject myPanel = GameObject.Find("Panel");
        myPanel.GetComponent<Animator>().SetBool("IsPress", true);
    }

    public void LoadPlayBtnClicked()
    {
        //levelToLoad = "Level 1";
        playBtnSound();
        //StartCoroutine(Wait());
        //GameObject myCamera = GameObject.Find("Main Camera");
        //myCamera.GetComponent<Animator>().SetBool("PlayerPlay", true);
    }

    public void OptionBtnClicked()
    {
        //levelToLoad = "Level 1";
        playBtnSound();
        //StartCoroutine(Wait());
        //GameObject myCamera = GameObject.Find("Main Camera");
        //myCamera.GetComponent<Animator>().SetBool("PlayerPlay", true);
    }

    public void QuitBtnClicked()
    {
        levelToLoad = "";
        playBtnSound();
        Application.Quit();
        Debug.Log("This app can quit hehe");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
