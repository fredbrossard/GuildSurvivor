#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using AudioManagement.Scriptable;
using UnityEditor;
using UnityEngine;

namespace AudioManagement.Tool
{
    public class CreateAudioElementScriptableHelper : MonoBehaviour
    {
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

        [SerializeField] private string _audioFolderPath = null;
        [SerializeField] private AudioBind[] binds = null;
        
        #region CREATE
        public void CreateAll()
        {
            if (_audioFolderPath == string.Empty)
            {
                Debug.LogError("AudioFolderPath is empty");
                return;
            }

            if(binds == null)
            {
                Debug.LogError("Audio binds are empty");
            }

            foreach(AudioBind bind in binds)
            {
                string fullPath = _audioFolderPath + "/" + bind.folderName;

                if(Directory.Exists(fullPath))
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
            foreach(var audioClip in audioClips) 
            { 
                AudioElement audioElement = ScriptableObject.CreateInstance<AudioElement>();
                audioElement.name = GetAudioFileName(audioClip, bind);
                //audioElement.delay = bind.delay;
                audioElement.clip = (AudioClip)AssetDatabase.LoadAssetAtPath(audioClip, typeof(AudioClip));
                audioElement.generalVolume = bind.generalVolume;
                audioElement.isLooping = bind.isLooping;
                audioElement.randomPitch = new(1, 1);
                audioElement.playOneShot = bind.playOneShot;

                if(!File.Exists(fullPath + "/" + audioElement.name + ".asset"))
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
            if(filesFinded != null)
            {
                foreach (var file in filesFinded) 
                { 
                    if(!file.Contains(".meta") && !file.Contains(".asset"))
                    {
                        files.Add(file);
                    }
                }
            }
        }

        private void DisplayClipNames(ref List<string> audioClips, string folderName)
        {
            Debug.Log("====Clip Name for " + folderName + "====");

            if(audioClips != null)
            {
                for(int i = 0; i < audioClips.Count; ++i)
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
                bind?.library.elements.Clear();

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
    }
}
#endif