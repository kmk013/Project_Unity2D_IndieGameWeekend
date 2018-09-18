using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private UILabel labelStart;

    private UIPanel panelSpoon;
    private UILabel labelSpoon;
    private UISprite spriteSpoon;

    UIPanel panelName;
    UIInput inputName;

    private UIProgressBar progressBar;

    void Start()
    {
        InitDatabase();
        InitUI();
    }

    /// <summary>
    /// 시작( 버튼을 누를 때
    /// </summary>
    public void OnPressStart()
    {
        if (Database.age < 0)
        {
            panelName.gameObject.SetActive(true);
            panelName.alpha = 1;
        }
        else
        {
            LoadNextScene();
        }
    }

    /// <summary>
    /// 게임 시작시 수저 선택.
    /// </summary>
    public void SelectSpoon()
    {
        panelSpoon.gameObject.SetActive(true);
        panelSpoon.alpha = 1;

        Database.spoon = (SpoonColor)Random.Range(0, 5);

        switch (Database.spoon)
        {
            case SpoonColor.SOIL:
                labelSpoon.text = "당신은 흙수저입니다.";
                spriteSpoon.spriteName = "spoonSoil";
                Database.money = 10000;
                break;

            case SpoonColor.BRONZE:
                labelSpoon.text = "당신은 동수저입니다.";
                spriteSpoon.spriteName = "spoonBronze";
                Database.money = 30000;
                break;

            case SpoonColor.SILVER:
                labelSpoon.text = "당신은 은수저입니다.";
                spriteSpoon.spriteName = "spoonSilver";
                Database.money = 50000;
                break;

            case SpoonColor.GOLD:
                labelSpoon.text = "당신은 금수저입니다.";
                spriteSpoon.spriteName = "spoonGold";
                Database.money = 100000;
                break;

            case SpoonColor.CSS:
                labelSpoon.text = "당신은 순(純)Silver수저입니다.";
                spriteSpoon.spriteName = "spoonCSS";
                Database.money = 10000000;
                break;
        }

        Database.age = 0;
    }
    
    public void SetName()
    {
        Database.babyName = inputName.value;
        //panelName.gameObject.SetActive(false);
        //SelectSpoon();

        AsyncOperation async = SceneManager.LoadSceneAsync("Box");
        progressBar.gameObject.SetActive(true);
        progressBar.value = async.progress;
    }

    /// <summary>
    /// 다음 씬으로 이동
    /// </summary>
    public void LoadNextScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("Main");
        progressBar.gameObject.SetActive(true);
        progressBar.value = async.progress;
    }

    /// <summary>
    /// UI 초기화
    /// </summary>
    public void InitUI()
    {
        labelStart = GameObject.Find("ButtonStart/Caption").GetComponent<UILabel>();

        labelSpoon = GameObject.Find("LabelSpoon").GetComponent<UILabel>();
        spriteSpoon = GameObject.Find("SpriteSpoon").GetComponent<UISprite>();

        panelSpoon = GameObject.Find("PanelSpoon").GetComponent<UIPanel>();
        panelSpoon.gameObject.SetActive(false);

        progressBar = GameObject.Find("LoadingBar").GetComponent<UIProgressBar>();
        progressBar.gameObject.SetActive(false);

        panelName = GameObject.Find("PanelName").GetComponent<UIPanel>();
        inputName = GameObject.Find("InputName").GetComponent<UIInput>();
        panelName.gameObject.SetActive(false);

        if (Database.age >= 0)
        {
            labelStart.text = "이어하기";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// DB 초기화
    /// </summary>
    public void InitDatabase()
    {
        //저장된 데이터 로드
        Database.age = PlayerPrefs.GetInt("Age", -1);
        Database.money = PlayerPrefs.GetInt("Money", 0);
        Database.life = PlayerPrefs.GetInt("Life", 0);
        Database.spoon = (SpoonColor)PlayerPrefs.GetInt("Spoon", 0);

        //대사 리스트 초기화
        Database.scripts = new List<ScriptPerDay>();

        //대사 내용 초기화
        for (int i = 0; i <= 30; i++)
        {
            ScriptPerDay tempScript = new ScriptPerDay();
            tempScript.button = new string[3];
            tempScript.cost = new int[3];
            tempScript.script = new List<string>();
            Database.scripts.Add(tempScript);
        }

        //0살
        Database.scripts[0].script.Add("세상에서 아주아주 귀여운 당신의 아이 {0}이 태어났어요!");
        Database.scripts[0].script.Add("우와~ 정말 귀엽죠! 이 아이가 건강하게 30살 까지 자라게 도와주세요.");
        //Database.scripts[0].script.Add("...");
        Database.scripts[0].script.Add("그네엄마 : 어머 {0}엄마 우리아이는 뢋데호텔에서 Dol잔치 할건데 자기는?");
        Database.scripts[0].button[0] = "어머! 자기야~ 당연하지 나도 거기야 ~^^ ";
        Database.scripts[0].cost[0] = 30000;
        Database.scripts[0].button[1] = "Dol잔치야 아무대서나 하면 되지 ~ 호호";
        Database.scripts[0].cost[1] = 10000;
        Database.scripts[0].button[2] = "우리형편에 Dol잔치는... ";
        Database.scripts[0].cost[2] = 0;

        //1살
        Database.scripts[1].script.Add("무럭무럭 자라나는 우리 {0}에게 오늘은 무엇을 만들어주지?");
        Database.scripts[1].button[0] = "고오급 이유식";
        Database.scripts[1].cost[0] = 10000;
        Database.scripts[1].button[1] = "이유식";
        Database.scripts[1].cost[1] = 5000;
        Database.scripts[1].button[2] = "미음";
        Database.scripts[1].cost[2] = 0;

        //2살
        Database.scripts[2].script.Add("아이가 음악을 옹알 거리는 것 같다. 무슨 음악을 들려 줄까?");
        Database.scripts[2].button[0] = "러블리즈의 Ah-Choo";
        Database.scripts[2].cost[0] = 5000;
        Database.scripts[2].button[1] = "베토벤 \"교황곡\"";
        Database.scripts[2].cost[1] = 3000;
        Database.scripts[2].button[2] = "직접 불러주는 노동요";
        Database.scripts[2].cost[2] = 0;

        //3살
        Database.scripts[3].script.Add("아이가 걸음마를 하며 나한테 뭐라 뭐라 말을건다. 나는 무슨 답을 할까?");
        Database.scripts[3].button[0] = "{0}도련님 어서오세요~";
        Database.scripts[3].cost[0] = 10000;
        Database.scripts[3].button[1] = "{0}이 안녕~";
        Database.scripts[3].cost[1] = 5000;
        Database.scripts[3].button[2] = "뭐 이 seedfoot련아";
        Database.scripts[3].cost[2] = 0;

        //4살
        Database.scripts[4].script.Add("싸X지 없는 읍읍 그네엄마가 다시 찾아왔다.");
        Database.scripts[4].script.Add("그네엄마 : {0}엄마 우리아이는 영어 유치원에 보낼려고 하는데 자기는?");
        Database.scripts[4].button[0] = "^^ 그네엄마 우리애 이미 거기다녀";
        Database.scripts[4].cost[0] = 30000;
        Database.scripts[4].button[1] = "영어는 무슨 한국어만 잘하면 돼";
        Database.scripts[4].cost[1] = 30000;
        Database.scripts[4].button[2] = "? 알아서 공부하겠지";
        Database.scripts[4].cost[2] = 0;

        //5살
        Database.scripts[5].script.Add("엄마 내 친구들은 다 논텐도4DX 있단 말야 빼애액 나도 사줘!");
        Database.scripts[5].button[0] = "어머! 당장 사줄게";
        Database.scripts[5].cost[0] = 10000;
        Database.scripts[5].button[1] = "쌰오미 논텐도DX4 사줄게...";
        Database.scripts[5].cost[1] = 5000;
        Database.scripts[5].button[2] = "공부해 ^^";
        Database.scripts[5].cost[2] = 0;

        //6살
        Database.scripts[6].script.Add("{0}이가 그만 그네랑 싸우다가 그네 코뼈를...");
        Database.scripts[6].button[0] = "나이스! 울 아들 장하다! (돈주며)";
        Database.scripts[6].cost[0] = 50000;
        Database.scripts[6].button[1] = "사내아이가 싸울 수 도 있죠...";
        Database.scripts[6].cost[1] = 10000;
        Database.scripts[6].button[2] = "왜 우리 {0}이 기를 죽이고 그래요!!!";
        Database.scripts[6].cost[2] = 0;

        //7살
        Database.scripts[7].script.Add("그네엄마 : 우리 그네에게 어쩜 상스러운 짓을 자식 교육을 시키는거야 마는거야?");
        Database.scripts[7].button[0] = "뭐? 머리채를 잡는다.";
        Database.scripts[7].cost[0] = 30000;
        Database.scripts[7].button[1] = "뭐? 욕을한다.";
        Database.scripts[7].cost[1] = 10000;
        Database.scripts[7].button[2] = "죄송합니다. 교육시킬게요...";
        Database.scripts[7].cost[2] = 0;

        //8살
        Database.scripts[8].script.Add("엄마들 : 우리 애들은 니뽄산 란도X을 사줄거야 자기는?");
        Database.scripts[8].button[0] = "훗... 독일 장인이 수 놓은 고급 백팩이지!";
        Database.scripts[8].cost[0] = 20000;
        Database.scripts[8].button[1] = "Oh ~ Ya Made In Korea";
        Database.scripts[8].cost[1] = 5000;
        Database.scripts[8].button[2] = "대대로 전해지는 고급 지게";
        Database.scripts[8].cost[2] = 0;

        //9살
        Database.scripts[9].script.Add("{0} : 엄마 영어 시간에 영단어를 배웠어 이 뜻은 뭐야? Sex");
        Database.scripts[9].button[0] = "남녀를 구분 짓는 단어야";
        Database.scripts[9].cost[0] = 10000;
        Database.scripts[9].button[1] = "아직은 때가 아니야";
        Database.scripts[9].cost[1] = 3000;
        Database.scripts[9].button[2] = "뭐긴 뭐야 사랑의 교미지~";
        Database.scripts[9].cost[2] = 0;

        //10살
        Database.scripts[10].script.Add("{0} : 엄마 그네가 이 문제 풀던데 이 문제의 답은 뭐야?");
        Database.scripts[10].button[0] = "E = mc^2";
        Database.scripts[10].cost[0] = 0;
        Database.scripts[10].button[1] = "전개가 다 안됨";
        Database.scripts[10].cost[1] = 0;
        Database.scripts[10].button[2] = "1+1=1";
        Database.scripts[10].cost[2] = 0;

        //11살
        Database.scripts[11].script.Add("아들과 계곡을 놀려 왔다. 여기서 나의 행동은?");
        Database.scripts[11].button[0] = "아들과 즐겁게 놀아준다.";
        Database.scripts[11].cost[0] = 10000;
        Database.scripts[11].button[1] = "아들이 노는 것을 지켜본다.";
        Database.scripts[11].cost[1] = 5000;
        Database.scripts[11].button[2] = "계곡은 역시 치맥이지";
        Database.scripts[11].cost[2] = 0;

        //12살
        Database.scripts[12].script.Add("엄마 ㅠㅠ 나 브론즈야...");
        Database.scripts[12].button[0] = "아들! 걱정하지마! 대리를 해준다 ";
        Database.scripts[12].cost[0] = 30000;
        Database.scripts[12].button[1] = "아들! 듀오 해줄게";
        Database.scripts[12].cost[1] = 10000;
        Database.scripts[12].button[2] = "뭐어? 브~으~로~오~온~즈 ?";
        Database.scripts[12].cost[2] = 0;

        //13살
        Database.scripts[13].script.Add("엄마 드디어 실버로 왔어!");
        Database.scripts[13].button[0] = "승리의 스킨을 받자! 대리를 한다";
        Database.scripts[13].cost[0] = 10000;
        Database.scripts[13].button[1] = "아들 골드로 올라가자";
        Database.scripts[13].cost[1] = 5000;
        Database.scripts[13].button[2] = "뭐어? 실~로~오~온~즈 ?";
        Database.scripts[13].cost[2] = 0;

        //14살, 랜덤
        Database.scripts[14].script.Add("드디어 자녀분이 중학교에 입학했습니다. 결과는?");
        Database.scripts[14].button[0] = "신식학교 이며 여학생들이 많음";
        Database.scripts[14].cost[0] = 0;
        Database.scripts[14].button[1] = "집근처";
        Database.scripts[14].cost[1] = 0;
        Database.scripts[14].button[2] = "남중";
        Database.scripts[14].cost[2] = 0;

        //15살
        Database.scripts[15].script.Add("아들이 중2병에 온 것 같다. 아들의 흑염룡 위치는?");
        Database.scripts[15].button[0] = "그런거 없다.";
        Database.scripts[15].cost[0] = 20000;
        Database.scripts[15].button[1] = "오른쪽 눈에 시냅스가 있다.";
        Database.scripts[15].cost[1] = 10000;
        Database.scripts[15].button[2] = "오른쪽 눈에 시냅스가 바닥에 마방진이 있다.";
        Database.scripts[15].cost[2] = 0;

        //16살
        Database.scripts[16].script.Add("엄마 저 고민있어요... 성적이 안좋은데 어쩔까요?");
        Database.scripts[16].button[0] = "아직은 중학생이야 너무 공부에 연연하지말고 좋아하는 것을 찾아 보면 어떨까?";
        Database.scripts[16].cost[0] = 30000;
        Database.scripts[16].button[1] = "공부를 조금 더 해보자";
        Database.scripts[16].cost[1] = 10000;
        Database.scripts[16].button[2] = "? 이게 성적이니? 에휴... 맨날 게임만 하니까";
        Database.scripts[16].cost[2] = 0;

        //17살
        Database.scripts[17].script.Add("아닛!... 어머님... 이건... (아들이 담배피다 걸렸다 어쩌면 좋을까?)");
        Database.scripts[17].button[0] = "호기심에 실수 할 수 있지 이제 피지말자 약속!";
        Database.scripts[17].cost[0] = 10000;
        Database.scripts[17].button[1] = "당장 안 꺼? 다시는 피지마";
        Database.scripts[17].cost[1] = 3000;
        Database.scripts[17].button[2] = "신발장에 있는 엑스칼리버를 꺼내 돼지 두르치기를 시전한다.";
        Database.scripts[17].cost[2] = 0;

        //18살
        Database.scripts[18].script.Add("요번에는 마의 고2병에 온 것 같다. 아들이 하고 있는 것은?");
        Database.scripts[18].button[0] = "건전한 게임 부모님 힘내요";
        Database.scripts[18].cost[0] = 5000;
        Database.scripts[18].button[1] = "조금 불건전한 게임 문명6(타임머신)";
        Database.scripts[18].cost[1] = 2000;
        Database.scripts[18].button[2] = "일루전사의 최신작 허니 셀렉트";
        Database.scripts[18].cost[2] = 0;

        //19살
        Database.scripts[19].script.Add("(아들이 침대에 누워있다. 많이 힘든가 보다)");
        Database.scripts[19].button[0] = "이불을 덮어주고 방을 청소해 준다.";
        Database.scripts[19].cost[0] = 10000;
        Database.scripts[19].button[1] = "이불을 덮어준다.";
        Database.scripts[19].cost[1] = 5000;
        Database.scripts[19].button[2] = "아들의 저금통을 스틸한다.";
        Database.scripts[19].cost[2] = 0;

        //20살, 랜덤
        Database.scripts[20].script.Add("아들이 로또를 사서 나에게 줬다.");
        Database.scripts[20].button[0] = "우옷! 1등!!!";
        Database.scripts[20].cost[0] = -100000;
        Database.scripts[20].button[1] = "오호~ 3등";
        Database.scripts[20].cost[1] = -30000;
        Database.scripts[20].button[2] = "결과가 나온 종이였다. (쓰레기)";
        Database.scripts[20].cost[2] = 0;

        //21살
        Database.scripts[21].script.Add("(아들이 다닌다는 S대에 연락을 해보았다. 과연 무슨 대답을 들을 수 있을까?)");
        Database.scripts[21].button[0] = "1년 장학생으로 선정되셨습니다.";
        Database.scripts[21].cost[0] = 50000;
        Database.scripts[21].button[1] = "성실하고 착한 학생입니다.";
        Database.scripts[21].cost[1] = 10000;
        Database.scripts[21].button[2] = "누...누구라구요?";
        Database.scripts[21].cost[2] = 0;

        //22살
        Database.scripts[22].script.Add("아들이 컴퓨터를 키고 갔다.");
        Database.scripts[22].button[0] = "열심히 작성한 포트폴리오";
        Database.scripts[22].cost[0] = 0;
        Database.scripts[22].button[1] = "네다음 웹툰";
        Database.scripts[22].cost[1] = 0;
        Database.scripts[22].button[2] = "직박구리 폴더";
        Database.scripts[22].cost[2] = 0;

        //23살
        Database.scripts[23].script.Add("(아들에게 우편이 왔다 무슨 내용일까?)");
        Database.scripts[23].button[0] = "합격통지서";
        Database.scripts[23].cost[0] = 30000;
        Database.scripts[23].button[1] = "동일이름의 잘못된 우편";
        Database.scripts[23].cost[1] = 10000;
        Database.scripts[23].button[2] = "군대 영장";
        Database.scripts[23].cost[2] = 0;

        //24살
        Database.scripts[24].script.Add("{0} : 어머니 저는 음악을 하고 싶습니다!");
        Database.scripts[24].button[0] = "오냐 너 하고 싶은거 해라 단 몸만 건강해라!";
        Database.scripts[24].cost[0] = 30000;
        Database.scripts[24].button[1] = "취미로만 하면 좋겠구나.";
        Database.scripts[24].cost[1] = 10000;
        Database.scripts[24].button[2] = "신발장에 있는 엑스칼리버를 꺼내 악기들을 도★륙낸다.";
        Database.scripts[24].cost[2] = 0;

        //25살
        Database.scripts[25].script.Add("아들의 공연이 사람들에게 어땠을까?");
        Database.scripts[25].button[0] = "많은 관객들이 박수를 보냈다.";
        Database.scripts[25].cost[0] = 10000;
        Database.scripts[25].button[1] = "관객들이 박수를 보냈다.";
        Database.scripts[25].cost[1] = 5000;
        Database.scripts[25].button[2] = "아쉽지만 내 아들은 음치다...";
        Database.scripts[25].cost[2] = 0;

        //26살
        Database.scripts[26].script.Add("어머니 전공을 바꿔볼라 합니다.");
        Database.scripts[26].button[0] = "아들의 인생을 위해 말린다.";
        Database.scripts[26].cost[0] = 5000;
        Database.scripts[26].button[1] = "유니티쨩이 그려진 책을 권해준다.";
        Database.scripts[26].cost[1] = 3000;
        Database.scripts[26].button[2] = "ㅋ";
        Database.scripts[26].cost[2] = 0;

        //27살
        Database.scripts[27].script.Add("(아들이 게임 개발을 열심히 한다. 결과물은?)");
        Database.scripts[27].button[0] = "3D스펙타클 온라인 MMORPG";
        Database.scripts[27].cost[0] = 30000;
        Database.scripts[27].button[1] = "2D런닝게임";
        Database.scripts[27].cost[1] = 10000;
        Database.scripts[27].button[2] = "유니티 실행중...만 30분";
        Database.scripts[27].cost[2] = 0;

        //28살
        Database.scripts[28].script.Add("{0}: 어머니 제가 이 행사에 참여할라고 합니다!");
        Database.scripts[28].button[0] = "아들에게 도트찍는법을 가르쳐 준다.";
        Database.scripts[28].cost[0] = 10000;
        Database.scripts[28].button[1] = "참여하라고 한다.";
        Database.scripts[28].cost[1] = 5000;
        Database.scripts[28].button[2] = "반대한다.";
        Database.scripts[28].cost[2] = 0;

        //29살, 랜덤요소
        Database.scripts[29].script.Add("결국 아들은 인디게임장에 입성 했다 아들의 운명은?");
        Database.scripts[29].button[0] = "아트 담당이 5명인 풍족한 개발자 ";
        Database.scripts[29].cost[0] = 0;
        Database.scripts[29].button[1] = "디자이너가 한명인 적당한 개발자";
        Database.scripts[29].cost[1] = 0;
        Database.scripts[29].button[2] = "도트찍는 개발자";
        Database.scripts[29].cost[2] = 0;

        //30살
        Database.scripts[30].script.Add("축하드립니다. 무사히 30살까지 자식을 키우셨군요!");
        Database.scripts[30].script.Add("어떠신가요? 비록 게임이라 과장과 허구성이 깊지만 부모님들이 얼마나 고민을 하시고 힘든신지 조금이라도 알게되었으면 좋겠습니다.");
        Database.scripts[30].script.Add("과연 아들은 어떤 모습으로 성장 했을까요?");
        Database.scripts[30].button[0] = "결과보기";
        Database.scripts[30].cost[0] = 10;
        Database.scripts[30].button[1] = "결과보기";
        Database.scripts[30].cost[1] = 10;
        Database.scripts[30].button[2] = "결과보기";
        Database.scripts[30].cost[2] = 10;
    }
}