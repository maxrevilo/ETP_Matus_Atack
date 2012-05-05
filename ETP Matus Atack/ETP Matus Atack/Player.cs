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
    
    public class Player : Personaje 
    {
        Motor_Colisiones Colisiones;
        FireCube LastFire;
        IceCube LastIce;
        public List<Disparo> Proyectiles;
        SoundEffect sE;
        MouseState oldMS;

        Vector3 posAnt;

        public float magic = MaxMagic;
        public const float MaxMagic = 1000;
        public const float pasiveMagic = 50;
        const float iceCost = 75;
        const float fireCost = 150;

        public Player(Game game,Motor_Colisiones Motor)
            : base(game, new Vector3(9000,0,0), 1.0f, 0, Personaje.MaxLife , true)
        {

            Colisiones = Motor;
            Colisiones.Update_P_R(3.0f);
            LastFire = new FireCube();
            LastIce = new IceCube();
            posAnt = posicion;
            Proyectiles = new List<Disparo>();
            oldMS = Mouse.GetState();
            sE = game.Content.Load<SoundEffect>("SonidoFX\\Fireball");
        }

        public override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Mover(time);
            rotar(time);
            Evita_Salir();
            Disparar(time);

            
            Colisiones.Update_P(new Vector2(posicion.X,posicion.Z));

            posAnt = posicion;

            magic = Math.Min(MaxMagic, magic + pasiveMagic * time);

            oldMS = Mouse.GetState();

            base.Update(gameTime);
        }

        private void rotar(float time) {
            float HalfWidht = Game.GraphicsDevice.Viewport.Width / 2;
            float HalfHeight = Game.GraphicsDevice.Viewport.Height / 2;

            const float vAng = 0.0005f;

            angulo += (HalfWidht - Mouse.GetState().X) * vAng;
            
            Mouse.SetPosition((int)HalfWidht, (int)HalfHeight);
        }

        private void Mover(float Time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                if(aceleracion <1.6f)
                    aceleracion += 1.2f;
            if (Keyboard.GetState().IsKeyUp(Keys.LeftControl))
                aceleracion = 1;


            float proyX = Helper.Cos(angulo);
            float proyY = Helper.Sin(angulo);

            Vector3 direc = Vector3.Zero;

            const float speed = 1000;

            if (Keyboard.GetState().IsKeyDown(Keys.D) && !Colisiones.Colision_Pj_Ed())
                direc.X = speed * Time;
            if (Keyboard.GetState().IsKeyDown(Keys.A) && !Colisiones.Colision_Pj_Ed())
                direc.X = -speed * Time;
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !Colisiones.Colision_Pj_Ed())
                direc.Z = -speed * Time;
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !Colisiones.Colision_Pj_Ed())
                direc.Z = speed * Time;

            Matrix orient = Matrix.CreateRotationY(angulo);

            direc = Vector3.Transform(direc, orient);

            posicion += direc;

        }

        private void Evita_Salir()
        {
            char Dir = ' ';
            if (Colisiones.Colision_Pj_Ed())
                Dir = Colisiones.Donde_Choca();

            switch (Dir)
            {
                case 'u':
                    posicion.Z -= 30;
                    break;
                case 'd':
                    posicion.Z += 30;
                    break;
                case 'l':
                    posicion.X -= 30;
                    break;
                case 'r':
                    posicion.X += 30;
                    break;
            }
        }

        private void Disparar(float Time)
        {
            if (Mouse.GetState().RightButton == ButtonState.Pressed && oldMS.RightButton != ButtonState.Pressed && magic >= iceCost)
            {
                LastIce.setVal(posicion + new Vector3(0, 100, 0), angulo, 0.4f, 2000f, 1000f);
                shoot(LastIce);
                magic -= iceCost;
                sE.Play();
            }

            // Para Multiples Instancias de La Bola de Fuego Necesito muchas Declaraciones por cada disparo
            // Asi no se hacerlo
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && oldMS.LeftButton != ButtonState.Pressed  && magic >= fireCost)
            {
                LastFire.setVal(posicion + new Vector3(0, 100, 0), angulo, 0.4f, 3000f, 1000f);
                shoot(LastFire);
                magic -= fireCost;
                sE.Play();
            }

            LastIce.update(Time);
            LastFire.update(Time);

            foreach (Disparo tiro in Proyectiles)
            {
                if (tiro != null && !tiro.Ready())
                {
                    tiro.update(Time);
                }
            }
        }

        private void shoot(Disparo nuevoTiro)
        {
            foreach (Disparo tiro in Proyectiles)
            {
                if (tiro != null && tiro.Ready())
                {
                    tiro.setVal(nuevoTiro);
                    return;
                }
            }

            Disparo nuevo;

            if (nuevoTiro is IceCube)
                nuevo = new IceCube();
            else
                nuevo = new FireCube();

            nuevo.setVal(nuevoTiro);
            Proyectiles.Add(nuevo);
        }
    }
}