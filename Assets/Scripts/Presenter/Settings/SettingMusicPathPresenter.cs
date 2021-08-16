using NoteEditor.Model;
using System.IO;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace NoteEditor.Presenter
{
    public class SettingMusicPathPresenter : MonoBehaviour
    {
        [SerializeField]
        InputField musicPathInputField = default;
        [SerializeField]
        Text musicPathInputFieldText = default;
        [SerializeField]
        Color defaultTextColor = default;
        [SerializeField]
        Color invalidStateTextColor = default;

        void Awake()
        {
            musicPathInputField.OnValueChangedAsObservable()
                .Select(path => Directory.Exists(Path.Combine(Settings.WorkSpacePath.Value ?? "", path)))
                .Subscribe(exists => musicPathInputFieldText.color = exists ? defaultTextColor : invalidStateTextColor);

            musicPathInputField.OnValueChangedAsObservable()
                .Where(path => Directory.Exists(Path.Combine(Settings.WorkSpacePath.Value ?? "", path)))
                .Subscribe(path => Settings.MusicPath.Value = path);

            Settings.MusicPath.DistinctUntilChanged()
                .Subscribe(path => musicPathInputField.text = path);
        }
    }
}
