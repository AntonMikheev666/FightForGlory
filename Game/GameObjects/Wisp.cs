﻿using System.Drawing;
using Game.BaseStructures.Enums;
using Game.Fighters;

namespace Game.GameObjects
{
    public class Wisp : GameObject
    {
        public override int Damage => 20;

        public Wisp(RectangleF body, bool lookRight, PlayerNumber source)
        {
            Source = source;
            var y = body.Bottom - (body.Bottom - body.Top) / 1.5f;
            float x;
            if (lookRight)
            {
                Speed = 50;
                x = body.Right;
            }
            else
            {
                Speed = -50;
                x = body.Left;
            }
            Size = new RectangleF(x, y, 60, 60);
        }

        public override bool CheckState(Fighter opponent)
        {
            if (IsOutsideScreen()) return true;
            if (Speed > 0)
            {
                if (opponent.IsBlocking && !opponent.LookingRight) return false;
                if (!HasReached(opponent)) return false;
                opponent.HealthPoints -= Damage;
                return true;
            }
            if (opponent.IsBlocking && opponent.LookingRight) return false;
            if (!HasReached(opponent)) return false;
            opponent.HealthPoints -= Damage;
            return true;
        }
    }
}
