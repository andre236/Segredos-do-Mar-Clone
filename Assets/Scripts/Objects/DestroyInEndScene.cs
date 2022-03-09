using UnityEngine;

namespace Objects
{
    public class DestroyInEndScene : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("EndScene"))
            {
                Destroy(gameObject);
            }
        }
    }
}