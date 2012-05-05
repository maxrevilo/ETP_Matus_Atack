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
    public class Mundo
    {
        public Game Juego;
        public List<Escenario> Escenas;
        public SpriteFont fuente;
        public Player jugador;

        Motor_Colisiones Colisiones;

        public Mundo(Game game)
        {
            Juego = game;
            Colisiones = new Motor_Colisiones();
            jugador = new Player(game,Colisiones);
            Escenas = new List<Escenario>();
            
        }

        public void Initialize()
        {
            // Agrega Los Escenarios;
            Escenas.Add(new Escenario1(Juego,Colisiones,jugador));
            
        }

  
        public void Update(GameTime gameTime)
        {
            float time = (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (EstadoDeJuego.onJuego())
            {
                foreach (Escenario i in Escenas)
                {
                    i.Update(gameTime);
                }

                if (jugador.isAlive)
                    jugador.Update(gameTime);

                if (Colisiones.Colision_Pj_En())
                   jugador.hurt(50*time);

                if (jugador.vida <= 0)
                {
                    jugador.isAlive = false;
                    EstadoDeJuego.setLoose();
                }
            }
        }




    }
}