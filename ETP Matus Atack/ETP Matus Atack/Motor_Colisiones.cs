using System;
using System.IO;
using System.Collections;
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
    public class Motor_Colisiones
    {
        Geom Inter;

        //Estatica
        public Rectangle[] CE;


        //Dinamica
        //Jugador
        Vector2 Posicion_J;
        float Radio_J;

        //Enemigos
        float Radio_M = 40f;
        public Circulo[] CM;

        //Balas
        List<Circulo> Circulos_B_FC;
        List<Circulo> Circulos_B_IC;
        float Radio_B_FC = 40f;
        float Radio_B_IC = 40f;


        public Motor_Colisiones()
        {
            Circulos_B_FC = new List<Circulo>();
            Circulos_B_IC = new List<Circulo>();
        }

       
        //Verificar Coliciones con Edificios
        int Edificion_En_Colicion=-1;
        public bool Colision_Pj_Ed()
        {
            Circulo Cj = new Circulo((int)Posicion_J.X, (int)Posicion_J.Y, (int)Radio_J);
            bool Intercep = false;
            for (int i = 0; i < CE.Length && !Intercep; i++)
            {
                if (Cj.Intercepta(CE[i]))
                {
                    Intercep = true;
                    Edificion_En_Colicion = i;
                }
            }
            return Intercep;
        }
        public int Get_Colision_Ed()
        {
            return Edificion_En_Colicion;
        }
        public bool Colision_Ma_Ed(Matus Enemigo)
        {
            Circulo Cm = new Circulo((int)Enemigo.posicion.X, (int)Enemigo.posicion.Z, (int)Radio_M);
            bool Intercep = false;
            for (int i = 0; i < CE.Length && !Intercep; i++)
            {
                if (Cm.Intercepta(CE[i]))
                {
                    Intercep = true;
                    Edificion_En_Colicion = i;
                }
            }
            return Intercep;            
        }

        //Enemigos entre si
        public bool Colision_Pr_Ma(Personaje PerS, Matus Ene2)
        {
            Circulo Cm1 = new Circulo((int)PerS.posicion.X, (int)PerS.posicion.Z, (int)Radio_M);
            Circulo Cm2 = new Circulo((int)Ene2.posicion.X, (int)Ene2.posicion.Z, (int)Radio_M);
            return Cm1.Intercepta(Cm2);     
        }

        // Verifica Coliciones Con Enemigos
        int Enemigo_En_Colision=-1;
        public bool Colision_Pj_En()
        {
            Circulo Cj = new Circulo((int)Posicion_J.X, (int)Posicion_J.Y, (int)Radio_J);
            bool Intercep = false;
            for (int i = 0; i < CM.Length && !Intercep; i++)
            {
                if (Cj.Intercepta(CM[i]))
                {
                    Intercep = true;
                    Enemigo_En_Colision = i;
                }
            }
            return Intercep;
        }
        public int Get_Colision_En()
        {
            return Enemigo_En_Colision;
        }

        //Verificar Coliciones de FireCube Con Enemigos
        public void Colision_Enemigo_FC(Matus Enemigo)
        {
            Circulo Aux = new Circulo((int)Enemigo.posicion.X, (int)Enemigo.posicion.Z,(int)Radio_M);
            for (int i = 0; i < Circulos_B_FC.Count() && Enemigo.isAlive; i++)
                if (Aux.Intercepta(Circulos_B_FC.ElementAt(i)))
                    Enemigo.setOnFire();
        }

        //Verificar Coliciones de IceCube Con Enemigo
        public void Colision_Enemigo_IC(Matus Enemigo)
        {
            Circulo Aux = new Circulo((int)Enemigo.posicion.X, (int)Enemigo.posicion.Z, (int)Radio_M);
            for (int i = 0; i < Circulos_B_IC.Count() && Enemigo.isAlive; i++)
                if (Aux.Intercepta(Circulos_B_IC.ElementAt(i)))
                    Enemigo.setFrozen();
        }

        //Verificar Colisiones de Proyectil con Enemigo
        public void Colision_Enemigo_PY(Matus Enemigo, Disparo Proyectil)
        {
            Circulo Aux = new Circulo((int)Enemigo.posicion.X, (int)Enemigo.posicion.Z, (int)Radio_M);
            Circulo Aux2 = new Circulo((int)Proyectil.Posicion.X, (int)Proyectil.Posicion.Z, (int)Radio_B_FC);
            if (Enemigo.isAlive && Aux.Intercepta(Aux2))
                if (Proyectil.Tipo)//IceCube
                {
                    Enemigo.setFrozen();
                    Proyectil.ready = true;
                }
                else
                {
                    Enemigo.setOnFire();
                    Proyectil.ready = true;
                }
        }

        //Cargar circulos de FireCube
        public void Agregar_Circulos_FireCube(Vector2 Pos)
        {
            Circulos_B_FC.Add(new Circulo((int)Pos.X, (int)Pos.Y, (int)Radio_B_FC));
        }
        public void Actualizar_Circulos_FC(int index, Vector2 Pos)
        {
            Circulos_B_FC.Insert(index, new Circulo((int)Pos.X, (int)Pos.Y, (int)Radio_B_FC));
        }
        public void Update_R_FC(float Radio)
        {
            Radio_B_FC = Radio;
        }

        //Cargar circulos de IceCube
        public void Agregar_Circulos_IceCube(Vector2 Pos)
        {
            Circulos_B_IC.Add(new Circulo((int)Pos.X, (int)Pos.Y, (int)Radio_B_IC));
        }
        public void Actualizar_Circulos_IC(int index, Vector2 Pos)
        {
            Circulos_B_IC.Insert(index, new Circulo((int)Pos.X, (int)Pos.Y, (int)Radio_B_IC));
        }
        public void Update_R_IC(float Radio)
        {
            Radio_B_FC = Radio;
        }
        
        // Cargar Posiciones del Jugador
        public void Update_P(Vector2 Nueva_Posicion)
        {
            Posicion_J = Nueva_Posicion;
        }
        public void Update_P_R(float Radio)
        {
            Radio_J = Radio;
        }

        // Cargar Posiciones y Circulos de Los Enemigos
        public void Agregar_Circulos_M(Vector2[] Pos)
        {
            CM = new Circulo[Pos.Length];
            for(int i=0; i<Pos.Length ;i++)
            {
                CM[i] = new Circulo((int)Pos[i].X, (int)Pos[i].Y,(int)Radio_M);
            }
        }
        public void Actualizar_Circulos_M(int index, Vector2 Pos)
        {
            CM[index] = new Circulo((int)Pos.X,(int)Pos.Y,(int)Radio_M);
        }
        public void Update_M(float radio)
        {
            Radio_M = radio;
        }


        // Bases De Rectangulos No Usadas
        public void FunAuxRec(string filename)
        {
            //string path = Path.Combine(StorageContainer.TitleLocation, filename);
            int i = 0;
            string line = "";
            String[] Vector;
            //using (StreamReader tr = new StreamReader(path))
            using (StreamReader tr = new StreamReader(TitleContainer.OpenStream(filename)))
            {
                line = tr.ReadLine();
                CE = new Rectangle[int.Parse(line)];
                line = tr.ReadLine();

                while (line != null)
                {
                    Vector = line.Split(' ');
                    CE[i] = new Rectangle(int.Parse(Vector[0]), int.Parse(Vector[1]), int.Parse(Vector[2]), int.Parse(Vector[3]));
                    i++;
                    line = tr.ReadLine();
                }
            }
        }

        // Por Doi¿nde Choca
        public char Donde_Choca()
        {
            Circulo Cj = new Circulo((int)Posicion_J.X, (int)Posicion_J.Y, (int)Radio_J);
            return Geom.indicarDireccion(CE[Edificion_En_Colicion], Cj.BoundingRect());
        }

        public char Dir_Enem(Personaje M1, Matus M2)
        {
            Circulo Cj1 = new Circulo((int)M1.posicion.X, (int)M1.posicion.Z, (int)Radio_M);
            Circulo Cj2 = new Circulo((int)M2.posicion.X, (int)M2.posicion.Z, (int)Radio_M);
            return Geom.indicarDireccion(Cj1.BoundingRect(), Cj2.BoundingRect());
        }

        //public char Dir_Ene_Jug(

    }
}
