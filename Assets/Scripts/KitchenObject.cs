using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    
    [SerializeField] private KitchenObjectSo kitchenObjectSo;

    private IKitchenObjectParent kitchenObjectParent;
    
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        
        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("IKitchenObjectParent already had a KitchenObject");
        }
        kitchenObjectParent.SetKitchenObject(this);
        
        transform.parent = kitchenObjectParent.GetKitchenObjectDisplayPoint();
        transform.localPosition = Vector3.zero;
    }
    
    public KitchenObjectSo GetKitchenObjectSo() {
        return kitchenObjectSo;
    }
    
    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }
}
