using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ETP_Matus_Atack
{
    public class CameraAnimated : Camera
    {

        private floatFade fieldOfViewFade;
        private bool fieldOfViewFadeOn;

        private Vector3Fade cameraPositionFade;
        private bool cameraPositionFadeOn;

        private Vector3Fade cameraTargetFade;
        private bool cameraTargetFadeOn;

        private Vector3Fade upFade;
        private bool upFadeOn;



        public CameraAnimated()
            : base()
        { }


        public void FadeFieldOfView(float NewFieldOfView, float Disminution)
        {
            fieldOfViewFadeOn = true;
            fieldOfViewFade = new floatFade();
            fieldOfViewFade.Actual = fieldOfView;
            fieldOfViewFade.Next = NewFieldOfView;
            fieldOfViewFade.disminution = Disminution;
        }

        public void NOT_FadeFieldOfView()
        {
            fieldOfViewFadeOn = false;
        }


        public void FadeCameraPosition(Vector3 NewCameraPosition, float Disminution)
        {
            cameraPositionFadeOn = true;
            cameraPositionFade = new Vector3Fade();
            cameraPositionFade.Actual = CameraPosition;
            cameraPositionFade.Next = NewCameraPosition;
            cameraPositionFade.disminution = Disminution;
        }

        public void NOT_FadeCameraPosition()
        {
            cameraPositionFadeOn = false;
        }

        public bool CameraPositionOnTarget(float offset)
        {
            if (cameraPositionFadeOn)
                return Vector3.DistanceSquared(cameraPositionFade.Actual, cameraPositionFade.Next) <= offset;
            else return true;
        }


        public void FadeCameraTarget(Vector3 NewCameraTarget, float Disminution)
        {
            cameraTargetFadeOn = true;
            cameraTargetFade = new Vector3Fade();
            cameraTargetFade.Actual = CameraTarget;
            cameraTargetFade.Next = NewCameraTarget;
            cameraTargetFade.disminution = Disminution;
        }

        public void NOT_FadeCameraTarget()
        {
            cameraTargetFadeOn = false;
        }

        public void FadeUp(Vector3 NewUp, float Disminution)
        {
            upFadeOn = true;
            upFade = new Vector3Fade();
            upFade.Actual = Up;
            upFade.Next = NewUp;
            upFade.disminution = Disminution;
        }

        public void NOT_FadeUp()
        {
            upFadeOn = false;
        }

        public void Update(GameTime gameTime)
        {
            if (fieldOfViewFadeOn)
            {
                fieldOfViewFade.Advance((float)gameTime.ElapsedGameTime.TotalSeconds);
                FieldOfView = fieldOfViewFade.Actual;
            }

            if (cameraPositionFadeOn)
            {
                cameraPositionFade.Advance((float)gameTime.ElapsedGameTime.TotalSeconds);
                CameraPosition = cameraPositionFade.Actual;
            }

            if (cameraTargetFadeOn)
            {
                cameraTargetFade.Advance((float)gameTime.ElapsedGameTime.TotalSeconds);
                CameraTarget = cameraTargetFade.Actual;
            }

            if (upFadeOn)
            {
                upFade.Advance((float)gameTime.ElapsedGameTime.TotalSeconds);
                Up = upFade.Actual;
            }

        }
        

    }


    struct Vector3Fade
    {
        public Vector3 Actual;
        public Vector3 Next;

        public float disminution;

        public float EndPresition;

        public void Advance(float time)
        {
            if (IsFarFromEnd())
                Actual -= time * (Actual - Next) * disminution;
        }

        public bool IsFarFromEnd()
        {
            return EndPresition * EndPresition < Vector3.DistanceSquared(Actual, Next);
        }
    }

    struct floatFade
    {
        public float Actual;
        public float Next;


        public float disminution;

        public float EndPresition;

        public void Advance(float time)
        {
            if (IsFarFromEnd())
                Actual -= time * (Actual - Next) * disminution;
        }

        public bool IsFarFromEnd()
        {
            return EndPresition < Math.Abs(Actual - Next);
        }
    }

}
