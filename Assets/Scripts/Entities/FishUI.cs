using UnityEngine;
using UnityEngine.UI;
using Entities.enums;

namespace Entities
{
    public class FishUI : MonoBehaviour
    {
        private GameObject _generalStatus;
        private Image _currentLifeSpanBar;
        private Image _currentHealthFishBar;
        private Image _hungryImage;
        private Image _oceanImage;
        private Image _balloonImage;
        private Image _sickImage;
        private Image _reloadImage;

        private Text _currentLifeStageTXT;
        private Text _currentHealthFishTXT;
        private Text _currentNameFish;
        private Fish _fish;

        private LifeSpan _lifeSpan;
        private HealthFish _healthFish;
        private FishInteraction _fishInteraction;

        private void Awake()
        {
            _generalStatus = transform.Find("Canvas").transform.Find("GeneralStatus").gameObject;
            _currentHealthFishBar = _generalStatus.transform.Find("HealthBar").transform.Find("CurrentHealthBar").GetComponent<Image>();
            _currentLifeSpanBar = _generalStatus.transform.Find("LifeSpanBar").transform.Find("CurrentLifeSpanBar").GetComponent<Image>();
            _currentLifeStageTXT = _generalStatus.transform.Find("CurrentLifeStageTXT").GetComponent<Text>();
            _currentHealthFishTXT = _generalStatus.transform.Find("CurrentHealthStatusTXT").GetComponent<Text>();
            _currentNameFish = _generalStatus.transform.Find("NameFishTXT").GetComponent<Text>();
            _oceanImage = transform.Find("Canvas").transform.Find("ReadyToOcean").GetComponent<Image>();
            _balloonImage = transform.Find("Canvas").transform.Find("HealthStatus").GetComponent<Image>();
            _sickImage = transform.Find("Canvas").transform.Find("HealthStatus").transform.Find("SickImage").GetComponent<Image>();
            _hungryImage = transform.Find("Canvas").transform.Find("HealthStatus").transform.Find("HungryImage").GetComponent<Image>();
            _reloadImage = transform.Find("Canvas").transform.Find("ReloadIcon").GetComponent<Image>();

            _healthFish = GetComponent<HealthFish>();
            _fish = GetComponent<Fish>();
            _lifeSpan = GetComponent<LifeSpan>();
            _fishInteraction = GetComponent<FishInteraction>();

            _generalStatus.SetActive(false);
            _balloonImage.gameObject.SetActive(false);
            _oceanImage.gameObject.SetActive(false);
            _hungryImage.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateBarsAndText();
            ShowOceanImage();
            ShowHealthImageStage();
            ShowReloadImage();
        }

        void UpdateBarsAndText()
        {
            _currentNameFish.text = _fish.NameFish;
            _currentLifeStageTXT.text = _fish.CurrentLifeStage.ToString();
            _currentHealthFishTXT.text = _fish.CurrentHealthStatus.ToString();
            _currentLifeSpanBar.fillAmount = _lifeSpan.CurrentLifeTime / (float)_lifeSpan.MaxLifeTime;
            _currentHealthFishBar.fillAmount = _healthFish.CurrentHungry / _healthFish.MaxHungry;
        }

        void ShowHealthImageStage()
        {
            switch (_fish.CurrentHealthStatus)
            {
                case HealthStatus.Hungry:
                    _balloonImage.gameObject.SetActive(true);
                    _hungryImage.gameObject.SetActive(true);
                    _sickImage.gameObject.SetActive(false);
                    break;
                case HealthStatus.Sick:
                    _balloonImage.gameObject.SetActive(true);
                    _hungryImage.gameObject.SetActive(false);
                    _sickImage.gameObject.SetActive(true);
                    break;
                default:
                    _balloonImage.gameObject.SetActive(false);
                    _hungryImage.gameObject.SetActive(false);
                    _sickImage.gameObject.SetActive(false);
                    break;
            }

        }

        void ShowOceanImage()
        {
            if (_fish.CurrentLifeStage != LifeStage.Old)
                return;
            else
                _oceanImage.gameObject.SetActive(true);

        }

        void ShowReloadImage()
        {
            _reloadImage.fillAmount = _fishInteraction.CurrentTimeToRemove / _fishInteraction.MaxTimeToRemove;
        }

        private void OnMouseOver()
        {
            _generalStatus.SetActive(true);

        }

        private void OnMouseExit()
        {
            _generalStatus.SetActive(false);
        }

    }
}