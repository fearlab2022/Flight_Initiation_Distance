using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4f;

    private Rigidbody2D body;
    private bool moving = false;

    Vector2 initialPosition = new Vector2(49.5f, 1f);

    /**
    reset the position and state of player
    */
    public void reset()
    {
        body.velocity = new Vector2(0f, 0f);
        transform.position = initialPosition;
        moving = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moving = true;
        }
        if (moving)
        {
            Vector2 movement = new Vector2(speed, body.velocity.y);
            body.velocity = movement;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Message result = GameObject.FindWithTag("Finish").GetComponent<Message>();
        ScoreDisplay score = GameObject.FindWithTag("Score").GetComponent<ScoreDisplay>();

        if (other.gameObject.layer == 6)
        {
            score.caught();
            result.caught();
        }
        else if (other.gameObject.layer == 8)
        {
            score.escaped();
            result.escaped();
        }
        Time.timeScale = 0;
    }
}
