using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Gyro : MonoBehaviour {

	private Gyroscope gyro;
    Quaternion quat = Quaternion.identity;
    private UIProgressBar progressBar;
    private float time = 30.0f;
    UILabel labelTimer;
    bool isStart = false;

    void Start () {
		gyro = Input.gyro;
		gyro.enabled = true;

        labelTimer = GameObject.Find("Label").GetComponent<UILabel>();
        progressBar = GameObject.Find("GuageBar").GetComponent<UIProgressBar>();
        progressBar.value = 0;
    }

    void Update()
    {
        if (Input.anyKeyDown && time > 0)
        {
            isStart = true;
        }

        if (isStart)
        {
            time -= Time.deltaTime;
            labelTimer.text = string.Format("Time : {0:N0}", time);

            quat.w = gyro.attitude.w;
            quat.x = -gyro.attitude.x;
            quat.y = 0;
            quat.z = 0;

            transform.rotation = Quaternion.Euler(90, 0, 0) * quat;

            chkGame();
        }
    }

	void chkGame(){
		if (time <= 0.001f) {
            //labelTimer.text = string.Format("Time : {0:N0}", time);
            time = 0;
            labelTimer.text = "Clear!!\n ++10000";
            Database.money += 10000;
            isStart = false;
		}
		else if (transform.rotation.x >= 0.25f || transform.rotation.x <= -0.25f) {
			if (progressBar.value >= 0.99f) {
                labelTimer.text = "GameOver";
                isStart = false;
			} else {
                progressBar.value += 0.01f;
			}
		}
	}

    public void QuitGame()
    {
        SceneManager.LoadScene("Main");
        //Application.Quit();
    }
}
