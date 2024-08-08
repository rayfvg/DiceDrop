using UnityEngine;

public class MoneyTile : MonoBehaviour
{

    public ParticleSystem partical;
    public AudioSource TakeCoin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Wallet>(out Wallet wallet))
        {
            wallet.AddCurrentMoney();
            partical.Play();
            TakeCoin.Play();
            Destroy(gameObject, 0.3f);
        }
    }
}
