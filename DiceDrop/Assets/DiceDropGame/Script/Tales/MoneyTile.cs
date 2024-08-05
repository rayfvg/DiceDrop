using UnityEngine;

public class MoneyTile : MonoBehaviour
{

    public ParticleSystem partical;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Wallet>(out Wallet wallet))
        {
            wallet.AddMoney(1);
            partical.Play();
            Destroy(gameObject, 0.3f);
        }
    }
}
