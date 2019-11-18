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

    private MacroController macroManager;
    public GameObject student;
    public GameObject player;
    private Transform spawnPoint;

    private GameObject playerInstance;

    private float initCameraSize;
    Rect initViewPort;

    // Start is called before the first frame update
    void Start()
    {
        sectionSelect = true;
        macroLearning = false;
        studentPlaying = false;

        selector = GameObject.Find("Selector");
        //spawnPoint = GameObject.Find("Spawn Point").transform;
        macroManager = FindObjectOfType<MacroController>();
        camera = Camera.main;
        initCameraSize = camera.orthographicSize;
        initViewPort = new Rect(camera.rect);
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
            macroLearning = true;
            StartCoroutine(ZoomIn(selector.transform.position));
        }

        if (Input.GetKeyDown(KeyCode.Z) && macroLearning)
        {
            StartCoroutine(ZoomOutSelection());
        }
    }

    IEnumerator ZoomIn(Vector3 pos)
    {
        UnityEngine.UI.Text uiText = GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>();
        uiText.text = "PERFORM THE ACTION YOU WISH TO TEACH";
        selector.transform.position = camera.WorldToScreenPoint(camera.transform.position);
        Rect viewport = camera.rect;
        Vector3 targetPos = camera.ScreenToWorldPoint(pos);
        Vector3 currentPos = targetPos;
        Vector3 selectorScale = selector.transform.localScale;

        playerInstance = Instantiate(player, new Vector3(currentPos.x-3.5f, currentPos.y+2, 0), player.transform.rotation);

        float timer = 0.0f;

        while (timer < 1.0f)
        {
            //Debug.Log(timer);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, 5.0f, timer);
            //viewport.y = Mathf.Lerp(viewport.y, 0.0f, timer);
            //viewport.height = Mathf.Lerp(viewport.height, 1.0f, timer);

            viewport.y = Mathf.Lerp(initViewPort.y, 0.0f, timer);
            viewport.height = Mathf.Lerp(initViewPort.height, 1.0f, timer);

            currentPos.x = Mathf.Lerp(currentPos.x, targetPos.x, timer);
            selectorScale.x = Mathf.Lerp(selectorScale.x, 0.28f, timer);
            selectorScale.y = Mathf.Lerp(selectorScale.y, 1.0f, timer);
            camera.rect = viewport;
            camera.transform.position = currentPos;
            selector.transform.localScale = selectorScale;

            timer += Time.deltaTime;

            yield return null;
        }

        if (true)
        {
            StartCoroutine(macroManager.RecordMacro());
        }
    }

    public IEnumerator ZoomOutSelection()
    {
        //Debug.Log("Zooming out");
        macroLearning = false;
        sectionSelect = true;
        Destroy(playerInstance);
        UnityEngine.UI.Text uiText = GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>();
        uiText.text = "SELECT AN AREA TO TEACH TO YOUR STUDENT";
        Rect viewport = camera.rect;
        Vector3 currentPos = camera.transform.position;
        Vector3 selectorScale = selector.transform.localScale;
        float timer = 0.0f;

        while (timer < 1.0f)
        {
            //Debug.Log(camera.orthographicSize);
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, initCameraSize, timer);
            viewport.y = Mathf.Lerp(viewport.y, 0.2f, timer);
            viewport.height = Mathf.Lerp(viewport.height, 0.6f, timer);
            currentPos.x = Mathf.Lerp(currentPos.x, 11, timer);
            selectorScale.x = Mathf.Lerp(selectorScale.x, 0.15f, timer);
            selectorScale.y = Mathf.Lerp(selectorScale.y, 0.6f, timer);
            camera.rect = viewport;
            camera.transform.position = currentPos;
            selector.transform.localScale = selectorScale;

            timer += Time.deltaTime;

            yield return null;
        }
    }
    
}
