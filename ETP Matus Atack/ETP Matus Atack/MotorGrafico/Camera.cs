using System;
using Microsoft.Xna.Framework;

namespace ETP_Matus_Atack
{
    public class Camera
    {
        #region properties:

        public Matrix ViewMatrix
        {
            get 
            {
                if(cameraPositionChanged || cameraTargetChanged || upChanged)
                {
                    viewMatrix = Matrix.CreateLookAt(cameraPosition,
                                                cameraTarget,
                                                up);
                    cameraPositionChanged = false;
                    cameraTargetChanged = false;
                    upChanged = false;
                }

                return viewMatrix;
            }
        }
        


        public Matrix ProjectionMatrix
        {
            get 
            {
                if (fieldOfViewChanged || aspectRadioChanged || nearPlaneChanged || farPlaneChanged)
                {
                    projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                                                        fieldOfView,
                                                        aspectRadio, 
                                                        nearPlane,
                                                        farPlane);
                    fieldOfViewChanged = false;
                    aspectRadioChanged = false;
                    nearPlaneChanged = false;
                    farPlaneChanged = false;
                }

                return projectionMatrix;
            }
        }
        


        public Vector3 Up
        {
            get { return up; }
            set 
            {   
                up = value;
                upChanged = true;
            }
        }


        public Vector3 CameraPosition
        {
            get { return cameraPosition; }
            set
            {
                cameraPosition = value;
                cameraPositionChanged = true;
            }
        }


        public Vector3 CameraTarget
        {
            get { return cameraTarget; }
            set
            {
                cameraTarget = value;
                cameraTargetChanged = true;
            }
        }


        public float FarPlane
        {
            get { return farPlane; }
            set
            {
                farPlane = value;
                farPlaneChanged = true;
            }
        }


        public float NearPlane
        {
            get { return nearPlane; }
            set
            {
                nearPlane = value;
                nearPlaneChanged = true;
            }
        }


        public float FieldOfView
        {
            get { return fieldOfView; }
            set
            {
                fieldOfView = value;
                fieldOfViewChanged = true;
            }
        }



        public float AspectRadio
        {
            get { return aspectRadio; }
            set
            {
                aspectRadio = value;
                aspectRadioChanged = true;
            }
        }


        #endregion

        #region Private Vars:

        private Matrix viewMatrix;

        private Matrix projectionMatrix;



        private Vector3 up;
        private bool upChanged;

        private Vector3 cameraPosition;
        private bool cameraPositionChanged;

        private Vector3 cameraTarget;
        private bool cameraTargetChanged;


        public float farPlane;
        private bool farPlaneChanged;

        public float nearPlane;
        private bool nearPlaneChanged;

        public float fieldOfView;
        private bool fieldOfViewChanged;

        public float aspectRadio;
        private bool aspectRadioChanged;


        #endregion

        public Camera()
        {
            viewMatrix = Matrix.Identity;
            projectionMatrix = Matrix.Identity;
            up = Vector3.Up;
        }


        public void setViewMatrix(Vector3 CameraPosition, Vector3 CameraTarget, Vector3 Up)
        {
            this.CameraPosition = CameraPosition;
            this.CameraTarget = CameraTarget;
            this.Up = Up;
        }


        public void setProjectionMatrix(float FieldOfView, float AspectRadio
                                                , float NearPlane, float FarPlane)
        {
            this.FieldOfView = FieldOfView;
            this.AspectRadio = AspectRadio;
            this.NearPlane = NearPlane;
            this.FarPlane = FarPlane;
        }




    }
}
