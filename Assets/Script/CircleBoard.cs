using UnityEngine;
using System.Collections;

public class CircleBoard : MonoBehaviour {

    public static Vector3 init_vec3 = new Vector3(-0.05f, 1.475f, 1.0f);
    public static Vector3 init_rot = new Vector3(8.2f, 40.0f, 2.5f);

    public float speed = 2000.0f;
    float time = 0.0f;
	
	// Update is called once per frame
	void Update () {
        if (time >= 1.2f && time < 3.7f)
        {
            transform.Rotate(new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime, Space.Self);
            speed -= (time - 1.2f) * 13;

            if (speed < 0.0f)
                speed = 0.0f;

            //Debug.Log("time : " + time + ", speed : " + speed);
        }
        else if (time >= 3.7f && (transform.position != init_vec3 || transform.rotation != Quaternion.Euler(init_rot)))
        {
            transform.position = init_vec3;
            transform.rotation = Quaternion.Euler(init_rot);
        }
        time += Time.deltaTime;
    }

    void OnDisable()
    {
        time = 0.0f;
        speed = 2000.0f;

        transform.position = init_vec3;
        transform.rotation = Quaternion.Euler(init_rot);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
