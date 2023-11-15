using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    public RealtimeCsPy realtimecspy;
    /*�e��ϐ��ݒ�*/
    public float speed = 1.0f;
    public float jumpPow = 1.0f;
    private float x_speed;
    private string status_msg;
    new Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        realtimecspy = GameObject.Find("voiceRec").GetComponent<RealtimeCsPy>();
        status_msg = realtimecspy.receive_msg;
        Debug.Log(status_msg);
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        status_msg = realtimecspy.receive_msg;
        Debug.Log(status_msg);
        //player�ړ�
        PlayerMove();
        if ((Input.GetKeyDown(KeyCode.Space) && isGround())
          || (status_msg == "jump" && isGround()))
        {
            Jump();
        }
    }

    public void PlayerMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            x_speed = -speed;
        else if (Input.GetKey(KeyCode.RightArrow))
            x_speed = speed;
        else if (Input.GetKey(KeyCode.UpArrow))
            x_speed = 0;
        rigidbody2D.velocity = new Vector2(x_speed, rigidbody2D.velocity.y);
    }
    public void Jump()
    {
        rigidbody2D.AddForce(Vector2.up * jumpPow, ForceMode2D.Impulse);
    }
    public bool isGround()
    {
        return Physics2D.Linecast(transform.position - transform.right * 0.3f, transform.position - transform.up * 0.2f, groundLayer)
                || Physics2D.Linecast(transform.position + transform.right * 0.3f, transform.position - transform.up * 0.2f, groundLayer);
    }
}
