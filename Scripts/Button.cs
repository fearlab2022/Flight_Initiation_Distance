using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Color highlighColor = Color.yellow;
    public Color color = Color.white;

    Vector3 mouseDownScale = new Vector3(19f, 10f, 1f);
    Vector3 mouseUpScale = new Vector3(19.5f, 10.5f, 1f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = highlighColor;
    }

    void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

    void OnMouseDown()
    {
        transform.localScale = mouseDownScale;
    }

    void OnMouseUp()
    {
        transform.localScale = mouseUpScale;

        Message message = GameObject.FindWithTag("Finish").GetComponent<Message>();
        message.reset();

        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.reset();


        ScoreDisplay score = GameObject.FindWithTag("Score").GetComponent<ScoreDisplay>();
        score.reset();

        Predator predator = GameObject.FindWithTag("Predator").GetComponent<Predator>();
        predator.reset();



        Time.timeScale = 1f;
    }
}
