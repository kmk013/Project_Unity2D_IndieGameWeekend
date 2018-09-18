using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour {
    private UILabel labelMessage;

    private int scriptNumber = 0;
    private bool scriptDisplaying = false;
    string[] texts = new string[3];
    private UI2DSprite bgImage;
    public Sprite[] endingSprites = new Sprite[3];

    UIButton buttonTitle;

    // Use this for initialization
    void Start () {
        labelMessage = GameObject.Find("Message").GetComponent<UILabel>();
        buttonTitle = GameObject.Find("ButtonTitle").GetComponent<UIButton>();
        buttonTitle.gameObject.SetActive(false);
        bgImage = GameObject.Find("BGImage").GetComponent<UI2DSprite>();

        if (Database.life > 10)
        {
            //해피엔딩
            bgImage.sprite2D = endingSprites[0] as Sprite;
            bgImage.MakePixelPerfect();

            texts[0] = "훌륭하게 자라준 {0}.";
            texts[1] = "어느덧 독립하여 새로 결혼하고 가정도 꾸리게 되었습니다. 부모님을 꼭 모시고 살겠다며 대가족을 이룬 {0}이네.";
            texts[2] = "어느 좋은 날 가족사진을 찍으며 가족은 행복한 시간을 보내고 있습니다.";
        }
        else if (Database.life < -10)
        {
            //배드엔딩
            bgImage.sprite2D = endingSprites[1] as Sprite;
            bgImage.MakePixelPerfect();

            texts[0] = "{0}의 비행은 어디부터 잘못 된 걸까요? 왜 행복하지 못할까요?";
            texts[1] = "언젠가 돈 달라던 전화 이후로 마지막 전화를 받은지 얼마나 지난지 모르겠습니다.";
            texts[2] = "얼굴이라도 한 번 보고싶은 마음입니다.";
        }
        else
        {
            //노말엔딩
            bgImage.sprite2D = endingSprites[2] as Sprite;
            bgImage.MakePixelPerfect();

            texts[0] = "{0}이는 열심히 살아 이제 겨우 사회에 자리잡았습니다.";
            texts[1] = "바쁜 일상생활, 회사일에 쫓겨 살아 명절이 아니면 얼굴 보기도 힘듭니다.";
            texts[2] = "용돈이라도 쓰라며 매월 들어오는 통장을 보며 부모님은 눈물을 흘립니다.";
        }

        SetMessage(0);
    }

    void Update()
    {
            if (scriptNumber <= 3)
            {
                if (Input.anyKeyDown)
                {
                    SetMessage(scriptNumber);
                }
            }
    }

    void SetMessage(int number)
    {
        if (scriptNumber >= 3)
        {
            buttonTitle.gameObject.SetActive(true);
            
        }
        else if (scriptDisplaying)
        {
            scriptDisplaying = false;
            scriptNumber++;
        }
        else
        {
            scriptDisplaying = true;
            StartCoroutine(DisplayMessage(string.Format(texts[number], Database.babyName)));
        }
    }

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

    public void Reboot()
    {
        Database.money =0;
        Database.age=0;
        Database.life=0;
        SceneManager.LoadScene("Title");
    }
}
