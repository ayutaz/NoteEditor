using NoteEditor.DTO;
using System.Linq;
using UnityEngine;

namespace NoteEditor.Model
{
    public class SettingsSerializer
    {
        public static void Deserialize(string json)
        {
            var dto = JsonUtility.FromJson<SettingsDTO>(json);
            Settings.NoteInputKeyCodes.Value = dto.noteInputKeyCodes
                .Select(keyCodeNum => (KeyCode)keyCodeNum)
                .ToList();

            Settings.MaxBlock = dto.maxBlock;

            Settings.WorkSpacePath.Value = string.IsNullOrEmpty(dto.workSpacePath)
                ? Application.persistentDataPath
                : dto.workSpacePath;

            Settings.MusicPath.Value = string.IsNullOrEmpty(dto.musicPath)
                ? Application.persistentDataPath
                : dto.musicPath;

            Settings.NotesPath.Value = string.IsNullOrEmpty(dto.notesPath)
                ? Application.persistentDataPath
                : dto.notesPath;
        }

        public static string Serialize()
        {
            var data = new SettingsDTO();

            data.workSpacePath = Settings.WorkSpacePath.Value;
            data.musicPath = Settings.MusicPath.Value;
            data.notesPath = Settings.NotesPath.Value;
            data.maxBlock = EditData.MaxBlock.Value;
            data.noteInputKeyCodes = Settings.NoteInputKeyCodes.Value
                .Take(EditData.MaxBlock.Value)
                .Select(keyCode => (int)keyCode)
                .ToList();

            return JsonUtility.ToJson(data);
        }
    }
}
