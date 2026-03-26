using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tartarus
{
    public class AICharacterLocomotionManager : CharacterLocomotionManager
    {
        public void RotateTowardsAgent(AICharacterManager aiCharacter)
        {
            if(aiCharacter.isMoving)
                aiCharacter.transform.rotation = aiCharacter.navMeshAgent.transform.rotation;

        }
    }

}