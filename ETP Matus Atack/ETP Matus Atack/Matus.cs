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
    
    public class Matus : Personaje 
    {
        bool view;

        Vector3 posInic;
        Vector3 posFinal;
        float radio;

        int tipoMatus;
        float dano;

        public Status status;
        TimeSpan statusTime;

        public Matus(Game game, Vector3 ps, float radius, int tM, float dano, Vector3 psFin)
            : base(game, ps, 0.5f, 0.0f, Personaje.MaxLife , true)
        {
            view = false;
            posInic = ps;

            radio = radius;
            tipoMatus = tM;
            this.dano = dano;

            posFinal = psFin;
        }

        public void setOnFire()
        {
            status = Status.OnFire;
            statusTime = new TimeSpan(0, 0, 2);
            hurt(600f);
        }

        public void setFrozen()
        {
            status = Status.Frozen;
            statusTime = new TimeSpan(0, 0, 3);
            hurt(400f);
        }

        public void Update(GameTime gameTime, Vector3 posPlayer, List<Disparo> Proyectiles)
        {

            if (status != Status.Normal)
            {
                if (statusTime.TotalMilliseconds <= 0.1)
                {
                    status = Status.Normal;
                }
                else
                    statusTime -= gameTime.ElapsedGameTime;
            }

            bool onHot = Vector3.DistanceSquared(posicion, posPlayer) <= radio * radio;

            if (!view && onHot)
            {
                view = true;
            } else if(view && !onHot) view = false;
            
            if (Vector3.DistanceSquared(posicion, posFinal) <= 1000)
            {
                Vector3 Aux = posInic;
                posInic = posFinal;
                posFinal = Aux;
            }

            Mover(posPlayer, (float) gameTime.ElapsedGameTime.TotalSeconds);

            if (vida <= 0)
                isAlive = false;

        }

        private void Mover(Vector3 posPlayer, float time)
        {
            Vector3 destino;
            if (!view) destino = posFinal;
            else destino = posPlayer;

            angulo = (float)Math.Atan2((double)(posicion.X - destino.X), (double)(posicion.Z - destino.Z));

            Matrix orient = Matrix.CreateRotationY(angulo);

            float mult = 1;
            if (status == Status.Frozen) mult = 0.6f;
            else if (status == Status.OnFire) mult = 1.3f;

            posicion += mult * 250 * Vector3.Transform(-Vector3.UnitZ, orient) * time;
            
        }

        public enum Status
        {
            Normal,
            Frozen,
            OnFire
        }
    }
}