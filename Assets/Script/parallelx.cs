using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallelx : MonoBehaviour
{

    [SerializeField] float speedX;

    PlayerController player;

    float posY = 0;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        posY = transform.position.y;
    }
    void Update()
    {
        if (player.canMove && player.canParallelx)
        {
            if (player.canJump)
            {
                transform.Translate((player.rb.velocity.x * 0.2f)* speedX * Time.deltaTime, -player.rb.velocity.y * 0.01f * Time.deltaTime, 0);

                //transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, posY, 3f * Time.deltaTime));
            }
            else
            {
                transform.Translate((player.rb.velocity.x * 0.2f) * speedX * 0.7f * Time.deltaTime, -player.rb.velocity.y * 0.01f * Time.deltaTime, 0);
            }
        }
    }
}
