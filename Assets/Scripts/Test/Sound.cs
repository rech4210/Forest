using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    int audioIdx;
    AudioSource[] audioSource;
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
    }
    
    void Update()
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            if (audioSource[i].isPlaying)
            {
                return;
            }
        }

        bool isSnow = -150 < transform.position.x && transform.position.x < 0 && 95 < transform.position.z && transform.position.z < 250;
        if(isSnow)
        {
            audioIdx = Random.Range(0, 2);
        }
        else
        {
            audioIdx = Random.Range(2, 4);
        }
        
        
        if (TestMove2.walk)
        {
            audioSource[audioIdx].Play();
        }
        audioSource[audioIdx].volume = Random.Range(0.2f, 1);
    }
}
