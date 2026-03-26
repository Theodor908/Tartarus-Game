using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        [Header("Debug delete later")]
        [SerializeField] InstantCharacterEffect testEffect;
        [SerializeField] bool processTestEffect = false;

        private void Update()
        {
            if(processTestEffect)
            {
                processTestEffect = false;
                // Use a copied effect to avoid modifying the original asset
                InstantCharacterEffect effect = Instantiate(testEffect);
                ProcessInstantEffect(effect);
            }
        }

    }

}
