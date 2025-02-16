using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class TimedLoopAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip clip;

    public SplineAnimate splineAnimator;

    void Awake()
    {
        audioSource.PlayOneShot(clip);
        splineAnimator.Completed += EndOfPath;
    }

    public void EndOfPath()
    {
        audioSource.PlayOneShot(clip);
    }
}
