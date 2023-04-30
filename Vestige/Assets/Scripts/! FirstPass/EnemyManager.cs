using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Important locations")]
    public Transform waterSource;
    public Vector3 restSpot;
    public Quaternion initialRotation;
    [Header("Effects")]
    public ParticleSystem fireVFX;
    [Header("Status")]
    public bool isFired;
    public bool isPanic;
    public bool isSleep;
    public bool isDead;
    [Header("animation")]
    public bool isFiredPlayed = false;
    [Header("detections")]
    public float detectionRadius = 20f;
    public float minDetectionAngle = -60f;
    public float maxDetectionAngle = 60f;
    public float stoppingDistance = 1.5f;
    public float RotationSpeed = 15f;
    public LayerMask playerMask;
    private void Awake()
    {
        waterSource = FindObjectOfType<Water>().position;
        restSpot = this.transform.position;
        initialRotation = this.transform.rotation;

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    #region Animation functions
    public void TurnOnFire()
    {
        fireVFX.Play();
    }
    public void TurnOffFire()
    {
        fireVFX.Stop();
    }
    #endregion
}
