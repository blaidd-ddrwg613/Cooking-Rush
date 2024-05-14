using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private KitchenObjectSo slicedKitchenObjectReference;
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // No Kitchen Object on Counter
            if (player.HasKitchenObject()) {
                // Player has a kitchenObject
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else {
                // Player not holding anything
            }
        } else {
            // Counter Had a kitchenObject
            if (player.HasKitchenObject()) {
                // Player has a kitchenObject
            } 
            else {
                // Player not holding kitchenObject
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            
        }
    }

    public override void Use(Player player) {
        if (HasKitchenObject()) {
            // There is a kitchenObject
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(slicedKitchenObjectReference, this);
        }
    }
}
