using NoteEditor.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace NoteEditor.GLDrawing
{
    public class BeatNumberRenderer : MonoBehaviour
    {
        [SerializeField]
        GameObject beatNumberPrefab = default;

        List<RectTransform> rectTransformPool = new List<RectTransform>();
        List<Text> textPool = new List<Text>();

        int size;
        int countPrevActive = 0;
        int countCurrentActive = 0;

        public void Render(Vector3 pos, int number)
        {
            if (countCurrentActive < size)
            {
                if (countCurrentActive >= countPrevActive)
                {
                    textPool[countCurrentActive].gameObject.SetActive(true);
                }

                rectTransformPool[countCurrentActive].position = pos;
                textPool[countCurrentActive].text = number.ToString();
            }
            else
            {
                var obj = Instantiate(beatNumberPrefab, pos, Quaternion.identity) as GameObject;
                obj.transform.SetParent(transform);
                obj.transform.localScale = Vector3.one;
                rectTransformPool.Add(obj.GetComponent<RectTransform>());
                textPool.Add(obj.GetComponent<Text>());
                size++;
            }

            countCurrentActive++;
        }

        public void Begin()
        {
            countPrevActive = countCurrentActive;
            countCurrentActive = 0;
        }

        public void End()
        {
            if (countCurrentActive < countPrevActive)
            {
                for (int i = countCurrentActive; i < countPrevActive; i++)
                {
                    textPool[i].gameObject.SetActive(false);
                }
            }

            if (countCurrentActive * 2 < size)
            {
                foreach (var text in textPool.Skip(countCurrentActive + 1))
                {
                    Destroy(text.gameObject);
                }

                rectTransformPool.RemoveRange(countCurrentActive, size - countCurrentActive);
                textPool.RemoveRange(countCurrentActive, size - countCurrentActive);
                size = countCurrentActive;
            }
        }
    }
}
