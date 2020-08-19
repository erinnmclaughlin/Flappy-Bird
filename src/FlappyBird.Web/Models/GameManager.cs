using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBird.Web.Models
{
    public class GameManager : INotifyPropertyChanged
    {
        private readonly int _gravity = 2;
        private readonly int _speed = 2;

        private readonly int _width;
        public int Width { get { return _width; } }
        private readonly int _height;
        public int Height { get { return _height; } }
        public int SkyHeight { get { return Height / 73 * 58; } }
        public int GroundHeight { get { return Height - SkyHeight;  } }
        private readonly int _border;
        public int Border { get { return _border; } }
         
        public bool IsRunning { get; private set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public BirdModel Bird { get; private set; }
        public ObservableCollection<PipeModel> Pipes { get; private set; }

        public GameManager(int gameWidth = 500)
        {
            _width = gameWidth;
            _height = (int)(gameWidth * 1.46);
            _border = (int)(gameWidth / 6.25);
            ResetGame();
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                ResetGame();
                MainLoop();
            }
        }

        public async void MainLoop()
        {
            IsRunning = true;

            while(IsRunning)
            {
                MoveGameObjects();
                CheckForCollisions();            
                ManagePipes();

                await Task.Delay(20);
            }
        }

        private void CheckForCollisions()
        {
            if (Bird.IsOnGround())
                GameOver("Bird collided with the ground.");

            var centeredPipe = GetCenteredPipe();
            if (centeredPipe != null)
            {
                if (Bird.DistanceFromGround < centeredPipe.DistanceFromGround)
                    GameOver($"Bird collided with lower pipe.\nBird Bottom: {Bird.DistanceFromGround}\nPipe Top: {centeredPipe.DistanceFromGround}");

                else if (Bird.DistanceFromGround > centeredPipe.DistanceFromGround + centeredPipe.Gap - 45)
                    GameOver($"Bird collided with upper pipe.\nBird Top: {Bird.DistanceFromGround - 45}\nPipe Bottom: {centeredPipe.DistanceFromGround + centeredPipe.Gap}");
            }
                
        }

        private void ManagePipes()
        {
            if (!Pipes.Any() || Pipes.Last().IsCentered())
                GeneratePipe();

            if (Pipes.First().IsOffScreen())
                Pipes.Remove(Pipes.First());
        }

        private void MoveGameObjects()
        {
            Bird.Fall(_gravity);
            foreach (var pipe in Pipes)
            {
                pipe.Move(_speed);
            }
        }
       
        private void GameOver(string message = null)
        {
            IsRunning = false;

            if (message != null)
                Console.WriteLine(message);
        }

        private void GeneratePipe()
        {
            Pipes.Add(new PipeModel(Width, GroundHeight));
        }

        private void ResetGame()
        {
            Bird = new BirdModel(_width);
            Pipes = new ObservableCollection<PipeModel>();
            Pipes.CollectionChanged += (o, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pipes)));
        }

        private PipeModel GetCenteredPipe()
        {
            return Pipes.FirstOrDefault(p => p.IsCentered());
        }

    }
}
