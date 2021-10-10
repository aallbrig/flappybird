namespace Core
{
    public interface IKillable
    {
        public void Kill();
    }

    public class Obstacle
    {
        public void Kill(IKillable victim) => victim.Kill();
    }
}