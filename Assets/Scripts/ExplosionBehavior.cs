using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    ParticleSystem prt;
    // Start is called before the first frame update
    void Start()
    {
         prt = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (prt.isStopped) gameObject.SetActive(false);
    }
}
