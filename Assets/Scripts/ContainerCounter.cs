using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent {
    
    [SerializeField] private KitchenObjectSo kitchenObjectSo;
    [SerializeField] private Transform counterPointTop;
    
    private KitchenObject kitchenObject;
    
    public override void Interact(Player player) {
        if (kitchenObject == null) {
            // Spawn the object on top of the counter
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab, counterPointTop);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        } else {
            // Give the object to the player
            kitchenObject.SetKitchenObjectParent(player);
        }
    }
    
    public Transform GetKitchenObjectDisplayPoint() {
        return counterPointTop;
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
