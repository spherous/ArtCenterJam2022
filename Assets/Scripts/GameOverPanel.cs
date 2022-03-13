using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum EndGameStatus {Win = 0, Lose = 1};
public static class EndGameStatusExtensions
{
    public static Color GetColor(this EndGameStatus status) => status switch {
        EndGameStatus.Win => Color.green,
        EndGameStatus.Lose => Color.red,
        _ => Color.white
    };
}

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GroupFader fader;
    [SerializeField] TextMeshProUGUI resultText;
    public string winText = "Persevered";
    public string loseText = "Overwhelmed";

    public void GameOver(EndGameStatus endGameStatus)
    {
        fader.FadeIn();
        resultText.color = endGameStatus.GetColor();
        resultText.text = endGameStatus == EndGameStatus.Win ? winText : loseText;
    }
}