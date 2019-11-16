using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool sectionSelect;
    private bool macroLearning;
    private bool studentPlaying;
    // Camera sizes - 9.6, 5, 
    private GameObject selector;
    private Camera camera;

    public GameObject teacher;
    public GameObject student;

    // Start is called before the first frame update
    void Start()
    {
        sectionSelect = true;
        macroLearning = false;
        studentPlaying = false;

        selector = GameObject.Find("Selector");
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (sectionSelect)
        {
            Vector3 selectorPos = selector.transform.position;
            selectorPos.x = Input.mousePosition.x;
            selector.transform.position = selectorPos;
        }
        
        if (sectionSelect && Input.GetMouseButtonDown(0))
        {
            //selector.SetActive(false);
            sectionSelect = false;
            StartCoroutine(ZoomIn(selector.transform.position));
        }
    }

    IEnumerator ZoomIn(Vector3 pos)
    {
        selector.transform.position = camera.WorldToScreenPoint(camera.transform.position);
        Vector3 targetPos = camera.ScreenToWorldPoint(pos);
        Vector3 currentPos = targetPos;
        Vector3 selectorScale = selector.transform.localScale;
        float timer = 0.0f;

        while (timer < 1.0f)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 5.0f, timer);
            currentPos.x = Mathf.Lerp(currentPos.x, targetPos.x, timer);
            selectorScale.x = Mathf.Lerp(selectorScale.x, 0.3f, timer);
            camera.transform.position = currentPos;
            selector.transform.localScale = selectorScale;

            timer += 0.2f * Time.deltaTime;

            yield return null;
        }

        Vector3 spawnPos = camera.transform.position;
        spawnPos.x -= 2.0f;
        spawnPos.y += 2.0f;
        spawnPos.z = -2.3f;

        Instantiate(teacher, spawnPos, Quaternion.identity);
    }

    IEnumerator ZoomOut()
    {
        yield return null;
    }
}
