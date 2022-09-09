using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMasterSc : MonoBehaviour
{
    [SerializeField]private GameObject scoreTextObject;
    private TextMeshProUGUI _scoreText;
    public string defaultScorePhrase;
    // ______________________________________________________________
    private static UIMasterSc _instance;
    public static UIMasterSc Instance { get => _instance;}
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
            ScoreChanged();
        }
    }
    //___________________________________________________________________

    public void ScoreChanged()
    {
        if(!scoreTextObject) return;
        if(scoreTextObject && !_scoreText)
        {
            _scoreText = scoreTextObject.GetComponent<TextMeshProUGUI>();
        }
        _scoreText.SetText(defaultScorePhrase+GameManager.GetScore());
    }

}