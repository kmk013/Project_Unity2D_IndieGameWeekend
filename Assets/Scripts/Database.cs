using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SpoonColor
{
    SOIL = 0,
    BRONZE = 1,
    SILVER = 2,
    GOLD = 3,
    CSS = 4
}

public class ScriptPerDay
{
    public int[] cost;
    public string[] button;
    public List<string> script;
}

public class Database : MonoBehaviour {
    public static int money;
    public static int age;
    public static int life;
    public static SpoonColor spoon;
    public static List<ScriptPerDay> scripts;
    public static string babyName;

    public static void SaveData()
    {
        PlayerPrefs.SetInt("Age", age);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Life", money);
        PlayerPrefs.SetInt("Spoon", (int)spoon);
        PlayerPrefs.SetString("Name", babyName);
    }
}
