using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class STDLevelLoader : MonoSingleton<STDLevelLoader>
{
    public Animator crossfadeController;

    public float transitionDelay = 1f;


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel( int levelIndex)
    {
        crossfadeController.SetTrigger("Start");
        yield return new WaitForSeconds(transitionDelay);
        SceneManager.LoadScene(levelIndex);
    }

}
