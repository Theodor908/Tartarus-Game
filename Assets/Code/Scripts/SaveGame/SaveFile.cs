using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Tartarus
{
    // 10 slots
    public class SaveFile
    {
        public string saveDataPath = "";
        public string saveFileName = "";

        // Check if the file exists before we create a new save file
        public bool CheckForSaveFile()
        {
            if(File.Exists(Path.Combine(saveDataPath, saveFileName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete character slots
        public void DeleteSaveFile()
        {
            File.Delete(Path.Combine(saveDataPath, saveFileName));
        }

        // Create a new save file

        public void CreateNewSaveFile(CharacterSaveData characterData)
        {
            string savePath = Path.Combine(saveDataPath, saveFileName);

            try
            {
                //Check if the file exists
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("Creating save game file at: " + savePath);

                // Convert the character data to a json string

                string jsonData = JsonUtility.ToJson(characterData, true);

                // Write the json string to the file

                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter fileWriter = new StreamWriter(stream))
                    {
                        fileWriter.Write(jsonData);
                    }
                }

            }
            catch (System.Exception e)
            {
                Debug.LogError("Error whilst creating save file: " + Path.Combine(saveDataPath, saveFileName) + " " + e);
            }

        }

        // Load game

        public CharacterSaveData LoadSaveFile()
        {
            CharacterSaveData characterData = null;
            // Look for file path
            string loadPath = Path.Combine(saveDataPath, saveFileName);

            if(File.Exists(loadPath))
            {
                try
                {
                    // Read the json data from the file
                   using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                    {
                       using (StreamReader fileReader = new StreamReader(stream))
                        {
                           string jsonData = fileReader.ReadToEnd();

                           // Convert the json data to a character save data object
                           characterData = JsonUtility.FromJson<CharacterSaveData>(jsonData);
                       }
                   }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error whilst loading save file: " + Path.Combine(saveDataPath, saveFileName) + " " + e);
                }
            }
            else
            {
                Debug.LogError("No save file found at: " + loadPath);
            }

            return characterData;

        }

    }
}
