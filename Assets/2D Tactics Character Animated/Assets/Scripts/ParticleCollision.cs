using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleCollision : MonoBehaviour
{
    public Text GameOver_text;
    public ParticleSystem RainGenerator;
    public PlayerMovement playerMove;
    private void OnParticleCollision(GameObject other)
    {
        if (other.layer.Equals(9)) {
            playerMove.GetComponent<PlayerMovement>();
            GameOver_text.text = "GAME OVER";
            RainGenerator.Stop();
            playerMove.enabled = false;
            Debug.Log("passou");
        }
    }

}
