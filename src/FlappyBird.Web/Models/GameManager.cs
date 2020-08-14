using System.ComponentModel;
using System.Threading.Tasks;

namespace FlappyBird.Web.Models
{
    public class GameManager
    {
        private readonly int _gravity = 2;
        private readonly int _speed = 2;

        public bool IsRunning { get; private set; } = false;

        public BirdModel Bird { get; private set; }
        public PipeModel Pipe { get; private set; }

        public GameManager()
        {
            Bird = new BirdModel();
            Pipe = new PipeModel();
        }

        public async void MainLoop()
        {
            IsRunning = true;

            while(IsRunning)
            {
                Bird.Fall(_gravity);
                Pipe.Move(_speed);

                if (Bird.DistanceFromBottom <= 0)
                    GameOver();

                await Task.Delay(20);
            }
        }

        private void GameOver()
        {
            IsRunning = false;
        }
    }
}
