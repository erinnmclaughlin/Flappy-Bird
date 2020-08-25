using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlappyBird.Web.Models
{
    public class GameManager
    {
        private readonly int _gravity = 2;

        public event EventHandler MainLoopCompleted;

        public bool IsRunning { get; private set; } = false;

        public BirdModel Bird { get; private set; }
        public List<PipeModel> Pipes { get; private set; }

        public GameManager()
        {
            ResetGameObjects();
        }

        public async void MainLoop()
        {
            IsRunning = true;

            while(IsRunning)
            {
                MoveGameObjects();
                CheckForCollisions();            
                ManagePipes();

                MainLoopCompleted?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);
            }
        }

        public void Jump()
        {
            if (IsRunning) 
                Bird.Jump();
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                ResetGameObjects();
                MainLoop();
            }           
        }

        private void CheckForCollisions()
        {
            if (Bird.IsOnGround())
                GameOver();

            var centeredPipe = GetCenteredPipe();
            if (centeredPipe != null &&
                (Bird.DistanceFromGround < centeredPipe.GapLower ||
                Bird.DistanceFromGround > centeredPipe.GapUpper - 45)) // <-- minus bird height
            {
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
                pipe.Move();
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

        private void ResetGameObjects()
        {
            Bird = new BirdModel();
            Pipes = new List<PipeModel>();
        }
    }
}
