using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text scoreText;
    public Text winText;

    private Rigidbody rb;
    private int count;
    private int score;
    private bool level2;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        score = 0;
        level2 = false;
        SetAllText ();
        winText.text = "";
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float colorR = Mathf.Abs(transform.position.x / 10);
        float colorG = Mathf.Abs(transform.position.y / 10);
        float colorB = Mathf.Abs(transform.position.z / 10);

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        Color colornow = new Vector4(colorR, colorG, colorB);
        GetComponent<Renderer>().material.color = colornow;

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            score = score + 1;
            SetAllText();
            if (count >= 12 && level2 == false)
            {
                transform.position = new Vector3(23.87f, transform.position.y, 0.56f);

                level2 = true;
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            score = score - 1;
            SetAllText();
        }
    }

    void SetAllText()
    {
        countText.text = "Count: " + count.ToString();
        scoreText.text = "Score: " + score.ToString();
        if (count >= 24)
        {
            winText.text = "You Finished With A Score Of: " + score.ToString();
        }
    }
}
