using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField]
    private GameObject spark;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 sparkPos = other.transform.position;
        
        Destroy(other.gameObject);

        GameObject newSpark = Instantiate(this.spark, sparkPos, Quaternion.identity);
        newSpark.transform.localScale = new Vector2(.5f, .5f);

    }
    
}
