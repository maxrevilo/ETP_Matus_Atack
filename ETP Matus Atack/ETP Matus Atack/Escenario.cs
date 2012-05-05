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

    public class Escenario
    {
        //Variables Generales del Escenario
        Game Juego;
        public Motor_Colisiones Colisionable;

        //Variables De Cada Escenario
        float TiempoTranscurrido;
        float TiempoMax;

        //Variable para Enemigos
        Vector3[] Posiciones_M;
        float[] Scales_M;
        public List<Matus> Enemigos;
        public int Cantidad_Enemigos;
        int[] Modelos_Enemigos;

        // Variables para Edificios
        Vector3[] Posiciones_E;
        float[] Scales_E;
        public List<Edificios> Edif;
        public int Cantidad_Edificios;
        int[] Modelos_Edificios;
        Player jugador;

        public Escenario(Game game,Motor_Colisiones Colision, Player jugador)
        {
            Juego = game;
            Edif = new List<Edificios>();
            Enemigos = new List<Matus>();
            Colisionable = Colision;
            this.jugador = jugador;
        }

        public void Initialize(Vector3[] Pos_Enem, Vector3[] Pos_Edif, float[] Scal_Enem, float[] Scal_Edif, int[] Model_Enemig, int[] Model_Edif)
        {
            Posiciones_M = Pos_Enem;
            Posiciones_E = Pos_Edif;
            Scales_M = Scal_Enem;
            Scales_E = Scal_Edif;
            Modelos_Enemigos = Model_Enemig;
            Modelos_Edificios = Model_Edif;
            
            Cantidad_Enemigos = Posiciones_M.Length;
            Cantidad_Edificios = Posiciones_E.Length;

            Vector2[] aux = new Vector2[Cantidad_Enemigos];
            for (int i = 0; i < Cantidad_Enemigos; i++)
            {
                aux[i] = new Vector2(Pos_Enem[i].X, Pos_Enem[i].Z);
            }

            Colisionable.Agregar_Circulos_M(aux);


        }

        public void Inicializar_Edificios()
        {
            for (int i = 0; i < Cantidad_Edificios; i++)
            {
                Edif.Add(new Edificios(Posiciones_E[i], Scales_E[i],Modelos_Edificios[i]));
            }
        }

        public void Inicializar_Enemigos()
        {
            for (int i = 0; i < Cantidad_Enemigos; i++)
            {
                Enemigos.Add(new Matus(Juego, Posiciones_M[i], 5000f, Modelos_Enemigos[i], 4000f, new Vector3(7000f, 0, -500f)));
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < Cantidad_Enemigos; i++)
            {
                if(Enemigos[i].isAlive)
                    Enemigos[i].Update(gameTime, jugador.posicion, jugador.Proyectiles);

                Colisionable.Actualizar_Circulos_M(i, new Vector2(Enemigos[i].posicion.X, Enemigos[i].posicion.Z));
            }

            char Dir;
            foreach (Matus M in Enemigos)
            {
                foreach (Disparo P in jugador.Proyectiles)
                    Colisionable.Colision_Enemigo_PY(M, P);

                Dir = ' ';
                if (Colisionable.Colision_Ma_Ed(M))
                    Dir = Colisionable.Donde_Choca();

                switch (Dir)
                {
                    case 'u':
                        M.posicion.Z -= 30;
                        break;
                    case 'd':
                        M.posicion.Z += 30;
                        break;
                    case 'l':
                        M.posicion.X -= 30;
                        break;
                    case 'r':
                        M.posicion.X += 30;
                        break;
                }                

            }
            No_Unir();
        }

        public void No_Unir()
        {
            char Dir;
            foreach (Matus M1 in Enemigos)
            {
                foreach (Matus M2 in Enemigos)
                {
                    Dir = ' ';
                    if (!M1.Equals(M2) && Colisionable.Colision_Pr_Ma(M1, M2))
                        Dir = Colisionable.Dir_Enem(M1, M2);

                    switch (Dir)
                    {
                        case 'u':
                            M2.posicion.Z -= 30;
                            break;
                        case 'd':
                            M2.posicion.Z += 30;
                            break;
                        case 'l':
                            M2.posicion.X -= 30;
                            break;
                        case 'r':
                            M2.posicion.X += 30;
                            break;
                    }
                }
                Dir = ' ';
                if (Colisionable.Colision_Pr_Ma(jugador, M1))
                    Dir = Colisionable.Dir_Enem(jugador, M1);

                switch (Dir)
                {
                    case 'u':
                        M1.posicion.Z -= 30;
                        break;
                    case 'd':
                        M1.posicion.Z += 30;
                        break;
                    case 'l':
                        M1.posicion.X -= 30;
                        break;
                    case 'r':
                        M1.posicion.X += 30;
                        break;
                }
            }
        }

    }
}