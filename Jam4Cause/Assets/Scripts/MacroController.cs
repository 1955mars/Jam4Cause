using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FrameData
{
    public bool jumpPressed;
    public bool rightPressed;
    public bool leftPressed;
    public bool actionPressed;
}

public class MacroController : MonoBehaviour
{
    private int currentMacro;
    private int frameIndex;
    private List<FrameData>[] macros;

    [SerializeField]
    public bool recording;
    public bool playing;
    public Student student;
    public GameObject teacher;

    // Start is called before the first frame update
    void Start()
    {
        recording = false;
        playing = false;
        currentMacro = -1;
        macros = new List<FrameData>[3];
        macros[0] = new List<FrameData>();
        macros[1] = new List<FrameData>();
        macros[2] = new List<FrameData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentMacro = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            currentMacro = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            currentMacro = 2;

        if (Input.GetKeyDown(KeyCode.R) && !recording)
            StartCoroutine(RecordMacro());

        if (Input.GetKeyDown(KeyCode.P) && !playing && macros[currentMacro].Count > 0)
        {
            frameIndex = 0;
            playing = true;
            student.playingMacro = true;
        }

        if (recording)
        {
            Debug.Log("RECORDING");
            FrameData currentFrame = new FrameData();

            currentFrame.jumpPressed = (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
            currentFrame.rightPressed = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
            currentFrame.leftPressed = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
            currentFrame.actionPressed = false;

            macros[currentMacro].Add(currentFrame);
        }

        if (playing && frameIndex < macros[currentMacro].Count)
        {
            student.currentFrame = macros[currentMacro][frameIndex];
            frameIndex++;
        }
        else if (student != null)
        {
            student.playingMacro = false;
            playing = false;
        }
    }

    public IEnumerator RecordMacro()
    {
        currentMacro += 1;

        Vector3 spawnPos = Camera.main.GetComponent<Camera>().transform.position;
        spawnPos.x -= 2.0f;
        spawnPos.y += 2.5f;
        spawnPos.z = -2.3f;

        //Instantiate(teacher, spawnPos, Quaternion.identity);

        yield return new WaitForSeconds(1.0f);

        recording = true;

        yield return new WaitForSeconds(8.0f);

        recording = false;

        if (currentMacro < 2)
        {
            StartCoroutine(FindObjectOfType<GameManager>().ZoomOutSelection());
        }
    }
}
