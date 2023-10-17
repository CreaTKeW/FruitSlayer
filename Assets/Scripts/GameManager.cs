using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public TextMeshPro scoreText;

    public void IncreaseScore()
    {
        score += 2;
        //scoreText = score.ToString();
    }
}
