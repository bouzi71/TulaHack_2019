using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GamePlay
{
    public class ColorController : MonoBehaviour
    {
        public List<Image> images;
        public List<Text> texts;


        public void Enable()
        {
            gameObject.SetActive(true);
            foreach(var im in images)
            {
                im.gameObject.SetActive(true);
                im.color = Color.white;
            }
            foreach (var txt in texts)
            {
                txt.gameObject.SetActive(true);
                txt.color = Color.white;
            }   
        }

        public IEnumerator Enable(float t)
        {
            float ct = 0, t_start = Time.unscaledTime;
            gameObject.SetActive(true);
            foreach (var im in images) im.gameObject.SetActive(true);
            foreach (var txt in texts) txt.gameObject.SetActive(true);

            while ((ct = (Time.unscaledTime - t_start) / t) < 1)
            {
                foreach (var im in images) im.color = new Color(1,1,1,ct);
                foreach (var txt in texts) txt.color = new Color(1, 1, 1, ct);

                yield return null;
            }

            foreach (var im in images) im.color = Color.white;
            foreach (var txt in texts) txt.color = Color.white;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            foreach (var im in images)
            {
                im.gameObject.SetActive(false);
                im.color = new Color(1,1,1,0);
            }
            foreach (var txt in texts)
            {
                txt.gameObject.SetActive(false);
                txt.color = new Color(1, 1, 1, 0);
            }
        }

        public IEnumerator Disable(float t)
        {
            float ct = 0, t_start = Time.unscaledTime;

            while ((ct = (Time.unscaledTime - t_start) / t) < 1)
            {
                var _ct = 1 - ct;
                foreach (var im in images) im.color = new Color(1, 1, 1, _ct);
                foreach (var txt in texts) txt.color = new Color(1, 1, 1, _ct);

                yield return null;
            }

            foreach (var im in images) im.color = new Color(1,1,1,0);
            foreach (var txt in texts) txt.color = new Color(1, 1, 1, 0);

            foreach (var im in images) im.gameObject.SetActive(false);
            foreach (var txt in texts) txt.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
