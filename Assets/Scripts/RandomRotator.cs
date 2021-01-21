using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField]
    private float _spinMin;
    [SerializeField]
    private float _spinMax;


    void Start()
    {

        //set random rotating for asteroid 
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * Random.Range(_spinMin, _spinMax);
    }
}