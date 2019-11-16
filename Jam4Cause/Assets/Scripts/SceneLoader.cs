using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void Scene_loader(int scene_index)
    {
        SceneManager.LoadScene(scene_index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
