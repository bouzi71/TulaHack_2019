using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        #endregion

        #region Learn
        public void OnBackMainScene()
        {
            StartCoroutine(learn_colorController.Disable(t_ienumerate_game / 2));
            Invoke("Invoke_OnStart", t_invoke_delay);
        }
        #endregion
    }
}

