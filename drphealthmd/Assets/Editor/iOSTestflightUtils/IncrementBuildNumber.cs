using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor;

public class IncrementBuildNumber : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report)
    {
        if (report.summary.platform == BuildTarget.iOS)
        {
            int currentBuildNumber;
            if (int.TryParse(PlayerSettings.iOS.buildNumber, out currentBuildNumber))
            {
                string newBuildNumber = (currentBuildNumber + 1).ToString();
                Debug.Log("Setting new iOS build number to " + newBuildNumber);
                PlayerSettings.iOS.buildNumber = newBuildNumber;
            }
            else
            {
                Debug.LogError("Failed to parse build number " + PlayerSettings.iOS.buildNumber + " as int.");
            }
        }
    }
}
