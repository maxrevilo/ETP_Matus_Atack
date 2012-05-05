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
    public class Disparo
    {
        public Vector3 Posicion;
        public float direction;
        public float size;
        public float speed;
        protected float Tiempo_Recarga;
        float Tiempo_Transcurrido;
        public bool ready;
        public bool Tipo;

        public bool Ready()
        {
            return ready;
        }

        

        public Disparo(bool B)
        {
            ready = true;
            Tipo = B;
        }

        public void setVal(Vector3 Pos, float direccion, float size, float speed, float Tiempo)
        {
            Posicion = Pos;
            this.direction = direccion;
            this.size = size;
            this.speed = speed;
            Tiempo_Recarga = Tiempo;
            ready = false;
        }

        public void setVal(Disparo disparo)
        {
            Posicion = disparo.Posicion;
            this.direction = disparo.direction;
            this.size = disparo.size;
            this.speed = disparo.speed;
            Tiempo_Recarga = disparo.Tiempo_Recarga;
            ready = false;
            
        }

        public void update(float Time)
        {
            if (!ready)
            {
                Tiempo_Transcurrido += Time;
                Matrix orientation = Matrix.CreateRotationY(direction);
                Posicion += Vector3.Transform(new Vector3(0,0,-1)*speed, orientation) * Time;
            }

            if (Tiempo_Transcurrido > Tiempo_Recarga)
            {
                ready = true;
                Tiempo_Transcurrido = 0;
            }

        }
    }
}
