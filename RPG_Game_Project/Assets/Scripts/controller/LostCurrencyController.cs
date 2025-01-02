using UnityEngine;

public class LostCurrencyController : MonoBehaviour
{
   public int Currency;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {
            int rancurrency = Random.Range(10, 500);
            Currency = rancurrency;
            PlayerManager.instance.Currency += Currency;
            Destroy(gameObject);
        }
    }
}
