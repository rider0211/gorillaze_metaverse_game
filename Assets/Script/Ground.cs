using UnityEngine;

public class Ground : MonoBehaviour
{
    PlayerController player;
    RaycastHit2D hit;
    public LayerMask ignoreLayer;


    private void Start()
    {
        player = GetComponentInParent<PlayerController>();

    }

    private void Update()
    {
        hit = Physics2D.Raycast(transform.position, -transform.up, 0.5f, ~ignoreLayer);
        Debug.DrawRay(transform.position, -transform.up * 0.5f, Color.black);

        if (hit.collider != null && hit.collider.gameObject.tag == "Ground")
        {
            //Grounded
            player.toggleJump(true);
        }
        else
        {
            //Not Grounded

            player.toggleJump(false);
        }
    }

    void resetJump()
    {
        player.toggleJump(true);
    }

}
