using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pref : MonoBehaviour
{
   public static int BestScore
    {
        set
        {
            int oldScore = PlayerPrefs.GetInt(PrefKey.BestScore.ToString(), 0);
            if(value > oldScore || oldScore == 0)
            {
                PlayerPrefs.SetInt(PrefKey.BestScore.ToString(), value);
            }

        }
        get => PlayerPrefs.GetInt(PrefKey.BestScore.ToString(), 0);
    }
}
