using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLaser : MonoBehaviour
{
    public AudioSource laserSound;
    public Transform bullet;

    // Update is called once per frame
    void Update()
    {
        try
        {
            transform.position = bullet.position;
        }

        catch
        {

        }

        if (! laserSound.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
