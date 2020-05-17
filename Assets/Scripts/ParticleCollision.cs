using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleCollision : MonoBehaviour
{
    public Text GameOver_text;
    public Button Restart_button;
    public Button Quit_button;
    public ParticleSystem RainGenerator;
    public PlayerMovement playerMove;
    public Text score;
    public Score score_code;
    private void OnParticleCollision(GameObject other)
    {
        if (other.layer.Equals(9)) {
            playerMove.GetComponent<PlayerMovement>();
            score.text = "";
            GameOver_text.text = "GAME OVER";
            Restart_button.gameObject.SetActive(true);
            Quit_button.gameObject.SetActive(true);
            RainGenerator.Stop();
            playerMove.runSpeed = 0;
            //yield return new WaitForSeconds(4);
            playerMove.able = false;
            score_code.enable_score = false;


        }
    }

    

}
