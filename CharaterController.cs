using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaterController : MonoBehaviour
{
    public float myhealthValue;
    private float myHealth;
    public bool inMenu = false;

    //Movement
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private float moveInput;
    private bool facingRight = true;
    public int score = 0;

    private int extraJump;
    public int extraJumpValue;

    private Rigidbody2D rb;

    //Jump
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    
    void Start()
    {
        myHealth = myhealthValue;
        extraJump = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        
        //AudioManenger.Instance.PlayMusic(AudioManenger.Instance.music02);
        AudioManenger.Instance.SetMusicVolume(0.15f);
    }

    private void Update()
    {
        #region Jump
        //Jump functions
        if (isGrounded == true)
        {
            extraJump = extraJumpValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            AudioManenger.Instance.PlaySFX(AudioManenger.Instance.playerJumpSfx);
            rb.velocity = Vector2.up * jumpForce;
            extraJump --;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJump == 0 && isGrounded == true)
        {
            AudioManenger.Instance.PlaySFX(AudioManenger.Instance.playerJumpSfx);
            rb.velocity = Vector2.up * jumpForce;
        }
        #endregion
        //IF player dies, Load MainMenu screen
        if (myHealth == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void FixedUpdate()
    {
        //Jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius, whatIsGround);

        //Movement
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        //Flips character depending on moveInput value
        if (facingRight == false && moveInput > 0)
        {
            flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            flip();
        }
    }

    //Flips Character function
    void flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If player collide with Spike = play audio, take damage
        if (other.gameObject.CompareTag("Spikes"))
        {
            AudioManenger.Instance.PlaySFX(AudioManenger.Instance.spikeTrapSfx);
            myHealth = myHealth - 1;
        }
        //If player collide with Kill trigger (fall out of map) = take damage
        if (other.gameObject.CompareTag("KillTrigger"))
        {
            myHealth = myHealth - 1;
        }
        //If player collide with Finish trigger = play audio, load Main menu
        if (other.gameObject.CompareTag("Finish"))
        {
            AudioManenger.Instance.PlaySFX(AudioManenger.Instance.FinishSfx);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
