using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class AIState : ScriptableObject
    {

        public virtual AIState Tick(AICharacterManager aiCharacter)
        {

            //Find player

            //If found player, return new AIStateChase

            //If not found player, return new AIStatePatrol

            return this;
        }

        protected virtual AIState SwitchState(AICharacterManager aiCharacter, AIState newState)
        {
            ResetStateFlags(aiCharacter);
            return newState;
        }

        protected virtual void ResetStateFlags(AICharacterManager aiCharacter)
        {
            //Reset all flags
        }

    }
}