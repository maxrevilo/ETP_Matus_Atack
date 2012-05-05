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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        
        Song cancion;
        Mundo Ciudad;

        GraphicManager Graphic_Manager;
        KeyboardState oldkbs;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            oldkbs = Keyboard.GetState();
        }


        protected override void Initialize()
        {
            Ciudad = new Mundo(this);
            Ciudad.Initialize();

            Graphic_Manager = new GraphicManager(this, Ciudad);
            Graphic_Manager.Initialize();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            cancion = Content.Load<Song>("17-I'll Face Myself -Battle-");
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(cancion);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            bool port = EstadoDeJuego.onPortada();
            bool inst1 = EstadoDeJuego.onInstrucciones1();
            bool inst2 = EstadoDeJuego.onInstrucciones2();

            if (port || inst1 || inst2)
            {
                if (Keyboard.GetState().GetPressedKeys().Length > 0 && oldkbs.GetPressedKeys().Length == 0)
                {
                    if (port)
                        EstadoDeJuego.setInstrucciones1();
                    else if (inst1)
                        EstadoDeJuego.setInstrucciones2();
                    else
                        EstadoDeJuego.setJuego();
                }
            }
            else if (EstadoDeJuego.onJuego())
            {
                if (Graphic_Manager.readyToFollow)
                    Ciudad.Update(gameTime);
            }
            else
            {//Win or Loose
                if (Keyboard.GetState().GetPressedKeys().Length > 0)
                {
                    EstadoDeJuego.setPortada();
                }
            }

            Graphic_Manager.Update(gameTime);

            oldkbs = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Graphic_Manager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
