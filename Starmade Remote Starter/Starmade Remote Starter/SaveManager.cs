using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;



namespace Starmade_Remote_Starter
{
    public class SaveManager
    {
        private String folderPath;
        const String folder = "\\SMRS_Data";
        const String saveFilePath = "\\SMRS_Data\\Server_data.bin";

        public SaveManager()
        {
            folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
        public List<ServerObject> GetSavedData()
        {
            List<ServerObject> tempServerObjects = new List<ServerObject>();

            if (!SaveDirectoryExists())
            {
                return tempServerObjects;
            }
            try
            {
                using (FileStream stream = new FileStream(folderPath +saveFilePath, FileMode.OpenOrCreate))
                {
                    BinaryFormatter ReadFormatter = new BinaryFormatter();
                    tempServerObjects = (List<ServerObject>)ReadFormatter.Deserialize(stream);
                }
            }
            catch (SerializationException)
            {
                MessageBox.Show("Failed to retrieve data", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            return tempServerObjects;

        }
        public void SaveServerObjects(List<ServerObject> target)
        {
            
            if (!Directory.Exists(folderPath + folder))
            {
                Directory.CreateDirectory(folderPath +folder);
            }

            try
            {
                using (FileStream stream = new FileStream(folderPath + saveFilePath, FileMode.OpenOrCreate))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, target);
                }
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }

        public bool SaveDirectoryExists()
        {
            if (Directory.Exists(folderPath + folder))
            {
                return true;
            }
            return false;
        }
    }
}
