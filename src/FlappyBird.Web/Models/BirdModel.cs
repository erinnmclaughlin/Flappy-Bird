using System.ComponentModel;

namespace FlappyBird.Web.Models
{
    public class BirdModel : INotifyPropertyChanged
    {
        public int _distanceFromGround = 100;

        public int DistanceFromGround
        {
            get { return _distanceFromGround; }
            set
            {
                _distanceFromGround = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DistanceFromGround)));
            }
        }
        public int JumpStrength { get; private set; } = 50;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Fall(int gravity)
        {
            DistanceFromGround -= gravity;
        }

        public void Jump()
        {
            if (DistanceFromGround < 530)
                DistanceFromGround += JumpStrength;
        }
    }
}