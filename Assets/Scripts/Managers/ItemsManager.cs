using System;
using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Manager
{
    public class ItemsManager : MonoBehaviour
    {

        private GameObject _foodPage;
        private GameObject _remedyPage;

        private bool _normalFoodSelected;
        private bool _averageFoodSelected;
        private bool _superFoodSelected;

        private bool _remedySelected;

        public delegate void SelectedItemsHandler();
        public delegate void UsedItemHandler();

        public event SelectedItemsHandler NormalFoodSelected;
        public event SelectedItemsHandler AverageFoodSelected;
        public event SelectedItemsHandler SuperFoodSelected;
        public event SelectedItemsHandler RemedySelected;

        public Action UsedAverageFood { get; internal set; }
        public Action UsedSuperFood { get; internal set; }
        public Action UsedNormalFood { get; internal set; }
        public Action UsedRemedy { get; internal set; }
        public Action UnselectedAllItems { get; internal set; }



        private void Update()
        {
            CheckInputs();
        }

        void CheckInputs()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            bool AbleToUse = mousePosition.y >= -2.5f;

            if (Input.GetMouseButtonDown(0) && _normalFoodSelected && AbleToUse)
            {
                UsedNormalFood();
            }
            if (Input.GetMouseButtonDown(0) && _averageFoodSelected && AbleToUse)
            {
                UsedAverageFood();
            }
            if (Input.GetMouseButtonDown(0) && _superFoodSelected && AbleToUse)
            {
                UsedSuperFood();
            }
            if (Input.GetMouseButtonDown(0) && _remedySelected && AbleToUse)
            {
                UsedRemedy();
            }
        }

        // -- Reference in Button on Scene -- //
        public void SelectingNormalFood()
        {
            _normalFoodSelected = true;
            _averageFoodSelected = false;
            _superFoodSelected = false;
            _remedySelected = false;

            NormalFoodSelected?.Invoke();

        }

        // -- Reference in Button on Scene -- //
        public void SelectingAverageFood()
        {
            _normalFoodSelected = false;
            _averageFoodSelected = true;
            _superFoodSelected = false;
            _remedySelected = false;
            AverageFoodSelected?.Invoke();
        }

        // -- Reference in Button on Scene -- //
        public void SelectingSuperFood()
        {
            _normalFoodSelected = false;
            _averageFoodSelected = false;
            _superFoodSelected = true;
            _remedySelected = false;

            SuperFoodSelected?.Invoke();
        }

        // -- Remedy Section -- //

        public void SelectingRemedy()
        {
            _normalFoodSelected = false;
            _averageFoodSelected = false;
            _superFoodSelected = false;

            _remedySelected = true;
            RemedySelected?.Invoke();
        }

        public void DeactiveAllItems()
        {
            _normalFoodSelected = false;
            _averageFoodSelected = false;
            _superFoodSelected = false;
            _remedySelected = false;

            UnselectedAllItems?.Invoke();
        }

    }
}