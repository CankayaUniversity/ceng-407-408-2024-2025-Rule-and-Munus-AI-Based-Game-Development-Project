using UnityEditor;
using UnityEngine;

namespace CodyDreams
{
    namespace Developer
    {
        [InitializeOnLoad]
        // LPSNP stands for Low Poly Stylized Nature Pack
        // This script belongs to that pack
        public class FeedbackWindowLPSNP : EditorWindow
        {
            private const string AssetStoreFeedbackUrl = "https://assetstore.unity.com/packages/3d/environments/low-poly-stylized-nature-281338"; // URL for the current pack
            private const string FakeFogUrl = "https://assetstore.unity.com/packages/tools/particles-effects/fake-fog-296903"; // Fake Fog URL
            private const string WebsiteUrl = "https://sites.google.com/view/codydreams/home"; // Your website URL
            private const string ItchUrl = "https://cody-dreams.itch.io/"; // Itch.io games URL
            private const float WindowWidth = 600f;  // Fixed width
            private const float WindowHeight = 350f; // Increased height for additional content
            private static FeedbackWindowLPSNP window;
            private static bool showOnStartup;

            // Static constructor for initializing static members
            static FeedbackWindowLPSNP()
            { 
                // Show the feedback window on startup based on preference
                if (!showOnStartup)
                {
                    ShowWindow();
                }
            }

            private void OnEnable()
            {
                // Ensure the preference is up-to-date
                showOnStartup = EditorPrefs.GetBool("ShowFeedbackWindowOnStartupLPSNP", true);
            }

            [MenuItem("Window/Cody Dremas/FeedBack windows/Low Poly stylized Nature ")]
            public static void ShowWindow()
            {
                if (window == null)
                {
                    window = GetWindow<FeedbackWindowLPSNP>("Feedback");
                }
                else
                {
                    FocusWindowIfItsOpen<FeedbackWindowLPSNP>();
                }

                // Set window size constraints
                window.minSize = new Vector2(WindowWidth, WindowHeight);
                window.maxSize = new Vector2(WindowWidth, WindowHeight);
            }

            private void OnGUI()
            {
                // Centering the content both vertically and horizontally
                EditorGUILayout.BeginVertical(GUILayout.Width(WindowWidth), GUILayout.Height(WindowHeight));
                GUILayout.FlexibleSpace(); // Push content to center vertically

                // Centering the label and button using GUIStyle
                GUIStyle centeredLabelStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleCenter
                };

                GUIStyle normalLabelStyle = new GUIStyle(EditorStyles.label)
                {
                    alignment = TextAnchor.MiddleCenter
                };

                GUILayout.Label("We'd love your feedback!", centeredLabelStyle, GUILayout.ExpandWidth(true));
                GUILayout.Label("This Low Poly stylized Nature pack is perfect to use with our Fake Fog asset!", normalLabelStyle, GUILayout.ExpandWidth(true));
                GUILayout.Label("Please leave a review and support us.", normalLabelStyle, GUILayout.ExpandWidth(true));

                GUILayout.Space(10); // Space between labels and buttons

                // Feedback button
                if (GUILayout.Button("Give Feedback", GUILayout.ExpandWidth(true)))
                {
                    OpenFeedbackUrl(AssetStoreFeedbackUrl);
                }

                GUILayout.Space(5); // Space between buttons
                GUILayout.Label("Support us by Buying Fake Fog Asset Pack", normalLabelStyle, GUILayout.ExpandWidth(true));

                // Fake Fog pack button
                if (GUILayout.Button("Buy Fake Fog Asset Pack", GUILayout.ExpandWidth(true)))
                {
                    OpenFeedbackUrl(FakeFogUrl);
                }

                GUILayout.Space(5); // Space between buttons

                // Website button
                if (GUILayout.Button("Visit Our Website", GUILayout.ExpandWidth(true)))
                {
                    OpenFeedbackUrl(WebsiteUrl);
                }

                GUILayout.Space(5); // Space between buttons

                // Itch.io games button with a note
                if (GUILayout.Button("Support Us by Donating on Itch.io and Playing Our Games", GUILayout.ExpandWidth(true)))
                {
                    OpenFeedbackUrl(ItchUrl);
                }

                GUILayout.Label("Note: These are older game jam projects, so quality may vary.", normalLabelStyle, GUILayout.ExpandWidth(true));

                GUILayout.FlexibleSpace(); // Push content to center vertically

                // Move toggle and label to the bottom-left corner
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace(); // Push toggle and label to the left
                bool newShowOnStartup = EditorGUILayout.Toggle("Don't show this again", showOnStartup);
                if (newShowOnStartup != showOnStartup)
                {
                    // Save the new state if it has changed
                    showOnStartup = newShowOnStartup;
                    EditorPrefs.SetBool("ShowFeedbackWindowOnStartupLPSNP", showOnStartup);
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
            }

            private void OpenFeedbackUrl(string url)
            {
                Application.OpenURL(url);
            }

            private void OnDestroy()
            {
                // Save the preference when the window is closed
                EditorPrefs.SetBool("ShowFeedbackWindowOnStartupLPSNP", showOnStartup);
            }
        }
    }
}
