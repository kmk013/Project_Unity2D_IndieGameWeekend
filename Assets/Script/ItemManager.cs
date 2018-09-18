using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class ItemManager : MonoBehaviour {

    int keyboard = 1;
    int mouse_keyboard = 2;
    int money = 5;
    int fan = 5;
    //int candy = 3;
    int cracker = 60;
    int lot = 25; // losing ticket

    public static ItemManager instance;

    StringBuilder sb = new StringBuilder();

	// Use this for initialization
	void Start () {
        instance = this;

        keyboard = PlayerPrefs.GetInt("keyboard", 1);
        mouse_keyboard = PlayerPrefs.GetInt("mouse_keyboard", 2);
        money = PlayerPrefs.GetInt("money", 5);
        fan = PlayerPrefs.GetInt("fan", 5);
        //candy = PlayerPrefs.GetInt("candy", 3);
        cracker = PlayerPrefs.GetInt("cracker", 60);
        lot = PlayerPrefs.GetInt("lot", 25);

        //Debug.Log(keyboard + ", " + mouse_keyboard + ", " + money + ", " + fan + ", " + candy + ", " + cracker + ", " + lot);
        set();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.F12))
        {
            textSetting();
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.R))
        {
            set();
        }
        else
        {
        }
    }

    void textSetting()
    {
        sb.Remove(0, sb.Length);
        sb.Append("keyboard: ");
        sb.Append(keyboard.ToString());
        sb.Append("\n");

        sb.Append("mouse_keyboard: ");
        sb.Append(mouse_keyboard.ToString());
        sb.Append("\n");

        sb.Append("money: ");
        sb.Append(money.ToString());
        sb.Append("\n");

        sb.Append("fan: ");
        sb.Append(fan.ToString());
        sb.Append("\n");

        //sb.Append("candy: ");
        //sb.Append(candy.ToString());
        //sb.Append("\n");

        sb.Append("cracker: ");
        sb.Append(cracker.ToString());
        sb.Append("\n");

        sb.Append("lot: ");
        sb.Append(lot.ToString());

    }

    void set()
    {
        PlayerPrefs.SetInt("keyboard", 1);
        PlayerPrefs.SetInt("mouse_keyboard", 2);
        PlayerPrefs.SetInt("money", 5);
        PlayerPrefs.SetInt("fan", 5);
        //PlayerPrefs.SetInt("candy", 3);
        PlayerPrefs.SetInt("cracker", 60);
        PlayerPrefs.SetInt("lot", 25);

        keyboard = PlayerPrefs.GetInt("keyboard", 1);
        mouse_keyboard = PlayerPrefs.GetInt("mouse_keyboard", 2);
        money = PlayerPrefs.GetInt("money", 5);
        fan = PlayerPrefs.GetInt("fan", 5);
        //candy = PlayerPrefs.GetInt("candy", 3);
        cracker = PlayerPrefs.GetInt("cracker", 60);
        lot = PlayerPrefs.GetInt("lot", 25);

        Debug.Log("Cleared");
    }

    public void save()
    {
        PlayerPrefs.SetInt("keyboard", keyboard);
        PlayerPrefs.SetInt("mouse_keyboard", mouse_keyboard);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("fan", fan);
        //PlayerPrefs.SetInt("candy", candy);
        PlayerPrefs.SetInt("cracker", cracker);
        PlayerPrefs.SetInt("lot", lot);
    }

    public int pick()
    {
        return Random.Range(0, 5);
    }
}
