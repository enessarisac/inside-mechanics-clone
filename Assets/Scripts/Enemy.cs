using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Type")]
public class Enemy : ScriptableObject
{
   public string enemyType;
   public bool isBoss;
   public bool isRunning;
   public float speed;
}
