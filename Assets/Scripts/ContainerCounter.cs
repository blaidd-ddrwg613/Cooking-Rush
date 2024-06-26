using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    
    [SerializeField] private KitchenObjectSo kitchenObjectSo;

    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player) {
        // Spawn the object and give it to the player
        if (!player.HasKitchenObject()) {
            KitchenObject.SpawnKitchenObject(kitchenObjectSo, player);
            
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
