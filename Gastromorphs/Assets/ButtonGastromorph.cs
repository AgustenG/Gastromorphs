using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGastromorph : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI id;

    private void Awake()
    {
        Button btn = button.GetComponent<Button>();
            
        btn.onClick.AddListener(() => {
            Gastromorph gastromorph = GastromorphsManager.Instance.GetGastromorphFromId(id.text);
            SingleGastroPage.Instance.SetGastromorphAttributes(gastromorph);
            CanvasManager.instance.startGastromorph();
            SingleGastroPage.Instance.OpenGastromorph(gastromorph);
            ModelRotation.Instance.ActivateModel(gastromorph.Name);

        });


       
    }
}
