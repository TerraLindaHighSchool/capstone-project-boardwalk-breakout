using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plushie2 : MonoBehaviour
{
    // Start is called before the first frame update

    private Transform bear;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        bear = GameObject.Find("Player").transform;
        offset = new Vector3(0, 0, 3);
    }

    void Update()
    {
        transform.position = bear.position + offset;

    }
}