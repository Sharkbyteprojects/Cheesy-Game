using UnityEngine;
using UnityEngine.SceneManagement;
public class boss : MonoBehaviour
{
    Transform player;
    public Transform activator, bSpawn;
    public GameObject Bullet;
    public int bosslives = 20, playeruplives = 10;
    public float bossSpeed = 4f, cooldown = 6f, cooldownMinus = 0.6f, hitcooldown = 10f;
    float counting = 0f, hitc = 0f;
    public string target = "";
    public displayText dtxt;
    string iT = "";
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        iT = dtxt.TextToDisplay;
    }
    bool firstpass = true;
    // Update is called once per frame
    void LateUpdate()
    {
        if (counting > 0f)
        {
            counting -= Time.deltaTime;
        }
        if (hitc > 0f)
        {
            hitc -= Time.deltaTime;
        }
        if (bosslives == 0)
        {
            player.parent.gameObject.GetComponent<characterController>().score += 20;
            SceneManager.LoadScene(target);
            Destroy(gameObject);
            return;
        }
        if (player.position.y < activator.position.y)
        {
            int bht = Mathf.RoundToInt(hitc);
            if (bht < 0)
            {
                bht = 0;
            }
            dtxt.TextToDisplay = string.Format(iT, bosslives, bht);
            if (firstpass)
            {
                firstpass = false;
                player.parent.gameObject.GetComponent<characterController>().lives += playeruplives;
            }
            transform.position = new Vector3(transform.position.x, Vector3.MoveTowards(transform.position, player.position, bossSpeed).y, transform.position.z);
            if (counting <= 0f)
            {
                Vector2 dir = Vector2.left;
                Debug.DrawRay(transform.position, dir, Color.red);
                RaycastHit2D rh = Physics2D.Raycast(bSpawn.position, dir, Mathf.Infinity);
                if (rh.collider != null)
                {
                    if (rh.collider.gameObject.tag == "Player")
                    {
                        Debug.Log(rh.collider.gameObject.tag);
                        Instantiate(Bullet, bSpawn.position, Quaternion.identity, transform.parent);
                        counting = cooldown;
                        cooldown -= cooldownMinus;
                        if (cooldown <= 1f)
                        {
                            cooldown = 1f;
                        }
                    }
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hitc <= 0f)
        {
            if (collision.gameObject.tag == "Player")
            {
                bosslives--;
                if (bosslives < 0)
                {
                    bosslives = 0;
                    return;
                }
                player.parent.gameObject.GetComponent<characterController>().score++;
                hitc = hitcooldown;
            }
        }
    }
}
