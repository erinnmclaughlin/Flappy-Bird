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

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsRunning { get; private set; } = false;

        public BirdModel Bird { get; private set; }
        public ObservableCollection<PipeModel> Pipes { get; private set; }

        public GameManager()
        {
            Bird = new BirdModel();
            Pipes = new ObservableCollection<PipeModel>();
            Pipes.CollectionChanged += (o, e) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pipes)));
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
            if (Bird.DistanceFromGround <= 0)
                GameOver();

            var centeredPipe = GetCenteredPipe();
            if (centeredPipe != null)
            {
                var birdHitLowerPipe = Bird.DistanceFromGround <= centeredPipe.BottomDistanceFromGround + 1;
                var birdHitUpperPipe = Bird.DistanceFromGround >= centeredPipe.TopDistanceFromGround - 45;

                if (birdHitLowerPipe || birdHitUpperPipe)
                    GameOver();                
            }
                
        }

        private void ManagePipes()
        {
            if (!Pipes.Any() || Pipes.Last().DistanceFromLeft < 250)
                GeneratePipe();

            if (Pipes.First().DistanceFromLeft < -60)
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
       
        private void GameOver()
        {
            IsRunning = false;
        }

        private void GeneratePipe()
        {
            Pipes.Add(new PipeModel());
        }

        private PipeModel GetCenteredPipe()
        {
            return Pipes.FirstOrDefault(p => p.IsCentered());
        }

    }
}
