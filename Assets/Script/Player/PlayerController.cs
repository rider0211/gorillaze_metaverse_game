using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header ("Movement Values")]
    public float moveSpeed =10f;
    public float jumpForce =10f;
    public float autoMoveSpeed = 5f;
    public float fallSpeed = 2.5f;

    public Rigidbody2D rb;
    Animator anim;

    public bool canJump = false, canMove = true, canParallelx = false;
    float moveWhileJump = 0, tempSpeed = 0;

    private void Start() 
    {
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        tempSpeed = autoMoveSpeed;
    }

#region Singleton

    public static PlayerController instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion
    private void FixedUpdate() 
    {
        if (canMove)
        {
            autoMovePlayer();

            //keyboardMovement();

            if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKey(KeyCode.Space)))
            {
                jump();
            }

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

            //anim.SetFloat("Run", 0);
        }

    }    
 
    void autoMovePlayer()
    {

        rb.velocity = new Vector2(autoMoveSpeed, rb.velocity.y);

        anim.SetFloat("Run", Mathf.Abs(autoMoveSpeed));

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallSpeed - 1) * Time.deltaTime;
        }


        if (!canJump)
        {
            rb.transform.position += new Vector3(Mathf.Clamp(moveWhileJump, 0, 0.05f), 0, 0);
            moveWhileJump += 0.009f;

        }

    }
    void keyboardMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveSpeed * horizontalInput, rb.velocity.y);

        anim.SetFloat("Run", (Mathf.Abs(moveSpeed * horizontalInput)));

        //flip Sprite
        if (moveSpeed * horizontalInput < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void jump()
    {
        if(canJump)
        {
            //rb.velocity =new Vector2(rb.velocity.x,jumpForce);

            //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

            rb.velocity = Vector2.up * jumpForce;

            anim.SetBool("Jump", true);

            canJump = false;
        }
    }
    public void toggleJump(bool enable)
    {
        if (enable)
        {
            canJump = true;
            moveWhileJump = 0;
        }
        else
        {
            canJump = false;
        }
    }

    IEnumerator deathCondition()
    {
        canMove = false;

        anim.SetTrigger("Death");

        yield return new WaitForSeconds(0.5f);

        UiManager.instance.gamePlayPanel_GameObject.SetActive(false);

        if (HealthManager.instance.getHealth() < 1)
        {
            UiManager.instance.GameOverPanel();
        }
        else
        {
            UiManager.instance.RetryPanel();
        }
    }
    IEnumerator winCondition()
    {
        AudioManager.instance.playSFX("winSound");

        GameManager.instance.updateTotalCoins(levelManager.instance.getCoin());

        anim.SetFloat("Run", 0);

        canMove = false;

        anim.SetTrigger("Finish");

        yield return new WaitForSeconds(0.5f);

        UiManager.instance.gamePlayPanel_GameObject.SetActive(false);

        UiManager.instance.LevelCompletePanel();
    }

    #region Collisions
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && !canJump && !anim.GetBool("Jump"))
        {

            anim.SetBool("pressedJumpButton", true);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (anim.GetBool("pressedJumpButton"))
            {
                anim.SetBool("pressedJumpButton", false);
            }
            else
            {
                anim.SetBool("Jump", false);
            }

            Instantiate(UiManager.instance.footDust_FX, transform.GetChild(1).transform.position, Quaternion.identity);

            autoMoveSpeed = 0;
            Invoke("reAssign", 0.07f);
        }

        if (canMove)
        {
            if (other.gameObject.tag.Equals("death"))
            {
                AudioManager.instance.bgMusic.Stop();

                AudioManager.instance.playSFX("gameOver");

                HealthManager.instance.removeHealth();

                StartCoroutine(deathCondition());
            }

            if (other.gameObject.tag.Equals("respawn"))
            {
                AudioManager.instance.playSFX("gameOver");

                StartCoroutine(deathCondition());
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (canMove)
        {
            if (other.gameObject.tag.Equals("startParallelx"))
            {
                canParallelx = true;
            }
            
            if (other.gameObject.tag.Equals("endParallelx"))
            {
                canParallelx = false;
            }

            if (other.gameObject.tag.Equals("Coin"))
            {
                AudioManager.instance.playSFX("coinPickUp");

                //UiManager.instance.pickUpCoin_FX.SetActive(false);

                Destroy(other.gameObject.transform.parent.gameObject);

                //UiManager.instance.pickUpCoin_FX.SetActive(true);

                levelManager.instance.updateCoin(+1);

            }

            if (other.gameObject.tag.Equals("alligatorHiss"))
            {
                AudioManager.instance.playSFX("alligatorHiss");

                other.gameObject.transform.GetComponent<crocodileAnimatorTrigger>().anim.SetTrigger("attack");

            }

            if (other.gameObject.tag.Equals("snakeHiss"))
            {
                AudioManager.instance.playSFX("snakeHiss");
            }

            if (other.gameObject.tag.Equals("Finish"))
            {
                AudioManager.instance.bgMusic.Stop();

                StartCoroutine(winCondition());
            }
        }
    }

    #endregion

    void reAssign()
    {
        autoMoveSpeed = tempSpeed;
    }

    public void StopPlayer()
    {
        anim.SetFloat("Run", 0);

        canMove = false;

        anim.SetTrigger("Finish");
    }
}
