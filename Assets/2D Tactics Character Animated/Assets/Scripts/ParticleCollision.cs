using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D col)
    {

        Debug.Log("hit");
    }
}
