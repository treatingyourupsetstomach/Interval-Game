using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public AudioClip successSFX;
    public AudioClip failSFX;
    public AudioClip successVoiceSFX;
    public AudioClip failVoiceSFX;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void LoadSpesificScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextlevel2()); // StartCorutine må calle for at IEmuerator skal runne
    }

    public IEnumerator LoadNextlevel2() // IEmulator er for timers and stuff
    {
        _audioSource.PlayOneShot(successSFX);

        yield return new WaitForSeconds(successSFX.length + 0.5f);
        
        _audioSource.PlayOneShot(successVoiceSFX);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        yield return new WaitForSeconds(5f); // WaitForSeconds will force the code after to trigger after seconds, no need for timers "_clip".length gets the length of the audioclip
        
        
        SceneManager.LoadScene(nextSceneIndex);
    }

    public IEnumerator Reloadlevel()
    {
        _audioSource.PlayOneShot(failSFX);
        
        yield return new WaitForSeconds(failSFX.length + 0.5f);
        
        _audioSource.PlayOneShot(failVoiceSFX);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    public void FailReloadScene()
    {
        StartCoroutine(Reloadlevel());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    
}
