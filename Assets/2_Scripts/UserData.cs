using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static string unlockChar = "UnlockChar_";
    public static string curChar = "curChar";
    public static string gold = "gold_";
    public static string curLevel = "curLevel";



    public static void SetGold(int value)
    {
        PlayerPrefs.SetInt(gold, value);
        SharedScene.ins.RefreshGoldText();
    }





    public static bool IsUnlockedChar(string charName)
    {


        if (PlayerPrefs.GetString(unlockChar + charName) == "true")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int GetCurLevel()
    {
        if (PlayerPrefs.GetInt(curLevel) == 0)
        {
            SetCurLevel(1);
        }
        return PlayerPrefs.GetInt(curLevel);

    }

    public static void SetCurLevel(int value)
    {
        PlayerPrefs.SetInt(curLevel, value);
    }


    public static void SetUnlockChar(string charName)
    {
        PlayerPrefs.SetString(UserData.unlockChar + charName, "true");
    }







    public static void SetCurChar(string charName)
    {


        if (IsUnlockedChar(charName))
            PlayerPrefs.SetString(curChar, charName);
        SharedScene.ins.RefreshGoldText();

    }

    public static int GetGold()
    {
        return PlayerPrefs.GetInt(gold);
    }

    public static void MinusGold(int gold)
    {
        var curGold = GetGold();
        curGold -= gold;
        SetGold(curGold);
        SharedScene.ins.RefreshGoldText();
    }




}