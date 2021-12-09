using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SunMovement : MonoBehaviour
{
    public float sunBaseSpeed = 12f;
    public float previousSunSpeed;
    public float timeScaleFactor;
    public float currentSunSpeed;
    public float maxSunSpeed = 300f;
    public Transform player;
    public bool usesTimeFormula = true;
    float radius;
    public AudioSource gameMusic;
    // Start is called before the first frame update
    void Start()
    {
        radius = GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        timeScaleFactor = Time.timeSinceLevelLoad;

        //Michael's time scale formula. Set bool to false in inspector to disable it
        if (usesTimeFormula)
        {
            float x = timeScaleFactor;
            timeScaleFactor = 9.0f * (((0.42f * (x - 70.0f)) / Mathf.Sqrt(Mathf.Pow((0.8f * x) - 70.0f, 2.0f) + 500.0f)) + .42f); //I'm sorry
        }
        currentSunSpeed = sunBaseSpeed + ((timeScaleFactor / 5) * Mathf.Abs((transform.position.x - player.position.x) / 50));
        if (currentSunSpeed > maxSunSpeed)
        {
            currentSunSpeed = maxSunSpeed;
        }
        if (currentSunSpeed < (previousSunSpeed - sunBaseSpeed))
        {
            currentSunSpeed = previousSunSpeed - (sunBaseSpeed / 10);
        }

        transform.position = new Vector3(transform.position.x + (currentSunSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        previousSunSpeed = currentSunSpeed;
    }

    private bool CheckForPlayerCollision()
    {
        float distanceToPlayerX = player.position.x - transform.position.x;
        float distanceToPlayerY = player.position.y - transform.position.y;
        float distanceToPlayerZ = player.position.z - transform.position.z;

        float squareSum = (distanceToPlayerX * distanceToPlayerX) + (distanceToPlayerY * distanceToPlayerY) + (distanceToPlayerZ * distanceToPlayerZ);
        if(squareSum < (radius * radius))
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // kill player and transition to GameOver Scene
            //Debug.Log("Player killed by sun");
            gameMusic.Stop();
            SceneManager.LoadScene("GameOver");
        }

        if (collision.gameObject.tag == "Platform")
        {
            //Debug.Log("Platform collide");
            collision.gameObject.SetActive(false);
        }
    }
}
