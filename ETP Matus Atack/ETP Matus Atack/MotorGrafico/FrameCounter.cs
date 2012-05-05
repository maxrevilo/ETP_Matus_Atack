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
    public class FrameCounter : DrawableGameComponent
    {

        private double frames;
        private double updates;
        private TimeSpan timeSpam;


        private float framesTotales;
        private float updateTotales;

        
        private TimeSpan tiempoDeMuestra;
        /// <summary>
        /// Numero de segundos contando frames, si el valor es 1 se estan
        /// contando frames por segundo si es 60 se cuentan frames por minutos.
        /// </summary>
        public float SegundosDeMuestra
        {
            get { return (float)tiempoDeMuestra.TotalSeconds; }
            set
            {
                tiempoDeMuestra = new TimeSpan(0, 0, 0, 0, (int)(value*1000f));
            }
        }
        
        
        public float cuadrosTotales
        {
            get { return framesTotales; }
        }

        public float ActualizacionesTotales
        {
            get { return updateTotales; }
        }



        public FrameCounter(Game game)
            : base(game)
        {
            frames = 0;
            updates = 0;
            SegundosDeMuestra = 1;
            timeSpam = new TimeSpan(0, 0, 0);
        }
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            timeSpam -= gameTime.ElapsedGameTime;

            updates++;

            CheckFin();

            base.Update(gameTime);
        }
        

        public override void Draw(GameTime gameTime)
        {

            frames++;

            CheckFin();

            base.Draw(gameTime);
        }

        private void CheckFin()
        {
            if (timeSpam.TotalMilliseconds <= 0.0)
            {
                framesTotales = (float)frames;
                updateTotales = (float)updates;
                frames = 0;
                updates = 0;
                timeSpam += tiempoDeMuestra;
            }
        }
    }
}