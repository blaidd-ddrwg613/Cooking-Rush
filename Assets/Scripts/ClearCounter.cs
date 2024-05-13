using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    
    [SerializeField] private KitchenObjectSo kitchenObjectSo;
   
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else {
                // Player not holding anything
            }
        } else {
            if (player.HasKitchenObject()) {
                
            } 
            else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            
        }
        
    }
 
}
