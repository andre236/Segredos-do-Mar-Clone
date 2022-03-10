using UnityEngine;
using UnityEngine.UI;
using Entities;
using Manager;

namespace Miscellaneous
{
    public class GetAchievement : MonoBehaviour
    {
        private Text _titleFishTXT;
        private Image _imageFish;
        private Text _amountRemovedTXT;
        private Text _amountBoughtTXT;
        [SerializeField]
        private FishObject _fishSettings;
        void Awake()
        {
            _titleFishTXT = transform.Find("titleFishTXT").GetComponent<Text>();
            _imageFish = transform.Find("imageFish").GetComponent<Image>();
            _amountRemovedTXT = transform.Find("AmountRemovedTXT").GetComponent<Text>();
            _amountBoughtTXT = transform.Find("AmountBuyTXT").GetComponent<Text>();
        }

        private void Start()
        {
            _titleFishTXT.text = _fishSettings.NameFish;
            _imageFish.sprite = _fishSettings.SpriteFish;

            if (PlayerPrefs.HasKey("BOUGHT_" + _fishSettings.NameFish))
                _amountBoughtTXT.text = string.Concat("x", PlayerPrefs.GetInt("BOUGHT_" + _fishSettings.NameFish));
            else
                _amountBoughtTXT.text = "x0";

            if (PlayerPrefs.HasKey("BREED_" + _fishSettings.NameFish))
                _amountRemovedTXT.text = string.Concat("x", PlayerPrefs.GetInt("BREED_" + _fishSettings.NameFish));
            else
                _amountRemovedTXT.text = "x0";
        }

        private void OnEnable()
        {
            if (PlayerPrefs.HasKey("BOUGHT_" + _fishSettings.NameFish))
                _amountBoughtTXT.text = string.Concat("x", PlayerPrefs.GetInt("BOUGHT_" + _fishSettings.NameFish));
            else
                _amountBoughtTXT.text = "x0";

            if (PlayerPrefs.HasKey("BREED_" + _fishSettings.NameFish))
                _amountRemovedTXT.text = string.Concat("x", PlayerPrefs.GetInt("BREED_" + _fishSettings.NameFish));
            else
                _amountRemovedTXT.text = "x0";
        }

    }
}