using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETP_Matus_Atack
{
    public static class EstadoDeJuego
    {
        public enum Estado
        {
            Portada,
            Instrucciones1,
            Instrucciones2,
            Juego,
            Win,
            Loose
        }

        static Estado estado = Estado.Portada;

        static public bool onPortada()
        {
            return estado == Estado.Portada;
        }

        static public bool onInstrucciones1()
        {
            return estado == Estado.Instrucciones1;
        }

        static public bool onInstrucciones2()
        {
            return estado == Estado.Instrucciones2;
        }

        static public bool onJuego()
        {
            return estado == Estado.Juego;
        }

        static public bool onWin()
        {
            return estado == Estado.Win;
        }

        static public bool onLoose()
        {
            return estado == Estado.Loose;
        }





        static public void setPortada()
        {
            estado = Estado.Portada;
        }

        static public void setInstrucciones1()
        {
            estado = Estado.Instrucciones1;
        }

        static public void setInstrucciones2()
        {
            estado = Estado.Instrucciones2;
        }

        static public void setJuego()
        {
            estado = Estado.Juego;
        }

        static public void setWin()
        {
            estado = Estado.Win;
        }

        static public void setLoose()
        {
            estado = Estado.Loose;
        }

        

    }
}
