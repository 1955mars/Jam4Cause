using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    int numberOfLevels = 5; // The menu is scene 0
    public static int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Student" && currentLevel <= numberOfLevels)
        {
            currentLevel++;
            SceneManager.LoadScene(currentLevel);
            Debug.Log("Loading");
        }
        else if (other.gameObject.tag == "Student")
        {
            SceneManager.LoadScene(0);
            Debug.Log("Back we go");
        }
    }
}
