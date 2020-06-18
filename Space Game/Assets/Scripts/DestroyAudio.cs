using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAudio : MonoBehaviour
{
    public AudioSource sound;

    // Update is called once per frame
    void Update()
    {
        if (!sound.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
