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
        
        audioIdx = Random.Range(0, audioSource.Length);
        if (TestMove2.walk)
        {
            audioSource[audioIdx].Play();
        }
        audioSource[audioIdx].volume = Random.Range(0.2f, 1);
    }
}
