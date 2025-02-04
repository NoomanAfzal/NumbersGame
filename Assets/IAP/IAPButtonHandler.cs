using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IAPButtonHandler : MonoBehaviour
{
    public IAPProducts iAPProduct;
    public Text priceText;
    public UnityEvent onPurchaseComplete;

    private Button btn;

    private void Start()
    {
        if (priceText)
            priceText.text = MyIAPHandler.Instance.GetProductPrice(iAPProduct);

        btn = GetComponent<Button>();
        if (btn)
        {
            btn.onClick.AddListener(OnClickBuy);
        }
    }

     public void OnClickBuy()
    {
        MyIAPHandler.Instance.BuyIAP(iAPProduct, PurchaseComplete); // Only pass PurchaseComplete
    }

    public void PurchaseComplete()
    {
        onPurchaseComplete.Invoke();
    }
}
