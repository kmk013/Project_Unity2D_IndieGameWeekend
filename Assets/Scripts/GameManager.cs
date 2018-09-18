using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UILabel labelStatus; // 메시지라벨
    private GameObject[] buttons = new GameObject[3]; //버튼
    private UILabel[] labelButtonCaption = new UILabel[3]; //버튼위 자막
    private UILabel[] labelButtonCosts = new UILabel[3]; //버튼아래 자막
    private UILabel labelMessage;
    private int rouletteStatus = 0; //0: false, 1: true, 3: selected 랜덤 돌린 상태.
    private int rouletteNumber = -1; // 랜덤 번호. -1이면 사용하지 않음. 0,1,2는 버튼번호
    private UI2DSprite bgImage;

    public Sprite[] ageSprites = new Sprite[31]; // 나이별 그림들

    private UIPanel panelNoMoney; // 돈부족창
    private UIPanel panelName; 

    private int scriptNumber = 0; //자막 번호. 그 나이대의 몇번째인지.
    private bool scriptDisplaying = false; //자막표시중인지

    // 일단 원칙상 한번만 실행되어야 할 것 들
    void Start()
    {
        InitUI();
        SetStatusUI();
        SetDayUI();
        SetMessage(scriptNumber);
        SetButtons(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rouletteStatus != 2)
        {
            if (scriptNumber <= Database.scripts[Database.age].script.Count)
            {
                if (Input.anyKeyDown)
                {
                    SetMessage(scriptNumber);
                }
            }

            if (!scriptDisplaying)
            {
                if (Input.anyKeyDown && rouletteStatus == 1)
                {
                    rouletteStatus = 2;
                }
            }
        }
    }

    #region 메세지

    /// <summary>
    /// 메시지보임
    /// </summary>
    /// <param name="number"></param>
    void SetMessage(int number)
    {
        if (scriptNumber >= Database.scripts[Database.age].script.Count)
        {
            if (Database.age < 30)
            {
                SetButtons(true);
            }
            else
            {
                SceneManager.LoadScene("Ending");
            }
        }
        else if (scriptDisplaying)
        {
            scriptDisplaying = false;
            scriptNumber++;
        }
        else
        {
            scriptDisplaying = true;
            StartCoroutine(DisplayMessage(string.Format(Database.scripts[Database.age].script[number], Database.babyName)));
        }
    }

    /// <summary>
    /// 메세지를 한글자씩 표시하는 코루틴
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    IEnumerator DisplayMessage(string msg)
    {
        for (int i = 0; i <= msg.Length; i++)
        {
            if (!scriptDisplaying)
            {
                labelMessage.text = msg;
                yield break;
            }
            labelMessage.text = msg.Substring(0, i);
            yield return new WaitForEndOfFrame();
        }

        scriptNumber++;
        scriptDisplaying = false;
        yield return null;
    }

    #endregion 메세지

    #region UI

    /// <summary>
    /// 상태값표시 UI
    /// </summary>
    void SetStatusUI()
    {
        StringBuilder sb = new StringBuilder();
        sb.Length = 0;

        switch (Database.spoon)
        {
            case SpoonColor.SOIL:
                sb.Append("흙수저");
                break;

            case SpoonColor.BRONZE:
                sb.Append("동수저");
                break;

            case SpoonColor.SILVER:
                sb.Append("은수저");
                break;

            case SpoonColor.GOLD:
                sb.Append("금수저");
                break;

            case SpoonColor.CSS:
                sb.Append("순(純)Silver수저");
                break;
        }
        sb.Append("\n");
        sb.Append("나이 ");
        sb.Append(Database.age);
        sb.Append("살");

        sb.Append("\n");
        sb.Append("자금 ");
        sb.Append(Database.money.ToString("N0"));

        sb.Append("\n");
        sb.Append("인생도 ");
        sb.Append(Database.life);

        labelStatus.text = sb.ToString();

        bgImage.sprite2D = ageSprites[Database.age] as Sprite;
        bgImage.MakePixelPerfect();
    }

    /// <summary>
    /// 버튼 자막들 바꾸는 스크립트
    /// </summary>
    void SetDayUI()
    {
        for (int i = 0; i < 3; i++)
        {
            //버튼 텍스트 입력
            labelButtonCaption[i].text = string.Format(Database.scripts[Database.age].button[i], Database.babyName);

            //비용 텍스트 입력.
            if (Database.scripts[Database.age].cost[i] > 0)
            {
                labelButtonCosts[i].text = string.Format("[{0:N0}]", Database.scripts[Database.age].cost[i]);
            }
            else if (Database.scripts[Database.age].cost[i] < 0)
            {
                labelButtonCosts[i].text = string.Format("[+{0:N0}]", Database.scripts[Database.age].cost[i] * -1);
            }
            else
            { 
                labelButtonCosts[i].text = "[공짜]";
            }
        }
    }

    //창관련
    public void OpenPopup(UIPanel panel)
    {
        panel.gameObject.SetActive(true);
        panel.alpha = 1;
    }

    public void ClosePopup(UIPanel panel)
    {
        panel.gameObject.SetActive(false);
    }

    #endregion UI

    #region 버튼

    public void OnGameButtonClick(GameObject btn)
    {
        switch (btn.name.Substring(6))
        {
            case "Gyro":
                SceneManager.LoadScene("Gyro");
                break;
            case "Stone":
                break;
            case "Parcel":
                Database.money += 10000;
                SetStatusUI();
                break;
        }
    }

    public void OnLifeButtonClick(GameObject btn)
    {
        int number = int.Parse(btn.name.Substring(6));

        int cost = Database.scripts[Database.age].cost[number];

        if (!CheckHaveMoney(cost)) return;

        if (rouletteNumber < 0)
        {
            switch (number)
            {
                case 0:
                    Database.life++;
                    break;
                case 2:
                    Database.life--;
                    break;
            }
        }

        Database.money -= cost;
        scriptNumber = 0;
        Database.age++;
        rouletteNumber = -1;
        rouletteStatus = 0;

        SetMessage(scriptNumber);
        SetStatusUI();
        SetDayUI();
        SetButtons(false);
    }

    bool CheckHaveMoney(int cost)
    {
        if (Database.money < cost)
        {
            OpenPopup(panelNoMoney);
            return false;
        }
        else
        {
            return true;
        }
    }

    void SetButtons(bool isOpen)
    {
        if (Database.scripts[Database.age].cost[0] <= 0 && rouletteNumber < 0)
        {
            RandomButton();
        }
        for (int i = 0; i < 3; i++)
        {
            buttons[i].SetActive(isOpen);
            labelButtonCosts[i].gameObject.SetActive(isOpen);
        }
    }

    //랜덤
    void RandomButton()
    {
        StartCoroutine("Roulette");
    }
    /// <summary>
    /// 랜덤 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Roulette()
    {
        rouletteStatus = 1;
        rouletteNumber = 0;

        for (int i = 0; i < 3; i++)
        {
            labelButtonCosts[i].gameObject.SetActive(false);
            buttons[i].GetComponent<BoxCollider>().enabled = false;
        }

        while (rouletteStatus == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                buttons[i].SetActive(false);
                labelButtonCosts[i].gameObject.SetActive(false);
            }

            buttons[rouletteNumber++].SetActive(true);
            yield return new WaitForSeconds(0.2f);

            if (rouletteNumber > 2) rouletteNumber = 0;
        }

        for (int i = 0; i < 3; i++)
        {
            buttons[i].SetActive(false);
            buttons[i].GetComponent<BoxCollider>().enabled = true;
        }
        buttons[rouletteNumber].SetActive(true);
    }

    /// <summary>
    /// 종료
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion 버튼

    #region 초기화

    void InitUI()
    {
        labelStatus = GameObject.Find("LabelStatus").GetComponent<UILabel>();

        for (int i = 0; i < 3; i++)
        {
            buttons[i] = GameObject.Find("Button" + i);
            labelButtonCaption[i] = GameObject.Find("Button" + i + "/Caption").GetComponent<UILabel>();
            labelButtonCosts[i] = GameObject.Find("Button" + i + "/Cost").GetComponent<UILabel>();
        }

        labelMessage = GameObject.Find("Message").GetComponent<UILabel>();

        panelNoMoney = GameObject.Find("PanelNoMoney").GetComponent<UIPanel>();

        bgImage = GameObject.Find("BGImage").GetComponent<UI2DSprite>();
    }

    #endregion 초기화
}