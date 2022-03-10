using UnityEngine;

namespace Miscellaneous
{
    public class AutoDestruction : MonoBehaviour
    {
        private void OnEnable()
        {
            Destroy(gameObject, 1f);
        }
    }
}