using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public static AudioClip LongJump;
    public static AudioClip Victory;
    public static AudioClip Death;
    static AudioSource audsrc;

    // Start is called before the first frame update
    void Start()
    {
        LongJump = Resources.Load<AudioClip>("JumpLong");
        Victory = Resources.Load<AudioClip>("PlayerVictory");
        Death = Resources.Load<AudioClip>("PlayerDeath");
        audsrc = GetComponent<AudioSource>();
    }

    public static void PlayTune(string sound)
    {
        switch (sound)
        {
            case "JumpLong":
                audsrc.PlayOneShot(LongJump);
                break;
            case "PlayerVictory":
                audsrc.PlayOneShot(Victory);
                break;
            case "PlayerDeath":
                audsrc.PlayOneShot(Death);
                break;
        }
    }
}
