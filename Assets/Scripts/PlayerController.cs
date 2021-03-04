using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Field Declarations

    [SerializeField] private ProjectileController projectilePrefab;
    [SerializeField] private GameObject availableBullet;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject expolsion;
    //Set by GameSceneController
    [HideInInspector] public float shieldDuration;
    [HideInInspector] public float speed;

    private bool projectileEnabled = true;
    private WaitForSeconds shieldTimeOut;
    
    #endregion

    #region Startup

    private void Start()
    {
        shieldTimeOut = new WaitForSeconds(shieldDuration);
        EnableShield();
    }

    #endregion

    #region Movement & Control

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (projectileEnabled)
            {
                FireProjectile();
            }
        }
    }

    private void MovePlayer()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        if(Mathf.Abs(horizontalMovement) > Mathf.Epsilon)
        {
            horizontalMovement = horizontalMovement * Time.deltaTime * speed;
            horizontalMovement += transform.position.x;

            float limit =
                Mathf.Clamp(horizontalMovement, ScreenBounds.left, ScreenBounds.right);

            transform.position = new Vector2(limit, transform.position.y);
        }
    }

    #endregion

    #region Projectile Management

    public void EnableProjectile()
    {
        projectileEnabled = true;
        availableBullet.SetActive(projectileEnabled);
    }

    public void DisableProjectile()
    {
        projectileEnabled = false;
        availableBullet.SetActive(projectileEnabled);
    }

    private void FireProjectile()
    {
        Vector2 spawnPosition = availableBullet.transform.position;

        ProjectileController projectile =
            Instantiate(projectilePrefab, spawnPosition, Quaternion.AngleAxis(90, Vector3.forward));

       projectile.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        projectile.gameObject.layer = LayerMask.NameToLayer("PlayerProjectile");
        projectile.isPlayers = true;
        projectile.projectileSpeed = 4;
        projectile.projectileDirection = Vector2.up;
    }

    #endregion

    #region Damage

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<ProjectileController>())
            TakeHit();
    }

    private void TakeHit()
    {
        GameObject xp = Instantiate(expolsion, transform.position, Quaternion.identity);
        xp.transform.localScale = new Vector2(2, 2);

        Destroy(gameObject);
    }

    #endregion

    #region Shield Management

    public void EnableShield()
    {
        shield.SetActive(true);
        StartCoroutine(DisableShield());
    }

    private IEnumerator DisableShield()
    {
        yield return shieldTimeOut;
        shield.SetActive(false);
        
    }

    #endregion
}
