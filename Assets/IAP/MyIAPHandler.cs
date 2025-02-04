using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

[System.Serializable]
public enum IAPProducts
{
    removeads = 0,
    hint=1,
    undo=2,
    remove=3
}

[System.Serializable]
public class ProductID
{
    public string ProductId;
    public ProductType ProductType;
    public IAPProducts ProductEnum;
    public string Price { get; set; }
}

public class MyIAPHandler : MonoBehaviour, IStoreListener
{
    
    public Action PurchaseComplete;
    public Action<PurchaseFailureReason> PurchaseFailed { get; private set; }

    static MyIAPHandler instance;

    IStoreController m_StoreController; // The Unity Purchasing system.
    public List<ProductID> ProductIds = new List<ProductID>();

    private bool isIAPInitialized = false;

    public static MyIAPHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MyIAPHandler>();
                if (instance && instance.gameObject)
                {
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            // If I am the first instance, make me the Singleton
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If a Singleton already exists and you find
            // another reference in the scene, destroy it!
            if (this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Start()
    {
        InitializePurchasing();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add products for purchasing
        foreach (var product in ProductIds)
        {
            builder.AddProduct(product.ProductId, product.ProductType);
        }

        UnityPurchasing.Initialize(this, builder);
    }

   public void BuyIAP(IAPProducts iapProduct, Action purchaseCompleteAction, Action<PurchaseFailureReason> purchaseFailedAction = null)
    {
        if (!isIAPInitialized)
        {
            Debug.LogError("IAP Not Initialized");
            return;
        }

        ClearAllDelegates();
        PurchaseComplete = purchaseCompleteAction;
        PurchaseFailed = purchaseFailedAction;

        m_StoreController.InitiatePurchase(ProductIds[(int)iapProduct].ProductId);
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;

        // Retrieve prices for products
        foreach (var product in ProductIds)
        {
            product.Price = m_StoreController.products.WithID(product.ProductId).metadata.localizedPriceString.ToString();
        }

        isIAPInitialized = true;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"In-App Purchasing initialize failed: {error}");
        isIAPInitialized = false;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string? message)
    {
        Debug.Log($"In-App Purchasing initialize failed: {error}");
        isIAPInitialized = false;
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // Retrieve the purchased product
        var product = args.purchasedProduct;
        if (PurchaseComplete != null)
            PurchaseComplete.Invoke();

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        // We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    public void ClearAllDelegates()
    {
        PurchaseComplete = null;
    }

    public string GetProductPrice(IAPProducts iapProduct)
    {
        return ProductIds[(int)iapProduct].Price;
    }
}
