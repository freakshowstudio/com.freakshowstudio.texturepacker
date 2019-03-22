
using System;
using System.IO;

using UnityEngine;
using UnityEditor;


namespace FreakshowStudio.TexturePacker.Editor
{
    public class PackerWizard : ScriptableWizard
    {
        private enum Channel
        {
            Red,
            Green,
            Blue,
            Alpha,
        }

        #region Inspector Variables
        #pragma warning disable 0649
        [SerializeField]
        private Texture2D _redChannelTexture;

        [SerializeField]
        private Channel _redChannelSource = Channel.Red;

        [SerializeField]
        private Texture2D _greenChannelTexture;

        [SerializeField]
        private Channel _greenChannelSource = Channel.Green;

        [SerializeField]
        private Texture2D _blueChannelTexture;

        [SerializeField]
        private Channel _blueChannelSource = Channel.Blue;

        [SerializeField]
        private Texture2D _alphaChannelTexture;

        [SerializeField]
        private Channel _alphaChannelSource = Channel.Alpha;
        #pragma warning restore 0649
        #endregion Inspector Variables

        [MenuItem("Window/Texture Packer")]
        private static void CreateWizard()
        {
            DisplayWizard<PackerWizard>(
                "Texture Packer",
                "Create",
                "Cancel");
        }

        private void OnWizardCreate()
        {
            var areSet =
                _redChannelTexture != null &&
                _greenChannelTexture != null &&
                _blueChannelTexture != null &&
                _alphaChannelTexture != null;

            if (!areSet)
            {
                EditorUtility.DisplayDialog(
                    "Texture Packer",
                    "All texture channels need to be set.",
                    "Ok");
                return;
            }

            var isReadable =
                _redChannelTexture.isReadable &&
                _greenChannelTexture.isReadable &&
                _blueChannelTexture.isReadable &&
                _alphaChannelTexture.isReadable;

            if (!isReadable)
            {
                EditorUtility.DisplayDialog(
                    "Texture Packer",
                    "One or more of the textures are not marked " +
                    "as readable. Please make sure all textures are marked " +
                    "as readable in the import settings.",
                    "Ok");
                return;
            }

            var isSameWidth =
                (_redChannelTexture.width == _greenChannelTexture.width) &&
                (_greenChannelTexture.width == _blueChannelTexture.width) &&
                (_blueChannelTexture.width == _alphaChannelTexture.width);

            var isSameHeight =
                (_redChannelTexture.height == _greenChannelTexture.height) &&
                (_greenChannelTexture.height == _blueChannelTexture.height) &&
                (_blueChannelTexture.height == _alphaChannelTexture.height);

            if (!(isSameWidth && isSameHeight))
            {
                EditorUtility.DisplayDialog(
                    "Texture Packer",
                    "The size of the textures are not equal",
                    "Ok");
                return;
            }

            int width = _redChannelTexture.width;
            int height = _redChannelTexture.height;

            var redPixels = _redChannelTexture.GetPixels32();
            var greenPixels = _greenChannelTexture.GetPixels32();
            var bluePixels = _blueChannelTexture.GetPixels32();
            var alphaPixels = _alphaChannelTexture.GetPixels32();

            var areEqual =
                (redPixels.Length == greenPixels.Length) &&
                (greenPixels.Length == bluePixels.Length) &&
                (bluePixels.Length == alphaPixels.Length);

            if (!areEqual)
            {
                EditorUtility.DisplayDialog(
                    "Texture Packer",
                    "The size of the textures are not equal",
                    "Ok");
                return;
            }

            int l = redPixels.Length;
            var newPixels = new Color32[l];

            for (int i = 0; i < l; i++)
            {
                byte redChannel;
                byte greenChannel;
                byte blueChannel;
                byte alphaChannel;

                switch (_redChannelSource)
                {
                    case Channel.Red:
                        redChannel = redPixels[i].r;
                        break;
                    case Channel.Green:
                        redChannel = redPixels[i].g;
                        break;
                    case Channel.Blue:
                        redChannel = redPixels[i].b;
                        break;
                    case Channel.Alpha:
                        redChannel = redPixels[i].a;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (_greenChannelSource)
                {
                    case Channel.Red:
                        greenChannel = greenPixels[i].r;
                        break;
                    case Channel.Green:
                        greenChannel = greenPixels[i].g;
                        break;
                    case Channel.Blue:
                        greenChannel = greenPixels[i].b;
                        break;
                    case Channel.Alpha:
                        greenChannel = greenPixels[i].a;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (_blueChannelSource)
                {
                    case Channel.Red:
                        blueChannel = bluePixels[i].r;
                        break;
                    case Channel.Green:
                        blueChannel = bluePixels[i].g;
                        break;
                    case Channel.Blue:
                        blueChannel = bluePixels[i].b;
                        break;
                    case Channel.Alpha:
                        blueChannel = bluePixels[i].a;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (_alphaChannelSource)
                {
                    case Channel.Red:
                        alphaChannel = alphaPixels[i].r;
                        break;
                    case Channel.Green:
                        alphaChannel = alphaPixels[i].g;
                        break;
                    case Channel.Blue:
                        alphaChannel = alphaPixels[i].b;
                        break;
                    case Channel.Alpha:
                        alphaChannel = alphaPixels[i].a;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Color32 newColor = new Color32(
                    redChannel,
                    greenChannel,
                    blueChannel,
                    alphaChannel);

                newPixels[i] = newColor;
            }

            var filePath = EditorUtility.SaveFilePanel(
                "Save Texture",
                "",
                "New Texture.png",
                "png");

            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            Texture2D newTexture = new Texture2D(width, height);
            newTexture.SetPixels32(newPixels);

            var bytes = newTexture.EncodeToPNG();

            File.WriteAllBytes(filePath, bytes);

            AssetDatabase.Refresh();
        }

        private void OnWizardOtherButton()
        {
            Close();
        }
    }
}
