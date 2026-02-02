#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AudioManagement.Scriptable;
using UnityEditor;
using UnityEngine;

namespace AudioManagement.Tool
{
    public class AudioElementScriptableHelper : MonoBehaviour
    {
        public enum AudioExtention
        {
            wav,
            mp3,
            ogg
        }
        [Serializable]
        public class AudioBind
        {
            public string folderName;
            public AudioLibrary library;
            [Range(0f, 1f)] public float generalVolume = 1f;
            public bool isLooping = false;
            public bool playOneShot = false;
            public ulong delay = 0;
        }

        [Header("CreateClip")]
        [SerializeField] private string _audioFolderPath = null;
        [SerializeField] private AudioBind[] binds = null;

        [Header("Check audio clip")]
        [SerializeField] private string jsonDataPath;
        [SerializeField] private string[] audioClipPaths;
        [SerializeField] private AudioExtention audioExtention;

        #region CREATE
        public void CreateAll()
        {
            if (_audioFolderPath == string.Empty)
            {
                Debug.LogError("AudioFolderPath is empty");
                return;
            }

            if (binds == null)
            {
                Debug.LogError("Audio binds are empty");
            }

            foreach (AudioBind bind in binds)
            {
                string fullPath = _audioFolderPath + "/" + bind.folderName;

                if (Directory.Exists(fullPath))
                {
                    string[] filesFinded = Directory.GetFiles(fullPath);
                    List<string> audioClips = new List<string>();
                    DeleteUselessFiles(out audioClips, filesFinded);
                    DisplayClipNames(ref audioClips, bind.folderName);

                    if (audioClips != null && audioClips.Count > 0)
                    {
                        CreateAudioElementScriptable(ref audioClips, bind, fullPath);
                    }
                }
                else
                {
                    Debug.LogError("Directory doesn't exist: " + fullPath);
                }
            }
        }

        private void CreateAudioElementScriptable(ref List<string> audioClips, AudioBind bind, string fullPath)
        {
            Debug.Log("====Creation audio element scriptable====");
            if (audioClips == null)
            {
                return;
            }
            int itemCreatedCount = 0;

            if (bind.library.version > 1000)
                bind.library.version = 0;

            bind.library.version = bind.library.version + 1;
            foreach (var audioClip in audioClips)
            {
                AudioElement audioElement = ScriptableObject.CreateInstance<AudioElement>();
                audioElement.name = GetAudioFileName(audioClip, bind);
                //audioElement.delay = bind.delay;
                audioElement.clip = (AudioClip)AssetDatabase.LoadAssetAtPath(audioClip, typeof(AudioClip));
                audioElement.generalVolume = bind.generalVolume;
                audioElement.isLooping = bind.isLooping;
                audioElement.randomPitch = new(1, 1);
                audioElement.playOneShot = bind.playOneShot;

                if (!File.Exists(fullPath + "/" + audioElement.name + ".asset"))
                {
                    AssetDatabase.CreateAsset(audioElement, fullPath + "/" + audioElement.name + ".asset");
                    Debug.Log("Item: " + audioElement.name + ".asset has created");

                    bind.library.elements.Add(audioElement);
                    itemCreatedCount++;
                }
            }

            Debug.Log(itemCreatedCount + " audio elements are created");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void DeleteUselessFiles(out List<string> files, string[] filesFinded)
        {
            files = new List<string>();
            if (filesFinded != null)
            {
                foreach (var file in filesFinded)
                {
                    if (!file.Contains(".meta") && !file.Contains(".asset"))
                    {
                        files.Add(file);
                    }
                }
            }
        }

        private void DisplayClipNames(ref List<string> audioClips, string folderName)
        {
            Debug.Log("====Clip Name for " + folderName + "====");

            if (audioClips != null)
            {
                for (int i = 0; i < audioClips.Count; ++i)
                {
                    Debug.Log(audioClips[i]);
                }
            }
        }

        private string GetAudioFileName(string audioClipName, AudioBind bind)
        {
            return audioClipName.Replace(_audioFolderPath, "").Replace(_audioFolderPath, "")
                        .Replace(bind.folderName, "").Replace("\\", "").Replace(@"/", "")
                        .Replace(".ogg", "").Replace(".mp3", "").Replace(".wav", "");
        }
        #endregion

        #region DELETE
        public void DeleteAll()
        {
            foreach (AudioBind bind in binds)
            {
                if (bind != null)
                {
                    bind.library.elements.Clear();
                    bind.library.version = 0;
                }

                string fullPath = _audioFolderPath + "/" + bind.folderName;

                if (Directory.Exists(fullPath))
                {

                    DeleteAssetFiles(Directory.GetFiles(fullPath));
                }
                else
                {
                    Debug.LogError("Directory doesn't exist: " + fullPath);
                }
            }
        }

        private void DeleteAssetFiles(string[] filesFinded)
        {
            if (filesFinded != null)
            {
                int fileDeletedCount = 0;
                foreach (var file in filesFinded)
                {
                    if (file.Contains(".asset"))
                    {
                        AssetDatabase.DeleteAsset(file);
                        fileDeletedCount++;
                    }
                }

                Debug.Log(fileDeletedCount + " files has deleted");
            }
        }
        #endregion

        #region CHECK_AUDIO_CLIP_EXIT
        public async Task CheckAudioClipIsExit()
        {
            await LoadAllCutscene();
        }

        private async Task LoadAllCutscene()
        {
            if (File.Exists(jsonDataPath))
            {
                Debug.Log("Load Json");
                string content = await File.ReadAllTextAsync(jsonDataPath);

                ///TODO with dialogue json in starter
            //DialogueRecord dialogues = JsonConvert.DeserializeObject<DialogueRecord>(content);

            //    foreach (var clipPath in audioClipPaths)
            //    {
            //        if (!Directory.Exists(clipPath))
            //        {
            //            Debug.LogError("Clip folder: " + clipPath + " doesn't exist");
            //            return;
            //        }
            //    }

            //    if (dialogues != null && dialogues.records != null)
            //    {
            //        int clipExistCount = 0;
            //        int maxClip = 0;
            //        bool clipFinded = false;

            //        foreach (var record in dialogues.records)
            //        {
            //            clipFinded = false;
            //            foreach (var clipPath in audioClipPaths)
            //            {
            //                string path = clipPath + "/" + record.fields.audio + GetAudioExtention(audioExtention);

            //                if (File.Exists(path) && !string.IsNullOrEmpty(record.fields.audio))
            //                {
            //                    Debug.Log("Audio: " + record.fields.audio + " exist");
            //                    clipFinded = true;
            //                    clipExistCount++;
            //                    maxClip++;
            //                }
            //            }
            //            if (!clipFinded && !string.IsNullOrEmpty(record.fields.audio))
            //            {
            //                Debug.LogError("Audio: " + record.fields.audio);
            //                maxClip++;
            //            }
            //        }
            //        Debug.Log("There are " + clipExistCount + "/" + maxClip + " audio clips");
            //    }
            //    Debug.Log("Check ended");
            //}
            //else
            //{
            //    Debug.Log("Json data path doesn't exist");
            }
        }

        private string GetAudioExtention(AudioExtention audioExtention)
        {
            switch (audioExtention)
            {
                case AudioExtention.wav:
                    return ".wav";

                case AudioExtention.ogg:
                    return ".ogg";

                default:
                    return ".mp3";
            }
        }

        public void CheckAudioClipRefInAudioElement()
        {
            if (binds != null)
            {
                bool findOneThings = false;
                foreach (var bind in binds)
                {
                    foreach (var element in bind.library.elements)
                    {
                        if (element.clip == null)
                        {
                            findOneThings = true;
                            Debug.LogError("Audio clip is missing in: " + element.name);
                        }

                        if (element.clip != null
                            && element.name != element.clip.name)
                        {
                            findOneThings = true;
                            Debug.LogError("Clip isn't ref in good audio element " + element.name);
                        }
                    }
                }

                if (!findOneThings)
                {
                    Debug.Log("All libraries are ok");
                }
            }
        }
        #endregion
    }
    
}
#endif