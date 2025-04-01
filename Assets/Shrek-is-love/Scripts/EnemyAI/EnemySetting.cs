using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "AI/Enemy Settings")]
public class EnemySettings : ScriptableObject
{
    [Header("Navigation")]
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 6;
    public float speedRun = 9;
    public float stoppingDistance = 0.5f;
    public float attackWaitTime = 0.2f;

    [Header("Vision")]
    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    [Header("Combat")]
    public float attackRange = 5f;
    public float chaseStopDistance = 6f;
    public float minChaseDistance = 2.5f;
    public float lookingDistance = 0.8f;
}