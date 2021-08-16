using System.Collections.Generic;

namespace NoteEditor.DTO
{
    [System.Serializable]
    public class SettingsDTO
    {
        public string workSpacePath;
        public string musicPath;
        public string notesPath;
        public int maxBlock;
        public List<int> noteInputKeyCodes;

        public static SettingsDTO GetDefaultSettings()
        {
            return new SettingsDTO
            {
                workSpacePath = "",
                musicPath = "Music",
                notesPath = "Notes",
                maxBlock = 5,
                noteInputKeyCodes = new List<int> { 114, 99, 103, 121, 98 }
            };
        }
    }
}
