using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    
    [SerializeField] private KitchenObjectSo kitchenObjectSo;
    private IKitchenObjectParent kitchenObjectParent;

    public static KitchenObject SpawnKitchenObject(KitchenObjectSo kitchenObjectSo, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
    
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

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
}
