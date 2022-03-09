using UnityEngine;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] _audiosClip;
        private AudioSource _currentBGM;

        public static AudioManager Instance;

        private void Awake()
        {

        }



    }
}