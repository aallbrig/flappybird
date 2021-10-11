namespace Core
{
    public interface IRewardable
    {
        void Reward();
    }

    public class Rewarder
    {

        public static void Reward(IRewardable rewardable) => rewardable.Reward();
    }
}