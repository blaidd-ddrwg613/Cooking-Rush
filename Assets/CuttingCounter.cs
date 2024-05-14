using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounter : BaseCounter {
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // No Kitchen Object on Counter
            if (player.HasKitchenObject()) {
                // Player has a kitchenObject
                if (HasRecipeForInput(player.GetKitchenObject().GetKitchenObjectSo())) {
                    // Object can be cut 
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
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
        if (HasKitchenObject() && HasRecipeForInput(GetKitchenObject().GetKitchenObjectSo())) {
            // There is a kitchenObject
            KitchenObjectSo outputKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSo());
            
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSo, this);
        }
    }

    private KitchenObjectSo GetOutputForInput(KitchenObjectSo inputKitchenObjectSo) {
        foreach (CuttingRecipeSO cuttingRecipeSo in cuttingRecipeSoArray) {
            if (cuttingRecipeSo.input == inputKitchenObjectSo) {
                return cuttingRecipeSo.output;
            }
        }

        return null;
    }

    private bool HasRecipeForInput(KitchenObjectSo input) {
        foreach (CuttingRecipeSO cuttingRecipeSo in cuttingRecipeSoArray) {
            if (cuttingRecipeSo.input == input) {
                return true;
            }
        }

        return false;
    }
}
