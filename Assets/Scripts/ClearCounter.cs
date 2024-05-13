using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    
    [SerializeField] private KitchenObjectSo kitchenObjectSo;
   
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
 
}
