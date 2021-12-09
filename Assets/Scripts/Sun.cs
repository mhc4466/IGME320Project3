using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sun : MonoBehaviour
{
    [SerializeField]TMP_Text sunText;
    [SerializeField] GameObject player;
    [SerializeField] float offset = 1600;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float sunToPlayer = Mathf.Abs( Vector3.Distance(this.transform.position, player.transform.position))+offset;
        sunText.text = "Distance from the Sun: " + sunToPlayer.ToString("000") + " meters";


    }
}
