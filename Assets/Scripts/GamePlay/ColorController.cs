using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GamePlay
{
    public class ColorController : MonoBehaviour
    {
        // Store initial color value
        public Dictionary<int, float> m_InitialAlphas = new Dictionary<int, float>();

        public List<Image> images;
        public List<Text> texts;

        public List<Graphic> graphicsElemets;

        private void Awake()
        {
            Instantiate();
        }

        private void Instantiate()
        {
            graphicsElemets = new List<Graphic>();
            graphicsElemets.AddRange(images);
            graphicsElemets.AddRange(texts);
        }

        public void Enable()
        {
            gameObject.SetActive(true);

            if (graphicsElemets.Count == 0) Instantiate();
            foreach (var im in graphicsElemets)
            {
                im.gameObject.SetActive(true);
                im.color = ChangeColorAlpha(im.color, RestoreInitialAlpha(im));
            }
        }

        public IEnumerator Enable(float t)
        {
            float ct = 0;
            float t_start = Time.unscaledTime;

            gameObject.SetActive(true);

            if (graphicsElemets.Count == 0) Instantiate();
            foreach (var im in graphicsElemets) im.gameObject.SetActive(true);

            while ((ct = (Time.unscaledTime - t_start) / t) < 1)
            {
                foreach (var im in graphicsElemets)
                    im.color = ChangeColorAlpha(im.color, ct, RestoreInitialAlpha(im));

                yield return null;
            }

            foreach (var im in graphicsElemets) im.color = ChangeColorAlpha(im.color, RestoreInitialAlpha(im));
        }

        public void Disable()
        {
            gameObject.SetActive(false);

            if (graphicsElemets.Count == 0) Instantiate();
            foreach (var im in graphicsElemets)
            {
                StoreInitialAlpha(im, im.color);
                im.gameObject.SetActive(false);
                im.color = ChangeColorAlpha(im.color, 0);
            }
        }

        public IEnumerator Disable(float t)
        {
            float ct = 0, t_start = Time.unscaledTime;

            if (graphicsElemets.Count == 0) Instantiate();
            foreach (var im in graphicsElemets)
                StoreInitialAlpha(im, im.color);

            while ((ct = (Time.unscaledTime - t_start) / t) < 1)
            {
                foreach (var im in graphicsElemets)
                    im.color = ChangeColorAlpha(im.color, 1 - ct, RestoreInitialAlpha(im));

                yield return null;
            }

            foreach (var im in graphicsElemets)
            {
                im.gameObject.SetActive(false);
                im.color = ChangeColorAlpha(im.color, 0);
            }
 
            gameObject.SetActive(false);
        }



        //
        // Сохраняем инчальную альфу
        //
        private void StoreInitialAlpha(Object o, Color c)
        { 
            int hash = o.GetHashCode();

            if (m_InitialAlphas.ContainsKey(hash))
                return;

            m_InitialAlphas.Add(hash, c.a);
        }



        //
        // Возвращает инчальную альфу
        //
        private float RestoreInitialAlpha(Object o)
        {
            int hash = o.GetHashCode();

            if (! m_InitialAlphas.ContainsKey(hash))
                return 1.0f;

            return m_InitialAlphas[hash];
        }



        //
        // Изменят alpha цвета, не трогая сам цвет
        //
        private Color ChangeColorAlpha(Color c, float newAlpha)
        {
            return new Color(c.r, c.g, c.b, newAlpha);
        }
        private Color ChangeColorAlpha(Color c, float delta, float initial)
        {
            return new Color(c.r, c.g, c.b, delta*initial);
        }
    }
}
