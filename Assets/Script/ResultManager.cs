using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

    public GameObject circleBoard;
    public SpriteRenderer whiteArea;
    public SpriteRenderer resultImage;
    public Sprite soil;
    public Sprite bronze;
    public Sprite silver;
    public Sprite gold;
    public Sprite css;

    SpriteRenderer sr_cb = null;

    float[,] grade =
    { { 255f, 255f, 255f, 255f }, { 54f, 189f, 246f, 255f }, { 240f, 87f, 220f, 255f }, { 255f, 247f, 0f, 255f } };
    // 일반,                   희귀,                 영웅,                   전설

    public static ResultManager instance;

	// Use this for initialization
	void Start () {
        instance = this;

        circleBoard.SetActive(false);
        whiteArea.enabled = false;
        resultImage.sprite = null;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void result(int value)
    {
        circleBoard.SetActive(true);
        whiteArea.enabled = true;

        switch (value)
        {
            case 0:
                resultImage.sprite = soil;
                whiteArea.color = new Color(grade[3, 0], grade[3, 1], grade[3, 2], grade[3, 3]);
                break;
            case 1:
                resultImage.sprite = bronze;
                whiteArea.color = new Color(grade[3, 0], grade[3, 1], grade[3, 2], grade[3, 3]);
                break;
            case 2:
                resultImage.sprite = silver;
                whiteArea.color = new Color(grade[2, 0], grade[2, 1], grade[2, 2], grade[2, 3]);
                break;
            case 3:
                resultImage.sprite = gold;
                whiteArea.color = new Color(grade[1, 0], grade[1, 1], grade[1, 2], grade[1, 3]);
                break;
            case 4:
                resultImage.sprite = css;
                whiteArea.color = new Color(grade[0, 0], grade[0, 1], grade[0, 2], grade[0, 3]);
                break;
        }

        Debug.Log(whiteArea.color);
    }

    public void setCircleBoardColor(GameObject circleBoard)
    {
        if (sr_cb == null)
            sr_cb = circleBoard.GetComponent<SpriteRenderer>();

        sr_cb.color = whiteArea.color;
    }

    public void reset()
    {
        circleBoard.SetActive(false);
        whiteArea.enabled = false;
        resultImage.sprite = null;
    }
}
