using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour {

    public float xSensitivity = 60f;
    public float ySensitivity = 60f;

    public bool inverted = false;

    public float minY = -60f;
    public float maxY = 60f;

    public bool clampX = false;
    public float minX = -60f;
    public float maxX = 60f;

    float y;
    float x;

    Transform body;

    // Use this for initialization
    void Start()
    {
        body = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (inverted)
        {
            mouseY *= -1f;
        }

        y += mouseY * ySensitivity * Time.deltaTime;
        y = Mathf.Clamp(y, minY, maxY);
        transform.localEulerAngles = new Vector3(-y, 0, 0);

        x += mouseX * xSensitivity * Time.deltaTime;
        if (clampX)
        {
            x = Mathf.Clamp(x, minX, maxX);
        }
        body.localEulerAngles = new Vector3(0f, x, 0);
    }
}
