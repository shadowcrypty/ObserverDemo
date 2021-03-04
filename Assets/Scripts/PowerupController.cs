using UnityEngine;

public class PowerupController :MonoBehaviour
{
    #region Field Declarations

    public GameObject explosion;

    [SerializeField]
    private PowerType powerType;

    #endregion

    #region Movement

    void Update()
    {
       Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 3, Space.World);

        if (ScreenBounds.OutOfBounds(transform.position))
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Collisons

    private void OnCollisionEnter2D(Collision2D collision)
    {
       //TODO: Apply Power ups
       
       Destroy(gameObject);
    }

    #endregion
}

public enum PowerType
{
    Shield,
    X2
};