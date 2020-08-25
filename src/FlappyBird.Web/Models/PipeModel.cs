using System;

namespace FlappyBird.Web.Models
{
    public class PipeModel
    {
        private int _speed = 2;

        public int DistanceFromLeft { get; private set; } = 500;
        public int DistanceFromBottom { get; private set; } = new Random().Next(1, 60);
        
        // pipe height - ground height + pipe distance from bottom
        public int GapLower => 300 - 150 + DistanceFromBottom;

        // pipe gap - ground height + pipe distance from bottom
        public int GapUpper => 430 - 150 + DistanceFromBottom;


        public void Move()
        {
            DistanceFromLeft -= _speed;
        }

        public bool IsCentered()
        {
            // half of the game width minus half the width of the bird
            var centerLeft = (500 - 60) / 2;

            // half of the game width plus half the width of the bird
            var centerRight = (500 + 60) / 2;            

            return (DistanceFromLeft < centerRight && DistanceFromLeft > centerLeft - 60);
        }
    }
}