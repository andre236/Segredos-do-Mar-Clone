using System;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class GoldChest : MonoBehaviour
    {
        private AudioSource _openingChestSound;
        private AudioSource _collectingGoldSound;
        private AudioSource _generatingGoldSound;

        private Animator _chestAnimator;

        [field: SerializeField]
        public int CurrentChestGold { get; private set; } = 0;
        [field: SerializeField]
        public int MaxChestGold { get; private set; }

        public delegate void GoldChestFilledHandler(int amountGold);
        public event GoldChestFilledHandler GoldCollected;

        public Action<int, int, bool> ShowCurrentChestGold { get; internal set; }

        private void Awake()
        {
            _chestAnimator = GetComponent<Animator>();

            _openingChestSound = transform.Find("OpeningSound").GetComponent<AudioSource>();
            _collectingGoldSound = transform.Find("CollectingSound").GetComponent<AudioSource>();
            _generatingGoldSound = transform.Find("GeneratedGold").GetComponent<AudioSource>();
        }


        private void OnMouseDown()
        {
            CollectGold();
        }

        private void CollectGold()
        {
            if (CurrentChestGold == MaxChestGold)
            {
                GoldCollected?.Invoke(CurrentChestGold);
                CurrentChestGold -= MaxChestGold;
                _collectingGoldSound.Play();
                AnimateChest();
            }
        }

        internal int OnBoughtChestExpansion(int newMaxGold)
        {
            return MaxChestGold = newMaxGold;
        }

        private void AnimateChest()
        {
            if (CurrentChestGold >= MaxChestGold)
            {
                if (!_openingChestSound.isPlaying && !_chestAnimator.GetBool("IsFull"))
                    _openingChestSound.Play();
                _chestAnimator.SetBool("IsFull", true);
            }
            else
            {
                _chestAnimator.SetBool("IsFull", false);
                //if(!_closingChestSFX.isPlaying)
                //_closingChestSFX.Play();
            }
        }

        public void OnGeneratedGold(int amount)
        {
            int some = CurrentChestGold + amount;
            if (some > MaxChestGold)
            {
                CurrentChestGold = MaxChestGold;
            }
            else
            {
                CurrentChestGold += amount;
            }
            AnimateChest();
            _generatingGoldSound.Play();
        }

        internal void OnStartedChestGold(int maxChestGold)
        {
            MaxChestGold = maxChestGold;
        }

        private void OnMouseOver()
        {
            ShowCurrentChestGold?.Invoke(MaxChestGold, CurrentChestGold, true);
        }

        private void OnMouseExit()
        {
            ShowCurrentChestGold?.Invoke(MaxChestGold, CurrentChestGold, false);
        }

    }
}