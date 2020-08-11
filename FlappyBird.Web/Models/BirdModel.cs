namespace FlappyBird.Web.Models
{
    public class BirdModel
    {
        public int DistanceFromBottom { get; private set; } = 100;

        public void Fall(int gravity)
        {
            DistanceFromBottom -= gravity;
        }
    }
}