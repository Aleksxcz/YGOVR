using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ImageDownloader : MonoBehaviour
{
    public string zipFileUrl = "https://www.dropbox.com/s/i9owmowpyvpd8zv/images.zip?e=1&st=dqaeq6eo&dl=1";  // Replace with actual URL
    private string downloadPath;
    private string extractPath;

    void Start()
    {
        downloadPath = Application.persistentDataPath + "/images.zip";  // Temp download location
        extractPath = Application.dataPath + "/Resources/Card Pics";  // Target extraction folder in Unity's Assets/Resources

        // Check if images already exist
        if (Directory.Exists(extractPath) && Directory.GetFiles(extractPath, "*", SearchOption.AllDirectories).Length > 0)
        {
            Debug.Log("Images are already extracted in Resources.");
        }
        else
        {
            StartCoroutine(DownloadAndExtractZIP());
        }
    }

    IEnumerator DownloadAndExtractZIP()
    {
        // Step 1: Download the ZIP file
        UnityWebRequest www = UnityWebRequest.Get(zipFileUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            File.WriteAllBytes(downloadPath, www.downloadHandler.data);
            Debug.Log("ZIP file downloaded successfully.");
            Debug.Log("ZIP downloaded to: " + downloadPath);
            // Step 2: Extract ZIP file
            ExtractZipFile(downloadPath, extractPath);

            // Step 3: Refresh the Unity asset database to recognize new files
            #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
            #endif
        }
        else
        {
            Debug.LogError("Failed to download ZIP file: " + www.error);
        }
    }

    void ExtractZipFile(string zipFilePath, string extractDirectory)
    {
        // If the directory exists, clear it to avoid old files lingering
        if (Directory.Exists(extractDirectory))
        {
            Directory.Delete(extractDirectory, true);
        }

        Directory.CreateDirectory(extractDirectory);

        // Extract ZIP contents
        ZipFile.ExtractToDirectory(zipFilePath, extractDirectory);
        Debug.Log("ZIP file extracted to Resources.");

        // Delete the ZIP file after extraction (optional)
        File.Delete(zipFilePath);
    }
}
