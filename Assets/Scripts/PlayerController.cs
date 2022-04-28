using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float speed;
    private float boost;
    private bool gameOver;

    private Rigidbody rb;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timeText;
    public GameObject endText;

    private int count;
    private float timeLeft;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 10;
        count = 0;
        boost = 5.0f;
        timeLeft = 20;
        SetCountText();
        SetTimeText();
        gameOver = false;
        endText.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        if (!gameOver)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
    }

    void SetTimeText()
    {
        timeText.text = "Time: " + timeLeft.ToString("#.00");
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            timeLeft = 0;
            SetTimeText();
            gameOver = true;
            GameOver();
        }
        SetTimeText();
    }

    void FixedUpdate()
    {
        if (!gameOver)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp") && !gameOver)
        {
            //other.gameObject.SetActive(false);
            float newY = other.transform.position.y;
            float newX = Random.Range(-9.0f, 9.0f);
            float newZ = Random.Range(-9.0f, 9.0f);
            other.transform.position = new Vector3(newX, newY, newZ);
            count++;
            timeLeft += boost;
            boost *= 0.9f;
            SetCountText();
        }
    }

    void GameOver()
    {
        //transform.position = new Vector3(0.0f, 0.5f, 0.0f);
        endText.SetActive(true);
    }
}
