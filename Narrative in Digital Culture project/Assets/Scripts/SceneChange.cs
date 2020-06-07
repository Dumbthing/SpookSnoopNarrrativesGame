using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    Animator anim;
    private int sceneToLoad;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToNextScene(int sceneIndex)
    {
        sceneToLoad = sceneIndex;
        anim.SetTrigger("FadeOut");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
