using System;

namespace FlappyBird.Web.Models
{
    public class PipeModel
    {
        private readonly int _speed = 2;

        public int DistanceFromLeft { get; private set; } = 500;
        public int DistanceFromBottom { get; private set; } = new Random().Next(1, 60);
        public int GapLower => 300 - 150 + DistanceFromBottom; // pipe height - ground height + pipe distance from bottom
        public int GapUpper => 430 - 150 + DistanceFromBottom; // pipe gap - ground height + pipe distance from bottom

        public void Move()
        {
            DistanceFromLeft -= _speed;
        }

        public bool IsCentered()
        {
            // half of the game width minus half the width of the bird
            var gameCenterLeft = (500 - 60) / 2;

            // half of the game width plus half the width of the bird
            var gameCenterRight = (500 + 60) / 2;            

            return (DistanceFromLeft < gameCenterRight && DistanceFromLeft > gameCenterLeft - 60); 
        }
    }
}