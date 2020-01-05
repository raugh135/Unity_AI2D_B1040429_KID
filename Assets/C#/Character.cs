using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int speed = 50;
    public float jump = 0.5f;
    public string Name = "玩家";
    public bool pass = false;
    public bool isGround;

    [Header("血量"), Range(0, 200)]
    public float hp = 100;
    public Image hpBar;
    public GameObject final;

    private float hpMax;

    public UnityEvent onEat;

    private Rigidbody2D r2d;
    private Transform tran;
    private Animator ani;

    // Start is called before the first frame update
    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        tran = GetComponent<Transform>();
        ani = GetComponent<Animator>();
        hpMax = hp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) Turn(0);
        if (Input.GetKeyDown(KeyCode.A)) Turn(180);
        if (this.transform.position.y <= -6) final.SetActive(true);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Walk();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            onEat.Invoke();
        }
    }

    private void Walk()
    {
            r2d.AddForce(new Vector2(speed * Input.GetAxis("Horizontal"), 0));
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            r2d.AddForce(new Vector2(0, jump));
        }
    }

    private void Turn(int direction)
    {
        tran.eulerAngles = new Vector3(0, direction, 0);
    }

    public void Damage (float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / hpMax;

        if (hp <= 0) final.SetActive(true);
    }

}
