using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "timer", menuName = "ScriptableObjects/timer", order = 1)]
public class TimeHolder : ScriptableObject
{
    [SerializeField] float timeForGame;
    
    public float TimeForGame 
    {
        get { return timeForGame; }
        set { timeForGame = value; }
    }
    
}
