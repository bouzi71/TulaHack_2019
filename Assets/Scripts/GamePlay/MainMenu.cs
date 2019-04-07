using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

namespace GamePlay
{
    public class MainMenu : MonoBehaviour
    {
        [System.Serializable]
        public class CameraPositions
        {
            public string key;
            public Vector3 postion;
            public Vector3 euler;
        }


        [SerializeField]
        private Transform main_camera;

        public List<CameraPositions> cameraPositions;

        [Header("Color components")]
        [SerializeField]
        private ColorController mainscene_colorController;
        [SerializeField]
        private ColorController load_colorController;
        [SerializeField]
        private ColorController learn_colorController;
        [SerializeField]
        private ColorController exper_colorController;

        public float t_ienumerate_game;
        public float t_invoke_delay;

        #region OnLoadGame
        private void Start()
        {
            var camera_data = cameraPositions.Find(el => el.key == "OnLoad");

            main_camera.position = camera_data.postion;
            main_camera.eulerAngles = camera_data.euler;

            load_colorController.Enable();
            mainscene_colorController.Disable();
            learn_colorController.Disable();
            exper_colorController.Disable();
        }

        #endregion

        #region OnStartPress
        public void OnStartBtnPress()
        {
            StartCoroutine(load_colorController.Disable(t_ienumerate_game/2));
            StartCoroutine(IStartSimLoad(cameraPositions.Find(el => el.key == "OnStartPress")));
            Invoke("Invoke_OnStart", t_invoke_delay);
            
        }
        private void Invoke_OnStart()
        {
            StartCoroutine(mainscene_colorController.Enable(t_ienumerate_game/2));
        }
        private IEnumerator IStartSimLoad(CameraPositions cam_dat)
        {
            Vector3 startP = main_camera.position;
            Vector3 startE = main_camera.eulerAngles;

            float startT = Time.unscaledTime, ct = 0;

            while((ct = (Time.unscaledTime - startT)/ t_ienumerate_game) < 1)
            {
                main_camera.position = Vector3.Lerp(startP, cam_dat.postion, ct);
                main_camera.eulerAngles = Vector3.Lerp(startE, cam_dat.euler, ct);

                yield return null;
            }

            main_camera.position = cam_dat.postion;
            main_camera.eulerAngles = cam_dat.euler;
        }
        #endregion

        #region Main Scene
        #region OnBackMenu
        public void OnBackLoadPress()
        {
            StartCoroutine(mainscene_colorController.Disable(t_ienumerate_game/2));
            StartCoroutine(IBackMenu(cameraPositions.Find(el => el.key == "OnLoad")));
            Invoke("Invoke_OnBack", t_invoke_delay);
        }
        private void Invoke_OnBack()
        {
            StartCoroutine(load_colorController.Enable(t_ienumerate_game/2));
        }
        private IEnumerator IBackMenu(CameraPositions cam_dat)
        {
            Vector3 startP = main_camera.position;
            Vector3 startE = main_camera.eulerAngles;

            float startT = Time.unscaledTime, ct = 0;

            while ((ct = (Time.unscaledTime - startT) / t_ienumerate_game) < 1)
            {
                main_camera.position = Vector3.Lerp(startP, cam_dat.postion, ct);
                main_camera.eulerAngles = Vector3.Lerp(startE, cam_dat.euler, ct);

                yield return null;
            }

            main_camera.position = cam_dat.postion;
            main_camera.eulerAngles = cam_dat.euler;
        }
        
        public void OnLearnPress()
        {
            StartCoroutine(mainscene_colorController.Disable(t_ienumerate_game / 2));
            //StartCoroutine(IBackMenu(cameraPositions.Find(el => el.key == "OnLoad")));
            Invoke("Invoke_OnLearn", t_invoke_delay);
        }
        private void Invoke_OnLearn()
        {
            StartCoroutine(learn_colorController.Enable(t_ienumerate_game / 2));
        }
        #endregion

        #region OnRestart
        public void OnRestart()
        {
            Objects.Control.SpaceController.Instance.Restart();
        }
        #endregion

        #region OnExper
        public void OnExperBtnPress()
        {
            StartCoroutine(mainscene_colorController.Disable(t_ienumerate_game / 2));
            //StartCoroutine(IBackMenu(cameraPositions.Find(el => el.key == "OnLoad")));
            Invoke("Invoke_OnExper", t_invoke_delay);
        }
        private void Invoke_OnExper()
        {
            ChangeObjectType();
            StartCoroutine(exper_colorController.Enable(t_ienumerate_game / 2));
        }
        #endregion
        #endregion

        #region Learn
        public void OnBackMainScene()
        {
            if (Objects.Visual.CanvasCatcher.Instance.detail)
            {
                Objects.Visual.CanvasCatcher.Instance.BaseCamera();
            }
            else
            {
                StartCoroutine(learn_colorController.Disable(t_ienumerate_game / 2));
                Invoke("Invoke_OnStart", t_invoke_delay);
            }
        }

        public void CathCanvas(Transform _parent)
        {
            Objects.Visual.CanvasCatcher.Instance.SetCamera(_parent);
        }
        #endregion

        #region Experiment
        //[SerializeField]
        //private Objects.Control.Create.ObjectCreator creator;
        [Header("Experiment")]
        [SerializeField]
        private Slider sl_mass;
        [SerializeField]
        private Slider sl_speed;
        [SerializeField]
        private Text text;

        [SerializeField]
        private float min_mass;
        [SerializeField]
        private float max_mass;
        [SerializeField]
        private float max_speed;

        public void OnBackFromExper()
        {
            StartCoroutine(exper_colorController.Disable(t_ienumerate_game / 2));
            Invoke("Invoke_OnStart", t_invoke_delay);
        }
        public void CreateNewObject()
        {
            Objects.Control.Create.ObjectCreator.Instance.Create();
            OnBackFromExper();
        }

        public void OnSpeedChange(Slider slider)
        {
            Objects.Control.Create.ObjectCreator.Instance.speed = slider.value;
        }
        public void OnMassChange(Slider slider)
        {
            float value = 0;
            Objects.Control.Create.ObjectCreator.Instance.mass = value.SinLerp(min_mass, max_mass, slider.value);
        }

        private Objects.Data.SpaceObject.ObjectType _t = Objects.Data.SpaceObject.ObjectType.Meteor;
        public void ChangeObjectType()
        {
            string txt = "";

            _t = (Objects.Data.SpaceObject.ObjectType)((int)++_t % 3);
            switch (_t)
            {
                case Objects.Data.SpaceObject.ObjectType.Meteor:
                    {
                        txt = "ЗАПУСТИ МЕТЕОРИТ!";
                        min_mass = 0.05f;
                        max_mass = 10.0f;
                        break;
                    }
                case Objects.Data.SpaceObject.ObjectType.Asteroid:
                    {
                        txt = "ЗАПУСТИ АСТЕРОИД!";
                        min_mass = 10.0f;
                        max_mass = 30.0f;
                        break;
                    }
                case Objects.Data.SpaceObject.ObjectType.BlackHole:
                    {
                        txt = "ЗАПУСТИ ЧЕРНУЮ ДЫРУ!";
                        min_mass = 1000;
                        max_mass = 1e+8f;
                        break;
                    }
                default: break;
            }

            text.text = txt;

            //sl_mass.minValue = min_mass;
            //sl_mass.maxValue = max_mass;
            //sl_mass.value = min_mass;


            OnSpeedChange(sl_speed);
            OnMassChange(sl_mass);
        }

        #endregion
    }
}

namespace Extensions
{
    public static class flaotExtensions
    {
        public static float SinLerp(this float value, float min, float max, float ct)
        {
            return min+Mathf.Sin(ct) * (max-min);
        }
    }
}

