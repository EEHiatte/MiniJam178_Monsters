using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.Splines.Interpolators;

public class BulgeAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip clip;

    public SplineAnimate splineAnimator;

    public LevelController levelController;

    void Awake()
    {
        audioSource.PlayOneShot(clip);
        splineAnimator.Completed += EndOfPath;
    }

    public void EndOfPath()
    {
        audioSource.PlayOneShot(clip);
        splineAnimator.Duration = Mathf.Lerp(0.5f, 5f, levelController.PlayerHealth / levelController.PlayerMaxHealth);
        splineAnimator.Restart(true);
    }
}
