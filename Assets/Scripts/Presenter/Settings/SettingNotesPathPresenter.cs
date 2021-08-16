using NoteEditor.Model;
using System.IO;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace NoteEditor.Presenter
{
    public class SettingNotesPathPresenter : MonoBehaviour
    {
        [SerializeField]
        InputField notesPathInputField = default;
        [SerializeField]
        Text notesPathInputFieldText = default;
        [SerializeField]
        Color defaultTextColor = default;
        [SerializeField]
        Color invalidStateTextColor = default;

        void Awake()
        {
            notesPathInputField.OnValueChangedAsObservable()
                .Select(path => Directory.Exists(Path.Combine(Settings.WorkSpacePath.Value ?? "", path)))
                .Subscribe(exists => notesPathInputFieldText.color = exists ? defaultTextColor : invalidStateTextColor);

            notesPathInputField.OnValueChangedAsObservable()
                .Where(path => Directory.Exists(Path.Combine(Settings.WorkSpacePath.Value ?? "", path)))
                .Subscribe(path => Settings.NotesPath.Value = path);

            Settings.NotesPath.DistinctUntilChanged()
                .Subscribe(path => notesPathInputField.text = path);
        }
    }
}
