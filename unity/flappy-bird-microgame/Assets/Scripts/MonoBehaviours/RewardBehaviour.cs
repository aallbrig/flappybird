using Core;
using UnityEngine;

namespace MonoBehaviours
{
    public class RewardBehaviour : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            var bird = other.gameObject.GetComponent<IContainBird>();
            if (bird != null)
                Reward(bird.Bird);
        }
        public void Reward(IRewardable ableToReceiveRewards) => Rewarder.Reward(ableToReceiveRewards);
    }
}