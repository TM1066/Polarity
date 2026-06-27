using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
// using PlayFab; - SHELVED FOR NOW! - focusing on Arcane machine first and foremost
// using PlayFab.ClientModels;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] scoreTexts = new TextMeshProUGUI[10];
    [SerializeField] TextMeshProUGUI[] highScoreTexts = new TextMeshProUGUI[10];
    [SerializeField] TextMeshProUGUI[] nameTexts = new TextMeshProUGUI[10];
    [SerializeField] List<Image> colorDisplays = new List<Image>();

    [SerializeField] Image replayButtonBG;
    [SerializeField] TextMeshProUGUI replayButtonText;
    [SerializeField] Image backButtonBG;
    [SerializeField] TextMeshProUGUI backButtonText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     // Setting up all entries on Leaderboard
    //     foreach (TextMeshProUGUI scoreText in scoreTexts)
    //     {
    //         scoreText.text = "";
    //         scoreText.color = Color.clear;
    //     }
    //     foreach (TextMeshProUGUI nameText in nameTexts)
    //     {
    //         nameText.text = "";
    //         nameText.color = Color.clear;
    //     }
    //     foreach (Image image in colorDisplays)
    //     {
    //         image.color = Color.clear;
    //     }
    //     foreach (TextMeshProUGUI text in highScoreTexts)
    //     {
    //         text.enabled = false;
    //     }

    //     int leaderboardAccessIndex = 0;
        
    //     foreach (User user in GlobalManager.currentSelectedMap.mapLeaderboard)
    //     {
    //         nameTexts[leaderboardAccessIndex].text = user.userName;
    //         colorDisplays[leaderboardAccessIndex].color = new Color(user.color.r, user.color.g, user.color.b, 0f);
    //         scoreTexts[leaderboardAccessIndex].text = user.score.ToString();

    //         leaderboardAccessIndex++;
    //     }
    // }

    // private void SetHighScoreMessages()
    // {
    //     for (int i = 0; i < GlobalManager.currentSelectedMap.GetMapLeaderboardChanged().Count - 1; i++)
    //     {
    //         if (GlobalManager.currentSelectedMap.GetMapLeaderboardChanged()[i])
    //         {
    //             highScoreTexts[i].enabled = true; // re-enable high score texts for the places on the board that it should be shown
    //         }
    //     }
    // }

//     public IEnumerator ShowScoresAndCheckForHighScores()
//     {
//         foreach (TextMeshProUGUI highScoreText in highScoreTexts)
//         {
//             if (GlobalManager.currentSelectedMap.GetMapLeaderboardChanged()[Array.IndexOf(highScoreTexts, highScoreText)])
//             {
//                 StartCoroutine(ScriptUtils.PositionLerp(this.gameObject.transform, this.gameObject.transform.position, new Vector2 (0,0),2f));

//                 yield return new WaitForSeconds(2);

//                 StartCoroutine(ShowNewHighScoreText(highScoreText));

//                 yield return new WaitForSeconds(3f);
//                 //Not player, just the current player as being looped through
//                 User currentUser = GlobalManager.currentSelectedMap.mapLeaderboard[Array.IndexOf(highScoreTexts, highScoreText)];

//                 foreach (TextMeshProUGUI text in nameTexts)
//                 {
//                     if (currentUser != null)
//                     {
//                         text.text = currentUser.userName;
//                         ScriptUtils.ColorLerpOverTime(text, Color.clear, Color.black, 0.5f);
//                     }
//                 }
//                 foreach (TextMeshProUGUI text in scoreTexts)
//                 {
//                     if (currentUser != null)
//                     {
//                         text.text = currentUser.userName;
//                         ScriptUtils.ColorLerpOverTime(text, Color.clear, Color.black, 0.5f);
//                     }
//                 }
//                 foreach (TextMeshProUGUI text in nameTexts)
//                 {
//                     if (currentUser != null)
//                     {
//                         text.text = currentUser.userName;
//                         ScriptUtils.ColorLerpOverTime(text, Color.clear, Color.black, 0.5f);
//                     }
//                 }





//             }
//         }
//     }

//     public IEnumerator ShowNewHighScoreText(TextMeshProUGUI text)
//     {
//         text.gameObject.SetActive(true);
//         text.text = "New High Score!!!";

//         StartCoroutine(ScriptUtils.ColorLerpOverTime(text, Color.clear, Color.white, 0.5f));
//         yield return new WaitForSeconds(0.5f);
//         StartCoroutine(ScriptUtils.ColorLerpOverTime(text, Color.white, Color.clear, 0.5f));
//         yield return new WaitForSeconds(0.5f);
//         StartCoroutine(ScriptUtils.ColorLerpOverTime(text, Color.clear, Color.white, 0.5f));
//         yield return new WaitForSeconds(0.5f);
//         StartCoroutine(ScriptUtils.ColorLerpOverTime(text, Color.white, Color.clear, 0.5f));
//         yield return new WaitForSeconds(0.5f);
//         StartCoroutine(ScriptUtils.ColorLerpOverTime(text, Color.clear, Color.white, 0.5f));
//         yield return new WaitForSeconds(0.5f);
//         StartCoroutine(ScriptUtils.ColorLerpOverTime(text, Color.white, Color.clear, 0.5f));
//         yield return new WaitForSeconds(0.5f);

//     }
}
