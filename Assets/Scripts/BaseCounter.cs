using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    
    [SerializeField] private Transform counterPointTop;

    private KitchenObject kitchenObject;
    
    public virtual void Interact(Player player){}
    
    public virtual void Use(Player player) {}

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
