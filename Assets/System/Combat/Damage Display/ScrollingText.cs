using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingText : MonoBehaviour 
{
    public float duration = 1f;
    public float speed;

    private TextMesh textMesh;
    private float startTime;

	void Awake () 
    {
        textMesh = GetComponent<TextMesh>();
        startTime = Time.time;
	}
	

	void Update () 
    {
        if(Time.time - startTime < duration)
        {
            transform.LookAt(Camera.main.transform);
            transform.Translate(Vector3.up * speed * Time.deltaTime);   
        }
        else
        {
            
            Destroy(gameObject);
        }
	}

    public void SetText(string text)
    {
        textMesh.text = text;    
    }

    public void SetColor(Color color)
    {
        textMesh.color = color;
    }
}
