using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    float speed;

    [Space]
    [Header("Axises of elliptical path of BulletLauncher")]
    [SerializeField]
    float horizontalAxis = 12;
    [SerializeField]
    float verticalAxis = 8;

    [Space]
    [Header("Cooldown for shooting")]
    [SerializeField]
    private float cooldown;

    private float time;

    [SerializeField]
    AudioSource audioSource;
    

    void Update()
    {
        //moving to the player
        transform.Translate((player.transform.position - gameObject.transform.position) * Time.deltaTime * speed);
        gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y);
        

        //shoot to random direction
        if (time > 0f)
        {
            time -= Time.deltaTime;
        }
        else
        {
            // request bullet from bullet pool
            GameObject bullet = BulletPoolManager.Instance.RequestBullet();
            bullet.GetComponent<BulletBehavior>().Scoreable = false;
            var (position, direction) = RNDDotOnEllipse();
            bullet.transform.position = position;
            bullet.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            time = cooldown;
            audioSource.Play();

        }


    }

    //calculate point and direction for shoot
    (Vector3, Vector3) RNDDotOnEllipse()
    {
        float cx = gameObject.transform.position.x;
        float cy = gameObject.transform.position.y;

        float angle = Random.Range(0, 360);
        float x = cx + (horizontalAxis * Mathf.Cos(angle));
        float y = cy+5 + (verticalAxis * Mathf.Sin(angle));
        (Vector3, Vector3) tuple = (new Vector3(x, y, 0), new Vector3(x - cx, y - cy));

        return tuple;
    }
}
