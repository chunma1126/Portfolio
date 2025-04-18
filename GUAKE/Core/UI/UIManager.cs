using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Player")]
    public TextMeshProUGUI _ammoText;
    public TextMeshProUGUI coinText;
    public WeaponController Player;
    public Transform minimapCam;
    public Image hitCrossHair;
    public Image crossHair;
    public Image reloadImage;
    public Slider dashSlider;
    private bool isCrossChange;
    
    
    [Header("Bottle")] 
    public Image bottleImage;
    public TextMeshProUGUI bottleText;
    
    [Header("Graphic")]
    public Volume Volume;
    private bool screenEffectting;
    
    public DiePanel DiePanel;
    
    [Header("PopUpText")]
    public TextMeshProUGUI popUpText;
    private Vector3 popUpTextOriginScale;
    private bool onPopUpText;

    [Header("Settings")]
    public GameObject SettingPanel;
    public Slider musicSlider;
    public Slider sfxSlider;
    private void Start()
    {
        SetAmmoText();
        popUpTextOriginScale = popUpText.transform.localScale;
        popUpText.transform.localScale = Vector3.zero;
    }
    
    public void SetAmmoText()
    {
        _ammoText.SetText($"{Player.GetCurrentGun().gunMagazine.ammoInMagazine} <color=#bdc3c7>{Player.GetCurrentGun().gunMagazine.totalAmmo}</color>");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnOffSettingPanel();
        }
    }
    
    private void LateUpdate()
    {
        minimapCam.position = new Vector3(Player.transform.position.x , minimapCam.position.y , Player.transform.position.z);
    }
    public void BloodScreen(Color color ,float duration = 0.2f , float targetIntensity = 0.5f, float screenTime = 0f)
    {
        if(screenEffectting == true)return;
        
        if (Volume.profile.TryGet(out Vignette vignette))
        {
            
            vignette.color.value = color;
            vignette.color.overrideState = true;
            StartCoroutine(BloodScreenCoroutine(vignette , duration , targetIntensity ,screenTime));
        }
    }
    public void OnDiePanel()
    {
        DiePanel.OnPanel();
    }
    public void CoinText()
    {
        coinText.SetText($"coin:{PlayerStatController.Instance.PlayerStatSo._statDic[StatType.Money].GetValue()}");
    }
    IEnumerator BloodScreenCoroutine(Vignette vignette , float _duration , float _targetIntensity , float screenTime)
    {
        screenEffectting = true;
        
        float duration = _duration;
        float targetIntensity = _targetIntensity;
        float initialIntensity = vignette.intensity.value;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(initialIntensity, targetIntensity, elapsed / duration);
            yield return null;
        }
        
        yield return new WaitForSeconds(screenTime);
        
        elapsed = 0f;
        initialIntensity = vignette.intensity.value;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(initialIntensity, 0f, elapsed / duration);
            yield return null;
        }
        
        vignette.intensity.value = 0f; 
        
        screenEffectting = false;
    }
    public void ChangeCrosshair()
    {
        StartCoroutine(ChangeCrosshairCoroutine());
    }
    private IEnumerator ChangeCrosshairCoroutine()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(hitCrossHair.DOFade(1, 0f));
        sequence.Append(hitCrossHair.DOFade(0, 0.2f));
        yield return null;
    }
    public void SetCrosshair(Sprite sprite)
    {
        crossHair.sprite = sprite;
    }
    public void PopupText(string str)
    {
        if(onPopUpText)return;

        onPopUpText = true;
        
        popUpText.SetText(str);
        popUpText.gameObject.SetActive(true);
        popUpText.transform.DOScale(popUpTextOriginScale, 0.5f).SetEase(Ease.OutExpo)
            .OnComplete(() => {
                popUpText.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InExpo)
                    .OnComplete(() => {
                        popUpText.gameObject.SetActive(false);
                        
                        onPopUpText = false;
                    });
            });
        
    }
    public void OnOffSettingPanel()
    {
        if (SettingPanel.activeSelf)
        {
            OffSettingPanel();
        }
        else
        {
            OnSettingPanel();
        }
    }
    private void OnSettingPanel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Time.timeScale = 0;
        Vector3 originalScale = SettingPanel.transform.localScale;

        SettingPanel.transform.localScale = Vector3.zero;
        
        SettingPanel.gameObject.SetActive(true);
        SettingPanel.transform.DOScale(originalScale, 0.5f).SetEase(Ease.OutBounce).SetUpdate(true);
    }
    private void OffSettingPanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Time.timeScale = 1;
        SettingPanel.gameObject.SetActive(false);
    }
    public void GoToGameBtn()
    {
       OffSettingPanel();
    }
    public void GoToTitleBtn(string main)
    {
        SceneManager.LoadScene(main);
    }
    public void GoToExitBtn()
    {
        Application.Quit();
    }
    public void SetBottleUI(string str,Sprite sprite)
    {
        bottleText.SetText(str);
        bottleImage.sprite = sprite;
    }
    public void Reload(float duration)
    {
        reloadImage.fillAmount = 1;
        reloadImage.DOFillAmount(0, duration);
    }
    
   
    public void UseDash()
    {
        //StopCoroutine(DashCoroutine());
        StartCoroutine(DashCoroutine());
    }
    
    private IEnumerator DashCoroutine()
    {
        dashSlider.value = 0;
        float elapsedTime = 0;

        while (elapsedTime < 1)
        {
            dashSlider.value = Mathf.Lerp(0, 1, elapsedTime / 1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        dashSlider.value = 1;
    }
}

