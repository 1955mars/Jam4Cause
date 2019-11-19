using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathOrRevert : MonoBehaviour
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
        if (other.gameObject.tag == "Student")
        {
            // Just go back to the start of this level
            SceneManager.LoadScene(currentLevel);
            Debug.Log("Restarting on death");
        }
        else if (other.gameObject.tag == "Player")
        {
            // This would end the current macro if possible
        }
    }
}
