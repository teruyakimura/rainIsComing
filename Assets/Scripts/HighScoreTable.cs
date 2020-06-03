using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
        {
            return;
        };
            entryContainer = transform.Find("highScoreEntryContainer");
            entryTemplate = entryContainer.Find("highScoreEntryTemplate");

            entryTemplate.gameObject.SetActive(false);
            HighscoreEntry defaultScore = new HighscoreEntry { score = 3000 };
            //PlayerPrefs.DeleteKey("HighScoreTable");

            try
            {
                string jsonString = PlayerPrefs.GetString("HighScoreTable");
                Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

                //sort
                highscores.highscoreEntryList.Sort((x, y) => y.score.CompareTo(x.score));

                if (highscores.highscoreEntryList.Count > 10)
                {
                    for (int h = highscores.highscoreEntryList.Count; h > 10; h--)
                    {
                        highscores.highscoreEntryList.RemoveAt(10);
                    }
                }

                highscoreEntryTransformList = new List<Transform>();
                foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
                {
                    CreateHighScoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
                }
            }
            catch (NullReferenceException)
            {
                Highscores highscores = new Highscores();
                highscores.highscoreEntryList = new List<HighscoreEntry>();
                for (int i = 0; i < 10; i++)
                {
                    highscores.highscoreEntryList.Add(defaultScore);
                }
                //sort
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

                highscoreEntryTransformList = new List<Transform>();
                foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
                {
                    CreateHighScoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
                }
            }
    }

    private void CreateHighScoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, y: -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;

        entryTransform.Find("PositionEntry").GetComponent<Text>().text = rank.ToString();

        int score = highscoreEntry.score;

        entryTransform.Find("ScoreEntry").GetComponent<Text>().text = score.ToString();

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score };

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
