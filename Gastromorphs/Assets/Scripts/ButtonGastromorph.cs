using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGastromorph : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI id;
    public bool isFromSearch;

    private void Awake()
    {
        Button btn = button.GetComponent<Button>();
            
        btn.onClick.AddListener(() => {
            Gastromorph gastromorph = GastromorphsManager.Instance.GetGastromorphFromId(id.text);
            SingleGastroPage.Instance.SetGastromorphAttributes(gastromorph);
            CanvasManager.instance.startGastromorph(isFromSearch);
            SingleGastroPage.Instance.OpenGastromorph(gastromorph);
            ModelRotation.Instance.ActivateModel(gastromorph.Name);
            AudioManager.Instance.clickGeneral();

        });


       
    }
}
