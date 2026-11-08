
using UnityEngine;
using Nautilus.Json;
using Nautilus.Options;
using Nautilus.Options.Attributes;

namespace Visor
{
    [Menu("Visor")]
    public class VisorOptions : ConfigFile
    {
        [Choice(Label = "Shadow Mode",
            Options = new[] { "No Shadows", "Hard Shadows Only", "All Shadows" },
            Tooltip = "Choose the mode of realtime shadows in-game. Does not affect baked shadows, whether that be lightmaps or albedo textures.")]
        [OnChange(nameof(ShadowOption_OnChanged))]
        public string shadowOption = "All Shadows";


        [Slider(Label = "Shadow Distance",
            Min = 0f,
            Max = 8192f,
            DefaultValue = 200f,
            Tooltip = "Change the distance shadows are displayed until. Maximum of 8192 meters (the farthest one can swim before being reset)")]
        [OnChange(nameof(ShadowDistance_OnChanged))]
        public float shadowDistance = 200f;
        
        [Choice(Label = "Shadow Resolution", Options = new[] { "Low", "Medium", "High", "Very High" }, Tooltip = "Set the resolution of realtime shadows. Can seriously impact performance at high levels.")]
        [OnChange(nameof(ShadowResolution_OnChanged))]
        public string shadowResolution = "Medium";
        
        [Choice(Label = "Shadow Cascades", Options = new[] {"1", "2", "4"}, Tooltip = "Determines how many cascades realtime shadows are split into. Reduces blockiness.")]
        [OnChange(nameof(ShadowCascade_OnChanged))]
        public string shadowCascades = "4";
        
        // Commented as it doesn't always work, and need to determine if players want this
        /*
        [Slider(Label = "Shadow Cascade Split Distance", Min = 0f, Max = 8192f, DefaultValue = 100, Tooltip = "Change how far until the shadow cascades split between the near and far cascade. Does not apply on 1 cascade mode. On 4 cascades, it is converted into a Vector positioning equivalent.")]
        [OnChange(nameof(ShadowCascadeSplit_OnChanged))]
        public float shadowCascadesSplit = 100f;
        */

        [Choice(Label = "Mipmap Streaming",
            Options = new[] { "Disabled", "Enabled" },
            Tooltip = "Enable or disable mipmap streaming based on camera distance. May improve performance in some scenarios, but can also struggle on some devices.")]
        [OnChange(nameof(StreamingChoice_OnChanged))]
        public string streamingChoice = "Enabled";


        [Choice(Label = "Anisotropic Filtering Mode",
            Options = new[] { "No Filtering", "Enable Filtering", "Force Filtering On" },
            Tooltip = "Set the Anisotropic filtering mode. Fixes the issue of blurry textures in peripheral vision when on.")]
        [OnChange(nameof(AnisoChoice_OnChanged))]
        public string anisoChoice = "No Filtering";


        [Choice(Label = "Soft Particles",
            Options = new[] { "Soft Particles Disabled", "Soft Particles Enabled" },
            Tooltip = "Set whether soft particles are enabled.")]
        [OnChange(nameof(SoftChoice_OnChanged))]
        public string softChoice = "Soft Particles Enabled";

        private void ShadowOption_OnChanged( ChoiceChangedEventArgs<string> e)
        {
            switch (e.Value)
            {
                case "No Shadows":
                    QualitySettings.shadows = ShadowQuality.Disable;
                    ErrorMessage.AddWarning("Shadows disabled.");
                    break;
                case "Hard Shadows Only":
                    QualitySettings.shadows = ShadowQuality.HardOnly;
                    ErrorMessage.AddWarning("Shadows set to hard shadows only.");
                    break;
                case "All Shadows":
                    QualitySettings.shadows = ShadowQuality.All;
                    ErrorMessage.AddWarning("Shadows set to all shadows.");
                    break;
            }
        }

        private void ShadowDistance_OnChanged( SliderChangedEventArgs e)
        {
            QualitySettings.shadowDistance = e.Value;
            ErrorMessage.AddWarning("Shadow distance set to: " + e.Value);
        }

        private void ShadowResolution_OnChanged(ChoiceChangedEventArgs<string> e)
        {
            switch (e.Value)
            {
                case "Low Quality":
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    ErrorMessage.AddWarning("Shadow resolution set to low.");
                    break;
                case "Medium Quality":
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                    ErrorMessage.AddWarning("Shadow resolution set to medium.");
                    break;
                case "High Quality":
                    QualitySettings.shadowResolution = ShadowResolution.High;
                    ErrorMessage.AddWarning("Shadow resolution set to high.");
                    break;
                case "Very High Quality":
                    QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                    ErrorMessage.AddWarning("Shadow resolution set to very high.");
                    break;
            }
        }

        private void ShadowCascade_OnChanged(ChoiceChangedEventArgs<string> e)
        {
            switch (e.Value)
            {
                case "1":
                    QualitySettings.shadowCascades = 1;
                    ErrorMessage.AddWarning("Shadow cascades set to 1 cascade.");
                    break;
                case "2":
                    QualitySettings.shadowCascades = 2;
                    ErrorMessage.AddWarning("Shadow cascades set to 2 cascades.");
                    break;
                case "4":
                    QualitySettings.shadowCascades = 4;
                    ErrorMessage.AddWarning("Shadow cascades set to 4 cascades.");
                    break;
            }
        }

        // Comment reason: see slider above
        /*
        public void ShadowCascadeSplit_OnChanged(SliderChangedEventArgs e)
        {
            if (QualitySettings.shadowCascades == 1)
            {
                // Do nothing!
            }else if (QualitySettings.shadowCascades == 2)
            {
                QualitySettings.shadowCascade2Split = e.Value;
                ErrorMessage.AddWarning("Shadow cascade split distance set to: "  + QualitySettings.shadowCascade2Split);
            }
            else
            {
                QualitySettings.shadowCascade4Split = new Vector3(e.Value * 0.15f, e.Value, Mathf.Min(e.Value * 2.33f, 0.90f));
                ErrorMessage.AddWarning("Shadow cascade split distance set to: " + QualitySettings.shadowCascade4Split);
            }
                    
            
            
        }
    */
        private void AnisoChoice_OnChanged( ChoiceChangedEventArgs<string> e)
        {
            switch (e.Value)
            {
                case "No Filtering":
                    QualitySettings.anisotropicFiltering =  AnisotropicFiltering.Disable;
                    ErrorMessage.AddWarning("Anisotropic filtering disabled.");
                    break;
                case "Enable Filtering":
                    QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
                    ErrorMessage.AddWarning("Anisotropic filtering enabled.");  
                    break;
                case "Force Filtering On":
                    QualitySettings.anisotropicFiltering  = AnisotropicFiltering.ForceEnable;
                    ErrorMessage.AddWarning("Anisotropic filtering force enabled.");
                    break;
                    
            }
        }

        private void SoftChoice_OnChanged( ChoiceChangedEventArgs<string> e)
        {
            switch (e.Value)
            {
                case "Soft Particles Disabled":
                    QualitySettings.softParticles = false;
                    ErrorMessage.AddWarning("Soft particles disabled.");
                    break;
                case "Soft Particles Enabled":
                    QualitySettings.softParticles = true;
                    ErrorMessage.AddWarning("Soft particles enabled.");
                    break;
            }
        }

        private void StreamingChoice_OnChanged( ChoiceChangedEventArgs<string> e)
        {
            switch (e.Value)   
            {
                case  "Disabled":
                    QualitySettings.streamingMipmapsActive = false;
                    ErrorMessage.AddWarning("Streaming mipmaps disabled.");
                    break;
                case "Enabled":
                    QualitySettings.streamingMipmapsActive = true;
                    ErrorMessage.AddWarning("Streaming mipmaps enabled.");
                    break;
            }
        }
    }
}