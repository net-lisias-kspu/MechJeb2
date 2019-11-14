using UnityEngine;

using Log = MechJeb2.Log;

// The previous coroutine behaviour on loading bundles were removed on a somewaht desperate attempt to prevent MechJeb from borking when some other Add'Ons are installed.
// The current guess is Add'Ons with bundled shaders (Firespitter has it, by the way).
// See https://forum.kerbalspaceprogram.com/index.php?/topic/188933-180-modders-notes/

// Note - two days latter, this was proved to be wrong. :P
// https://forum.kerbalspaceprogram.com/index.php?/topic/189545-cant-load-with-mechjeb-installed/&do=findComment&comment=3705625

namespace MuMech
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    public class MechJebBundlesManager : MonoBehaviour
    {
        private const string shaderBundle = "shaders.bundle";
        private string shaderPath;

        private const string diffuseAmbientName = "Assets/Shaders/MJ_DiffuseAmbiant.shader";
        private const string diffuseAmbientIgnoreZName = "Assets/Shaders/MJ_DiffuseAmbiantIgnoreZ.shader";

        public static Shader diffuseAmbient;
        public static Shader diffuseAmbientIgnoreZ;
        public static Texture2D comboBoxBackground;

        internal void Awake()
        {
            string gameDataPath = KSPUtil.ApplicationRootPath + "/GameData/MechJeb2/Bundles/";
            shaderPath = gameDataPath + shaderBundle;
        }

        internal void Start()
        {
            // We do this in MainMenu because something is going on in that scene that kills anything loaded with a bundle
            if (diffuseAmbient)
                Log.info("Shaders already loaded");

            Log.info("Loading Shaders Bundles");

            // Load the font asset bundle
            AssetBundleCreateRequest bundleLoadRequest = AssetBundle.LoadFromFileAsync(shaderPath);

            AssetBundle assetBundle = bundleLoadRequest.assetBundle;
            if (assetBundle == null)
            {
                Log.err("Failed to load AssetBundle {0}", shaderPath);
                return;
            }

            AssetBundleRequest assetLoadRequest = assetBundle.LoadAssetAsync<Shader>(diffuseAmbientName);
            diffuseAmbient = assetLoadRequest.asset as Shader;

            assetLoadRequest = assetBundle.LoadAssetAsync<Shader>(diffuseAmbientIgnoreZName);
            diffuseAmbientIgnoreZ = assetLoadRequest.asset as Shader;

            assetBundle.Unload(false);
            Log.info("Loaded Shaders Bundles");

            comboBoxBackground = new Texture2D(16, 16, TextureFormat.RGBA32, false);
            comboBoxBackground.wrapMode = TextureWrapMode.Clamp;

            for (int x = 0; x < comboBoxBackground.width; x++)
            for (int y = 0; y < comboBoxBackground.height; y++)
            {
                if (x == 0 || x == comboBoxBackground.width-1 || y == 0 || y == comboBoxBackground.height-1)
                    comboBoxBackground.SetPixel(x, y, new Color(0, 0, 0, 1));
                else
                    comboBoxBackground.SetPixel(x, y, new Color(0.05f, 0.05f, 0.05f, 0.95f));
            }

            comboBoxBackground.Apply();
        }
    }
}


