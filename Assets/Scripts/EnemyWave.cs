using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "ScriptableObjects/EnemyWave")]

public class EnemyWave : ScriptableObject
{
    public GameObject[] enemies;
    public int[] amounts;
    public bool inOrder = false;
    public float minTime, maxTime;
    public int difficultyLevel;
}
