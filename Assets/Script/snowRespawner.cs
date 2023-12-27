using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowRespawner : MonoBehaviour
{
    public Vector3 respawnPos = Vector3.zero;

    [Header ("HorizontalSpeed")]
    [SerializeField] float speedX;
    PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (player.canJump)
        {
            transform.Translate((player.rb.velocity.x * 0.2f) * speedX * Time.deltaTime, -player.rb.velocity.y * 0.01f * Time.deltaTime, 0);

            //transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, posY, 3f * Time.deltaTime));
        }
        else
        {
            transform.Translate((player.rb.velocity.x * 0.2f) * speedX * 0.7f * Time.deltaTime, -player.rb.velocity.y * 0.01f * Time.deltaTime, 0);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");
        if (collision.gameObject.tag.Equals("respawn"))
        {
            transform.position = respawnPos;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("respawn"))
        {
            transform.position = respawnPos;
        }
    }
}

