using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct FrameData
{
    public bool jumpPressed;
    public bool rightPressed;
    public bool leftPressed;
    public bool actionPressed;
}

public class MacroController : MonoBehaviour
{
    private List<FrameData> macro1;
    private List<FrameData> macro2;
    private List<FrameData> macro3;

    [SerializeField]
    private bool recording;

    // Start is called before the first frame update
    void Start()
    {
        recording = false;
        macro1 = new List<FrameData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            recording = !recording;
        }

        if (recording)
        {
            FrameData currentFrame = new FrameData();

            currentFrame.jumpPressed = Input.GetKeyDown(KeyCode.Space);
            currentFrame.rightPressed = (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow));
            currentFrame.leftPressed = (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow));
            currentFrame.actionPressed = false;

            //macro1.Add(currentFrame);
        }

        if (Input.GetKeyDown(KeyCode.P) && !recording && macro1.Count > 0)
        {

        }
    }
}
