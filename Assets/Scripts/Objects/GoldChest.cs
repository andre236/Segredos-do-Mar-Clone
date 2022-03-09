using System;
using UnityEngine;

namespace Objects
{
    public class GoldChest : MonoBehaviour
    {
        private AudioSource _openingChestSFX, _closingChestSFX, _collectingSFX;

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

            _openingChestSFX = transform.Find("OpeningSFX").GetComponent<AudioSource>();
            _closingChestSFX = transform.Find("ClosingSFX").GetComponent<AudioSource>();
            _collectingSFX = transform.Find("CollectingSFX").GetComponent<AudioSource>();
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
                if(!_collectingSFX.isPlaying)
                _collectingSFX.Play();
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
                if(!_openingChestSFX.isPlaying && !_chestAnimator.GetBool("IsFull"))
                _openingChestSFX.Play();
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