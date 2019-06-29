using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseFollow : MonoBehaviour
{
    public float xRotationDelta = 2f;
    public float xTranslationDelta = 2f;

    private float xRotationInitial;
    private float xTranslationInitial;

    // Start is called before the first frame update
    void Start()
    {
        Transform xf = GetComponent<Transform>();
        xRotationInitial = xf.eulerAngles.x;
        xTranslationInitial = xf.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float halfWidth = Screen.width * 0.5f;
        float halfHeight = Screen.height * 0.5f;
        float fracWidth = Mathf.Clamp((Input.mousePosition.x - halfWidth) / halfWidth, -1f, 1f);
        float fracHeight = Mathf.Clamp((Input.mousePosition.y - halfHeight) / halfHeight, -1f, 1f);

        Transform xf = GetComponent<Transform>();
        Vector3 pos = xf.position;
        pos.x =  xTranslationInitial + xTranslationDelta * fracWidth;
        xf.position = pos;
        Vector3 euler = xf.eulerAngles;
        euler.x = xRotationInitial + xRotationDelta * -fracHeight;
        xf.eulerAngles = euler;
    }
}
