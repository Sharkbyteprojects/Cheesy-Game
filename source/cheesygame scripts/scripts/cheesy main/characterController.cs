using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class characterController : MonoBehaviour
{
    public GameObject player, hat;
    public float speedOfPlayer = 20f, jumpHight = 20f, jumpSpeed = 20f, castLengh = 2f, maxVelo = 80f;
    bool offline = false, grounded = false, jumping = false, fj = false;
    public string horizontalInput = "Horizontal", SceneOnLoose = "gameOver";
    Rigidbody2D rrigidbody;
    const string storename = "store";
    store storage;
    float jumposaur = 0f, mia = 0f;
    Vector3 normalpos, lsp;
    public void setLSP(Vector3 sp)
    {
        lsp = sp;
    }
    public Sprite[] hatSelector;
    public int lives = 4;
    public int score
    {
        get { return storage.score; }
        set
        {
            if (value < 0)
            {
                storage.score = 0;
            }
            else
            {
                storage.score = value;
            }
        }
    }
    void death()
    {
        Analytics.CustomEvent("Killed", new Dictionary<string, object> {
            {"position", player.transform.position },
            {"score", score },
            {"death", true },
            {"lives", lives },
            {"level", SceneManager.GetActiveScene().name }
        });
        Debug.Log("Killed");
        SceneManager.LoadScene(SceneOnLoose);
    }
    public void kill()
    {
        Analytics.CustomEvent("Killed", new Dictionary<string, object> {
            {"position", player.transform.position },
            {"score", score },
            {"death", false },
            {"lives", lives },
            {"level", SceneManager.GetActiveScene().name  }
        });
        score -= 1;
        if (--lives < 1)
        {
            death();
        }
        else
        {
            player.transform.position = lsp;
        }
    }
    //Transform svepoint;
    public void mobileSetMia(float mias)
    {
        mia = mias;
    }
    public GameObject storeprefab;
    public void jumpMobile(bool val)
    {
        jumping = val;
    }
    public void dMobileJump()
    {
        fj = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject x = GameObject.FindGameObjectWithTag(storename);
        if (x == null)
        {
            x = Instantiate(storeprefab);
            DontDestroyOnLoad(x);
        }
        storage = x.GetComponent<store>();
        rrigidbody = player.GetComponent<Rigidbody2D>();
        normalpos = player.transform.position;
        lsp = normalpos;
        int selected = Mathf.RoundToInt(Random.Range(-1f, hatSelector.Length));
        if (selected < 0)
        {
            selected = 0;
        }
        else if (selected >= hatSelector.Length)
        {
            selected = hatSelector.Length - 1;
        }
        hat.GetComponent<SpriteRenderer>().sprite = hatSelector[selected];
        Debug.Log(selected);
    }
    // Update is called once per frame
    void Update()
    {
        if (offline)
        {
            return;
        }
        grounded = false;
        RaycastHit2D rc;
        Camera main = Camera.main;
        Vector3 x = main.transform.position;
        x.x = player.transform.position.x;
        if ((main.orthographicSize * 0.5f) + x.y < player.transform.position.y)
        {
            x.y += main.orthographicSize * 0.5f;
        }
        else if ((-(main.orthographicSize * 0.2f)) + x.y > player.transform.position.y)
        {
            x.y -= main.orthographicSize * 0.5f;
        }
        main.transform.position = x;
        Debug.DrawRay(player.transform.position, Vector2.down, Color.red);
        if (Physics2D.Raycast(player.transform.position, Vector2.down, castLengh).collider != null)
        {
            grounded = true;
        }
        float inputAxis = Input.GetAxis(horizontalInput);
        if (!(inputAxis < -0.1f || inputAxis > 0.1f))
        {
            inputAxis = mia;
        }
        Vector3 oldval = transform.position;
        if (grounded && (inputAxis < -0.1f || inputAxis > 0.1f))
        {
            oldval.x += inputAxis * Time.deltaTime * speedOfPlayer;
        }
        transform.position = oldval;
        if (inputAxis < 0f)
        {
            hat.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (inputAxis > 0f)
        {
            hat.GetComponent<SpriteRenderer>().flipX = false;
        }
        if ((Input.GetButton("Jump") || jumping) && (jumposaur > 0f || grounded))
        {
            Vector3 e = player.transform.position;
            float oneminu = Time.deltaTime * jumpSpeed;
            e.y += oneminu;
            if ((Input.GetButtonDown("Jump") || fj) && grounded)
            {
                jumposaur = jumpHight;
            }
            player.transform.position = e;
            jumposaur -= oneminu;
        }
        if (rrigidbody.velocity.y < -maxVelo)
        {
            death();
            rrigidbody.position = new Vector2(0f, 0f);
        }
        fj = false;
    }
}
