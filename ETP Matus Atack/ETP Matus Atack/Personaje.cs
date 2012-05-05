using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace ETP_Matus_Atack
{
    public class Personaje : Microsoft.Xna.Framework.GameComponent
    {
        public Vector3 posicion;
        public float aceleracion;
        public float angulo;
        public float vida;
        public const float MaxLife = 1000;
        public bool isAlive;

        public Personaje(Game game, Vector3 p, float ac, float ang, float sizeLife, bool isA)
            : base(game)
        {
            posicion = p;
            aceleracion = ac;
            angulo = ang;
            vida = sizeLife;
            isAlive = isA;
        }

        public void hurt(float amount)
        {
            vida = MathHelper.Clamp(vida - amount, 0, MaxLife);
        }

        
    }
}