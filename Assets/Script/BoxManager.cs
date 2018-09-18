using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoxManager : MonoBehaviour {

    public GameObject box_left;
    public GameObject box_right;
    public GameObject stick_left;
    public GameObject stick_right;
    public GameObject particle_top;
    public GameObject particle_left;
    public GameObject particle_right;
    //public GameObject cb;
    //public GameObject CMR;
    public GameObject column_1;
    public GameObject column_2;
    public GameObject column_3;
    public GameObject box_back;
    public GameObject ys_left;
    public GameObject ys_right;
    public GameObject box_front;
    public GameObject circleBoard;
    public GameObject blur_top;
    public GameObject blur_left;
    public GameObject blur_right;
    public GameObject nextBtn;

    public Animator ani_box_left;
    public Animator ani_box_right;
    public Animator ani_stick_left;
    public Animator ani_stick_right;
    public Animator ani_cb;
    public Animator ani_cmr;

    public CircleBoard cb;
    //public CameraMagager cmr;
    public Text resultText;

    public static BoxManager instance;

    bool pressed = false;
    bool result = false;
    float time = 0.0f;
    int resultNum = -1;

    // Use this for initialization
    void Start () {
        instance = this;
        cb.enabled = false;
        //cmr.enabled = false;
        resultText.text = "";
        nextBtn.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            openRootBox();
        }
        if (pressed)
        {
            if (time >= 2.5f)
            {
                box_left.SetActive(false);
                box_right.SetActive(false);
                stick_left.SetActive(false);
                stick_right.SetActive(false);
                column_1.SetActive(false);
                column_2.SetActive(false);
                column_3.SetActive(false);
                box_back.SetActive(false);
                ys_left.SetActive(false);
                ys_right.SetActive(false);
                box_front.SetActive(false);
                blur_top.SetActive(false);
                blur_left.SetActive(false);
                blur_right.SetActive(false);

                ResultManager.instance.setCircleBoardColor(circleBoard);

                if (circleBoard.transform.position.Equals(CircleBoard.init_vec3))
                {
                    circleBoard.SetActive(false);
                }

                if (result)
                {
                    resultNum = ItemManager.instance.pick();
                    ResultManager.instance.result(resultNum);
                    ItemManager.instance.save();
                    result = false;
                }
            }

            if (time >= 4.5f)
            {
                switch (resultNum)
                {
                    case 0:
                        resultText.text = "당신은 흙수저입니다.";
                        Database.money = 10000;
                        break;
                    case 1:
                        resultText.text = "당신은 동수저입니다.";
                        Database.money = 30000;
                        break;
                    case 2:
                        resultText.text = "당신은 은수저입니다.";
                        Database.money = 50000;
                        break;
                    case 3:
                        resultText.text = "당신은 금수저입니다.";
                        Database.money = 100000;
                        break;
                    case 4:
                        resultText.text = "당신은 순(純)Silver수저입니다.";
                        Database.money = 10000000;
                        break;
                }
                nextBtn.SetActive(true);
            }

            Database.age = 0;

            time += Time.deltaTime;
        }
        

	}

    public void openRootBox()
    {
        if (!pressed)
        {
            SoundManager.instance.open();

            ani_cmr.SetBool("isClicked", true);

            particle_top.SetActive(false);
            particle_left.SetActive(false);
            particle_right.SetActive(false);

            ani_box_left.SetBool("isClicked", true);
            ani_box_right.SetBool("isClicked", true);
            ani_stick_left.SetBool("isClicked", true);
            ani_stick_right.SetBool("isClicked", true);
            ani_cb.SetBool("isClicked", true);

            cb.enabled = true;
            //cmr.enabled = true;

            result = true;
            pressed = true;
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void reset() // Next
    {
        ani_box_left.SetBool("isClicked", false);
        ani_box_right.SetBool("isClicked", false);
        ani_stick_left.SetBool("isClicked", false);
        ani_stick_right.SetBool("isClicked", false);
        ani_cb.SetBool("isClicked", false);
        ani_cmr.SetBool("isClicked", false);

        pressed = false;

        resultText.text = "";
        cb.enabled = false;

        time = 0.0f;

        particle_top.SetActive(true);
        particle_left.SetActive(true);
        particle_right.SetActive(true);

        circleBoard.SetActive(true);
        box_left.SetActive(true);
        box_right.SetActive(true);
        stick_left.SetActive(true);
        stick_right.SetActive(true);
        column_1.SetActive(true);
        column_2.SetActive(true);
        column_3.SetActive(true);
        box_back.SetActive(true);
        ys_left.SetActive(true);
        ys_right.SetActive(true);
        box_front.SetActive(true);
        blur_top.SetActive(true);
        blur_left.SetActive(true);
        blur_right.SetActive(true);
        nextBtn.SetActive(false);

        ResultManager.instance.reset();
    }
}
