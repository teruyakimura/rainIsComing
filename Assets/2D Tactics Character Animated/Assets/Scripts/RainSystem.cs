using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystem : MonoBehaviour
{
    
    public ParticleSystem p;
    float moveSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        p.playbackSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {

        p.playbackSpeed += Time.deltaTime*moveSpeed;
    }




}
