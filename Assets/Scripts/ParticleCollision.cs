using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleCollision : MonoBehaviour
{
    public Text GameOver_text;
    public Button Restart_button;
    public Button Quit_button;
    public Button HighScore_button;
    public ParticleSystem RainGenerator;
    public PlayerMovement playerMove;
    public Text score;
    public Text finalScore;
    public Score score_code;
    public HighScoreTable highscoreTable;
    private bool _triggerHasPassed = false;
    private void OnParticleCollision(GameObject other)
    {
        if (_triggerHasPassed)
        {
            return;
        }
        if (other.layer.Equals(9)) {
            playerMove.GetComponent<PlayerMovement>();
            score_code.enable_score = false;
            score.text = "";
            GameOver_text.text = "GAME OVER";
            finalScore.text = "Final Score: " + (int)score_code.score;
            Restart_button.gameObject.SetActive(true);
            Quit_button.gameObject.SetActive(true);
            HighScore_button.gameObject.SetActive(true);
            RainGenerator.Stop();
            playerMove.runSpeed = 0;
            //yield return new WaitForSeconds(4);
            playerMove.able = false;

            HighscoreEntry highscoreEntry = new HighscoreEntry { score = (int)score_code.score };

            string jsonString = PlayerPrefs.GetString("HighScoreTable");
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            highscores.highscoreEntryList.Add(highscoreEntry);

            highscores.highscoreEntryList.Sort((x, y) => y.score.CompareTo(x.score));

            if (highscores.highscoreEntryList.Count > 10)
            {
                for (int h = highscores.highscoreEntryList.Count; h > 10; h--)
                {
                    highscores.highscoreEntryList.RemoveAt(10);
                }
            }

            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("HighScoreTable", json);
            PlayerPrefs.Save();

            _triggerHasPassed = true;
        }
        Debug.Log("passou");
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
    }

}
