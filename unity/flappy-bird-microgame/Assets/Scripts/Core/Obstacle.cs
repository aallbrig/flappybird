namespace Core
{
    public interface IKillable
    {
        public void Kill();
    }

    public class Obstacle
    {
        public static Obstacle Of() => new Obstacle();
        public void Kill(IKillable victim) => victim.Kill();
    }
}